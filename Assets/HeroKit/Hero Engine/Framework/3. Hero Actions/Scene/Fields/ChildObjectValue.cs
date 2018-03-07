// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get a child object attached to a hero object.
    /// </summary>
    public static class ChildObjectValue
    {
        /// <summary>
        /// Get the name of a child object attached to a hero object.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <returns></returns>
        public static string GetValue(HeroKitObject heroKitObject, int actionFieldIDA, int actionFieldIDB)
        {
            bool onChild = BoolValue.GetValue(heroKitObject, actionFieldIDA);
            string childName = (onChild) ? StringFieldValue.GetValueA(heroKitObject, actionFieldIDB) : "";
            return childName;
        }
    }
}