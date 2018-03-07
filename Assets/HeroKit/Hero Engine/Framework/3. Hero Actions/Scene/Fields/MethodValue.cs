// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.Reflection;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get a method from a method field.
    /// </summary>
    public static class MethodValue
    {
        /// <summary>
        /// Get a method from a method field.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The method.</returns>
        public static MethodInfo GetValue(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the value
            MethodInfo value = action.actionFields[actionFieldID].method.methodInfo;

            // Return the value
            return value;
        }
    }
}