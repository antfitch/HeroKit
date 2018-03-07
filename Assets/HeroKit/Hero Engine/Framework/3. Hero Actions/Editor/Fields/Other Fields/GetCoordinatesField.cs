// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with X, Y, Z coordinates (Integers).
    /// </summary>
    public static class GetCoordinatesField
    {
        /// <summary>
        /// Get X, Y, Z coordinates.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldA">Action field parameters.</param>
        /// <param name="actionFieldB">Action field parameters.</param>
        /// <param name="actionFieldC">Action field parameters.</param>
        /// <param name="actionFieldD">Action field parameters.</param>
        /// <param name="actionFieldE">Action field parameters.</param>
        /// <param name="actionFieldF">Action field parameters.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static void BuildField(string title, HeroActionParams actionParams, HeroActionField actionFieldA, HeroActionField actionFieldB, HeroActionField actionFieldC, HeroActionField actionFieldD, HeroActionField actionFieldE, HeroActionField actionFieldF, bool titleToLeft = false)
        {
            if (title != "") SimpleLayout.Label(title);

            bool changeX = GetBoolValue.BuildField("X:", actionParams, actionFieldA, true);
            if (changeX) GetFloatField.BuildFieldA("", actionParams, actionFieldB);
            bool changeY = GetBoolValue.BuildField("Y:", actionParams, actionFieldC, true);
            if (changeY) GetFloatField.BuildFieldA("", actionParams, actionFieldD);
            bool changeZ = GetBoolValue.BuildField("Z:", actionParams, actionFieldE, true);
            if (changeZ) GetFloatField.BuildFieldA("", actionParams, actionFieldF);
        }
    }
}