// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get an object.
    /// </summary>
    public static class RigidbodyValue
    {
        /// <summary>
        /// Get a value from an object field.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The value from the object field.</returns>
        public static Rigidbody GetValue(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the value
            Rigidbody rigidbody = action.actionFields[actionFieldID].component as Rigidbody;

            // Return the value
            return rigidbody;
        }
    }
}