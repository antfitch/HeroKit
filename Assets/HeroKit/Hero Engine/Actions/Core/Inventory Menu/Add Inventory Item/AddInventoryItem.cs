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
    /// Add an item to the inventory menu.
    /// </summary>
    public class AddInventoryItem : IHeroKitAction
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
        public static AddInventoryItem Create()
        {
            AddInventoryItem action = new AddInventoryItem();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // add menu to scene if it doesn't exist
            HeroKitObject targetObject = HeroKitCommonRuntime.GetPrefabFromAssets(HeroKitCommonRuntime.settingsInfo.inventoryMenu, true);
            HeroObject item = null;
            int count = 0;
            bool runThis = (targetObject != null);

            if (runThis)
            {
                targetObject.gameObject.SetActive(true);

                // get the container for the inventory slots
                GameObject parent = HeroKitCommonRuntime.GetChildGameObject(targetObject.gameObject, "Inventory Menu Content");
                if (parent != null)
                {
                    // get the item we want to add
                    item = HeroObjectFieldValue.GetValueC(heroKitObject, 0);
                    if (item != null)
                    {
                        // get the number of items to add
                        bool addMultiple = BoolValue.GetValue(heroKitObject, 1);
                        count = (addMultiple) ? IntegerFieldValue.GetValueA(heroKitObject, 2) : 1;

                        // check to see if the inventory slot already exists in the menu
                        GameObject gameObject = HeroKitCommonRuntime.GetChildGameObject(parent, item.name, true);
                        HeroKitObject heroObject = null;

                        // add item if it doesn't exist
                        if (gameObject == null) 
                        {
                            // get the inventory slot 
                            GameObject prefab = HeroKitCommonRuntime.settingsInfo.inventorySlot;
                            if (prefab == null)
                                Debug.LogError("Can't add prefab because it can't be found. (Inventory Slot)");
                            
                            // add prefab to parent
                            if (parent != null && prefab != null)
                            {
                                // create the game object
                                gameObject = UnityEngine.Object.Instantiate(prefab, parent.transform);
                                
                                // get the hero kit listener component
                                HeroKitListenerUI heroListener = heroKitObject.GetGameObjectComponent<HeroKitListenerUI>("HeroKitListenerUI", false, gameObject);
                                if (heroListener != null)
                                {
                                    // add item                
                                    heroListener.item = item;

                                    // setup notifications
                                    HeroKitObject notifications = HeroObjectFieldValue.GetValueA(heroKitObject, 3)[0];
                                    if (notifications != null)
                                    {
                                        heroListener.sendNotificationsHere = notifications;
                                        heroListener.actionType = 1;
                                        heroListener.stateID = EventValue.GetStateID(heroKitObject, 4);
                                        heroListener.eventID = EventValue.GetEventID(heroKitObject, 4);
                                    }

                                    // rename the object
                                    gameObject.name = heroListener.item.name;
                                }

                                // get the hero object component
                                heroObject = heroKitObject.GetGameObjectComponent<HeroKitObject>("HeroKitObject", false, gameObject);
                                if (heroObject != null)
                                {
                                    heroObject.doNotSave = true; 
                                    heroObject.heroGUID = HeroKitCommonRuntime.GetHeroGUID();
                                }

                                // enable the game object
                                gameObject.SetActive(true);
                            }
                        }

                        // if prefab is not active, make it active
                        if (!gameObject.activeSelf)
                            gameObject.SetActive(true);

                        // get hero kit object
                        if (heroObject == null)
                            heroObject = heroKitObject.GetGameObjectComponent<HeroKitObject>("HeroKitObject", false, gameObject);

                        // add the # of items to add to integer variable list, slot 1
                        heroObject.heroList.ints.items[1].value = count;

                        // play event 1 in the hero kit object attached to this prefab
                        heroObject.PlayEvent(1);
                    }
                }
            }

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Item: " + item + "\n" +
                                      "Count: " + count;
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