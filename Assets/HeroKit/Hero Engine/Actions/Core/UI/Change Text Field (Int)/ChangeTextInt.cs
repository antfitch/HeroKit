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
    /// Change the value of a UI text field (integer value only).
    /// </summary>
    public class ChangeTextInt : IHeroKitAction
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
        public static ChangeTextInt Create()
        {
            ChangeTextInt action = new ChangeTextInt();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            SceneObjectValueData objectData = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            string stringForText = IntegerFieldValue.GetValueA(heroKitObject, 2).ToString();
            Text text = null;

            // object is hero object
            if (objectData.heroKitObject != null)
            {
                text = objectData.heroKitObject[0].GetHeroComponent<Text>("Text");
            }

            // object is game object
            else if (objectData.gameObject != null)
            {
                text = heroKitObject.GetGameObjectComponent<Text>("Text", false, objectData.gameObject[0]);          
            }

            if (text != null)
            {
                text.text = stringForText;
            }

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string strGO = (text != null) ? text.gameObject.name : "";
                string debugMessage = "Game Object: " + strGO + "\n" +
                                      "Text: " + stringForText;
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