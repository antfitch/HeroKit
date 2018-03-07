﻿// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{
    /// <summary>
    /// Get a field in a component on an object.
    /// </summary>
    public class GetFieldInfo : IHeroKitAction
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
        public static GetFieldInfo Create()
        {
            GetFieldInfo action = new GetFieldInfo();
            return action;
        }

        // execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get the hero object where the parameters are stored
            HeroKitObject targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1)[0];
            UnityObjectField objectData = UnityObjectFieldValue.GetValueA(heroKitObject, 2);
            string scriptName = (objectData.value != null) ? objectData.value.name : "";

            bool runThis = (targetObject != null && scriptName != "");

            if (runThis)
            {
                ExecuteOnTarget(targetObject, scriptName);
            }

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "MonoScript: " + scriptName;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, string scriptName)
        {
            MonoBehaviour component = HeroKitCommonRuntime.GetComponentFromScript(heroKitObject, targetObject, scriptName);
            FieldInfoValue.SetValueB(targetObject, 3, 4, component);
        }

        // Not used
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