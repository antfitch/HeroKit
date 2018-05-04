// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using UnityEngine.UI;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Show the save game menu.
    /// </summary>
    public class ShowSaveGameMenu : IHeroKitAction
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
        public static ShowSaveGameMenu Create()
        {
            ShowSaveGameMenu action = new ShowSaveGameMenu();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            string title = StringFieldValue.GetValueA(heroKitObject, 0);
            HeroKitObject targetObject = SetupSaveMenu(title, 1);
            bool runThis = (targetObject != null);

            if (runThis)
            {
                // enable the canvas
                Canvas canvas = targetObject.GetHeroComponent<Canvas>("Canvas");
                canvas.enabled = true;
            }

            if (heroKitObject.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));

            return -99;
        }

        public HeroKitObject SetupSaveMenu(string menuTitle, int menuID)
        {
            // add menu to scene if it doesn't exist
            HeroKitObject targetObject = HeroKitCommonRuntime.GetPrefabFromAssets(HeroKitCommonRuntime.settingsInfo.saveMenu, true);
            targetObject.gameObject.SetActive(true);

            // change title
            string title = menuTitle;
            GameObject titleObject = HeroKitCommonRuntime.GetChildGameObject(targetObject.gameObject, "SaveMenuTitle", true);
            Text titleText = targetObject.GetGameObjectComponent<Text>("Text", false, titleObject);
            titleText.text = title;

            // add save slots
            GameObject parentObject = HeroKitCommonRuntime.GetChildGameObject(targetObject.gameObject, "Save Menu Content", true);
            if (parentObject.transform.childCount == 0)
            {
                HeroKitObject saveObject = HeroKitCommonRuntime.GetPrefabFromAssets(HeroKitCommonRuntime.settingsInfo.saveSlot, false);
                DuplicateHeroUIObject dup = new DuplicateHeroUIObject();
                dup.CreateUIObjects(saveObject.gameObject, parentObject, 20, true, saveObject, 1, 4, true);
            }

            // menu type = save
            HeroKitObject[] children = parentObject.transform.GetComponentsInChildren<HeroKitObject>();
            for (int i = 0; i < children.Length; i++)
            {
                children[i].heroList.ints.items[0].value = menuID;
            }

            return targetObject;
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