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
    /// Rotate an object a specific number of degrees.
    /// </summary>
    public class RotateXDegrees2D : IHeroKitAction
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
        public static RotateXDegrees2D Create()
        {
            RotateXDegrees2D action = new RotateXDegrees2D();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);           
            Vector3 degrees = new Vector3();
            degrees.x = IntegerFieldValue.GetValueA(heroKitObject, 2); // x is on purpose even though we are changing z
            int speed = IntegerFieldValue.GetValueA(heroKitObject, 3);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], speed, degrees);

            // set up update for long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = true;
            updateIsDone = false;
            heroKitObject.longActions.Add(this);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Speed: " + speed + "\n" +
                                      "Euler Angles: " + degrees;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, int speed, Vector3 degrees)
        {
            // set some values
            Transform transform = targetObject.GetHeroComponent<Transform>("transform");
            if (transform != null)
            {
                degrees = degrees + transform.localEulerAngles;

                // rotate the image
                rotateObject = targetObject.GetHeroComponent<HeroObjectRotateToDegrees>("HeroObjectRotateToDegrees", true);
                rotateObject.speed = speed;
                rotateObject.degrees = degrees;
                rotateObject.Initialize();
            }
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private HeroObjectRotateToDegrees rotateObject;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }

        // update the action
        public void Update()
        {
            if (rotateObject == null || !rotateObject.enabled)
                updateIsDone = true;
        }
    }
}