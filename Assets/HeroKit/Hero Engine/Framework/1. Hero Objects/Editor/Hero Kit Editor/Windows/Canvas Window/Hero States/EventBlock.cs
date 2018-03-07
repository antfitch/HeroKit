// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;
using System.Collections.Generic;
using System.Linq;
using HeroKit.Editor.ActionField;
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor
{
    /// <summary>
    /// Block for Hero Event that appears in Hero Kit Editor.
    /// </summary>
    internal class EventBlock
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The hero object.
        /// </summary>
        private static HeroObject heroObject;
        /// <summary>
        /// Name of the block.
        /// </summary>
        private static string blockName = "Event";
        /// <summary>
        /// The Hero Event.
        /// </summary>
        private static HeroEvent eventBlock;
        /// <summary>
        /// The ID of the state that this action exists within.
        /// </summary>
        private static int stateIndex;
        /// <summary>
        /// The ID of the event that this action exists within.
        /// </summary>
        private static int eventIndex;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Block to display on the canvas.
        /// </summary>
        /// <param name="heroKitAction">Hero kit action to display in the block.</param>
        /// <param name="indexState">ID of the state for this event.</param>
        /// <param name="indexEvent">ID of this event.</param>
        public static void Block(HeroObject heroKitObject, int indexState, int indexEvent)
        {
            // exit early if object is null
            if (heroKitObject == null) return;

            // exit early if state no longer exists
            if (heroKitObject.states.states == null || heroKitObject.states.states.Count - 1 < indexState) return;

            // exit early if event no longer exists
            if (heroKitObject.states.states[indexState].heroEvent == null || heroKitObject.states.states[indexState].heroEvent.Count - 1 < indexEvent) return;

            // save the id of the state that this event belongs in
            stateIndex = indexState;
            eventIndex = indexEvent;

            // assign hero object to this class
            heroObject = heroKitObject;
            eventBlock = heroObject.states.states[stateIndex].heroEvent[eventIndex];

            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            // draw heading for block
            DrawHeader();

            // draw body for the block
            DrawBody();
        }
        /// <summary>
        /// Draw the header of the block.
        /// </summary>
        private static void DrawHeader()
        {
            string eventName = "";
            if (eventBlock.name == "")
            {
                if (eventBlock.eventType > 0)
                    eventName = EventTypeField.field.items[eventBlock.eventType - 1];
            }
            else
            {
                eventName = eventBlock.name;
            }

            HeroKitCommon.DrawBlockTitle(blockName + " " + eventIndex + ": " + eventName);
        }
        /// <summary>
        /// Draw the body of the block.
        /// </summary>
        private static void DrawBody()
        {
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);

            // draw name of the block
            eventBlock.name = HeroKitCommon.DrawBlockName(eventBlock.name);

            SimpleLayout.Line();
            SimpleLayout.Space(5);

            DrawEventTypeField();

            DrawIntConditionFields();

            DrawBoolConditionFields();

            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// When the event should run.
        /// </summary>
        private static void DrawEventTypeField()
        {
            SimpleLayout.BeginVertical(Box.StyleB);

            SimpleLayout.BeginHorizontal();
            eventBlock.eventType = EventTypeField.SetValues(heroObject, stateIndex, eventIndex, 80);
            if (eventBlock.eventType == 5) SimpleLayout.Button(Content.AddIcon, addConditionInputList, Button.StyleA, 25);
            SimpleLayout.EndHorizontal();

            // input event
            if (eventBlock.eventType == 5)
            {
                for (int listID = 0; listID < eventBlock.inputConditions.Count; listID++)
                {
                    // exit early if there are no items in this condition
                    if (eventBlock.inputConditions[listID].items == null) break;

                    SimpleLayout.BeginVertical(Box.StyleA);

                    // draw content for condition
                    for (int condID = 0;
                        listID < eventBlock.inputConditions.Count &&
                        condID < eventBlock.inputConditions[listID].items.Count;
                        condID++)
                    {
                        SimpleLayout.BeginHorizontal();
                        if (condID == 0) SimpleLayout.Button(Content.AddIcon, addEventCondition, listID, Button.StyleA, 25);
                        eventBlock.inputConditions[listID].items[condID].inputType = EventInputTypeField.SetValues(heroObject, stateIndex, eventIndex, listID, condID, (condID == 0) ? 42 : 70);
                        int inputType = eventBlock.inputConditions[listID].items[condID].inputType;

                        // mouse
                        if (inputType == 1)
                        {
                            eventBlock.inputConditions[listID].items[condID].key = EventMouseField.SetValues(heroObject, stateIndex, eventIndex, listID, condID, 0);
                        }
                        // keyboard
                        else if (inputType == 2)
                        {
                            eventBlock.inputConditions[listID].items[condID].key = EventKeyField.SetValues(heroObject, stateIndex, eventIndex, listID, condID, 0);
                        }
                        // joystick
                        else if (inputType == 3)
                        {
                            eventBlock.inputConditions[listID].items[condID].key = EventJoystickField.SetValues(heroObject, stateIndex, eventIndex, listID, condID, 0);
                        }
                        // touch
                        if (inputType == 4)
                        {
                            eventBlock.inputConditions[listID].items[condID].key = EventTouchField.SetValues(heroObject, stateIndex, eventIndex, listID, condID, 0);
                        }

                        eventBlock.inputConditions[listID].items[condID].pressType = EventPressTypeField.SetValues(heroObject, stateIndex, eventIndex, listID, condID, 0);
                        SimpleLayout.Button(Content.DeleteIcon, deleteEventCondition, listID, condID, Button.StyleA, 25);
                        SimpleLayout.EndHorizontal();
                    }

                    SimpleLayout.EndVertical();
                }
            }

            // message event
            else if (eventBlock.eventType == 6)
            {
                // what kind of tags are acceptable?
                eventBlock.messageSettings[0] = EventMessageTagField.SetValues(heroObject, stateIndex, eventIndex, 80);

                // if a specific tag is needed, get it
                if (eventBlock.messageSettings[0] == 4)
                {
                    eventBlock.messageSettings[1] = TagListField.SetEventValues(heroObject, stateIndex, eventIndex, 80);
                    eventBlock.messageTag = "";
                    if (eventBlock.messageSettings[1] > 0)
                    {
                        eventBlock.messageTag = UnityEditorInternal.InternalEditorUtility.tags[eventBlock.messageSettings[1] - 1];
                    }
                }

                // what kind of interaction is possible (on touch, interact, on leave?)
                eventBlock.messageSettings[2] = EventMessageInteractField.SetValues(heroObject, stateIndex, eventIndex, 80);

                // if message type = Touch Object
                if (eventBlock.messageSettings[2] == 3 || eventBlock.messageSettings[2] == 4)
                {
                    eventBlock.messageSettings[3] = EventMessageRunField.SetValues(heroObject, stateIndex, eventIndex, 80);
                }
            }

            else
            {
                // clear the input list if we're using another type of condition
                eventBlock.inputConditions = new List<ConditionInputList>();
            }

            SimpleLayout.EndVertical();
        }

        // --------------------------------------------------------------
        // Methods (Conditional Fields)
        // --------------------------------------------------------------

        /// <summary>
        /// Integer conditions that must be satisfied for the event to run.
        /// </summary>
        private static void DrawIntConditionFields()
        {
            SimpleLayout.BeginVertical(Box.StyleB);

            // ROW: START -----------------------------------------
            SimpleLayout.BeginHorizontal();
            // COLUMN 1: START ------------------------------------
            SimpleLayout.BeginVertical();
            SimpleLayout.Label("INTEGERS (these conditions must be met before this event can run):");
            SimpleLayout.EndVertical();
            // COLUMN 1: END --------------------------------------
            SimpleLayout.Space();
            // COLUMN 2: START ------------------------------------
            SimpleLayout.BeginVertical(); 
            SimpleLayout.Button(Content.AddIcon, addIntCondition, Button.StyleA, 25);
            SimpleLayout.EndVertical();
            // COLUMN 2: END --------------------------------------
            SimpleLayout.EndHorizontal();
            // ROW: END -------------------------------------------

            if (eventBlock.intConditions.Count > 0)
                SimpleLayout.Line();

            // List of entries
            for (int i = 0; i < eventBlock.intConditions.Count; i++)
            {
                // ROW 1: START ---------------------------------------
                SimpleLayout.BeginHorizontal();

                // COLUMN 1: START ------------------------------------
                SimpleLayout.BeginVertical();

                // VALUE 1
                SimpleLayout.BeginHorizontal();
                GetIntegerField.BuildEventField("Value 1:", eventBlock.intConditions[i].itemA, heroObject);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // OPERATOR
                SimpleLayout.BeginHorizontal();
                SimpleLayout.Space(60);
                eventBlock.intConditions[i].operatorID = new OperatorField().SetValues(eventBlock.intConditions[i].operatorID, 0);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                //SimpleLayout.Space(4);

                // VALUE 2
                SimpleLayout.BeginHorizontal();
                GetIntegerField.BuildEventField("Value 2:", eventBlock.intConditions[i].itemB, heroObject);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                SimpleLayout.EndVertical();
                // COLUMN 1: END --------------------------------------

                // COLUMN 2: START ------------------------------------
                SimpleLayout.BeginVertical();
                SimpleLayout.Button(Content.DeleteIcon, deleteIntCondition, i, Button.StyleA, 25);
                SimpleLayout.EndVertical();
                // COLUMN 2: END --------------------------------------

                SimpleLayout.EndHorizontal();
                // ROW 1: END -----------------------------------------

                // ROW 2: START ---------------------------------------
                if (i < eventBlock.intConditions.Count-1)
                    SimpleLayout.Line();
                // ROW 2: END -----------------------------------------

            }

            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Add a condition to the integer condition list.
        /// </summary>
        private static void addIntCondition()
        {
            HeroKitCommon.deselectField();
            eventBlock.intConditions.Add(new ConditionIntFields());
        }
        /// <summary>
        /// Delete a condition from the integer condition list. 
        /// </summary>
        /// <param name="index"></param>
        private static void deleteIntCondition(int index)
        {
            if (eventBlock.intConditions.Count > index)
            {
                HeroKitCommon.deselectField();
                eventBlock.intConditions.RemoveAt(index);
            }
        }

        /// <summary>
        /// Bool conditions that must be satisfied for the event to run.
        /// </summary>
        private static void DrawBoolConditionFields()
        {
            SimpleLayout.BeginVertical(Box.StyleB);

            // ROW: START -----------------------------------------
            SimpleLayout.BeginHorizontal();
            // COLUMN 1: START ------------------------------------
            SimpleLayout.BeginVertical();
            SimpleLayout.Label("BOOLS (these conditions must be met before this event can run):");
            SimpleLayout.EndVertical();
            // COLUMN 1: END --------------------------------------
            SimpleLayout.Space();
            // COLUMN 2: START ------------------------------------
            SimpleLayout.BeginVertical();
            SimpleLayout.Button(Content.AddIcon, addBoolCondition, Button.StyleA, 25);
            SimpleLayout.EndVertical();
            // COLUMN 2: END --------------------------------------
            SimpleLayout.EndHorizontal();
            // ROW: END -------------------------------------------

            if (eventBlock.boolConditions.Count > 0)
                SimpleLayout.Line();

            // List of entries
            for (int i = 0; i < eventBlock.boolConditions.Count; i++)
            {
                // ROW 1: START ---------------------------------------
                SimpleLayout.BeginHorizontal();

                // COLUMN 1: START ------------------------------------
                SimpleLayout.BeginVertical();

                // VALUE 1
                SimpleLayout.BeginHorizontal();
                GetBoolField.BuildEventField("Value 1:", eventBlock.boolConditions[i].itemA, heroObject);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // OPERATOR
                SimpleLayout.BeginHorizontal();
                SimpleLayout.Space(60);
                eventBlock.boolConditions[i].operatorID = new TrueFalseField().SetValues(eventBlock.boolConditions[i].operatorID, 0);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                //SimpleLayout.Space(4);

                // VALUE 2
                SimpleLayout.BeginHorizontal();
                GetBoolField.BuildEventField("Value 2:", eventBlock.boolConditions[i].itemB, heroObject);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                SimpleLayout.EndVertical();
                // COLUMN 1: END --------------------------------------

                // COLUMN 2: START ------------------------------------
                SimpleLayout.BeginVertical();
                SimpleLayout.Button(Content.DeleteIcon, deleteBoolCondition, i, Button.StyleA, 25);
                SimpleLayout.EndVertical();
                // COLUMN 2: END --------------------------------------

                SimpleLayout.EndHorizontal();
                // ROW 1: END -----------------------------------------

                // ROW 2: START ---------------------------------------
                if (i < eventBlock.boolConditions.Count - 1)
                    SimpleLayout.Line();
                // ROW 2: END -----------------------------------------

            }

            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Add a condition to the bool condition list.
        /// </summary>
        private static void addBoolCondition()
        {
            HeroKitCommon.deselectField();
            eventBlock.boolConditions.Add(new ConditionBoolFields());
        }
        /// <summary>
        /// Delete a condition from the bool condition list. 
        /// </summary>
        /// <param name="index"></param>
        private static void deleteBoolCondition(int index)
        {
            if (eventBlock.boolConditions.Count > index)
            {
                HeroKitCommon.deselectField();
                eventBlock.boolConditions.RemoveAt(index);
            }
        }

        /// <summary>
        /// Add an input condition list. 
        /// </summary>
        private static void addConditionInputList()
        {
            HeroKitCommon.deselectField();
            ConditionInputList list = new ConditionInputList();
            list.items = new List<ConditionInputField>();
            list.items.Add(new ConditionInputField());
            eventBlock.inputConditions.Add(list);
        }
        /// <summary>
        /// Add a condition to an input condition list.
        /// </summary>
        /// <param name="listID">ID assigned to the input condition list.</param>
        private static void addEventCondition(int listID)
        {
            HeroKitCommon.deselectField();
            eventBlock.inputConditions[listID].items.Add(new ConditionInputField());
        }
        /// <summary>
        /// Delete a condition from an input condition list.
        /// </summary>
        /// <param name="listID">ID assigned to the input condition list.</param>
        /// <param name="conditionID">ID assigned to the condition.</param>
        private static void deleteEventCondition(int listID, int conditionID)
        {
            HeroKitCommon.deselectField();

            if (eventBlock.inputConditions.Count > listID)
            {
                if (eventBlock.inputConditions[listID].items.Count > conditionID)
                {
                    eventBlock.inputConditions[listID].items.RemoveAt(conditionID);
                }

                if (eventBlock.inputConditions[listID].items.Count == 0)
                {
                    eventBlock.inputConditions.RemoveAt(listID);
                }
            }
        }
    }
}