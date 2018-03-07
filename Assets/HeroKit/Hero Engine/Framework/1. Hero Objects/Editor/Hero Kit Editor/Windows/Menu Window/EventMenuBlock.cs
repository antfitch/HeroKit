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
    /// Menu Block for Hero Events that appears in Hero Kit Editor. (Hero Object)
    /// </summary>
    internal class EventMenuBlock : EditorWindow
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
        private const string blockName = "Event";
        /// <summary>
        /// The ID of the state that this event exists within.
        /// </summary>
        private static int stateIndex;

        /// <summary>
        /// List of events.
        /// </summary>
        private static List<HeroEvent> items;
        /// <summary>
        /// List of events where context menu was openend.
        /// </summary>
        private static List<HeroEvent> itemsContextMenu;
        /// <summary>
        /// List of events that were copied (and can be pasted somewhere).
        /// </summary>
        private static List<HeroEvent> savedFieldList;
        /// <summary>
        /// List of the most recently deleted events.
        /// </summary>
        private static readonly LinkedList<HeroEvent> deletedFields;
        /// <summary>
        /// List of the index values for the most recently deleted events.
        /// The index tells us where the events was in the list before it was deleted.
        /// </summary>
        private static readonly LinkedList<int> deletedFieldsIndex;
        /// <summary>
        /// List of the state IDs for the most recently deleted events.
        /// </summary>
        private static readonly LinkedList<int> deletedFieldsState;

        /// <summary>
        /// Indent level for the items in the menu.
        /// </summary>
        private static readonly int indentMenu = HeroObjectMenuBlock.indentLevel * 2 - 6;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        static EventMenuBlock()
        {
            // create deleted field list
            deletedFields = new LinkedList<HeroEvent>();
            deletedFieldsIndex = new LinkedList<int>();
            deletedFieldsState = new LinkedList<int>();
        }
        /// <summary>
        /// Block to display in the menu. Get list from hero kit object.
        /// </summary>
        /// <param name="heroKitObject">Hero object info to display in the menu.</param>
        /// <param name="indexState">ID of the state where this event resides.</param>
        public static void Block(HeroObject heroKitObject, int indexState)
        {
            // exit early if object is null
            if (heroKitObject == null)
            {
                return;
            }

            // save the id of the state that this event belongs in
            stateIndex = indexState;

            // assign hero object to this class
            heroObject = heroKitObject;
            items = heroObject.states.states[stateIndex].heroEvent;

            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            DrawItems();

            SimpleLayout.BeginHorizontal();
            SimpleLayout.Space(indentMenu-12);
            addItemButton();
            SimpleLayout.EndHorizontal();
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
                // get the box to draw around the foldout
                GUIStyle style = Box.StyleDefault;
                GUIStyle buttonStyle = Button.StyleFoldoutText;
                if (HeroObjectMenuBlock.stateFocus && HeroObjectMenuBlock.stateID == stateIndex && HeroObjectMenuBlock.eventID == i && HeroObjectMenuBlock.actionID == -1)
                {
                    style = Box.StyleMenuSelected;
                    buttonStyle = Button.StyleFoldoutTextB;
                }

                // use default name for event if name not provided
                string eventName = "";
                if (items[i].name == "")
                { 
                    if (items[i].eventType > 0)
                        eventName = HeroField.EventTypeField.field.items[items[i].eventType-1];
                }
                else
                {
                    eventName = items[i].name;
                }

                // show foldout
                SimpleLayout.BeginHorizontal();
                SimpleLayout.Space(indentMenu);
                SimpleLayout.BeginHorizontal(style);
                GUIStyle foldoutStyle = (items[i].visible) ? Button.StyleFoldoutOpen : Button.StyleFoldoutClosed;
                SimpleLayout.Button("", toggleBlock, showContextMenu, stateIndex, i, foldoutStyle, 10);
                SimpleLayout.Button(blockName + " " + i + ": " + eventName, showBlockContent, showContextMenu, stateIndex, i, buttonStyle);
                SimpleLayout.EndHorizontal();
                SimpleLayout.EndHorizontal();

                // show actions for open foldout
                DrawActionForEvent(i);
            }
        }
        /// <summary>
        /// Draw the actions for this event.
        /// </summary>
        /// <param name="eventIndex">ID of this event.</param>
        private static void DrawActionForEvent(int eventIndex)
        {
            // exit early if an event has been removed
            if (eventIndex >= items.Count)
            {
                return;
            }

            // draw the events for this state
            if (items[eventIndex].visible)
                ActionMenuBlock.Block(heroObject, stateIndex, eventIndex);
        }
        /// <summary>
        /// Button to add a new item to list.
        /// </summary>
        public static void addItemButton()
        {
            HeroKitCommon.DrawAddNewItem(blockName, addItem, Button.StyleAddMenuItem, 11);
        }

        // --------------------------------------------------------------
        // Methods (On Click Event in Menu)
        // --------------------------------------------------------------

        /// <summary>
        /// Toggle the event menu block on and off.
        /// </summary>
        /// <param name="i">ID assigned to the event.</param>
        private static void toggleBlock(int i)
        {
            HeroKitCommon.deselectField();
            HeroKitCommon.toggleBool(ref heroObject.states.states[stateIndex].heroEvent[i].visible);
        }
        /// <summary>
        /// Show the event in the canvas window.
        /// </summary>
        /// <param name="eventID">ID of the event.</param>
        private static void showBlockContent(int eventID)
        {
            HeroKitCommon.ResetCanvasContent();
            HeroObjectMenuBlock.typeID = 2;
            HeroObjectMenuBlock.stateFocus = true;
            HeroObjectMenuBlock.stateID = stateIndex;
            HeroObjectMenuBlock.eventID = eventID;
        }
        /// <summary>
        /// Show the event in the canvas window.
        /// </summary>
        /// <param name="eventID">ID of the event.</param>
        /// <param name="stateID">ID of the state where the event resides.</param>
        public static void showBlockContent(int eventID, int stateID)
        {
            HeroKitCommon.ResetCanvasContent();
            HeroObjectMenuBlock.typeID = 2;
            HeroObjectMenuBlock.stateFocus = true;
            HeroObjectMenuBlock.stateID = stateID;
            HeroObjectMenuBlock.eventID = eventID;
        }

        // --------------------------------------------------------------
        // Methods (Context Menu)
        // --------------------------------------------------------------

        /// <summary>
        /// When an event title is right-clicked, display the context menu for it.
        /// </summary>
        /// <param name="stateID">ID of the state where the event resides.</param>
        /// <param name="eventID">ID of the event.</param>
        public static void showContextMenu(int stateID, int eventID)
        {
            int buttonID = eventID;
            HeroObjectMenuBlock.stateIndexContext = stateID;
            HeroObjectMenuBlock.eventIndexContext = eventID;
            HeroObjectMenuBlock.actionIndexContext = -1;

            itemsContextMenu = heroObject.states.states[HeroObjectMenuBlock.stateIndexContext].heroEvent;

            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add " + blockName + " Above"), false, addItemAbove, buttonID);
            menu.AddItem(new GUIContent("Add " + blockName + " Below"), false, addItemBelow, buttonID);
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

            HeroKitCommon.deselectField();
            int indexA = index - 1;
            int indexB = index;

            if (indexA >= 0)
            {
                HeroEvent fieldA = itemsContextMenu[indexA];
                HeroEvent fieldB = itemsContextMenu[indexB];
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
                HeroEvent fieldA = itemsContextMenu[indexA];
                HeroEvent fieldB = itemsContextMenu[indexB];
                itemsContextMenu[indexA] = fieldB;
                itemsContextMenu[indexB] = fieldA;
            }
        }

        /// <summary>
        /// Add item at end of list.
        /// </summary>
        private static void addItem()
        {
            HeroKitCommon.deselectField();
            items.Add(new HeroEvent());
        }
        /// <summary>
        /// Add item at specific position in the list.
        /// </summary>
        /// <param name="index">Index in list where item should be added.</param>
        private static void addItem(int index)
        {
            HeroKitCommon.deselectField();
            items.Insert(index, new HeroEvent());
        }
        /// <summary>
        /// Add item above another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void addItemAbove(object obj)
        {
            int index = (int)obj;

            HeroKitCommon.deselectField();
            itemsContextMenu.Insert(index, new HeroEvent());
        }
        /// <summary>
        /// Add item below another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void addItemBelow(object obj)
        {
            int index = (int)obj+1;

            HeroKitCommon.deselectField();
            itemsContextMenu.Insert(index, new HeroEvent());
        }

        /// <summary>
        /// Copy an item.
        /// </summary>
        /// <param name="obj">The item.</param>
        private static void copyItem(object obj)
        {
            int index = (int)obj;

            savedFieldList = new List<HeroEvent>();
            savedFieldList.Add(new HeroEvent(itemsContextMenu[index]));
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
                List<HeroEvent> itemsToPaste = new List<HeroEvent>(savedFieldList.Select(x => x.Clone(x)));
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
                List<HeroEvent> itemsToPaste = new List<HeroEvent>(savedFieldList.Select(x => x.Clone(x)));
                itemsContextMenu.InsertRange(index, itemsToPaste);
            }
        }
        /// <summary>
        /// Insert item above another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void pasteItemAbove(object obj)
        {
            // this is called when item right clicked
            pasteItem((int)obj);
        }
        /// <summary>
        /// Insert item below another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void pasteItemBelow(object obj)
        {
            // this is called when item right clicked
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
                List<HeroEvent> itemsToPaste = new List<HeroEvent>(savedFieldList.Select(x => x.Clone(x)));
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
                saveDeletedItem(HeroObjectMenuBlock.stateIndexContext, index);
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
        /// <param name="stateID">ID of the state where the event resides.</param>
        /// <param name="eventID">ID of the event.</param>
        private static void saveDeletedItem(int stateID, int eventID)
        {
            // add to deleted item to front of the stack
            HeroEvent t = new HeroEvent();
            t = t.Clone(itemsContextMenu[eventID]);

            deletedFields.AddFirst(t);
            deletedFieldsIndex.AddFirst(eventID);
            deletedFieldsState.AddFirst(stateID);

            // if there are too many items in the stack, pop the last item in the stack
            if (deletedFields.Count > 10)
            {
                deletedFields.RemoveLast();
                deletedFieldsIndex.RemoveLast();
                deletedFieldsState.RemoveLast();
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
                int index = deletedFieldsIndex.First();
                HeroEvent field = deletedFields.First();

                // get state that contains the list of events
                List<HeroEvent> list = heroObject.states.states[stateID].heroEvent;

                // insert this event in the list
                list.Insert(index, field);

                // delete the field from the deleted items queue
                deletedFields.RemoveFirst();
                deletedFieldsIndex.RemoveFirst();
            }
        }
    }
}