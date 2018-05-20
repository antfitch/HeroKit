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
    /// Change the fade screen in and out menu.
    /// </summary>
    public class ChangeFadeScreenInOut : IHeroKitAction
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
        public static ChangeFadeScreenInOut Create()
        {
            ChangeFadeScreenInOut action = new ChangeFadeScreenInOut();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            GameObject menuTemplate = PrefabValue.GetValue(heroKitObject, 0);
            bool runThis = (menuTemplate != null);

            if (runThis)
            {
                // if new prefab is not in scene, delete the old one from scene and attach new prefab to settings.
                GameObject prefab = HeroKitCommonRuntime.settingsInfo.fadeInOutScreen;
                if (prefab != null && prefab != menuTemplate)
                {
                    HeroKitCommonRuntime.DeletePrefabFromScene(prefab, true);
                    HeroKitCommonRuntime.settingsInfo.fadeInOutScreen = menuTemplate;
                }
            }

            if (heroKitObject.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));

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