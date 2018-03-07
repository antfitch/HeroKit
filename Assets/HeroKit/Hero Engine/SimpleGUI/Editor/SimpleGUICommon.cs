using UnityEngine;
using HeroKit.Scene;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace SimpleGUI
{
    /// <summary>
    /// Methods that are commonly used with GUI objects.
    /// </summary>
    public static class SimpleGUICommon
    {
        /// <summary>
        /// Is developer using the pro skin (dark) or default skin (light)?
        /// </summary>
        public static bool isProSkin = EditorGUIUtility.isProSkin;

        /// <summary>
        /// Populate a drop-down list field with values from a list of type HeroListObjectField. 
        /// </summary>
        /// <typeparam name="T">The type of HeroList (IntList, BoolList, etc).</typeparam>
        /// <typeparam name="G">The type assigned to the HeroList's HeroListObjectField (int, bool, etc).</typeparam>
        /// <param name="itemList">The list of items in a HeroList.</param>
        /// <param name="abbreviation">The character(s) that should appear in a drop-down list that displays the objects in a HeroList.</param>
        /// <returns>The list of values as a string array.</returns>
        public static string[] PopulateDropDownField<T, G>(List<T> itemList, string abbreviation) where T : HeroListObjectField<G>
        {
            // get count
            int count = itemList.Count;

            // get the items
            string[] items = new string[count];
            for (int i = 0; i < count; i++)
            {
                items[i] = "[" + abbreviation + i + "] " + itemList[i].name;
            }

            // set the field
            return items;
        }

        /// <summary>
        /// Populate a drop-down list field with event values from a state within a hero object. 
        /// </summary>
        /// <param name="itemList">The list of items in a HeroList.</param>
        /// <param name="abbreviation">The character(s) that should appear in a drop-down list that displays the objects in a HeroList.</param>
        /// <returns>The list of values as a string array.</returns>
        public static string[] PopulateEventDropDownField(List<HeroEvent> itemList, string abbreviation)
        {
            // get count
            int count = itemList.Count;

            // get the items
            string[] items = new string[count];
            for (int i = 0; i < count; i++)
            {
                items[i] = "[" + abbreviation + i + "] " + itemList[i].name;
            }

            // set the field
            return items;
        }

        /// <summary>
        /// Populate a drop-down list field with state values from a hero object. 
        /// </summary>
        /// <param name="itemList">The list of items in a HeroList.</param>
        /// <param name="abbreviation">The character(s) that should appear in a drop-down list that displays the objects in a HeroList.</param>
        /// <returns>The list of values as a string array.</returns>
        public static string[] PopulateStateDropDownField(List<HeroState> itemList, string abbreviation)
        {
            // get count
            int count = itemList.Count;

            // get the items
            string[] items = new string[count];
            for (int i = 0; i < count; i++)
            {
                items[i] = "[" + abbreviation + i + "] " + itemList[i].name;
            }

            // set the field
            return items;
        }

        /// <summary>
        /// Populate a drop-down list field with values from a generic list. 
        /// </summary>
        /// <typeparam name="T">The type of list (int, bool, etc).</typeparam>
        /// <param name="itemList">The list of values</param>
        /// <returns>The list of values as a string array.</returns>
        public static string[] PopulateDropDownField<T>(List<T> itemList)
        {
            // get count
            int count = itemList.Count;

            // get the items
            string[] items = new string[count];
            for (int i = 0; i < count; i++)
            {
                items[i] = itemList[i].ToString();
            }

            // set the field
            return items;
        }

        /// <summary>
        /// Get a color with a hexidecimal value.
        /// </summary>
        /// <param name="hexadecimal">The hexidecimal value.</param>
        /// <returns>Color as a Color object.</returns>
        public static Color GetColor(string hexadecimal)
        {
            Color color;
            ColorUtility.TryParseHtmlString(hexadecimal, out color);
            return color;
        }

        /// <summary>
        /// Convert a color to a hexidecimal value.
        /// </summary>
        /// <param name="color">Get a color.</param>
        /// <returns>The hexidecimal value for the color.</returns>
        public static string GetHexFromColor(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }

        /// <summary>
        /// Gets a 2D texture with a hexidecimal value
        /// </summary>
        /// <param name="hexadecimal">The hexidecimal value</param>
        /// <returns>Color as a Texture2D object.</returns>
        public static Texture2D GetTextureFromColor(string hexadecimal)
        {
            Color color = GetColor(hexadecimal);
            var texture = new Texture2D(1, 1, TextureFormat.ARGB32, false, true);
            texture.hideFlags = HideFlags.HideAndDontSave;
            texture.filterMode = FilterMode.Point;
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }

        /// <summary>
        /// Alter brightness of a color (hexidecimal)
        /// </summary>
        /// <param name="hexadecimal">The hexidecimal value for the color.</param>
        /// <param name="changeInBrightness">The change in brighness.</param>
        /// <returns>The updated hexidecimal value with the brightness change.</returns>
        public static string AlterHexBrightness(string hexadecimal, int changeInBrightness)
        {
            string rHex = hexadecimal.Substring(0, 2);
            string gHex = hexadecimal.Substring(2, 2);
            string bHex = hexadecimal.Substring(4, 2);

            int r = Convert.ToInt32(rHex, 16);
            int g = Convert.ToInt32(gHex, 16);
            int b = Convert.ToInt32(bHex, 16);

            r = Math.Max(0, Math.Min(255, r + changeInBrightness));
            g = Math.Max(0, Math.Min(255, g + changeInBrightness));
            b = Math.Max(0, Math.Min(255, b + changeInBrightness));

            string rHexNew = r.ToString("X2");
            string gHexNew = g.ToString("X2");
            string bHexNew = b.ToString("X2");

            return rHexNew + gHexNew + bHexNew;
        }

        // -----------------------------------------------------------------------------------
        // Copyright (c) Rotorz Limited. All rights reserved.
        // Licensed under the MIT license.

        /// <summary>
        /// Converts a string that represents a PNG to a 2D texture.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>String as a Texture2D object.</returns>
        public static Texture2D StringToTexture(string s)
        {
            // Convert string to byte array 
            byte[] imageData = Convert.FromBase64String(s);

            // Get image width
            int imageWidth = GetImageWidth(imageData);

            // Get image height
            int imageHeight = GetImageHeight(imageData);

            // Convert image to texture
            var texture = new Texture2D(imageWidth, imageHeight, TextureFormat.ARGB32, false, true);
            texture.hideFlags = HideFlags.HideAndDontSave;
            texture.name = "";
            texture.filterMode = FilterMode.Point;
            texture.LoadImage(imageData);
            
            // Return the texture
            return texture;
        }

        /// <summary>
        /// Gets the width of an image passed in as a byte array.
        /// </summary>
        /// <param name="imageData">The byte array that represents a PNG image.</param>
        /// <returns>The width of the image.</returns>
        private static int GetImageWidth(byte[] imageData)
        {
            return ReadInt(imageData, 3 + 15);
        }

        /// <summary>
        /// Gets the height of an image passed in as a byte array.
        /// </summary>
        /// <param name="imageData">The byte array that represents a PNG image.</param>
        /// <returns>The height of the image.</returns>
        private static int GetImageHeight(byte[] imageData)
        {
            return ReadInt(imageData, 3 + 15 + 2 + 2);
        }

        /// <summary>
        /// Gets the size of something in a byte array.
        /// </summary>
        /// <param name="imageData">The byte array.</param>
        /// <param name="offset">The offset to ignore.</param>
        /// <returns>The size of something in a byte array.</returns>
        private static int ReadInt(byte[] imageData, int offset)
        {
            return (imageData[offset] << 8) | imageData[offset + 1];
        }
    }
}