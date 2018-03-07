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
    /// Have the 2D third-person camera follow a hero object.
    /// </summary>
    public class OrthoCameraOn : IHeroKitAction
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
        public static OrthoCameraOn Create()
        {
            OrthoCameraOn action = new OrthoCameraOn();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            int smoothing = 5;
            float posY = 0;
            float posX = 0;
            float size = 5;

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

                // change Z pos
                if (BoolValue.GetValue(heroKitObject, 5))
                {
                    posX = FloatFieldValue.GetValueA(heroKitObject, 6);
                }

                // change Y pos
                if (BoolValue.GetValue(heroKitObject, 7))
                {
                    posY = FloatFieldValue.GetValueA(heroKitObject, 8);
                }

                // change size of viewport
                if (BoolValue.GetValue(heroKitObject, 9))
                {
                    size = FloatFieldValue.GetValueA(heroKitObject, 10);
                }
            }

            bool runThis = (camera != null && targetObject != null);

            if (runThis)
            {
                // change perspective to "orthographic"
                camera.orthographic = true;
                camera.orthographicSize = size;

                // set up the camera
                CameraController cameraController = heroKitObject.GetGameObjectComponent<CameraController>("CameraController", true, camera.gameObject);
                cameraController.targetObject = targetObject;
                cameraController.smoothing = smoothing;
                cameraController.defaultPos = new Vector3(posX, posY, -10);
                cameraController.defaultAngles = new Vector3(0, 0, 0);
                cameraController.firstPerson = false;
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