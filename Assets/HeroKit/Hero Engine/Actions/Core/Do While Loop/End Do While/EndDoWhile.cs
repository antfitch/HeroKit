// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// End of a Do While statement.
    /// </summary>
    public class EndDoWhile : IHeroKitAction
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
        public static EndDoWhile Create()
        {
            EndDoWhile action = new EndDoWhile();
            return action;
        }

        // No actions for the End action
        public int Execute(HeroKitObject hko)
        {
            // assign variables
            heroKitObject = hko;
            eventID = heroKitObject.heroStateData.eventBlock;
            int actionID = heroKitObject.heroStateData.action;
            int nextAction = -99;

            // the last time Do While action was hit, was the evaluation successful? 
            bool conditionsMet = heroKitObject.heroState.heroEvent[eventID].actions[actionID].actionFields[0].bools[0];

            // if conditions were met, go to beginning of loop and re-test conditions
            if (conditionsMet) nextAction = heroKitObject.heroState.heroEvent[eventID].actions[actionID].actionFields[0].ints[0]-1;

            heroKitObject.heroState.heroEvent[eventID].actions[actionID].actionFields[0].ints[1]++;

            //if (heroKitObject.heroState.heroEvent[eventID].actions[actionID].actionField[0].ints[1] > 4)
            //{
            //    Debug.Log("too many loops");
            //    return -99;
            //}                

            if (heroKitObject.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));

            return nextAction;
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