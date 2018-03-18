﻿// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace HeroKit.RpgEditor
{
    /// <summary>
    /// Block for Hero Properties that appears in Hero Kit Editor.
    /// </summary>
    internal static class AmmunitionBlock
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
        private static string blockName = "Item";
        /// <summary>
        /// The Hero Property.
        /// </summary>
        private static HeroProperties propertyBlock;
        /// <summary>
        /// The ID of the property.
        /// </summary>
        private static int itemIndex;

        /// <summary>
        /// Attributes Hero Property.
        /// </summary>
        private static HeroProperties attributeBlock;

        private static List<HeroAction> heroActions;
        private static int indent;

        // for context menu
        private static List<HeroAction> items;
        private static List<HeroAction> savedFieldList;
        private static readonly LinkedList<HeroAction> deletedFields;
        private static readonly LinkedList<int> deletedFieldsIndex;

        private static List<StringField> stringFields;
        private static List<IntField> intFields;
        private static List<FloatField> floatFields;
        private static List<BoolField> boolFields;
        private static List<UnityObjectField> uoFields;

        private static List<StringField> stringFields_att;
        private static List<IntField> intFields_att;
        private static List<FloatField> floatFields_att;
        private static List<BoolField> boolFields_att;
        private static List<UnityObjectField> uoFields_att;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        static AmmunitionBlock()
        {
            deletedFields = new LinkedList<HeroAction>();
            deletedFieldsIndex = new LinkedList<int>();
        }

        /// <summary>
        /// Block to display on the canvas.
        /// </summary>
        /// <param name="heroKitAction">Hero kit action to display in the block.</param>
        /// <param name="indexProperty">ID of the property.</param>
        public static void Block(HeroObject heroKitObject, int indexProperty)
        {
            // exit early if object is null
            if (heroKitObject == null) return;

            // exit early if property no longer exists
            if (heroKitObject.propertiesList.properties == null || heroKitObject.propertiesList.properties.Count - 1 < indexProperty) return;

            // assign hero object to this class
            heroObject = heroKitObject;

            // save the id of the property that this event belongs in
            itemIndex = indexProperty;
            propertyBlock = heroObject.propertiesList.properties[itemIndex];
            attributeBlock = HeroKitCommon.ammunitionDatabase_attributes.propertiesList.properties[itemIndex];
            heroActions = heroObject.states.states[itemIndex].heroEvent[0].actions;
            items = heroActions;

            // save the fields
            stringFields = propertyBlock.itemProperties.strings.items;
            intFields = propertyBlock.itemProperties.ints.items;
            floatFields = propertyBlock.itemProperties.floats.items;
            boolFields = propertyBlock.itemProperties.bools.items;
            uoFields = propertyBlock.itemProperties.unityObjects.items;

            stringFields_att = attributeBlock.itemProperties.strings.items;
            intFields_att = attributeBlock.itemProperties.ints.items;
            floatFields_att = attributeBlock.itemProperties.floats.items;
            boolFields_att = attributeBlock.itemProperties.bools.items;
            uoFields_att = attributeBlock.itemProperties.unityObjects.items;

            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            DrawBody();
        }
        /// <summary>
        /// Draw the body.
        /// </summary>
        private static void DrawBody()
        {
            // draw all fields
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            DrawItemFields();
            SimpleLayout.Space(500);
            SimpleLayout.EndVertical();
        }

        /// <summary>
        /// Draw fields for an item
        /// </summary>
        private static void DrawItemFields()
        {
            DrawItemType();
            BasicFields();
            HeroKitCommon.DrawMoneyValue(stringFields_att, intFields_att);
            //HeroKitCommon.DrawMonetaryValue(intFields_att, boolFields_att);
            HeroKitCommon.DrawItemWeight(intFields_att);
            SimpleLayout.Line();
            HeroKitCommon.DrawStatsValue(stringFields_att, intFields_att);
            //HeroKitCommon.DrawStats(intFields_att, boolFields_att);           
            //HeroKitCommon.DrawConditions(intFields_att, boolFields_att);
            HeroKitCommon.DrawConditionsValue(stringFields_att, intFields_att);
            HeroKitCommon.DrawElementValue(stringFields_att, intFields_att, "Attach elements to this item");
            //HeroKitCommon.DrawElements(intFields_att, boolFields_att, "Attach elements to this item", false);
            SimpleLayout.Line();
            DrawActions(heroObject.propertiesList.properties);
        }
        /// <summary>
        /// Draw item type
        /// </summary>
        private static void DrawItemType()
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            DropDownValues itemList = HeroKitCommon.databaseList(HeroKitCommon.ammunitionTypeDatabase);
            SimpleLayout.Label("Ammunition Type" + ":");
            intFields[0].value = SimpleLayout.DropDownList(intFields[0].value, itemList, 0, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));
            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Draw first group of fields (name, desc, icon, price)
        /// </summary>
        private static void BasicFields()
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // name field
            SimpleLayout.Label("Name" + ":");
            stringFields[0].value = SimpleLayout.TextField(stringFields[0].value, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));

            // icon field
            SimpleLayout.Label("Icon" + ":");
            uoFields[0].value = SimpleLayout.ObjectField(uoFields[0].value as Sprite, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));

            // description field
            SimpleLayout.Label("Description" + ":");
            stringFields[1].value = SimpleLayout.TextArea(stringFields[1].value, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450), 50);

            SimpleLayout.EndVertical();
        }

        /// <summary>
        /// Draw Actions
        /// </summary>
        private static void DrawActions(List<HeroProperties> items)
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label("Additional actions to perform when used:");
            SimpleLayout.Space();
            SimpleLayout.Button("[+Action]", addItem, 65);
            SimpleLayout.EndHorizontal();

            SimpleLayout.Line();

            // exit early if there are no items
            if (heroActions != null && heroActions.Count > 0)
            {
                // display items  
                for (int i = 0; i < heroActions.Count; i++)
                {
                    //---------------------------------------------
                    // get the prefix to show before the name of the item
                    //---------------------------------------------
                    string prefix = (heroActions[i].actionTemplate != null) ? heroActions[i].actionTemplate.title : "";

                    //---------------------------------------------
                    // get the name to show for the action
                    //---------------------------------------------
                    string itemName = heroActions[i].name;
                    if (heroActions[i].actionTemplate != null)
                    {
                        itemName = (heroActions[i].name != "") ? heroActions[i].name : heroActions[i].actionTemplate.name;
                    }

                    // dont show item name if prefix found and if item has no name
                    itemName = (prefix != "" && heroActions[i].name == "") ? "" : itemName;

                    // if no item, take note
                    if (itemName == "")
                        itemName = "[none]";

                    //---------------------------------------------
                    // set indent level of this action
                    //---------------------------------------------
                    if (heroActions[i].actionTemplate != null)
                    {
                        // get new indent
                        indent = indent + heroActions[i].actionTemplate.indentThis;

                        // if indent is negative, change it to zero (happens if too many end statements added)
                        if (indent < 0) indent = 0;
                    }
                    heroActions[i].indent = indent;
                    string space = "".PadRight(indent * 5);

                    //---------------------------------------------
                    // set the color of the action title text
                    //---------------------------------------------
                    string hexColor = (SimpleGUICommon.isProSkin) ? "FFFFFF" : "000000";
                    if (heroActions[i].actionTemplate != null)
                    {
                        hexColor = SimpleGUICommon.GetHexFromColor(heroActions[i].actionTemplate.titleColor);

                        // lighten colors for dark skin
                        if (SimpleGUICommon.isProSkin)
                            hexColor = SimpleGUICommon.AlterHexBrightness(hexColor, 150);
                    }

                    //---------------------------------------------
                    // draw this action
                    //---------------------------------------------

                    // get the box to draw around the foldout
                    GUIStyle style = Box.StyleMenu2Selected; // Box.StyleDefault;
                    GUIStyle buttonStyle = Button.StyleFoldoutText;
                    //if (HeroKitMenuBlock.itemFocus && HeroKitMenuBlock.itemID == i)
                    //{
                    //    style = Box.StyleMenuSelected;
                    //    buttonStyle = Button.StyleFoldoutTextB;
                    //}

                    // show foldout
                    SimpleLayout.BeginHorizontal(style);
                    GUIStyle foldoutStyle = (heroActions[i].visible) ? Button.StyleFoldoutOpen : Button.StyleFoldoutClosed;
                    SimpleLayout.Button("", showBlockContent, showContextMenu, i, foldoutStyle, 10);                  
                    SimpleLayout.Button(i + ": " + space + "<color=#" + hexColor + ">" + prefix + itemName + "</color>", showBlockContent, showContextMenu, i, buttonStyle);
                    //SimpleLayout.Button("▲", blah, 20);
                    //SimpleLayout.Button("▼", blah, 20);
                    //SimpleLayout.Button("[+]", blah, 20);
                    //SimpleLayout.Space(5);
                    //SimpleLayout.Button("[–]", blah, 20);
                    SimpleLayout.EndHorizontal();

                    HeroKitAction oldTemplate = heroActions[i].actionTemplate;
                    if (heroActions[i].visible)
                    {
                        SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleC);

                        SimpleLayout.Space(5);
                        SimpleLayout.BeginHorizontal();
                        SimpleLayout.Space(5);

                        SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                        SimpleLayout.Label("Action:");
                        heroActions[i].actionTemplate = SimpleLayout.ObjectField(heroActions[i].actionTemplate, 450);
                        SimpleLayout.EndVertical();

                        SimpleLayout.EndHorizontal();

                        if (heroActions[i].actionTemplate)
                            HeroKit.Editor.ActionBlockBuilder.BuildFields(heroObject, heroActions[i], heroActions[i].actionTemplate, oldTemplate);

                        SimpleLayout.EndVertical();
                    }


                    //---------------------------------------------
                    // set indent level of next action
                    //---------------------------------------------

                    // note if delete called, the last item in list won't exist. check to make sure it is still there.
                    if (heroActions.Count > i && heroActions[i].actionTemplate != null)
                    {
                        // get new indent
                        indent = indent + heroActions[i].actionTemplate.indentNext;

                        // if indent is negative, change it to zero (happens if too many end statements added)
                        if (indent < 0) indent = 0;
                    }

                    // if we are at the end of the action list, reset indent
                    if (i == (heroActions.Count - 1)) indent = 0;
                }
            }
            SimpleLayout.EndVertical();
        }

        private static void showBlockContent(int id)
        {
            bool newVisble = !heroActions[id].visible;

            for (int i = 0; i < heroActions.Count; i++)
                heroActions[i].visible = false;

            heroActions[id].visible = newVisble;
        }
        private static void addAction()
        {
            heroObject.states.states[itemIndex].heroEvent[0].actions.Add(new HeroAction());
        }
        private static void addItem(List<IntField> intFields, int id)
        {
            HeroKitCommon.addItem(intFields, id);
        }
        private static void removeItem(List<IntField> intFields, int id)
        {
            HeroKitCommon.removeItem(intFields, id);
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

            items = heroActions;

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
            HeroKitCommon.addItem(items, new HeroAction());
        }
        /// <summary>
        /// Add item above another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void addItemAbove(object obj)
        {
            int index = (int)obj;
            HeroKitCommon.addItem(items, new HeroAction(), index);
        }
        /// <summary>
        /// Add item below another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void addItemBelow(object obj)
        {
            int index = (int)obj + 1;
            HeroKitCommon.addItem(items, new HeroAction(), index);
        }
        /// <summary>
        /// Copy an item.
        /// </summary>
        /// <param name="obj">The item.</param>
        private static void copyItem(object obj)
        {
            int index = (int)obj;
            savedFieldList = HeroKitCommon.copyAction(items, savedFieldList, index);
        }
        /// <summary>
        /// Insert item above another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void pasteItemAbove(object obj)
        {
            // this is called when item right clicked
            int index = (int)obj;
            HeroKitCommon.pasteAction(items, savedFieldList, index);
        }
        /// <summary>
        /// Insert item below another item in the list.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void pasteItemBelow(object obj)
        {
            // this is called when item right clicked
            int index = (int)obj + 1;
            HeroKitCommon.pasteAction(items, savedFieldList, index);
        }
        /// <summary>
        /// Delete an item and replace it with this item.
        /// </summary>
        /// <param name="obj">The other item.</param>
        public static void pasteItemHere(object obj)
        {
            int index = (int)obj;
            HeroKitCommon.pasteActionHere(items, savedFieldList, index);
        }
        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="object">The item.</param>
        private static void deleteItem(object obj)
        {
            int index = (int)obj;
            HeroKitCommon.deleteAction(items, deletedFields, deletedFieldsIndex, blockName, index);
        }
        /// <summary>
        /// Restore the last item that was deleted from the list.
        /// </summary>
        private static void restoreItem(object obj)
        {
            HeroKitCommon.restoreItem(items, deletedFields, deletedFieldsIndex);
        }



        /// <summary>
        /// Not used
        /// </summary>
        //private static void DrawRecordType()
        //{
        //    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

        //    item type field(specific item or random item)
        //    SimpleLayout.Label("Type of record to create" + ":");
        //    DropDownValues recordValues = new DropDownValues();
        //    recordValues.setValues("", new string[] { "Item", "Compound Item", "Random Compound Item" });
        //    intFields[4].value = SimpleLayout.DropDownList(intFields[4].value, recordValues, 0, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));
        //    recordType = intFields[4].value;

        //    SimpleLayout.EndVertical();
        //}
        //private static void TargetOfItem()
        //{
        //    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

        //    target field
        //    SimpleLayout.Label("You can use this item on" + ":");
        //    DropDownValues targetValues = new DropDownValues();
        //    targetValues.setValues("", new string[] { "No One", "One Enemy", "All Enemies", "One Ally", "All Allies", "One Enemy or Ally", "All Enemies or Allies" });
        //    intFields[1].value = SimpleLayout.DropDownList(intFields[1].value, targetValues, 0, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));
        //    SimpleLayout.EndVertical();
        //}
        //private static void DrawGroupC()
        //{
        //    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

        //    SimpleLayout.Label("When can this item be used?");
        //    SimpleLayout.Line();

        //    use in menu
        //    SimpleLayout.BeginHorizontal();
        //    boolFields[2].value = SimpleLayout.BoolField(boolFields[2].value);
        //    SimpleLayout.Label("use in item menu");
        //    SimpleLayout.EndHorizontal();

        //    use in battle
        //    SimpleLayout.BeginHorizontal();
        //    boolFields[3].value = SimpleLayout.BoolField(boolFields[3].value);
        //    SimpleLayout.Label("use in battle menu");
        //    SimpleLayout.EndHorizontal();

        //    use in battle
        //    SimpleLayout.BeginHorizontal();
        //    boolFields[4].value = SimpleLayout.BoolField(boolFields[4].value);
        //    SimpleLayout.Label("remove this item after use");
        //    SimpleLayout.EndHorizontal();

        //    SimpleLayout.EndVertical();
        //}
        //private static void DrawGroupD()
        //{
        //    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

        //    user animation field
        //    SimpleLayout.Label("Particle effect to use on caster" + ":");
        //    uoFields[1].value = SimpleLayout.ObjectField(uoFields[1].value as ParticleSystem, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));

        //    target animation field
        //    SimpleLayout.Label("Particle effect to use on target" + ":");
        //    uoFields[2].value = SimpleLayout.ObjectField(uoFields[2].value as ParticleSystem, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));

        //    sound effect when used
        //    SimpleLayout.Label("Sound effect to play when used" + ":");
        //    uoFields[3].value = SimpleLayout.ObjectField(uoFields[3].value as AudioClip, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));

        //    SimpleLayout.EndVertical();
        //}
    }
}