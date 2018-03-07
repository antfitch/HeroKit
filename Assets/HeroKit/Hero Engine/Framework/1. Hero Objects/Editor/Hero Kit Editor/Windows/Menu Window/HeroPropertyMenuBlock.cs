// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEditor;
using HeroKit.Scene;
using UnityEngine;
using SimpleGUI.Fields;
using SimpleGUI;

namespace HeroKit.Editor
{
    /// <summary>
    /// Main Menu for the Hero Kit Editor (Hero Property).
    /// </summary>
    internal class HeroPropertyMenuBlock : EditorWindow
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The hero kit editor.
        /// </summary>
        private static HeroKitEditor heroEditor;
        /// <summary>
        /// ID of the currently selected variable type in the main menu. (ex. int, float)
        /// </summary>
        public static int variableID = 0;
        /// <summary>
        /// A character that looks like a long dash. This goes in front of things in the menu that cannot be expanded.
        /// </summary>
        public static string textIcon = '\u2014'.ToString();
        /// <summary>
        /// Indent level for things in the menu that need to be indented.
        /// </summary>
        public static int indentLevel = 18;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Block in which to display the main menu. 
        /// </summary>
        /// <param name="heroKitProperty">Hero property info to display in the menu.</param>
        /// <param name="heroKitEditor">Hero kit editor.</param>
        public static void Block(HeroKitProperty heroKitProperty, HeroKitEditor heroKitEditor)
        {
            // exit early if object is null
            if (heroKitProperty == null)
            {
                return;
            }

            // save the editor
            heroEditor = heroKitEditor;

            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
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
            if (variableID == menuID)
            {
                style = Box.StyleMenuSelected;
                buttonStyle = Button.StyleFoldoutTextB;
            }

            // show options
            SimpleLayout.BeginHorizontal(style);
            SimpleLayout.Button(textIcon + " " + itemName, showBlockContent, menuID, buttonStyle);
            SimpleLayout.EndHorizontal();
        }

        /// <summary>
        /// Show the variables in the canvas window.
        /// </summary>
        /// <param name="varID">Index of the variable type.</param>
        public static void showBlockContent(int varID)
        {
            HeroKitCommon.deselectField();
            variableID = varID;
        }
        /// <summary>
        /// Change the selection in the menu.
        /// </summary>
        public static void changeSelection()
        {
            switch (Event.current.keyCode)
            {
                case KeyCode.DownArrow:
                    gotoVariablesDown();
                    break;

                case KeyCode.UpArrow:
                    gotoVariablesUp();
                    break;
            }

            // refresh the editor
            heroEditor.Repaint();
        }
        /// <summary>
        /// Go down to variable type X.
        /// </summary>
        private static void gotoVariablesDown()
        {
            int count = 7;

            if (variableID < count - 1)
            {
                variableID = HeroObjectMenuBlock.goDownHeroList(variableID);
            }
        }
        /// <summary>
        /// Go up to variable type X.
        /// </summary>
        private static void gotoVariablesUp()
        {
            if (variableID > 0)
            {
                variableID = HeroObjectMenuBlock.goUpHeroList(variableID);
            }
        }
    }
}