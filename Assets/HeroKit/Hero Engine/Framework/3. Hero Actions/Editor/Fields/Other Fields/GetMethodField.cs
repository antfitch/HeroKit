// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEditor;
using HeroKit.Scene;
using SimpleGUI;
using System.Collections.Generic;
using HeroKit.Editor.ActionBlockFields;
using System.Reflection;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with method fields.
    /// </summary>
    public static class GetMethodField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get a method from a script.
        /// </summary>
        /// <param name="script">The script that contains the method.</param>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="actionField"></param>
        /// <returns>The method from the script.</returns>
        public static MethodInfo BuildFieldA(MonoScript script, string title, HeroActionParams actionParams, HeroActionField actionField)
        {
            // create the fields
            MethodFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "") SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the list you want to work with.
            //-----------------------------------------
            List<MethodInfo> items = AddMethodsToList(script);
            data = BuildItemFieldList(data, items);

            // exit early if there is no field name
            if (data.fieldName == null) return null;

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[0] = data.fieldID;
            actionField.strings[0] = data.fieldName.methodInfo.Name;
            actionField.method = data.fieldName; 

            // return a value
            return data.fieldName.methodInfo;
        }

        // --------------------------------------------------------------
        // Helpers
        // --------------------------------------------------------------

        /// <summary>
        /// Get a list of methods.
        /// </summary>
        /// <param name="monoScript">The script that contains the methods.</param>
        /// <returns>A list of methods.</returns>
        private static List<MethodInfo> AddMethodsToList(MonoScript monoScript)
        {
            // exit early if there is no monoscript
            if (monoScript == null) return new List<MethodInfo>();

            // get the class attached to the monoscript
            System.Type classType = monoScript.GetClass();

            // exit early of there is no class attached to monoscript
            if (classType == null) return new List<MethodInfo>();

            // get the methods attached to the class
            List<MethodInfo> list = new List<MethodInfo>();
            MethodInfo[] methodInfo = classType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < methodInfo.Length; i++)
            {
                if (!methodInfo[i].IsSpecialName)
                    list.Add(methodInfo[i]);
            }

            return list;
        }

        /// <summary>
        /// Create a drop-down list of methods in the action field.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="items">A list of methods.</param>
        /// <returns>The data for this action field.</returns>
        private static MethodFieldData BuildItemFieldList(MethodFieldData data, List<MethodInfo> items)
        {
            // hero object does not exist on game object or int list is empty
            if (items.Count == 0)
            {
                data.fieldID = 0;
                data.fieldName = null;
            }
            // everything looks okay. draw int list.
            else
            {
                // if we are referencing a field that no longer exists in int list, reset condition int field
                if (items.Count < data.fieldID)
                {
                    data.fieldID = 0;
                    data.fieldName = null;
                }

                // draw the list
                data.fieldID = HeroField.MethodListField.SetValues(items, data.fieldID, 0);

                // populate the fieldName
                if (data.fieldID > 0)
                    data.fieldName = new SerializableMethodInfo(items[data.fieldID - 1]);
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
        private static MethodFieldData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            MethodFieldData data = new MethodFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldID = actionField.ints[0];
            data.fieldNameString = actionField.strings[0];
            data.fieldName = null;
            return data;
        }
    }

    /// <summary>
    /// Data needed to use the GetMethodField.
    /// </summary>
    public struct MethodFieldData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 1, 0);
            ActionCommon.CreateActionField(ref actionField.strings, 1, "");
        }

        public string title { get; set; }
        public int fieldID { get; set; }
        public SerializableMethodInfo fieldName;
        public string fieldNameString;
    }

}