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
    /// Gets a game object that triggered an input event (ex: button press, collision) on a game object with a hero kit listener.
    /// </summary>
    public class GetSourceHeroObject : IHeroKitAction
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
        public static GetSourceHeroObject Create()
        {
            GetSourceHeroObject action = new GetSourceHeroObject();
            return action;
        }

        // Gets objects in a scene that match a certerin criteria
        public int Execute(HeroKitObject hko)
        {
            // Get variables
            heroKitObject = hko;
            eventID = hko.heroStateData.eventBlock;

            // get the source object
            GameObject sourceGameObject = heroKitObject.heroState.heroEvent[eventID].messenger;
            HeroKitObject sourceHeroObject = null;
            bool runThis = (sourceGameObject != null);

            // get the hero object          
            if (runThis)
            {
                sourceHeroObject = heroKitObject.GetGameObjectComponent<HeroKitObject>("HeroKitObject", false, sourceGameObject);
                List<HeroKitObject> heroObjectList = new List<HeroKitObject>();
                heroObjectList.Add(sourceHeroObject);

                // save hero object
                HeroObjectFieldValue.SetValueB(heroKitObject, 0, heroObjectList);
            }

            //-----------------------------------------
            // debugging stuff
            //-----------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Hero Object: " + sourceHeroObject;
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