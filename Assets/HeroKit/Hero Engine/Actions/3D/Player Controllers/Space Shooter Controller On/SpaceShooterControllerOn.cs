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
    /// Enable player controller A.
    /// </summary>
    public class SpaceShooterControllerOn : IHeroKitAction
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
        public static SpaceShooterControllerOn Create()
        {
            SpaceShooterControllerOn action = new SpaceShooterControllerOn();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            int speed = IntegerFieldValue.GetValueA(heroKitObject, 2);
            int animationType = DropDownListValue.GetValue(heroKitObject, 3);
            string walkAnimation = AnimationParameterValue.GetValueA(heroKitObject, 4, 5, 6);
            string idleAnimation = (animationType == 2) ? AnimationParameterValue.GetValueA(heroKitObject, 7, 8, 9) : "";
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], speed, animationType, walkAnimation, idleAnimation);

            if (heroKitObject.debugHeroObject)
            {
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, int speed, int animationType, string walkAnimation, string idleAnimation)
        {
            // get the movement script
            SpaceShooterController3D moveObject = targetObject.GetHeroComponent<SpaceShooterController3D>("SpaceShooterController3D", true);
            moveObject.settings.animationType = animationType;
            moveObject.settings.speed = speed;
            moveObject.settings.moveDefault = walkAnimation;
            moveObject.settings.lookDefault = idleAnimation;
            moveObject.enabled = true;
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