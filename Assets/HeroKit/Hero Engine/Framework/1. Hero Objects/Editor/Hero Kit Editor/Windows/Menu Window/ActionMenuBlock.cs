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

namespace HeroKit.Editor
{
    /// <summary>
    /// Menu Block for Hero Actions that appears in Hero Kit Editor. (Hero Object)
    /// </summary>
    internal class ActionMenuBlock : EditorWindow
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
        private const string blockName = "Action";
        /// <summary>
        /// The Hero Event.
        /// </summary>
        private static HeroEvent eventBlock;
        /// <summary>
        /// The ID of the state that this action exists within.
        /// </summary>
        private static int stateIndex;
        /// <summary>
        /// The ID of the event that this action exists within.
        /// </summary>
        private static int eventIndex;

        /// <summary>
        /// List of actions.
        /// </summary>
        private static List<HeroAction> items;
        /// <summary>
        /// List of actions where context menu was openend.
        /// </summary>
        private static List<HeroAction> itemsContextMenu;
        /// <summary>
        /// List of actions that were copied (and can be pasted somewhere).
        /// </summary>
        private static List<HeroAction> savedFieldList;
        /// <summary>
        /// List of the most recently deleted actions.
        /// </summary>
        private static readonly LinkedList<HeroAction> deletedFields;
        /// <summary>
        /// List of the index values for the most recently deleted actions.
        /// The index tells us where the action was in the list before it was deleted.
        /// </summary>
        private static readonly LinkedList<int> deletedFieldsIndex;
        /// <summary>
        /// List of the state IDs for the most recently deleted actions.
        /// </summary>
        private static readonly LinkedList<int> deletedFieldsState;
        /// <summary>
        /// List of the event IDs for the most recently deleted actions.
        /// </summary>
        private static readonly LinkedList<int> deletedFieldsEvent;

        /// <summary>
        /// Indent level for the items in the menu.
        /// </summary>
        private static readonly int indentMenu = HeroObjectMenuBlock.indentLevel * 3 - 5;
        /// <summary>
        /// Indent level for actions that use the indent level field (conditional statement actions).
        /// </summary>
        private static int indent;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        static ActionMenuBlock()
        {
            // create deleted field list
            deletedFields = new LinkedList<HeroAction>();
            deletedFieldsIndex = new LinkedList<int>();
            deletedFieldsState = new LinkedList<int>();
            deletedFieldsEvent = new LinkedList<int>();
        }
        /// <summary>
        /// Block to display in the menu. Get list from hero kit object.
        /// </summary>
        /// <param name="heroKitObject">Hero object info to display in the menu.</param>
        /// <param name="indexState">ID of the state where this action resides.</param>
        /// <param name="indexEvent">ID of the event where this action resides.</param>
        public static void Block(HeroObject heroKitObject, int indexState, int indexEvent)
        {
            // exit early if object is null
            if (heroKitObject == null)
            {
                return;
            }

            // save the id of the state that this event belongs in
            stateIndex = indexState;
            eventIndex = indexEvent;

            // assign hero object to this class
            heroObject = heroKitObject;
            eventBlock = heroObject.states.states[stateIndex].heroEvent[eventIndex];
            items = eventBlock.actions;

            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            DrawItems();
            AddItemButton();
        }
        /// <summary>
        /// Draw the body of the block.
        /// </summary>
        private static void DrawItems()
        {
            // exit early if there are no items
            if (items == null || items.Count == 0) return;

            // display items (move up, move down, int field, 
            for (int i = 0; i < items.Count; i++)
            {
                //---------------------------------------------
                // get the box to draw around the foldout
                //---------------------------------------------
                GUIStyle style = Box.StyleDefault;
                GUIStyle buttonStyle = Button.StyleFoldoutText;
                if (HeroObjectMenuBlock.stateFocus && HeroObjectMenuBlock.stateID == stateIndex && HeroObjectMenuBlock.eventID == eventIndex && HeroObjectMenuBlock.actionID == i)
                {
                    style = Box.StyleMenuSelected;
                    buttonStyle = Button.StyleFoldoutTextB;
                }

                //---------------------------------------------
                // get the prefix to show before the name of the item
                //---------------------------------------------
                string prefix = (items[i].actionTemplate != null) ? items[i].actionTemplate.title : "";

                //---------------------------------------------
                // get the name to show for the action
                //---------------------------------------------
                string itemName = items[i].name;
                if (items[i].actionTemplate != null)
                {
                    itemName = (items[i].name != "") ? items[i].name : items[i].actionTemplate.name;
                }

                // dont show item name if prefix found and if item has no name
                itemName = (prefix != "" && items[i].name == "") ? "" : itemName;

                //---------------------------------------------
                // set indent level of this action
                //---------------------------------------------
                if (items[i].actionTemplate != null)
                {
                    // get new indent
                    indent = indent + items[i].actionTemplate.indentThis;

                    // if indent is negative, change it to zero (happens if too many end statements added)
                    if (indent < 0) indent = 0;
                }
                items[i].indent = indent;
                string space = "".PadRight(indent * 5);

                //---------------------------------------------
                // set the color of the action title text
                //---------------------------------------------
                string hexColor = (SimpleGUICommon.isProSkin) ? "FFFFFF" : "000000";
                if (items[i].actionTemplate != null)
                {
                    hexColor = SimpleGUICommon.GetHexFromColor(items[i].actionTemplate.titleColor);

                    // lighten colors for dark skin
                    if (SimpleGUICommon.isProSkin)
                        hexColor = SimpleGUICommon.AlterHexBrightness(hexColor, 150);
                }

                //---------------------------------------------
                // if button is disabled, change the color of the text
                //---------------------------------------------
                if (!items[i].active)
                {
                    buttonStyle = Button.StyleFoldoutDisabledText;
                    hexColor = SimpleGUICommon.GetHexFromColor(Button.StyleFoldoutDisabledText.normal.textColor);
                }

                //---------------------------------------------
                // show foldout
                //---------------------------------------------
                SimpleLayout.BeginHorizontal();
                SimpleLayout.Space(indentMenu);
                SimpleLayout.BeginHorizontal(style);
                string name = HeroObjectMenuBlock.textIcon + " " + blockName + " " + i + ": " + space + "<color=#" + hexColor + ">" + prefix + itemName + "</color>";
                SimpleLayout.Button(name, showBlockContent, showContextMenu, stateIndex, eventIndex, i, buttonStyle);
                SimpleLayout.EndHorizontal();
                SimpleLayout.EndHorizontal();

                //---------------------------------------------
                // set indent level of next action
                //---------------------------------------------

                // note if delete called, the last item in list won't exist. check to make sure it is still there.
                if (items.Count > i && items[i].actionTemplate != null)
                {
                    // get new indent
                    indent = indent + items[i].actionTemplate.indentNext;

                    // if indent is negative, change it to zero (happens if too many end statements added)
                    if (indent < 0) indent = 0;
                }

                // if we are at the end of the action list, reset indent
                if (i == (items.Count - 1)) indent = 0;

            }
        }
        /// <summary>
        /// Button to add a new item to list.
        /// </summary>
        public static void AddItemButton()
        {
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Space(indentMenu - 3);
            HeroKitCommon.DrawAddNewItem("Action", addItem, Button.StyleAddMenuItem, -2);
            SimpleLayout.EndHorizontal();
            SimpleLayout.Space(3);
        }

        // --------------------------------------------------------------
        // Methods (On Click Action in Menu)
        // --------------------------------------------------------------

        /// <summary>
        /// Show the action in the canvas window.
        /// </summary>
        /// <param name="actionID">Index of the action.</param>
        public static void showBlockContent(int actionID)
        {
            HeroKitCommon.ResetCanvasContent();
            HeroObjectMenuBlock.typeID = 3;
            HeroObjectMenuBlock.stateFocus = true;
            HeroObjectMenuBlock.stateID = stateIndex;
            HeroObjectMenuBlock.eventID = eventIndex;
            HeroObjectMenuBlock.actionID = actionID;
        }
        /// <summary>
        /// Show the action in the canvas window.
        /// </summary>
        /// <param name="actionID">ID of the action.</param>
        /// <param name="eventID">ID of the event where the action resides.</param>
        /// <param name="stateID">ID of the state where the action resides.</param>
        public static void showBlockContent(int actionID, int eventID, int stateID)
        {
            HeroKitCommon.ResetCanvasContent();
            HeroObjectMenuBlock.typeID = 3;
            HeroObjectMenuBlock.stateFocus = true;
            HeroObjectMenuBlock.stateID = stateID;
            HeroObjectMenuBlock.eventID = eventID;
            HeroObjectMenuBlock.actionID = actionID;
        }

        // --------------------------------------------------------------
        // Methods (Context Menu)
        // --------------------------------------------------------------

        /// <summary>
        /// When an action title is right-clicked, display the context menu for it.
        /// </summary>
        /// <param name="stateID">ID of the state where the action resides.</param>
        /// <param name="eventID">ID of the event where the action resides.</param>
        /// <param name="actionID">ID of the action.</param>
        public static void showContextMenu(int stateID, int eventID, int actionID)
        {
            int buttonID = actionID;
            HeroObjectMenuBlock.stateIndexContext = stateID;
            HeroObjectMenuBlock.eventIndexContext = eventID;
            HeroObjectMenuBlock.actionIndexContext = actionID;

            itemsContextMenu = heroObject.states.states[HeroObjectMenuBlock.stateIndexContext].heroEvent[HeroObjectMenuBlock.eventIndexContext].actions;

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
            if (items[buttonID].active)
                menu.AddItem(new GUIContent("Disable " + blockName), false, toggleActivateItem, buttonID);
            else
                menu.AddItem(new GUIContent("Enable " + blockName), false, toggleActivateItem, buttonID);

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

            HeroKitCommon.deselectField();
            int indexA = index - 1;
            int indexB = index;

            if (indexA >= 0)
            {
                HeroAction fieldA = itemsContextMenu[indexA];
                HeroAction fieldB = itemsContextMenu[indexB];
                itemsContextMenu[indexA] = fieldB;
                itemsContextMenu[indexB] = fieldA;
            }
        }
        /// <summary>
        /// Move item down.
        /// </summary>
        /// <param name="obj">Item to move down.</param>
        private static void moveItemDown(object obj)
        {
            int index = (int)obj;

            HeroKitCommon.deselectField();
            int indexA = index;
            int indexB = index + 1;

            if (indexB < itemsContextMenu.Count)
            {
                HeroAction fieldA = itemsContextMenu[indexA];
                HeroAction fieldB = itemsContextMenu[indexB];
                itemsContextMenu[indexA] = fieldB;
                itemsContextMenu[indexB] = fieldA;
            }
        }

        /// <summary>
        /// Activate the item.
        /// </summary>
        /// <param name="obj">Item to activate.</param>
        private static void toggleActivateItem(object obj)
        {
            int index = (int)obj;

            HeroKitCommon.deselectField();
            itemsContextMenu[index].active = !itemsContextMenu[index].active;
        }

        /// <summary>
        /// Add item at end of list.
        /// </summary>
        private static void addItem()
        {
            HeroKitCommon.deselectField();
            items.Add(new HeroAction());
        }
        /// <summary>
        /// Add item at specific position in the list.
        /// </summary>
        /// <param name="index">Index in list where item should be added.</param>
        public static void addItem(int index)
        {
            HeroKitCommon.deselectField();
            items.Insert(index, new HeroAction());
        }
        /// <summary>
        /// Add item above another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void addItemAbove(object obj)
        {
            int index = (int)obj;

            HeroKitCommon.deselectField();
            itemsContextMenu.Insert(index, new HeroAction());
        }
        /// <summary>
        /// Add item below another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void addItemBelow(object obj)
        {
            int index = (int)obj + 1;

            HeroKitCommon.deselectField();
            itemsContextMenu.Insert(index, new HeroAction());
        }

        /// <summary>
        /// Copy an item.
        /// </summary>
        /// <param name="obj">The item.</param>
        private static void copyItem(object obj)
        {
            int index = (int)obj;

            savedFieldList = new List<HeroAction>();
            savedFieldList.Add(new HeroAction(itemsContextMenu[index]));
        }

        /// <summary>
        /// Insert item at the end of the list.
        /// </summary>
        private static void pasteItem()
        {
            // paste at end of list
            if (savedFieldList != null)
            {
                HeroKitCommon.deselectField();
                List<HeroAction> itemsToPaste = new List<HeroAction>(savedFieldList.Select(x => x.Clone(x)));
                itemsContextMenu.AddRange(itemsToPaste);
            }
        }
        /// <summary>
        /// Insert item at a specific index in the list.
        /// </summary>
        /// <param name="index">The index where the items should be inserted.</param>
        private static void pasteItem(int index)
        {
            // paste at specific location in list
            if (savedFieldList != null)
            {
                HeroKitCommon.deselectField();
                List<HeroAction> itemsToPaste = new List<HeroAction>(savedFieldList.Select(x => x.Clone(x)));
                itemsContextMenu.InsertRange(index, itemsToPaste);
            }
        }
        /// <summary>
        /// Insert item above another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void pasteItemAbove(object obj)
        {
            // this is called when action right clicked
            pasteItem((int)obj);
        }
        /// <summary>
        /// Insert item below another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void pasteItemBelow(object obj)
        {
            // this is called when action right clicked
            pasteItem((int)obj + 1);
        }
        /// <summary>
        /// Delete an item and replace it with this item.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void pasteItemHere(object obj)
        {
            int index = (int)obj;

            // paste at specific location in list
            if (savedFieldList != null)
            {
                HeroKitCommon.deselectField();
                List<HeroAction> itemsToPaste = new List<HeroAction>(savedFieldList.Select(x => x.Clone(x)));
                itemsContextMenu.RemoveAt(index);
                itemsContextMenu.InsertRange(index, itemsToPaste);           
            }
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="object">The item.</param>
        public static void deleteItem(object obj)
        {
            int index = (int)obj;

            if (itemsContextMenu.Count > index)
            {
                HeroKitCommon.deselectField();
                saveDeletedItem(HeroObjectMenuBlock.stateIndexContext, HeroObjectMenuBlock.eventIndexContext, index);
                itemsContextMenu.RemoveAt(index);
            }
            else
            {
                Debug.LogWarning("Delete " + blockName + ": Item at index [" + index + "] does not exist");
            }

        }
        /// <summary>
        /// Store a deleted item for future restoration.
        /// </summary>
        /// <param name="stateID">ID of the state where the action resides.</param>
        /// <param name="eventID">ID of the event where the action resides.</param>
        /// <param name="actionID">ID of the action.</param>
        private static void saveDeletedItem(int stateID, int eventID, int actionID)
        {
            // add to deleted item to front of the stack
            HeroAction t = new HeroAction();
            t = t.Clone(itemsContextMenu[actionID]);

            deletedFields.AddFirst(t);
            deletedFieldsIndex.AddFirst(actionID);
            deletedFieldsState.AddFirst(stateID);
            deletedFieldsEvent.AddFirst(eventID);

            // if there are too many items in the stack, pop the last item in the stack
            if (deletedFields.Count > 10)
            {
                deletedFields.RemoveLast();
                deletedFieldsIndex.RemoveLast();
                deletedFieldsState.RemoveLast();
                deletedFieldsEvent.RemoveLast();
            }
        }

        /// <summary>
        /// Restor the last item that was deleted from the list.
        /// </summary>
        private static void restoreItem(object obj)
        {
            if (deletedFieldsIndex.Count > 0)
            {
                HeroKitCommon.deselectField();

                int stateID = deletedFieldsState.First();
                int eventID = deletedFieldsEvent.First();
                int index = deletedFieldsIndex.First();
                HeroAction field = deletedFields.First();

                // get state that contains the list of events
                List<HeroAction> list = heroObject.states.states[stateID].heroEvent[eventID].actions;

                // insert this event in the list
                list.Insert(index, field);

                // delete the field from the deleted items queue
                deletedFields.RemoveFirst();
                deletedFieldsIndex.RemoveFirst();
                deletedFieldsEvent.RemoveFirst();
            }
        }
    }
}