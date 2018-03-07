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
    /// Change the position of an object in the scene.
    /// </summary>
    public class ChangePosition : IHeroKitAction
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
        public static ChangePosition Create()
        {
            ChangePosition action = new ChangePosition();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            string childName = ChildObjectValue.GetValue(heroKitObject, 2, 3);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], childName);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Child (if used): " + childName + "\n" +
                                      "Position to Change: " + position;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        private Vector3 position = new Vector3();
        public void ExecuteOnTarget(HeroKitObject targetObject, string childName)
        {
            Transform transform = null;
            if (childName == "")
                transform = targetObject.transform;
            else
                transform = targetObject.GetHeroChildComponent<Transform>("Transform", childName);


            Vector3 pos = transform.localPosition;

            // get the values to update
            bool changeX = BoolValue.GetValue(heroKitObject, 4);
            if (changeX)
            {
                pos.x = FloatFieldValue.GetValueA(heroKitObject, 5);
                position.x = pos.x;
            }

            bool changeY = BoolValue.GetValue(heroKitObject, 6);
            if (changeY)
            {
                pos.y = FloatFieldValue.GetValueA(heroKitObject, 7);
                position.y = pos.y;
            } 

            bool changeZ = BoolValue.GetValue(heroKitObject, 8);
            if (changeZ)
            {
                pos.z = FloatFieldValue.GetValueA(heroKitObject, 9);
                position.z = pos.z;
            }

            // update the transform
            transform.localPosition = pos;
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