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
    /// Remove a journal entry from the journal menu.
    /// </summary>
    public class RemoveJournalEntry : IHeroKitAction
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
        public static RemoveJournalEntry Create()
        {
            RemoveJournalEntry action = new RemoveJournalEntry();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // add menu to scene if it doesn't exist
            HeroKitObject targetObject = HeroKitCommonRuntime.GetPrefabFromAssets(HeroKitCommonRuntime.settingsInfo.journalMenu, true);
            HeroObject item = null;
            bool runThis = (targetObject != null);

            if (runThis)
            {
                targetObject.gameObject.name = HeroKitCommonRuntime.settingsInfo.journalMenu.name;
                targetObject.gameObject.SetActive(true);

                // get the container for the inventory slots
                GameObject parent = HeroKitCommonRuntime.GetChildGameObject(targetObject.gameObject, "Journal Menu Content");
                if (parent != null)
                {
                    // get the item we want to remove
                    item = HeroObjectFieldValue.GetValueC(heroKitObject, 0);
                    if (item != null)
                    {
                        // check to see if the inventory slot already exists in the menu
                        GameObject gameObject = HeroKitCommonRuntime.GetChildGameObject(parent, item.name, false);

                        // if the item exists, remove it
                        if (gameObject != null)
                        {
                            HeroKitObject heroObject = heroKitObject.GetGameObjectComponent<HeroKitObject>("HeroKitObject", false, gameObject);

                            // play event 2 in the hero kit object attached to this prefab
                            if (heroObject != null)
                                heroObject.PlayEvent(2);
                        }
                    }
                }
            }

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Journal Entry: " + item;
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