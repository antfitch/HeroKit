// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with a child game object.
    /// </summary>
    public static class GetChildObjectField
    {
        /// <summary>
        /// Get a child game object.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldA">Action field parameters.</param>
        /// <param name="actionFieldB">Action field parameters.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        public static void BuildField(string title, HeroActionParams actionParams, HeroActionField actionFieldA, HeroActionField actionFieldB, bool titleToLeft = false, int rightOffset=0)
        {
            bool useChild = GetBoolValue.BuildField(title, actionParams, actionFieldA, true);
            if (useChild)
            {
                GetStringField.BuildFieldA("Name of the child object:", actionParams, actionFieldB, titleToLeft, false, rightOffset);
            }
        }
    }
}