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
    /// Get a random float and save it in a hero kit object.
    /// </summary>
    public class RandomFloat : IHeroKitAction
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
        public static RandomFloat Create()
        {
            RandomFloat action = new RandomFloat();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            float bottom = FloatFieldValue.GetValueA(heroKitObject, 0);
            float top = FloatFieldValue.GetValueA(heroKitObject, 1);
            float result = HeroKitCommonRuntime.GetRandomFloat(bottom, top);
            FloatFieldValue.SetValueB(heroKitObject, 2, result);

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Float: " + result + "\n" +
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