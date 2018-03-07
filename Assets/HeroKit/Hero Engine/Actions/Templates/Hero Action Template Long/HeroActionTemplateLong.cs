// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// [Summary about what your hero action does.]
    /// </summary>
    public class HeroActionTemplateLong : IHeroKitAction
    {
        //--------------------------------------------------
        // HOW TO MODIFY FOR YOUR OWN ACTION
        // 1. Change the name of the class from HeroActionTemplateLong to the name of your hero action (ex. GetRay).
        // 2. Don't make any changes in section A.
        // 3. In section B, replace the THREE HeroActionTemplate references to the name of your class (ex. GetRay).
        // 4. In section C, read the notes in this section carefully.
        // 4. In secttion D, read the notes in this section carefully.
        //--------------------------------------------------

        //--------------------------------------------------
        // SECTION A
        //--------------------------------------------------

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

        //--------------------------------------------------
        // SECTION B
        //--------------------------------------------------

        // This is used by HeroKitCommon.GetAction() to add this action to the ActionDictionary. Don't delete!
        public static HeroActionTemplateLong Create()
        {
            HeroActionTemplateLong action = new HeroActionTemplateLong();
            return action;
        }

        //--------------------------------------------------
        // SECTION C
        //--------------------------------------------------

        // Pauses an event for X amount of time before going on to the next action
        public int Execute(HeroKitObject hko)
        {
            // assign variables (don't change)
            heroKitObject = hko;

            // replace this part with with your fields & code
            int secondsToWait = IntegerFieldValue.GetValueA(heroKitObject, 0);
            timeToWait = secondsToWait + Time.time;

            // set up update for long action (don't change)
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = true;
            updateIsDone = false;
            heroKitObject.longActions.Add(this); // if you want to update this action using FixedUpdate, change longActions to longActionsFixed

            // show debug message for this action (add a custom message if desired or delete the custom message).
            if (heroKitObject.debugHeroObject)
            {
                string customMessage = "Seconds to wait: " + secondsToWait;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, customMessage));
            }

            // the return code (don't change)
            return -99;       
        }

        //--------------------------------------------------
        // SECTION D
        //--------------------------------------------------

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------

        // these are variables that will be used while long action is active. you can change these for your action.
        private float currentTime;
        private float timeToWait;
        private float timeLeft;

        // has action completed? (don't remove any of this)
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }

        // update the action. (don't remove)
        // This is called every frame. 
        // When the long action is complete, set updateIsDone to true.
        public void Update()
        {
            currentTime = Time.time;
            timeLeft = timeToWait-currentTime;
            if (timeLeft <= 0) updateIsDone = true;
        }
    }
}