// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Rotate an image.
    /// </summary>
    public class UIRotate : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// Type of rotation. 1=rotate to degree, 2=rotate clockwise, 3=rotate counterclockwise
        /// </summary>
        public int rotationType;
        /// <summary>
        /// Speed of the rotation.
        /// </summary>
        public float speed;       
        /// <summary>
        /// Duration of the rotation.
        /// </summary>
        public float duration;
        /// <summary>
        /// Degrees to rotate.
        /// </summary>
        public float degrees; 
        /// <summary>
        /// Amount of time to rotate the image.
        /// </summary>
        private float targetTime;

        /// <summary>
        /// The number of degrees that have changed since the start of the rotation.
        /// </summary>
        private int degreesChanged;
        /// <summary>
        /// The current rotation degree of the image.
        /// </summary>
        private float currentDegree;

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
            targetTime = Time.time + duration;
            degreesChanged = 0;
            currentDegree = transform.localEulerAngles.z;
            enabled = true;
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate ()
        {
            // rotate to a specific angle
            switch (rotationType)
            {
                case 1:
                    if (degrees > 0)
                        RotateToAngleClockwise();
                    else if (degrees < 0)
                        RotateToAngleCounterclockwise();
                    break;
                case 2:
                    RotateClockwise();
                    break;
                case 3:
                    RotateCounterclockwise();
                    break;
            }
        }

        /// <summary>
        /// Rotate the image clockwise until the image is at a specific angle.
        /// </summary>
        private void RotateToAngleClockwise()
        {
            // see if degree has changed
            if ((int)currentDegree != (int)transform.localEulerAngles.z)
            {
                degreesChanged++;
            }

            // if we've rotated to the correct degree, exit
            if (degreesChanged >= degrees)
            {
                enabled = false;
            }

            // get current degree
            currentDegree = transform.localEulerAngles.z;

            // rotate the object
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        }

        /// <summary>
        /// Rotate the image counterclockwise until the image is at a specific angle.
        /// </summary>
        private void RotateToAngleCounterclockwise()
        {
            // see if degree has changed
            if ((int)currentDegree != (int)transform.localEulerAngles.z)
            {
                degreesChanged--;
            }

            // if we've rotated to the correct degree, exit
            if (degreesChanged <= degrees)
            {
                enabled = false;
            }

            // get current degree
            currentDegree = transform.localEulerAngles.z;

            // rotate the object
            transform.Rotate(Vector3.back * Time.deltaTime * speed);
        }

        /// <summary>
        /// Rotate the image clockwise.
        /// </summary>
        private void RotateClockwise()
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);

            if (targetTime < Time.time)
            {
                enabled = false;
            }
        }

        /// <summary>
        /// Rotate the image counterclockwise.
        /// </summary>
        private void RotateCounterclockwise()
        {
            transform.Rotate(Vector3.back * Time.deltaTime * speed);

            if (targetTime < Time.time)
            {
                enabled = false;
            }
        }
    }
}