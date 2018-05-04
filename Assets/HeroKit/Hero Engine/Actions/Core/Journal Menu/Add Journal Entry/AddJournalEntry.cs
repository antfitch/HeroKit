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
    /// Add an entry to the journal menu.
    /// </summary>
    public class AddJournalEntry : IHeroKitAction
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
        public static AddJournalEntry Create()
        {
            AddJournalEntry action = new AddJournalEntry();
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
                targetObject.gameObject.SetActive(true);

                // get the container for the inventory slots
                GameObject parent = HeroKitCommonRuntime.GetChildGameObject(targetObject.gameObject, "Journal Menu Content");
                if (parent != null)
                {
                    // get the item we want to add
                    item = HeroObjectFieldValue.GetValueC(heroKitObject, 0);
                    if (item != null)
                    {
                        // check to see if the inventory slot already exists in the menu
                        GameObject gameObject = HeroKitCommonRuntime.GetChildGameObject(parent, item.name, true);
                        HeroKitObject heroObject = null;

                        // add item if it doesn't exist
                        if (gameObject == null)
                        {
                            // get the inventory slot 
                            GameObject prefab = HeroKitCommonRuntime.settingsInfo.journalSlot;
                            if (prefab == null)
                                Debug.LogError("Prefab for journal slot is missing. (Journal Slot)");

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

                            // if prefab is not active, make it active
                            if (!gameObject.activeSelf)
                                gameObject.SetActive(true);

                            // get hero kit object
                            if (heroObject == null)
                                heroObject = heroKitObject.GetGameObjectComponent<HeroKitObject>("HeroKitObject", false, gameObject);

                            // play event 1 in the hero kit object attached to this prefab
                            heroObject.PlayEvent(1);
                        }
                        else
                        {
                            gameObject.SetActive(true);
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