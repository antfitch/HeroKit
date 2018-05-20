// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using HeroKit.Scene.ActionField;
using HeroKit.Scene.Scripts;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Fade out a scene.
    /// </summary>
    public class FadeOutScene : IHeroKitAction
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
        public static FadeOutScene Create()
        {
            FadeOutScene action = new FadeOutScene();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            float speed = IntegerFieldValue.GetValueA(heroKitObject, 0) * 0.1f;

            // get the fade UI
            HeroKitObject targetObject = HeroKitCommonRuntime.GetPrefabFromAssets(HeroKitCommonRuntime.settingsInfo.fadeInOutScreen, true);
            bool runThis = (targetObject != null);

            if (runThis)
            {
                targetObject.gameObject.SetActive(true);

                // fade out the scene
                uiColor = targetObject.GetHeroComponent<UIColor>("UIColor", true);
                uiColor.targetImage = targetObject.GetComponentInChildren<Image>(true);
                uiColor.targetColor.a = 1f;
                uiColor.startColor.a = 0f;
                uiColor.speed = speed;
                uiColor.Initialize();

                // set up update for long action
                eventID = heroKitObject.heroStateData.eventBlock;
                heroKitObject.heroState.heroEvent[eventID].waiting = true;
                updateIsDone = false;
                heroKitObject.longActionsFixed.Add(this);
            }

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Speed: " + speed;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            // return value
            return -99;
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private UIColor uiColor;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }

        // update the action
        public void Update()
        {
            if (uiColor == null || !uiColor.enabled)
                updateIsDone = true;
        }
    }
}