// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;
using SimpleGUI.Fields;
using UnityEngine;

namespace HeroKit.Editor
{
    // manages state-level functions for editor
    internal static class TitleBlock
    {
        public static void Block(string name, int heroType)
        {
            GUIContent icon = new GUIContent();
            switch (heroType)
            {
                case 0:
                    icon = Content.HeroObjectIcon;
                    break;
                case 1:
                    icon = Content.HeroActionIcon;
                    break;
                case 2:
                    icon = Content.HeroPropertyIcon;
                    break;
            }

            SimpleLayout.BeginHorizontal();

            SimpleLayout.BeginVertical();
            SimpleLayout.Space(8);
            SimpleLayout.Label(icon);
            SimpleLayout.Space(7);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical();
            SimpleLayout.Space(9);

            SimpleLayout.Label(" " + name, HeroKitCommon.GetWidthForTitleField(88), Label.StyleHeroTitle);
            SimpleLayout.Space(7);
            SimpleLayout.EndVertical();

            SimpleLayout.EndHorizontal();
        }
    }
}