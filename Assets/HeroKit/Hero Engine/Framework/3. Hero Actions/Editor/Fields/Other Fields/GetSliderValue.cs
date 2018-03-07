// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with a slider.
    /// </summary>
    public static class GetSliderValue
    {
        /// <summary>
        /// A slider (float values).
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="min">Smallest value for the slider.</param>
        /// <param name="max">Largest value for the slider.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static void BuildFieldA(string title, HeroActionParams actionParams, HeroActionField actionField, int min=0, int max=100, bool titleToLeft = false)
        {
            SliderData slider = new SliderData();
            slider.useSlider = true;
            slider.min = min;
            slider.max = max;
            GetFloatField.BuildFieldA(title, actionParams, actionField, titleToLeft, slider);
        }

        /// <summary>
        /// A slider (integer values).
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="min">Smallest value for the slider.</param>
        /// <param name="max">Largest value for the slider.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static void BuildFieldB(string title, HeroActionParams actionParams, HeroActionField actionField, int min = 0, int max = 100, bool titleToLeft = false)
        {
            SliderData slider = new SliderData();
            slider.useSlider = true;
            slider.min = min;
            slider.max = max;
            GetIntegerField.BuildFieldA(title, actionParams, actionField, titleToLeft, slider);
        }
    }

    /// <summary>
    /// Data needed for GetSliderValue
    /// </summary>
    public struct SliderData
    {
        // for a slider
        public float min;
        public float max;
        public bool useSlider;
    }

}