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
    /// Change the layer of one or more objects and thier children.
    /// </summary>
    public class ChangeLayer : IHeroKitAction
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
        public static ChangeLayer Create()
        {
            ChangeLayer action = new ChangeLayer();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            bool includeChildren = BoolValue.GetValue(heroKitObject, 2);
            int newLayer = DropDownListValue.GetValue(heroKitObject, 3) - 1;
            bool runThis = (targetObject != null && newLayer != -1);

            // note: debug info needs to go first for this action because
            // if you put it after state is changed, the wrong action is printed to log
            // debug info
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Layer: " + newLayer + "\n" +
                                      "Include Children: " + includeChildren;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], includeChildren, newLayer);

            // normal return
            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, bool includeChildren, int newLayer)
        {
            // set layer
            targetObject.gameObject.layer = newLayer;
            if (includeChildren)
            {
                Transform[] children = targetObject.gameObject.GetComponentsInChildren<Transform>(true);
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].gameObject.layer = newLayer;
                }
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