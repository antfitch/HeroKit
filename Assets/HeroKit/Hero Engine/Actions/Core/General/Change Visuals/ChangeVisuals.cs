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
    /// Change the prefab that contains 3D visuals on an object.
    /// </summary>
    public class ChangeVisuals : IHeroKitAction
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
        public static ChangeVisuals Create()
        {
            ChangeVisuals action = new ChangeVisuals();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            bool changePrefab = BoolValue.GetValue(heroKitObject, 2);
            GameObject prefab = PrefabValue.GetValue(heroKitObject, 3);
            bool changeRigidbody = BoolValue.GetValue(heroKitObject, 4);
            Rigidbody rigidbody = RigidbodyValue.GetValue(heroKitObject, 5);

            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], changePrefab, prefab, changeRigidbody, rigidbody);

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Result: " + prefab + " " + rigidbody;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, bool changePrefab, GameObject prefab, bool changeRigidbody, Rigidbody rigidbody)
        {
            if (changePrefab)
                HeroKitCommonRuntime.AddPrefab(targetObject.gameObject, prefab, false, HeroKitCommonRuntime.visualsName);

            if (changeRigidbody)
                HeroKitCommonRuntime.AddRigidbody(rigidbody, targetObject.gameObject);
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