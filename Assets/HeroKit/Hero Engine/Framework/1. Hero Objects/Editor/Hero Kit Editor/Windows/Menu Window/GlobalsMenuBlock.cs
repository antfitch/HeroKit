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
    /// Menu Block for Variables that appears in Hero Kit Editor. (Hero Object)
    /// </summary>
    internal class GlobalsMenuBlock : EditorWindow
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The hero object.
        /// </summary>
        private static HeroObject heroObject;
        /// <summary>
        /// Name of the block.
        /// </summary>
        private const string blockName = "Globals";
        /// <summary>
        /// Indent level for the items in the menu.
        /// </summary>
        private static readonly int indentMenu = HeroObjectMenuBlock.indentLevel;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        static GlobalsMenuBlock()
        {
        }
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

            // assign hero object to this class
            heroObject = heroKitObject;

            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            DrawHeading();
            DrawItems();
        }
        /// <summary>
        /// Draw the heading for this block.
        /// </summary>
        private static void DrawHeading()
        {
            // get the box to draw around the foldout
            GUIStyle style = Box.StyleDefault;
            GUIStyle buttonStyle = Button.StyleFoldoutHeading;
            if (HeroObjectMenuBlock.globalFocus && HeroObjectMenuBlock.globalID == -1)
            {
                style = Box.StyleMenuSelected;
                buttonStyle = Button.StyleFoldoutHeadingB;
            }

            // show foldout
            SimpleLayout.BeginHorizontal(style);
            GUIStyle foldoutStyle = (heroObject.globalsVisible) ? Button.StyleFoldoutOpen : Button.StyleFoldoutClosed;
            SimpleLayout.Button("", toggleBlock, foldoutStyle, 10);
            SimpleLayout.Button(blockName, showBlockTitle, buttonStyle);
            SimpleLayout.EndHorizontal();
        }
        /// <summary>
        /// Draw the body of the block.
        /// </summary>
        private static void DrawItems()
        {
            // exit early if list is hidden
            if (!heroObject.globalsVisible) return;

            DrawListItem("Integers", 0);
            DrawListItem("Floats", 5);
            DrawListItem("Bools", 1);
            DrawListItem("Strings", 2);
            DrawListItem("Game Objects", 3);
            DrawListItem("Hero Objects", 4);
            DrawListItem("Unity Objects", 6);

            SimpleLayout.Space(5);
        }

        /// <summary>
        /// Draw an item in the list.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="menuID">ID assigned to the list type.</param>
        private static void DrawListItem(string itemName, int menuID)
        {
            // get the box to draw around the foldout
            GUIStyle style = Box.StyleDefault;
            GUIStyle buttonStyle = Button.StyleFoldoutText;
            if (HeroObjectMenuBlock.globalFocus && HeroObjectMenuBlock.globalID == menuID)
            {
                style = Box.StyleMenuSelected;
                buttonStyle = Button.StyleFoldoutTextB;
            }

            // show foldout
            SimpleLayout.BeginHorizontal(style);
            SimpleLayout.Space(indentMenu);
            SimpleLayout.Button(HeroObjectMenuBlock.textIcon + " " + itemName, showBlockContent, menuID, buttonStyle);
            SimpleLayout.EndHorizontal();
        }

        // --------------------------------------------------------------
        // Methods (On Click Globals in Menu)
        // --------------------------------------------------------------

        /// <summary>
        /// Toggle the container for all globals on and off.
        /// </summary>
        private static void toggleBlock()
        {
            HeroKitCommon.deselectField();
            HeroKitCommon.toggleBool(ref heroObject.globalsVisible);
        }
        /// <summary>
        /// Show the variable type in the canvas window.
        /// </summary>
        /// <param name="globalID">ID of the variable type.</param>
        public static void showBlockContent(int globalID)
        {
            HeroKitCommon.ResetCanvasContent();
            HeroObjectMenuBlock.typeID = 6;
            HeroObjectMenuBlock.globalFocus = true;
            HeroObjectMenuBlock.globalID = globalID;
        }
        /// <summary>
        /// Show Variables in the canvas window. 
        /// </summary>
        public static void showBlockTitle()
        {
            HeroKitCommon.ResetCanvasContent();
            HeroObjectMenuBlock.typeID = 6;
            HeroObjectMenuBlock.globalFocus = true;            
        }
    }
}