// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Wait X number of frames before performing the next action.
    /// </summary>
    public class WaitFrames : IHeroKitAction
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
        public static WaitFrames Create()
        {
            WaitFrames action = new WaitFrames();
            return action;
        }

        // Pauses an event for X amount of time before going on to the next action
        public int Execute(HeroKitObject hko)
        {
            // assign variables
            heroKitObject = hko;
            framesToWait = IntegerFieldValue.GetValueA(heroKitObject, 0);
            currentFrame = 0;

            // set up update for long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = true;
            updateIsDone = false;
            heroKitObject.longActions.Add(this);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Frames to wait: " + framesToWait;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;       
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private int currentFrame;
        private int framesToWait;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }

        // update the action
        public void Update()
        {
            currentFrame += 1;

            if (currentFrame >= framesToWait) updateIsDone = true;

            //Debug.Log("tick");
        }
    }
}