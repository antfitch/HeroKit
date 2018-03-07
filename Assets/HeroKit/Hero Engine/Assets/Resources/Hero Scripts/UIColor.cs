// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Adjust the color of an image.
    /// </summary>
    public class UIColor : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The image to modify.
        /// </summary>
        public Image targetImage;
        /// <summary>
        /// The target color of the image.
        /// </summary>
        public Color targetColor;
        /// <summary>
        /// The initial color of the image.
        /// </summary>
        public Color startColor;
        /// <summary>
        /// The speed of the change in color.
        /// </summary>
        public float speed;
        /// <summary>
        /// Strength of lerp between two positions (0 to 1).
        /// </summary>
        private float t;

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Call this when the component is attached to the game object
        /// </summary>
        public void Awake()
        {
            enabled = false;
        }

        /// <summary>
        /// Initialize this script.
        /// </summary>
        public void Initialize()
        {
            // enables update methods for this class
            enabled = true;
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate ()
        {
            targetImage.color = Color.Lerp(targetImage.color, targetColor, t += Time.deltaTime / speed);

            // exit early if we are not at target color yet
            if (targetImage.color != targetColor) return;

            t = 0f;
            enabled = false;
        }
    }
}