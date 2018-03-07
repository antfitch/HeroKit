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
    /// Save the game.
    /// </summary>
    public class SaveGame : IHeroKitAction
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
        public static SaveGame Create()
        {
            SaveGame action = new SaveGame();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // save the current scene
            SaveScene saveScene = new SaveScene();
            saveScene.SaveSceneData(heroKitObject, false); // save scene objects
            saveScene.SaveSceneData(heroKitObject, true);  // save persistent objects

            // get the path where you want to put the save game file on the player's device
            string saveGameName = StringFieldValue.GetValueA(heroKitObject, 0, true);
            string path = Application.persistentDataPath + "/HeroSaves/" + saveGameName + ".json";

            // if there is an existing save file, get the seconds played
            GameSaveData existingGame = HeroKitCommonRuntime.GetSaveGame(saveGameName);

            // convert game data to json data
            string jsonText = GetSaveGameData(existingGame);

            // save the json data to player's device
            File.WriteAllText(path, jsonText);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Save Game Name: " + saveGameName + "\n" +
                                      "Full Path: " + path;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        private string GetSaveGameData(GameSaveData existingGame)
        {
            // if save file existed, get date we need to transfer to new save file
            bool newGame = (existingGame == null);
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            TimeSpan gameplay = new TimeSpan();
            if (newGame)
            {              
                TimeSpan timeElapsed = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
                startDate = DateTime.Now.Subtract(timeElapsed);
                endDate = DateTime.Now;
                gameplay = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
            }
            else
            {
                startDate = new DateTime(existingGame.startYear, existingGame.startMonth, existingGame.startDay, existingGame.startHour, existingGame.startMinute, existingGame.startSecond);
                endDate = DateTime.Now;

                TimeSpan oldTime = new TimeSpan(existingGame.playtimeDays, existingGame.playtimeHours, existingGame.playtimeMinutes, existingGame.playtimeSeconds);
                TimeSpan newTime = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
                gameplay = oldTime + newTime;
            }

            // get the json files
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.temporaryCachePath + "/HeroScenes/");
            FileInfo[] fileInfo = directoryInfo.GetFiles("*.json");

            // set up the game save data class
            GameSaveData saveData = new GameSaveData();
            saveData.scenes = new SceneSaveData[fileInfo.Length];

            // build the game save data
            for (int i = 0; i < fileInfo.Length; i++)
            {
                //Debug.Log(fileInfo[i].Name);
                string jsonSceneText = GetCachedScene(fileInfo[i].Name);
                saveData.scenes[i] = JsonUtility.FromJson<SceneSaveData>(jsonSceneText);
            }

            // save the current scene name
            saveData.lastScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            saveData.lastSceneID = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

            // add start date
            saveData.startYear = startDate.Year;
            saveData.startMonth = startDate.Month;
            saveData.startDay = startDate.Day;
            saveData.startHour = startDate.Hour;
            saveData.startMinute = startDate.Minute;
            saveData.startSecond = startDate.Second;

            // add current date
            saveData.endYear = endDate.Year;
            saveData.endMonth = endDate.Month;
            saveData.endDay = endDate.Day;
            saveData.endHour = endDate.Hour;
            saveData.endMinute = endDate.Minute;
            saveData.endSecond = endDate.Second;

            // add gameplay
            saveData.playtimeDays = (int)gameplay.TotalDays;
            saveData.playtimeHours = (int)gameplay.TotalHours;
            saveData.playtimeMinutes = (int)gameplay.TotalMinutes;
            saveData.playtimeSeconds = gameplay.Seconds;

            // variables
            saveData.globalInts = HeroKitDatabase.GetGlobals().ints.Save();
            saveData.globalFloats = HeroKitDatabase.GetGlobals().floats.Save();
            saveData.globalBools = HeroKitDatabase.GetGlobals().bools.Save();
            saveData.globalStrings = HeroKitDatabase.GetGlobals().strings.Save();

            // convert the game save data class to json
            string jsonGameText = JsonUtility.ToJson(saveData);

            // return the json file
            return jsonGameText;
        }

        // load the cached data for a scene after the scene has loaded
        public string GetCachedScene(string sceneName)
        {
            string jsonText = "";

            // get the path where you want to load cached scene data
            string path = Application.temporaryCachePath + "/HeroScenes/" + sceneName;

            // get the json data in the file if it exists
            if (File.Exists(path))
            {
                jsonText = File.ReadAllText(path);
            }

            return jsonText;
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

    /// <summary>
    /// Contains data that can be saved in a file on the player's device
    /// </summary>
    [Serializable]
    public class GameSaveData
    {
        // scene where game was saved
        public string lastScene;
        public int lastSceneID;

        // monitor how long the game has been played
        public int startYear;
        public int startMonth;
        public int startDay;
        public int startHour;
        public int startMinute;
        public int startSecond;

        public int endYear;
        public int endMonth;
        public int endDay;
        public int endHour;
        public int endMinute;
        public int endSecond;

        public int playtimeDays;
        public int playtimeHours;
        public int playtimeMinutes;
        public int playtimeSeconds;

        // globals
        public int[] globalInts;
        public float[] globalFloats;
        public bool[] globalBools;
        public string[] globalStrings;

        // cached data for all scenes when game was saved
        public SceneSaveData[] scenes;
    }
}