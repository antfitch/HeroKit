// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System;
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Rotate a game object clockwise or counterclockwise.
    /// </summary>
    public class HeroObjectRotate : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// Speed of the rotation.
        /// </summary>
        public float speed;       
        /// <summary>
        /// Degrees to rotate.
        /// </summary>
        public Vector3 degrees;

        // --------------------------------------------------------------
        // Methods 
        // --------------------------------------------------------------

        /// <summary>
        /// Call this method when this component is created.
        /// </summary>
        public void Awake()
        {
            enabled = false;
        }

        /// <summary>
        /// Initialize the script.
        /// </summary>
        public void Initialize()
        {
            // 0=0, 1=+1, 2=-1
            if (Math.Abs(degrees.x - 2) < 0.1f) degrees.x = -1;
            if (Math.Abs(degrees.y - 2) < 0.1f) degrees.y = -1;
            if (Math.Abs(degrees.z - 2) < 0.1f) degrees.z = -1;

            // enables update methods for this class
            enabled = true;
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate ()
        {
            RotateInDirection();
        }

        /// <summary>
        /// Rotate game object clockwise or counterclockwise.
        /// </summary>
        private void RotateInDirection()
        {
            transform.Rotate(degrees * Time.deltaTime * speed);
        }

        /// <summary>
        /// Stop rotating the game object.
        /// </summary>
        public void StopRotate()
        {
            enabled = false;
        }
    }
}