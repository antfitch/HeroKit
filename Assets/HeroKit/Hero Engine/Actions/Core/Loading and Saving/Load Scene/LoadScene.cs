// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Load a scene.
    /// </summary>
    public class LoadScene : IHeroKitAction
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
        public static LoadScene Create()
        {
            LoadScene action = new LoadScene();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // Get the scene object
            string sceneName = UnityObjectFieldValue.GetValueA(heroKitObject, 0).sceneName;
            Vector3 cameraPosition = new Vector3();
            Vector3 cameraRotation = new Vector3();
            bool runThis = (sceneName != "");

            if (runThis)
            {
                bool useDefaultScene = BoolValue.GetValue(heroKitObject, 1);
                bool removeNonPersistent = BoolValue.GetValue(heroKitObject, 2);
                bool saveCurrentScene = !BoolValue.GetValue(heroKitObject, 3);

                // get position
                bool getPosition = BoolValue.GetValue(heroKitObject, 4);
                Vector3 defaultPosition = new Vector3(-999999, -999999, -999999);
                cameraPosition = (getPosition) ? CoordinatesValue.GetValue(heroKitObject, 5, 6, 7, 8, 9, 10, defaultPosition) : defaultPosition;

                // get rotation
                bool getRotation = BoolValue.GetValue(heroKitObject, 11);
                Vector3 defaultRotation = new Vector3(-999999, -999999, -999999);
                cameraRotation = (getRotation) ? CoordinatesValue.GetValue(heroKitObject, 12, 13, 14, 15, 16, 17, defaultRotation) : defaultRotation;

                // Save the current scene
                if (saveCurrentScene)
                {
                    SaveScene saveScene = new SaveScene();
                    saveScene.SaveSceneData(heroKitObject, false); // save scene objects
                    saveScene.SaveSceneData(heroKitObject, true);  // save persistent objects
                }

                // Load the scene. Load any cached data for the scene.
                HeroKitCommonScene.LoadScene(sceneName, useDefaultScene, removeNonPersistent, cameraPosition, cameraRotation);

                // set up update for long action
                eventID = heroKitObject.heroStateData.eventBlock;
                heroKitObject.heroState.heroEvent[eventID].waiting = true;
                updateIsDone = false;
                heroKitObject.longActions.Add(this);
            }

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Scene ID: " + sceneName + "\n" +
                                      "Camera Position: " + cameraPosition + "\n" +
                                      "Camera Rotation: " + cameraRotation;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            // Return value
            return -99;
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }

        // update the action
        public void Update()
        {
            if (HeroKitCommonScene.sceneIsLoaded && HeroKitCommonScene.sceneObjectsLoaded)
            {
                updateIsDone = true;
            }
        }
    }
}