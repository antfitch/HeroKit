// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using HeroKit.Editor.ActionBlockFields;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with conditional fields.
    /// </summary>
    public static class GetConditionalField
    {
        // --------------------------------------------------------------
        // Actin Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Create a flag that tracks the result of a condition.
        /// </summary>
        /// <remarks>This field is hidden in the editor.</remarks>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        public static void BuildField(HeroActionParams actionParams, HeroActionField actionField)
        {
            ConditionalFieldData data = CreateFieldData(actionField);
            data.skip = false;
            actionField.bools[0] = data.skip;
        }

        // --------------------------------------------------------------
        // Initialize Action Field
        // --------------------------------------------------------------

        /// <summary>
        /// Create the subfields that we need for this action field.
        /// </summary>
        /// <param name="actionField">The action field.</param>
        /// <returns>The data for this action field.</returns>
        private static ConditionalFieldData CreateFieldData(HeroActionField actionField)
        {
            ConditionalFieldData data = new ConditionalFieldData();
            data.Init(ref actionField);
            data.skip = actionField.bools[0];
            return data;
        }
    }

    /// <summary>
    /// Data needed to use GetConditionalField.
    /// </summary>
    public struct ConditionalFieldData
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.bools, 1, false);
        }

        public bool skip;
    }
}