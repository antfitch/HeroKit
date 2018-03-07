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

namespace HeroKit.Editor
{
    /// <summary>
    /// Block for Hero Properties that appears in Hero Kit Editor.
    /// </summary>
    internal static class PropertiesBlock
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
        private static string blockName = "Property";
        /// <summary>
        /// The Hero Property.
        /// </summary>
        private static HeroProperties propertyBlock;
        /// <summary>
        /// The ID of the property.
        /// </summary>
        private static int propertyIndex;

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
            propertyIndex = indexProperty;
            propertyBlock = heroObject.propertiesList.properties[propertyIndex];

            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            DrawHeader();
            DrawBody();
        }
        /// <summary>
        /// Draw the header of the block.
        /// </summary>
        private static void DrawHeader()
        {
            string title = (propertyBlock.propertyTemplate != null) ? propertyBlock.propertyTemplate.name : "[None]";
            HeroKitCommon.DrawBlockTitle(blockName + " " + propertyIndex + ": " + title);
        }
        /// <summary>
        /// Draw the body of the block.
        /// </summary>
        private static void DrawBody()
        {
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);

            DrawItemType();

            // display the inspector fields in the editor
            if (propertyBlock.propertyTemplate != null)
            {
                HeroKitCommon.BuildPropertyFields(heroObject, propertyIndex);
                DrawStrings();
                DrawInts();
                DrawFloats();
                DrawBools();  
                DrawHeroObjects();
                DrawUnityObjects();
                DrawGameObjects();
            }

            SimpleLayout.EndVertical();
        }

        // --------------------------------------------------------------
        // Methods (Fields)
        // --------------------------------------------------------------

        /// <summary>
        /// A field for a hero property file.
        /// </summary>
        private static void DrawItemType()
        {
            SimpleLayout.BeginHorizontal();
            SimpleLayout.Label("Item Type:");
            SimpleLayout.Space(13);
            propertyBlock.propertyTemplate = SimpleLayout.ObjectField(propertyBlock.propertyTemplate, HeroKitCommon.GetWidthForField(120));
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            // SHOW FIELDS FOR SPECIFIC HERO PROPERTY
            // if hero property has been removed, sanitize properties list
            if (propertyBlock.propertyTemplate != null)
            {
                SimpleLayout.Space(5);
                SimpleLayout.Line();
            }
            else
            {
                propertyBlock.itemProperties = new HeroList();
            }
        }
        /// <summary>
        /// Fields for the hero property (strings).
        /// </summary>
        private static void DrawStrings()
        {
            List<StringField> items = propertyBlock.itemProperties.strings.items;

            // exit early if there are no values
            if (items == null || items.Count == 0)
                return;

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            for (int i = 0; i < items.Count; i++)
            {
                SimpleLayout.Label(items[i].name + ":");
                if (!items[i].useTextField)
                    items[i].value = SimpleLayout.TextField(items[i].value, HeroKitCommon.GetWidthForField(60, 150));
                else
                    items[i].value = SimpleLayout.TextArea(items[i].value, HeroKitCommon.GetWidthForField(60, 150));
            }
            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Fields for the hero property (ints).
        /// </summary>
        private static void DrawInts()
        {
            List<IntField> items = propertyBlock.itemProperties.ints.items;

            // exit early if there are no values
            if (items == null || items.Count == 0)
                return;

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            for (int i = 0; i < items.Count; i++)
            {
                SimpleLayout.Label(items[i].name + ":");
                items[i].value = SimpleLayout.IntField(items[i].value, HeroKitCommon.GetWidthForField(60, 150));
            }
            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Fields for the hero property (floats).
        /// </summary>
        private static void DrawFloats()
        {
            List<FloatField> items = propertyBlock.itemProperties.floats.items;

            // exit early if there are no values
            if (items == null || items.Count == 0)
                return;

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            for (int i = 0; i < items.Count; i++)
            {
                SimpleLayout.Label(items[i].name + ":");
                items[i].value = SimpleLayout.FloatField(items[i].value, HeroKitCommon.GetWidthForField(60, 150));
            }
            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Fields for the hero property (bools).
        /// </summary>
        private static void DrawBools()
        {
            List<BoolField> items = propertyBlock.itemProperties.bools.items;

            // exit early if there are no values
            if (items == null || items.Count == 0)
                return;

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            for (int i = 0; i < items.Count; i++)
            {
                SimpleLayout.Label(items[i].name + ":");
                items[i].value = SimpleLayout.BoolField(items[i].value);
            }
            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Fields for the hero property (game objects).
        /// </summary>
        private static void DrawGameObjects()
        {
            List<GameObjectField> items = propertyBlock.itemProperties.gameObjects.items;

            // exit early if there are no values
            if (items == null || items.Count == 0)
                return;

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            for (int i = 0; i < items.Count; i++)
            {
                SimpleLayout.Label(items[i].name + " (read only)");
            }
            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Fields for the hero property (hero objects).
        /// </summary>
        private static void DrawHeroObjects()
        {
            List<HeroObjectField> items = propertyBlock.itemProperties.heroObjects.items;

            // exit early if there are no values
            if (items == null || items.Count == 0)
                return;

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            for (int i = 0; i < items.Count; i++)
            {
                SimpleLayout.Label(items[i].name + ":");
                items[i].value = SimpleLayout.ObjectField(items[i].value, HeroKitCommon.GetWidthForField(60, 150));
            }
            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Fields for the hero property (unity objects).
        /// </summary>
        private static void DrawUnityObjects()
        {
            List<UnityObjectField> items = propertyBlock.itemProperties.unityObjects.items;

            // exit early if there are no values
            if (items == null || items.Count == 0)
                return;

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            for (int i = 0; i < items.Count; i++)
            {
                SimpleLayout.Label(items[i].name + ":");

                switch (items[i].objectType)
                {
                    case 1: // audio
                        items[i].value = SimpleLayout.ObjectField(items[i].value as AudioClip, HeroKitCommon.GetWidthForField(60, 150));
                        break;
                    case 2: // sprite
                        items[i].value = SimpleLayout.ObjectField(items[i].value as Sprite, HeroKitCommon.GetWidthForField(60, 150));
                        break;
                    case 3: // scene
                        items[i].value = SimpleLayout.ObjectField(items[i].value as SceneAsset, HeroKitCommon.GetWidthForField(60, 150));
                        if (items[i].value != null)
                        {
                            // add the scene to the editor build settings if it doesn't already exist there.
                            HeroKitCommon.AddSceneToBuildSettings(items[i].value as SceneAsset);
                        }
                        break;
                    case 4: // particle system
                        items[i].value = SimpleLayout.ObjectField(items[i].value as ParticleSystem, HeroKitCommon.GetWidthForField(60, 150));
                        break;
                    case 5: // script
                        items[i].value = SimpleLayout.ObjectField(items[i].value as MonoScript, HeroKitCommon.GetWidthForField(60, 150));
                        break;
                }
            }
            SimpleLayout.EndVertical();
        }
    }
}