// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get a tag mask from a task field.
    /// </summary>
    public static class TagMaskValue
    {
        /// <summary>
        /// Get a value from a tag mask field.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The value for the tag mask.</returns>
        public static int GetValue(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the value
            int value = action.actionFields[actionFieldID].ints[0];

            // Return the value
            return value;
        }
    }
}