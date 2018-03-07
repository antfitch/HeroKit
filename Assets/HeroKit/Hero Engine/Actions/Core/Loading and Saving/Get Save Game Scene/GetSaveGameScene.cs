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
    /// Get the last scene loaded in a save game file.
    /// </summary>
    public class GetSaveGameScene : IHeroKitAction
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
        public static GetSaveGameScene Create()
        {
            GetSaveGameScene action = new GetSaveGameScene();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            string saveGameName = StringFieldValue.GetValueA(heroKitObject, 0, true);
            GameSaveData savedGame = HeroKitCommonRuntime.GetSaveGame(saveGameName);
            bool runThis = (savedGame != null);

            // save the name of the scene
            if (runThis)
            {
                StringFieldValue.SetValueB(heroKitObject, 1, savedGame.lastScene);
            }

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string lastScene = (savedGame != null) ? savedGame.lastScene : "";
                string debugMessage = "Scene: " + lastScene;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }


        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------

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