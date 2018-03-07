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
    /// [Summary about what your hero action does.]
    /// </summary>
    public class HeroActionTemplate : IHeroKitAction
    {
        //--------------------------------------------------
        // HOW TO MODIFY FOR YOUR OWN ACTION
        // 1. Change the name of the class from HeroActionTemplate to the name of your hero action (ex. GetRay).
        // 2. Don't make any changes in section A.
        // 3. In section B, replace the THREE HeroActionTemplate references to the name of your class (ex. GetRay).
        // 4. In section C, read the notes in this section carefully.
        // 4. In secttion D, ignore these unless you are making a long action (if you are making a long action, see the long action template for this).
        //--------------------------------------------------

        //--------------------------------------------------
        // SECTION A
        //--------------------------------------------------

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

        //--------------------------------------------------
        // SECTION B
        //--------------------------------------------------

        // This is used by HeroKitCommon.GetAction() to add this action to the ActionDictionary. Don't delete!
        public static HeroActionTemplate Create()
        {
            HeroActionTemplate action = new HeroActionTemplate();
            return action;
        }

        //--------------------------------------------------
        // SECTION C
        //--------------------------------------------------

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // assign values (don't change)
            heroKitObject = hko;

            // replace this part with with your fields & code
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            bool boolValue = BoolFieldValue.GetValueA(heroKitObject, 3);

            // check to see if we can run this action on one or more hero game objects (don't change if this action can be used on more than one hero game object).
            bool runThis = (targetObject != null);

            // execute action for all objects in list (don't change if this action can be used on more than one hero game object).
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], boolValue);

            // show debug message for this action (add a custom message if desired or delete the custom message).
            if (hko.debugHeroObject)
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, "custom message"));

            // the return code (don't change)
            return -99;
        }

        // Execute the action on a target hero game object
        public void ExecuteOnTarget(HeroKitObject targetObject, bool boolValue)
        {
            // replace this with your fields
            BoolFieldValue.SetValueB(targetObject, 2, boolValue);
        }

        //--------------------------------------------------
        // SECTION D
        //--------------------------------------------------

        // These are ignored because this is not an action that needs to continue for multiple frames.
        // However, do not remove these. They are required for all actions.
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