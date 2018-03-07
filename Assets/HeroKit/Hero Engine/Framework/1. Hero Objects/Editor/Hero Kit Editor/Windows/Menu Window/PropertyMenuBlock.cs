// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;

namespace HeroKit.Editor
{
    /// <summary>
    /// Menu Block for Properties that appears in Hero Kit Editor. (Hero Object)
    /// </summary>
    internal class PropertyMenuBlock : EditorWindow
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// Name of the block.
        /// </summary>
        private const string blockName = "Properties";

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Block to display in the menu. 
        /// </summary>
        /// <param name="heroKitObject">Hero object info to display in the menu.</param>
        public static void Block(HeroObject heroKitObject)
        {
            // exit early if object is null
            if (heroKitObject == null)
            {
                return;
            }

            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            // get the box to draw around the foldout
            GUIStyle style = Box.StyleDefault;
            GUIStyle buttonStyle = Button.StyleFoldoutHeading;
            if (HeroObjectMenuBlock.propertyFocus)
            {
                style = Box.StyleMenuSelected;
                buttonStyle = Button.StyleFoldoutHeadingB;
            }

            // draw the properties
            SimpleLayout.BeginHorizontal(style);
            SimpleLayout.Button(" " + HeroObjectMenuBlock.textIcon + "  " + blockName, showBlockContent, buttonStyle);
            SimpleLayout.EndHorizontal();
        }
        /// <summary>
        /// Show the property info in the canvas window.
        /// </summary>
        public static void showBlockContent()
        {
            HeroKitCommon.ResetCanvasContent();
            HeroObjectMenuBlock.typeID = 4;
            HeroObjectMenuBlock.propertyFocus = true;
        }
    }
}