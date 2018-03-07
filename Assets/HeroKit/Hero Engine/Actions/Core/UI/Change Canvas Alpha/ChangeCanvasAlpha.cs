// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Change the alpha of a UI canvas.
    /// </summary>
    public class ChangeCanvasAlpha : IHeroKitAction
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
        public static ChangeCanvasAlpha Create()
        {
            ChangeCanvasAlpha action = new ChangeCanvasAlpha();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            SceneObjectValueData objectData = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            endAlpha = SliderValue.GetValue(heroKitObject, 2) * 0.01f;
            duration = (float)(IntegerFieldValue.GetValueA(heroKitObject, 3) * 0.10);
            wait = BoolValue.GetValue(heroKitObject, 4);
            Canvas canvas = null;

            // object is hero object
            if (objectData.heroKitObject != null)
            {                
                canvas = objectData.heroKitObject[0].GetHeroComponent<Canvas>("Canvas");
                if (canvas != null)
                {
                    canvasGroup = objectData.heroKitObject[0].GetHeroComponent<CanvasGroup>("CanvasGroup", true);
                }
                else
                {
                    Debug.LogError("Can't change alpha because this game object does not have a Canvas component attached to it.");
                    return -99;
                }
            }

            // object is game object
            else if (objectData.gameObject != null)
            {
                canvas = heroKitObject.GetGameObjectComponent<Canvas>("Canvas", false, objectData.gameObject[0]);
                if (canvas != null)
                {
                    canvasGroup = heroKitObject.GetGameObjectComponent<CanvasGroup>("CanvasGroup", true, objectData.gameObject[0]);
                }
                else
                {
                    Debug.LogError("Can't change alpha because this game object does not have a Canvas component attached to it.");
                    return -99;
                }
            }

            if (canvasGroup.alpha != endAlpha)
            {
                startAlpha = canvasGroup.alpha;

                // set up update for long action
                heroKitObject.heroState.heroEvent[eventID].waiting = wait;
                updateIsDone = false;
                heroKitObject.longActionsFixed.Add(this);
            }

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string strCanvas = (canvas != null) ? canvas.gameObject.name : "";
                string debugMessage = "Canvas: " + strCanvas + "\n" +
                                      "Alpha: " + endAlpha + "\n" +
                                      "Duration: " + duration;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private CanvasGroup canvasGroup;
        private bool wait;

        private float duration;
        private float endAlpha;
        private float startAlpha;
        private float t = 0;

        // not used
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject, wait);
        }
        public void Update()
        {
            if (canvasGroup == null)
            {
                updateIsDone = true;
                return;
            }

            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);

            if (t < 1)
                t += Time.deltaTime / duration;

            bool exit = HeroKitCommonRuntime.DoFloatsMatch(canvasGroup.alpha, endAlpha);

            if (exit)
                updateIsDone = true;
        }
    }
}