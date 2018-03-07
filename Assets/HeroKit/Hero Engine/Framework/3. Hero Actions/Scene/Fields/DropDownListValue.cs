// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get a value from a drop-down list field.
    /// </summary>
    public static class DropDownListValue
    {
        /// <summary>
        /// Get a value from a drop-down list field.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>An integer that represents the selection in the drop-down list (1-X)</returns>
        public static int GetValue(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the value
            int value = action.actionFields[actionFieldID].ints[0];

            // Return the value
            return value;
        }

        /// <summary>
        /// Get a value from a drop-down list field for an event.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="boolFields">The conditional bool field that contains the drop-down list.</param>
        /// <returns>An integer that represents the selection in the drop-down list (1-X)</returns>
        public static int GetValueEvent(HeroKitObject heroKitObject, ConditionBoolFields boolFields)
        {
            // Get the value
            int value = boolFields.operatorID;

            // Return the value
            return value;
        }

        /// <summary>
        /// Get a value from a drop-down list field for an event.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="intFields">The conditional int field that contains the drop-down list.</param>
        /// <returns>An integer that represents the selection in the drop-down list (1-X)</returns>
        public static int GetValueEvent(HeroKitObject heroKitObject, ConditionIntFields intFields)
        {
            // Get the value
            int value = intFields.operatorID;

            // Return the value
            return value;
        }
    }
}