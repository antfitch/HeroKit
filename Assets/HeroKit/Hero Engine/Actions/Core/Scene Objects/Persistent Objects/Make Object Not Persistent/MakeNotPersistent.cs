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
    /// Make a hero kit object not available in all scenes.
    /// </summary>
    public class MakeNotPersistent : IHeroKitAction
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
        public static MakeNotPersistent Create()
        {
            MakeNotPersistent action = new MakeNotPersistent();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i]);

            // note: debug info needs to go first for this action because
            // if you put it after state is changed, the wrong action is printed to log
            if (heroKitObject.debugHeroObject)
            {
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));
            } 

            // normal return
            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject)
        {
            if (targetObject.persist == true)
            {
                // alert hero kit that this object is no longer persistent
                targetObject.persist = false;

                GameObject newObject = UnityEngine.Object.Instantiate(targetObject.gameObject);
                newObject.name = targetObject.gameObject.name;
                targetObject.gameObject.SetActive(false);
                UnityEngine.Object.Destroy(targetObject.gameObject);
                targetObject = heroKitObject.GetGameObjectComponent<HeroKitObject>("HeroKitObject", false, newObject);
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