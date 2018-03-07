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
    /// Play a background music clip.
    /// </summary>
    public class PlayBGM : IHeroKitAction
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
        public static PlayBGM Create()
        {
            PlayBGM action = new PlayBGM();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            UnityObjectField unityObject = UnityObjectFieldValue.GetValueA(heroKitObject, 2);
            AudioClip audioClip = (unityObject.value != null) ? (AudioClip)unityObject.value : null;
            bool changeSettings = BoolValue.GetValue(heroKitObject, 3);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], audioClip, changeSettings);

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "BGM: " + audioClip;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, AudioClip audioClip, bool changeSettings)
        {
            // change settings if needed
            if (changeSettings)
            {
                HeroActionCommonRuntime.ChangeAudioSettings(heroKitObject, targetObject, "AudioSource-BGM", 4);
            }

            // play the audio clip
            AudioSource audioSource = targetObject.GetHeroComponent<AudioSource>("AudioSource-BGM", true, true);
            audioSource.loop = true;
            audioSource.playOnAwake = true;
            audioSource.clip = audioClip;
            audioSource.Play();
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