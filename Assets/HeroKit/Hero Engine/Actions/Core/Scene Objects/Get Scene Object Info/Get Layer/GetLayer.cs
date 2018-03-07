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
    /// Get the layer assigned to an object.
    /// </summary>
    public class GetLayer : IHeroKitAction
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
        public static GetLayer Create()
        {
            GetLayer action = new GetLayer();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            SceneObjectValueData data = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            GameObject[] targetObject = HeroKitCommonRuntime.GetGameObjectsFromSceneObjects(data);
            bool runThis = (targetObject != null && targetObject.Length > 0);

            // get value from first game object in list
            if (runThis)
            {
                IntegerFieldValue.SetValueB(heroKitObject, 2, targetObject[0].layer);
            }

            // debug info
            if (heroKitObject.debugHeroObject)
            {
                string strLayer = (targetObject != null && targetObject.Length > 0 && targetObject[0] != null) ? targetObject[0].layer.ToString() : "";
                string debugMessage = "Layer: " + strLayer;
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