// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.Actions;
using System.Collections.Generic;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Control the main camera.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// Hero kit object that the camera should follow.
        /// </summary>
        public HeroKitObject targetObject;
        /// <summary>
        /// The speed of the follow.
        /// </summary>
        public float smoothing;
        /// <summary>
        /// The starting position of the camera.
        /// </summary>
        public Vector3 defaultPos;
        /// <summary>
        /// The starting euler angles of the camera.
        /// </summary>
        public Vector3 defaultAngles;
        /// <summary>
        /// First-person camera = True. Third-person camera = False.
        /// </summary>
        public bool firstPerson;
        /// <summary>
        /// Move the camera to this position.
        /// </summary>
        private Transform target;
        /// <summary>
        /// The name of the object that we are following.
        /// </summary>
        private int targetGUID;

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Initialize this script.
        /// </summary>
        public void Initialize()
        {
            // set the target object
            GetTarget(targetObject);

            // set the position and angle of the camera
            transform.position = defaultPos;
            transform.eulerAngles = defaultAngles;

            // enables update methods for this class
            enabled = true;
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate ()
        {
            // attempt to get target if it no longer exists.
            if (target == null) RestoreTarget();

            // first person camera
            if (firstPerson)
            {
                transform.localPosition = new Vector3(target.localPosition.x, target.localPosition.y + defaultPos.y, target.localPosition.z + defaultPos.z);
            }
            // third person camera
            else
            {
                // Create a postion the camera is aiming for based on the offset from the target.
                Vector3 targetCamPos = new Vector3(target.localPosition.x, target.localPosition.y + defaultPos.y, target.localPosition.z + defaultPos.z);

                // Smoothly interpolate between the camera's current position and it's target position.
                transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            }
        }

        /// <summary>
        /// Set the target game object for the camera to follow.
        /// </summary>
        /// <param name="heroKitObject"></param>
        private void GetTarget(HeroKitObject heroKitObject)
        {
            // exit early if there is no target object for this camera controller
            if (targetObject == null)
            {
                enabled = false;
                return;
            }

            // set up target info
            target = heroKitObject.transform;
            targetGUID = heroKitObject.heroGUID;
        }

        /// <summary>
        /// If a target existed, but was lost, attempt to find the target and re-attach it to the camera.
        /// </summary>
        private void RestoreTarget()
        {
            List<HeroKitObject> heroKitObjects = HeroActionCommonRuntime.GetHeroObjectsByGUID(HeroActionCommonRuntime.GetHeroObjectsInScene(), 1, targetGUID);
            if (heroKitObjects.Count > 0)
            {
                target = heroKitObjects[0].transform;
            }

            if (target == null)
            {
                Debug.LogError("No target found for CameraFollow on " + transform.name + ". Disabling script.");
                enabled = false;
            }               
        }
    }
}