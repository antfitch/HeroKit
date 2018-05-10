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
    /// Enable player controller B.
    /// </summary>
    public class TopDownControllerOn2D : IHeroKitAction
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
        public static TopDownControllerOn2D Create()
        {
            TopDownControllerOn2D action = new TopDownControllerOn2D();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            int speed = IntegerFieldValue.GetValueA(heroKitObject, 2);
            int moveType = DropDownListValue.GetValue(heroKitObject, 3);  // 4-way or 8-way?
            int animationType = DropDownListValue.GetValue(heroKitObject, 4);  // 4-way or 8-way?
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], speed, moveType, animationType);

            if (heroKitObject.debugHeroObject)
            {
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, int speed, int moveType, int animationType)
        {
            // get the movement script
            HeroSettings2D moveSettings = targetObject.GetHeroComponent<HeroSettings2D>("HeroSettings2D", true);
            moveSettings.animType = (HeroSettings2D.AnimType)(animationType-1);
            moveSettings.moveType = (HeroSettings2D.MoveType)(moveType-1);
            moveSettings.animator = targetObject.GetHeroChildComponent<Animator>("Animator", HeroKitCommonRuntime.visualsName);
            moveSettings.moveSpeed = speed;
            moveSettings.faceDir = HeroSettings2D.FaceDir.down;

            RpgController2D moveObject = targetObject.GetHeroComponent<RpgController2D>("RpgController2D", true);
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