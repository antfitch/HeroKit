// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionBlockFields;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with colors.
    /// </summary>
    public static class GetColorValue
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get a color.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>A color.</returns>
        public static Color BuildField(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            // create the fields
            ColorValueData data = CreateFieldData(title, actionField);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the bool list you want to work with.
            // The bool list is in hero object editor > Variables
            //-----------------------------------------
            data.fieldValue = SimpleLayout.ColorField(data.fieldValue);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.colors[0] = data.fieldValue;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return data.fieldValue;
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
        private static ColorValueData CreateFieldData(string title, HeroActionField actionField)
        {
            ColorValueData data = new ColorValueData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldValue = actionField.colors[0];

            return data;
        }
    }

    /// <summary>
    /// Data needed for GetColorValue.
    /// </summary>
    public struct ColorValueData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.colors, 1, new Color(0,0,0));
        }

        public string title { get; set; }
        public Color fieldValue;
    }
}