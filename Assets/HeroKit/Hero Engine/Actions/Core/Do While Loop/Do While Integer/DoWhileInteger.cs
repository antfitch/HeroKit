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
    /// A conditional statement that checks if the result is true.
    /// If the result is true, actions inside the conditional statement are executed until the conditional statement is no longer true.
    /// </summary>
    public class DoWhileInteger : IHeroKitAction
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
        public static DoWhileInteger Create()
        {
            DoWhileInteger action = new DoWhileInteger();
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
            int actionCount = heroKitObject.heroState.heroEvent[eventID].actions.Count;
            int nextAction = -99;

            // get the conditional action that follows this action in the if / if else / else / end sequence
            int nextConditionalAction = HeroActionCommonRuntime.GetNextConditionAction(heroKitObject, eventID, actionID, currentIndent);

            // evaluate the if statement
            int comparison = DropDownListValue.GetValue(heroKitObject, 2);
            int value1 = IntegerFieldValue.GetValueB(heroKitObject, 1);
            int value2 = IntegerFieldValue.GetValueA(heroKitObject, 3);
            bool evaluation = HeroActionCommonRuntime.CompareIntegers(comparison, value1, value2);

            // if there are no actions inside a do while loop, show error message and break out of loop
            if (actionID == nextConditionalAction)
            {
                Debug.LogWarning("Loop has no actions! Breaking out of loop early.");
                heroKitObject.heroState.heroEvent[eventID].actions[nextConditionalAction + 1].actionFields[0].bools[0] = false;
                return -99;
            }

            // if the conditional action that follows is an End Do While, we need to set a value.
            if (nextConditionalAction != -99)
            {
                if (heroKitObject.heroState.heroEvent[eventID].actions[nextConditionalAction + 1].actionTemplate.name == "End Do While")
                {
                    // Was beginning of do while a success? Assign this to the End Do While action.
                    heroKitObject.heroState.heroEvent[eventID].actions[nextConditionalAction + 1].actionFields[0].bools[0] = evaluation;

                    // if sucess was true, what is the id assigned to beginning of do while?
                    heroKitObject.heroState.heroEvent[eventID].actions[nextConditionalAction + 1].actionFields[0].ints[0] = actionID;
                }
            }

            // if evaluation is false, go to end of loop.
            if (!evaluation)
                nextAction = nextConditionalAction;

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = heroKitObject.heroState.heroEvent[eventID].actions[actionID].name + " = " + evaluation;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            // return the action that we want the game loop to think just executed
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