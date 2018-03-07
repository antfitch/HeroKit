﻿// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// A conditional statement that checks if the result is true (string) (if the string contains another string).
    /// If the result is true, actions inside the conditional statement are executed.
    /// Otherwise, the actions are skipped.
    /// </summary>
    public class IfStringContains : IHeroKitAction
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
        public static IfStringContains Create()
        {
            IfStringContains action = new IfStringContains();
            return action;
        }

        // Check to see if an integer meets certain conditions in an if statement
        // This is used by both If and Else If
        public int Execute(HeroKitObject hko)
        {
            // assign variables
            heroKitObject = hko;
            eventID = heroKitObject.heroStateData.eventBlock;
            int actionID = heroKitObject.heroStateData.action;
            int currentIndent = heroKitObject.heroState.heroEvent[eventID].actions[actionID].indent;

            // evaluate the if statement
            int comparison = DropDownListValue.GetValue(heroKitObject, 2);
            string value1 = StringFieldValue.GetValueB(heroKitObject, 1);
            string value2 = StringFieldValue.GetValueA(heroKitObject, 3);
            //bool evaluation = HeroActionCommonRuntime.CompareStrings(comparison, value1, value2);
            bool evaluation = value1.Contains(value2);

            // next we need to get the action that we want the game loop to think just executed
            // this checks to see if the if statement should be run
            // if it should run, it disables the next conditional action if it is "Else" or "Else If" 
            int thisAction = HeroActionCommonRuntime.RunConditionalIfAction(heroKitObject, eventID, actionID, currentIndent, evaluation);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "String A: " + value1 + "\n" +
                                      "String B: " + value2 + "\n" +
                                      "Result: " + evaluation;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            // return the action that we want the game loop to think just executed
            return thisAction;
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