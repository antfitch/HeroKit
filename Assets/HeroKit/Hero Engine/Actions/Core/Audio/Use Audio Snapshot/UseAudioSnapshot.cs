// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEngine.Audio;
using System;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{
    /// <summary>
    /// Use an audio snapshot to control settings for a group of audio clips.
    /// </summary>
    public class UseAudioSnapshot : IHeroKitAction
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
        public static UseAudioSnapshot Create()
        {
            UseAudioSnapshot action = new UseAudioSnapshot();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            eventID = heroKitObject.heroStateData.eventBlock;
            AudioMixerSnapshot snapshot = ObjectValue.GetValue<AudioMixerSnapshot>(heroKitObject, 0);
            bool runThis = (snapshot != null);

            if (runThis)    
                snapshot.TransitionTo(.01f);

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Snapshot: " + snapshot;
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