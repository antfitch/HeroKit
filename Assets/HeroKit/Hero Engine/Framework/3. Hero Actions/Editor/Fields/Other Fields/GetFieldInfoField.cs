// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;
using HeroKit.Editor.ActionBlockFields;
using System.Reflection;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with fields in a script.
    /// </summary>
    public static class GetFieldInfoField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Set a field in a script.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldA">Action field.</param>
        /// <param name="actionFieldB">Action field.</param>
        /// <param name="script">The script.</param>
        public static void BuildFieldA(string title, HeroActionParams actionParams, HeroActionField actionFieldA, HeroActionField actionFieldB, MonoScript script)
        {
            // create the fields
            FieldInfoFieldData data = CreateFieldData(title, actionFieldA, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "") SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the list you want to work with.
            //-----------------------------------------
            FieldInfo[] fields = GetFields(script);
            data = BuildItemFieldList(data, fields);

            if (fields != null && fields.Length != 0 && data.fieldID > 0)
            {
                FieldInfo field = fields[data.fieldID - 1];
                SetFieldOnScript(field, actionParams, actionFieldB);
            }

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionFieldA.ints[0] = data.fieldID;
        }

        /// <summary>
        /// Get a value from a field in a script and save this value on a hero kit object.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldA">Action field.</param>
        /// <param name="actionFieldB">Action field.</param>
        /// <param name="script">The script.</param>
        public static void BuildFieldB(string title, HeroActionParams actionParams, HeroActionField actionFieldA, HeroActionField actionFieldB, MonoScript script)
        {
            // create the fields
            FieldInfoFieldData data = CreateFieldData(title, actionFieldA, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "") SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the list you want to work with.
            //-----------------------------------------
            FieldInfo[] fields = GetFields(script);
            data = BuildItemFieldList(data, fields);

            if (fields != null && fields.Length != 0 && data.fieldID > 0)
            {
                FieldInfo field = fields[data.fieldID - 1];
                SetFieldOnHero(field, actionParams, actionFieldB);
            }

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionFieldA.ints[0] = data.fieldID;
        }

        // --------------------------------------------------------------
        // Helpers
        // --------------------------------------------------------------

        /// <summary>
        /// Get a value from a hero object list and set this value for a field in a script.
        /// </summary>
        /// <param name="field">The field to set.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldB">Action field.</param>
        private static void SetFieldOnScript(FieldInfo field, HeroActionParams actionParams, HeroActionField actionFieldB)
        {
            // Get value on the target instance.
            object value = field.FieldType;

            // Test value type.
            if (value == typeof(int))
            {
                GetIntegerField.BuildFieldA(field.Name + " (Int): ", actionParams, actionFieldB);
            }
            else if (value == typeof(float))
            {
                GetFloatField.BuildFieldA(field.Name + " (Float): ", actionParams, actionFieldB);
            }
            else if (value == typeof(string))
            { 
                GetStringField.BuildFieldA(field.Name + " (String): ", actionParams, actionFieldB);
            }
            else if (value == typeof(bool))
            {
                GetBoolField.BuildFieldA(field.Name + " (Bool): ", actionParams, actionFieldB);
            }
            else if (value == typeof(HeroKitObject))
            {
                GetHeroObjectField.BuildFieldA(field.Name + " (HeroObject): ", actionParams, actionFieldB);
            }
            else if (value == typeof(GameObject))
            {
                GetGameObjectField.BuildFieldA(field.Name + " (GameObject): ", actionParams, actionFieldB);
            }
            else
            {
                string valueType = value.ToString();
                int index = valueType.LastIndexOf('.');
                if (valueType.Length >= 2)
                    valueType = valueType.Substring(index + 1, valueType.Length - index - 1);

                SimpleLayout.Label(valueType + " " + field.Name + ": [Value can't be stored by HeroKit]");
            }
        }

        /// <summary>
        /// Get a field from a script and set this value in a hero object list.
        /// </summary>
        /// <param name="field">The field to set.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldB">Action field.</param>
        private static void SetFieldOnHero(FieldInfo field, HeroActionParams actionParams, HeroActionField actionFieldB)
        {
            // Get value on the target instance.
            object value = field.FieldType;

            // Test value type.
            if (value == typeof(int))
            {
                GetIntegerField.BuildFieldB(field.Name + " (Int): ", actionParams, actionFieldB);
            }
            else if (value == typeof(float))
            {
                GetFloatField.BuildFieldB(field.Name + " (Float): ", actionParams, actionFieldB);
            }
            else if (value == typeof(string))
            {
                GetStringField.BuildFieldB(field.Name + " (String): ", actionParams, actionFieldB);
            }
            else if (value == typeof(bool))
            {
                GetBoolField.BuildFieldB(field.Name + " (Bool): ", actionParams, actionFieldB);
            }
            else if (value == typeof(HeroKitObject))
            {
                GetHeroObjectField.BuildFieldB(field.Name + " (HeroObject): ", actionParams, actionFieldB);
            }
            else if (value == typeof(GameObject))
            {
                GetGameObjectField.BuildFieldB(field.Name + " (GameObject): ", actionParams, actionFieldB);
            }
            else
            {
                string valueType = value.ToString();
                int index = valueType.LastIndexOf('.');
                if (valueType.Length >= 2)
                    valueType = valueType.Substring(index + 1, valueType.Length - index - 1);

                SimpleLayout.Label(valueType + " " + field.Name + ": [Value can't be stored by HeroKit]");
            }
        }

        /// <summary>
        /// Get all fields in a script.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns>The fields in the script.</returns>
        private static FieldInfo[] GetFields(MonoScript script)
        {
            if (script == null) return new FieldInfo[0];

            // get the class attached to the monoscript
            System.Type classType = script.GetClass();

            // exit early of there is no class attached to monoscript
            if (classType == null) return new FieldInfo[0];

            FieldInfo[] fieldInfo = classType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            return fieldInfo;
        }

        /// <summary>
        /// Create a drop-down list of script fields in the action field.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="items">A list of script fields.</param>
        /// <returns>The data for this action field.</returns>
        private static FieldInfoFieldData BuildItemFieldList(FieldInfoFieldData data, FieldInfo[] items)
        {
            // there are no fields in the script.
            if (items.Length == 0)
            {
                data.fieldID = 0;
            }
            // everything looks okay. draw list.
            else
            {
                // if we are referencing a field that no longer exists, reset field.
                if (items.Length < data.fieldID)
                {
                    data.fieldID = 0;
                }

                // draw the list
                data.fieldID = HeroField.FieldInfoListField.SetValues(items, data.fieldID, 0);
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
        private static FieldInfoFieldData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            FieldInfoFieldData data = new FieldInfoFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldID = actionField.ints[0];
            data.startID = actionField.ints[1];
            data.endID = actionField.ints[2];
            return data;
        }
    }

    /// <summary>
    /// Data needed to use the GetFieldInfoField.
    /// </summary>
    public struct FieldInfoFieldData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 3, 0);
        }

        public string title { get; set; }
        public int fieldID { get; set; }
        public int startID { get; set; }
        public int endID { get; set; }
    }
}