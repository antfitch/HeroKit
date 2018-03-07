// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;
using System.Collections.Generic;

namespace HeroKit.Editor
{
    /// <summary>
    /// Block for Hero Object variables (strings) that appear in Hero Kit Editor.
    /// </summary>
    internal static class StringListBlock
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
        private static string blockName = "String";
        /// <summary>
        /// Show the tools menu.
        /// </summary>
        private static bool showTools;
        /// <summary>
        /// List of fields in the string list.
        /// </summary>
        private static List<StringField> items;
        /// <summary>
        /// List of string fields that were copied (and can be pasted somewhere).
        /// </summary>
        private static List<StringField> savedFieldList;
        /// <summary>
        /// List of the most recently deleted string fields.
        /// </summary>
        private static LinkedList<StringField> deletedFields;
        /// <summary>
        /// List of the index values for the most recently deleted string fields.
        /// The index tells us where the string field was in the list before it was deleted.
        /// </summary>
        private static LinkedList<int> deletedFieldsIndex;
        /// <summary>
        /// The smallest number of items that can be copied or deleted in a string list.
        /// </summary>
        private static int rangeBottom;
        /// <summary>
        /// The largest number of items that can be copied or deleted in a string list.
        /// </summary>
        private static int rangeTop;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        static StringListBlock()
        {
            // create deleted field list
            deletedFields = new LinkedList<StringField>();
            deletedFieldsIndex = new LinkedList<int>();
        }
        /// <summary>
        /// Block to display on the canvas. Get list from hero kit object.
        /// </summary>
        /// <param name="heroKitObject">Hero object info to display in the block.</param>
        /// <param name="globals">Display gobal variables instead of local variables?</param>
        public static void Block(HeroObject heroKitObject, bool globals=false)
        {
            // exit early if object is null
            if (heroKitObject == null)
            {
                return;
            }

            // assign hero object to this class
            heroObject = heroKitObject;
            items = (globals) ? HeroKitCommon.GetGlobals().globals.strings.items : heroObject.lists.strings.items;

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
            items = heroKitProperty.properties.strings.items;

            // draw components
            DrawHeader();
            DrawBlock();
        }
        /// <summary>
        /// Draw the header of the block.
        /// </summary>
        private static void DrawHeader()
        {
            HeroKitCommon.DrawBlockTitle("String List");
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

                items[i].name = SimpleLayout.StringListField(items[i].name, HeroKitCommon.GetWidthForField(270, 150));
                items[i].value = SimpleLayout.StringListField(items[i].value, HeroKitCommon.GetWidthForField(270, 150), items[i].useTextField);

                SimpleLayout.BeginHorizontal();
                items[i].useVariables = SimpleLayout.BoolField("Use variables", 80, items[i].useVariables, 20);
                items[i].useTextField = SimpleLayout.BoolField("Use text field", 80, items[i].useTextField, 20);
                SimpleLayout.EndHorizontal();

                SimpleLayout.EndVertical();
                HeroKitCommon.DrawListButtons(addItem, copyItem, pasteItem, deleteItem, i);
                SimpleLayout.EndHorizontal();

                if (i != items.Count-1)
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
            SimpleLayout.Label("You can insert integers, bools, and strings" + "\n" +
                               "into this string with codes." + "\n" +
                               "\n" +
                               "Codes:" + "\n" +
                                "[V-I-0] = Variables, Integers (slot 0)" + "\n" +
                                "[V-B-0] = Variables, Bools (slot 0)" + "\n" +
                                "[V-S-0] = Variables, Strings (slot 0)" + "\n" +
                                "\n" +
                                "[P-0-I-0] = Properties (slot 0), Integers (slot 0)" + "\n" +
                                "[P-0-B-0] = Properties (slot 0), Bools (slot 0)" + "\n" +
                                "[P-0-S-0] = Properties (slot 0), Strings (slot 0)" + "\n" +
                                "\n" +
                                "[G-I-0] = Globals, Integers (slot 0)" + "\n" +
                                "[G-B-0] = Globals, Bools (slot 0)" + "\n" +
                                "[G-S-0] = Globals, Strings (slot 0)"
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
            HeroKitCommon.DrawTools(ref showTools, ref rangeBottom, ref rangeTop, blockName, copyItemRange, pasteItem, deleteItemRange);
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
            HeroKitCommon.addItem(items, new StringField());
        }
        /// <summary>
        /// Add item at specific position in the list.
        /// </summary>
        /// <param name="index">Index in list where item should be added.</param>
        private static void addItem(int index)
        {
            HeroKitCommon.addItem(items, new StringField(), index);
        }

        /// <summary>
        /// Copy an item.
        /// </summary>
        /// <param name="index">Index of item to copy.</param>
        private static void copyItem(int index)
        {
            savedFieldList = HeroKitCommon.copyItem(new StringField(items[index]));
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
            // paste at end of list
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