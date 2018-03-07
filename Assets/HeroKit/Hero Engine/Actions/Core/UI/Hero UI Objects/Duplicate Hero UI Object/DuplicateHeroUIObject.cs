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
    /// Clone a UI hero kit object.
    /// </summary>
    public class DuplicateHeroUIObject : IHeroKitAction
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
        public static DuplicateHeroUIObject Create()
        {
            DuplicateHeroUIObject action = new DuplicateHeroUIObject();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            GameObject prefab = PrefabValue.GetValue(heroKitObject, 0);
            GameObject parent = GameObjectFieldValue.GetValueA(heroKitObject, 1);
            int count = IntegerFieldValue.GetValueA(heroKitObject, 2);
            bool incrementItemID = BoolValue.GetValue(heroKitObject, 3);
            HeroKitObject notifications = HeroObjectFieldValue.GetValueA(heroKitObject, 4)[0];
            int stateID = EventValue.GetStateID(heroKitObject, 5);
            int eventID = EventValue.GetEventID(heroKitObject, 5);
            bool runThis = (prefab != null && parent != null);

            if (runThis)    
                CreateUIObjects(prefab, parent, count, incrementItemID, notifications, stateID, eventID, false);

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Parent Game Object: " + parent + "\n" +
                                      "Object to Duplicate: " + prefab + "\n" +
                                      "Number of Duplicates: " + count + "\n" +
                                      "Increment Item ID: " + incrementItemID + "\n" +
                                      "Send Notifications Here: " + notifications + "\n" +
                                      "Notification State ID: " + stateID + "\n" + 
                                      "Notification Event ID: " + eventID;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void CreateUIObjects(GameObject prefabObject, GameObject contentObject, int objectCount, bool incrementID,
                                    HeroKitObject notificationObject, int stateID, int eventID, bool selfNotify)
        {
            GameObject prefab = prefabObject;
            GameObject parent = contentObject;
            int count = objectCount;

            for (int i = 0; i < count; i++)
            {
                // create the game object
                GameObject gameObject = UnityEngine.Object.Instantiate(prefab, parent.transform);
                gameObject.name = prefab.name + " " + (i + 1);
                HeroKitObject hko = gameObject.GetComponent<HeroKitObject>();

                // get the hero kit listener component
                HeroKitListenerUI heroListener = hko.GetGameObjectComponent<HeroKitListenerUI>("HeroKitListenerUI", false, gameObject);
                if (heroListener != null)
                {
                    // increment item id?
                    if (incrementID)
                    {
                        heroListener.itemID = (i + 1);
                    }

                    // setup notifications
                    HeroKitObject notifications = (selfNotify) ? hko : notificationObject;
                    if (notifications != null)
                    {
                        heroListener.sendNotificationsHere = notifications;
                        heroListener.actionType = 1;
                        heroListener.stateID = stateID;
                        heroListener.eventID = eventID;
                    }
                }

                // get the hero object component
                HeroKitObject heroObject = hko.GetGameObjectComponent<HeroKitObject>("HeroKitObject", false, gameObject);
                if (heroObject != null)
                {
                    heroObject.doNotSave = true;
                    heroObject.heroGUID = HeroKitCommonRuntime.GetHeroGUID();
                }

                gameObject.SetActive(true);
            }
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