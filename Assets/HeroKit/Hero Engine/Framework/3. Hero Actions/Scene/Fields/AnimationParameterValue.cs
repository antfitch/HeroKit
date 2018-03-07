// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get an animation parameter from a hero object.
    /// </summary>
    public static class AnimationParameterValue
    {
        /// <summary>
        /// Get the name of an animation from an animation controller.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <param name="actionFieldIDC">ID assigned to action field C.</param>
        /// <returns>The name of the animation.</returns>
        public static string GetValueA(HeroKitObject heroKitObject, int actionFieldIDA, int actionFieldIDB, int actionFieldIDC)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the value
            string value = (action.actionFields[actionFieldIDB].strings.Count > 0) ? action.actionFields[actionFieldIDB].strings[0] : "";

            // if value is not set, log this
            if (value == "")
                Debug.LogError("Animation name not found for " + action.actionTemplate.name + ". Returning \"\"." + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));

            // Return the value
            return value;
        }
    }
}