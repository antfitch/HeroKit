// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using HeroKit.Scene.Scripts;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Have the first-person camera follow a hero object.
    /// </summary>
    public class FirstPersonCameraOn : IHeroKitAction
    {
        // set up properties needed for all actions
        private HeroKitObject _heroKitObject;
        public HeroKitObject heroKitObject
        {
            get { return _heroKitObject; }
            set { _heroKitObject = value; }
        }
        private int _eventID;
        public int eventID
        {
            get { return _eventID; }
            set { _eventID = value; }
        }
        private bool _updateIsDone;
        public bool updateIsDone
        {
            get { return _updateIsDone; }
            set { _updateIsDone = value; }
        }

        // This is used by HeroKitCommon.GetAction() to add this action to the ActionDictionary. Don't delete!
        public static FirstPersonCameraOn Create()
        {
            FirstPersonCameraOn action = new FirstPersonCameraOn();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values    
            int smoothing = 1;
            float posY = 1;
            float posZ = 0.5f;
            float angle = 0;

            // get the object to follow
            HeroKitObject targetObject = HeroObjectFieldValue.GetValueA(heroKitObject, 1)[0];

            // get the camera 
            Camera camera = CameraFieldValue.GetValue(heroKitObject, 0, 11);

            // change settings
            if (BoolValue.GetValue(heroKitObject, 2))
            {
                // change smoothing
                if (BoolValue.GetValue(heroKitObject, 3))
                {
                    smoothing = IntegerFieldValue.GetValueA(heroKitObject, 4);
                }

                // change Y pos
                if (BoolValue.GetValue(heroKitObject, 5))
                {
                    posY = FloatFieldValue.GetValueA(heroKitObject, 6);
                }

                // change Z pos
                if (BoolValue.GetValue(heroKitObject, 7))
                {
                    posZ = FloatFieldValue.GetValueA(heroKitObject, 8);
                }

                // change angle
                if (BoolValue.GetValue(heroKitObject, 9))
                {
                    angle = FloatFieldValue.GetValueA(heroKitObject, 10);
                }
            }

            bool runThis = (camera != null && targetObject != null);

            if (runThis)
            {
                // change perspective to "perspective"
                camera.orthographic = false;

                // set up the camera
                CameraController cameraController = heroKitObject.GetGameObjectComponent<CameraController>("CameraController", true, camera.gameObject);
                cameraController.targetObject = targetObject;
                cameraController.smoothing = smoothing;
                cameraController.defaultPos = new Vector3(0, posY, posZ);
                cameraController.defaultAngles = new Vector3(angle, 0, 0);
                cameraController.firstPerson = true;
                cameraController.Initialize();
            }

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Camera: " + camera;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        // Not used
        public bool RemoveFromLongActions()
        {
            throw new NotImplementedException();
        }
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}