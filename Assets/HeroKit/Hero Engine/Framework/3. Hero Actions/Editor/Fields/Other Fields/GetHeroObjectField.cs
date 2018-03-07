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
    /// Action field for the hero kit editor. Work with hero object fields.
    /// </summary>
    public static class GetHeroObjectField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get a value from a hero object field.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static HeroObject BuildFieldA(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            HeroObjectFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

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
            // Get the type of object we are working with. (if it isn't a global)
            //-----------------------------------------
            data = ActionCommon.GetTargetHeroObject(data);

            //-----------------------------------------
            // Get the list you want to work with.
            //-----------------------------------------
            // if this is a list, draw ints (2=variables, 3=properties)
            if (data.fieldType != 1)
            {
                data = SetPropertyID(data, -1);
                List<HeroObjectField> items = GetItemsFromList(data, -1);
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

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            //-----------------------------------------
            // Return value
            //-----------------------------------------
            return GetTargetHeroObject(data.targetHeroObject, data.fieldType, data.fieldID, data.propertyID); 
        }

        /// <summary>
        /// Get a value from a hero object field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static HeroObject BuildFieldB(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            HeroObjectFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

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
            // Get the type of object we are working with 
            // Option 1: This object (object that this hero object is attached to)
            // Option 2: Another object (another object in the scene that has a hero object attached to it)
            //-----------------------------------------
            if (data.fieldType == 1 || data.fieldType == 2)
                data = ActionCommon.GetTargetHeroObject(data);

            //-----------------------------------------
            // Get the hero list you want to work with.
            // The list is in hero object editor > Variables
            //-----------------------------------------

            // if this is a field, draw field (1=value)
            data = SetPropertyID(data);
            List<HeroObjectField> items = GetItemsFromList(data);
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

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return GetTargetHeroObject(data.targetHeroObject, data.fieldType+1, data.fieldID, data.propertyID);
        }

        /// <summary>
        /// Get a value from a hero object field in a hero object template.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="heroObject">Hero object that is the target of this action.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static HeroObject BuildFieldC(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            HeroObjectFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

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
            // Get the type of object we are working with 
            // Option 1: This object (object that this hero object is attached to)
            // Option 2: Another object (another object in the scene that has a hero object attached to it)
            //-----------------------------------------
            SimpleLayout.Space(-8);
            data = ActionCommon.GetTargetHeroTemplate(data);

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

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return data.targetHeroObject;
        }

        /// <summary>
        /// Get a value from a hero object field. This is for a field that contains a hero kit object in a scene.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static void BuildFieldD(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            HeroObjectFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

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
            // Get the type of object we are working with 
            //-----------------------------------------
            SimpleLayout.Space(-8);
            data = ActionCommon.GetHeroObjectInScene(data);

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

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
        }

        /// <summary>
        /// Get a value from a hero object. Use this hero object by default, but let developer select another hero object.
        /// If another hero object is selected, you can get with a Value, Variable, Property, or Global.
        /// </summary>
        /// <param name="title">Title for the action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldA">Action field.</param>
        /// <param name="actionFieldB">Action field.</param>
        /// <returns>A hero object.</returns>
        public static HeroObject BuildFieldE(string title, HeroActionParams actionParams, HeroActionField actionFieldA, HeroActionField actionFieldB)
        {
            bool useAnotherObject = GetBoolValue.BuildField(title, actionParams, actionFieldA, true);
            HeroObject targetHeroObject = useAnotherObject ? BuildFieldA("", actionParams, actionFieldB) : actionParams.heroObject;

            return targetHeroObject;
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
        private static HeroObjectFieldData SetPropertyID(HeroObjectFieldData data, int indexShift = 0)
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
        /// Get the target hero object.
        /// </summary>
        /// <param name="targetObject"></param>
        /// <param name="fieldType"></param>
        /// <param name="fieldID"></param>
        /// <returns></returns>
        private static HeroObject GetTargetHeroObject(HeroObject targetObject, int fieldType, int fieldID, int propertyID)
        {
            HeroObject heroObject = null;

            // value
            if (fieldType == 1)
            {
                heroObject = targetObject;
            }

            if (targetObject != null && fieldID > 0)
            {
                // variable
                if (fieldType == 2 && targetObject.lists.heroObjects.items.Count >= fieldID)
                {
                    heroObject = targetObject.lists.heroObjects.items[fieldID - 1].value;
                }
                // property
                else if (fieldType == 3 && targetObject.propertiesList.properties[propertyID].itemProperties.heroObjects.items.Count >= fieldID)
                {
                    heroObject = targetObject.propertiesList.properties[propertyID].itemProperties.heroObjects.items[fieldID - 1].value;
                }
                // global
                else if (fieldType == 4 && HeroKitCommon.GetGlobals().globals.heroObjects.items.Count >= fieldID)
                {
                    heroObject = HeroKitCommon.GetGlobals().globals.heroObjects.items[fieldID - 1].value;
                }
            }

            return heroObject;
        }

        /// <summary>
        /// Get a list of hero object fields.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="indexShift">Make sure we are using the correct field type.</param>
        /// <returns>A list of hero object fields.</returns>
        private static List<HeroObjectField> GetItemsFromList(HeroObjectFieldData data, int indexShift = 0)
        {
            List<HeroObjectField> items = new List<HeroObjectField>();
            int fieldType = data.fieldType + indexShift;

            // Local Variable
            if (fieldType == 1 && data.targetHeroObject != null)
            {
                items = data.targetHeroObject.lists.heroObjects.items;
            }

            // Property
            else if (fieldType == 2 && data.targetHeroObject != null)
            {
                List<HeroProperties> heroProperties = data.targetHeroObject.propertiesList.properties;
                if (heroProperties != null && heroProperties.Count > 0)
                    items = heroProperties[data.propertyID - 1].itemProperties.heroObjects.items;
            }

            // Global Variable
            else if (fieldType == 3)
            {
                items = HeroKitCommon.GetGlobals().globals.heroObjects.items;
            }

            return items;
        }

        /// <summary>
        /// Create a drop-down list of hero object fields in the action field.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="items">A list of hero object fields.</param>
        /// <returns>The data for this action field.</returns>
        private static HeroObjectFieldData BuildItemFieldList(HeroObjectFieldData data, List<HeroObjectField> items)
        {
            bool drawValues = ActionCommon.InitItemFieldList(ref data, items, "Hero Objects");

            if (drawValues)
                data.fieldID = HeroField.HeroObjectListField.SetValues(items, data.fieldID, 0);

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
        private static HeroObjectFieldData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            HeroObjectFieldData data = new HeroObjectFieldData();
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
    public struct HeroObjectFieldData : ITargetHeroObject, ITitle
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