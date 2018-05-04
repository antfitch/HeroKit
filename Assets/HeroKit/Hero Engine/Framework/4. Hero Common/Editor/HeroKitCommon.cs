// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroKit.Editor
{
    /// <summary>
    /// Common actions needed for HeroKit the Hero Kit Editor.
    /// </summary>
    public static class HeroKitCommon
    {
        // --------------------------------------------------------------
        // General
        // --------------------------------------------------------------

        /// <summary>
        /// Deselect the currently selected field.
        /// </summary>
        public static void deselectField()
        {
            GUI.FocusControl("");
        }

        /// <summary>
        /// Toggle a bool field in a scriptable object.
        /// </summary>
        /// <param name="b">The bool to toggle.</param>
        public static void toggleBool(ref bool b)
        {
            b = !b;
        }

        /// <summary>
        /// Draw a menu bar.
        /// </summary>
        /// <param name="name">Name of the item for which the menu bar was created.</param>
        /// <param name="add">Action delegate. Add a new item to the list.</param>
        /// <param name="paste">Action delegate. Paste an item to the end of the list.</param>
        /// <param name="restore">Action delegate. Restore an item that was deleted from the list.</param>
        /// <param name="toggleTools">Actin delegate. Toggle the menu bar on and off.</param>
        /// <param name="showTools">Should we show the menu bar?</param>
        public static void DrawMenuBar(string name, UnityAction add, UnityAction paste,
                                UnityAction restore, UnityAction toggleTools, bool showTools)
        {
            // Display menu items if states are visible
            SimpleLayout.BeginVertical(Box.StyleC);
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Button(Content.AddIcon, add, Button.StyleA, 25);
            SimpleLayout.Button(Content.RestoreIcon, restore, Button.StyleA, 25);
            SimpleLayout.Button(Content.MenuIcon, toggleTools, Button.StyleA, 25);
            SimpleLayout.EndHorizontal();
            SimpleLayout.EndVertical();
        }

        /// <summary>
        /// Draw the title at the top of a block in the canvas window.
        /// </summary>
        /// <param name="name">The name to draw.</param>
        public static void DrawBlockTitle(string name)
        {
            SimpleLayout.BeginHorizontal(Box.StyleCanvasTitleBox);
            SimpleLayout.Label(name, GetWidthForField(25), Label.StyleBlockTitle);
            SimpleLayout.EndHorizontal();
            SimpleLayout.Line();
        }

        /// <summary>
        /// Draw the name field for the block in the canvas window.
        /// </summary>
        /// <param name="name">The name to draw.</param>
        /// <returns>The name field for the block.</returns>
        public static string DrawBlockName(string name)
        {
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label("Name: ");
            string blockName = SimpleLayout.TextField(name, GetWidthForField(85));
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
            SimpleLayout.Space(5);

            return blockName;
        }

        /// <summary>
        /// If a field needs to adjust its width to match the size of the canvas, you
        /// can get the with of the field here.
        /// </summary>
        /// <param name="paddingOnRight">Padding to add on the right.</param>
        /// <param name="width">Width of the field.</param>
        /// <returns>Final width of the field.</returns>
        public static int GetWidthForField(int paddingOnRight, int width = 100)
        {
            if (HeroKitEditor.windowCanvas.width - paddingOnRight - width > 0)
            {
                width = (int)HeroKitEditor.windowCanvas.width - paddingOnRight;
            }

            return width;
        }

        /// <summary>
        /// Get the width of a title field.
        /// </summary>
        /// <param name="padding">Padding for the field.</param>
        /// <param name="width">Width of the field.</param>
        /// <returns>Final width of the field.</returns>
        public static int GetWidthForTitleField(int padding = 75, int width = 100)
        {
            if (HeroKitEditor.windowTitle.width - padding - width > 0)
            {
                width = (int)HeroKitEditor.windowTitle.width - padding;
            }

            return width;
        }

        /// <summary>
        /// This replaces values in a new list with values from an old list.
        /// Values are replaced in the new list if there is room in the new list.
        /// </summary>
        /// <typeparam name="T">HeroListObjectField (contains a name and a value).</typeparam>
        /// <typeparam name="G">Represents the value in the HeroListObjectField.</typeparam>
        /// <param name="newList">The new, empty list.</param>
        /// <param name="oldList">The current list.</param>
        /// <returns>The list.</returns>
        public static List<T> PasteValues<T, G>(List<T> newList, List<T> oldList) where T : HeroListObjectField<G>
        {
            if (newList != null && oldList != null && oldList.Count != 0 && newList.Count != 0)
            {
                for (int i = 0; i < newList.Count; i++)
                {
                    if (i < oldList.Count)
                    {
                        newList[i].value = oldList[i].value;
                    }
                }
            }
            return newList;
        }

        /// <summary>
        /// This builds the fields in the inspector (not the editor).
        /// </summary>
        /// <param name="heroObject">The hero object.</param>
        public static void BuildInspectorFields(HeroObject heroObject)
        {            
            // reset hero kit objects in hero objects list
            if (heroObject.lists.heroObjects.items != null && heroObject.lists.heroObjects.items.Count != 0)
            {
                for (int i = 0; i < heroObject.lists.heroObjects.items.Count; i++)
                {
                    heroObject.lists.heroObjects.items[i].heroKitGameObjects = new List<HeroKitObject>();
                }
            }

            // referesh nesting actions
            if (heroObject.states.states != null && heroObject.states.states.Count != 0)
            {
                for (int stateID = 0; stateID < heroObject.states.states.Count; stateID++)
                {
                    if (heroObject.states.states[stateID].heroEvent == null) return;
                    for (int eventID = 0; eventID < heroObject.states.states[stateID].heroEvent.Count; eventID++)
                    {
                        if (heroObject.states.states[stateID].heroEvent[eventID].actions == null) return;
                        for (int actionID = 0; actionID < heroObject.states.states[stateID].heroEvent[eventID].actions.Count; actionID++)
                        {
                            HeroAction heroAction = heroObject.states.states[stateID].heroEvent[eventID].actions[actionID];
                            
                            // reset game object field to default.
                            heroAction.gameObjectFields = new List<int>();
                        }
                    }
                }
            }
        }

        ///// <summary>
        ///// Build Property Fields for a hero object.
        ///// </summary>
        ///// <param name="heroObject">The hero object.</param>
        //public static void BuildPropertyFieldsOld(HeroObject heroObject)
        //{
        //    // refresh properties
        //    if (heroObject.properties.propertyTemplate)
        //    {
        //        // copy the old and new values
        //        HeroList oldList = heroObject.properties.itemProperties.Clone(heroObject.properties.itemProperties);
        //        HeroList newList = heroObject.properties.propertyTemplate.properties.Clone(heroObject.properties.propertyTemplate.properties);

        //        // attach new values to hero object
        //        heroObject.properties.itemProperties = newList;

        //        // replace new values with old values if they exist
        //        heroObject.properties.itemProperties.strings.items = PasteValues<StringField, string>(heroObject.properties.itemProperties.strings.items, oldList.strings.items);
        //        heroObject.properties.itemProperties.ints.items = PasteValues<IntField, int>(heroObject.properties.itemProperties.ints.items, oldList.ints.items);
        //        heroObject.properties.itemProperties.floats.items = PasteValues<FloatField, float>(heroObject.properties.itemProperties.floats.items, oldList.floats.items);
        //        heroObject.properties.itemProperties.bools.items = PasteValues<BoolField, bool>(heroObject.properties.itemProperties.bools.items, oldList.bools.items);
        //        heroObject.properties.itemProperties.heroObjects.items = PasteValues<HeroObjectField, HeroObject>(heroObject.properties.itemProperties.heroObjects.items, oldList.heroObjects.items);
        //        heroObject.properties.itemProperties.unityObjects.items = PasteValues<UnityObjectField, UnityEngine.Object>(heroObject.properties.itemProperties.unityObjects.items, oldList.unityObjects.items);
        //    }
        //    else
        //    {
        //        heroObject.properties.itemProperties = new HeroList();
        //    }
        //}

        /// <summary>
        /// Build All Property List Fields for a hero object.
        /// </summary>
        /// <param name="heroObject">The hero object.</param>
        public static void BuildAllPropertyFields(HeroObject heroObject)
        {
            for (int i = 0; i < heroObject.propertiesList.properties.Count; i++)
            {
                BuildPropertyFields(heroObject, i);
            }
        }

        /// <summary>
        /// Build Property List Fields for a hero object.
        /// </summary>
        /// <param name="heroObject">The hero object.</param>
        /// <param name="id">The ID assigned to the property in the property list.</param>
        public static void BuildPropertyFields(HeroObject heroObject, int id)
        {
            // refresh properties
            if (heroObject.propertiesList.properties[id].propertyTemplate)
            {
                // copy the old and new values
                HeroList oldList = heroObject.propertiesList.properties[id].itemProperties.Clone(heroObject.propertiesList.properties[id].itemProperties);
                HeroList newList = heroObject.propertiesList.properties[id].propertyTemplate.properties.Clone(heroObject.propertiesList.properties[id].propertyTemplate.properties);

                // attach new values to hero object
                heroObject.propertiesList.properties[id].itemProperties = newList;

                // replace new values with old values if they exist
                heroObject.propertiesList.properties[id].itemProperties.strings.items = PasteValues<StringField, string>(heroObject.propertiesList.properties[id].itemProperties.strings.items, oldList.strings.items);
                heroObject.propertiesList.properties[id].itemProperties.ints.items = PasteValues<IntField, int>(heroObject.propertiesList.properties[id].itemProperties.ints.items, oldList.ints.items);
                heroObject.propertiesList.properties[id].itemProperties.floats.items = PasteValues<FloatField, float>(heroObject.propertiesList.properties[id].itemProperties.floats.items, oldList.floats.items);
                heroObject.propertiesList.properties[id].itemProperties.bools.items = PasteValues<BoolField, bool>(heroObject.propertiesList.properties[id].itemProperties.bools.items, oldList.bools.items);
                heroObject.propertiesList.properties[id].itemProperties.heroObjects.items = PasteValues<HeroObjectField, HeroObject>(heroObject.propertiesList.properties[id].itemProperties.heroObjects.items, oldList.heroObjects.items);
                heroObject.propertiesList.properties[id].itemProperties.unityObjects.items = PasteValues<UnityObjectField, UnityEngine.Object>(heroObject.propertiesList.properties[id].itemProperties.unityObjects.items, oldList.unityObjects.items);
            }
            else
            {
                heroObject.propertiesList.properties[id].itemProperties = new HeroList();
            }
        }

        // --------------------------------------------------------------
        // Hero List
        // --------------------------------------------------------------

        /// <summary>
        /// Draw tools for a hero list in the hero kit editor.
        /// </summary>
        /// <param name="showTools">Should we show the tools menu?</param>
        /// <param name="rangeBottom">Smallest number in a range.</param>
        /// <param name="rangeTop">Largest number in a range.</param>
        /// <param name="blockName">Name of the block.</param>
        /// <param name="copyItemRange">Action delegate. Copy the items in the range.</param>
        /// <param name="pasteItem">Action delegate. Paste the copied items at the end of the list.</param>
        /// <param name="deleteItemRange">Action delegat3e. Delete the items in the range.</param>
        public static void DrawTools(ref bool showTools, ref int rangeBottom, ref int rangeTop, string blockName, UnityAction<int, int> copyItemRange, UnityAction pasteItem, UnityAction<int, int> deleteItemRange)
        {
            // exit early if tools are hidden
            if (!showTools)
                return;

            // Range fields (to, from, copy, paste, delete)
            SimpleLayout.BeginVertical(Box.StyleA);
            SimpleLayout.BeginHorizontal();
            rangeBottom = SimpleLayout.IntField("range", 40, rangeBottom, 40);
            rangeTop = SimpleLayout.IntField("to", 20, rangeTop, 40);
            SimpleLayout.Space();
            SimpleLayout.Button("Copy " + blockName + "s", copyItemRange, rangeBottom, rangeTop, Button.StyleA, 100);
            SimpleLayout.Button("Paste " + blockName + "s", pasteItem, Button.StyleA, 100);
            SimpleLayout.Button("Delete " + blockName + "s", deleteItemRange, rangeBottom, rangeTop, Button.StyleA, 100);
            SimpleLayout.EndHorizontal();
            SimpleLayout.EndVertical();

            // show line
            SimpleLayout.Line();
        }

        /// <summary>
        /// Draw no items in list.
        /// </summary>
        /// <param name="blockName">The name of the block.</param>
        public static void DrawNoItemsInList(string blockName)
        {
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Space(10);
            SimpleLayout.Label("No " + blockName + "s");
            SimpleLayout.EndHorizontal();
        }

        /// <summary>
        /// Draw up & down arrows for a hero list.
        /// </summary>
        /// <param name="moveItemUp">Action delegate. Move an item up in the list.</param>
        /// <param name="moveItemDown">Action delegate. Move an item down in the list.</param>
        /// <param name="i">The ID assigned to the item in the list.</param>
        public static void DrawListArrows(UnityAction<int> moveItemUp, UnityAction<int> moveItemDown, int i)
        {
            SimpleLayout.Space(0);
            SimpleLayout.Button(Content.MoveUpIcon, moveItemUp, i, Button.StyleA, 25);
            SimpleLayout.Button(Content.MoveDownIcon, moveItemDown, i, Button.StyleA, 25);
            SimpleLayout.Label("Slot " + i + ":", 60, Label.StyleListField);
        }

        /// <summary>
        /// Draw add, copy, paste, delete buttons for a hero list.
        /// </summary>
        /// <param name="addItem">Action delegate. Add an item to the list.</param>
        /// <param name="copyItem">Action delegate. Copy an item in the list.</param>
        /// <param name="pasteItem">Action delegate. Paste an item in the list.</param>
        /// <param name="deleteItem">Action delegate. Delete an item in the list.</param>
        /// <param name="i">The ID assigned to the item in the list.</param>
        public static void DrawListButtons(UnityAction<int> addItem, UnityAction<int> copyItem, UnityAction<int> pasteItem, UnityAction<int> deleteItem, int i)
        {
            SimpleLayout.Space();
            SimpleLayout.Button(Content.AddIcon, addItem, i + 1, Button.StyleA, 25);
            SimpleLayout.Button(Content.CopyIcon, copyItem, i, Button.StyleA, 25);
            SimpleLayout.Button(Content.PasteIcon, pasteItem, i + 1, Button.StyleA, 25);
            SimpleLayout.Space(10);
            SimpleLayout.Button(Content.DeleteIcon, deleteItem, i, Button.StyleA, 25);
            SimpleLayout.Space(0);
        }

        /// <summary>
        /// Toggle the tools menu for the hero list on and off.
        /// </summary>
        /// <param name="showTools">Show the tools menu?</param>
        /// <returns>Is the tools menu showing?</returns>
        public static bool toggleItemTools(bool showTools)
        {
            deselectField();
            toggleBool(ref showTools);
            return showTools;
        }

        /// <summary>
        /// Button to add a new item to a hero list.
        /// </summary>
        /// <param name="name">Name of the item.</param>
        /// <param name="addItem">Action delegate. Add an item to a hero list.</param>
        /// <param name="style">The style of the button.</param>
        /// <param name="indent">Indent the button.</param>
        public static void DrawAddNewItem(string name, UnityAction addItem, GUIStyle style, int indent = 0)
        {
            SimpleLayout.Space(3);
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Space(indent);
            SimpleLayout.Button("[+" + name + "]", addItem, style);
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
            SimpleLayout.Space(3);
        }

        /// <summary>
        /// Buttons to add and delete item in a hero list.
        /// </summary>
        /// <param name="name">Name of the item.</param>
        /// <param name="addItem">Action delegate. Add an item to a hero list.</param>
        /// <param name="style">The style of the button.</param>
        /// <param name="indent">Indent the button.</param>
        public static void DrawMenuItems(string name, UnityAction deleteItem, UnityAction addItem)
        {
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Space(0);
            SimpleLayout.Button("[+]", addItem, Button.StyleDefault);
            SimpleLayout.Space(5);
            SimpleLayout.Button("[–]", deleteItem, Button.StyleDefault);
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
            SimpleLayout.Space(15);
        }

        /// <summary>
        /// Move an item up in a hero list.
        /// </summary>
        /// <typeparam name="T">Type of item.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="index">The ID of the item to move.</param>
        /// <returns>The updated list.</returns>
        public static List<T> moveItemUp<T>(List<T> items, int index)
        {
            deselectField();
            int indexA = index - 1;
            int indexB = index;

            if (indexA >= 0)
            {
                T fieldA = items[indexA];
                T fieldB = items[indexB];
                items[indexA] = fieldB;
                items[indexB] = fieldA;
            }

            return items;
        }
        /// <summary>
        /// Move an item down in a hero list.
        /// </summary>
        /// <typeparam name="T">Type of item.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="index">The ID of the item to move.</param>
        /// <returns>The updated list.</returns>
        public static List<T> moveItemDown<T>(List<T> items, int index)
        {
            deselectField();
            int indexA = index;
            int indexB = index + 1;

            if (indexB < items.Count)
            {
                T fieldA = items[indexA];
                T fieldB = items[indexB];
                items[indexA] = fieldB;
                items[indexB] = fieldA;
            }

            return items;
        }

        /// <summary>
        /// Add an item to the end of a hero list.
        /// </summary>
        /// <typeparam name="T">Type of item.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="entry">The item to add.</param>
        public static void addItem<T>(List<T> items, T entry)
        {
            deselectField();
            items.Add(entry);
        }
        /// <summary>
        /// Add an item to a specific position in a hero list.
        /// </summary>
        /// <typeparam name="T">Type of item.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="entry">The item to add.</param>
        /// <param name="index">Index where the item needs to be added.</param>
        public static void addItem<T>(List<T> items, T entry, int index)
        {
            deselectField();
            items.Insert(index, entry);
        }

        /// <summary>
        /// Copy an item in a hero list.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="entry">The item.</param>
        /// <returns></returns>
        public static List<T> copyItem<T>(T entry)
        {
            List<T> savedItems = new List<T> { entry };
            return savedItems;
        }
        /// <summary>
        /// Copy a range of items in a hero list.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="items">The list of items.</param>
        /// <param name="start">The first item to copy.</param>
        /// <param name="stop">The last item to copy.</param>
        /// <returns>The updated list.</returns>
        public static List<T> copyItemRange<T>(List<T> items, int start, int stop) where T : IHeroListField<T>
        {
            int count = stop - start + 1;
            List<T> itemsToCopy = items.GetRange(start, count);
            return new List<T>(itemsToCopy.Select(x => x.Clone(x)));
        }

        /// <summary>
        /// Insert copied items to the end of a hero list.
        /// </summary>
        /// <typeparam name="T">Type of item.</typeparam>
        /// <param name="savedFieldList">The items to paste.</param>
        /// <param name="items">The items in the hero list.</param>
        /// <returns>The updated list.</returns>
        public static List<T> pasteItem<T>(List<T> savedFieldList, List<T> items) where T : IHeroListField<T>
        {
            // paste at end of list
            if (savedFieldList != null)
            {
                deselectField();
                List<T> itemsToPaste = new List<T>(savedFieldList.Select(x => x.Clone(x)));
                items.AddRange(itemsToPaste);
            }
            return items;
        }
        /// <summary>
        /// Insert copied items at a specific position in the list.
        /// </summary>
        /// <typeparam name="T">Type of item.</typeparam>
        /// <param name="savedFieldList">The items to paste.</param>
        /// <param name="items">The items in the hero list.</param>
        /// <param name="index">The position in the hero list where the items should be placed.</param>
        /// <returns>The updated list.</returns>
        public static List<T> pasteItem<T>(List<T> savedFieldList, List<T> items, int index) where T : IHeroListField<T>
        {
            // paste at specific location in list
            if (savedFieldList != null)
            {
                deselectField();
                List<T> itemsToPaste = new List<T>(savedFieldList.Select(x => x.Clone(x)));
                items.InsertRange(index, itemsToPaste);
            }
            return items;
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="deletedFields">A list of items that have been deleted.</param>
        /// <param name="deletedFieldsIndex">A list of indexes for the items. An index represents the position in the hero list where an item was deleted.</param>
        /// <param name="items">The list of items.</param>
        /// <param name="index">The index in the hero list where the item should be deleted.</param>
        /// <param name="blockName">The name of the block where the item exists.</param>
        /// <returns>The updated list.</returns>
        public static List<T> deleteItem<T>(ref LinkedList<T> deletedFields, ref LinkedList<int> deletedFieldsIndex, List<T> items, int index, string blockName) where T : IHeroListField<T>
        {
            if (items.Count > index)
            {
                deselectField();
                saveDeletedItem(ref deletedFields, ref deletedFieldsIndex, ref items, index);
                items.RemoveAt(index);
            }
            else
            {
                Debug.LogWarning("Delete " + blockName + "s" + ": Item at index [" + index + "] does not exist");
            }

            return items;
        }
        /// <summary>
        /// Delete a range of items.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="deletedFields">A list of items that have been deleted.</param>
        /// <param name="deletedFieldsIndex">A list of indexes for the items. An index represents the position in the hero list where an item was deleted.</param>
        /// <param name="items">The list of items.</param>
        /// <param name="start">The first item to copy.</param>
        /// <param name="stop">The last item to copy.</param>
        /// <param name="blockName">The name of the block where the item exists.</param>
        /// <returns>The updated list.</returns>
        public static List<T> deleteItemRange<T>(ref LinkedList<T> deletedFields, ref LinkedList<int> deletedFieldsIndex, List<T> items, int start, int stop, string blockName) where T : IHeroListField<T>
        {
            for (int i = start; i <= stop; i++)
            {
                deleteItem(ref deletedFields, ref deletedFieldsIndex, items, start, blockName);
            }
            return items;
        }
        /// <summary>
        /// Store a deleted item for future restoration.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="deletedFields">A list of items that have been deleted.</param>
        /// <param name="deletedFieldsIndex">A list of indexes for the items. An index represents the position in the hero list where an item was deleted.</param>
        /// <param name="items">The list of items.</param>
        /// <param name="index">The index in the hero list where the item should be deleted.</param>
        private static void saveDeletedItem<T>(ref LinkedList<T> deletedFields, ref LinkedList<int> deletedFieldsIndex, ref List<T> items, int index) where T : IHeroListField<T>
        {
            // add to deleted item to front of the stack
            T t = (T)Activator.CreateInstance(typeof(T), new object[] { });
            t = t.Clone(items[index]);

            deletedFields.AddFirst(t);
            deletedFieldsIndex.AddFirst(index);

            // if there are too many items in the stack, pop the last item in the stack
            if (deletedFields.Count > 10)
            {
                deletedFields.RemoveLast();
                deletedFieldsIndex.RemoveLast();
            }
        }
        /// <summary>
        /// Resore an item that was deleted.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="deletedFields">A list of items that have been deleted.</param>
        /// <param name="deletedFieldsIndex">A list of indexes for the items. An index represents the position in the hero list where an item was deleted.</param>
        /// <param name="items">The list of items.</param>
        /// <returns>The updated list.</returns>
        public static List<T> restoreItem<T>(ref LinkedList<T> deletedFields, ref LinkedList<int> deletedFieldsIndex, List<T> items) where T : IHeroListField<T>
        {
            if (deletedFieldsIndex.Count > 0)
            {
                deselectField();
                int index = deletedFieldsIndex.First();
                T field = deletedFields.First();
                items.Insert(index, field);

                // delete fields
                deletedFields.RemoveFirst();
                deletedFieldsIndex.RemoveFirst();
            }

            return items;
        }

        // --------------------------------------------------------------
        // Main Menu
        // --------------------------------------------------------------

        /// <summary>
        /// When user clicks on item in main menu, this resets values that determine what is shown in the canvas.
        /// </summary>
        public static void ResetCanvasContent()
        {
            deselectField();

            HeroObjectMenuBlock.typeID = -1;

            HeroObjectMenuBlock.propertyFocus = false;
            HeroObjectMenuBlock.propertyID = -1;

            HeroObjectMenuBlock.variableFocus = false;
            HeroObjectMenuBlock.variableID = -1;

            HeroObjectMenuBlock.globalFocus = false;
            HeroObjectMenuBlock.globalID = -1;

            HeroObjectMenuBlock.settingsFocus = false;

            HeroObjectMenuBlock.stateFocus = false;
            HeroObjectMenuBlock.stateID = -1;
            HeroObjectMenuBlock.eventID = -1;
            HeroObjectMenuBlock.actionID = -1;
        }

        /// <summary>
        /// Add the scene to the editor build settings if it doesn't already exist there.
        /// </summary>
        /// <param name="sceneAsset">The scene to add.</param>
        public static int AddSceneToBuildSettings(SceneAsset sceneAsset)
        {
            // get the scene path
            string path = AssetDatabase.GetAssetPath(sceneAsset);

            // add scene to build settings
            int sceneID = AddSceneToBuildSettings(path);

            return sceneID;
        }
        /// <summary>
        /// Add the scene to the editor build settings if it doesn't already exist there.
        /// </summary>
        /// <param name="path">The path for the scene to add.</param>
        public static int AddSceneToBuildSettings(string path)
        {
            // check to see if the scene exists in the editor build settings. if it does, exit early
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                if (EditorBuildSettings.scenes[i].path == path)
                    return i;
            }

            // create a new array with an extra slot for the new scene
            EditorBuildSettingsScene[] scenesInEditorA = EditorBuildSettings.scenes;
            EditorBuildSettingsScene[] scenesInEditorB = new EditorBuildSettingsScene[scenesInEditorA.Length + 1];
            Array.Copy(scenesInEditorA, scenesInEditorB, scenesInEditorA.Length);

            // get the scene (as an editor build scene)
            EditorBuildSettingsScene newScene = new EditorBuildSettingsScene(path, true);

            // add the scene to the new array of scenes
            scenesInEditorB[scenesInEditorB.Length - 1] = newScene;

            // assign the updated scene array to the editor build settings
            EditorBuildSettings.scenes = scenesInEditorB;

            return scenesInEditorB.Length - 1;
        }
        /// <summary>
        /// Add all scenes to the editor build settings if they don't already exist there.
        /// </summary>
        public static void AddScenesToBuildSettings()
        {
            // get all scenes in the project
            List<string> scenePaths = new List<string>();
            string[] paths = AssetDatabase.GetAllAssetPaths();
            for (int i = 0; i < paths.Length; i++)
            {
                if (paths[i].EndsWith(".unity"))
                {
                    scenePaths.Add(paths[i]);
                }
            }

            // add all scenes to build settings
            for (int i = 0; i < scenePaths.Count; i++)
            {
                AddSceneToBuildSettings(scenePaths[i]);
            }
        }

        // --------------------------------------------------------------
        // Asset Bundles
        // --------------------------------------------------------------

        /// <summary>
        /// Assign all hero objects in the project to the asset bundle for hero objects.
        /// </summary>
        public static void AddHeroObjectsToBundle()
        {
            string[] guids = AssetDatabase.FindAssets("t:HeroObject");
            for (int i = 0; i < guids.Length; i++)
            {
                // get path of asset from its guid
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);

                // assign asset to asset bundle
                AddHeroObjectToBundle(path);
            }

            //BuildPipeline();
        }

        /// <summary>
        /// Assign one hero object in the project to the asset bundle for hero objects.
        /// </summary>
        public static void AddHeroObjectToBundle(string path)
        {
            string assetBundleName = "herokit/hero objects";
            AssetImporter.GetAtPath(path).assetBundleName = assetBundleName;
        }

        // --------------------------------------------------------------
        // Hero Kit Project Structure
        // --------------------------------------------------------------

        static string myGameDir = Application.productName + " (HeroKit)";
        static string myGameDirLong = "Assets" + "/" + myGameDir;

        static string assetsDir = "Assets";
        static string assetsDirLong = myGameDirLong + "/" + assetsDir;

        static string gameAssetDir = "Game Assets";
        static string gameAssetDirLong = assetsDirLong + "/" + gameAssetDir;

        static string locDir = "Localizations";
        static string locDirLong = gameAssetDirLong + "/" + locDir;

        static string locResourceDir = "Resources";
        static string locResourceDirLong = locDirLong + "/" + locResourceDir;

        static string locAudioDir = "Audio";
        static string locAudioDirLong = locResourceDirLong + "/" + locAudioDir;

        static string engDir = "English";
        static string engDirLong = locAudioDirLong + "/" + engDir;

        static string heroObjectDir = "Hero Objects";
        static string heroObjectDirLong = assetsDirLong + "/" + heroObjectDir;

        static string heroObjectResourceDir = "Resources";
        static string heroObjectResourceDirLong = heroObjectDirLong + "/" + heroObjectResourceDir;

        static string heroPropertyDir = "Hero Properties";
        static string heroPropertyDirLong = assetsDirLong + "/" + heroPropertyDir;

        static string heroActionDir = "Hero Actions";
        static string heroActionDirLong = assetsDirLong + "/" + heroActionDir;

        static string sceneDir = "Scenes";
        static string sceneDirLong = myGameDirLong + "/" + sceneDir;

        /// <summary>
        /// Create folder structure for a HeroKit project in the Project Tab.
        /// </summary>
        public static void CreateHeroKitProjectFolders()
        {
            // Assets > [Name of Game]
            if (!AssetDatabase.IsValidFolder(myGameDirLong))
                MakeFolder("Assets", myGameDir);

            // Assets > [Name of Game] > Scenes
            if (!AssetDatabase.IsValidFolder(sceneDirLong))
                MakeFolder(myGameDirLong, sceneDir);

            // Assets > [Name of Game] > Assets
            if (!AssetDatabase.IsValidFolder(assetsDirLong))
                MakeFolder(myGameDirLong, assetsDir);

            // Assets > [Name of Game] > Assets > Hero Objects
            if (!AssetDatabase.IsValidFolder(heroObjectDirLong))
                MakeFolder(assetsDirLong, heroObjectDir);

            // Assets > [Name of Game] > Assets > Hero Objets > Resources
            if (!AssetDatabase.IsValidFolder(heroObjectResourceDirLong))
                MakeFolder(heroObjectDirLong, heroObjectResourceDir);

            // Assets > [Name of Game] > Assets > Hero Properties
            if (!AssetDatabase.IsValidFolder(heroPropertyDirLong))
                MakeFolder(assetsDirLong, heroPropertyDir);

            // Assets > [Name of Game] > Assets > Hero Actions
            if (!AssetDatabase.IsValidFolder(heroActionDirLong))
                MakeFolder(assetsDirLong, heroActionDir);

            // Assets > [Name of Game] > Assets > Game Assets
            if (!AssetDatabase.IsValidFolder(gameAssetDirLong))
                MakeFolder(assetsDirLong, gameAssetDir);

            // Assets > [Name of Game] > Assets > Game Assets > Localizations
            if (!AssetDatabase.IsValidFolder(locDirLong))
                MakeFolder(gameAssetDirLong, locDir);

            // Assets > [Name of Game] > Assets > Game Assets > Localizations > Resources
            if (!AssetDatabase.IsValidFolder(locResourceDirLong))
                MakeFolder(locDirLong, locResourceDir);

            // Assets > [Name of Game] > Assets > Game Assets > Localizations > Resources > Audio
            if (!AssetDatabase.IsValidFolder(locAudioDirLong))
                MakeFolder(locResourceDirLong, locAudioDir);

            // Assets > [Name of Game] > Assets > Game Assets > Localizations > Resources > Audio > English
            if (!AssetDatabase.IsValidFolder(engDirLong))
                MakeFolder(locAudioDirLong, engDir);
        }

        /// <summary>
        /// Create and add hero object to the project folder.
        /// </summary>
        /// <param name="focusOnObject">After hero object is created, should it be selected in Project Tab?</param>
        /// <returns>The hero object.</returns>
        public static HeroObject AddHeroObjectToFolder(bool focusOnObject)
        {
            HeroObject heroObject = null;

            // build folder structure if it does not exist
            if (!AssetDatabase.IsValidFolder("Assets" + "/" + myGameDir))
                MakeFolder("Assets", myGameDir);
            if (!AssetDatabase.IsValidFolder(myGameDirLong + "/" + assetsDir))
                MakeFolder(myGameDirLong, assetsDir);
            if (!AssetDatabase.IsValidFolder(assetsDirLong + "/" + heroObjectDir))
                MakeFolder(assetsDirLong, heroObjectDir);
            if (!AssetDatabase.IsValidFolder(heroObjectDirLong + "/" + heroObjectResourceDir))
                MakeFolder(heroObjectDirLong, heroObjectResourceDir);

            heroObject = CreateHeroObject(focusOnObject, heroObjectResourceDirLong);

            return heroObject;
        }

        /// <summary>
        /// Create and add hero property to the project folder.
        /// </summary>
        /// <param name="focusOnObject">After hero property is created, should it be selected in Project Tab?</param>
        /// <returns>The hero property.</returns>
        public static HeroKitProperty AddHeroPropertyToFolder(bool focusOnObject)
        {
            HeroKitProperty heroProperty = null;

            // build folder structure if it does not exist
            if (!AssetDatabase.IsValidFolder("Assets" + "/" + myGameDir))
                MakeFolder("Assets", myGameDir);
            if (!AssetDatabase.IsValidFolder(myGameDirLong + "/" + assetsDir))
                MakeFolder(myGameDirLong, assetsDir);
            if (!AssetDatabase.IsValidFolder(assetsDirLong + "/" + heroPropertyDir))
                MakeFolder(assetsDirLong, heroPropertyDir);

            heroProperty = CreateCustomAsset.CreateAsset<HeroKitProperty>("Hero Property", focusOnObject, heroPropertyDirLong);

            return heroProperty;
        }

        /// <summary>
        /// Create and add hero action to the project folder.
        /// </summary>
        /// <param name="focusOnObject">After hero action is created, should it be selected in Project Tab?</param>
        /// <returns>The hero action.</returns>
        public static HeroKitAction AddHeroActionToFolder(bool focusOnObject)
        {
            HeroKitAction heroAction = null;

            // build folder structure if it does not exist
            if (!AssetDatabase.IsValidFolder("Assets" + "/" + myGameDir))
                MakeFolder("Assets", myGameDir);
            if (!AssetDatabase.IsValidFolder(myGameDirLong + "/" + assetsDir))
                MakeFolder(myGameDirLong, assetsDir);
            if (!AssetDatabase.IsValidFolder(assetsDirLong + "/" + heroActionDir))
                MakeFolder(assetsDirLong, heroActionDir);

            heroAction = CreateCustomAsset.CreateAsset<HeroKitAction>("Hero Action", focusOnObject, heroActionDirLong);

            return heroAction;
        }

        /// <summary>
        /// Create a folder in the Project Tab.
        /// </summary>
        /// <param name="directory">The directory where this folder should be placed.</param>
        /// <param name="folder">The name of the folder to create.</param>
        /// <returns></returns>
        private static string MakeFolder(string directory, string folder)
        {
            string guid = AssetDatabase.CreateFolder(directory, folder);
            string path = AssetDatabase.GUIDToAssetPath(guid);
            return path;
        }

        /// <summary>
        /// Create hero object in the Project Tab.
        /// </summary>
        /// <param name="focusOnObject">After hero object is created, should it be selected in Project Tab?</param>
        /// <param name="directory">The directory where the hero object should be stored. Don't enter anything if you want to store it in the currently selected directory in the Project Tab.</param>
        /// <returns>The hero object.</returns>
        public static HeroObject CreateHeroObject(bool focusOnObject = true, string directory = "")
        {
            HeroObject heroObject = CreateCustomAsset.CreateAsset<HeroObject>("Hero Object", focusOnObject, directory);
            string path = AssetDatabase.GetAssetPath(heroObject);
            AddHeroObjectToBundle(path);
            return heroObject;
        }

        // --------------------------------------------------------------
        // Hero Kit Globals
        // --------------------------------------------------------------

        /// <summary>
        /// Store hero kit globals here.
        /// </summary>
        private static HeroKitGlobals globals;
        /// <summary>
        /// Get the hero kit global variables.
        /// </summary>
        /// <returns>The hero kit global variables.</returns>
        public static HeroKitGlobals GetGlobals()
        {
            // get the globals
            if (globals == null)
            {
                globals = Resources.Load<HeroKitGlobals>("Hero Globals/HeroKitGlobals");
                if (globals == null)
                {
                    globals = CreateCustomAsset.CreateAsset<HeroKitGlobals>("HeroKitGlobals", false, "Assets/HeroKit/Hero Engine/Assets/Resources/Hero Globals");
                }
            }

            return globals;
        }
        /// <summary>
        /// Save the hero kit global variables.
        /// </summary>
        public static void SaveGlobals()
        {
            if (globals != null)
                EditorUtility.SetDirty(globals);
        }

        //-------------------------------------------
        // Get hero kit session info
        //-------------------------------------------

        /// <summary>
        /// Store session information for the hero kit editor here.
        /// </summary>
        private static HeroKitSession sessionData;
        /// <summary>
        /// Save the current hero kit editor session.
        /// </summary>
        /// <param name="heroObject">The current hero object that is open in the hero kit editor.</param>
        public static void SaveHeroSession(HeroObject heroObject)
        {
            if (heroObject == null)
                return;

            // get the template
            if (sessionData == null)
            {
                GetHeroKitSession();
            }

            // copy value from template into globals
            if (sessionData != null)
            {
                sessionData.heroObject = heroObject;
                sessionData.mainMenuTypeID = HeroObjectMenuBlock.typeID;
                sessionData.mainMenuStateID = HeroObjectMenuBlock.stateID;
                sessionData.mainMenuEventID = HeroObjectMenuBlock.eventID;
                sessionData.mainMenuActionID = HeroObjectMenuBlock.actionID;
                sessionData.mainMenuVariableID = HeroObjectMenuBlock.variableID;
                sessionData.mainMenuGlobalID = HeroObjectMenuBlock.globalID;

                sessionData.mainMenuPropertyFocus = HeroObjectMenuBlock.propertyFocus;
                sessionData.mainMenuStateFocus = HeroObjectMenuBlock.stateFocus;
                sessionData.mainMenuVariableFocus = HeroObjectMenuBlock.variableFocus;
                sessionData.mainMenuGlobalFocus = HeroObjectMenuBlock.globalFocus;
                sessionData.mainMenuSettingsFocus = HeroObjectMenuBlock.settingsFocus;

                EditorUtility.SetDirty(sessionData);
            }
        }
        /// <summary>
        /// Load the hero kit editor session information. 
        /// </summary>
        /// <returns></returns>
        public static HeroObject LoadHeroSessionHeroObject()
        {
            HeroObject heroObject = null;

            // get the template
            if (sessionData == null)
            {
                GetHeroKitSession();
            }

            // get the hero object that is stored
            if (sessionData != null)
            {
                heroObject = sessionData.heroObject;

                HeroObjectMenuBlock.typeID = sessionData.mainMenuTypeID;
                HeroObjectMenuBlock.stateID = sessionData.mainMenuStateID;
                HeroObjectMenuBlock.eventID = sessionData.mainMenuEventID;
                HeroObjectMenuBlock.actionID = sessionData.mainMenuActionID;
                HeroObjectMenuBlock.variableID = sessionData.mainMenuVariableID;
                HeroObjectMenuBlock.globalID = sessionData.mainMenuGlobalID;

                HeroObjectMenuBlock.propertyFocus = sessionData.mainMenuPropertyFocus;
                HeroObjectMenuBlock.stateFocus = sessionData.mainMenuStateFocus;
                HeroObjectMenuBlock.variableFocus = sessionData.mainMenuVariableFocus;
                HeroObjectMenuBlock.globalFocus = sessionData.mainMenuGlobalFocus;
                HeroObjectMenuBlock.settingsFocus = sessionData.mainMenuSettingsFocus;
            }

            // return the hero object
            return heroObject;
        }
        /// <summary>
        /// Get the session info for hero kit
        /// </summary>
        public static void GetHeroKitSession()
        {
            string[] guids = AssetDatabase.FindAssets("t:HeroKitSession");

            if (guids != null && guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                sessionData = (HeroKitSession)AssetDatabase.LoadAssetAtPath(path, typeof(HeroKitSession));
            }

            if (sessionData == null)
            {
                sessionData = CreateCustomAsset.CreateAsset<HeroKitSession>("HeroKitSession", false, "Assets/HeroKit/Hero Engine/Assets/Hero Session");
                Debug.Log("no session data.");
            }
                
        }

        // --------------------------------------------------------------
        // Hero Kit Settings
        // --------------------------------------------------------------

        /// <summary>
        /// Store hero kit settings here.
        /// </summary>
        private static HeroKitSettings settingsData;
        /// <summary>
        /// Reload the hero kit editor settings.
        /// </summary>
        public static void RefreshHeroSettings()
        {
            // get the template
            if (settingsData == null)
            {
                GetHeroKitSettings();
            }

            // if menus missing, add defaults
            if (settingsData != null)
            {
                string settingsPath = "Hero Templates/Menus/";

                if (settingsData.dialogBox == null)
                    settingsData.dialogBox = Resources.Load<GameObject>(settingsPath + "HeroKit Dialog Box A");

                if (settingsData.fadeInOutScreen == null)
                    settingsData.fadeInOutScreen = Resources.Load<GameObject>(settingsPath + "HeroKit Fade Screen In Out");

                if (settingsData.gameoverMenu == null)
                    settingsData.gameoverMenu = Resources.Load<GameObject>(settingsPath + "HeroKit Game Over Menu");

                if (settingsData.inventoryMenu == null)
                    settingsData.inventoryMenu = Resources.Load<GameObject>(settingsPath + "HeroKit Inventory Menu");

                if (settingsData.journalMenu == null)
                    settingsData.journalMenu = Resources.Load<GameObject>(settingsPath + "HeroKit Journal Menu");

                if (settingsData.optionsMenu == null)
                    settingsData.optionsMenu = Resources.Load<GameObject>(settingsPath + "HeroKit Options Menu");

                if (settingsData.saveMenu == null)
                    settingsData.saveMenu = Resources.Load<GameObject>(settingsPath + "HeroKit Save Menu");

                if (settingsData.startMenu == null)
                    settingsData.startMenu = Resources.Load<GameObject>(settingsPath + "HeroKit Start Menu");

                if (settingsData.inventoryItem == null)
                    settingsData.inventoryItem = Resources.Load<HeroKitProperty>(settingsPath + "Inventory Item");

                if (settingsData.inventorySlot == null)
                    settingsData.inventorySlot = Resources.Load<GameObject>(settingsPath + "Inventory Slot");

                if (settingsData.journalItem == null)
                    settingsData.journalItem = Resources.Load<HeroKitProperty>(settingsPath + "Journal Item");

                if (settingsData.journalSlot == null)
                    settingsData.journalSlot = Resources.Load<GameObject>(settingsPath + "Journal Slot");

                if (settingsData.saveSlot == null)
                    settingsData.saveSlot = Resources.Load<GameObject>(settingsPath + "Save Slot");

                EditorUtility.SetDirty(settingsData);
            }
        }
        /// <summary>
        /// Load the hero kit editor settings information. 
        /// </summary>
        /// <returns></returns>
        public static HeroKitSettings LoadHeroSettings()
        {
            // get the template
            if (settingsData == null)
            {
                GetHeroKitSettings();
            }

            // return the hero object
            return settingsData;
        }
        /// <summary>
        /// Get the settings info for hero kit
        /// </summary>
        public static void GetHeroKitSettings()
        {
            string[] guids = AssetDatabase.FindAssets("t:HeroKitSettings"); 

            if (guids != null && guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                settingsData = (HeroKitSettings)AssetDatabase.LoadAssetAtPath(path, typeof(HeroKitSettings));
            }

            if (settingsData == null)
            {
                settingsData = CreateCustomAsset.CreateAsset<HeroKitSettings>("HeroKitSettings", false, "Assets/HeroKit/Hero Engine/Assets/Resources/Hero Settings");
                Debug.Log("no settings data.");
            }

        }
    }
}