// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Load a scene and take an object from the previous scene to the new scene.
    /// </summary>
    public class GoToScene : IHeroKitAction
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
        public static GoToScene Create()
        {
            GoToScene action = new GoToScene();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // Get the hero kit object to move
            HeroKitObject targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1, false)[0];

            // Get the scene object
            string sceneName = UnityObjectFieldValue.GetValueA(heroKitObject, 2).sceneName;

            // get new position for target object
            Vector3 defaultPosition = new Vector3(-999999, -999999, -999999);
            Vector3 targetPosition = CoordinatesValue.GetValue(heroKitObject, 3, 4, 5, 6, 7, 8, defaultPosition);

            // get new rotation for target object
            Vector3 targetDirection = defaultPosition;
            int directionType = DropDownListValue.GetValue(heroKitObject, 9);
            switch (directionType) 
            {
                case 1: // retain
                    break;
                case 2: // left
                    targetDirection.y = -90f;
                    break;
                case 3: // right
                    targetDirection.y = 90f;
                    break;
                case 4: // up
                    targetDirection.y = 0f;
                    break;
                case 5: // down
                    targetDirection.y = 180f;
                    break;
                case 6: // custom
                    targetDirection = CoordinatesValue.GetValue(heroKitObject, 10, 11, 12, 13, 14, 15, defaultPosition);
                    break;
            }

            // move camera to object's position?
            bool moveCameraPos = BoolValue.GetValue(heroKitObject, 16);

            // move camera to object's rotation?
            bool moveCameraRotation = BoolValue.GetValue(heroKitObject, 17);

            // scene transition settings
            bool useDefaultScene = BoolValue.GetValue(heroKitObject, 18);
            bool removeNonPersistent = BoolValue.GetValue(heroKitObject, 19);
            bool saveCurrentScene = !BoolValue.GetValue(heroKitObject, 20);

            bool runThis = (targetObject != null && sceneName != "");

            // NEXT STEP:
            // save current scene, 
            // check if object is persistant. if it is, load next scene, change direction and rotation of object.
            // if it isn't see what we can do to physically move it from one scene to another. Keep in mind that object could exist in two scenes after this (if it is saved in scene data)
            // maybe disable it in current scene and create a new verison in the next scene.

            if (runThis)
            {
                // Save the current scene
                if (saveCurrentScene)
                {
                    SaveScene saveScene = new SaveScene();
                    saveScene.SaveSceneData(heroKitObject, false); // save scene objects
                    saveScene.SaveSceneData(heroKitObject, true);  // save persistent objects
                }

                // Load the scene. Load any cached data for the scene.
                HeroKitCommonScene.LoadSceneWithObject(sceneName, useDefaultScene, removeNonPersistent, 
                                                       targetPosition, targetDirection, targetObject, moveCameraPos, moveCameraRotation);
            }

            if (targetObject == null)
            {
                Debug.LogError("There is no object to move to the next scene! Terminating action early.");
            }

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Scene: " + sceneName + "\n" +
                                      "Move this object: " + targetObject + "\n" +
                                      "To this position: " + targetPosition;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            // Return value
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