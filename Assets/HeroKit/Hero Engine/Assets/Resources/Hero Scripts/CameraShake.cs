// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Shake the main camera.
    /// </summary>
    public class CameraShake : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The speed of the shaking.
        /// </summary>
        public float speed;        
        /// <summary>
        /// The strength of the shaking.
        /// </summary>
        public float magnitude;   
        /// <summary>
        /// The amount of time the camera should shake.
        /// </summary>
        public float time;         
        /// <summary>
        /// The default position of the camera.
        /// </summary>
        private Vector3 startPosition;
        /// <summary>
        /// Shaking is finished. Move the camera back to its default position.
        /// </summary>
        private bool goHome;

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Call this when the component is attached to the game object
        /// </summary>
        private void Awake()
        {
            enabled = false;
        }

        /// <summary>
        /// Initialize this script.
        /// </summary>
        public void Initialize()
        {
            // get the start postion of the camera
            startPosition = transform.localPosition;

            // enables update methods for this class
            enabled = true;
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate ()
        {
            if (speed > 0)
            {
                if (goHome)
                {
                    transform.localPosition = startPosition;
                }
                else
                {
                    Vector3 shaking = Random.insideUnitSphere * (magnitude * 0.01f);
                    Vector3 newPos = new Vector3(transform.localPosition.x + shaking.x, transform.localPosition.y + shaking.y, transform.localPosition.z + shaking.z);
                    transform.localPosition = newPos;
                }

                goHome = !goHome;
                speed -= Time.deltaTime * time;
            }
            else
            {
                transform.localPosition = startPosition;
                goHome = false;
                enabled = false;
            }
        }
    }
}