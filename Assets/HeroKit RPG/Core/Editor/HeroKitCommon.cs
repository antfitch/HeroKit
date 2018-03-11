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
using System.Collections;
using System.Linq;
using System.Text;

namespace HeroKit.RpgEditor
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
        /// When user clicks on item in main menu, this resets values that determine what is shown in the canvas.
        /// </summary>
        public static void ResetCanvasContent()
        {
            HeroKit.Editor.HeroKitCommon.deselectField();
            HeroKitMenuBlock.settingsFocus = false;
            HeroKitMenuBlock.itemFocus = false;
            HeroKitMenuBlock.itemID = -1;
        }

        //-------------------------------------------
        // Databases
        //-------------------------------------------

        // item database variables
        public static HeroObject itemDatabase;
        public static HeroKitProperty itemProperties; 
        // item type database variables
        public static HeroObject itemTypeDatabase;
        public static HeroKitProperty itemTypeProperties;
        // affix database variables
        public static HeroObject affixDatabase;
        public static HeroKitProperty affixProperties;
        // affix type database variables
        public static HeroObject affixTypeDatabase;
        public static HeroKitProperty affixTypeProperties;
        // stats database variables
        public static HeroObject statsDatabase;
        public static HeroKitProperty statsProperties;
        // meters database variables
        public static HeroObject meterDatabase;
        public static HeroKitProperty meterProperties;
        // money database variables
        public static HeroObject moneyDatabase;
        public static HeroKitProperty moneyProperties;
        // element database variables
        public static HeroObject elementDatabase;
        //public static HeroObject conditionDatabase_elements;
        public static HeroKitProperty elementProperties;
        // condition database variables
        public static HeroObject conditionDatabase;
        public static HeroKitProperty conditionProperties;
        // weapon type database variables
        public static HeroObject weaponTypeDatabase;
        public static HeroKitProperty weaponTypeProperties;
        // weapon database variables
        public static HeroObject weaponDatabase;
        public static HeroKitProperty weaponProperties;
        // armor type database variables
        public static HeroObject armorTypeDatabase;
        public static HeroKitProperty armorTypeProperties;
        // armor database variables
        public static HeroObject armorDatabase;
        public static HeroKitProperty armorProperties;
        // ammunition type database variables
        public static HeroObject ammunitionTypeDatabase;
        public static HeroKitProperty ammunitionTypeProperties;
        // ammunition database variables
        public static HeroObject ammunitionDatabase;
        public static HeroKitProperty ammunitionProperties;
        // ability type database variables
        public static HeroObject abilityTypeDatabase;
        public static HeroKitProperty abilityTypeProperties;
        // ability database variables
        public static HeroObject abilityDatabase;
        public static HeroKitProperty abilityProperties;
        // formula database variables
        public static HeroObject formulaDatabase;
        public static HeroKitProperty formulaProperties;
        // state database variables
        public static HeroObject classDatabase;
        public static HeroKitProperty classProperties;

        // attribute database variables
        public static HeroObject itemDatabase_attributes;
        public static HeroObject affixDatabase_attributes;
        public static HeroObject conditionDatabase_attributes;
        public static HeroObject weaponDatabase_attributes;
        public static HeroObject armorDatabase_attributes;
        public static HeroObject ammunitionDatabase_attributes;
        public static HeroObject abilityDatabase_attributes;
        public static HeroKitProperty attributeProperties;

        public static void LoadHeroKitRpgDatabases()
        {
            string dbPath = "Assets/HeroKit RPG/Core/Assets/Hero Objects";
            string propertiesPath = "Assets/HeroKit RPG/Core/Assets/Hero Properties";

            // items database
            itemDatabase = (itemDatabase == null) ? GetScriptableObject<HeroObject>("ItemDatabase", dbPath) : itemDatabase;
            itemProperties = (itemProperties == null) ? GetScriptableObject<HeroKitProperty>("ItemProperties", propertiesPath) : itemProperties;

            // item type database
            itemTypeDatabase = (itemTypeDatabase == null) ? GetScriptableObject<HeroObject>("ItemTypeDatabase", dbPath) : itemTypeDatabase;
            itemTypeProperties = (itemTypeProperties == null) ? GetScriptableObject<HeroKitProperty>("ItemTypeProperties", propertiesPath) : itemTypeProperties;

            // affixes database
            affixDatabase = (affixDatabase == null) ? GetScriptableObject<HeroObject>("AffixDatabase", dbPath) : affixDatabase;
            affixProperties = (affixProperties == null) ? GetScriptableObject<HeroKitProperty>("AffixProperties", propertiesPath) : affixProperties;

            // affix type database
            affixTypeDatabase = (affixTypeDatabase == null) ? GetScriptableObject<HeroObject>("AffixTypeDatabase", dbPath) : affixTypeDatabase;
            affixTypeProperties = (affixTypeProperties == null) ? GetScriptableObject<HeroKitProperty>("AffixTypeProperties", propertiesPath) : affixTypeProperties;

            // stats database
            statsDatabase = (statsDatabase == null) ? GetScriptableObject<HeroObject>("StatsDatabase", dbPath) : statsDatabase;
            statsProperties = (statsProperties == null) ? GetScriptableObject<HeroKitProperty>("StatsProperties", propertiesPath) : statsProperties;

            // meter database
            meterDatabase = (meterDatabase == null) ? GetScriptableObject<HeroObject>("MeterDatabase", dbPath) : meterDatabase;
            meterProperties = (meterProperties == null) ? GetScriptableObject<HeroKitProperty>("MeterProperties", propertiesPath) : meterProperties;

            // money database
            moneyDatabase = (moneyDatabase == null) ? GetScriptableObject<HeroObject>("MoneyDatabase", dbPath) : moneyDatabase;
            moneyProperties = (moneyProperties == null) ? GetScriptableObject<HeroKitProperty>("MoneyProperties", propertiesPath) : moneyProperties;

            // attribute database
            itemDatabase_attributes = (itemDatabase_attributes == null) ? GetScriptableObject<HeroObject>("ItemDatabase_attributes", dbPath) : itemDatabase_attributes;
            affixDatabase_attributes = (affixDatabase_attributes == null) ? GetScriptableObject<HeroObject>("AffixDatabase_attributes", dbPath) : affixDatabase_attributes;
            conditionDatabase_attributes = (conditionDatabase_attributes == null) ? GetScriptableObject<HeroObject>("ConditionDatabase_attributes", dbPath) : conditionDatabase_attributes;
            weaponDatabase_attributes = (weaponDatabase_attributes == null) ? GetScriptableObject<HeroObject>("WeaponDatabase_attributes", dbPath) : weaponDatabase_attributes;
            armorDatabase_attributes = (armorDatabase_attributes == null) ? GetScriptableObject<HeroObject>("ArmorDatabase_attributes", dbPath) : armorDatabase_attributes;
            ammunitionDatabase_attributes = (ammunitionDatabase_attributes == null) ? GetScriptableObject<HeroObject>("AmmunitionDatabase_attributes", dbPath) : ammunitionDatabase_attributes;
            abilityDatabase_attributes = (abilityDatabase_attributes == null) ? GetScriptableObject<HeroObject>("AbilityDatabase_attributes", dbPath) : abilityDatabase_attributes;
            attributeProperties = (attributeProperties == null) ? GetScriptableObject<HeroKitProperty>("AttributeProperties", propertiesPath) : attributeProperties;

            // element database  
            elementDatabase = (elementDatabase == null) ? GetScriptableObject<HeroObject>("ElementDatabase", dbPath) : elementDatabase;
            elementProperties = (elementProperties == null) ? GetScriptableObject<HeroKitProperty>("ElementProperties", propertiesPath) : elementProperties;

            // condition database
            conditionDatabase = (conditionDatabase == null) ? GetScriptableObject<HeroObject>("ConditionDatabase", dbPath) : conditionDatabase;
            conditionProperties = (conditionProperties == null) ? GetScriptableObject<HeroKitProperty>("ConditionProperties", propertiesPath) : conditionProperties;

            // weapons type database
            weaponTypeDatabase = (weaponTypeDatabase == null) ? GetScriptableObject<HeroObject>("WeaponTypeDatabase", dbPath) : weaponTypeDatabase;
            weaponTypeProperties = (weaponTypeProperties == null) ? GetScriptableObject<HeroKitProperty>("WeaponTypeProperties", propertiesPath) : weaponTypeProperties;

            // weapons database
            weaponDatabase = (weaponDatabase == null) ? GetScriptableObject<HeroObject>("WeaponDatabase", dbPath) : weaponDatabase;
            weaponProperties = (weaponProperties == null) ? GetScriptableObject<HeroKitProperty>("WeaponProperties", propertiesPath) : weaponProperties;

            // armor type database
            armorTypeDatabase = (armorTypeDatabase == null) ? GetScriptableObject<HeroObject>("ArmorTypeDatabase", dbPath) : armorTypeDatabase;
            armorTypeProperties = (armorTypeProperties == null) ? GetScriptableObject<HeroKitProperty>("ArmorTypeProperties", propertiesPath) : armorTypeProperties;
              
            // armor database
            armorDatabase = (armorDatabase == null) ? GetScriptableObject<HeroObject>("ArmorDatabase", dbPath) : armorDatabase;
            armorProperties = (armorProperties == null) ? GetScriptableObject<HeroKitProperty>("ArmorProperties", propertiesPath) : armorProperties;

            // ammunition type database
            ammunitionTypeDatabase = (ammunitionTypeDatabase == null) ? GetScriptableObject<HeroObject>("AmmunitionTypeDatabase", dbPath) : ammunitionTypeDatabase;
            ammunitionTypeProperties = (ammunitionTypeProperties == null) ? GetScriptableObject<HeroKitProperty>("AmmunitionTypeProperties", propertiesPath) : ammunitionTypeProperties;

            // ammunition database
            ammunitionDatabase = (ammunitionDatabase == null) ? GetScriptableObject<HeroObject>("AmmunitionDatabase", dbPath) : ammunitionDatabase;
            ammunitionProperties = (ammunitionProperties == null) ? GetScriptableObject<HeroKitProperty>("AmmunitionProperties", propertiesPath) : ammunitionProperties;

            // ability type database
            abilityTypeDatabase = (abilityTypeDatabase == null) ? GetScriptableObject<HeroObject>("AbilityTypeDatabase", dbPath) : abilityTypeDatabase;
            abilityTypeProperties = (abilityTypeProperties == null) ? GetScriptableObject<HeroKitProperty>("AbilityTypeProperties", propertiesPath) : abilityTypeProperties;

            // ability database
            abilityDatabase = (abilityDatabase == null) ? GetScriptableObject<HeroObject>("AbilityDatabase", dbPath) : abilityDatabase;
            abilityProperties = (abilityProperties == null) ? GetScriptableObject<HeroKitProperty>("AbilityProperties", propertiesPath) : abilityProperties;

            // formula database
            formulaDatabase = (formulaDatabase == null) ? GetScriptableObject<HeroObject>("FormulaDatabase", dbPath) : formulaDatabase;
            formulaProperties = (formulaProperties == null) ? GetScriptableObject<HeroKitProperty>("FormulaProperties", propertiesPath) : formulaProperties;

            // state database
            classDatabase = (classDatabase == null) ? GetScriptableObject<HeroObject>("ClassDatabase", dbPath) : classDatabase;
            classProperties = (classProperties == null) ? GetScriptableObject<HeroKitProperty>("ClassProperties", propertiesPath) : classProperties;

        }
        public static T GetScriptableObject<T>(string name, string path) where T : UnityEngine.ScriptableObject
        {
            T heroItem = null;

            string[] guids = AssetDatabase.FindAssets(name);

            if (guids != null && guids.Length > 0)
            {
                string dir = AssetDatabase.GUIDToAssetPath(guids[0]);
                heroItem = (T)AssetDatabase.LoadAssetAtPath(dir, typeof(T));
            }

            if (heroItem == null)
            {
                heroItem = CreateCustomAsset.CreateAsset<T>(name, false, path);
                Debug.Log("No item called " + name + " found. Creating a new one.");
            }

            return heroItem;
        }

        //-------------------------------------------
        // Item lists
        //-------------------------------------------

        // get a list of all items in a database
        public static DropDownValues databaseList(HeroObject database)
        {
            List<string> items = new List<string>();
            for (int i = 0; i < database.propertiesList.properties.Count; i++)
            {
                items.Add(database.propertiesList.properties[i].itemProperties.strings.items[0].value);
            }
            DropDownValues itemsList = new DropDownValues();
            itemsList.setValues("", items.ToArray());
            return itemsList;
        }
        // get a list of items that are of a specific item type
        public static DropDownValues databaseTypeList(HeroObject database, int itemType, int itemTypeTarget)
        {
            List<string> items = new List<string>();
            List<int> ids = new List<int>();
            items.Add("All");
            ids.Add(-1);
            for (int i = 0; i < database.propertiesList.properties.Count; i++)
            {
                // ony get weapons that match weapon type
                if (database.propertiesList.properties[i].itemProperties.ints.items[itemType].value == itemTypeTarget)
                {
                    items.Add(database.propertiesList.properties[i].itemProperties.strings.items[0].value);
                    ids.Add(i);
                }
            }
            DropDownValues itemList = new DropDownValues();
            itemList.setValues("", items.ToArray(), ids.ToArray());
            return itemList;
        }
        // get a list of all items in a database
        public static DropDownValues conditionSeverityList()
        {
            string[] items = { "1=No defense against this",
                               "2=Very weak against this",
                               "3=Weak against this",
                               "4=Neutral",
                               "5=Strong against this",
                               "6=Very strong against this",
                               "7-Immune to this"
                             };
            DropDownValues itemsList = new DropDownValues();
            itemsList.setValues("", items);
            return itemsList;
        }

        //-------------------------------------------
        // Modify data in a menu
        //-------------------------------------------

        /// <summary>
        /// Add item at end of list.
        /// </summary>      
        public static void addItem<T>(List<T> items, T newItem)
        {
            HeroKit.Editor.HeroKitCommon.deselectField();
            items.Add(newItem);
        }
        public static void addItem<T>(List<T> items, T newItem, int index)
        {
            HeroKit.Editor.HeroKitCommon.deselectField();
            items.Insert(index, newItem);
        }

        /// <summary>
        /// Move item up.
        /// </summary>
        public static void moveItemUp<T>(List<T> items, int index)
        {
            HeroKit.Editor.HeroKitCommon.deselectField();
            int indexA = index - 1;
            int indexB = index;

            if (indexA >= 0)
            {
                T fieldA = items[indexA];
                T fieldB = items[indexB];
                items[indexA] = fieldB;
                items[indexB] = fieldA;
            }
        }
        /// <summary>
        /// Move item down.
        /// </summary>
        /// <param name="obj">Item to move down.</param>
        public static void moveItemDown<T>(List<T> items, int index)
        {
            HeroKit.Editor.HeroKitCommon.deselectField();
            int indexA = index;
            int indexB = index + 1;

            if (indexB < items.Count)
            {
                T fieldA = items[indexA];
                T fieldB = items[indexB];
                items[indexA] = fieldB;
                items[indexB] = fieldA;
            }
        }

        /// <summary>
        /// Copy an item.
        /// </summary>
        /// <param name="obj">The item.</param>
        public static List<HeroProperties> copyItem(List<HeroProperties> items, List<HeroProperties> savedItems, int index)
        {
            savedItems = new List<HeroProperties>();
            savedItems.Add(new HeroProperties(items[index]));
            return savedItems;
        }
        /// <summary>
        /// Insert item at the end of the list.
        /// </summary>
        public static void pasteItem(List<HeroProperties> items, List<HeroProperties> savedItems, int index)
        {
            // paste at specific location in list
            if (savedItems != null)
            {
                HeroKit.Editor.HeroKitCommon.deselectField();

                List<HeroProperties> itemsToPaste = new List<HeroProperties>(savedItems.Select(x => x.Clone(x)));
                items.InsertRange(index, itemsToPaste);
            }
        }
        /// <summary>
        /// Delete an item and replace it with this item.
        /// </summary>
        public static void pasteItemHere(List<HeroProperties> items, List<HeroProperties> savedItems, int index)
        {
            // paste at specific location in list
            if (savedItems != null)
            {
                HeroKit.Editor.HeroKitCommon.deselectField();

                List<HeroProperties> itemsToPaste = new List<HeroProperties>(savedItems.Select(x => x.Clone(x)));
                items.RemoveAt(index);
                items.InsertRange(index, itemsToPaste);
            }
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="object">The item.</param>
        public static void deleteItem(List<HeroProperties> items, LinkedList<HeroProperties> deletedItems, LinkedList<int> deletedIDs, string blockName, int index)
        {
            if (items.Count > index)
            {
                HeroKit.Editor.HeroKitCommon.deselectField();
                saveDeletedItem(items, deletedItems, deletedIDs, index);
                items.RemoveAt(index);
            }
            else
            {
                if (blockName != "")
                    Debug.LogWarning("Delete " + blockName + "s" + ": Item at index [" + index + "] does not exist");
            }
        }
        /// <summary>
        /// Store a deleted item for future restoration.
        /// </summary>
        /// <param name="itemID">ID of the state.</param>
        public static void saveDeletedItem(List<HeroProperties> items, LinkedList<HeroProperties> deletedItems, LinkedList<int> deletedIDs, int itemID)
        {
            // add to deleted item to front of the stack
            HeroProperties itemToSave = new HeroProperties();
            itemToSave = itemToSave.Clone(items[itemID]);

            deletedItems.AddFirst(itemToSave);
            deletedIDs.AddFirst(itemID);

            // if there are too many items in the stack, pop the last item in the stack
            if (deletedItems.Count > 10)
            {
                deletedItems.RemoveLast();
                deletedIDs.RemoveLast();
            }
        }
        /// <summary>
        /// Restor the last item that was deleted from the list.
        /// </summary>
        public static void restoreItem<T>(List<T> items, LinkedList<T> deletedItems, LinkedList<int> deletedIDs)
        {
            if (deletedIDs.Count > 0)
            {
                HeroKit.Editor.HeroKitCommon.deselectField();
                int index = deletedIDs.First();
                T itemField = deletedItems.First();

                // insert this event in the list
                items.Insert(index, itemField);

                // delete fields
                deletedItems.RemoveFirst();
                deletedIDs.RemoveFirst();
            }
        }

        public static HeroProperties getHeroProperties(HeroKitProperty heroKitProperty)
        {
            HeroProperties hp = new HeroProperties();
            hp.propertyTemplate = heroKitProperty;
            hp.itemProperties = heroKitProperty.properties.Clone(heroKitProperty.properties);
            return hp;
        }

        // !!! IOC not available for scriptable objects (if things have changed, merge this)
        /// <summary>
        /// Copy an item.
        /// </summary>
        /// <param name="obj">The item.</param>
        public static List<HeroState> copyState(List<HeroState> items, List<HeroState> savedItems, int index)
        {
            savedItems = new List<HeroState>();
            savedItems.Add(new HeroState(items[index]));
            return savedItems;
        }
        /// <summary>
        /// Insert item at the end of the list.
        /// </summary>
        public static void pasteState(List<HeroState> items, List<HeroState> savedItems, int index)
        {
            // paste at specific location in list
            if (savedItems != null)
            {
                HeroKit.Editor.HeroKitCommon.deselectField();

                List<HeroState> itemsToPaste = new List<HeroState>(savedItems.Select(x => x.Clone(x)));
                items.InsertRange(index, itemsToPaste);
            }
        }
        /// <summary>
        /// Delete an item and replace it with this item.
        /// </summary>
        public static void pasteStateHere(List<HeroState> items, List<HeroState> savedItems, int index)
        {
            // paste at specific location in list
            if (savedItems != null)
            {
                HeroKit.Editor.HeroKitCommon.deselectField();

                List<HeroState> itemsToPaste = new List<HeroState>(savedItems.Select(x => x.Clone(x)));
                items.RemoveAt(index);
                items.InsertRange(index, itemsToPaste);
            }
        }
        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="object">The item.</param>
        public static void deleteState(List<HeroState> items, LinkedList<HeroState> deletedItems, LinkedList<int> deletedIDs, string blockName, int index)
        {
            if (items.Count > index)
            {
                HeroKit.Editor.HeroKitCommon.deselectField();
                saveDeletedState(items, deletedItems, deletedIDs, index);
                items.RemoveAt(index);
            }
            else
            {
                if (blockName != "")
                    Debug.LogWarning("Delete " + blockName + "s" + ": Item at index [" + index + "] does not exist");
            }
        }
        /// <summary>
        /// Store a deleted item for future restoration.
        /// </summary>
        /// <param name="itemID">ID of the state.</param>
        public static void saveDeletedState(List<HeroState> items, LinkedList<HeroState> deletedItems, LinkedList<int> deletedIDs, int itemID)
        {
            // add to deleted item to front of the stack
            HeroState itemToSave = new HeroState();
            itemToSave = itemToSave.Clone(items[itemID]);

            deletedItems.AddFirst(itemToSave);
            deletedIDs.AddFirst(itemID);

            // if there are too many items in the stack, pop the last item in the stack
            if (deletedItems.Count > 10)
            {
                deletedItems.RemoveLast();
                deletedIDs.RemoveLast();
            }
        }

        /// <summary>
        /// Copy an item.
        /// </summary>
        /// <param name="obj">The item.</param>
        public static List<HeroAction> copyAction(List<HeroAction> items, List<HeroAction> savedItems, int index)
        {
            savedItems = new List<HeroAction>();
            savedItems.Add(new HeroAction(items[index]));
            return savedItems;
        }
        /// <summary>
        /// Insert item at the end of the list.
        /// </summary>
        public static void pasteAction(List<HeroAction> items, List<HeroAction> savedItems, int index)
        {
            // paste at specific location in list
            if (savedItems != null)
            {
                HeroKit.Editor.HeroKitCommon.deselectField();

                List<HeroAction> itemsToPaste = new List<HeroAction>(savedItems.Select(x => x.Clone(x)));
                items.InsertRange(index, itemsToPaste);
            }
        }
        /// <summary>
        /// Delete an item and replace it with this item.
        /// </summary>
        public static void pasteActionHere(List<HeroAction> items, List<HeroAction> savedItems, int index)
        {
            // paste at specific location in list
            if (savedItems != null)
            {
                HeroKit.Editor.HeroKitCommon.deselectField();

                List<HeroAction> itemsToPaste = new List<HeroAction>(savedItems.Select(x => x.Clone(x)));
                items.RemoveAt(index);
                items.InsertRange(index, itemsToPaste);
            }
        }
        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="object">The item.</param>
        public static void deleteAction(List<HeroAction> items, LinkedList<HeroAction> deletedItems, LinkedList<int> deletedIDs, string blockName, int index)
        {
            if (items.Count > index)
            {
                HeroKit.Editor.HeroKitCommon.deselectField();
                saveDeletedAction(items, deletedItems, deletedIDs, index);
                items.RemoveAt(index);
            }
            else
            {
                if (blockName != "")
                    Debug.LogWarning("Delete " + blockName + "s" + ": Item at index [" + index + "] does not exist");
            }
        }
        /// <summary>
        /// Store a deleted item for future restoration.
        /// </summary>
        /// <param name="itemID">ID of the state.</param>
        public static void saveDeletedAction(List<HeroAction> items, LinkedList<HeroAction> deletedItems, LinkedList<int> deletedIDs, int itemID)
        {
            // add to deleted item to front of the stack
            HeroAction itemToSave = new HeroAction();
            itemToSave = itemToSave.Clone(items[itemID]);

            deletedItems.AddFirst(itemToSave);
            deletedIDs.AddFirst(itemID);

            // if there are too many items in the stack, pop the last item in the stack
            if (deletedItems.Count > 10)
            {
                deletedItems.RemoveLast();
                deletedIDs.RemoveLast();
            }
        }


        //-------------------------------------------
        // Stats for items and attributes
        //-------------------------------------------

        /// <summary>
        /// Draw when item can be used
        /// </summary>
        public static void DrawMonetaryValue(List<IntField> intFields_att, List<BoolField> boolFields_att, string title="Set monetary value")
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            int countField = 1;
            int itemMaxCount = 10;

            // title bar
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Button("[+]", addItem, intFields_att, countField, Button.StyleDefault, 20);
            SimpleLayout.Button("[–]", removeItem, intFields_att, countField, Button.StyleDefault, 15);
            SimpleLayout.Label(title);
            SimpleLayout.EndHorizontal();

            // list of values
            string[] items = new string[HeroKitCommon.moneyDatabase.propertiesList.properties.Count];
            for (int i = 0; i < HeroKitCommon.moneyDatabase.propertiesList.properties.Count; i++)
            {
                items[i] = HeroKitCommon.moneyDatabase.propertiesList.properties[i].itemProperties.strings.items[0].value;
            }
            DropDownValues moneyList = new DropDownValues();
            moneyList.setValues("", items);

            if (intFields_att[countField].value > 0)
            {
                int s = countField + 1;

                SimpleLayout.Line();
                for (int i = 0; i < intFields_att[countField].value; i++)
                {
                    SimpleLayout.BeginHorizontal();
                    intFields_att[s + itemMaxCount + i].value = SimpleLayout.IntField(intFields_att[s + itemMaxCount + i].value, 100);
                    intFields_att[s + i].value = SimpleLayout.DropDownList(intFields_att[s + i].value, moneyList, 0, 150);
                    SimpleLayout.Space();
                    SimpleLayout.EndHorizontal();
                }
            }

            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Draw when item can be used
        /// </summary>
        public static void DrawMeters(List<IntField> intFields_att, List<BoolField> boolFields_att, string title="Change meters", int startIndex = 53)
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            int countField = startIndex;
            int itemMaxCount = 10;

            // title bar
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Button("[+]", addItem, intFields_att, countField, Button.StyleDefault, 20);
            SimpleLayout.Button("[–]", removeItem, intFields_att, countField, Button.StyleDefault, 15);
            SimpleLayout.Label(title);
            SimpleLayout.EndHorizontal();

            // list of meters
            string[] items = new string[HeroKitCommon.meterDatabase.propertiesList.properties.Count];
            for (int i = 0; i < HeroKitCommon.meterDatabase.propertiesList.properties.Count; i++)
            {
                items[i] = HeroKitCommon.meterDatabase.propertiesList.properties[i].itemProperties.strings.items[0].value;
            }
            DropDownValues meterList = new DropDownValues();
            meterList.setValues("", items);           

            // list of stat change types
            DropDownValues changeList = new DropDownValues();
            string[] changeItems = { "value", "% of value", "max value" };
            changeList.setValues("", changeItems);

            if (intFields_att[countField].value > 0)
            {
                int s = countField + 1;

                SimpleLayout.Line();
                for (int i = 0; i < intFields_att[countField].value; i++)
                {
                    SimpleLayout.BeginHorizontal();
                    intFields_att[s + itemMaxCount + i].value = SimpleLayout.IntField(intFields_att[s + itemMaxCount + i].value, 100);
                    intFields_att[s + i].value = SimpleLayout.DropDownList(intFields_att[s + i].value, meterList, 0, 150);
                    intFields_att[s + itemMaxCount * 2 + i].value = SimpleLayout.DropDownList(intFields_att[s + itemMaxCount * 2 + i].value, changeList, 0, 100);
                    SimpleLayout.Space();
                    SimpleLayout.EndHorizontal();
                }
            }

            SimpleLayout.EndVertical();
        }
        public static void DrawMetersB(List<IntField> intFields_att, List<BoolField> boolFields_att, string title = "Change meters")
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            int countField = 53;
            int itemMaxCount = 10;

            // title bar
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Button("[+]", addItem, intFields_att, countField, Button.StyleDefault, 20);
            SimpleLayout.Button("[–]", removeItem, intFields_att, countField, Button.StyleDefault, 15);
            SimpleLayout.Label(title);
            SimpleLayout.EndHorizontal();

            // list of meters
            string[] items = new string[HeroKitCommon.meterDatabase.propertiesList.properties.Count];
            for (int i = 0; i < HeroKitCommon.meterDatabase.propertiesList.properties.Count; i++)
            {
                items[i] = HeroKitCommon.meterDatabase.propertiesList.properties[i].itemProperties.strings.items[0].value;
            }
            DropDownValues meterList = new DropDownValues();
            meterList.setValues("", items);

            // list of stat change types
            DropDownValues changeList = new DropDownValues();
            string[] changeItems = { "temporary", "permanent" };
            changeList.setValues("", changeItems);

            if (intFields_att[countField].value > 0)
            {
                int s = countField + 1;

                SimpleLayout.Line();
                for (int i = 0; i < intFields_att[countField].value; i++)
                {
                    SimpleLayout.BeginHorizontal();
                    intFields_att[s + i].value = SimpleLayout.DropDownList(intFields_att[s + i].value, meterList, 0, 150);
                    intFields_att[s + itemMaxCount + i].value = SimpleLayout.IntField(intFields_att[s + itemMaxCount + i].value, 100);
                    SimpleLayout.Label("%");
                    SimpleLayout.Space();
                    intFields_att[s + itemMaxCount * 2 + i].value = SimpleLayout.DropDownList(intFields_att[s + itemMaxCount * 2 + i].value, changeList, 0, 100);
                    SimpleLayout.EndHorizontal();
                }
            }

            SimpleLayout.EndVertical();
        }
        public static void DrawMetersC(List<IntField> intFields_att, List<BoolField> boolFields_att, string title = "Change meters", int startIndex = 53)
        {
            // meter & formula

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            int countField = startIndex;
            int itemMaxCount = 10;

            // title bar
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Button("[+]", addItem, intFields_att, countField, Button.StyleDefault, 20);
            SimpleLayout.Button("[–]", removeItem, intFields_att, countField, Button.StyleDefault, 15);
            SimpleLayout.Label(title);
            SimpleLayout.EndHorizontal();

            // list of meters
            string[] items = new string[HeroKitCommon.meterDatabase.propertiesList.properties.Count];
            for (int i = 0; i < HeroKitCommon.meterDatabase.propertiesList.properties.Count; i++)
            {
                items[i] = HeroKitCommon.meterDatabase.propertiesList.properties[i].itemProperties.strings.items[0].value;
            }
            DropDownValues meterList = new DropDownValues();
            meterList.setValues("", items);

            string[] formulas = new string[HeroKitCommon.formulaDatabase.propertiesList.properties.Count];
            for (int i = 0; i < HeroKitCommon.formulaDatabase.propertiesList.properties.Count; i++)
            {
                formulas[i] = HeroKitCommon.formulaDatabase.propertiesList.properties[i].itemProperties.strings.items[0].value;
            }
            DropDownValues formulaList = new DropDownValues();
            formulaList.setValues("", formulas);

            if (intFields_att[countField].value > 0)
            {
                int s = countField + 1;

                SimpleLayout.Line();
                for (int i = 0; i < intFields_att[countField].value; i++)
                {
                    SimpleLayout.BeginHorizontal();
                    intFields_att[s + itemMaxCount + i].value = SimpleLayout.IntField(intFields_att[s + itemMaxCount + i].value, 100);
                    intFields_att[s + i].value = SimpleLayout.DropDownList(intFields_att[s + i].value, meterList, 0, 150);
                    SimpleLayout.Space();
                    SimpleLayout.Label("Formula:");
                    intFields_att[s + itemMaxCount * 2 + i].value = SimpleLayout.DropDownList(intFields_att[s + itemMaxCount * 2 + i].value, formulaList, 0, 150);
                    SimpleLayout.Space();
                    SimpleLayout.EndHorizontal();
                }
            }

            SimpleLayout.EndVertical();
        }

        /// <summary>
        /// Draw when item can be used
        /// </summary>
        public static void DrawStats(List<IntField> intFields_att, List<BoolField> boolFields_att, string title="Change stats")
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            int countField = 22;
            int itemMaxCount = 10;

            // title bar
            SimpleLayout.BeginHorizontal();                      
            SimpleLayout.Button("[+]", addItem, intFields_att, countField, Button.StyleDefault, 20);
            SimpleLayout.Button("[–]", removeItem, intFields_att, countField, Button.StyleDefault, 15);
            SimpleLayout.Label(title);
            SimpleLayout.EndHorizontal();

            // list of modifiers
            string[] items = new string[HeroKitCommon.statsDatabase.propertiesList.properties.Count];
            for (int i = 0; i < HeroKitCommon.statsDatabase.propertiesList.properties.Count; i++)
            {
                items[i] = HeroKitCommon.statsDatabase.propertiesList.properties[i].itemProperties.strings.items[0].value;
            }
            DropDownValues statList = new DropDownValues();
            statList.setValues("", items);

            if (intFields_att[countField].value > 0)
            {
                int s = countField + 1;

                SimpleLayout.Line();
                for (int i = 0; i < intFields_att[countField].value; i++)
                {
                    SimpleLayout.BeginHorizontal();
                    intFields_att[s + itemMaxCount + i].value = SimpleLayout.IntField(intFields_att[s + itemMaxCount + i].value, 100);
                    intFields_att[s + i].value = SimpleLayout.DropDownList(intFields_att[s + i].value, statList, 0, 150);
                    SimpleLayout.Space();
                    SimpleLayout.EndHorizontal();
                }
            }

            SimpleLayout.EndVertical();
        }
        public static void DrawStatsB(List<IntField> intFields_att, List<BoolField> boolFields_att, string title = "Change stats")
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            int countField = 22;
            int itemMaxCount = 10;

            // title bar
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Button("[+]", addItem, intFields_att, countField, Button.StyleDefault, 20);
            SimpleLayout.Button("[–]", removeItem, intFields_att, countField, Button.StyleDefault, 15);
            SimpleLayout.Label(title);
            SimpleLayout.EndHorizontal();

            // list of modifiers
            string[] items = new string[HeroKitCommon.statsDatabase.propertiesList.properties.Count];
            for (int i = 0; i < HeroKitCommon.statsDatabase.propertiesList.properties.Count; i++)
            {
                items[i] = HeroKitCommon.statsDatabase.propertiesList.properties[i].itemProperties.strings.items[0].value;
            }
            DropDownValues statList = new DropDownValues();
            statList.setValues("", items);

            // list of stat change types
            DropDownValues changeList = new DropDownValues();
            string[] changeItems = { "temporary", "permanent" };
            changeList.setValues("", changeItems);

            if (intFields_att[countField].value > 0)
            {
                int s = countField + 1;

                SimpleLayout.Line();
                for (int i = 0; i < intFields_att[countField].value; i++)
                {
                    SimpleLayout.BeginHorizontal();
                    intFields_att[s + i].value = SimpleLayout.DropDownList(intFields_att[s + i].value, statList, 0, 150);
                    intFields_att[s + itemMaxCount + i].value = SimpleLayout.IntField(intFields_att[s + itemMaxCount + i].value, 100);
                    SimpleLayout.Label("%");
                    SimpleLayout.Space();
                    intFields_att[s + itemMaxCount * 2 + i].value = SimpleLayout.DropDownList(intFields_att[s + itemMaxCount * 2 + i].value, changeList, 0, 100);
                    SimpleLayout.EndHorizontal();
                }
            }

            SimpleLayout.EndVertical();
        }

        /// <summary>
        /// Draw when item can be used
        /// </summary>
        public static void DrawElements(List<IntField> intFields_att, List<BoolField> boolFields_att, string title = "Change elements", bool showVal = true)
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            int countField = 84;
            int itemMaxCount = 10;

            // title bar
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Button("[+]", addItem, intFields_att, countField, Button.StyleDefault, 20);
            SimpleLayout.Button("[–]", removeItem, intFields_att, countField, Button.StyleDefault, 15);
            SimpleLayout.Label(title);
            SimpleLayout.EndHorizontal();

            // list of modifiers
            string[] items = new string[HeroKitCommon.elementDatabase.propertiesList.properties.Count];
            for (int i = 0; i < HeroKitCommon.elementDatabase.propertiesList.properties.Count; i++)
            {
                items[i] = HeroKitCommon.elementDatabase.propertiesList.properties[i].itemProperties.strings.items[0].value;
            }
            DropDownValues elementList = new DropDownValues();
            elementList.setValues("", items);
            
            // list of stat change types
            DropDownValues changeTypeValues = new DropDownValues();
            string[] changeType = { "1-Very weak against this",
                                    "2-Weak against this",
                                    "3-Strong agains this",
                                    "4-Very strong against this"
                                    };
            changeTypeValues.setValues("", changeType);

            if (intFields_att[countField].value > 0)
            {
                int s = countField + 1;

                SimpleLayout.Line();
                for (int i = 0; i < intFields_att[countField].value; i++)
                {
                    SimpleLayout.BeginHorizontal();
                    intFields_att[s + i].value = SimpleLayout.DropDownList(intFields_att[s + i].value, elementList, 0, 150);
                    if (showVal) intFields_att[s + itemMaxCount + i].value = SimpleLayout.DropDownList(intFields_att[s + itemMaxCount + i].value, changeTypeValues, 0, 150);
                    SimpleLayout.Space();
                    SimpleLayout.EndHorizontal();
                }
            }

            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Draw when item can be used
        /// </summary>
        public static void DrawConditions(List<IntField> intFields_att, List<BoolField> boolFields_att, string title = "Change conditions", string remove = "Remove all conditions")
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            int countField = 105;
            int itemMaxCount = 10;

            // title bar
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Button("[+]", addItem, intFields_att, countField, Button.StyleDefault, 20);
            SimpleLayout.Button("[–]", removeItem, intFields_att, countField, Button.StyleDefault, 15);
            SimpleLayout.Label(title);
            SimpleLayout.Space();          
            boolFields_att[0].value = SimpleLayout.BoolField(boolFields_att[0].value);
            SimpleLayout.Label(remove);
            SimpleLayout.EndHorizontal();
            
            // list of modifiers
            string[] items = new string[HeroKitCommon.conditionDatabase.propertiesList.properties.Count];
            for (int i = 0; i < HeroKitCommon.conditionDatabase.propertiesList.properties.Count; i++)
            {
                items[i] = HeroKitCommon.conditionDatabase.propertiesList.properties[i].itemProperties.strings.items[0].value;
            }
            DropDownValues condList = new DropDownValues();
            condList.setValues("", items);

            // list of stat change types
            DropDownValues changeTypeValues = new DropDownValues();
            string[] changeType = { "ON", "OFF" };
            changeTypeValues.setValues("", changeType);

            // list of stat change types
            DropDownValues changePercentage = new DropDownValues();
            string[] percentage = { "100% success", "90% success", "80% success", "70% success", "60% success",
                                    "50% success", "40% success", "30% success", "20% success", "10% success" };
            changePercentage.setValues("", percentage);

            if (intFields_att[countField].value > 0)
            {
                int s = countField + 1;

                SimpleLayout.Line();
                for (int i = 0; i < intFields_att[countField].value; i++)
                {
                    SimpleLayout.BeginHorizontal();
                    intFields_att[s + i].value = SimpleLayout.DropDownList(intFields_att[s + i].value, condList, 0, 150);
                    intFields_att[s + itemMaxCount + i].value = SimpleLayout.DropDownList(intFields_att[s + itemMaxCount + i].value, changeTypeValues, 0, 150);
                    intFields_att[s + itemMaxCount * 2 + i].value = SimpleLayout.DropDownList(intFields_att[s + itemMaxCount * 2 + i].value, changePercentage, 0, 100);
                    SimpleLayout.Space();
                    SimpleLayout.EndHorizontal();
                }
            }

            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Draw when item can be used
        /// </summary>
        public static void DrawExtras(List<IntField> intFields_att, string title = "Add variance to stats and meters")
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            SimpleLayout.BeginHorizontal();           
            intFields_att[0].value = SimpleLayout.IntField(intFields_att[0].value, 100);
            SimpleLayout.Label(title);
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
            SimpleLayout.EndVertical();
        }

        /// <summary>
        /// Draw item type
        /// </summary>
        public static void DrawItemWeight(List<IntField> intFields_att, string title = "Item Weight")
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            SimpleLayout.BeginHorizontal();     
            intFields_att[136].value = SimpleLayout.IntField(intFields_att[136].value, 100);
            SimpleLayout.Label(title);
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Draw item type
        /// </summary>
        public static void DrawStackItems(List<BoolField> boolFields_att, string title = "Stack item")
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label(title + ":");
            boolFields_att[1].value = SimpleLayout.BoolField(boolFields_att[1].value);
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
            SimpleLayout.EndVertical();
        }

        public static void addItem(List<IntField> intFields_att, int id)
        {
            if (intFields_att[id].value < 10)
                intFields_att[id].value++;
        }
        public static void removeItem(List<IntField> intFields_att, int id)
        {
            if (intFields_att[id].value > 0)
                intFields_att[id].value--;
        }



        // make bit = 0
        static int ZeroBit(int value, int position)
        {
            return value & ~(1 << position);
        }

        // make bit = 1
        static int OneBit(int value, int position)
        {
            return value | (1 << position);
        }


        public static BitArray CreateBitArray(string str)
        {
            bool[] bits = new bool[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '1')
                    bits[i] = true;
            }

            BitArray bitArray = new BitArray(bits);
            return bitArray;
        }
        public static string CreateBitString(BitArray bitArray)
        {
            // Display all bits.
            string str = "";
            foreach (bool bit in bitArray)
            {
                if (bit == true)
                    str += "1";
                else
                    str += "0";
            }
            return str;
        }
        public static string ResizeBitString(string str, int itemsLength)
        {
            // if string length is different than items length, adjust string
            if (str.Length != itemsLength)
            {
                // increase string on right side (items were added)
                if (str.Length < itemsLength)
                {
                    int diff = Mathf.Abs(str.Length - itemsLength);
                    str = str + new string('0', diff);
                }
                // decrease string on right side (items were removed)
                else if (str.Length > itemsLength)
                    str = str.Substring(0, itemsLength);
            }

            return str;
        }
        public static int[] CreateIntArray(string str)
        {
            int[] intArray = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                intArray[i] = (int)Char.GetNumericValue(str[i]);
            }
            return intArray;
        }
        public static string CreateIntString(int[] intArray)
        {
            // Display all ints.
            string str = "";
            foreach (int num in intArray)
            {
                str += num.ToString();
            }
            return str;
        }
        public static int[] CreateIntArrayLarge(string str)
        {
            int seperatorCount = str.Count(x => x == '|');
            int[] intArray = new int[seperatorCount];
            string tempInt = "";
            int intCount = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '|')
                {
                    intArray[intCount] = Int32.Parse(tempInt);
                    tempInt = "";
                    intCount++;
                    continue;
                }
                else
                {
                    tempInt = tempInt + str[i];
                }
            }
            return intArray;
        }
        public static string CreateIntStringLarge(int[] intArray)
        {
            // Display all ints.
            string str = "";
            foreach (int num in intArray)
            {
                str += num.ToString() + "|";
            }
            return str;
        }
        public static string ResizeIntStringLarge(string str, int itemsLength)
        {
            int seperatorCount = str.Count(x => x == '|');

            // if string length is different than items length, adjust string
            if (seperatorCount != itemsLength)
            {
                // increase string on right side (items were added)
                if (seperatorCount < itemsLength)
                {
                    int diff = Mathf.Abs(seperatorCount - itemsLength);
                    str = str + RepeatSubstring("0|", diff);
                }

                // decrease string on right side (items were removed)
                else if (seperatorCount > itemsLength)
                {
                    // get index of seperator for last item in list (itemsLength)
                    int index = GetNthIndex(str, '|', itemsLength);

                    // delete anything after that seperator
                    str = str.Substring(0, index + 1);
                }
            }

            return str;
        }
        // repeat a substring
        public static string RepeatSubstring(string substring, int count)
        {
            if (!string.IsNullOrEmpty(substring))
            {
                StringBuilder builder = new StringBuilder(substring.Length * count);
                for (int i = 0; i < count; i++) builder.Append(substring);
                return builder.ToString();
            }
            return string.Empty;
        }
        // get nth index of a character in a string
        public static int GetNthIndex(string str, char character, int number)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == character)
                {
                    count++;
                    if (count == number)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }



        // [add] [remove] buttons for search that gets all items of a type or one item of a type 
        public static void addRemoveButton(BitArray bitArray, int itemID, DropDownValues itemList)
        {
            // add weapon button (add one item)
            if (itemID != -1)
            {
                SimpleLayout.Button("Add", addToBitarray, bitArray, itemID, Button.StyleA);
                SimpleLayout.Space(1);
                SimpleLayout.Button("Remove", removeFromBitarray, bitArray, itemID, Button.StyleA);
            }
            // add all weapons of a specific type
            else
            {
                SimpleLayout.Button("Add", addAllToBitarray, bitArray, itemList, Button.StyleA);
                SimpleLayout.Space(1);
                SimpleLayout.Button("Remove", removeAllFromBitarray, bitArray, itemList, Button.StyleA);
            }
        }
        // turn an item on in the bit array
        public static void addToBitarray(BitArray bitArray, int ID)
        {
            // turn an item of a specific type on
            if (ID >= 0)
                bitArray[ID-1] = true;
        }
        // turn all items of a specific type on in the bit array
        public static void addAllToBitarray(BitArray bitArray, DropDownValues items)
        {
            // start at 1 because 0 = -1 (all)
            for (int i = 1; i < items.ids.Length; i++)
            {
                bitArray[items.ids[i-1]] = true;
            }
        }
        // turn an item off in the bit array
        public static void removeFromBitarray(BitArray bitArray, int ID)
        {
            // turn an item of a specific type on
            if (ID >= 0)
                bitArray[ID-1] = false;
        }
        // turn all items of a specific type off in the bit array
        public static void removeAllFromBitarray(BitArray bitArray, DropDownValues items)
        {
            // start at 1 because 0 = -1 (all)
            for (int i = 1; i < items.ids.Length; i++)
            {
                bitArray[items.ids[i-1]] = false;
            }
        }
        // generate a list of items in bit array
        public static void getOnInBitarray(BitArray bitArray, DropDownValues items)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    SimpleLayout.BeginHorizontal();
                    bitArray[i] = SimpleLayout.BoolField(bitArray[i]);
                    SimpleLayout.Label(items.items[i]);
                    SimpleLayout.EndHorizontal();
                }
            }
        }
        // generate a list of items of a specific type in bit array
        public static void getOnInBitarray(BitArray bitArray, DropDownValues items, int itemType, HeroObject database, int itemTypeSlot)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (database.propertiesList.properties[i].itemProperties.ints.items[itemTypeSlot].value == itemType)
                {
                    SimpleLayout.BeginHorizontal();
                    bitArray[i] = SimpleLayout.BoolField(bitArray[i]);
                    SimpleLayout.Label(items.items[i]);
                    SimpleLayout.EndHorizontal();
                }
            }
        }



        // [add] [remove] buttons for search that gets all items of a type or one item of a type 
        public static void addRemoveButton(int[] intArray, int itemID, DropDownValues itemList)
        {
            // add weapon button (add one item)
            if (itemID != -1)
            {
                SimpleLayout.Button("Add", addToIntarray, intArray, itemID, Button.StyleA);
                SimpleLayout.Space(1);
                SimpleLayout.Button("Remove", removeFromIntarray, intArray, itemID, Button.StyleA);
            }
            // add all weapons of a specific type
            else
            {
                SimpleLayout.Button("Add", addAllToIntarray, intArray, itemList, Button.StyleA);
                SimpleLayout.Space(1);
                SimpleLayout.Button("Remove", removeAllFromIntarray, intArray, itemList, Button.StyleA);
            }
        }
        // turn an item on in the bit array
        public static void addToIntarray(int[] intArray, int ID)
        {
            // turn an item of a specific type on
            if (ID >= 0)
                intArray[ID-1] = 1;
        }
        // turn all items of a specific type on in the bit array
        public static void addAllToIntarray(int[] intArray, DropDownValues items)
        {
            // start at 1 because 0 = -1 (all)
            for (int i = 1; i < items.ids.Length; i++)
            {
                intArray[items.ids[i-1]] = 1;
            }
        }
        // turn an item off in the bit array
        public static void removeFromIntarray(int[] intArray, int ID)
        {
            // turn an item of a specific type on
            if (ID >= 0)
                intArray[ID-1] = 0;
        }
        // turn all items of a specific type off in the bit array
        public static void removeAllFromIntarray(int[] intArray, DropDownValues items)
        {
            // start at 1 because 0 = -1 (all)
            for (int i = 1; i < items.ids.Length; i++)
            {
                intArray[items.ids[i-1]] = 0;
            }
        }
        // generate a list of items in bit array (ints can be 0-9)
        public static void getOnInIntarray(int[] intArray, DropDownValues items, DropDownValues choices)
        {
            for (int i = 0; i < intArray.Length; i++)
            {
                if (intArray[i] > 0)
                {
                    SimpleLayout.BeginHorizontal();

                    // show checkbox
                    bool val = (intArray[i] == 0) ? false : true;
                    val = SimpleLayout.BoolField(val);
                    if (!val) intArray[i] = 0;

                    // show name of item
                    SimpleLayout.Label(items.items[i]);

                    SimpleLayout.Space();

                    // show drop down list
                    intArray[i] = SimpleLayout.DropDownList(intArray[i], choices, 0, 300);

                    SimpleLayout.EndHorizontal();
                }
            }
        }


        // generate a list of items in bit array
        public static void getOnInComboarray(BitArray bitArray, int[] intArray, DropDownValues items)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    SimpleLayout.BeginHorizontal();
                    bitArray[i] = SimpleLayout.BoolField(bitArray[i]);
                    SimpleLayout.Space(4);
                    intArray[i] = SimpleLayout.IntField(intArray[i], 50);
                    SimpleLayout.Label(items.items[i]);
                    SimpleLayout.Space();                
                    SimpleLayout.EndHorizontal();
                }
            }
        }
    }
}