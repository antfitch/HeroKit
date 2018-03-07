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
    /// Get specific hero objects in a hero object list. Only get objects that are assigned a specific hero property.
    /// </summary>
    public class FilterHeroObjectsByProperty : IHeroKitAction
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
        public static FilterHeroObjectsByProperty Create()
        {
            FilterHeroObjectsByProperty action = new FilterHeroObjectsByProperty();
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
            HeroKitProperty heroProperty = HeroPropertyValue.GetValue(heroKitObject, 3);
            List<HeroKitObject> list = HeroObjectFieldValue.GetValueB(heroKitObject, 4);
            List<HeroKitObject> filteredObjects = null;

            if (list != null)
            {
                HeroKitObject[] listObjects = list.ToArray();

                // filter the hero kit objects in the scene
                filteredObjects = HeroActionCommonRuntime.GetHeroObjectsByProperty(listObjects, objectCount, heroProperty);

                // assign the hero kit objects to the list
                HeroActionCommonRuntime.AssignObjectsToList(heroKitObject, getHeroFieldID, filteredObjects, actionType);
            }

            //-----------------------------------------
            // debugging stuff
            //-----------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string propertyStr = (heroProperty != null) ? heroProperty.name : "";
                string countStr = (filteredObjects != null) ? filteredObjects.Count.ToString() : 0.ToString();
                string debugMessage = "Get objects assigned to this hero property: " + propertyStr + "\n" +
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