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
    /// Show the load game menu.
    /// </summary>
    public class ShowLoadGameMenu : IHeroKitAction
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
        public static ShowLoadGameMenu Create()
        {
            ShowLoadGameMenu action = new ShowLoadGameMenu();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            string title = StringFieldValue.GetValueA(heroKitObject, 0);
            ShowSaveGameMenu showMenu = new ShowSaveGameMenu();
            HeroKitObject targetObject = showMenu.SetupSaveMenu(title, 2);
            bool runThis = (targetObject != null);

            if (runThis)
            {
                // menu was called from start menu
                bool calledFromStartMenu = BoolValue.GetValue(heroKitObject, 1);
                targetObject.heroList.bools.items[1].value = calledFromStartMenu;

                // enable the canvas
                Canvas canvas = targetObject.GetHeroComponent<Canvas>("Canvas");
                canvas.enabled = true;
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