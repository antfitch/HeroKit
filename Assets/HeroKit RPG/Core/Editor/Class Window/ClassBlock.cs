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
using System.Collections;

namespace HeroKit.RpgEditor
{
    /// <summary>
    /// Block for Hero Properties that appears in Hero Kit Editor.
    /// </summary>
    internal static class ClassBlock
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
        private static string blockName = "Class";
        /// <summary>
        /// The Hero Property.
        /// </summary>
        private static HeroProperties propertyBlock;
        /// <summary>
        /// The ID of the property.
        /// </summary>
        private static int itemIndex;

        private static List<StringField> stringFields;
        private static List<IntField> intFields;
        private static List<FloatField> floatFields;
        private static List<BoolField> boolFields;
        private static List<UnityObjectField> uoFields;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

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

            // save the fields
            stringFields = propertyBlock.itemProperties.strings.items;
            intFields = propertyBlock.itemProperties.ints.items;
            floatFields = propertyBlock.itemProperties.floats.items;
            boolFields = propertyBlock.itemProperties.bools.items;
            uoFields = propertyBlock.itemProperties.unityObjects.items;

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
        private static void DrawBody()
        {
            // draw all fields
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            DrawItemFields();
            SimpleLayout.EndVertical();
        }

        /// <summary>
        /// Draw fields for an item
        /// </summary>
        private static void DrawItemFields()
        {
            DrawBasics();
            DrawWeaponTypes();
            DrawArmorTypes();
            DrawAbilityTypes();
            DrawConditions();
            DrawElements();
            DrawMeters();
        }

        /// <summary>
        /// Draw first group of fields (name, desc, icon, price)
        /// </summary>
        private static void DrawBasics()
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // name field
            SimpleLayout.Label("Name" + ":");
            stringFields[0].value = SimpleLayout.TextField(stringFields[0].value, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));

            // description field
            SimpleLayout.Label("Description" + ":");
            stringFields[1].value = SimpleLayout.TextField(stringFields[1].value, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));

            SimpleLayout.EndVertical();
        }

        
        private static void DrawWeaponTypes()
        {
            // weapon type field
            DropDownValues weaponTypeList = HeroKitCommon.databaseList(HeroKitCommon.weaponTypeDatabase);
            DropDownValues weaponsAll = HeroKitCommon.databaseList(HeroKitCommon.weaponDatabase);

            // resize string if things have changed
            stringFields[2].value = HeroKitCommon.ResizeBitString(stringFields[2].value, weaponsAll.items.Length);

            // convert bitstring into bitarray
            BitArray bitArray = HeroKitCommon.CreateBitArray(stringFields[2].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label("Weapon types allowed for this class");
            SimpleLayout.Space();
            string buttonText = showWeapons ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleWeaponType, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showWeapons)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[0].value = SimpleLayout.DropDownList(intFields[0].value, weaponTypeList, 0, 150);                
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                HeroKitCommon.getOnInBitarray(bitArray, weaponsAll, intFields[0].value, HeroKitCommon.weaponDatabase, 14);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[2].value = HeroKitCommon.CreateBitString(bitArray);
        }
        private static void DrawArmorTypes()
        {
            // weapon type field
            DropDownValues armorTypeList = HeroKitCommon.databaseList(HeroKitCommon.armorTypeDatabase);
            DropDownValues armorAll = HeroKitCommon.databaseList(HeroKitCommon.armorDatabase);

            // resize string if things have changed
            stringFields[3].value = HeroKitCommon.ResizeBitString(stringFields[3].value, armorAll.items.Length);

            // convert bitstring into bitarray
            BitArray bitArray = HeroKitCommon.CreateBitArray(stringFields[3].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label("Armor types allowed for this class");
            SimpleLayout.Space();
            string buttonText = showArmor ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleArmorType, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showArmor)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[2].value = SimpleLayout.DropDownList(intFields[2].value, armorTypeList, 0, 150);                
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                HeroKitCommon.getOnInBitarray(bitArray, armorAll, intFields[2].value, HeroKitCommon.armorDatabase, 14);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[3].value = HeroKitCommon.CreateBitString(bitArray);
        }
        private static void DrawAbilityTypes()
        {
            // weapon type field
            DropDownValues abilityTypeList = HeroKitCommon.databaseList(HeroKitCommon.abilityTypeDatabase);
            DropDownValues abilityAll = HeroKitCommon.databaseList(HeroKitCommon.abilityDatabase);

            // resize string if things have changed
            stringFields[6].value = HeroKitCommon.ResizeBitString(stringFields[6].value, abilityAll.items.Length);

            // convert bitstring into bitarray
            BitArray bitArray = HeroKitCommon.CreateBitArray(stringFields[6].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label("Ability types allowed for this class");
            SimpleLayout.Space();
            string buttonText = showAbility ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleAbility, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showAbility)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[1].value = SimpleLayout.DropDownList(intFields[1].value, abilityTypeList, 0, 150);               
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                HeroKitCommon.getOnInBitarray(bitArray, abilityAll, intFields[1].value, HeroKitCommon.abilityDatabase, 0);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[6].value = HeroKitCommon.CreateBitString(bitArray);
        }

        private static DropDownValues severity = HeroKitCommon.conditionSeverityList();
        private static void DrawConditions()
        {
            DropDownValues conditions = HeroKitCommon.databaseList(HeroKitCommon.conditionDatabase);

            // resize string if things have changed
            stringFields[4].value = HeroKitCommon.ResizeBitString(stringFields[4].value, conditions.items.Length);

            // convert intstring into intarray
            int[] intArray = HeroKitCommon.CreateIntArray(stringFields[4].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label("Conditions that affect this class");
            SimpleLayout.Space();
            string buttonText = showConditions ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleConditions, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showConditions)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                
                // weapons list (weapons of one type only)
                intFields[4].value = SimpleLayout.DropDownList(intFields[4].value, conditions, 0, 150);
                // show [add] [remove] after weapons
                HeroKitCommon.addRemoveButton(intArray, intFields[4].value, conditions);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                HeroKitCommon.getOnInIntarray(intArray, conditions, severity);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[4].value = HeroKitCommon.CreateIntString(intArray);
        }
        private static void DrawElements()
        {
            DropDownValues elements = HeroKitCommon.databaseList(HeroKitCommon.elementDatabase);

            // resize string if things have changed
            stringFields[5].value = HeroKitCommon.ResizeBitString(stringFields[5].value, elements.items.Length);

            // convert intstring into intarray
            int[] intArray = HeroKitCommon.CreateIntArray(stringFields[5].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label("Elements that affect this class");
            SimpleLayout.Space();
            string buttonText = showElements ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleElements, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showElements)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[5].value = SimpleLayout.DropDownList(intFields[5].value, elements, 0, 150);
                HeroKitCommon.addRemoveButton(intArray, intFields[5].value, elements);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                HeroKitCommon.getOnInIntarray(intArray, elements, severity);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[5].value = HeroKitCommon.CreateIntString(intArray);
        }
        private static void DrawMeters()
        {
            DropDownValues meters = HeroKitCommon.databaseList(HeroKitCommon.meterDatabase);

            // resize string if things have changed
            stringFields[7].value = HeroKitCommon.ResizeBitString(stringFields[7].value, meters.items.Length);
            stringFields[8].value = HeroKitCommon.ResizeBitString(stringFields[8].value, meters.items.Length);

            // convert intstring into intarray
            BitArray bitArray = HeroKitCommon.CreateBitArray(stringFields[7].value);
            int[] intArray = HeroKitCommon.CreateIntArrayLarge(stringFields[8].value);

            // -------------------------------------
            // Draw form fields
            // -------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // Line 1: types allowed... [hide or show]
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label("Adjust max value of meters by amount entered");
            SimpleLayout.Space();
            string buttonText = showMeters ? "hide" : "show";
            SimpleLayout.Button(buttonText, toggleMeters, Button.StyleA);
            SimpleLayout.EndHorizontal();

            if (showMeters)
            {
                SimpleLayout.Line();

                // -------------------------------------
                // Create the add item drop down box
                // -------------------------------------
                SimpleLayout.BeginHorizontal();
                intFields[3].value = SimpleLayout.DropDownList(intFields[3].value, meters, 0, 150);
                HeroKitCommon.addRemoveButton(bitArray, intFields[3].value, meters);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // -------------------------------------
                // Create the items list
                // -------------------------------------
                SimpleLayout.Line();
                HeroKitCommon.getOnInComboarray(bitArray, intArray, meters);
            }

            SimpleLayout.EndVertical();

            // create bit string from bit array & save
            stringFields[7].value = HeroKitCommon.CreateBitString(bitArray);
            stringFields[8].value = HeroKitCommon.CreateIntStringLarge(intArray);
        }

        private static bool showWeapons = false;
        private static void toggleWeaponType()
        {
            showWeapons = !showWeapons;
        }

        private static bool showArmor = false;
        private static void toggleArmorType()
        {
            showArmor = !showArmor;
        }

        private static bool showAbility = false;
        private static void toggleAbility()
        {
            showAbility = !showAbility;
        }

        private static bool showConditions = false;
        private static void toggleConditions()
        {
            showConditions = !showConditions;
        }

        private static bool showElements = false;
        private static void toggleElements()
        {
            showElements = !showElements;
        }

        private static bool showMeters = false;
        private static void toggleMeters()
        {
            showMeters = !showMeters;
        }
    }
}