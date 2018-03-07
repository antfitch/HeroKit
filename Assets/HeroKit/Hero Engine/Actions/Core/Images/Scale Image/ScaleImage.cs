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
    /// Scale an image.
    /// </summary>
    public class ScaleImage : IHeroKitAction
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
        public static ScaleImage Create()
        {
            ScaleImage action = new ScaleImage();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            String imageGroupPrefabName = "HeroKit Image Canvas";
            String imagePrefabName = "HeroKit Image Sprite";
            int imageID = IntegerFieldValue.GetValueA(heroKitObject, 0);
            int speed = IntegerFieldValue.GetValueA(heroKitObject, 1);
            float scale = IntegerFieldValue.GetValueA(heroKitObject, 2) * 0.01f;
            wait = BoolValue.GetValue(heroKitObject, 3);
            HeroKitObject targetObject = null;
            Vector3 currentScale = new Vector3();
            Vector3 targetScale = new Vector3();

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

                    // get the current position
                    currentScale = targetObject.transform.localScale;
                    targetScale = new Vector3(scale, scale, targetObject.transform.localScale.z);
                }
            }

            // pan the camera
            uiScale = targetObject.GetHeroComponent<UIScale>("UIScale", true);
            uiScale.targetScale = targetScale;
            uiScale.speed = speed;
            uiScale.Initialize();

            // set up update for long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = wait;
            updateIsDone = false;
            heroKitObject.longActions.Add(this);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Image ID: " + imageID + "\n" +
                                      "Scale: " + scale*100 + "\n" +
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
        private UIScale uiScale;
        private bool wait;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject, wait);
        }

        // update the action
        public void Update()
        {
            if (uiScale == null || !uiScale.enabled)
                updateIsDone = true;
        }
    }
}