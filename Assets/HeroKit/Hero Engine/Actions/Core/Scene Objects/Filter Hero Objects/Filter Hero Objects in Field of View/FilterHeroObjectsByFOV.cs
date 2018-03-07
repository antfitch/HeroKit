﻿// --------------------------------------------------------------
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
    /// Get specific hero objects in a hero object list. Only get objects that in the field of view of another object.
    /// </summary>
    public class FilterHeroObjectsByFOV : IHeroKitAction
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
        public static FilterHeroObjectsByFOV Create()
        {
            FilterHeroObjectsByFOV action = new FilterHeroObjectsByFOV();
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
            HeroKitObject originHKO = HeroObjectFieldValue.GetValueA(heroKitObject, 3)[0];
            List<HeroKitObject> listObjects = HeroObjectFieldValue.GetValueB(heroKitObject, 4);
            string childName = ChildObjectValue.GetValue(heroKitObject, 5, 6);
            int rayDirectionType = DropDownListValue.GetValue(heroKitObject, 7);
            int fieldOfView = IntegerFieldValue.GetValueA(heroKitObject, 8);

            // get origin object
            GameObject originObject = null;
            Transform transform = null;
            if (childName == "")
                transform = originHKO.transform;
            else
                transform = originHKO.GetHeroChildComponent<Transform>("Transform", childName);

            originObject = transform.gameObject;

            // convert list objects to array
            HeroKitObject[] arrayObjects = (listObjects != null) ? listObjects.ToArray() : null;

            // filter the hero kit objects in the scene
            List<HeroKitObject> filteredObjects = HeroActionCommonRuntime.GetHeroObjectsFOV(arrayObjects, objectCount, originObject, fieldOfView, rayDirectionType);

            // assign the hero kit objects to the list
            HeroActionCommonRuntime.AssignObjectsToList(heroKitObject, getHeroFieldID, filteredObjects, actionType);

            //-----------------------------------------
            // debugging stuff
            //-----------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string countStr = (filteredObjects != null) ? filteredObjects.Count.ToString() : 0.ToString();
                string name = (originObject != null) ? originObject.name : "";
                string debugMessage = "Get objects in field of view of this object: " + name + "\n" +
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