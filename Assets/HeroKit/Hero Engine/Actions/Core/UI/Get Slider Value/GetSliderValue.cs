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
    /// Get the value assigned to a UI slider (float).
    /// </summary>
    public class GetSliderValue : IHeroKitAction
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
        public static GetSliderValue Create()
        {
            GetSliderValue action = new GetSliderValue();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            SceneObjectValueData objectData = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            Slider slider = null;

            // object is hero kit object
            if (objectData.heroKitObject != null)
            {
                slider = objectData.heroKitObject[0].GetHeroComponent<Slider>("Slider");
            }

            // object is game object
            else if (objectData.gameObject != null)
            {
                slider = heroKitObject.GetGameObjectComponent<Slider>("Slider", false, objectData.gameObject[0]);
            }

            if (slider != null)
            {
                FloatFieldValue.SetValueB(heroKitObject, 2, slider.value);
            }

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string strGO = (slider != null) ? slider.gameObject.name : "";
                string strValue = (slider != null) ? slider.value.ToString() : "";
                string debugMessage = "Game Object: " + strGO + "\n" +
                                      "Value: " + strValue;
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