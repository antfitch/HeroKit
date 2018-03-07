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
    /// Change the value of a toggle (checkbox) in a UI component.
    /// </summary>
    public class ChangeToggle : IHeroKitAction
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
        public static ChangeToggle Create()
        {
            ChangeToggle action = new ChangeToggle();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            SceneObjectValueData objectData = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            bool value = BoolFieldValue.GetValueA(heroKitObject, 2);
            Toggle toggle = null;

            // object is hero object
            if (objectData.heroKitObject != null)
            {
                toggle = objectData.heroKitObject[0].GetHeroComponent<Toggle>("Toggle");
            }

            // object is game object
            else if (objectData.gameObject != null)
            {
                toggle = heroKitObject.GetGameObjectComponent<Toggle>("Toggle", false, objectData.gameObject[0]);
            }

            if (toggle != null)
            {
                toggle.isOn = value;
            }

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string strGO = (toggle != null) ? toggle.gameObject.name : "";
                string debugMessage = "Game Object: " + strGO + "\n" +
                                      "Value: " + value;
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