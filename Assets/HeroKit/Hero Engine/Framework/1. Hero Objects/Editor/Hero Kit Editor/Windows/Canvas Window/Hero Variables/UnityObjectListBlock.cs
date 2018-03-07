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
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor
{
    /// <summary>
    /// Block for Hero Object variables (unity object) that appear in Hero Kit Editor.
    /// </summary>
    internal static class UnityObjectListBlock
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
        private static string blockName = "Unity Object";
        /// <summary>
        /// Show the tools menu.
        /// </summary>
        private static bool showTools;
        /// <summary>
        /// List of fields in the unity object list.
        /// </summary>
        private static List<UnityObjectField> items;
        /// <summary>
        /// List of unity object fields that were copied (and can be pasted somewhere).
        /// </summary>
        private static List<UnityObjectField> savedFieldList;
        /// <summary>
        /// List of the most recently deleted unity object fields.
        /// </summary>
        private static LinkedList<UnityObjectField> deletedFields;
        /// <summary>
        /// List of the index values for the most recently deleted unity object fields.
        /// The index tells us where the unity object field was in the list before it was deleted.
        /// </summary>
        private static LinkedList<int> deletedFieldsIndex;
        /// <summary>
        /// The smallest number of items that can be copied or deleted in a unity object list.
        /// </summary>
        private static int rangeBottom;
        /// <summary>
        /// The largest number of items that can be copied or deleted in a unity object list.
        /// </summary>
        private static int rangeTop;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        static UnityObjectListBlock()
        {
            // create deleted field list
            deletedFields = new LinkedList<UnityObjectField>();
            deletedFieldsIndex = new LinkedList<int>();
        }
        /// <summary>
        /// Block to display on the canvas. Get list from hero kit object.
        /// </summary>
        /// <param name="heroKitObject">Hero object info to display in the block.</param>
        /// <param name="globals">Display gobal variables instead of local variables?</param>
        public static void Block(HeroObject heroKitObject, bool globals = false)
        {
            // exit early if object is null
            if (heroKitObject == null)
            {
                return;
            }

            // assign hero object to this class
            heroObject = heroKitObject;
            items = (globals) ? HeroKitCommon.GetGlobals().globals.unityObjects.items : heroObject.lists.unityObjects.items;

            // draw components
            DrawHeader();
            DrawBlock();
        }
        /// <summary>
        /// Block to display on the canvas. Get list from hero kit property.
        /// </summary>
        /// <param name="heroKitObject">Hero object info to display in the block.</param>
        public static void Block(HeroKitProperty heroKitProperty)
        {
            // exit early if object is null
            if (heroKitProperty == null) return;

            // assign hero object to this class
            items = heroKitProperty.properties.unityObjects.items;

            // draw components
            DrawHeader();
            DrawBlock();
        }
        /// <summary>
        /// Draw the header of the block.
        /// </summary>
        private static void DrawHeader()
        {
            HeroKitCommon.DrawBlockTitle("Unity Object List");
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            HeroKitCommon.DrawMenuBar(blockName, addItem, pasteItem, restoreItem, toggleItemTools, showTools);
            DrawTools();
            DrawItems();
            DrawDescription();
        }
        /// <summary>
        /// Draw the body of the block.
        /// </summary>
        private static void DrawItems()
        {
            // exit early if there are no items
            if (items == null || items.Count == 0)
            {
                HeroKitCommon.DrawNoItemsInList(blockName);
                return;
            }

            SimpleLayout.BeginVertical(Box.StyleCanvasBox);

            // display items (move up, move down, int field, 
            for (int i = 0; i < items.Count; i++)
            {
                SimpleLayout.BeginHorizontal();
                HeroKitCommon.DrawListArrows(moveItemUp, moveItemDown, i);

                SimpleLayout.BeginVertical();
                items[i].objectType = new UnityObjectTypeField().SetValues(items[i].objectType, HeroKitCommon.GetWidthForField(270, 150));
                switch (items[i].objectType)
                {
                    case 1: // audio
                        items[i].value = SimpleLayout.ObjectField(items[i].value as AudioClip, HeroKitCommon.GetWidthForField(270, 150));
                        break;
                    case 2: // sprite
                        items[i].value = SimpleLayout.ObjectField(items[i].value as Sprite, HeroKitCommon.GetWidthForField(270, 150));
                        break;
                    case 3: // scene
                        items[i].value = SimpleLayout.ObjectField(items[i].value as SceneAsset, HeroKitCommon.GetWidthForField(270, 150));
                        if (items[i].value != null)
                        {
                            // add the scene to the editor build settings if it doesn't already exist there.
                            HeroKitCommon.AddSceneToBuildSettings(items[i].value as SceneAsset);
                        }
                        break;
                    case 4: // particle system
                        items[i].value = SimpleLayout.ObjectField(items[i].value as ParticleSystem, HeroKitCommon.GetWidthForField(270, 150));
                        break;
                    case 5: // mono script
                        items[i].value = SimpleLayout.ObjectField(items[i].value as MonoScript, HeroKitCommon.GetWidthForField(270, 150));
                        break;
                }
                items[i].name = SimpleLayout.StringListField(items[i].name, HeroKitCommon.GetWidthForField(270, 150));              
                SimpleLayout.EndVertical();

                HeroKitCommon.DrawListButtons(addItem, copyItem, pasteItem, deleteItem, i);
                SimpleLayout.EndHorizontal();

                if (i != items.Count - 1)
                    SimpleLayout.Line();
            }

            SimpleLayout.EndVertical();

            SimpleLayout.Space(20);
        }
        /// <summary>
        /// Draw a note about how to use the fields in this block.
        /// </summary>
        private static void DrawDescription()
        {
            SimpleLayout.Line();
            SimpleLayout.Label("Using unity objects requires unboxing" + "\n" +
                               "Unboxing is not performant, so use these objects sparingly." + "\n"
                               );
        }

        // --------------------------------------------------------------
        // Methods (Tools)
        // --------------------------------------------------------------

        /// <summary>
        /// Draw the tools menu in the block.
        /// </summary>
        private static void DrawTools()
        {
            HeroKitCommon.DrawTools(ref showTools, ref rangeBottom, ref rangeTop, "Object", copyItemRange, pasteItem, deleteItemRange);
        }
        /// <summary>
        /// Show or hide the tools menu.
        /// </summary>
        private static void toggleItemTools()
        {
            showTools = HeroKitCommon.toggleItemTools(showTools);
        }

        // --------------------------------------------------------------
        // Methods (Other)
        // --------------------------------------------------------------

        /// <summary>
        /// Move item up.
        /// </summary>
        /// <param name="index">Move item to this index in the list.</param>
        private static void moveItemUp(int index)
        {
            items = HeroKitCommon.moveItemUp(items, index);
        }
        /// <summary>
        /// Move item down.
        /// </summary>
        /// <param name="index">Move item to this index in the list.</param>
        private static void moveItemDown(int index)
        {
            items = HeroKitCommon.moveItemDown(items, index);
        }

        /// <summary>
        /// Add item at end of list.
        /// </summary>
        private static void addItem()
        {
            HeroKitCommon.addItem(items, new UnityObjectField());
        }
        /// <summary>
        /// Add item at specific position in the list.
        /// </summary>
        /// <param name="index">Index in list where item should be added.</param>
        private static void addItem(int index)
        {
            HeroKitCommon.addItem(items, new UnityObjectField(), index);
        }

        /// <summary>
        /// Copy an item.
        /// </summary>
        /// <param name="index">Index of item to copy.</param>
        private static void copyItem(int index)
        {
            savedFieldList = HeroKitCommon.copyItem(new UnityObjectField(items[index]));
        }
        /// <summary>
        /// Copy a range of items in the list.
        /// </summary>
        /// <param name="start">Start index.</param>
        /// <param name="stop">Stop index.</param>
        private static void copyItemRange(int start, int stop)
        {
            savedFieldList = HeroKitCommon.copyItemRange(items, start, stop);
        }

        /// <summary>
        /// Insert item(s) at the end of the list.
        /// </summary>
        private static void pasteItem()
        {
            items = HeroKitCommon.pasteItem(savedFieldList, items);
        }
        /// <summary>
        /// Insert item(s) at a specific index in the list.
        /// </summary>
        /// <param name="index">The index where the items should be inserted.</param>
        private static void pasteItem(int index)
        {
            // paste at specific location in list
            items = HeroKitCommon.pasteItem(savedFieldList, items, index);
        }

        /// <summary>
        /// Delete item at a specific index in the list.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        private static void deleteItem(int index)
        {
            items = HeroKitCommon.deleteItem(ref deletedFields, ref deletedFieldsIndex, items, index, blockName);
        }
        /// <summary>
        /// Delete a range of items from the list.
        /// </summary>
        /// <param name="start">Index of the first item in the range.</param>
        /// <param name="stop">Index of the last item in the range.</param>
        private static void deleteItemRange(int start, int stop)
        {
            items = HeroKitCommon.deleteItemRange(ref deletedFields, ref deletedFieldsIndex, items, start, stop, blockName);
        }

        /// <summary>
        /// Restor the last item (or range of items) that were deleted from the list.
        /// </summary>
        private static void restoreItem()
        {
            items = HeroKitCommon.restoreItem(ref deletedFields, ref deletedFieldsIndex, items);
        }
    }
}