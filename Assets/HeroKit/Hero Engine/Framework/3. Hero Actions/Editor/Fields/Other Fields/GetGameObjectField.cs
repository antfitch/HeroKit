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
    /// Action field for the hero kit editor. Work with game object fields.
    /// </summary>
    public static class GetGameObjectField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------


        /// <summary>
        /// Get a value from a game object field.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>A game object.</returns>
        public static GameObject BuildFieldA(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            GameObjectFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft)
            {
                SimpleLayout.Label(data.title);
            }
            else
            {
                SimpleLayout.Space(8);
            }

            //-----------------------------------------
            // Get the type of field you want to work with.
            //-----------------------------------------
            SimpleLayout.Space(-8);
            data.fieldType = new HeroField.ValueTypeFieldC().SetValues(data.fieldType, 0);

            //-----------------------------------------
            // Get the type of object we are working with 
            // Option 1: This object (object that this hero object is attached to)
            // Option 2: Another object (another object in the scene that has a hero object attached to it)
            //-----------------------------------------
            if (data.fieldType == 2 || data.fieldType == 3 || data.fieldType == 4)
                data = ActionCommon.GetTargetHeroObject(data);

            //-----------------------------------------
            // Get the integer list you want to work with.
            // The integer list is in hero object editor > Variables
            //-----------------------------------------

            // if this is a field, draw field (1=value)
            if (data.fieldType == 1)
                data.objectName = SimpleLayout.TextField(data.objectName, HeroKitCommon.GetWidthForField(133));

            // if this is a list, draw items (2=variables, 3=properties, 5=globals)
            if (data.fieldType == 2 || data.fieldType == 3 || data.fieldType == 5) 
            {
                data = SetPropertyID(data, -1);
                List<GameObjectField> items = GetItemsFromList(data, -1);
                data = BuildItemFieldList(data, items);
            }

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.heroObjects[0] = data.targetHeroObject;
            actionField.ints[0] = data.objectType;
            actionField.ints[1] = data.objectID;
            actionField.ints[2] = data.fieldID;
            actionField.ints[3] = data.fieldType;
            actionField.ints[4] = data.heroGUID;
            actionField.ints[5] = data.propertyID;
            actionField.strings[0] = data.objectName;
            actionField.gameObjects[0] = data.gameObject;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return data.gameObject;
        }

        /// <summary>
        /// Get a value from a game object field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>A game object.</returns>
        public static GameObject BuildFieldB(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            GameObjectFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft)
            {
                SimpleLayout.Label(data.title);
            }
            else
            {
                SimpleLayout.Space(8);
            }

            //-----------------------------------------
            // Get the type of field you want to work with.
            //-----------------------------------------
            SimpleLayout.Space(-8);
            data.fieldType = new HeroField.ValueTypeFieldB().SetValues(data.fieldType, 0);

            //-----------------------------------------
            // Get the type of object we are working with 
            // Option 1: This object (object that this hero object is attached to)
            // Option 2: Another object (another object in the scene that has a hero object attached to it)
            //-----------------------------------------
            if (data.fieldType == 1 || data.fieldType == 2)
                data = ActionCommon.GetTargetHeroObject(data);

            //-----------------------------------------
            // Get the integer list you want to work with.
            // The integer list is in hero object editor > Variables
            //-----------------------------------------
            data = SetPropertyID(data);
            List<GameObjectField> items = GetItemsFromList(data);
            data = BuildItemFieldList(data, items);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.heroObjects[0] = data.targetHeroObject;
            actionField.ints[0] = data.objectType;
            actionField.ints[1] = data.objectID;
            actionField.ints[2] = data.fieldID;
            actionField.ints[3] = data.fieldType;
            actionField.ints[4] = data.heroGUID;
            actionField.ints[5] = data.propertyID;
            actionField.strings[0] = data.objectName;
            actionField.gameObjects[0] = data.gameObject;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return data.gameObject;
        }

        // --------------------------------------------------------------
        // Helpers
        // --------------------------------------------------------------

        /// <summary>
        /// If we are working with a hero property list, get the ID of the property we are using.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="indexShift">Make sure we are using the correct field type.</param>
        /// <returns>Current data for this action field.</returns>
        private static GameObjectFieldData SetPropertyID(GameObjectFieldData data, int indexShift = 0)
        {
            int fieldType = data.fieldType + indexShift;

            // Property
            if (fieldType == 2 && data.targetHeroObject != null)
            {
                data.propertyID = HeroField.HeroPropertyListField.SetValues(data.targetHeroObject.propertiesList.properties.ToArray(), data.propertyID, 0);
            }

            return data;
        }

        /// <summary>
        /// Get a list of game object fields.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="indexShift">Make sure we are using the correct field type.</param>
        /// <returns>A list of game object fields.</returns>
        private static List<GameObjectField> GetItemsFromList(GameObjectFieldData data, int indexShift = 0)
        {
            List<GameObjectField> items = new List<GameObjectField>();
            int fieldType = data.fieldType + indexShift;

            // Local Variable
            if (fieldType == 1 && data.targetHeroObject != null)
            {
                items = data.targetHeroObject.lists.gameObjects.items;
            }

            // Property
            else if (fieldType == 2 && data.targetHeroObject != null)
            {
                List<HeroProperties> heroProperties = data.targetHeroObject.propertiesList.properties;
                if (heroProperties != null && heroProperties.Count > 0)
                    items = heroProperties[data.propertyID - 1].itemProperties.gameObjects.items;
            }

            // Global Variable
            else if (fieldType == 4)
            {
                items = HeroKitCommon.GetGlobals().globals.gameObjects.items;
            }

            return items;
        }

        /// <summary>
        /// Create a drop-down list of game object fields in the action field.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="items">A list of game object fields.</param>
        /// <returns>The data for this action field.</returns>
        private static GameObjectFieldData BuildItemFieldList(GameObjectFieldData data, List<GameObjectField> items)
        {
            bool drawValues = ActionCommon.InitItemFieldList(ref data, items, "Game Objects");

            if (drawValues)
                data.fieldID = HeroField.GameObjectListField.SetValues(items, data.fieldID, 0);

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
        private static GameObjectFieldData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            GameObjectFieldData data = new GameObjectFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.heroObject = heroObject;
            data.targetHeroObject = actionField.heroObjects[0];
            data.objectType = actionField.ints[0];
            data.objectID = actionField.ints[1];
            data.fieldID = actionField.ints[2];
            data.fieldType = actionField.ints[3];
            data.heroGUID = actionField.ints[4];
            data.propertyID = actionField.ints[5];
            data.objectName = actionField.strings[0];
            data.gameObject = actionField.gameObjects[0];
            return data;
        }
    }

    /// <summary>
    /// Data needed to use GetHeroObjectField.
    /// </summary>
    public struct GameObjectFieldData : ITargetHeroObject, ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 6, 0);
            ActionCommon.CreateActionField(ref actionField.heroObjects, 1, null);
            ActionCommon.CreateActionField(ref actionField.strings, 1, "");
            ActionCommon.CreateActionField(ref actionField.gameObjects, 1, null);
        }

        public string title { get; set; }
        public HeroObject heroObject { get; set; }
        public HeroObject targetHeroObject { get; set; }
        public int objectType { get; set; }
        public int objectID { get; set; }
        public int fieldType { get; set; }
        public int propertyID { get; set; }
        public int fieldID { get; set; }

        // for a hero object in a scene
        public int heroGUID { get; set; }
        public string objectName { get; set; }
        public GameObject gameObject { get; set; }
    }
}