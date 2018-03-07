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
    /// Save a value in PlayerPrefs.
    /// </summary>
    public class SaveSetting : IHeroKitAction
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
        public static SaveSetting Create()
        {
            SaveSetting action = new SaveSetting();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            string valueName = StringFieldValue.GetValueA(heroKitObject, 2);
            int valueType = DropDownListValue.GetValue(heroKitObject, 0);

            // integer
            if (valueType == 1)
            {
                int value = IntegerFieldValue.GetValueA(heroKitObject, 1);
                PlayerPrefs.SetInt(valueName, value);
            }
            // float
            else if (valueType == 2)
            {
                float value = FloatFieldValue.GetValueA(heroKitObject, 1);
                PlayerPrefs.SetFloat(valueName, value);
            }
            // bool
            else if (valueType == 3)
            {
                bool value = BoolFieldValue.GetValueA(heroKitObject, 1);
                int newValue = (value) ? 1 : 0;
                PlayerPrefs.SetInt(valueName, newValue);
            }
            // string
            else if (valueType == 4)
            {
                string value = StringFieldValue.GetValueA(heroKitObject, 1);
                PlayerPrefs.SetString(valueName, value);
            }

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Value Type (1=int, 2=float, 3=bool, 4=string): " + valueType + "\n" +
                                      "Value Name: " + valueName;
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