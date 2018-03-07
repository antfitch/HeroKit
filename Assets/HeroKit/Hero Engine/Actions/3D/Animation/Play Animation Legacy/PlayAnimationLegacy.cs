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
    /// Activate a trigger in an animator.
    /// </summary>
    public class PlayAnimationLegacy : IHeroKitAction
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
        public static PlayAnimationLegacy Create()
        {
            PlayAnimationLegacy action = new PlayAnimationLegacy();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            string animationName = AnimationParameterValue.GetValueA(heroKitObject, 2, 3, 4);
            bool runThis = (animationName != "" && targetObject != null);

            if (runThis)
            {
                // set up long data for animations
                animInfo = new LegacyAnimationInfo[targetObject.Length];

                // execute action for all objects in list
                for (int i = 0; runThis && i < targetObject.Length; i++)
                    ExecuteOnTarget(targetObject[i], animationName, i);
            }

            // set up the long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = true;
            updateIsDone = false;
            heroKitObject.longActions.Add(this);

            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = animationName + ": " + "on";
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, string animationName, int index)
        {
            // get the animator component
            Animation animation = targetObject.GetHeroChildComponent<Animation>("Animation", HeroKitCommonRuntime.visualsName);
            animInfo[index].animation = animation;
            animInfo[index].name = animationName;
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        LegacyAnimationInfo[] animInfo;

        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }
        public void Update()
        {
            for (int i = 0; i < animInfo.Length; i++)
            {
                if (animInfo[i].animation == null)
                    updateIsDone = true;

                animInfo[i].animation.CrossFade(animInfo[i].name, 0.1f);

                //Debug.Log(animation[animationName].time + " " + animation[animationName].length);

                if (!animInfo[i].started && animInfo[i].animation[animInfo[i].name].time != 0)
                    animInfo[i].started = true;

                if (animInfo[i].started && animInfo[i].animation[animInfo[i].name].time == 0)
                    animInfo[i].stopped = true;

                if (animInfo[i].stopped)
                    updateIsDone = true;

                if (updateIsDone)
                    break;
            }
        }
    }

    struct LegacyAnimationInfo
    {
        public Animation animation;
        public string name;
        public bool started;
        public bool stopped;
    }
}