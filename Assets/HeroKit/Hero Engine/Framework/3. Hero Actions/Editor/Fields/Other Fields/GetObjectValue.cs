// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionBlockFields;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with an object.
    /// </summary>
    public static class GetObjectValue
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get an object.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>The object.</returns>
        public static T BuildField<T>(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false) where  T : Object
        {
            // create the fields
            GetObjectValueData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the object you want to work with
            //-----------------------------------------
            data.fieldValue = SimpleLayout.ObjectField(data.fieldValue as T, HeroKitCommon.GetWidthForField(60));

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.component = data.fieldValue;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return actionField.component as T;
        }

        // --------------------------------------------------------------
        // Initialize Action Field
        // --------------------------------------------------------------

        /// <summary>
        /// Create the subfields that we need for this action field.
        /// </summary>
        /// <param name="title">The title of the action.</param>
        /// <param name="actionField">The action field.</param>
        /// <returns>The data for this action field.</returns>
        private static GetObjectValueData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            GetObjectValueData data = new GetObjectValueData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldValue = actionField.component;

            return data;
        }
    }

    /// <summary>
    /// Data needed for GetObjectValue
    /// </summary>
    public struct GetObjectValueData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
        }

        public string title { get; set; }
        public Object fieldValue;
    }
}