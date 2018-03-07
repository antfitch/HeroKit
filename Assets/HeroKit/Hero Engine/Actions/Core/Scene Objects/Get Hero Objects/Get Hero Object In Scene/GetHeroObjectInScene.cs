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
    /// Get hero kit objects from the scene.
    /// </summary>
    public class GetHeroObjectInScene : IHeroKitAction
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
        public static GetHeroObjectInScene Create()
        {
            GetHeroObjectInScene action = new GetHeroObjectInScene();
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
            int heroGuid = HeroObjectFieldValue.GetValueD(heroKitObject, 3);

            // filter the hero kit objects in the scene
            List<HeroKitObject> heroKitObjects = HeroActionCommonRuntime.GetHeroObjectsByGUID(HeroActionCommonRuntime.GetHeroObjectsInScene(), objectCount, heroGuid);

            // assign the hero kit objects to the list
            HeroActionCommonRuntime.AssignObjectsToList(heroKitObject, getHeroFieldID, heroKitObjects, actionType);

            //-----------------------------------------
            // debugging stuff
            //-----------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Hero Object GUID: " + heroGuid + "\n" +
                                      "Max number to return: " + objectCount;
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