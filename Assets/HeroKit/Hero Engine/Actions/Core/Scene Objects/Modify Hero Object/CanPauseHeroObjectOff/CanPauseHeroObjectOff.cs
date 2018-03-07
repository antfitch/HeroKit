// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Hero kit object will not pause when the game is paused.
    /// </summary>
    public class CanPauseHeroObjectOff : IHeroKitAction
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
        public static CanPauseHeroObjectOff Create()
        {
            CanPauseHeroObjectOff action = new CanPauseHeroObjectOff();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            heroKitObject.doNotPause = true;

            if (heroKitObject.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));

            return -99;
        }

        // not used
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