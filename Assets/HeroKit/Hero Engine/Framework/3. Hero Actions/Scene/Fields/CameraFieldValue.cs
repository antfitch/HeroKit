// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get a camera.
    /// </summary>
    public static class CameraFieldValue
    {
        /// <summary>
        /// Get a camera.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the camera field.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <returns></returns>
        public static Camera GetValue(HeroKitObject heroKitObject, int actionFieldIDA, int actionFieldIDB)
        {
            Camera camera = null;

            // get the camera 
            bool customCamera = BoolValue.GetValue(heroKitObject, actionFieldIDA);
            if (customCamera)
            {
                HeroKitObject cameraObject = HeroObjectFieldValue.GetValueA(heroKitObject, actionFieldIDB)[0];
                if (cameraObject != null)
                    camera = cameraObject.GetHeroComponent<Camera>("Camera", true);
            }
            else
            {
                camera = Camera.main;
            }

            return camera;
        }
    }
}