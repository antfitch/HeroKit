// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.ActionField;
using HeroKit.Scene.Scripts;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Shake the main camera.
    /// </summary>
    public class ShakeCamera : IHeroKitAction
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
        public static ShakeCamera Create()
        {
            ShakeCamera action = new ShakeCamera();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            Camera camera = CameraFieldValue.GetValue(heroKitObject, 0, 4);
            int speed = IntegerFieldValue.GetValueA(heroKitObject, 1);
            int magnitude = IntegerFieldValue.GetValueA(heroKitObject, 2);
            int time = IntegerFieldValue.GetValueA(heroKitObject, 3);
            bool runThis = (camera != null);

            // make sure object is a camera            
            if (runThis)
            {
                // shake the camera
                cameraShake = heroKitObject.GetGameObjectComponent<CameraShake>("CameraShake", true, camera.gameObject);
                cameraShake.speed = speed;
                cameraShake.magnitude = magnitude;
                cameraShake.time = time;
                cameraShake.Initialize();

                // set up update for long action
                eventID = heroKitObject.heroStateData.eventBlock;
                heroKitObject.heroState.heroEvent[eventID].waiting = true;
                updateIsDone = false;
                heroKitObject.longActions.Add(this);
            }

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Camera: " + camera + "\n" +
                                      "Magnitude: " + magnitude + "\n" +
                                      "Speed: " + speed + "\n" +
                                      "Speed: " + speed;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            // return value
            return -99;
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private CameraShake cameraShake;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }

        // update the action
        public void Update()
        {
            if (cameraShake == null || !cameraShake.enabled)
                updateIsDone = true;
        }
    }
}