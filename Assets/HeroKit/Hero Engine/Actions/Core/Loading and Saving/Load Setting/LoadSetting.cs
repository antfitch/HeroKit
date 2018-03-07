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
    /// Load a value from PlayerPrefs.
    /// </summary>
    public class LoadSetting : IHeroKitAction
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
        public static LoadSetting Create()
        {
            LoadSetting action = new LoadSetting();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            string valueName = StringFieldValue.GetValueA(heroKitObject, 2);
            int valueType = DropDownListValue.GetValue(heroKitObject, 0);
            bool runThis = (PlayerPrefs.HasKey(valueName));

            if (runThis)
            {
                // integer
                if (valueType == 1)
                {
                    int value = PlayerPrefs.GetInt(valueName);
                    IntegerFieldValue.SetValueB(heroKitObject, 1, value);
                }
                // float
                else if (valueType == 2)
                {
                    float value = PlayerPrefs.GetFloat(valueName);
                    FloatFieldValue.SetValueB(heroKitObject, 1, value);
                }
                // bool
                else if (valueType == 3)
                {
                    int value = PlayerPrefs.GetInt(valueName);
                    bool newValue = (value == 0) ? true : false;
                    BoolFieldValue.SetValueB(heroKitObject, 1, newValue);
                }
                // string
                else if (valueType == 4)
                {
                    string value = PlayerPrefs.GetString(valueName);
                    StringFieldValue.SetValueB(heroKitObject, 1, value);
                }
            }
            else
            {
                Debug.LogWarning(valueName + " was not found in player preferences.");
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