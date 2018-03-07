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
    /// Action field for the hero kit editor. Work with bool fields.
    /// </summary>
    public static class GetBoolField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get a value from a bool field.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static void BuildFieldA(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            // create the fields
            BoolFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the type of field you want to work with.
            //-----------------------------------------
            data.fieldType = new HeroField.ValueTypeField().SetValues(data.fieldType, 0);

            //-----------------------------------------
            // Get the type of game object we are working with 
            // Option 1: This game object (game object that this hero object is attached to)
            // Option 2: Another game object (another game object in the scene that has a hero object attached to it)
            //-----------------------------------------
            if (data.fieldType == 2 || data.fieldType == 3)
                data = ActionCommon.GetTargetHeroObject(data);

            //-----------------------------------------
            // Get the integer list you want to work with.
            // The integer list is in hero object editor > Variables
            //-----------------------------------------

            // if this is a field, draw field (1=value)
            if (data.fieldType == 1)
                data.fieldValue = SimpleLayout.BoolField(data.fieldValue);

            // if this is a list, draw ints (2=variables, 3=properties)
            if (data.fieldType != 1)
            {
                data = SetPropertyID(data, -1);
                List<BoolField> items = GetItemsFromList(data, -1);
                data = BuildItemFieldList(data, items);
            }

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[0] = data.objectType;
            actionField.ints[1] = data.objectID;
            actionField.ints[2] = data.fieldID;
            actionField.ints[3] = data.fieldType;
            actionField.ints[4] = data.heroGUID;
            actionField.ints[5] = data.propertyID;
            actionField.heroObjects[0] = data.targetHeroObject;
            actionField.strings[0] = data.objectName;
            actionField.bools[0] = data.fieldValue;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
        }

        /// <summary>
        /// Get a value from a bool field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static void BuildFieldB(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            // create the fields
            BoolFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the type of field you want to work with.
            //-----------------------------------------
            data.fieldType = new HeroField.ValueTypeFieldB().SetValues(data.fieldType, 0);

            //-----------------------------------------
            // Get the type of game object we are working with 
            // Option 1: This game object 
            // Option 2: Another game object in the Game Object list attached to this game object
            // Option 3: Another game object in the scene
            //-----------------------------------------          
            if (data.fieldType == 1 || data.fieldType == 2)
                data = ActionCommon.GetTargetHeroObject(data);

            //-----------------------------------------
            // Get the bool list you want to work with.
            // The bool list is in hero object editor > Variables
            //-----------------------------------------
            data = SetPropertyID(data);
            List<BoolField> items = GetItemsFromList(data);
            data = BuildItemFieldList(data, items);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[0] = data.objectType;
            actionField.ints[1] = data.objectID;
            actionField.ints[2] = data.fieldID;
            actionField.ints[3] = data.fieldType;
            actionField.ints[4] = data.heroGUID;
            actionField.ints[5] = data.propertyID;
            actionField.heroObjects[0] = data.targetHeroObject;
            actionField.strings[0] = data.objectName;
            actionField.bools[0] = data.fieldValue;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
        }

        /// <summary>
        /// Get a value from a bool field in a hero object template.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="heroObject">Hero object that is the target of this action.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static void BuildFieldC(string title, HeroActionParams actionParams, HeroActionField actionField, HeroObject heroObject, bool titleToLeft = false)
        {
            // create the fields
            BoolFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the type of field you want to work with.
            //-----------------------------------------
            data.fieldType = new HeroField.ValueTypeFieldB().SetValues(data.fieldType, 0);

            //-----------------------------------------
            // Get the type of game object we are working with 
            // Option 1: This game object 
            // Option 2: Another game object in the Game Object list attached to this game object
            // Option 3: Another game object in the scene
            //-----------------------------------------          
            data.targetHeroObject = heroObject;

            //-----------------------------------------
            // Get the bool list you want to work with.
            // The bool list is in hero object editor > Variables
            //-----------------------------------------
            data = SetPropertyID(data);
            List<BoolField> items = GetItemsFromList(data);
            data = BuildItemFieldList(data, items);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[0] = data.objectType;
            actionField.ints[1] = data.objectID;
            actionField.ints[2] = data.fieldID;
            actionField.ints[3] = data.fieldType;
            actionField.ints[4] = data.heroGUID;
            actionField.ints[5] = data.propertyID;
            actionField.heroObjects[0] = data.targetHeroObject;
            actionField.strings[0] = data.objectName;
            actionField.bools[0] = data.fieldValue;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
        }

        /// <summary>
        /// Get a value from a bool field on an event.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="boolData">Current data for this action field.</param>
        /// <param name="heroObject">Hero object that is the target of this action.</param>
        public static void BuildEventField(string title, ConditionBoolField boolData, HeroObject heroObject) 
        {
            BoolFieldData data = new BoolFieldData();
            data.heroObject = heroObject;
            data.targetHeroObject = boolData.targetHeroObject;
            data.objectType = boolData.objectType;
            data.objectID = boolData.objectID;
            data.fieldID = boolData.fieldID;
            data.fieldType = boolData.fieldType;
            data.heroGUID = boolData.heroGUID;
            data.propertyID = boolData.propertyID;
            data.objectName = boolData.objectName;
            data.fieldValue = boolData.fieldValue;

            SimpleLayout.Label(title);

            //-----------------------------------------
            // Get the type of field you want to work with.
            //-----------------------------------------
            data.fieldType = new HeroField.ValueTypeField().SetValues(data.fieldType, 0);

            //-----------------------------------------
            // Get the type of game object we are working with 
            // Option 1: This game object (game object that this hero object is attached to)
            // Option 2: Another game object (another game object in the scene that has a hero object attached to it)
            //-----------------------------------------
            if (data.fieldType == 2 || data.fieldType == 3)
                data = ActionCommon.GetTargetHeroObject(data);

            //-----------------------------------------
            // Get the list you want to work with.
            // The list is in hero object editor > Variables
            //-----------------------------------------

            // if this is a field, draw field (1=value)
            if (data.fieldType == 1)
                data.fieldValue = SimpleLayout.BoolField(data.fieldValue);

            // if this is a list, draw ints (2=variables, 3=properties)
            if (data.fieldType != 1)
            {
                data = SetPropertyID(data, -1);
                List<BoolField> items = GetItemsFromList(data, -1);
                data = BuildItemFieldList(data, items);
            }

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            boolData.objectType = data.objectType;
            boolData.objectID = data.objectID;
            boolData.fieldID = data.fieldID;
            boolData.fieldType = data.fieldType;
            boolData.heroGUID = data.heroGUID;
            boolData.propertyID = data.propertyID;
            boolData.targetHeroObject = data.targetHeroObject;
            boolData.objectName = data.objectName;
            boolData.fieldValue = data.fieldValue;
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
        private static BoolFieldData SetPropertyID(BoolFieldData data, int indexShift = 0)
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
        /// Get a list of bool fields.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="indexShift">Make sure we are using the correct field type.</param>
        /// <returns>A list of bool fields.</returns>
        private static List<BoolField> GetItemsFromList(BoolFieldData data, int indexShift=0)
        {
            List<BoolField> items = new List<BoolField>();
            int fieldType = data.fieldType + indexShift;

            // Local Variable
            if (fieldType == 1 && data.targetHeroObject != null)
            {
                items = data.targetHeroObject.lists.bools.items;
            }

            // Property
            else if (fieldType == 2 && data.targetHeroObject != null)
            {
                List<HeroProperties> heroProperties = data.targetHeroObject.propertiesList.properties;
                if (heroProperties != null && heroProperties.Count > 0)
                    items = heroProperties[data.propertyID-1].itemProperties.bools.items;
            }

            // Global Variable
            else if (fieldType == 3)
            {
                items = HeroKitCommon.GetGlobals().globals.bools.items;
            }

            return items;
        }

        /// <summary>
        /// Create a drop-down list of bool fields in the action field.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="items">A list of bool fields.</param>
        /// <returns>The data for this action field.</returns>
        private static BoolFieldData BuildItemFieldList(BoolFieldData data, List<BoolField> items)
        {
            bool drawValues = ActionCommon.InitItemFieldList(ref data, items, "Bools");

            if (drawValues)
                data.fieldID = HeroField.BoolListField.SetValues(items, data.fieldID, 0);

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
        private static BoolFieldData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            BoolFieldData data = new BoolFieldData();
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
            data.fieldValue = actionField.bools[0];
            return data;
        }
    }

    /// <summary>
    /// Data needed to use GetBoolField.
    /// </summary>
    public struct BoolFieldData : ITargetHeroObject, ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 6, 0);
            ActionCommon.CreateActionField(ref actionField.heroObjects, 1, null);
            ActionCommon.CreateActionField(ref actionField.strings, 1, "");
            ActionCommon.CreateActionField(ref actionField.bools, 1, false);
        }

        public string title { get; set; }
        public HeroObject heroObject { get; set; }
        public HeroObject targetHeroObject { get; set; }
        public int objectType { get; set; }
        public int objectID { get; set; }
        public int fieldType { get; set; }
        public int propertyID { get; set; }
        public int fieldID { get; set; }
        public bool fieldValue;

        // for a game object in a scene
        public int heroGUID { get; set; }
        public string objectName { get; set; }
        public GameObject gameObject { get; set; }
    }
}