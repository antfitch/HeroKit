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
    /// Morph a hero kit object into another hero kit object.
    /// </summary>
    public class MorphHeroObject : IHeroKitAction
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
        public static MorphHeroObject Create()
        {
            MorphHeroObject action = new MorphHeroObject();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            HeroObject heroObjectType = HeroObjectFieldValue.GetValueC(heroKitObject, 2);
            bool runThis = (targetObject != null && heroObjectType != null);

            // note: debug info needs to go first for this action because
            // if you put it after hero object is changed, the wrong action is printed to log
            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Morphed into this Hero Object Type: " + heroObjectType;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], heroObjectType);

            return -89;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, HeroObject heroObjectType)
        {
            targetObject.ChangeHeroObject(heroObjectType);
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