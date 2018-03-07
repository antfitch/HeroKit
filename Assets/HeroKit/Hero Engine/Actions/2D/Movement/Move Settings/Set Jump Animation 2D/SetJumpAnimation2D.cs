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
    /// Set the animation that will play when the object jumps.
    /// </summary>
    public class SetJumpAnimation2D : IHeroKitAction
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
        public static SetJumpAnimation2D Create()
        {
            SetJumpAnimation2D action = new SetJumpAnimation2D();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;

            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            bool changeStartAnim = BoolValue.GetValue(heroKitObject, 2);
            bool changeEndAnim = BoolValue.GetValue(heroKitObject, 6);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], changeStartAnim, changeEndAnim);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string jumpBegin = (moveObject != null) ? moveObject.jumpBegin : "";
                string jumpEnd = (moveObject != null) ? moveObject.jumpEnd : "";
                string debugMessage = "Jump Begin Animation: " + jumpBegin + "\n" +
                                      "Jump End Animation: " + jumpEnd;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        private HeroSettings2D moveObject;
        public void ExecuteOnTarget(HeroKitObject targetObject, bool changeStartAnim, bool changeEndAnim)
        {
            // get the animator component
            Animator animator = targetObject.GetHeroChildComponent<Animator>("Animator", HeroKitCommonRuntime.visualsName);
            if (animator != null)
            {
                // get the movement script
                moveObject = targetObject.GetHeroComponent<HeroSettings2D>("HeroSettings2D", true);

                if (changeStartAnim)
                {
                    moveObject.jumpBegin = AnimationParameterValue.GetValueA(heroKitObject, 3, 4, 5);
                }

                if (changeEndAnim)
                {
                    moveObject.jumpEnd = AnimationParameterValue.GetValueA(heroKitObject, 7, 8, 9);
                }

                //// disable jump script if it is not being used
                //if (!moveObject.haveJumped)
                //    moveObject.enabled = false;
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