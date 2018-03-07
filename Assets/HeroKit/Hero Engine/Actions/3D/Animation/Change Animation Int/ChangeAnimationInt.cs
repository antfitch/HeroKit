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
    /// Change an integer in an animator.
    /// </summary>
    public class ChangeAnimationInt : IHeroKitAction
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
        public static ChangeAnimationInt Create()
        {
            ChangeAnimationInt action = new ChangeAnimationInt();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            string animationName = AnimationParameterValue.GetValueA(heroKitObject, 2, 3, 4);
            int intVal = IntegerFieldValue.GetValueA(heroKitObject, 5);
            bool runThis = (animationName != "" && targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], animationName, intVal);

            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = animationName + ": " + intVal;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, string animationName, int intVal)
        {
            // get the animator component
            Animator animator = targetObject.GetHeroChildComponent<Animator>("Animator", HeroKitCommonRuntime.visualsName);

            // animate the target
            if (animator != null)
                animator.SetInteger(animationName, intVal);
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