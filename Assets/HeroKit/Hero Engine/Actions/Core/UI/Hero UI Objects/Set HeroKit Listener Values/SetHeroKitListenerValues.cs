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
    /// Set values in a hero kit listener component on a UI object.
    /// </summary>
    public class SetHeroKitListenerValues : IHeroKitAction
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
        public static SetHeroKitListenerValues Create()
        {
            SetHeroKitListenerValues action = new SetHeroKitListenerValues();
            return action;
        }

        // Gets objects in a scene that match a certerin criteria
        public int Execute(HeroKitObject hko)
        {
            // Get variables
            heroKitObject = hko;
            SceneObjectValueData objectData = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            HeroKitListenerUI listener = null;
            if (objectData.heroKitObject != null)
            {
                listener = objectData.heroKitObject[0].GetHeroComponent<HeroKitListenerUI>("HeroKitListenerUI");
            }
            else if (objectData.gameObject != null)
            {
                listener = heroKitObject.GetGameObjectComponent<HeroKitListenerUI>("HeroKitListenerUI", false, objectData.gameObject[0]);
            }

            if (listener != null)
            {
                // get item id
                bool getItemID = BoolValue.GetValue(heroKitObject, 2);
                if (getItemID)
                {
                    listener.itemID = IntegerFieldValue.GetValueA(heroKitObject, 3);
                    
                }

                // get item
                bool getItem = BoolValue.GetValue(heroKitObject, 9);
                if (getItem)
                {
                    listener.item = HeroObjectFieldValue.GetValueC(heroKitObject, 10);

                }

                // get hero kit object, state, and event to play
                bool getEvent = BoolValue.GetValue(heroKitObject, 6);
                if (getEvent)
                {
                    listener.sendNotificationsHere = HeroObjectFieldValue.GetValueA(heroKitObject, 7)[0];
                    listener.stateID = EventValue.GetStateID(heroKitObject, 8);
                    listener.eventID = EventValue.GetEventID(heroKitObject, 8);
                }
            }

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Listener: " + listener;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
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