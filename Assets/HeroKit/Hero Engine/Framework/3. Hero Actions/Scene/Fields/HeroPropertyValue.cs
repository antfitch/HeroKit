// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Work with Hero Kit Properties.
    /// </summary>
    public static class HeroPropertyValue
    {
        /// <summary>
        /// Get a hero kit property.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The hero kit property.</returns>
        public static HeroKitProperty GetValue(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the value
            HeroKitProperty value = action.actionFields[actionFieldID].heroProperties[0];

            // Return the value
            return value;
        }
    }
}