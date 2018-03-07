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
    /// Get a unity object from a hero object (default value).
    /// </summary>
    public class GetUnityObjectFromTemplate : IHeroKitAction
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
        public static GetUnityObjectFromTemplate Create()
        {
            GetUnityObjectFromTemplate action = new GetUnityObjectFromTemplate();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            HeroObject heroObject = HeroObjectFieldValue.GetValueC(heroKitObject, 0);
            UnityObjectField getThisObject = null;
            bool runThis = (heroObject != null);

            if (runThis)
            {
                getThisObject = UnityObjectFieldValue.GetValueC(heroKitObject, 1, heroObject);
                UnityObjectFieldValue.SetValueB(heroKitObject, 2, getThisObject);
            }

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Unity Object: " + getThisObject;
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