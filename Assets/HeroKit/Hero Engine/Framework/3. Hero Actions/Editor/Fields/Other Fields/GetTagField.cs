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
    /// Action field for the hero kit editor. Work with tags.
    /// </summary>
    public static class GetTagField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get a tag.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static void BuildField(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft=false)
        {
            TagFieldData data = CreateFieldData(title, actionField);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Display the tag field
            //-----------------------------------------
            actionField.ints[0] = HeroField.TagListField.SetValues(data.fieldID, 0);
            if (actionField.ints[0] > 0 && actionField.ints[0] <= UnityEditorInternal.InternalEditorUtility.tags.Length)
                actionField.strings[0] = UnityEditorInternal.InternalEditorUtility.tags[actionField.ints[0]-1];
            else
                actionField.strings[0] = "";

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
        public static TagFieldData CreateFieldData(string title, HeroActionField actionField)
        {
            TagFieldData data = new TagFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldID = actionField.ints[0];
            data.fieldName = actionField.strings[0];
            return data;
        }
    }

    /// <summary>
    /// Data needed for GetTagField.
    /// </summary>
    public struct TagFieldData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 1, 0);
            ActionCommon.CreateActionField(ref actionField.strings, 1, "");
        }

        public string title { get; set; }
        public int fieldID { get; set; }
        public string fieldName;
    }
}