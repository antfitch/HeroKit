// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using System.Collections.Generic;

namespace HeroKit.Scene.Actions
{
    /// <summary>
    /// Change a hero object on a hero kit object.
    /// </summary>
    public class ChangeHeroObject : IHeroKitAction
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
        public static ChangeHeroObject Create()
        {
            ChangeHeroObject action = new ChangeHeroObject();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            HeroKitObject[] getThisHeroObject = HeroObjectFieldValue.GetValueA(heroKitObject, 1);
            List<HeroKitObject> lst = new List<HeroKitObject>();

            for (int i = 0; i < getThisHeroObject.Length; i++)
                lst.Add(getThisHeroObject[i]);

            HeroObjectFieldValue.SetValueB(heroKitObject, 0, lst);

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "HeroObject: " + getThisHeroObject;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
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