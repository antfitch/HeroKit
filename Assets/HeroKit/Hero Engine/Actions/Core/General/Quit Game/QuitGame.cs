// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
//using UnityEditor;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Exit the game.
    /// </summary>
    public class QuitGame : IHeroKitAction
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
        public static QuitGame Create()
        {
            QuitGame action = new QuitGame();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // assign variables
            heroKitObject = hko;
            
            // display debug message
            if (hko.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));

            // quit the game
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
		        Application.Quit();
            #endif

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