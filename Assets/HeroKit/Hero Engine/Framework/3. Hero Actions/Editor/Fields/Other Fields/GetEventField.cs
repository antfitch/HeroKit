// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene;
using SimpleGUI;
using System.Collections.Generic;
using HeroKit.Editor.ActionBlockFields;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with events.
    /// </summary>
    public static class GetEventField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get an event.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="heroObject">Hero object that is the target of this action.</param>
        public static void BuildField(string title, HeroActionParams actionParams, HeroActionField actionField, HeroObject heroObject)
        {
            EventFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "") SimpleLayout.Label(data.title);

            // save target object
            data.targetHeroObject = heroObject;

            // draw states
            if (data.targetHeroObject != null)
            {
                SimpleLayout.Label("State:");
                List<HeroState> states = new List<HeroState>();
                if (data.targetHeroObject != null) states = data.targetHeroObject.states.states;
                data = GetTargetStateEvent(data, states);

                // draw events for state
                if (data.stateID > 0)
                {
                    SimpleLayout.Label("Event:");
                    List<HeroEvent> items = new List<HeroEvent>();
                    if (data.targetHeroObject != null && data.stateID > 0) items = data.targetHeroObject.states.states[data.stateID - 1].heroEvent;
                    data = BuildEventFieldList(data, items);
                }
            }


            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.heroObjects[0] = data.targetHeroObject;
            actionField.ints[0] = data.objectType;
            actionField.ints[1] = data.objectID;
            actionField.ints[2] = data.fieldID;
            actionField.ints[3] = data.stateID;
            actionField.ints[4] = data.heroGUID;
            actionField.ints[5] = data.fieldType;
            actionField.strings[0] = data.objectName;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            //SimpleLayout.Space();
            //SimpleLayout.EndHorizontal();
        }

        // --------------------------------------------------------------
        // Helpers
        // --------------------------------------------------------------

        /// <summary>
        /// Select a state on a hero object.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="items">The states on the hero object.</param>
        /// <returns>The data for this action field.</returns>
        private static EventFieldData GetTargetStateEvent(EventFieldData data, List<HeroState> items)
        {
            // hero object does not exist on game object or int list is empty
            if (data.targetHeroObject == null || items.Count == 0)
            {
                SimpleLayout.Label("[No States]");
                data.stateID = 0;
            }
            // everything looks okay. draw list.
            else
            {
                // if we are referencing a field that no longer exists in list, reset field
                if (items.Count < data.stateID)
                    data.stateID = 0;

                // draw the list
                data.stateID = new HeroField.StateListField().SetValues(data.stateID, items, 0);
            }

            return data;
        }
        /// <summary>
        /// Select an event in a state.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="items">The events in the selected state.</param>
        /// <returns>The data for this action field.</returns>
        private static EventFieldData BuildEventFieldList(EventFieldData data, List<HeroEvent> items)
        {
            // hero object does not exist on game object or int list is empty
            if (data.targetHeroObject == null || items.Count == 0)
            {
                SimpleLayout.Label("[No Events]");
                data.fieldID = 0;
            }
            // everything looks okay. draw int list.
            else
            {
                // if we are referencing a field that no longer exists in int list, reset condition int field
                if (items.Count < data.fieldID)
                    data.fieldID = 0;

                // draw the list
                data.fieldID = new HeroField.EventListField().SetValues(data.fieldID, items, 0);
            }

            return data;
        }

        // --------------------------------------------------------------
        // Initialize Action Field
        // --------------------------------------------------------------

        /// <summary>
        /// Create the subfields that we need for this action field.
        /// </summary>
        /// <param name="title">The title of the action.</param>
        /// <param name="actionField">The action field.</param>
        /// <param name="heroObject">The hero object that contains this action field.</param>
        /// <returns>The data for this action field.</returns>
        private static EventFieldData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            EventFieldData data = new EventFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.heroObject = heroObject;
            data.targetHeroObject = actionField.heroObjects[0];
            data.objectType = actionField.ints[0];
            data.objectID = actionField.ints[1];
            data.fieldID = actionField.ints[2];
            data.stateID = actionField.ints[3];
            data.heroGUID = actionField.ints[4];
            data.fieldType = actionField.ints[5];
            data.objectName = actionField.strings[0];
            return data;
        }
    }

    /// <summary>
    /// Data needed to use the GetEventField.
    /// </summary>
    public struct EventFieldData : ITargetHeroObject, ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 6, 0);
            ActionCommon.CreateActionField(ref actionField.heroObjects, 1, null);
            ActionCommon.CreateActionField(ref actionField.strings, 1, "");
        }

        public string title { get; set; }
        public HeroObject heroObject { get; set; }
        public HeroObject targetHeroObject { get; set; }
        public int objectType { get; set; }
        public int objectID { get; set; }
        public int fieldType { get; set; }
        public int fieldID { get; set; }
        public int propertyID { get; set; }
        public int stateID;

        // for a game object in a scene
        public int heroGUID { get; set; }
        public string objectName { get; set; }
        public GameObject gameObject { get; set; }
    }
}