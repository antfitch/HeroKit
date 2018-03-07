// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using HeroKit.Scene.Scripts;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Set the animation that should play when the object moves.
    /// </summary>
    public class SetMoveAnimation : IHeroKitAction
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
        public static SetMoveAnimation Create()
        {
            SetMoveAnimation action = new SetMoveAnimation();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            string animationName = AnimationParameterValue.GetValueA(heroKitObject, 2, 3, 4);
            bool runThis = (targetObject != null && animationName != "");

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], animationName);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string moveAnimation = (moveObject != null) ? moveObject.settings.moveDefault : "";
                string debugMessage = "Move Animation: " + moveAnimation;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        private HeroMove3D moveObject;
        public void ExecuteOnTarget(HeroKitObject targetObject, string animationName)
        {
            // get the animator component
            Animator animator = targetObject.GetHeroChildComponent<Animator>("Animator", HeroKitCommonRuntime.visualsName);
            if (animator != null)
            {
                // get the movement script
                moveObject = targetObject.GetHeroComponent<HeroMove3D>("HeroMove3D", true);
                moveObject.settings.moveDefault = animationName;
            }
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------

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