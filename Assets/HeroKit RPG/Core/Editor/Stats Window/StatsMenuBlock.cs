// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;
using System.Collections.Generic;
using System.Linq;

namespace HeroKit.RpgEditor
{
    /// <summary>
    /// Menu Block for an item that appears in Hero Kit RPG Editor. 
    /// </summary>
    internal class StatsMenuBlock : EditorWindow
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
        private const string blockName = "Stat";
        /// <summary>
        /// Indent level for the items in the menu.
        /// </summary>
        private static readonly int indentMenu = HeroKitMenuBlock.indentLevel;

        /// <summary>
        /// List of states.
        /// </summary>
        private static List<HeroProperties> items;
        /// <summary>
        /// List of states where context menu was openend.
        /// </summary>
        private static List<HeroProperties> itemsContextMenu;
        /// <summary>
        /// List of states that were copied (and can be pasted somewhere).
        /// </summary>
        private static List<HeroProperties> savedFieldList;
        /// <summary>
        /// List of the most recently deleted states.
        /// </summary>
        private static readonly LinkedList<HeroProperties> deletedFields;
        /// <summary>
        /// List of the index values for the most recently deleted states.
        /// The index tells us where the state was in the list before it was deleted.
        /// </summary>
        private static readonly LinkedList<int> deletedFieldsIndex;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        static StatsMenuBlock()
        {
            // create deleted field list
            deletedFields = new LinkedList<HeroProperties>();
            deletedFieldsIndex = new LinkedList<int>();
        }
        /// <summary>
        /// Block to display in the menu. Get list from hero kit object.
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
            items = heroObject.propertiesList.properties;

            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            HeroKit.Editor.HeroKitCommon.DrawMenuItems(blockName, deleteItem, addItem);
            DrawItems();   
        }
        /// <summary>
        /// Draw the body of the block.
        /// </summary>
        private static void DrawItems()
        {
            // exit early if there are no items
            if (items == null || items.Count == 0) return;

            // display items  
            for (int i = 0; i < items.Count; i++)
            {
                // get the box to draw around the foldout
                GUIStyle style = Box.StyleDefault;
                GUIStyle buttonStyle = Button.StyleFoldoutText;
                if (HeroKitMenuBlock.itemFocus && HeroKitMenuBlock.itemID == i)
                {
                    style = Box.StyleMenuSelected;
                    buttonStyle = Button.StyleFoldoutTextB;
                }

                string itemName = (items[i].itemProperties.strings.items[0].value != "") ? items[i].itemProperties.strings.items[0].value : "[none]";

                // show foldout
                SimpleLayout.BeginHorizontal(style);
                //SimpleLayout.Space(indentMenu);
                SimpleLayout.Button(i + ": " + itemName, showBlockContent, showContextMenu, i, buttonStyle);
                SimpleLayout.EndHorizontal();
            }
        }

        // --------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------

        private static HeroProperties getHeroProperties()
        {
            return HeroKitCommon.getHeroProperties(HeroKitCommon.statsProperties);
        }

        // --------------------------------------------------------------
        // Methods (On Click State in Menu)
        // --------------------------------------------------------------

        /// <summary>
        /// Show the state in the canvas window.
        /// </summary>
        /// <param name="itemID">ID of the state.</param>
        public static void showBlockContent(int itemID)
        {
            HeroKitCommon.ResetCanvasContent();
            HeroKitMenuBlock.itemFocus = true;
            HeroKitMenuBlock.itemID = itemID;
        }

        // --------------------------------------------------------------
        // Methods (Context Menu)
        // --------------------------------------------------------------

        /// <summary>
        /// When a state title is right-clicked, display the context menu for it.
        /// </summary>
        /// <param name="itemID">ID of the state.</param>
        public static void showContextMenu(int itemID)
        {
            int buttonID = itemID;
            HeroKitMenuBlock.itemIndexContext = itemID;

            items = heroObject.propertiesList.properties;

            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("New " + blockName + " Above"), false, addItemAbove, buttonID);
            menu.AddItem(new GUIContent("New " + blockName + " Below"), false, addItemBelow, buttonID);
            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Copy " + blockName), false, copyItem, buttonID);
            menu.AddSeparator("");

            if (savedFieldList != null && savedFieldList.Count != 0)
            {
                menu.AddItem(new GUIContent("Paste " + blockName + " Above"), false, pasteItemAbove, buttonID);
                menu.AddItem(new GUIContent("Paste " + blockName + " Below"), false, pasteItemBelow, buttonID);
                menu.AddItem(new GUIContent("Paste " + blockName + " Here"), false, pasteItemHere, buttonID);
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Paste " + blockName + " Above"));
                menu.AddDisabledItem(new GUIContent("Paste " + blockName + " Below"));
                menu.AddDisabledItem(new GUIContent("Paste " + blockName + " Here"));
            }
            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Delete " + blockName), false, deleteItem, buttonID);
            if (deletedFieldsIndex != null && deletedFieldsIndex.Count != 0)
            {
                menu.AddItem(new GUIContent("Restore Last Deleted " + blockName), false, restoreItem, buttonID);
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Restore Last Deleted " + blockName));
            }
            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Move " + blockName + " Up"), false, moveItemUp, buttonID);
            menu.AddItem(new GUIContent("Move " + blockName + " Down"), false, moveItemDown, buttonID);
            menu.ShowAsContext();
        }
        /// <summary>
        /// Move item up.
        /// </summary>
        /// <param name="obj">Item to move up.</param>
        private static void moveItemUp(object obj)
        {
            int index = (int)obj;
            HeroKitCommon.moveItemUp(items, index);
        }
        /// <summary>
        /// Move item down.
        /// </summary>
        /// <param name="obj">Item to move down.</param>
        private static void moveItemDown(object obj)
        {
            int index = (int)obj;
            HeroKitCommon.moveItemDown(items, index);
        }
        /// <summary>
        /// Add item at end of list.
        /// </summary>
        private static void addItem()
        {
            HeroKitCommon.addItem(items, getHeroProperties());
        }
        /// <summary>
        /// Add item above another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void addItemAbove(object obj)
        {
            int index = (int)obj;
            HeroKitCommon.addItem(items, getHeroProperties(), index);
        }
        /// <summary>
        /// Add item below another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void addItemBelow(object obj)
        {
            int index = (int)obj + 1;
            HeroKitCommon.addItem(items, getHeroProperties(), index);
        }
        /// <summary>
        /// Copy an item.
        /// </summary>
        /// <param name="obj">The item.</param>
        private static void copyItem(object obj)
        {
            int index = (int)obj;
            savedFieldList = HeroKitCommon.copyItem(items, savedFieldList, index);
        }
        /// <summary>
        /// Insert item above another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void pasteItemAbove(object obj)
        {
            // this is called when item right clicked
            int index = (int)obj;
            HeroKitCommon.pasteItem(items, savedFieldList, index);
        }
        /// <summary>
        /// Insert item below another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void pasteItemBelow(object obj)
        {
            // this is called when item right clicked
            int index = (int)obj + 1;
            HeroKitCommon.pasteItem(items, savedFieldList, index);
        }
        /// <summary>
        /// Delete an item and replace it with this item.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void pasteItemHere(object obj)
        {
            int index = (int)obj;
            HeroKitCommon.pasteItemHere(items, savedFieldList, index);
        }
        /// <summary>
        /// Delete item at end of list.
        /// </summary>
        private static void deleteItem()
        {
            int index = items.Count - 1;
            HeroKitCommon.deleteItem(items, deletedFields, deletedFieldsIndex, blockName, index);
        }
        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="object">The item.</param>
        private static void deleteItem(object obj)
        {
            int index = (int)obj;
            HeroKitCommon.deleteItem(items, deletedFields, deletedFieldsIndex, blockName, index);
        }
        /// <summary>
        /// Restore the last item that was deleted from the list.
        /// </summary>
        private static void restoreItem(object obj)
        {
            HeroKitCommon.restoreItem(items, deletedFields, deletedFieldsIndex);
        }
    }
}