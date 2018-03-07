// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using UnityEngine.UI;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Change a UI image.
    /// </summary>
    public class ChangeImage : IHeroKitAction
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
        public static ChangeImage Create()
        {
            ChangeImage action = new ChangeImage();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            SceneObjectValueData objectData = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            UnityObjectField unityObject = UnityObjectFieldValue.GetValueA(heroKitObject, 2);
            Sprite sprite = (unityObject.value != null) ? (Sprite)unityObject.value : null;
            Image image = null;

            // object is hero object
            if (objectData.heroKitObject != null)
            {
                image = objectData.heroKitObject[0].GetHeroComponent<Image>("Image");
            }

            // object is game object
            else if (objectData.gameObject != null)
            {
                image = heroKitObject.GetGameObjectComponent<Image>("Image", false, objectData.gameObject[0]);          
            }

            if (image != null)
            {
                image.sprite = sprite;
            }

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string strImage = (image != null) ? image.gameObject.name : "";
                string strSprite = (image != null && image.sprite != null) ? image.sprite.name : "";
                string debugMessage = "Game Object: " + strImage + "\n" +
                                      "Image: " + strSprite;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        // not used
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