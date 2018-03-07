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
    /// Move the main camera to a specific location.
    /// </summary>
    public class MoveCamera : IHeroKitAction
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
        public static MoveCamera Create()
        {
            MoveCamera action = new MoveCamera();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get the camera 
            Camera camera = CameraFieldValue.GetValue(heroKitObject, 0, 11);
            Vector3 position = new Vector3();
            int speed = 0;
            bool runThis = (camera != null);

            if (runThis)
            {
                int moveType = DropDownListValue.GetValue(heroKitObject, 1);
                position = new Vector3();

                // move to position
                if (moveType == 1)
                {
                    position.x = BoolValue.GetValue(heroKitObject, 2) ? FloatFieldValue.GetValueA(heroKitObject, 3) : camera.transform.localPosition.x;
                    position.y = BoolValue.GetValue(heroKitObject, 4) ? FloatFieldValue.GetValueA(heroKitObject, 5) : camera.transform.localPosition.y;
                    position.z = BoolValue.GetValue(heroKitObject, 6) ? FloatFieldValue.GetValueA(heroKitObject, 7) : camera.transform.localPosition.z;
                }

                // move to object
                else if (moveType == 2)
                {
                    SceneObjectValueData sceneObjectData = SceneObjectValue.GetValue(heroKitObject, 8, 9, false);
                    if (sceneObjectData.heroKitObject != null)
                    {
                        position.x = BoolValue.GetValue(heroKitObject, 2) ? sceneObjectData.heroKitObject[0].transform.localPosition.x : camera.transform.localPosition.x;
                        position.y = BoolValue.GetValue(heroKitObject, 4) ? sceneObjectData.heroKitObject[0].transform.localPosition.y : camera.transform.localPosition.y;
                        position.z = BoolValue.GetValue(heroKitObject, 6) ? sceneObjectData.heroKitObject[0].transform.localPosition.z : camera.transform.localPosition.z;
                    }
                    else if (sceneObjectData.gameObject != null)
                    {
                        position.x = BoolValue.GetValue(heroKitObject, 2) ? sceneObjectData.gameObject[0].transform.localPosition.x : camera.transform.localPosition.x;
                        position.y = BoolValue.GetValue(heroKitObject, 4) ? sceneObjectData.gameObject[0].transform.localPosition.y : camera.transform.localPosition.y;
                        position.z = BoolValue.GetValue(heroKitObject, 6) ? sceneObjectData.gameObject[0].transform.localPosition.z : camera.transform.localPosition.z;
                    }
                }

                speed = IntegerFieldValue.GetValueA(heroKitObject, 10);

                // pan the camera
                cameraPan = heroKitObject.GetGameObjectComponent<CameraPan>("CameraPan", true, camera.gameObject);
                cameraPan.targetPosition = position;
                cameraPan.speed = speed;
                cameraPan.Initialize();

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
                                      "Target Position: " + position + "\n" +
                                      "Speed: " + speed;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            // return value
            return -99;
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private CameraPan cameraPan;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }

        // update the action
        public void Update()
        {
            if (cameraPan == null || !cameraPan.enabled)
                updateIsDone = true;
        }
    }
}