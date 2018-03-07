// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------


namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Work with values in the the slider field.
    /// </summary>
    public static class SliderValue
    {
        /// <summary>
        /// Get a slider value from the slider field.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The value of the slider.</returns>
        public static int GetValue(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the value
            int value = IntegerFieldValue.GetValueA(heroKitObject, actionFieldID);

            // Return the value
            return value;
        }
    }
}