// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.Actions;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Move an image across the screen.
    /// </summary>
    public class UIPan : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The target position for the image.
        /// </summary>
        public Vector3 targetPosition;
        /// <summary>
        /// The speed of the image.
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
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, t += Time.deltaTime / speed);

            bool done = HeroActionCommonRuntime.CompareVectors(transform.localPosition, targetPosition, 1);
            if (done)
            {
                t = 0f;
                enabled = false;
            }
        }
    }
}