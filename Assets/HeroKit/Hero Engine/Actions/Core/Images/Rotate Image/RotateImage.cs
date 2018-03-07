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
    /// Rotate an image.
    /// </summary>
    public class RotateImage : IHeroKitAction
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
        public static RotateImage Create()
        {
            RotateImage action = new RotateImage();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            String imageGroupPrefabName = "HeroKit Image Canvas";
            String imagePrefabName = "HeroKit Image Sprite";
            int degrees = 0;
            int duration = 0;

            int imageID = IntegerFieldValue.GetValueA(heroKitObject, 0);
            int speed = IntegerFieldValue.GetValueA(heroKitObject, 1);
            int rotationType = DropDownListValue.GetValue(heroKitObject, 2);
            if (rotationType == 1)
            {
                degrees = IntegerFieldValue.GetValueA(heroKitObject, 3);
            }
            else if (rotationType == 2 || rotationType == 3)
            {
                duration = IntegerFieldValue.GetValueA(heroKitObject, 4);
            }
            wait = BoolValue.GetValue(heroKitObject, 5);
            HeroKitObject targetObject = null;

            // get the game object that contains the images
            GameObject imageGroup = GetImageGroup(imageGroupPrefabName);
            if (imageGroup != null)
            {
                // get the game object that contains the image
                GameObject imageObject = GetImage(imagePrefabName, imageID, imageGroup);
                if (imageObject != null)
                {
                    // get the hero kit object
                    targetObject = heroKitObject.GetGameObjectComponent<HeroKitObject>("HeroKitObject", false, imageObject);
                }
            }

            // rotate the image
            uiRotate = targetObject.GetHeroComponent<UIRotate>("UIRotate", true);
            uiRotate.rotationType = rotationType;
            uiRotate.speed = speed;
            uiRotate.degrees = degrees;
            uiRotate.duration = duration;
            uiRotate.Initialize();

            // set up update for long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = wait;
            updateIsDone = false;
            heroKitObject.longActions.Add(this);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Image ID: " + imageID + "\n" +
                                      "Degrees: " + degrees + "\n" +
                                      "Duration: " + duration + "\n" +
                                      "Speed: " + speed;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        private GameObject GetImageGroup(string prefabName)
        {
            GameObject gameObject = null;

            // get parent container for images. if it doesn't, add it.
            if (HeroKitDatabase.PersistentObjectDictionary.ContainsKey(prefabName))
            {
                gameObject = HeroKitDatabase.GetPersistentObject(prefabName);
            }

            return gameObject;
        }

        private GameObject GetImage(string prefabName, int imageID, GameObject group)
        {
            GameObject targetObject = null;

            // get parent container for images. if it doesn't, add it.
            if (HeroKitDatabase.PersistentObjectDictionary.ContainsKey(prefabName + imageID))
            {
                targetObject = HeroKitDatabase.GetPersistentObject(prefabName + imageID);
            }

            return targetObject;
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private UIRotate uiRotate;
        private bool wait;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject, wait);
        }

        // update the action
        public void Update()
        {
            if (uiRotate == null || !uiRotate.enabled)
                updateIsDone = true;
        }
    }
}