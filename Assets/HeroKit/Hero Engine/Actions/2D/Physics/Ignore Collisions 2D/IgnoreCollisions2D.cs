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
    /// A collider on a hero object should ignore collisions with the environment and other objects.
    /// </summary>
    public class IgnoreCollisions2D : IHeroKitAction
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
        public static IgnoreCollisions2D Create()
        {
            IgnoreCollisions2D action = new IgnoreCollisions2D();
            return action;
        }

        // execute this action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            string childName = ChildObjectValue.GetValue(heroKitObject, 2, 3);
            int colliderType = DropDownListValue.GetValue(heroKitObject, 4);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], childName, colliderType);

            //-----------------------------------------
            // debugging stuff
            //-----------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));
            }

            // return value
            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, string childName, int colliderType)
        {
            switch (colliderType)
            {
                case 2: // box
                    BoxCollider2D boxCollider = targetObject.GetHeroChildComponent<BoxCollider2D>("BoxCollider2D", childName);
                    if (boxCollider != null) boxCollider.isTrigger = true;
                    break;
                case 3: // capsule
                    CapsuleCollider2D capsuleCollider = targetObject.GetHeroChildComponent<CapsuleCollider2D>("CapsuleCollider2D", childName);
                    if (capsuleCollider != null) capsuleCollider.isTrigger = true;
                    break;
                case 4: // circle
                    CircleCollider2D circleCollider = targetObject.GetHeroChildComponent<CircleCollider2D>("CircleCollider2D", childName);
                    if (circleCollider != null) circleCollider.isTrigger = true;
                    break;
            }
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