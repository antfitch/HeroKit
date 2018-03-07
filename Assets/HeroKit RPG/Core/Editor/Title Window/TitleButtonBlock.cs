// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;

namespace HeroKit.RpgEditor
{
    /// <summary>
    /// Display the settings button in the title window.
    /// </summary>
    public static class TitleButtonBlock
    {
        /// <summary>
        /// Display block for hero property instead of hero object.
        /// </summary>
        static bool forProperties;

        /// <summary>
        /// Block to display in the title window. 
        /// </summary>
        public static void Block(bool useForProperties=false)
        {
            forProperties = useForProperties;
            DrawButton();     
        }
        /// <summary>
        /// Draw the button in the block.
        /// </summary>
        private static void DrawButton()
        {
            GUILayout.BeginVertical();
            SimpleLayout.Space(10);  

            GUILayout.BeginHorizontal();
            SimpleLayout.Button(Content.MenuIcon, clickOptionsButton, Button.StyleA, 25);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        /// <summary>
        /// Click on the button in the block.
        /// </summary>
        private static void clickOptionsButton()
        {
            HeroKitEditor.typeID = 0;
            //HeroKit.Editor.HeroKitCommon.ResetCanvasContent();
            //HeroKitMenuBlock.settingsFocus = true;
        }
    }
}