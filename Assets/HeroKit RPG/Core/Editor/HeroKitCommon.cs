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
        #region Databases
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
        // element type database variables
        public static HeroObject elementTypeDatabase;
        public static HeroKitProperty elementTypeProperties;
        // conditions type database variables
        public static HeroObject conditionTypeDatabase;
        public static HeroKitProperty conditionTypeProperties;
        // class database variables
        public static HeroObject classDatabase;
        public static HeroKitProperty classProperties;
        // subclass database variables
        public static HeroObject subclassDatabase;
        public static HeroKitProperty subclassProperties;
        // race database variables
        public static HeroObject raceDatabase;
        public static HeroKitProperty raceProperties;
        // subrace database variables
        public static HeroObject subraceDatabase;
        public static HeroKitProperty subraceProperties;
        // alignment database variables
        public static HeroObject alignmentDatabase;
        public static HeroKitProperty alignmentProperties;
        // character type database variables
        public static HeroObject characterTypeDatabase;
        public static HeroKitProperty characterTypeProperties;
        // character database variables
        public static HeroObject characterDatabase;
        public static HeroKitProperty characterProperties;

        // attribute database variables
        public static HeroObject itemDatabase_attributes;
        public static HeroObject affixDatabase_attributes;
        public static HeroObject conditionDatabase_attributes;
        public static HeroObject weaponDatabase_attributes;
        public static HeroObject armorDatabase_attributes;
        public static HeroObject ammunitionDatabase_attributes;
        public static HeroObject abilityDatabase_attributes;
        public static HeroObject classDatabase_attributes;
        public static HeroObject subclassDatabase_attributes;
        public static HeroObject raceDatabase_attributes;
        public static HeroObject subraceDatabase_attributes;
        public static HeroObject alignmentDatabase_attributes;
        public static HeroObject characterDatabase_attributes;
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

            // element type database  
            elementTypeDatabase = (elementTypeDatabase == null) ? GetScriptableObject<HeroObject>("ElementTypeDatabase", dbPath) : elementTypeDatabase;
            elementTypeProperties = (elementTypeProperties == null) ? GetScriptableObject<HeroKitProperty>("ElementTypeProperties", propertiesPath) : elementTypeProperties;

            // condition type database  
            conditionTypeDatabase = (conditionTypeDatabase == null) ? GetScriptableObject<HeroObject>("ConditionTypeDatabase", dbPath) : conditionTypeDatabase;
            conditionTypeProperties = (conditionTypeProperties == null) ? GetScriptableObject<HeroKitProperty>("ConditionTypeProperties", propertiesPath) : conditionTypeProperties;

            // class database
            classDatabase = (classDatabase == null) ? GetScriptableObject<HeroObject>("ClassDatabase", dbPath) : classDatabase;
            classProperties = (classProperties == null) ? GetScriptableObject<HeroKitProperty>("ClassProperties", propertiesPath) : classProperties;

            // subclass database
            subclassDatabase = (subclassDatabase == null) ? GetScriptableObject<HeroObject>("SubclassDatabase", dbPath) : subclassDatabase;
            subclassProperties = (subclassProperties == null) ? GetScriptableObject<HeroKitProperty>("SubclassProperties", propertiesPath) : subclassProperties;

            // race database
            raceDatabase = (raceDatabase == null) ? GetScriptableObject<HeroObject>("RaceDatabase", dbPath) : raceDatabase;
            raceProperties = (raceProperties == null) ? GetScriptableObject<HeroKitProperty>("RaceProperties", propertiesPath) : raceProperties;

            // subrace database
            subraceDatabase = (subraceDatabase == null) ? GetScriptableObject<HeroObject>("SubraceDatabase", dbPath) : subraceDatabase;
            subraceProperties = (subraceProperties == null) ? GetScriptableObject<HeroKitProperty>("SubraceProperties", propertiesPath) : subraceProperties;

            // alignment database
            alignmentDatabase = (alignmentDatabase == null) ? GetScriptableObject<HeroObject>("AlignmentDatabase", dbPath) : alignmentDatabase;
            alignmentProperties = (alignmentProperties == null) ? GetScriptableObject<HeroKitProperty>("AlignmentProperties", propertiesPath) : alignmentProperties;

            // character type database
            characterTypeDatabase = (characterTypeDatabase == null) ? GetScriptableObject<HeroObject>("CharacterTypeDatabase", dbPath) : characterTypeDatabase;
            characterTypeProperties = (characterTypeProperties == null) ? GetScriptableObject<HeroKitProperty>("CharacterTypeProperties", propertiesPath) : characterTypeProperties;

            // character database
            characterDatabase = (characterDatabase == null) ? GetScriptableObject<HeroObject>("CharacterDatabase", dbPath) : characterDatabase;
            characterProperties = (characterProperties == null) ? GetScriptableObject<HeroKitProperty>("CharacterProperties", propertiesPath) : characterProperties;

            // attribute database
            itemDatabase_attributes = (itemDatabase_attributes == null) ? GetScriptableObject<HeroObject>("ItemDatabase_attributes", dbPath) : itemDatabase_attributes;
            affixDatabase_attributes = (affixDatabase_attributes == null) ? GetScriptableObject<HeroObject>("AffixDatabase_attributes", dbPath) : affixDatabase_attributes;
            conditionDatabase_attributes = (conditionDatabase_attributes == null) ? GetScriptableObject<HeroObject>("ConditionDatabase_attributes", dbPath) : conditionDatabase_attributes;
            weaponDatabase_attributes = (weaponDatabase_attributes == null) ? GetScriptableObject<HeroObject>("WeaponDatabase_attributes", dbPath) : weaponDatabase_attributes;
            armorDatabase_attributes = (armorDatabase_attributes == null) ? GetScriptableObject<HeroObject>("ArmorDatabase_attributes", dbPath) : armorDatabase_attributes;
            ammunitionDatabase_attributes = (ammunitionDatabase_attributes == null) ? GetScriptableObject<HeroObject>("AmmunitionDatabase_attributes", dbPath) : ammunitionDatabase_attributes;
            abilityDatabase_attributes = (abilityDatabase_attributes == null) ? GetScriptableObject<HeroObject>("AbilityDatabase_attributes", dbPath) : abilityDatabase_attributes;
            classDatabase_attributes = (classDatabase_attributes == null) ? GetScriptableObject<HeroObject>("ClassDatabase_attributes", dbPath) : classDatabase_attributes;
            subclassDatabase_attributes = (subclassDatabase_attributes == null) ? GetScriptableObject<HeroObject>("SubclassDatabase_attributes", dbPath) : subclassDatabase_attributes;
            raceDatabase_attributes = (raceDatabase_attributes == null) ? GetScriptableObject<HeroObject>("RaceDatabase_attributes", dbPath) : raceDatabase_attributes;
            subraceDatabase_attributes = (subraceDatabase_attributes == null) ? GetScriptableObject<HeroObject>("SubraceDatabase_attributes", dbPath) : subraceDatabase_attributes;
            alignmentDatabase_attributes = (alignmentDatabase_attributes == null) ? GetScriptableObject<HeroObject>("AlignmentDatabase_attributes", dbPath) : alignmentDatabase_attributes;
            characterDatabase_attributes = (characterDatabase_attributes == null) ? GetScriptableObject<HeroObject>("CharacterDatabase_attributes", dbPath) : characterDatabase_attributes;

            attributeProperties = (attributeProperties == null) ? GetScriptableObject<HeroKitProperty>("AttributeProperties", propertiesPath) : attributeProperties;

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
        #endregion

        //-------------------------------------------
        // Item lists
        //-------------------------------------------
        # region drop-down lists
        // get properties for a database
        public static HeroProperties getHeroProperties(HeroKitProperty heroKitProperty)
        {
            HeroProperties hp = new HeroProperties();
            hp.propertyTemplate = heroKitProperty;
            hp.itemProperties = heroKitProperty.properties.Clone(heroKitProperty.properties);
            return hp;
        }
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
        // show all, show selected, show not selected
        public static DropDownValues showList()
        {
            // list of stat change types              
            string[] changeType = { "Show All", "Show Selected", "Show Not Selected" };
            DropDownValues itemsList = new DropDownValues();
            itemsList.setValues("", changeType);
            return itemsList;
        }
        #endregion

        //-------------------------------------------
        // Left Menu Actions
        //-------------------------------------------
        #region Right-Click Menu Actions
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
        #endregion

        //-------------------------------------------
        // In-Line Actions
        //-------------------------------------------
        #region Right-Click In-Line Actions
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
        #endregion

        //-------------------------------------------
        // Fields and Field Groups
        //-------------------------------------------

        #region field groups: draw value / draw list
        //-------------------------------------------
        // Checkbox, Item name, item value (int)
        //-------------------------------------------
        public static void DrawItemValue(List<StringField> stringFields, List<IntField> intFields, string title, 
                                         int boolMaskID, int intMaskID, int itemTypeID, bool showItem, UnityAction toggleItem,
                                         HeroObject database)
        {
            DropDownValues itemList = HeroKitCommon.databaseList(database);

            // resize string if things have changed
            stringFields[boolMaskID].value = HeroKitCommon.ResizeBitString(stringFields[boolMaskID].value, itemList.items.Length);
            stringFields[intMaskID].value = HeroKitCommon.ResizeIntStringLarge(stringFields[intMaskID].value, itemList.items.Length);

            // convert intstring into intarray
            BitArray bitArray = HeroKitCommon.CreateBitArray(stringFields[boolMaskID].value);
            int[] intArray = HeroKitCommon.CreateIntArrayLarge(stringFields[intMaskID].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label(title);
            SimpleLayout.Space();
            string buttonText = showItem ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleItem, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showItem)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[itemTypeID].value = SimpleLayout.DropDownList(intFields[itemTypeID].value, itemList, 0, 150);
                addRemoveButton(bitArray, intFields[itemTypeID].value, itemList);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                getOnInComboarray(bitArray, intArray, itemList);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[boolMaskID].value = HeroKitCommon.CreateBitString(bitArray);
            stringFields[intMaskID].value = HeroKitCommon.CreateIntStringLarge(intArray);
        }
        public static void DrawMoneyValue(List<StringField> stringFields, List<IntField> intFields, string title = "Worth of item")
        {
            DrawItemValue(stringFields, intFields, title, 0, 1, 1, showMoney, toggleMoney, HeroKitCommon.moneyDatabase);
        }
        public static void DrawStatsValue(List<StringField> stringFields, List<IntField> intFields, string title = "Change stats")
        {
            DrawItemValue(stringFields, intFields, title, 2, 3, 2, showStats, toggleStats, HeroKitCommon.statsDatabase);
        }
        public static void DrawMeterMaxList(List<StringField> stringFields, List<IntField> intFields, string title = "Meters (max starting values)")
        {
            DrawItemValue(stringFields, intFields, title, 24, 25, 13, showMeters, toggleMeters, HeroKitCommon.meterDatabase);
        }

        //-------------------------------------------
        // Checkbox, Item name, item value (int), drop-down list
        //-------------------------------------------
        public static void DrawItemValueB(List<StringField> stringFields, List<IntField> intFields, string title,
                                         int boolMaskID, int intMaskID, int selectMaskID, int itemTypeID, bool showItem, UnityAction toggleItem,
                                         DropDownValues itemList, DropDownValues selectionList, string intFieldLabel="")
        {
            // resize string if things have changed
            stringFields[boolMaskID].value = ResizeBitString(stringFields[boolMaskID].value, itemList.items.Length);
            stringFields[intMaskID].value = ResizeIntStringLarge(stringFields[intMaskID].value, itemList.items.Length);
            stringFields[selectMaskID].value = ResizeBitString(stringFields[selectMaskID].value, itemList.items.Length);

            // convert intstring into intarray
            BitArray bitArray = CreateBitArray(stringFields[boolMaskID].value);
            int[] intArray = CreateIntArrayLarge(stringFields[intMaskID].value);
            int[] selectArray = CreateIntArray(stringFields[selectMaskID].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label(title);
            SimpleLayout.Space();
            string buttonText = showItem ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleItem, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showItem)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[itemTypeID].value = SimpleLayout.DropDownList(intFields[itemTypeID].value, itemList, 0, 150);
                addRemoveButton(bitArray, intFields[itemTypeID].value, itemList);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                getDropdownComboarrayB(bitArray, intArray, selectArray, itemList, selectionList, intFieldLabel);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[boolMaskID].value = CreateBitString(bitArray);
            stringFields[intMaskID].value = CreateIntStringLarge(intArray);
            stringFields[selectMaskID].value = CreateIntString(selectArray);
        }
        public static void DrawMeterValue(List<StringField> stringFields, List<IntField> intFields, string title = "Change meters")
        {
            DropDownValues changeList = new DropDownValues();
            string[] changeItems = { "value", "% of value", "max value" };
            changeList.setValues("", changeItems);

            DropDownValues meterList = databaseList(meterDatabase);

            DrawItemValueB(stringFields, intFields, title, 4, 5, 6, 3, showMeters, toggleMeters, meterList, changeList);
        }
        public static void DrawMeterValue_Conditions(List<StringField> stringFields, List<IntField> intFields, string title = "Change meters")
        {
            DropDownValues changeList = new DropDownValues();
            string[] changeItems = { "permanent", "temporary" };
            changeList.setValues("", changeItems);

            DropDownValues meterList = databaseList(meterDatabase);

            DrawItemValueB(stringFields, intFields, title, 4, 5, 6, 3, showMeters, toggleMeters, meterList, changeList, "%");
        }
        public static void DrawStatValue_Conditions(List<StringField> stringFields, List<IntField> intFields, string title = "Change stats")
        {
            DropDownValues changeList = new DropDownValues();
            string[] changeItems = { "permanent", "temporary" };
            changeList.setValues("", changeItems);

            DropDownValues stats = databaseList(statsDatabase);

            DrawItemValueB(stringFields, intFields, title, 2, 3, 13, 2, showStats, toggleStats, stats, changeList, "%");
        }
        public static void DrawMeterValue_Abilities(List<StringField> stringFields, List<IntField> intFields, string title = "Change meters on target")
        {
            DropDownValues formulas = databaseList(formulaDatabase); 
            DropDownValues meters = databaseList(meterDatabase);

            DrawItemValueB(stringFields, intFields, title, 14, 15, 16, 9, showMeters2, toggleMeters2, meters, formulas, "points | formula:");
        }

        //-------------------------------------------
        // Checkbox, Item name
        //-------------------------------------------
        public static void DrawItemValueC(List<StringField> stringFields, List<IntField> intFields, string title,
                                         int boolMaskID, int itemTypeID, bool showItem, UnityAction toggleItem,
                                         DropDownValues itemList)
        {
            // resize string if things have changed
            stringFields[boolMaskID].value = ResizeBitString(stringFields[boolMaskID].value, itemList.items.Length);

            // convert intstring into intarray
            BitArray bitArray = CreateBitArray(stringFields[boolMaskID].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label(title);
            SimpleLayout.Space();
            string buttonText = showItem ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleItem, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showItem)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[itemTypeID].value = SimpleLayout.DropDownList(intFields[itemTypeID].value, itemList, 0, 150);
                addRemoveButton(bitArray, intFields[itemTypeID].value, itemList);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                getDropdownComboarrayC(bitArray, itemList);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[boolMaskID].value = CreateBitString(bitArray);
        }
        public static void DrawElementValue(List<StringField> stringFields, List<IntField> intFields, string title = "Attach elements to this item")
        {
            DropDownValues elements = databaseList(elementDatabase);

            DrawItemValueC(stringFields, intFields, title, 7, 4, showElements, toggleElements, elements);
        }
        public static void DrawAffixValue(List<StringField> stringFields, List<IntField> intFields, string title = "Attach affixes to this item")
        {
            DropDownValues affix = databaseList(affixDatabase);

            DrawItemValueC(stringFields, intFields, title, 11, 7, showAffix, toggleAffix, affix);
        }
        public static void DrawSocketsValue(List<StringField> stringFields, List<IntField> intFields, string title = "Attach sockets to this item")
        {
            DropDownValues sockets = databaseList(affixDatabase);

            DrawItemValueC(stringFields, intFields, title, 12, 8, showSockets, toggleSockets, sockets);
        }
        public static void DrawAlignmentValue(List<StringField> stringFields, List<IntField> intFields, string title = "Alignments available for this")
        {
            DropDownValues alignments = databaseList(alignmentDatabase);

            DrawItemValueC(stringFields, intFields, title, 30, 18, showAlignment, toggleAlignment, alignments);
        }

        //-------------------------------------------
        // Checkbox, Item name, drop-down list a, drop-down list b
        //-------------------------------------------
        public static void DrawItemValueD(List<StringField> stringFields, List<IntField> intFields, string title,
                                         int boolMaskID, int selectMaskIDA, int selectMaskIDB, int itemTypeID, bool showItem, UnityAction toggleItem,
                                         DropDownValues itemList, DropDownValues selectionListA, DropDownValues selectionListB)
        {
            // resize string if things have changed
            stringFields[boolMaskID].value = ResizeBitString(stringFields[boolMaskID].value, itemList.items.Length);
            stringFields[selectMaskIDA].value = ResizeIntStringLarge(stringFields[selectMaskIDA].value, itemList.items.Length);
            stringFields[selectMaskIDB].value = ResizeIntStringLarge(stringFields[selectMaskIDB].value, itemList.items.Length);

            // convert intstring into intarray
            BitArray bitArray = CreateBitArray(stringFields[boolMaskID].value);
            int[] intArray = CreateIntArrayLarge(stringFields[selectMaskIDA].value);
            int[] selectArray = CreateIntArrayLarge(stringFields[selectMaskIDB].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label(title);
            SimpleLayout.Space();
            string buttonText = showItem ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleItem, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showItem)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[itemTypeID].value = SimpleLayout.DropDownList(intFields[itemTypeID].value, itemList, 0, 150);
                addRemoveButton(bitArray, intFields[itemTypeID].value, itemList);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                getDropdownComboarrayD(bitArray, intArray, selectArray, itemList, selectionListA, selectionListB);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[boolMaskID].value = CreateBitString(bitArray);
            stringFields[selectMaskIDA].value = CreateIntStringLarge(intArray);
            stringFields[selectMaskIDB].value = CreateIntStringLarge(selectArray);
        }
        public static void DrawConditionsValue(List<StringField> stringFields, List<IntField> intFields, string title = "Change conditions")
        {
            DropDownValues conditions = databaseList(conditionDatabase);

            // list of stat change types
            DropDownValues changeType = new DropDownValues();
            string[] change = { "ON", "OFF" };
            changeType.setValues("", change);

            // list of stat change types
            DropDownValues changePercentage = new DropDownValues();
            string[] percentage = { "100% success", "90% success", "80% success", "70% success", "60% success",
                                    "50% success", "40% success", "30% success", "20% success", "10% success" };
            changePercentage.setValues("", percentage);

            DrawItemValueD(stringFields, intFields, title, 8, 9, 10, 5, showConditions, toggleConditions, conditions, changeType, changePercentage);
        }

        //-------------------------------------------
        // Checkbox, Item name (show all items of specific type)
        //-------------------------------------------
        public static void DrawItemValueE(List<StringField> stringFields, List<IntField> intFields, string title,
                                         int boolMaskID, int itemTypeID, bool showItem, 
                                         UnityAction toggleItem, int itemDatabase_itemType,
                                         HeroObject itemTypeDatabase, HeroObject itemDatabase,
                                         ref int showOnItem)
        {
            // weapon type field
            DropDownValues itemTypeList = HeroKitCommon.databaseList(itemTypeDatabase);
            DropDownValues itemList = HeroKitCommon.databaseList(itemDatabase);

            // resize string if things have changed
            stringFields[boolMaskID].value = HeroKitCommon.ResizeBitString(stringFields[boolMaskID].value, itemList.items.Length);

            // convert bitstring into bitarray
            BitArray bitArray = HeroKitCommon.CreateBitArray(stringFields[boolMaskID].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label(title);
            SimpleLayout.Space();
            string buttonText = showItem ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleItem, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showItem)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[itemTypeID].value = SimpleLayout.DropDownList(intFields[itemTypeID].value, itemTypeList, 0, 150);
                SimpleLayout.Space(4);
                showOnItem = SimpleLayout.DropDownList(showOnItem, showList(), 0, 150);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                HeroKitCommon.getOnInBitarray(bitArray, itemList, intFields[itemTypeID].value, itemDatabase, itemDatabase_itemType, showOnItem);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[boolMaskID].value = HeroKitCommon.CreateBitString(bitArray);
        }
        public static void DrawWeaponList(List<StringField> stringFields, List<IntField> intFields, string title = "Weapons allowed for this")
        {
            DrawItemValueE(stringFields, intFields, title, 17, 10, 
                           showWeapons, toggleWeaponType, 0,
                           weaponTypeDatabase, weaponDatabase, ref showOnWeapons);
        }
        public static void DrawArmorList(List<StringField> stringFields, List<IntField> intFields, string title = "Armor allowed for this")
        {
            DrawItemValueE(stringFields, intFields, title, 18, 12,
                           showArmor, toggleArmorType, 0,
                           armorTypeDatabase, armorDatabase, ref showOnArmor);
        }
        public static void DrawAbilityList(List<StringField> stringFields, List<IntField> intFields, string title = "Abilities allowed for this")
        {
            DrawItemValueE(stringFields, intFields, title, 19, 13,
                           showAbility, toggleAbility, 0,
                           abilityTypeDatabase, abilityDatabase, ref showOnAbility);
        }


        //-------------------------------------------
        // Checkbox, Item name, drop-down list
        //-------------------------------------------
        public static void DrawItemValueF(List<StringField> stringFields, List<IntField> intFields, string title,
                                         int boolMaskID, int selectMaskID, int itemTypeID, bool showItem, UnityAction toggleItem,
                                         DropDownValues itemList, DropDownValues selectionList)
        {
            // resize string if things have changed
            stringFields[boolMaskID].value = ResizeBitString(stringFields[boolMaskID].value, itemList.items.Length);
            stringFields[selectMaskID].value = ResizeIntStringLarge(stringFields[selectMaskID].value, itemList.items.Length);

            // convert intstring into intarray
            BitArray bitArray = CreateBitArray(stringFields[boolMaskID].value);
            int[] intArray = CreateIntArrayLarge(stringFields[selectMaskID].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label(title);
            SimpleLayout.Space();
            string buttonText = showItem ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleItem, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showItem)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[itemTypeID].value = SimpleLayout.DropDownList(intFields[itemTypeID].value, itemList, 0, 150);
                addRemoveButton(bitArray, intFields[itemTypeID].value, itemList);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                getDropdownComboarray(bitArray, intArray, itemList, selectionList);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[boolMaskID].value = CreateBitString(bitArray);
            stringFields[selectMaskID].value = CreateIntStringLarge(intArray);
        }
        public static void DrawMeterIncrementList(List<StringField> stringFields, List<IntField> intFields, string title = "Meters (formula to use on max when level up)")
        {
            DropDownValues meters = databaseList(meterDatabase);
            DropDownValues formulas = databaseList(formulaDatabase);

            DrawItemValueF(stringFields, intFields, title,
                           28, 29, 17, showMeters2, toggleMeters2,
                           meters, formulas);
        }
        public static void DrawStatIncrementList(List<StringField> stringFields, List<IntField> intFields, string title = "Stats (formula to use when level up)")
        {
            DropDownValues stats = databaseList(statsDatabase);
            DropDownValues formulas = databaseList(formulaDatabase);

            DrawItemValueF(stringFields, intFields, title,
                           26, 27, 16, showStats2, toggleStats2,
                           stats, formulas);
        }

        //-------------------------------------------
        // Checkbox, Item name, drop-down (show all items of specific type)
        //-------------------------------------------
        public static void DrawItemValueG(List<StringField> stringFields, List<IntField> intFields, string title,
                                         int boolMaskID, int selectMaskID, int itemTypeID, 
                                         bool showItem, UnityAction toggleItem, 
                                         int itemDatabase_itemType,
                                         HeroObject itemTypeDatabase, HeroObject itemDatabase, DropDownValues selectionList,
                                         ref int showOnItem)
        {
            // weapon type field
            DropDownValues itemTypeList = HeroKitCommon.databaseList(itemTypeDatabase);
            DropDownValues itemList = HeroKitCommon.databaseList(itemDatabase);

            // resize string if things have changed
            stringFields[boolMaskID].value = HeroKitCommon.ResizeBitString(stringFields[boolMaskID].value, itemList.items.Length);
            stringFields[selectMaskID].value = ResizeIntStringLarge(stringFields[selectMaskID].value, itemList.items.Length);

            // convert bitstring into bitarray
            BitArray bitArray = HeroKitCommon.CreateBitArray(stringFields[boolMaskID].value);
            int[] intArray = CreateIntArrayLarge(stringFields[selectMaskID].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label(title);
            SimpleLayout.Space();
            string buttonText = showItem ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleItem, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showItem)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[itemTypeID].value = SimpleLayout.DropDownList(intFields[itemTypeID].value, itemTypeList, 0, 150);
                SimpleLayout.Space(4);
                showOnItem = SimpleLayout.DropDownList(showOnItem, showList(), 0, 150);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                //getOnInBitarray(bitArray, itemList, intFields[itemTypeID].value, 
                //                itemDatabase, itemDatabase_itemType, showOnItem);

                getDropdownComboarrayE(bitArray, intArray, itemList, selectionList, itemDatabase,
                                       intFields[itemTypeID].value, itemDatabase_itemType, showOnItem);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[boolMaskID].value = HeroKitCommon.CreateBitString(bitArray);
            stringFields[selectMaskID].value = CreateIntStringLarge(intArray);
        }
        public static void DrawConditionsList(List<StringField> stringFields, List<IntField> intFields, string title = "Conditions that affect this")
        {
            DropDownValues severity = conditionSeverityList();

            DrawItemValueG(stringFields, intFields, title,
                           19, 20, 14, 
                           showConditions, toggleConditions,
                           3,
                           conditionTypeDatabase, conditionDatabase, severity,
                           ref showOnCondition);
        }
        public static void DrawElementsList(List<StringField> stringFields, List<IntField> intFields, string title = "Elements that affect this")
        {
            DropDownValues severity = conditionSeverityList();

            DrawItemValueG(stringFields, intFields, title,
                           21, 22, 15,
                           showElements, toggleElements,
                           0,
                           elementTypeDatabase, elementDatabase, severity,
                           ref showOnElement);
        }


        //-------------------------------------------
        // Drop-down list
        //-------------------------------------------
        public static void DrawItemDropdown(List<IntField> intFields, string title, int itemID, HeroObject itemDatabase)
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            DropDownValues itemList = HeroKitCommon.databaseList(itemDatabase);
            SimpleLayout.Label(title + ":");
            intFields[itemID].value = SimpleLayout.DropDownList(intFields[itemID].value, itemList, 0, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));
            SimpleLayout.EndVertical();
        }

        //-------------------------------------------
        // Name, Icon, Description Group
        //-------------------------------------------
        public static void BasicFieldsA(List<StringField> stringFields, List<UnityObjectField> uoFields, int nameID, int descID, int iconID)
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // name field
            SimpleLayout.Label("Name" + ":");
            stringFields[nameID].value = SimpleLayout.TextField(stringFields[nameID].value, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));

            // icon field
            SimpleLayout.Label("Icon" + ":");
            uoFields[iconID].value = SimpleLayout.ObjectField(uoFields[iconID].value as Sprite, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));

            // description field
            SimpleLayout.Label("Description" + ":");
            stringFields[descID].value = SimpleLayout.TextArea(stringFields[descID].value, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450), 50);

            SimpleLayout.EndVertical();
        }

        //-------------------------------------------
        // Name, Description Group
        //-------------------------------------------
        public static void BasicFieldsB(List<StringField> stringFields, int nameID, int descID)
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // name field
            SimpleLayout.Label("Name" + ":");
            stringFields[nameID].value = SimpleLayout.TextField(stringFields[nameID].value, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));

            // description field
            SimpleLayout.Label("Description" + ":");
            stringFields[descID].value = SimpleLayout.TextArea(stringFields[descID].value, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450), 50);

            SimpleLayout.EndVertical();
        }

        //-------------------------------------------
        // In-Line Actions Editor
        //-------------------------------------------
        public static void DrawActions(HeroObject heroObject, List<HeroAction> heroActions, 
                                        UnityAction addItem, UnityAction<int> showBlockContent, 
                                        UnityAction<int> showContextMenu)
        {
            List<HeroProperties> items = heroObject.propertiesList.properties;
            int indent = 0;

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
            intFields_att[6].value = SimpleLayout.IntField(intFields_att[6].value, 100);
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
        #endregion

        #region create, resize, add, remove bitarrays, intarrays
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
        // generate a list of items of a specific type in bit array (checkbox, name)
        public static void getOnInBitarray(BitArray bitArray, DropDownValues items, int itemType, 
                                           HeroObject database, int itemTypeSlot, int show)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (database.propertiesList.properties[i].itemProperties.ints.items[itemTypeSlot].value == itemType)
                {
                    // draw all
                    if (show <= 1)
                    {
                        drawBitarrayField(bitArray, i, items);
                    }
                    // draw selected
                    if (show == 2 && bitArray[i])
                    {
                        drawBitarrayField(bitArray, i, items);
                    }
                    // draw un-selected
                    if (show == 3 && !bitArray[i])
                    {
                        drawBitarrayField(bitArray, i, items);
                    }
                }
            }
        }
        private static void drawBitarrayField(BitArray bitArray, int bitID, DropDownValues items)
        {
            SimpleLayout.BeginHorizontal();
            bitArray[bitID] = SimpleLayout.BoolField(bitArray[bitID]);
            SimpleLayout.Label(items.items[bitID]);
            SimpleLayout.EndHorizontal();
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
        #endregion

        #region work with combo arrays
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
        // generate a list of items in bit array (checkbox, name, drop down list)
        public static void getDropdownComboarray(BitArray bitArray, int[] intArray, DropDownValues items, DropDownValues listItems)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    SimpleLayout.BeginHorizontal();
                    bitArray[i] = SimpleLayout.BoolField(bitArray[i]);
                    SimpleLayout.Space(4);
                    intArray[i] = SimpleLayout.DropDownList(intArray[i], listItems, 0, 200);
                    SimpleLayout.Label(items.items[i]);
                    SimpleLayout.Space();
                    SimpleLayout.EndHorizontal();
                }
            }
        }
        // generate a list of items in bit array (checkbox, item name, int field, drop-down list
        public static void getDropdownComboarrayB(BitArray bitArray, int[] intArray, int[] choiceArray, 
                                                  DropDownValues items, DropDownValues listItems, string intFieldLabel)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    SimpleLayout.BeginHorizontal();
                    bitArray[i] = SimpleLayout.BoolField(bitArray[i]);
                    SimpleLayout.Space(4);
                    intArray[i] = SimpleLayout.IntField(intArray[i]);
                    if (intFieldLabel != "") SimpleLayout.Label(intFieldLabel);
                    SimpleLayout.Space(4);
                    choiceArray[i] = SimpleLayout.DropDownList(choiceArray[i], listItems, 0, 100);
                    SimpleLayout.Label(items.items[i]);
                    SimpleLayout.Space();
                    SimpleLayout.EndHorizontal();
                }
            }
        }
        // generate a list of items in bit array (checkbox, item name)
        public static void getDropdownComboarrayC(BitArray bitArray, DropDownValues items)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    SimpleLayout.BeginHorizontal();
                    bitArray[i] = SimpleLayout.BoolField(bitArray[i]);
                    SimpleLayout.Space(4);
                    SimpleLayout.Label(items.items[i]);
                    SimpleLayout.Space();
                    SimpleLayout.EndHorizontal();
                }
            }
        }
        // generate a list of items in bit array (checkbox, item name, drop-down list, drop-down list
        public static void getDropdownComboarrayD(BitArray bitArray, int[] choiceArrayA, int[] choiceArrayB, DropDownValues items, DropDownValues choiceItemsA, DropDownValues choiceItemsB)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    SimpleLayout.BeginHorizontal();
                    bitArray[i] = SimpleLayout.BoolField(bitArray[i]);
                    SimpleLayout.Space(4);
                    choiceArrayA[i] = SimpleLayout.DropDownList(choiceArrayA[i], choiceItemsA, 0, 100);
                    SimpleLayout.Space(4);
                    choiceArrayB[i] = SimpleLayout.DropDownList(choiceArrayB[i], choiceItemsB, 0, 100);
                    SimpleLayout.Label(items.items[i]);
                    SimpleLayout.Space();
                    SimpleLayout.EndHorizontal();
                }
            }
        }
        // generate a list of items in bit array (checkbox, name, drop down list) (specific item type)
        public static void getDropdownComboarrayE(BitArray bitArray, int[] intArray, DropDownValues itemsList, DropDownValues selectionList, 
                                                  HeroObject database, int itemType, int itemTypeSlot, int show)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (database.propertiesList.properties[i].itemProperties.ints.items[itemTypeSlot].value == itemType)
                {
                    // draw all
                    if (show <= 1)
                    {
                        drawDropdownComboEField(bitArray, intArray, i, itemsList, selectionList);
                    }
                    // draw selected
                    if (show == 2 && bitArray[i])
                    {
                        drawDropdownComboEField(bitArray, intArray, i, itemsList, selectionList);
                    }
                    // draw un-selected
                    if (show == 3 && !bitArray[i])
                    {
                        drawDropdownComboEField(bitArray, intArray, i, itemsList, selectionList);
                    }
                }
            }
        }
        private static void drawDropdownComboEField(BitArray bitArray, int[] intArray, int i, DropDownValues itemsList, DropDownValues selectionList)
        {
            SimpleLayout.BeginHorizontal();
            bitArray[i] = SimpleLayout.BoolField(bitArray[i]);
            SimpleLayout.Space(4);
            intArray[i] = SimpleLayout.DropDownList(intArray[i], selectionList, 0, 200);
            SimpleLayout.Label(itemsList.items[i]);
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
        }
        #endregion

        //-------------------------------------------
        // Exapand / Collapse field groups
        //-------------------------------------------
        #region expand / collapse field groups
        /// <summary>
        /// Draw when item can be used
        /// </summary>
        private static bool showStats = false;
        private static void toggleStats()
        {
            showStats = !showStats;
        }
        private static bool showStats2 = false;
        private static void toggleStats2()
        {
            showStats2 = !showStats2;
        }

        private static bool showMeters = false;
        private static void toggleMeters()
        {
            showMeters = !showMeters;
        }
        private static bool showMeters2 = false;
        private static void toggleMeters2()
        {
            showMeters2 = !showMeters2;
        }

        private static bool showElements = false;
        private static void toggleElements()
        {
            showElements = !showElements;
        }
        private static int showOnElement = 0;

        private static bool showConditions = false;
        private static void toggleConditions()
        {
            showConditions = !showConditions;
        }
        private static int showOnCondition = 0;

        private static bool showMoney = false;
        private static void toggleMoney()
        {
            showMoney = !showMoney;
        }

        private static bool showAffix = false;
        private static void toggleAffix()
        {
            showAffix = !showAffix;
        }

        private static bool showSockets = false;
        private static void toggleSockets()
        {
            showSockets = !showSockets;
        }

        private static bool showWeapons = false;
        private static void toggleWeaponType()
        {
            showWeapons = !showWeapons;
        }
        private static int showOnWeapons = 0;

        private static bool showArmor = false;
        private static void toggleArmorType()
        {
            showArmor = !showArmor;
        }
        private static int showOnArmor = 0;

        private static bool showAbility = false;
        private static void toggleAbility()
        {
            showAbility = !showAbility;
        }
        private static int showOnAbility = 0;

        private static bool showAlignment = false;
        private static void toggleAlignment()
        {
            showAlignment = !showAlignment;
        }
        #endregion
    }
}