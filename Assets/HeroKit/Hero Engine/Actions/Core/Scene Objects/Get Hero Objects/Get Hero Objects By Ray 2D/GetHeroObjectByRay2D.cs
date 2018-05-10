// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{
    /// <summary>
    /// Get hero kit objects from the scene. Only get objects that are hit by a ray.
    /// </summary>
    public class GetHeroObjectByRay2D : IHeroKitAction
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
        public static GetHeroObjectByRay2D Create()
        {
            GetHeroObjectByRay2D action = new GetHeroObjectByRay2D();
            return action;
        }

        // Gets objects in a scene that match a certerin criteria
        public int Execute(HeroKitObject hko)
        {
            // Get variables
            heroKitObject = hko;

            //-----------------------------------------
            // Get the game objects in the scene that match specific parameters
            //-----------------------------------------
            int actionType = DropDownListValue.GetValue(heroKitObject, 0);
            int getHeroFieldID = 1;
            int objectCount = IntegerFieldValue.GetValueA(heroKitObject, 2);
            int rayDirectionType = DropDownListValue.GetValue(heroKitObject, 4);
            float rayDistance = IntegerFieldValue.GetValueA(heroKitObject, 5) * 0.1f;
            int rayType = DropDownListValue.GetValue(heroKitObject, 6);
            bool debugRay = BoolValue.GetValue(heroKitObject, 9);
            GameObject originObject = null;
            HeroKitObject targetObject = null;

            // ray is on a specific camera
            if (rayType == 2)
            {
                targetObject = HeroObjectFieldValue.GetValueA(heroKitObject, 3)[0];
                if (targetObject != null) originObject = targetObject.gameObject;
            }
            // ray is on hero object
            else if (rayType == 3)
            {
                targetObject = HeroObjectFieldValue.GetValueA(heroKitObject, 3)[0];
                string childName = ChildObjectValue.GetValue(heroKitObject, 7, 8);

                Transform transform = null;
                if (childName == "")
                    transform = targetObject.transform;
                else
                    transform = targetObject.GetHeroChildComponent<Transform>("Transform", childName);

                originObject = transform.gameObject;
            }

            // filter the hero kit objects in the scene
            List<HeroKitObject> filteredObjects = HeroActionCommonRuntime.GetHeroObjectsRay2D(objectCount, targetObject, originObject, rayType, rayDirectionType, rayDistance, debugRay);

            // assign the hero kit objects to the list
            HeroActionCommonRuntime.AssignObjectsToList(heroKitObject, getHeroFieldID, filteredObjects, actionType);

            //-----------------------------------------
            // debugging stuff
            //-----------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string countStr = (filteredObjects != null) ? filteredObjects.Count.ToString() : 0.ToString();
                string debugMessage = "Maximum number of objects to get: " + objectCount + "\n" +
                                      "Objects found: " + countStr;
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