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
    /// Rotate an object to a specific degree.
    /// </summary>
    public class RotateToDegrees2D : IHeroKitAction
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
        public static RotateToDegrees2D Create()
        {
            RotateToDegrees2D action = new RotateToDegrees2D();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            int speed = IntegerFieldValue.GetValueA(heroKitObject, 3);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], speed);

            // set up update for long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = true;
            updateIsDone = false;
            heroKitObject.longActions.Add(this);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Speed: " + speed + "\n" +
                                      "Euler Angles: " + degreesToChange;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        private Vector3 degreesToChange = new Vector3();
        public void ExecuteOnTarget(HeroKitObject targetObject, int speed)
        {
            // set some values
            Transform transform = targetObject.GetHeroComponent<Transform>("transform");
            if (transform != null)
            {
                Vector3 degrees = transform.localEulerAngles;

                // get the target rotation
                // note: z and x are swapped on purpose
                degrees.x = IntegerFieldValue.GetValueA(heroKitObject, 2);
                degreesToChange.z = degrees.x;

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