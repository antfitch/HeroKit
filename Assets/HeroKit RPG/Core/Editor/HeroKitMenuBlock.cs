// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEditor;
using HeroKit.Scene;
using UnityEngine;

namespace HeroKit.RpgEditor
{
    /// <summary>
    /// Main Menu for the Hero Kit Editor. (Hero Object)
    /// </summary>
    internal class HeroKitMenuBlock : EditorWindow
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The hero object.
        /// </summary>
        private static HeroObject heroObject;
        /// <summary>
        /// The hero kit editor.
        /// </summary>
        private static HeroKitEditor heroEditor;

        /// <summary>
        /// Item in focus.
        /// </summary>
        public static bool itemFocus = false;
        /// <summary>
        /// Settings menu is in focus.
        /// </summary>
        public static bool settingsFocus = false;

        /// <summary>
        /// ID of the currently selected item in the main menu.
        /// </summary>
        public static int itemID = 0;

        /// <summary>
        /// The item ID of whatever object was clicked to display the context menu.
        /// If there is no ID, this value is -1.
        /// </summary>
        public static int itemIndexContext = -1;

        /// <summary>
        /// A character that looks like a long dash. This goes in front of things in the menu that cannot be expanded.
        /// </summary>
        public static string textIcon = '\u2014'.ToString();
        /// <summary>
        /// Indent level for things in the menu that need to be indented.
        /// </summary>
        public static int indentLevel = 18;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Block in which to display the main menu. 
        /// </summary>
        /// <param name="heroKitObject">Hero object info to display in the menu.</param>
        /// <param name="heroKitEditor">Hero kit editor.</param>
        public static void Block(HeroObject heroKitObject, HeroKitEditor heroKitEditor)
        {
            // exit early if object is null
            if (heroKitObject == null)
            {
                return;
            }

            // assign hero object to this class
            heroObject = heroKitObject;

            // save the editor
            heroEditor = heroKitEditor;

            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            // draw the items list
            if (HeroKitEditor.typeID == 1)
                ItemMenuBlock.Block(heroObject);
            // draw the affixes list
            else if (HeroKitEditor.typeID == 2)
                AffixesMenuBlock.Block(heroObject);
            // draw the affix type list
            else if (HeroKitEditor.typeID == 3)
                AffixesTypeMenuBlock.Block(heroObject);
            // draw the stats list
            else if (HeroKitEditor.typeID == 4)
                StatsMenuBlock.Block(heroObject);
            // draw the meter stats list
            else if (HeroKitEditor.typeID == 5)
                MeterMenuBlock.Block(heroObject);
            // draw the meter stats list
            else if (HeroKitEditor.typeID == 6)
                MoneyMenuBlock.Block(heroObject);
            // draw the item type list
            else if (HeroKitEditor.typeID == 7)
                ItemTypeMenuBlock.Block(heroObject);
            // draw the elements list
            else if (HeroKitEditor.typeID == 8)
                ElementMenuBlock.Block(heroObject);
            // draw the conditions list
            else if (HeroKitEditor.typeID == 9)
                ConditionMenuBlock.Block(heroObject);
            // draw the weapon type list
            else if (HeroKitEditor.typeID == 10)
                WeaponTypeMenuBlock.Block(heroObject);
            // draw the weapon list
            else if (HeroKitEditor.typeID == 11)
                WeaponMenuBlock.Block(heroObject);
            // draw the armor type list
            else if (HeroKitEditor.typeID == 12)
                ArmorTypeMenuBlock.Block(heroObject);
            // draw the armor list
            else if (HeroKitEditor.typeID == 13)
                ArmorMenuBlock.Block(heroObject);
            // draw the ammunition type list
            else if (HeroKitEditor.typeID == 14)
                AmmunitionTypeMenuBlock.Block(heroObject);
            // draw the ammunition list
            else if (HeroKitEditor.typeID == 15)
                AmmunitionMenuBlock.Block(heroObject);
            // draw the ability type list
            else if (HeroKitEditor.typeID == 16)
                AbilityTypeMenuBlock.Block(heroObject);
            // draw the ability list
            else if (HeroKitEditor.typeID == 17)
                AbilityMenuBlock.Block(heroObject);
            // draw the ability list
            else if (HeroKitEditor.typeID == 18)
                FormulaMenuBlock.Block(heroObject);
            // draw the class list
            else if (HeroKitEditor.typeID == 19)
                ClassMenuBlock.Block(heroObject);
            // draw the element type list
            else if (HeroKitEditor.typeID == 20)
                ElementTypeMenuBlock.Block(heroObject);
            // draw the condition type list
            else if (HeroKitEditor.typeID == 21)
                ConditionTypeMenuBlock.Block(heroObject);
            // draw the subclass list
            else if (HeroKitEditor.typeID == 22)
                SubclassMenuBlock.Block(heroObject);
            // draw the race list
            else if (HeroKitEditor.typeID == 23)
                RaceMenuBlock.Block(heroObject);
            // draw the subrace list
            else if (HeroKitEditor.typeID == 24)
                SubraceMenuBlock.Block(heroObject);
            // draw the alignment list
            else if (HeroKitEditor.typeID == 25)
                AlignmentMenuBlock.Block(heroObject);
        }

        // --------------------------------------------------------------
        // Methods (On Click)
        // --------------------------------------------------------------

        /// <summary>
        /// Change the selection in the menu.
        /// </summary>
        public static void changeSelection()
        {
            switch (Event.current.keyCode)
            {
                case KeyCode.DownArrow:
                    goDown();
                    break;

                case KeyCode.UpArrow:
                    goUp();
                    break;
            }

            // refresh the editor
            heroEditor.Repaint();
        }
        /// <summary>
        /// Go down to state X using arrow keys.
        /// </summary>
        private static void goDown()
        {
            int count = heroObject.propertiesList.properties.Count;

            // go to next state in the list
            if (itemID < count - 1)
            {
                itemID++;
            }
        }
        /// <summary>
        /// Go up to state X using arrow keys.
        /// </summary>
        private static void goUp()
        {
            if (itemID > 0)
               itemID--;
        }
    }
}