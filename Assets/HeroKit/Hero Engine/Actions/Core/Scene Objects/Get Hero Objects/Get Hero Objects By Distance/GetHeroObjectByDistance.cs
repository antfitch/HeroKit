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
    /// Get hero kit objects from the scene. Only get objects that are in range of a specific coordinate.
    /// </summary>
    public class GetHeroObjectByDistance : IHeroKitAction
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
        public static GetHeroObjectByDistance Create()
        {
            GetHeroObjectByDistance action = new GetHeroObjectByDistance();
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
            GameObject targetObject = HeroObjectFieldValue.GetValueA(heroKitObject, 3)[0].gameObject;

            bool getX = BoolValue.GetValue(heroKitObject, 4);
            bool getY = BoolValue.GetValue(heroKitObject, 5);
            bool getZ = BoolValue.GetValue(heroKitObject, 6);

            float x = (getX) ? FloatFieldValue.GetValueA(heroKitObject, 7) : 0;
            float y = (getY) ? FloatFieldValue.GetValueA(heroKitObject, 8) : 0;
            float z = (getZ) ? FloatFieldValue.GetValueA(heroKitObject, 9) : 0;

            Vector3 radius = new Vector3(x, y, z);

            // filter the hero kit objects in the scene
            List<HeroKitObject> filteredObjects = HeroActionCommonRuntime.GetHeroObjectsDistance(heroKitObject, HeroActionCommonRuntime.GetHeroObjectsInScene(), objectCount, radius, getX, getY, getZ, targetObject);

            // assign the hero kit objects to the list
            HeroActionCommonRuntime.AssignObjectsToList(heroKitObject, getHeroFieldID, filteredObjects, actionType);

            //-----------------------------------------
            // debugging stuff
            //-----------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string xStr = (getX) ? "X: " + x + " " : "";
                string yStr = (getY) ? "Y: " + y + " " : "";
                string zStr = (getZ) ? "Z: " + z + " " : "";
                string countStr = (filteredObjects != null) ? filteredObjects.Count.ToString() : 0.ToString();
                string debugMessage = "Get objects in this radius: " + xStr + yStr + zStr + "\n" +
                                      "Maximum number of objects to get: " + objectCount + "\n" +
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