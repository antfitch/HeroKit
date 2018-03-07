// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Work with hero objects.
    /// </summary>
    public static class HeroTypeValue
    {
        /// <summary>
        /// Get a hero object.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The hero object.</returns>
        public static HeroObject GetValue(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the value
            HeroObject value = action.actionFields[actionFieldID].heroObjects[0];

            // Return the value
            return value;
        }
    }
}