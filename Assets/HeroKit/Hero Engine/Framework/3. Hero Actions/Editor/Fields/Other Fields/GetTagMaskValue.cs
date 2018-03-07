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
    /// Action field for the hero kit editor. Work with tag masks.
    /// </summary>
    public static class GetTagMaskValue
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get a tag mask.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static void BuildField(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            // create the fields
            TagMaskValueData data = CreateFieldData(title, actionField, actionParams.heroObject);

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
            data.fieldValue = SimpleLayout.TagMaskField(data.fieldValue, 200);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[0] = data.fieldValue;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
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
        public static TagMaskValueData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            TagMaskValueData data = new TagMaskValueData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldValue = actionField.ints[0];

            return data;
        }
    }

    /// <summary>
    /// Data needed for a GetTagMaskValue.
    /// </summary>
    public struct TagMaskValueData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 1, 0);
        }
        public string title { get; set; }
        public int fieldValue;
    }
}