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
    /// Action field for the hero kit editor. Work with properties in a script.
    /// </summary>
    public static class GetPropertyField
    {
        /// <summary>
        /// Set a property in a script.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="propertyFieldStart">Action field (range start).</param>
        /// <param name="propertyFieldEnd">Action field (range stop).</param>
        /// <param name="script">The script.</param>
        /// <param name="heroAction">The hero action.</param>
        public static void BuildFieldA(string title, HeroActionParams actionParams, HeroActionField actionField, int propertyFieldStart, int propertyFieldEnd, MonoScript script, HeroAction heroAction)
        {
            // create the fields
            PropertyFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "") SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the list you want to work with.
            //-----------------------------------------
            PropertyInfo[] properties = GetProperties(script);
            int availablePropertySlots = propertyFieldEnd - propertyFieldStart;

            if (properties != null && properties.Length != 0)
            {
                SimpleLayout.BeginVertical(Box.StyleB);
                for (int i = 0; i < properties.Length && i < availablePropertySlots; i++)
                {
                    PropertyInfo property = properties[i];
                    SetPropertyOnScript(property, actionParams, heroAction.actionFields[propertyFieldStart+i]);
                }
                SimpleLayout.EndVertical();
            }

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[1] = propertyFieldStart;
            actionField.ints[2] = propertyFieldEnd;
        }

        /// <summary>
        /// Get a value from a property in a script and save this value on a hero kit object.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldA">Action field.</param>
        /// <param name="actionFieldB">Action field.</param>
        /// <param name="script">The script.</param>
        public static void BuildFieldB(string title, HeroActionParams actionParams, HeroActionField actionFieldA, HeroActionField actionFieldB, MonoScript script)
        {
            // create the fields
            PropertyFieldData data = CreateFieldData(title, actionFieldA, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "") SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the list you want to work with.
            //-----------------------------------------
            PropertyInfo[] properties = GetPropertiesFromScript(script);
            data = BuildItemFieldList(data, properties);

            if (properties != null && properties.Length != 0 && data.fieldID > 0)
            {
                PropertyInfo property = properties[data.fieldID - 1];
                SetPropertyOnScript(property, actionParams, actionFieldB);
            }

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionFieldA.ints[0] = data.fieldID;
        }

        /// <summary>
        /// Get a property field from a drop down list (save property from script on hero object).
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldA">Action field.</param>
        /// <param name="actionFieldB">Action field.</param>
        /// <param name="script">The script.</param>
        public static void BuildFieldC(string title, HeroActionParams actionParams, HeroActionField actionFieldA, HeroActionField actionFieldB, MonoScript script)
        {
            // create the fields
            PropertyFieldData data = CreateFieldData(title, actionFieldA, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "") SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the list you want to work with.
            //-----------------------------------------
            PropertyInfo[] properties = GetPropertiesFromScript(script);
            data = BuildItemFieldList(data, properties);

            if (properties != null && properties.Length != 0 && data.fieldID > 0)
            {
                PropertyInfo property = properties[data.fieldID - 1];
                SetPropertyOnHero(property, actionParams, actionFieldB);
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
        /// Get all properties in a script.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns>The properties in the script.</returns>
        private static PropertyInfo[] GetProperties(MonoScript script)
        {
            if (script == null) return new PropertyInfo[0];

            // get the class attached to the monoscript
            System.Type classType = script.GetClass();

            // exit early of there is no class attached to monoscript
            if (classType == null) return new PropertyInfo[0];

            PropertyInfo[] propertyInfo = classType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            return propertyInfo;
        }

        /// <summary>
        /// Get a value from a hero object list and set this value for a property in a script.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldB">Action field.</param>
        private static void SetPropertyOnScript(PropertyInfo property, HeroActionParams actionParams, HeroActionField actionFieldB)
        {
            // Get value on the target instance.
            object value = property.PropertyType;

            // Test value type.
            if (value == typeof(int))
            {
                GetIntegerField.BuildFieldA(property.Name + " (Int): ", actionParams, actionFieldB);
            }
            else if (value == typeof(float))
            {
                GetFloatField.BuildFieldA(property.Name + " (Float): ", actionParams, actionFieldB);
            }
            else if (value == typeof(string))
            { 
                GetStringField.BuildFieldA(property.Name + " (String): ", actionParams, actionFieldB);
            }
            else if (value == typeof(bool))
            {
                GetBoolField.BuildFieldA(property.Name + " (Bool): ", actionParams, actionFieldB);
            }
            else if (value == typeof(HeroKitObject))
            {
                GetHeroObjectField.BuildFieldA(property.Name + " (HeroObject): ", actionParams, actionFieldB);
            }
            else if (value == typeof(GameObject))
            {
                GetGameObjectField.BuildFieldA(property.Name + " (GameObject): ", actionParams, actionFieldB);
            }
            else
            {
                string valueType = value.ToString();
                int index = valueType.LastIndexOf('.');
                if (valueType.Length >= 2)
                    valueType = valueType.Substring(index + 1, valueType.Length - index - 1);

                SimpleLayout.Label(valueType + " " + property.Name + ": [Value can't be stored by HeroKit]");
            }
        }

        /// <summary>
        /// et a property from a script and set this value in a hero object list.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldB">Action field.</param>
        private static void SetPropertyOnHero(PropertyInfo property, HeroActionParams actionParams, HeroActionField actionFieldB)
        {
            // Get value on the target instance.
            object value = property.PropertyType;

            // Test value type.
            if (value == typeof(int))
            {
                GetIntegerField.BuildFieldB(property.Name + " (Int): ", actionParams, actionFieldB);
            }
            else if (value == typeof(float))
            {
                GetFloatField.BuildFieldB(property.Name + " (Float): ", actionParams, actionFieldB);
            }
            else if (value == typeof(string))
            {
                GetStringField.BuildFieldB(property.Name + " (String): ", actionParams, actionFieldB);
            }
            else if (value == typeof(bool))
            {
                GetBoolField.BuildFieldB(property.Name + " (Bool): ", actionParams, actionFieldB);
            }
            else if (value == typeof(HeroKitObject))
            {
                GetHeroObjectField.BuildFieldB(property.Name + " (HeroObject): ", actionParams, actionFieldB);
            }
            else if (value == typeof(GameObject))
            {
                GetGameObjectField.BuildFieldB(property.Name + " (GameObject): ", actionParams, actionFieldB);
            }
            else
            {
                string valueType = value.ToString();
                int index = valueType.LastIndexOf('.');
                if (valueType.Length >= 2)
                    valueType = valueType.Substring(index + 1, valueType.Length - index - 1);

                SimpleLayout.Label(valueType + " " + property.Name + ": [Value can't be stored by HeroKit]");
            }
        }

        /// <summary>
        /// Get a list of properties.
        /// </summary>
        /// <param name="monoScript">The script that contains the properties.</param>
        /// <returns>The list of properties in a script.</returns>
        private static PropertyInfo[] GetPropertiesFromScript(MonoScript monoScript)
        {
            // exit early if there is no monoscript
            if (monoScript == null) return new PropertyInfo[0];

            // get the class attached to the monoscript
            System.Type classType = monoScript.GetClass();

            // exit early of there is no class attached to monoscript
            if (classType == null) return new PropertyInfo[0];

            PropertyInfo[] fieldInfo = classType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            return fieldInfo;
        }

        /// <summary>
        /// Create a drop-down list of script properties in the action field.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="items">A list of properties in a script.</param>
        /// <returns>The data for this action field.</returns>
        private static PropertyFieldData BuildItemFieldList(PropertyFieldData data, PropertyInfo[] items)
        {
            // hero object does not exist on game object or int list is empty
            if (items.Length == 0)
            {
                data.fieldID = 0;
            }
            // everything looks okay. draw int list.
            else
            {
                // if we are referencing a field that no longer exists in int list, reset condition int field
                if (items.Length < data.fieldID)
                {
                    data.fieldID = 0;
                }

                // draw the list
                data.fieldID = HeroField.PropertyListField.SetValues(items, data.fieldID, 0);
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
        public static PropertyFieldData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            PropertyFieldData data = new PropertyFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldID = actionField.ints[0];
            data.startID = actionField.ints[1];
            data.endID = actionField.ints[2];
            return data;
        }
    }

    /// <summary>
    /// Data needed to use GetPropertyField.
    /// </summary>
    public struct PropertyFieldData : ITitle
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