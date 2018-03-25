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

namespace HeroKit.RpgEditor
{
    /// <summary>
    /// Block for Hero Properties that appears in Hero Kit Editor.
    /// </summary>
    internal static class AffixesBlock
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
        private static string blockName = "Affix";
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
            attributeBlock = HeroKitCommon.affixDatabase_attributes.propertiesList.properties[itemIndex];

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
            HeroKitCommon.DrawItemDropdown(intFields, "Affix Type", 0, HeroKitCommon.affixTypeDatabase);
            HeroKitCommon.BasicFieldsA(stringFields, uoFields, 0, 1, 0);
            DrawColorField();
            HeroKitCommon.DrawMoneyValue(stringFields_att, intFields_att);

            SimpleLayout.Line();

            HeroKitCommon.DrawMeterValue(stringFields_att, intFields_att);
            //HeroKitCommon.DrawMeters(intFields_att, boolFields_att);
            HeroKitCommon.DrawStatsValue(stringFields_att, intFields_att);
            //HeroKitCommon.DrawStats(intFields_att, boolFields_att);
            HeroKitCommon.DrawExtras(intFields_att);

            SimpleLayout.Line();

            //HeroKitCommon.DrawConditions(intFields_att, boolFields_att);
            HeroKitCommon.DrawConditionsValue(stringFields_att, intFields_att);
            HeroKitCommon.DrawElementValue(stringFields_att, intFields_att, "Attach elements to this affix");
            //HeroKitCommon.DrawElements(intFields_att, boolFields_att, "Attach elements to this affix", false);
        }

        /// <summary>
        /// Draw first group of fields (name, desc, icon, price)
        /// </summary>
        private static void DrawColorField()
        {
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // color field
            SimpleLayout.Label("Color" + ":");
            stringFields[2].value = SimpleLayout.ColorField(stringFields[2].value, HeroKit.Editor.HeroKitCommon.GetWidthForField(60, 450));

            SimpleLayout.EndVertical();
        }
    }
}