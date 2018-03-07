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
    /// Make an object jump.
    /// </summary>
    public class Jump2DPlatformer : IHeroKitAction
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
        public static Jump2DPlatformer Create()
        {
            Jump2DPlatformer action = new Jump2DPlatformer();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            float jumpHeight = FloatFieldValue.GetValueA(heroKitObject, 2);
            int jumpDirection = DropDownListValue.GetValue(heroKitObject, 3);
            int directionForce = (jumpDirection > 1) ? IntegerFieldValue.GetValueA(heroKitObject, 4) : 0;
            wait = !BoolValue.GetValue(heroKitObject, 5);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], jumpHeight, jumpDirection, directionForce);

            // set up the long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = wait;
            updateIsDone = false;
            heroKitObject.longActions.Add(this);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Force: " + jumpHeight;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, float jumpHeight, int jumpDirection, int directionForce)
        {
            //directionForce
            float direction = 0;
            // go left
            if (jumpDirection == 2)
                direction = (float)(-1 * directionForce * 0.1);
            // go right
            if (jumpDirection == 3)
                direction = (float)(1 * directionForce * 0.1);

            // get the movement script
            moveObject = targetObject.GetHeroComponent<PlatformerMovement2D>("PlatformerMovement2D", true);
            moveObject.jumpHeight = jumpHeight;
            moveObject.directionForce = direction;
            moveObject.Move = moveObject.Jump;
            moveObject.Initialize();
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private PlatformerMovement2D moveObject;
        private bool wait;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject, wait);
        }

        // update the action
        public void Update()
        {
            if (moveObject == null || !moveObject.enabled)
                updateIsDone = true;
        }
    }
}