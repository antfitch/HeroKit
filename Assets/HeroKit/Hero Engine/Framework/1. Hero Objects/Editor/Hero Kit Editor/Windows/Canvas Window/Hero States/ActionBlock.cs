// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;

namespace HeroKit.Editor
{
    /// <summary>
    /// Block for Hero Actions that appears in Hero Kit Editor.
    /// </summary>
    internal class ActionBlock
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
        private static string blockName = "Action";
        /// <summary>
        /// The hero action.
        /// </summary>
        private static HeroAction action;
        /// <summary>
        /// The ID of the state that this action exists within.
        /// </summary>
        private static int stateIndex;
        /// <summary>
        /// The ID of the event that this action exists within.
        /// </summary>
        private static int eventIndex;
        /// <summary>
        /// The ID of this action.
        /// </summary>
        private static int actionIndex;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Block to display on the canvas.
        /// </summary>
        /// <param name="heroKitAction">Hero kit action to display in the block.</param>
        public static void Block(HeroObject heroKitObject, int indexState, int indexEvent, int indexAction)
        {
            // exit early if object is null
            if (heroKitObject == null) return;

            // exit early if state no longer exists
            if (heroKitObject.states.states == null || heroKitObject.states.states.Count-1 < indexState) return;

            // exit early if event no longer exists
            if (heroKitObject.states.states[indexState].heroEvent == null || heroKitObject.states.states[indexState].heroEvent.Count-1 < indexEvent) return;

            // exit early if action no longer exists
            if (heroKitObject.states.states[indexState].heroEvent[indexEvent].actions == null || heroKitObject.states.states[indexState].heroEvent[indexEvent].actions.Count-1 < indexAction) return;

            // save the id of the state that this event belongs in
            stateIndex = indexState;
            eventIndex = indexEvent;
            actionIndex = indexAction;

            // assign hero object to this class
            heroObject = heroKitObject;
            action = heroObject.states.states[stateIndex].heroEvent[eventIndex].actions[actionIndex];

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

            // draw contents of block
            DrawBody();
        }
        /// <summary>
        /// Draw the header of the block.
        /// </summary>
        private static void DrawHeader()
        {
            // get the prefix to show before the name of the item
            string prefix = (action.actionTemplate != null) ? action.actionTemplate.title : "";
            string actionName = action.name;
            if (actionName == "" && action.actionTemplate != null)
                actionName = action.actionTemplate.name;
            string name = blockName + " " + actionIndex + ": " + prefix + actionName;
            HeroKitCommon.DrawBlockTitle(name);
        }
        /// <summary>
        /// Draw the body of the block.
        /// </summary>
        private static void DrawBody()
        {
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);

            // draw name of the block
            action.name = HeroKitCommon.DrawBlockName(action.name);

            SimpleLayout.Line();

            // get current action template on hero object
            HeroKitAction oldTemplate = action.actionTemplate;

            // draw action field for block
            SimpleLayout.Space(5);
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label("Action:");
			action.actionTemplate = SimpleLayout.ObjectField(action.actionTemplate, HeroKitCommon.GetWidthForField(85));

            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            // SHOW FIELDS FOR SPECIFIC ACTION
            if (action.actionTemplate != null)
            {
                SimpleLayout.Space(5);
                SimpleLayout.Line();
                SimpleLayout.Space(5);

                // build fields
                ActionBlockBuilder.BuildFields(heroObject, action, action.actionTemplate, oldTemplate);

                // build description for action
                SimpleLayout.Space(5);
                SimpleLayout.Line();
                SimpleLayout.Label("Description:");
                SimpleLayout.Label(action.actionTemplate.description, true);
            }


            SimpleLayout.EndVertical();
        }
    }
}