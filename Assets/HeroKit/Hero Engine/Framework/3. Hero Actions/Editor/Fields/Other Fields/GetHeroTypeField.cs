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
    /// Action field for the hero kit editor. Work with hero objects.
    /// </summary>
    public static class GetHeroTypeField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get a hero object.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        public static void BuildField(string title, HeroActionParams actionParams, HeroActionField actionField)
        {
            // create the fields
            HeroTypeFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "") SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();

            //-----------------------------------------
            // Get the integer list you want to work with.
            // The integer list is in hero object editor > Variables
            //-----------------------------------------
            data.fieldValue = SimpleLayout.ObjectField(data.fieldValue, HeroKitCommon.GetWidthForField(65));

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.heroObjects[0] = data.fieldValue;

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
        public static HeroTypeFieldData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            HeroTypeFieldData data = new HeroTypeFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldValue = actionField.heroObjects[0];

            return data;
        }
    }

    /// <summary>
    /// Data needed for GetHeroTypeField.
    /// </summary>
    public struct HeroTypeFieldData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.heroObjects, 1, null);
        }

        public string title { get; set; }
        public HeroObject fieldValue;
    }
}