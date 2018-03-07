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
    /// Enable a component on an object.
    /// </summary>
    public class EnableComponent : IHeroKitAction
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
        public static EnableComponent Create()
        {
            EnableComponent action = new EnableComponent();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            SceneObjectValueData data = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            string childName = ChildObjectValue.GetValue(heroKitObject, 2, 3);
            string componentName = StringFieldValue.GetValueA(heroKitObject, 4);

            // get the game object to work with
            GameObject[] targetObject = HeroKitCommonRuntime.GetGameObjectsFromSceneObjects(data);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], childName, componentName);

            // debug info
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Component: " + componentName + "\n" +
                                      "Child (if updating component on child): " + childName;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(GameObject targetObject, string childName, string componentName)
        {
            // get transform to work with
            Transform targetTransform = (childName == "") ? targetObject.transform : targetObject.transform.Find(childName);

            // exit if transorm does not exist
            if (targetTransform == null) return;

            // update the components
            Component component = targetTransform.GetComponent(componentName);
            if (component != null)
            {
                Behaviour behaviour = (Behaviour)component;
                behaviour.enabled = true;
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