// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Enables audio in the scene.
    /// </summary>
    public class TurnAudioOn : IHeroKitAction
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
        public static TurnAudioOn Create()
        {
            TurnAudioOn action = new TurnAudioOn();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            Camera camera = Camera.main;
            bool runThis = (camera != null);

            if (runThis)
            {
                AudioListener.volume = 1f; 
            }

            // show debug message
            if (heroKitObject.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));

            return -99;
        }

        // Not used
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