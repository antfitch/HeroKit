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
    /// Change a unity object on a hero kit object.
    /// </summary>
    public class ChangeUnityObject : IHeroKitAction
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
        public static ChangeUnityObject Create()
        {
            ChangeUnityObject action = new ChangeUnityObject();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            UnityObjectField storeObjectHere = UnityObjectFieldValue.GetValueB(heroKitObject, 0);
            UnityObjectField getThisObject = UnityObjectFieldValue.GetValueA(heroKitObject, 1);

            bool runThis = (storeObjectHere != null && getThisObject != null);

            if (runThis)    
                UnityObjectFieldValue.SetValueB(heroKitObject, 0, getThisObject);

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