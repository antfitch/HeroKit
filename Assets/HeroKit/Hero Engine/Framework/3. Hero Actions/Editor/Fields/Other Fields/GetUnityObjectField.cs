// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using SimpleGUI;
using System.Collections.Generic;
using HeroKit.Editor.ActionBlockFields;
using UnityEngine.SceneManagement;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with unity object fields.
    /// </summary>
    public static class GetUnityObjectField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get a value from a unity object field.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <typeparam name="T">The type of unity object.</typeparam>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns></returns>
        public static T BuildFieldA<T>(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false) where T : Object
        {
            // create the fields
            UnityObjectFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

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
            {
                data.fieldValue = SimpleLayout.ObjectField(data.fieldValue as T, HeroKitCommon.GetWidthForField(135));
                SetScene(ref data);
            }

            // if this is a list, draw ints (2=variables, 3=properties)
            if (data.fieldType != 1)
            {
                data = SetPropertyID(data, -1);
                List<UnityObjectField> items = GetItemsFromList(data, -1);
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
            actionField.ints[6] = data.propertyID;
            actionField.heroObjects[0] = data.targetHeroObject;
            actionField.strings[0] = data.objectName;
            actionField.unityObjects[0] = data.fieldValue;
            actionField.ints[5] = data.sceneID;
            actionField.strings[1] = data.sceneName;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return data.fieldValue as T;
        }

        /// <summary>
        /// Get a value from a unity object field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <typeparam name="T">The type of unity object.</typeparam>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>A unity object.</returns>
        public static T BuildFieldB<T>(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false) where T : Object
        {
            // create the fields
            UnityObjectFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

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
            List<UnityObjectField> items = GetItemsFromList(data);
            data = BuildItemFieldList(data, items);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[0] = data.objectType;
            actionField.ints[1] = data.objectID;
            actionField.ints[2] = data.fieldID;
            actionField.ints[3] = data.fieldType;
            actionField.ints[4] = data.heroGUID;
            actionField.ints[6] = data.propertyID;
            actionField.heroObjects[0] = data.targetHeroObject;
            actionField.strings[0] = data.objectName;
            actionField.unityObjects[0] = data.fieldValue;
            actionField.ints[5] = data.sceneID;
            actionField.strings[1] = data.sceneName;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return data.fieldValue as T;
        }

        /// <summary>
        /// Get a value from a unity object field in a hero object template.
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
            UnityObjectFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

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
            List<UnityObjectField> items = GetItemsFromList(data);
            data = BuildItemFieldList(data, items);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[0] = data.objectType;
            actionField.ints[1] = data.objectID;
            actionField.ints[2] = data.fieldID;
            actionField.ints[3] = data.fieldType;
            actionField.ints[4] = data.heroGUID;
            actionField.ints[6] = data.propertyID;
            actionField.heroObjects[0] = data.targetHeroObject;
            actionField.strings[0] = data.objectName;
            actionField.unityObjects[0] = data.fieldValue;
            actionField.ints[5] = data.sceneID;
            actionField.strings[1] = data.sceneName;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
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
        private static UnityObjectFieldData SetPropertyID(UnityObjectFieldData data, int indexShift = 0)
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
        /// Get a list of unity object fields.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="indexShift">Make sure we are using the correct field type.</param>
        /// <returns>A list of unity object fields.</returns>
        private static List<UnityObjectField> GetItemsFromList(UnityObjectFieldData data, int indexShift=0)
        {
            List<UnityObjectField> items = new List<UnityObjectField>();
            int fieldType = data.fieldType + indexShift;

            // Local Variable
            if (fieldType == 1 && data.targetHeroObject != null)
            {
                items = data.targetHeroObject.lists.unityObjects.items;
            }

            // Property
            else if (fieldType == 2 && data.targetHeroObject != null)
            {
                List<HeroProperties> heroProperties = data.targetHeroObject.propertiesList.properties;
                if (heroProperties != null && heroProperties.Count > 0)
                    items = heroProperties[data.propertyID - 1].itemProperties.unityObjects.items;
            }

            // Global Variable
            else if (fieldType == 3)
            {
                items = HeroKitCommon.GetGlobals().globals.unityObjects.items;
            }

            return items;
        }

        /// <summary>
        /// Create a drop-down list of unity object fields in the action field.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="items">A list of unity object fields.</param>
        /// <returns>The data for this action field.</returns>
        private static UnityObjectFieldData BuildItemFieldList(UnityObjectFieldData data, List<UnityObjectField> items)
        {
            bool drawValues = ActionCommon.InitItemFieldList(ref data, items, "Unity Objects");

            if (drawValues)
            {
                data.fieldID = HeroField.UnityObjectListField.SetValues(items, data.fieldID, 0);
                if (data.fieldID > 0)
                {
                    data.fieldValue = items[data.fieldID - 1].value;

                    // set scene if it exists
                    SetScene(ref data);
                    items[data.fieldID - 1].sceneID = data.sceneID;
                    items[data.fieldID - 1].sceneName = data.sceneName;
                }
            }

            return data;
        }

        /// <summary>
        /// If object added is a scene, set the scene ID.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <returns>ID for the scene.</returns>
        private static void SetScene(ref UnityObjectFieldData data)
        {
            // if item is scene asset, add to to build settings
            SceneAsset testScene = data.fieldValue as SceneAsset;
            if (testScene != null)
            {
                // add the scene to the editor build settings if it doesn't already exist there.
                data.sceneID = HeroKitCommon.AddSceneToBuildSettings(testScene);
                data.sceneName = testScene.name;
            }
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
        private static UnityObjectFieldData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            UnityObjectFieldData data = new UnityObjectFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.heroObject = heroObject;
            data.targetHeroObject = actionField.heroObjects[0];
            data.objectType = actionField.ints[0];
            data.objectID = actionField.ints[1];
            data.fieldID = actionField.ints[2];
            data.fieldType = actionField.ints[3];
            data.heroGUID = actionField.ints[4];
            data.propertyID = actionField.ints[6];
            data.objectName = actionField.strings[0];
            data.fieldValue = actionField.unityObjects[0];
            data.sceneID = actionField.ints[5];
            data.sceneName = actionField.strings[1];
            return data;
        }
    }

    /// <summary>
    /// Data needed to use the GetUnityObjectField.
    /// </summary>
    public struct UnityObjectFieldData : ITargetHeroObject, ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 7, 0);
            ActionCommon.CreateActionField(ref actionField.heroObjects, 1, null);
            ActionCommon.CreateActionField(ref actionField.strings, 2, "");
            ActionCommon.CreateActionField(ref actionField.unityObjects, 1, null);
        }

        public string title { get; set; }
        public HeroObject heroObject { get; set; }
        public HeroObject targetHeroObject { get; set; }
        public int objectType { get; set; }
        public int objectID { get; set; }
        public int fieldType { get; set; }
        public int propertyID { get; set; }
        public int fieldID { get; set; }
        public Object fieldValue;

        // for a game object in a scene
        public int heroGUID { get; set; }
        public string objectName { get; set; }
        public GameObject gameObject { get; set; }

        // for a scene object
        public int sceneID { get; set; }
        public string sceneName { get; set; }
    }
}