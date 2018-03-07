// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEditor;
using HeroKit.Scene;
using UnityEngine;


namespace HeroKit.Editor
{
    /// <summary>
    /// Main Menu for the Hero Kit Editor. (Hero Object)
    /// </summary>
    internal class HeroObjectMenuBlock : EditorWindow
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The hero object.
        /// </summary>
        private static HeroObject heroObject;
        /// <summary>
        /// The hero kit editor.
        /// </summary>
        private static HeroKitEditor heroEditor;

        /// <summary>
        /// Property menu is in focus.
        /// </summary>
        public static bool propertyFocus = false;
        /// <summary>
        /// State menu is in focus.
        /// </summary>
        public static bool stateFocus = false;
        /// <summary>
        /// Variable menu is in focus.
        /// </summary>
        public static bool variableFocus = false;
        /// <summary>
        /// Global menu is in focus.
        /// </summary>
        public static bool globalFocus = false;
        /// <summary>
        /// Settings menu is in focus.
        /// </summary>
        public static bool settingsFocus = false;

        /// <summary>
        /// The type of object selected in the main menu. 
        /// state (1), event (2), action (3), properties (4), variables (5), globals (6), settings (7)
        /// </summary>
        public static int typeID = 0;
        /// <summary>
        /// ID of the currently selected property in the main menu.
        /// </summary>
        public static int propertyID = 0;
        /// <summary>
        /// ID of the currently selected state in the main menu.
        /// </summary>
        public static int stateID = 0;
        /// <summary>
        /// ID of the currently selected event in the main menu.
        /// </summary>
        public static int eventID = 0;
        /// <summary>
        /// ID of the currently selected action in the main menu.
        /// </summary>
        public static int actionID = 0;
        /// <summary>
        /// ID of the currently selected variable type in the main menu. (ex. int, float)
        /// </summary>
        public static int variableID = 0;
        /// <summary>
        /// ID of the currently selected global type in the main menu. (ex. int, float)
        /// </summary>
        public static int globalID = 0;

        /// <summary>
        /// The property ID of whatever object was clicked to display the context menu.
        /// If there is no ID, this value is -1.
        /// </summary>
        public static int propertyIndexContext = -1;
        /// <summary>
        /// The state ID of whatever object was clicked to display the context menu.
        /// If there is no ID, this value is -1.
        /// </summary>
        public static int stateIndexContext = -1;
        /// <summary>
        /// The event ID of whatever object was clicked to display the context menu.
        /// If there is no ID, this value is -1.
        /// </summary>
        public static int eventIndexContext = -1;
        /// <summary>
        /// The action ID of whatever object was clicked to display the context menu.
        /// If there is no ID, this value is -1.
        /// </summary>
        public static int actionIndexContext = -1;

        /// <summary>
        /// A character that looks like a long dash. This goes in front of things in the menu that cannot be expanded.
        /// </summary>
        public static string textIcon = '\u2014'.ToString();
        /// <summary>
        /// Indent level for things in the menu that need to be indented.
        /// </summary>
        public static int indentLevel = 18;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Block in which to display the main menu. 
        /// </summary>
        /// <param name="heroKitObject">Hero object info to display in the menu.</param>
        /// <param name="heroKitEditor">Hero kit editor.</param>
        public static void Block(HeroObject heroKitObject, HeroKitEditor heroKitEditor)
        {
            // exit early if object is null
            if (heroKitObject == null)
            {
                return;
            }

            // assign hero object to this class
            heroObject = heroKitObject;

            // save the editor
            heroEditor = heroKitEditor;

            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            // draw the properties
            //PropertyMenuBlock.Block(heroObject);

            // draw the list of hero properties
            PropertyListMenuBlock.Block(heroObject);

            // draw the states
            StateMenuBlock.Block(heroObject);

            // draw the variables
            VariablesMenuBlock.Block(heroObject);

            // draw the global variables
            GlobalsMenuBlock.Block(heroObject);
        }

        // --------------------------------------------------------------
        // Methods (On Click)
        // --------------------------------------------------------------

        /// <summary>
        /// Change the selection in the menu.
        /// </summary>
        public static void changeSelection()
        {
            //// property menu
            //if (propertyFocus)
            //{
            //    switch (Event.current.keyCode)
            //    {
            //        case KeyCode.DownArrow:
            //            gotoStatesFromProperties();
            //            break;
            //    }
            //}

            // property menu
            if (propertyFocus)
            {
                switch (Event.current.keyCode)
                {
                    case KeyCode.DownArrow:
                        if (heroObject.propertiesList.visible)
                            gotoPropertiesDown();
                        else
                            gotoStatesFromProperties();
                        break;

                    case KeyCode.UpArrow:
                        if (propertyID > -1)
                            gotoPropertiesUp();
                        else
                            gotoPropertiesTitle();
                        break;

                    case KeyCode.Return:
                        heroObject.propertiesList.visible = !heroObject.propertiesList.visible;
                        break;
                }
            }

            // state menu (states)
            else if (stateFocus && typeID == 1)
            {
                switch (Event.current.keyCode)
                {
                    case KeyCode.DownArrow:

                        // states are expanded
                        if (heroObject.states.visible)
                        {
                            // move from States to first state
                            if (stateID == -1)
                                gotoStateDown();

                            // if selected state is expanded, go to first event in state
                            else if (heroObject.states.states[stateID].visible)
                                gotoEventsFromStateDown();

                            // go to the next state or to variables
                            else
                                gotoStateDown();
                        }

                        // states are collapsed
                        else
                            gotoVariablesFromStates();

                        break;

                    case KeyCode.UpArrow:
                        // go to properties
                        if (stateID == -1)
                            gotoPropertiesFromStates();
                        // go to states
                        if (stateID == 0)
                            gotoStateUp();
                        // go to previous state
                        else if (stateID > 0 && !heroObject.states.states[stateID - 1].visible)
                            gotoStateUp();
                        // go to last event in previous state
                        else if (stateID > 0 && heroObject.states.states[stateID - 1].visible)
                            gotoEventsFromStateUp();
                        break;

                    case KeyCode.Return:
                        if (stateID == -1)
                            heroObject.states.visible = !heroObject.states.visible;
                        else
                            heroObject.states.states[stateID].visible = !heroObject.states.states[stateID].visible;
                        break;
                }
            }

            // state menu (events)
            else if (stateFocus && typeID == 2)
            {
                switch (Event.current.keyCode)
                {
                    case KeyCode.DownArrow:

                        // events are expanded
                        if (heroObject.states.states[stateID].visible)
                        {
                            // move from state to first event
                            if (eventID == -1)
                                gotoEventDown();

                            // if selected event is expanded, go to first action in event
                            else if (heroObject.states.states[stateID].heroEvent[eventID].visible &&
                                     heroObject.states.states[stateID].heroEvent[eventID].actions.Count > 0)
                                gotoActionsFromEventDown();

                            // go to the next event or to next state
                            else
                                gotoEventDown();
                        }

                        // events are collapsed
                        else
                            gotoStateFromEventDown();

                        break;

                    case KeyCode.UpArrow:
                        if (eventID > 0)
                            gotoEventUp();
                        else
                            gotoStateFromEventUp();
                        break;

                    case KeyCode.Return:
                        heroObject.states.states[stateID].heroEvent[eventID].visible = !heroObject.states.states[stateID].heroEvent[eventID].visible;
                        break;
                }
            }

            // state menu (actions)
            else if (stateFocus && typeID == 3)
            {
                switch (Event.current.keyCode)
                {
                    case KeyCode.DownArrow:

                        // actions are expanded
                        if (heroObject.states.states[stateID].heroEvent[eventID].visible)
                            gotoActionDown();

                        // actions are collapsed
                        else
                            gotoEventFromActionDown();

                        break;

                    case KeyCode.UpArrow:
                        if (actionID > 0)
                            gotoActionUp();
                        else
                            gotoEventFromActionUp();
                        break;

                    case KeyCode.Return:
                        heroObject.states.states[stateID].heroEvent[eventID].actions[actionID].visible = !heroObject.states.states[stateID].heroEvent[eventID].actions[actionID].visible;
                        break;
                }
            }

            // variable menu
            else if (variableFocus)
            {
                switch (Event.current.keyCode)
                {
                    case KeyCode.DownArrow:
                        if (heroObject.lists.visible)
                            gotoVariablesDown();
                        else
                            gotoGlobalsFromVariables();
                        break;

                    case KeyCode.UpArrow:
                        if (variableID > -1)
                            gotoVariablesUp();
                        else
                            gotoStatesFromVariables();
                        break;

                    case KeyCode.Return:
                        heroObject.lists.visible = !heroObject.lists.visible;
                        break;
                }
            }

            // globals menu
            else if (globalFocus)
            {
                switch (Event.current.keyCode)
                {
                    case KeyCode.DownArrow:
                        if (heroObject.globalsVisible)
                            gotoGlobalsDown();
                        break;

                    case KeyCode.UpArrow:
                        if (globalID > -1)
                            gotoGlobalsUp();
                        else
                            gotoVariablesFromGlobals();
                        break;

                    case KeyCode.Return:
                        heroObject.globalsVisible = !heroObject.globalsVisible;
                        break;
                }
            }

            // refresh the editor
            heroEditor.Repaint();
        }

        /// <summary>
        /// Go to Properties from first property.
        /// </summary>
        private static void gotoPropertiesTitle()
        {
            PropertyListMenuBlock.showBlockTitle();
        }
        /// <summary>
        /// Go down to property X.
        /// </summary>
        private static void gotoPropertiesDown()
        {
            int count = heroObject.propertiesList.properties.Count;

            // go to next property in the list
            if (propertyID < count - 1)
            {
                propertyID++;
            }

            // next property does not exist. go to states
            else
            {
                gotoStatesFromProperties();
            }
        }
        /// <summary>
        /// Go up to property X.
        /// </summary>
        private static void gotoPropertiesUp()
        {
            propertyID--;
        }
        /// <summary>
        /// Go to States from Properties using arrow keys.
        /// </summary>
        private static void gotoStatesFromProperties()
        {
            StateMenuBlock.showBlockTitle();
        }

        /// <summary>
        /// Go up to Properties from States.
        /// </summary>
        private static void gotoPropertiesFromStates()
        {
            // if property list is expanded and if there are any properties, go to the last property.
            // if property list is expanded and there are no properties, go to properties heading
            if (heroObject.propertiesList.visible)
            {
                int propertyCount = heroObject.propertiesList.properties.Count;
                if (propertyCount > 0)
                {
                    PropertyListMenuBlock.showBlockContent(propertyCount - 1);
                }
                else
                {
                    gotoPropertiesTitle();
                }
            }

            // go to state heading
            else
            {
                gotoPropertiesTitle();
            }
        }
        /// <summary>
        /// Go down to state X using arrow keys.
        /// </summary>
        private static void gotoStateDown()
        {
            int count = heroObject.states.states.Count;

            // go to next state in the list
            if (stateID < count - 1)
            {
                stateID++;
            }

            // next state does not exist. go to variables
            else
            {
                gotoVariablesFromStates();
            }
        }
        /// <summary>
        /// Go up to state X using arrow keys.
        /// </summary>
        private static void gotoStateUp()
        {
            stateID--;
        }
        /// <summary>
        /// Go down to Events in a state using arrow keys.
        /// </summary>
        private static void gotoEventsFromStateDown()
        {
            if (heroObject.states.states[stateID].heroEvent.Count > 0)
            {
                EventMenuBlock.showBlockContent(0, stateID);
            }
            else
            {
                gotoStateDown();
            }
            
        }
        /// <summary>
        /// Go up to Events in a state using arrow keys.
        /// </summary>
        private static void gotoEventsFromStateUp()
        {
            int count = heroObject.states.states[stateID-1].heroEvent.Count;
            if (count > 0)
            {
                stateID--;
                eventID = count - 1;
                EventMenuBlock.showBlockContent(count - 1, stateID);
            }
            else
            {
                gotoStateUp();
            }
        }
        /// <summary>
        /// Go down to Variables from States.
        /// </summary>
        private static void gotoVariablesFromStates()
        {
            VariablesMenuBlock.showBlockTitle();
        }

        /// <summary>
        /// Go up to state X from Events in that state.
        /// </summary>
        private static void gotoStateFromEventUp()
        {
            StateMenuBlock.showBlockContent(stateID);
        }
        /// <summary>
        /// Go down to event X.
        /// </summary>
        private static void gotoEventDown()
        {
            int count = heroObject.states.states[stateID].heroEvent.Count;

            // go to next event in the list
            if (eventID < count - 1)
            {
                eventID++;
            }

            // next event does not exist. go to variables
            else
            {
                gotoStateFromEventDown();
            }
        }
        /// <summary>
        /// Go up to event X.
        /// </summary>
        private static void gotoEventUp()
        {
            eventID--;

            // if event is expanded and there are actions inside, go to last action
            if (heroObject.states.states[stateID].heroEvent[eventID].visible)
            {
                int actionsCount = heroObject.states.states[stateID].heroEvent[eventID].actions.Count;
                if (actionsCount > 0)
                {
                    ActionMenuBlock.showBlockContent(actionsCount-1, eventID, stateID);
                }
            }
        }
        /// <summary>
        /// Go down to state X from last event in previous state.
        /// </summary>
        private static void gotoStateFromEventDown()
        {
            int stateCount = heroObject.states.states.Count;

            if (stateID < stateCount - 1)
            {
                stateID++;
                StateMenuBlock.showBlockContent(stateID);
            }
            else
            {
                gotoVariablesFromStates();
            }
        }
        /// <summary>
        /// Go down to first action in event X. 
        /// </summary>
        private static void gotoActionsFromEventDown()
        {
            ActionMenuBlock.showBlockContent(0, eventID, stateID);
        }

        /// <summary>
        /// Go up to event X from first action in the event.
        /// </summary>
        private static void gotoEventFromActionUp()
        {
            EventMenuBlock.showBlockContent(eventID, stateID);
        }
        /// <summary>
        /// Go down to action X.
        /// </summary>
        private static void gotoActionDown()
        {
            int count = heroObject.states.states[stateID].heroEvent[eventID].actions.Count;

            // go to next action in the list
            if (actionID < count - 1)
            {
                actionID++;
            }

            // next action does not exist. go to next event.
            else
            {
                gotoEventFromActionDown();
            }
        }
        /// <summary>
        /// Go up to action X.
        /// </summary>
        private static void gotoActionUp()
        {
            actionID--;
        }
        /// <summary>
        /// Go down to event X from last action in previous event.
        /// </summary>
        private static void gotoEventFromActionDown()
        {
            int eventCount = heroObject.states.states[stateID].heroEvent.Count;

            // go to next event
            if (eventID < eventCount - 1)
            {
                eventID++;
                EventMenuBlock.showBlockContent(eventID, stateID);
            }

            // go to next state
            else
            {
                gotoStateFromEventDown();
            }
        }

        /// <summary>
        /// Go down to variable type X.
        /// </summary>
        private static void gotoVariablesDown()
        {
            int count = 7;

            if (variableID < count - 1)
            {
                variableID = goDownHeroList(variableID);
            }
            else
            {
                gotoGlobalsFromVariables();
            }

        }
        /// <summary>
        /// Go up to variable type X.
        /// </summary>
        private static void gotoVariablesUp()
        {
            variableID = goUpHeroList(variableID);
        }
        /// <summary>
        /// Go down to Globals from Variables. 
        /// </summary>
        private static void gotoGlobalsFromVariables()
        {
            GlobalsMenuBlock.showBlockTitle();
        }
        /// <summary>
        /// Go up to States from Variables.
        /// </summary>
        private static void gotoStatesFromVariables()
        {
            // if states is expanded and if there are any states, go to the last state.
            // if states is expanded and there are no states, go to states heading
            if (heroObject.states.visible)
            {
                int stateCount = heroObject.states.states.Count;
                if (stateCount > 0)
                {
                    int eventCount = heroObject.states.states[stateCount - 1].heroEvent.Count;

                    // if there are no events in the last state, go to the last state
                    if (eventCount <= 0)
                    {
                        StateMenuBlock.showBlockContent(stateCount - 1);
                    }

                    // if states > state is expanded, go to the last event 
                    // if states > state is not expanded, to to the last state
                    else if (eventCount > 0)
                    {
                        // if states > state is expanded, go to the last event
                        if (heroObject.states.states[stateCount - 1].visible)
                        {
                            int actionCount = heroObject.states.states[stateCount - 1].heroEvent[eventCount-1].actions.Count;

                            // if there are no actions in the last event, go to the last event
                            if (actionCount <= 0)
                            {
                                EventMenuBlock.showBlockContent(eventCount - 1, stateCount - 1);
                            }

                            // if states > state > event is expanded, go to to last action
                            // if states > state > event is not expanded, go to the last event
                            else if (actionCount > 0)
                            {
                                // if state > event is expanded, go to the last action
                                if (heroObject.states.states[stateCount - 1].heroEvent[eventCount - 1].visible)
                                {
                                    ActionMenuBlock.showBlockContent(actionCount - 1, eventCount - 1, stateCount - 1);
                                }
                                else
                                {
                                    EventMenuBlock.showBlockContent(eventCount - 1, stateCount - 1);
                                }
                            }

                        }

                        // if states > state is not expanded, to to the last state
                        else
                        {
                            StateMenuBlock.showBlockContent(stateCount - 1);
                        }
                    }
                }
                else
                {
                    StateMenuBlock.showBlockTitle();
                }
            }

            // go to state heading
            else
            {
                StateMenuBlock.showBlockTitle();
            }
        }

        /// <summary>
        /// Go down to global type X.
        /// </summary>
        private static void gotoGlobalsDown()
        {
            int count = 7;
            if (globalID < count - 1)
            {
                globalID = goDownHeroList(globalID);
            }
        }
        /// <summary>
        /// Go up to global type X.
        /// </summary>
        private static void gotoGlobalsUp()
        {
            globalID = goUpHeroList(globalID);
        }
        /// <summary>
        /// Go up to Variables from Globals.
        /// </summary>
        private static void gotoVariablesFromGlobals()
        {
            if (heroObject.lists.visible)
            {
                int variableCount = 7;
                VariablesMenuBlock.showBlockContent(variableCount - 1);
            }
            else
            {
                VariablesMenuBlock.showBlockTitle();
            }
        }

        public static int goDownHeroList(int itemID)
        {
            int newItemID = itemID;
            switch (itemID)
            {
                case -1: // Variable List to integer
                    newItemID = 0;
                    break;
                case 0: // integer to float
                    newItemID = 5;
                    break;
                case 1: // bool to string
                    newItemID = 2;
                    break;
                case 2: // string to game object
                    newItemID = 3;
                    break;
                case 3: // game object to hero object
                    newItemID = 4;
                    break;
                case 4: // hero object to unity object
                    newItemID = 6;
                    break;
                case 5: // float to bool
                    newItemID = 1;
                    break;
            }
            return newItemID;
        }
        public static int goUpHeroList(int itemID)
        {
            int newItemID = itemID;
            switch (itemID)
            {
                case 0: // integer to variable list
                    newItemID = -1;
                    break;
                case 1: // bool to float
                    newItemID = 5;
                    break;
                case 2: // string to bool
                    newItemID = 1;
                    break;
                case 3: // game object to string
                    newItemID = 2;
                    break;
                case 4: // hero object to game object
                    newItemID = 3;
                    break;
                case 5: // float to int
                    newItemID = 0;
                    break;
                case 6: // unity object to hero object
                    newItemID = 4;
                    break;
            }
            return newItemID;
        }
    }
}