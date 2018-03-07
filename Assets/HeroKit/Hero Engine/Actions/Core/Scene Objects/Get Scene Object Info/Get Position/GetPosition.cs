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
    /// Get the position of an object in the scene.
    /// </summary>
    public class GetPosition : IHeroKitAction
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
        public static GetPosition Create()
        {
            GetPosition action = new GetPosition();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            SceneObjectValueData data = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            GameObject[] targetObject = HeroKitCommonRuntime.GetGameObjectsFromSceneObjects(data);
            Vector3 position = new Vector3();
            bool changeX = false;
            bool changeY = false;
            bool changeZ = false;
            bool runThis = (targetObject != null && targetObject.Length > 0);

            // get value from first game object in list
            if (runThis)
            {
                position = targetObject[0].transform.localPosition;

                changeX = BoolValue.GetValue(heroKitObject, 2);
                if (changeX) FloatFieldValue.SetValueB(heroKitObject, 3, position.x);

                changeY = BoolValue.GetValue(heroKitObject, 4);
                if (changeY) FloatFieldValue.SetValueB(heroKitObject, 5, position.y);

                changeZ = BoolValue.GetValue(heroKitObject, 6);
                if (changeZ) FloatFieldValue.SetValueB(heroKitObject, 7, position.z);
            }

            //-----------------------------------------
            // debugging stuff
            //-----------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string xStr = (changeX) ? "X: " + position.x + " " : "";
                string yStr = (changeY) ? "Y: " + position.y + " " : "";
                string zStr = (changeZ) ? "Z: " + position.z + " " : "";
                string debugMessage = "Position coordinates: " + xStr + yStr + zStr;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
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