// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using System.IO;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Load a save game file.
    /// </summary>
    public class LoadGame : IHeroKitAction
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
        public static LoadGame Create()
        {
            LoadGame action = new LoadGame();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get the path where you want to load the save game file
            string saveDataName = StringFieldValue.GetValueA(heroKitObject, 0, true);

            // get the saved game
            GameSaveData savedGame = HeroKitCommonRuntime.GetSaveGame(saveDataName);

            // load the game
            LoadSaveGameData(savedGame);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Game to Load: " + saveDataName;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        private bool LoadSaveGameData(GameSaveData saveData)
        {
            if (saveData == null) return false;

            // create scene json files and save them in the temp directory
            for (int i = 0; i < saveData.scenes.Length; i++)
            {
                string path = Application.temporaryCachePath + "/HeroScenes/" + saveData.scenes[i].sceneName + ".json";
                string jsonSceneText = JsonUtility.ToJson(saveData.scenes[i]);
                File.WriteAllText(path, jsonSceneText);
            }

            // load the global variables
            HeroList globals = HeroKitDatabase.globals;
            HeroKitCommonScene.AddVariables(globals.ints.items, saveData.globalInts);
            HeroKitCommonScene.AddVariables(globals.floats.items, saveData.globalFloats);
            HeroKitCommonScene.AddVariables(globals.bools.items, saveData.globalBools);
            HeroKitCommonScene.AddVariables(globals.strings.items, saveData.globalStrings);
            HeroKitDatabase.globals = globals;

            // load the scene that was last opened
            Vector3 defaultCoords = new Vector3(-999999, -999999, -999999);
            HeroKitCommonScene.LoadScene(saveData.lastScene, false, false, defaultCoords, defaultCoords);

            return true;
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