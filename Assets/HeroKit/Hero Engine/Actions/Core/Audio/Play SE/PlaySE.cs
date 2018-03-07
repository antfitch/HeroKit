// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.ActionField;
using HeroKit.Scene.Scripts;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Play a sound effect
    /// </summary>
    public class PlaySE : IHeroKitAction
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
        public static PlaySE Create()
        {
            PlaySE action = new PlaySE();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 4);
            UnityObjectField unityObject = UnityObjectFieldValue.GetValueA(heroKitObject, 1);
            AudioClip audioClip = (unityObject.value != null) ? (AudioClip)unityObject.value : null;
            bool fadeBGM = BoolValue.GetValue(heroKitObject, 2);
            bool fadeBGS = BoolValue.GetValue(heroKitObject, 3);
            bool changeSettings = BoolValue.GetValue(heroKitObject, 5);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], audioClip, fadeBGM, fadeBGS, changeSettings);

            // set up the long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = true;
            updateIsDone = false;
            heroKitObject.longActions.Add(this);

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "SE: " + audioClip;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, AudioClip audioClip, bool fadeBGM, bool fadeBGS, bool changeSettings)
        {
            AudioSource audioSourceBGM = null;
            AudioSource audioSourceBGS = null;
            AudioSource audioSourceSE = null;

            // change settings if needed
            if (changeSettings)
            {
                HeroActionCommonRuntime.ChangeAudioSettings(heroKitObject, targetObject, "AudioSource-SE", 6);
            }

            // get the audio clip
            audioSourceSE = targetObject.GetHeroComponent<AudioSource>("AudioSource-SE", true, true);

            // get fade BGM settings           
            if (fadeBGM)
            {
                audioSourceBGM = targetObject.GetHeroComponent<AudioSource>("AudioSource-BGM", false);
            }

            // get fade BGS settings           
            if (fadeBGS)
            {
                audioSourceBGS = targetObject.GetHeroComponent<AudioSource>("AudioSource-BGS", false);
            }

            // get the sound effects script
            heroObjectSE = targetObject.GetHeroComponent<HeroSoundEffect>("HeroSoundEffect", true);

            // set up sound effects script
            heroObjectSE.audioClip = audioClip;
            heroObjectSE.fadeBGM = fadeBGM;
            heroObjectSE.fadeBGS = fadeBGS;
            heroObjectSE.audioSourceBGM = audioSourceBGM;
            heroObjectSE.audioSourceBGS = audioSourceBGS;
            heroObjectSE.audioSourceSE = audioSourceSE;
            heroObjectSE.Initialize();
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private HeroSoundEffect heroObjectSE;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }

        // update the action
        public void Update()
        {
            if (heroObjectSE == null || !heroObjectSE.enabled)
                updateIsDone = true;
        }
    }
}