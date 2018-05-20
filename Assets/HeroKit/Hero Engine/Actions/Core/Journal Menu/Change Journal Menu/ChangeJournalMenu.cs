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
    /// Change the prefab for the journal menu.
    /// </summary>
    public class ChangeJournalMenu : IHeroKitAction
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
        public static ChangeJournalMenu Create()
        {
            ChangeJournalMenu action = new ChangeJournalMenu();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            GameObject menuTemplate = PrefabValue.GetValue(heroKitObject, 0);
            GameObject slotTemplate = PrefabValue.GetValue(heroKitObject, 1);
            bool runThis = (menuTemplate != null);

            if (runThis)
            {
                // if new prefab is not in scene, delete the old one from scene and attach new prefab to settings.
                GameObject prefabMenu = HeroKitCommonRuntime.settingsInfo.journalMenu;
                if (prefabMenu != null && prefabMenu != menuTemplate)
                {
                    HeroKitCommonRuntime.DeletePrefabFromScene(prefabMenu, true);
                    HeroKitCommonRuntime.settingsInfo.journalMenu = menuTemplate;
                }

                // if new prefab is not in scene, delete the old one from scene and attach new prefab to settings.
                GameObject prefabSlot = HeroKitCommonRuntime.settingsInfo.journalSlot;
                if (prefabSlot != null && prefabSlot != slotTemplate)
                {
                    HeroKitCommonRuntime.DeletePrefabFromScene(prefabSlot, true);
                    HeroKitCommonRuntime.settingsInfo.journalSlot = slotTemplate;
                }
            }

            if (heroKitObject.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));

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