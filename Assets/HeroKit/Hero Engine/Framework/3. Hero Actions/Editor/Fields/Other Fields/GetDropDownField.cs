// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionBlockFields;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with a drop-down field.
    /// </summary>
    public static class GetDropDownField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get values from a drop-down field.
        /// </summary>
        /// <typeparam name="T">The HeroList type.</typeparam>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="dropDownField">A hero list.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>The selected item in the drop-down list field</returns>
        public static int BuildField<T>(string title, HeroActionParams actionParams, HeroActionField actionField, T dropDownField, bool titleToLeft = false) where T : IDropDownList
        {
            DropDownFieldData data = CreateFieldData(title, actionField);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Display the operator field
            //-----------------------------------------
            actionField.ints[0] = dropDownField.SetValues(data.fieldID, 0);

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return actionField.ints[0];
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
        private static DropDownFieldData CreateFieldData(string title, HeroActionField actionField)
        {
            DropDownFieldData data = new DropDownFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldID = actionField.ints[0];

            return data;
        }
    }

    /// <summary>
    /// Data needed to use GetDropDownField.
    /// </summary>
    public struct DropDownFieldData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 1, 0);
        }

        public string title { get; set; }
        public int fieldID { get; set; }
    }
}