// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using System.Collections.Generic;
using HeroKit.Editor.ActionBlockFields;
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with a drop-down field.
    /// </summary>
    public static class GetDropDownBField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get values from a drop-down field.
        /// </summary>
        /// <typeparam name="T">The HeroList type.</typeparam>
        /// <typeparam name="U">The items for the HeroList type.</typeparam>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="dropDownField">A hero list.</param>
        /// <param name="items">The items for the hero list.</param>
        /// <returns>The selected item in the drop-down list field</returns>
        public static int BuildField<T,U>(string title, HeroActionParams actionParams, HeroActionField actionField, T dropDownField, List<U> items) where T : IDropDownListB<U>
        {
            DropDownBFieldData data = CreateFieldData(title, actionField);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "") SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();

            //-----------------------------------------
            // Display the drop down field
            //-----------------------------------------
            actionField.ints[0] = dropDownField.SetValues(data.fieldID, items, 0);

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            //-----------------------------------------
            // Return ID of item selected in the drop down field
            //-----------------------------------------
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
        private static DropDownBFieldData CreateFieldData(string title, HeroActionField actionField)
        {
            DropDownBFieldData data = new DropDownBFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldID = actionField.ints[0];

            return data;
        }
    }

    /// <summary>
    /// Data needed to use GetDropDownBField.
    /// </summary>
    public struct DropDownBFieldData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 1, 0);
        }

        public string title { get; set; }
        public int fieldID { get; set; }
    }
}