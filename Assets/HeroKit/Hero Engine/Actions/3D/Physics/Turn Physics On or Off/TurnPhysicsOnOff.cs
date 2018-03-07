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
    /// Enable or disable physics on an object in the scene.
    /// </summary>
    public class TurnPhysicsOnOff : IHeroKitAction
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
        public static TurnPhysicsOnOff Create()
        {
            TurnPhysicsOnOff action = new TurnPhysicsOnOff();
            return action;
        }

        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            int objectState = DropDownListValue.GetValue(heroKitObject, 2);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], objectState);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));
            }

            // return next action
            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, int objectState)
        {
            // change the physics state of the rigidbody
            Rigidbody rigidbody = targetObject.GetHeroComponent<Rigidbody>("Rigidbody", true);

            if (objectState == 1)
            {
                rigidbody.isKinematic = false;
                rigidbody.WakeUp();
            }
            if (objectState == 2)
            {
                rigidbody.isKinematic = true;
            }
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