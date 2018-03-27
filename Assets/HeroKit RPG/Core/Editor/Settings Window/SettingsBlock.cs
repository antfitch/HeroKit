// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;
using System.IO;

namespace HeroKit.RpgEditor
{
    /// <summary>
    /// Block for HeroKit Settings that appears in Hero Kit Editor.
    /// </summary>
    internal static class SettingsBlock
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// Name of the block.
        /// </summary>
        private static string blockName = "Main Menu";

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Block to display on the canvas.
        /// </summary>
        /// <param name="heroKitAction">Hero kit action to display in the block.</param>
        public static void Block()
        {
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
        /// Draw the body of the block.
        /// </summary>
        private static void DrawBody()
        {
            int bsize = 140;

            SimpleLayout.Space(22);
            SimpleLayout.BeginHorizontal();
            SimpleLayout.BeginVertical(Box.StyleA);


            // -----------------------------------


            SimpleLayout.BeginHorizontal();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Item Types", OpenItemTypeDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Items", OpenItemsDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Affix Types", OpenAffixTypeDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Affixes", OpenAffixDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Stats", OpenStatsDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Meter Stats", OpenMeterDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Currency Types", OpenMoneyDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Formulas", OpenFormulaDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.EndHorizontal();


            // -----------------------------------


            SimpleLayout.BeginHorizontal();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Element Types", OpenElementTypeDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Elements", OpenElementsDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Condition Types", OpenConditionTypeDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Conditions", OpenConditionDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Weapon Types", OpenWeaponTypeDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Weapons", OpenWeaponDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Armor Types", OpenArmorTypeDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Armor", OpenArmorDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.EndHorizontal();


            // -----------------------------------


            SimpleLayout.BeginHorizontal();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Ammunition Types", OpenAmmunitionTypeDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Ammunition", OpenAmmunitionDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Ability Types", OpenAbilityTypeDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Abilities", OpenAbilityDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Classes", OpenClassDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Subclasses", OpenSubclassDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Race", OpenRaceDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Subrace", OpenSubraceDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            SimpleLayout.EndHorizontal();


            // -----------------------------------

            // -----------------------------------


            SimpleLayout.BeginHorizontal();

            SimpleLayout.BeginVertical();
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Alignments", OpenAlignmentDB, Button.StyleBig, bsize);
            SimpleLayout.EndVertical();
            //SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            //SimpleLayout.Button("Alignments", OpenAmmunitionDB, Button.StyleBig, bsize);
            //SimpleLayout.EndVertical();
            SimpleLayout.EndVertical();

            //SimpleLayout.BeginVertical();
            //SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            //SimpleLayout.Button("Ability Types", OpenAbilityTypeDB, Button.StyleBig, bsize);
            //SimpleLayout.EndVertical();
            //SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            //SimpleLayout.Button("Abilities", OpenAbilityDB, Button.StyleBig, bsize);
            //SimpleLayout.EndVertical();
            //SimpleLayout.EndVertical();

            //SimpleLayout.BeginVertical();
            //SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            //SimpleLayout.Button("Classes", OpenClassDB, Button.StyleBig, bsize);
            //SimpleLayout.EndVertical();
            //SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            //SimpleLayout.Button("Subclasses", OpenSubclassDB, Button.StyleBig, bsize);
            //SimpleLayout.EndVertical();
            //SimpleLayout.EndVertical();

            //SimpleLayout.BeginVertical();
            //SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            //SimpleLayout.Button("Race", OpenRaceDB, Button.StyleBig, bsize);
            //SimpleLayout.EndVertical();
            //SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            //SimpleLayout.Button("Subrace", OpenSubraceDB, Button.StyleBig, bsize);
            //SimpleLayout.EndVertical();
            //SimpleLayout.EndVertical();

            SimpleLayout.EndHorizontal();


            // -----------------------------------



            SimpleLayout.EndHorizontal();
            SimpleLayout.Space();
            SimpleLayout.EndVertical();
        }

        // --------------------------------------------------------------
        // Methods (Misc)
        // --------------------------------------------------------------

        public static void OpenItemsDB()
        {
            // get HeroKit RPG Items
            HeroKitEditor.heroObject = HeroKitCommon.itemDatabase;
            HeroKitEditor.typeID = 1;
        }

        public static void OpenAffixDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.affixDatabase;
            HeroKitEditor.typeID = 2;
        }

        public static void OpenAffixTypeDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.affixTypeDatabase;
            HeroKitEditor.typeID = 3;
        }

        public static void OpenStatsDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.statsDatabase;
            HeroKitEditor.typeID = 4;
        }

        public static void OpenMeterDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.meterDatabase;
            HeroKitEditor.typeID = 5;
        }

        public static void OpenMoneyDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.moneyDatabase;
            HeroKitEditor.typeID = 6;
        }

        public static void OpenItemTypeDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.itemTypeDatabase;
            HeroKitEditor.typeID = 7;
        }

        public static void OpenElementsDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.elementDatabase;
            HeroKitEditor.typeID = 8;
        }

        public static void OpenConditionDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.conditionDatabase;
            HeroKitEditor.typeID = 9;
        }

        public static void OpenWeaponTypeDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.weaponTypeDatabase;
            HeroKitEditor.typeID = 10;
        }

        public static void OpenWeaponDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.weaponDatabase;
            HeroKitEditor.typeID = 11;
        }

        public static void OpenArmorTypeDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.armorTypeDatabase;
            HeroKitEditor.typeID = 12;
        }

        public static void OpenArmorDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.armorDatabase;
            HeroKitEditor.typeID = 13;
        }

        public static void OpenAmmunitionTypeDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.ammunitionTypeDatabase;
            HeroKitEditor.typeID = 14;
        }

        public static void OpenAmmunitionDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.ammunitionDatabase;
            HeroKitEditor.typeID = 15;
        }

        public static void OpenAbilityTypeDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.abilityTypeDatabase;
            HeroKitEditor.typeID = 16;
        }

        public static void OpenAbilityDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.abilityDatabase;
            HeroKitEditor.typeID = 17;
        }

        public static void OpenFormulaDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.formulaDatabase;
            HeroKitEditor.typeID = 18;
        }

        public static void OpenClassDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.classDatabase;
            HeroKitEditor.typeID = 19;
        }

        public static void OpenElementTypeDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.elementTypeDatabase;
            HeroKitEditor.typeID = 20;
        }

        public static void OpenConditionTypeDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.conditionTypeDatabase;
            HeroKitEditor.typeID = 21;
        }

        public static void OpenSubclassDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.subclassDatabase;
            HeroKitEditor.typeID = 22;
        }

        public static void OpenRaceDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.raceDatabase;
            HeroKitEditor.typeID = 23;
        }

        public static void OpenSubraceDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.subraceDatabase;
            HeroKitEditor.typeID = 24;
        }

        public static void OpenAlignmentDB()
        {
            HeroKitEditor.heroObject = HeroKitCommon.alignmentDatabase;
            HeroKitEditor.typeID = 25;
        }
    }
}