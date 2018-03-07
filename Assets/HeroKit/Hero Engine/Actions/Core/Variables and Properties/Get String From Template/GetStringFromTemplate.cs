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
    /// Get a string from a hero object (default value).
    /// </summary>
    public class GetStringFromTemplate : IHeroKitAction
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
        public static GetStringFromTemplate Create()
        {
            GetStringFromTemplate action = new GetStringFromTemplate();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            HeroObject heroObject = HeroObjectFieldValue.GetValueC(heroKitObject, 0);
            string getThisObject = "";
            bool runThis = (heroObject != null);

            if (runThis)
            {
                getThisObject = StringFieldValue.GetValueC(heroKitObject, 1, heroObject);
                StringFieldValue.SetValueB(heroKitObject, 2, getThisObject);
            }

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "String: " + getThisObject;
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