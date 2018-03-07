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
    /// Get specific hero objects in a hero object list. Only get objects that are at a specific position.
    /// </summary>
    public class FilterHeroObjectsByPosition : IHeroKitAction
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
        public static GetHeroObjectByPosition Create()
        {
            GetHeroObjectByPosition action = new GetHeroObjectByPosition();
            return action;
        }

        // Gets objects in a scene that match a certerin criteria
        public int Execute(HeroKitObject hko)
        {
            // Get variables
            heroKitObject = hko;
            eventID = heroKitObject.heroStateData.eventBlock;
            int actionCount = heroKitObject.heroState.heroEvent[eventID].actions.Count;

            //-----------------------------------------
            // Get the game objects in the scene that match specific parameters
            //-----------------------------------------
            int actionType = DropDownListValue.GetValue(heroKitObject, 0);
            int getHeroFieldID = 1;
            int objectCount = IntegerFieldValue.GetValueA(heroKitObject, 2);
            List<HeroKitObject> listObjects = HeroObjectFieldValue.GetValueB(heroKitObject, 10);

            // convert list objects to array
            HeroKitObject[] arrayObjects = (listObjects != null) ? listObjects.ToArray() : null;

            bool getX = BoolValue.GetValue(heroKitObject, 3);
            bool getY = BoolValue.GetValue(heroKitObject, 4);
            bool getZ = BoolValue.GetValue(heroKitObject, 5);

            float x = (getX) ? FloatFieldValue.GetValueA(heroKitObject, 6) : 0;
            float y = (getY) ? FloatFieldValue.GetValueA(heroKitObject, 7) : 0;
            float z = (getZ) ? FloatFieldValue.GetValueA(heroKitObject, 8) : 0;

            float radius = FloatFieldValue.GetValueA(heroKitObject, 9);
            Vector3 pos = new Vector3(x, y, z);

            // filter the hero kit objects in the scene
            List<HeroKitObject> filteredObjects = HeroActionCommonRuntime.GetHeroObjectsPosition(heroKitObject, arrayObjects, objectCount, pos, getX, getY, getZ, radius);

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
                string debugMessage = "Get objects at this position: " + xStr + yStr + zStr + "\n" +
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