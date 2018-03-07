// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;
using SimpleGUI.Fields;
using UnityEngine;

namespace HeroKit.RpgEditor
{
    // manages state-level functions for editor
    internal static class TitleBlock
    {
        public static void Block(string name, int heroType)
        {
            GUIContent icon = Content.HeroObjectIcon;

            SimpleLayout.BeginHorizontal();

            //SimpleLayout.BeginVertical();
            //SimpleLayout.Space(8);
            //SimpleLayout.Label(icon);
            //SimpleLayout.Space(7);
            //SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical();
            SimpleLayout.Space(9);
            SimpleLayout.Label(" " + name, HeroKit.Editor.HeroKitCommon.GetWidthForTitleField(88, 450), Label.StyleHeroTitle);
            SimpleLayout.Space(7);
            SimpleLayout.EndVertical();

            SimpleLayout.EndHorizontal();
        }
    }
}