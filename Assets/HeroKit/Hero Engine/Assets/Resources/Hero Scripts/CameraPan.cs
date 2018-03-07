// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.Actions;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Move the main camera to a specific position in the scene.
    /// </summary>
    public class CameraPan : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// Pan the camera to this position in the scene.
        /// </summary>
        public Vector3 targetPosition;
        /// <summary>
        /// Speed of the pan.
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
            // Create a postion the camera is aiming for.
            Vector3 targetCamPos = targetPosition;

            // Smoothly interpolate between the camera's current position and it's target position.
            //transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetCamPos, t+=Time.deltaTime / speed); 

            // if pan is finished, exit
            bool done = HeroActionCommonRuntime.CompareVectors(transform.position, targetCamPos, 0.1f);
            if (done)
            {
                t = 0f;
                enabled = false;
            }
        }
    }
}