// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Else condition in an If condition statement.
    /// This is called if the previous condition in the If statement was false.
    /// </summary>
    public class Else : IHeroKitAction
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
        public static Else Create()
        {
            Else action = new Else();
            return action;
        }

        // No actions for the End action
        public int Execute(HeroKitObject hko)
        {
            // assign variables
            heroKitObject = hko;
            eventID = heroKitObject.heroStateData.eventBlock;
            int actionID = heroKitObject.heroStateData.action;
            int currentIndent = heroKitObject.heroState.heroEvent[eventID].actions[actionID].indent;
            int actionCount = heroKitObject.heroState.heroEvent[eventID].actions.Count;

            // should we skip this action?
            bool skipThis = HeroActionCommonRuntime.SkipConditionalAction(heroKitObject, eventID, actionID);

            // skip to the next conditional action 
            if (skipThis)
            {
                // get the conditional action that follows this action in the if / if else / else / end sequence
                int nextConditionalAction = HeroActionCommonRuntime.GetNextConditionAction(heroKitObject, eventID, actionID, currentIndent);

                // get the next conditinal action and set its "skip me" flag if it should be skipped.
                HeroActionCommonRuntime.TurnOffConditionalAction(heroKitObject, eventID, nextConditionalAction, HeroActionCommonRuntime.actionsToSkipIf);

                if (heroKitObject.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));
                return nextConditionalAction;
            }

            // containue to next action
            else
            {
                if (heroKitObject.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));
                return -99;
            }

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