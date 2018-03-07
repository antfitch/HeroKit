using UnityEditor;
using UnityEngine;
using HeroKit.Scene;
using System.Collections.Generic;

namespace HeroKit
{
    /// <summary>
    /// Icon for hero object that appears in the hierarchy window.
    /// This class is initialized when the Unity Engine loads. 
    /// </summary>
    [InitializeOnLoad]
    internal class HierarchyIcon
    {
        /// <summary>
        /// Texture for the icon.
        /// </summary>
        private static readonly Texture2D texturePanel;

        /// <summary>
        /// Create the hierarchy icon.
        /// </summary>
        static HierarchyIcon()
        {
            // Init
            Texture2D[] textures = Resources.LoadAll<Texture2D>("Hierarchy Icon/");
            if (textures.Length != 0)
            {
                texturePanel = textures[0];
                EditorApplication.hierarchyWindowItemOnGUI += ShowHeroKitIcon;
            }
        }

        /// <summary>
        /// Create the hierarchy icon.
        /// </summary>
        /// <param name="instanceID">Instance ID for the icon.</param>
        /// <param name="selectionRect">Rect for the icon.</param>
        private static void ShowHeroKitIcon(int instanceID, Rect selectionRect)
        {
            // place the icon to the right of the list:
            Rect rect = new Rect(selectionRect);
            rect.x = rect.width + 10;
            rect.width = 20;

            GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (go != null && go.GetComponent<HeroKitObject>())
            {
                GUI.Label(rect, texturePanel);
            }
        }
    }
}