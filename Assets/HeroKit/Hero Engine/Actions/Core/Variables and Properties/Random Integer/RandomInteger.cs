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
    /// Get a random integer and save it in a hero kit object.
    /// </summary>
    public class RandomInteger : IHeroKitAction
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
        public static RandomInteger Create()
        {
            RandomInteger action = new RandomInteger();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            int bottom = IntegerFieldValue.GetValueA(heroKitObject, 0);
            int top = IntegerFieldValue.GetValueA(heroKitObject, 1);
            int result = HeroKitCommonRuntime.GetRandomInt(bottom, top);
            IntegerFieldValue.SetValueB(heroKitObject, 2, result);

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Integer: " + result + "\n" +
                                      "Bottom: " + bottom + "\n" +
                                      "Top: " + top;
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