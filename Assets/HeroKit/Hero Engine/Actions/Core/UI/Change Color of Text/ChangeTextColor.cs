// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.ActionField;
using UnityEngine.UI;
using HeroKit.Scene.Scripts;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Change the color of a UI text field.
    /// </summary>
    public class ChangeTextColor : IHeroKitAction
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
        public static ChangeTextColor Create()
        {
            ChangeTextColor action = new ChangeTextColor();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;           
            SceneObjectValueData objectData = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            Color endColor = ColorValue.GetValue(heroKitObject, 2);
            float duration = (float)(IntegerFieldValue.GetValueA(heroKitObject, 3) * 0.10);
            wait = BoolValue.GetValue(heroKitObject, 4);

            // get the text component
            if (objectData.heroKitObject != null)
            {
                // execute action for all objects in list
                for (int i = 0; i < objectData.heroKitObject.Length; i++)
                    ExecuteOnHeroObject(objectData.heroKitObject[i], endColor, duration);              
            }
            else if (objectData.gameObject != null)
            {
                // execute action for all objects in list
                for (int i = 0; i < objectData.gameObject.Length; i++)
                    ExecuteOnGameObject(objectData.gameObject[i], endColor, duration);        
            }

            // set up update for long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = wait;
            updateIsDone = false;
            heroKitObject.longActionsFixed.Add(this);

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Color: " + endColor + "\n" +
                                      "Duration: " + duration;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnHeroObject(HeroKitObject targetObject, Color endColor, float duration)
        {
            // get the script
            uiText = targetObject.GetHeroComponent<UIText>("UIText", true);
            Text text = targetObject.GetHeroComponent<Text>("Text");
            ExecuteOnTarget(uiText, text, endColor, duration);
        }

        public void ExecuteOnGameObject(GameObject targetObject, Color endColor, float duration)
        {
            // get the script
            uiText = heroKitObject.GetGameObjectComponent<UIText>("UIText", false, targetObject);
            Text text = heroKitObject.GetGameObjectComponent<Text>("Text", false, targetObject);
            ExecuteOnTarget(uiText, text, endColor, duration);
        }

        public void ExecuteOnTarget(UIText uiText, Text text, Color endColor, float duration)
        {
            // set up script
            uiText.textAction = UIText.TextAction.changeColor;
            uiText.text = text;
            uiText.targetColor = endColor;
            uiText.speed = duration;
            uiText.Initialize();
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private UIText uiText;
        private bool wait;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject, wait);
        }

        // update the action
        public void Update()
        {
            if (uiText == null || !uiText.enabled)
                updateIsDone = true;
        }
    }
}