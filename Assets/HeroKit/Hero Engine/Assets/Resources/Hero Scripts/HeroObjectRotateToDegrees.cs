// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

/// <summary>
/// Rotate a game object a specific number of degrees.
/// </summary>
namespace HeroKit.Scene.Scripts
{
    public class HeroObjectRotateToDegrees : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables (general)
        // --------------------------------------------------------------

        /// <summary>
        /// The speed of the rotation.
        /// </summary>
        public float speed;           
        /// <summary>
        /// The degrees to to work with.
        /// </summary>
        public Vector3 degrees;

        // --------------------------------------------------------------
        // Variables (internal)
        // --------------------------------------------------------------

        /// <summary>
        /// The starting position of the rotation.
        /// </summary>
        private Quaternion from;
        /// <summary>
        /// The target position of the rotation.
        /// </summary>
        private Quaternion to;
        /// <summary>
        /// The position of the rotation during the last frame.
        /// </summary>
        private Quaternion old;
        /// <summary>
        /// The amount of time that has elapsed during the rotation.
        /// </summary>
        private float elapsed;

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
            from = transform.rotation;
            to = Quaternion.Euler(degrees);
            elapsed = 0;
            speed = speed * 0.1f;

            enabled = true;            
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate ()
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(from, to, elapsed * speed);

            // exit if we are at destination
            if (old == transform.rotation)
                enabled = false;

            // record current position
            old = transform.rotation;
        }
    }
}