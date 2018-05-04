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
    /// Remove an item from the inventory menu.
    /// </summary>
    public class RemoveInventoryItem : IHeroKitAction
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
        public static RemoveInventoryItem Create()
        {
            RemoveInventoryItem action = new RemoveInventoryItem();
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
                targetObject.gameObject.name = HeroKitCommonRuntime.settingsInfo.inventoryMenu.name;
                targetObject.gameObject.SetActive(true);

                // get the container for the inventory slots
                GameObject parent = HeroKitCommonRuntime.GetChildGameObject(targetObject.gameObject, "Inventory Menu Content");
                if (parent != null)
                {
                    // get the item we want to remove
                    item = HeroObjectFieldValue.GetValueC(heroKitObject, 0);
                    if (item != null)
                    {
                        // get the number of items to remove
                        bool addMultiple = BoolValue.GetValue(heroKitObject, 1);
                        count = (addMultiple) ? IntegerFieldValue.GetValueA(heroKitObject, 2) : 1;

                        // check to see if the inventory slot already exists in the menu
                        GameObject gameObject = HeroKitCommonRuntime.GetChildGameObject(parent, item.name, false);
                        HeroKitObject heroObject = null;

                        // if the item exists, remove it
                        if (gameObject != null)
                        {
                            // get hero kit object
                            if (heroObject == null)
                                heroObject = heroKitObject.GetGameObjectComponent<HeroKitObject>("HeroKitObject", false, gameObject);

                            // add the # of items to add to integer variable list, slot 1
                            heroObject.heroList.ints.items[1].value = count;

                            // play event 2 in the hero kit object attached to this prefab
                            heroObject.PlayEvent(2);
                        }
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