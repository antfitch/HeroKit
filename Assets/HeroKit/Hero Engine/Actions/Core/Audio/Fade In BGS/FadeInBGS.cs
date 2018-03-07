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
    /// Fade in volume for a background sound clip.
    /// </summary>
    public class FadeInBGS : IHeroKitAction
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
        public static FadeInBGS Create()
        {
            FadeInBGS action = new FadeInBGS();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            float duration = FloatFieldValue.GetValueA(heroKitObject, 2);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], duration);

            // set up update for long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = true;
            updateIsDone = false;
            heroKitObject.longActions.Add(this);

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Duration: " + duration;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, float duration)
        {
            // stop the audio clip
            AudioSource audioSource = targetObject.GetHeroComponent<AudioSource>("AudioSource-BGS", false);

            // get the movement script
            audioFade = targetObject.GetHeroComponent<HeroAudioFade>("HeroAudioFade", true);

            // set up movement script
            audioFade.audioSource = audioSource;
            audioFade.fadeIn = true;
            audioFade.duration = duration;
            audioFade.Initialize();
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private HeroAudioFade audioFade;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }

        // update the action
        public void Update()
        {
            if (audioFade == null || !audioFade.enabled)
                updateIsDone = true;
        }
    }
}