// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;

namespace HeroKit.Editor.ActionField
{ 
    /// <summary>
    /// Action field for the hero kit editor. Work with a camera.
    /// </summary>
    public static class GetCameraField
    {
        /// <summary>
        /// Get the camera to use.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldA">Action field parameters.</param>
        /// <param name="actionFieldB">Action field parameters.</param>
        public static void BuildField(string title, HeroActionParams actionParams, HeroActionField actionFieldA, HeroActionField actionFieldB)
        {
            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (title != "") SimpleLayout.Label(title);

            //-----------------------------------------
            // Display the fields
            //-----------------------------------------
            bool customCamera = GetBoolValue.BuildField("Use a specific camera that is not the main camera?", actionParams, actionFieldA, true);
            if (customCamera)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                GetHeroObjectField.BuildFieldA("The camera is on this hero object:", actionParams, actionFieldB);
                SimpleLayout.EndVertical();
            }
        }
    }
}