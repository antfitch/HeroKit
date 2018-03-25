// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;
using System.Collections.Generic;

namespace HeroKit.RpgEditor
{
    /// <summary>
    /// Block for Hero Properties that appears in Hero Kit Editor.
    /// </summary>
    internal static class ConditionBlock
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
        private static string blockName = "Element";
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

        private static HeroProperties attributeBlock;
        private static List<StringField> stringFields_att;
        private static List<IntField> intFields_att;
        private static List<FloatField> floatFields_att;
        private static List<BoolField> boolFields_att;
        private static List<UnityObjectField> uoFields_att;

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
            attributeBlock = HeroKitCommon.conditionDatabase_attributes.propertiesList.properties[itemIndex];

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
            HeroKitCommon.DrawItemDropdown(intFields, "Condition Type", 3, HeroKitCommon.conditionTypeDatabase);
            HeroKitCommon.BasicFieldsB(stringFields, 0, 1);
            SimpleLayout.Line();
            DrawCantAttack();
            DrawCantBeAttacked();
            DrawCantEvade();
            SimpleLayout.Line();
            HeroKitCommon.DrawMeterValue_Conditions(stringFields_att, intFields_att);
            HeroKitCommon.DrawStatValue_Conditions(stringFields_att, intFields_att);
            HeroKitCommon.DrawElementValue(stringFields_att, intFields_att, "Change effect of elements on the character");
            HeroKitCommon.DrawConditionsValue(stringFields_att, intFields_att, "Change other conditions");
            SimpleLayout.Line();
            DrawTimerEndCond();
            DrawAttackEndCond();
        }

        private static void DrawTimerEndCond()
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            SimpleLayout.BeginHorizontal();
            boolFields[0].value = SimpleLayout.BoolField(boolFields[0].value);
            SimpleLayout.Label("Turn off condition with timer");
            SimpleLayout.EndHorizontal();

            if (boolFields[0].value)
            {
                SimpleLayout.Line();

                // list of stat change types
                DropDownValues changeTypeValues = new DropDownValues();
                string[] changeType = { "Seconds", "Minutes", "Hours"};
                changeTypeValues.setValues("", changeType);

                SimpleLayout.BeginHorizontal();
                intFields[0].value = SimpleLayout.DropDownList(intFields[0].value, changeTypeValues, 0, 150);
                intFields[1].value = SimpleLayout.IntField(intFields[1].value, 150);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
            }


            SimpleLayout.EndVertical();
        }
        private static void DrawAttackEndCond()
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            SimpleLayout.BeginHorizontal();
            boolFields[1].value = SimpleLayout.BoolField(boolFields[1].value);
            SimpleLayout.Label("Turn off condition when character is attacked");
            SimpleLayout.EndHorizontal();

            if (boolFields[1].value)
            {
                SimpleLayout.Line();
                SimpleLayout.BeginHorizontal();
                SimpleLayout.Label("Chance:");
                intFields[1].value = SimpleLayout.IntField(intFields[1].value, 150);
                SimpleLayout.Label("%");
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
            }


            SimpleLayout.EndVertical();
        }
        private static void DrawCantAttack()
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            SimpleLayout.BeginHorizontal();
            boolFields[2].value = SimpleLayout.BoolField(boolFields[2].value);
            SimpleLayout.Label("Character can't attack");
            SimpleLayout.EndHorizontal();

            SimpleLayout.EndVertical();
        }
        private static void DrawCantEvade()
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            SimpleLayout.BeginHorizontal();
            boolFields[3].value = SimpleLayout.BoolField(boolFields[3].value);
            SimpleLayout.Label("Character can't evade attack");
            SimpleLayout.EndHorizontal();

            SimpleLayout.EndVertical();
        }
        private static void DrawCantBeAttacked()
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            SimpleLayout.BeginHorizontal();
            boolFields[4].value = SimpleLayout.BoolField(boolFields[4].value);
            SimpleLayout.Label("Character can't be attacked");
            SimpleLayout.EndHorizontal();

            SimpleLayout.EndVertical();
        }
    }
}