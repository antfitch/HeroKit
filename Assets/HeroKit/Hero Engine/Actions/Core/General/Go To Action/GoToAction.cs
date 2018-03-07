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
    /// Go to a specific action in an event.
    /// </summary>
    public class GoToAction : IHeroKitAction
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
        public static GoToAction Create()
        {
            GoToAction action = new GoToAction();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // assign variables
            heroKitObject = hko;
            eventID = heroKitObject.heroStateData.eventBlock;  
            int actionCount = heroKitObject.heroState.heroEvent[eventID].actions.Count;

            // the action to skip to
            int nextActionID = IntegerFieldValue.GetValueA(heroKitObject, 0);

            if (hko.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, nextActionID.ToString()));
            return nextActionID+1;
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