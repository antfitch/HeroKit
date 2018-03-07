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
    /// Adds two integers and saves the result in a third integer.
    /// </summary>
    public class ChangeInteger : IHeroKitAction
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
        public static ChangeInteger Create()
        {
            ChangeInteger action = new ChangeInteger();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            int intA = IntegerFieldValue.GetValueA(heroKitObject, 1);
            int intB = IntegerFieldValue.GetValueA(heroKitObject, 3);
            int operation = DropDownListValue.GetValue(heroKitObject, 2);
            int result = HeroActionCommonRuntime.PerformMathOnIntegers(operation, intA, intB);
            IntegerFieldValue.SetValueB(heroKitObject, 0, result);

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "A: " + intA + "\n" +
                                      "B: " + intB + "\n" +
                                      "Result (C): " + result;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }
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