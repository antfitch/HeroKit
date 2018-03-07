// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Change the text in a label on a UI game object.
    /// </summary>
    public class UIText : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// Actions that can be performed on the text.
        /// </summary>
        public enum TextAction { changeColor };
        /// <summary>
        /// The action that will be performed on the text.
        /// </summary>
        public TextAction textAction;
        /// <summary>
        /// The text component to update.
        /// </summary>
        public Text text;
        /// <summary>
        /// The speed of the action.
        /// </summary>
        public float speed;
        /// <summary>
        /// The target color of the text.
        /// </summary>
        public Color targetColor;
        /// <summary>
        /// The initial color of the text.
        /// </summary>
        private Color startColor;
        /// <summary>
        /// Strength of lerp between two positions (0 to 1).
        /// </summary>
        private float t;

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Initialize this script.
        /// </summary>
        public void Initialize()
        {
            // exit early if something we need is missing
            if (text == null)
            {
                enabled = false;
            }
            else
            {
                // enables update methods for this class
                enabled = true;

                startColor = text.color;
                t = 0;
            }       
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate ()
        {
            if (textAction == TextAction.changeColor)
                ChangeColor();
        }

        /// <summary>
        /// Change the color of the text.
        /// </summary>
        private void ChangeColor()
        {
            text.color = Color.Lerp(startColor, targetColor, t);

            if (t < 1)
                t += Time.deltaTime / speed;

            bool exit = HeroKitCommonRuntime.DoColorsMatch(text.color, targetColor);

            if (exit)
                enabled = false;
        }
    }
}