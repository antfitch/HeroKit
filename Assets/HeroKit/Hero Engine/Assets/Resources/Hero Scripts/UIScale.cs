// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.Actions;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Change the scale of an image.
    /// </summary>
    public class UIScale : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The target scale for the image.
        /// </summary>
        public Vector3 targetScale;
        /// <summary>
        /// The speed of the scaling.
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
            // Smoothly interpolate between the camera's current position and it's target position.
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, t+=Time.deltaTime / speed); 

            bool done = HeroActionCommonRuntime.CompareVectors(transform.localScale, targetScale, 0.0001f);
            if (done)
            {
                t = 0f;
                enabled = false;
            }
        }
    }
}