// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with a game object or hero object.
    /// </summary>
    public static class GetSceneObjectValue
    {
        /// <summary>
        /// Get a hero kit object or game object.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldA">Action field.</param>
        /// <param name="actionFieldB">Action field.</param>
        public static void BuildField(string title, HeroActionParams actionParams, HeroActionField actionFieldA, HeroActionField actionFieldB)
        {
            SimpleLayout.Label("The type of object that contains the " + title + ":");

            string[] items = { "Hero Object", "Game Object" };
            int result = GetDropDownField.BuildField("", actionParams, actionFieldA, new GenericListField(items));

            if (result == 1)
            {
                SimpleLayout.Label("The hero object that contains the " + title + ":");
                GetHeroObjectField.BuildFieldA("", actionParams, actionFieldB);
            }

            else if (result == 2)
            {
                SimpleLayout.Label("The game object that contains the " + title + ":");
                GetGameObjectField.BuildFieldA("", actionParams, actionFieldB);
            }
        }
    }
}