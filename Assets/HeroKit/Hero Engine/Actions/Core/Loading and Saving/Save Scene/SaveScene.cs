// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Save the scene.
    /// </summary>
    public class SaveScene : IHeroKitAction
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
        public static SaveScene Create()
        {
            SaveScene action = new SaveScene();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // save the data in the current scene in a temporary directory
            SaveSceneData(heroKitObject, false);

            // save persistent data in a temporary directory
            SaveSceneData(heroKitObject, true);

            if (heroKitObject.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));

            return -99;
        }

        // Save the scene data in a temporary directory.
        public void SaveSceneData(HeroKitObject hko, bool savePersistentObjects)
        {
            // Save all herokit objects in the current scene in the database
            HeroKitDatabase.AddAllHeroKitObjects();

            // get the path where you want to put the save game file on the player's device
            string sceneName = (savePersistentObjects) ? "PersistentObjects" : UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            string path = Application.temporaryCachePath + "/HeroScenes/" + sceneName + ".json";

            // convert game data to json data
            string jsonText = GetSceneData(hko, savePersistentObjects);

            // save the json data to player's device
            File.WriteAllText(path, jsonText);
        }

        // Get the scene data that you want to save
        private string GetSceneData(HeroKitObject hko, bool savePersistentObjects)
        {
            // filter the hero kit objects in the scene
            int objectCount = 9999999;
            List<HeroKitObject> heroKitObjects = HeroActionCommonRuntime.GetHeroObjects(HeroActionCommonRuntime.GetHeroObjectsInScene(), objectCount);

            // get info to save
            string jsonText = "";

            // save general game info
            SceneSaveData saveData = SaveGeneralInfo(heroKitObjects, savePersistentObjects);
            jsonText += JsonUtility.ToJson(saveData);

            //Debug.Log(jsonText);

            return jsonText;
        }

        // save the fields
        public SceneSaveData SaveGeneralInfo(List<HeroKitObject> heroKitObjects, bool savePersistentObjects)
        {
            SceneSaveData saveData = new SceneSaveData();

            // Scene
            saveData.sceneName = (savePersistentObjects) ? "PersistentObjects" : UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            // Here objects in scene
            List<HeroSaveData> heroList = new List<HeroSaveData>();

            // save hero data
            for (int i = 0; i < heroKitObjects.Count; i++)
            {
                if (heroKitObjects[i] != null && heroKitObjects[i].heroObject != null && heroKitObjects[i].persist == savePersistentObjects)
                {
                    // only save objects that are flagged to be saved
                    if (!heroKitObjects[i].doNotSave)
                    {
                        HeroSaveData saveHeroData = new HeroSaveData();
                        saveHeroData = SaveHeroObject(saveHeroData, heroKitObjects[i]);
                        heroList.Add(saveHeroData);
                    }
                }
            }

            saveData.heroObjects = heroList.ToArray();

            return saveData;
        }
 
        private HeroSaveData SaveHeroObject(HeroSaveData saveData, HeroKitObject targetObject)
        {
            // transform info
            saveData.heroPosition = new float[3];
            saveData.heroPosition[0] = targetObject.transform.localPosition.x;
            saveData.heroPosition[1] = targetObject.transform.localPosition.y;
            saveData.heroPosition[2] = targetObject.transform.localPosition.z;

            saveData.heroRotation = new float[3];
            saveData.heroRotation[0] = targetObject.transform.localEulerAngles.x;
            saveData.heroRotation[1] = targetObject.transform.localEulerAngles.y;
            saveData.heroRotation[2] = targetObject.transform.localEulerAngles.z;

            saveData.heroScale = new float[3];
            saveData.heroScale[0] = targetObject.transform.localScale.x;
            saveData.heroScale[1] = targetObject.transform.localScale.y;
            saveData.heroScale[2] = targetObject.transform.localScale.z;

            // general info
            saveData.gameObjectName = targetObject.gameObject.name;
            saveData.gameObjectEnabled = targetObject.gameObject.activeSelf;
            saveData.heroName = targetObject.heroObject.name;
            saveData.heroGUID = targetObject.heroGUID;
            saveData.stateID = targetObject.heroStateData.state;

            saveData.gameObjectParentName = (targetObject.transform.parent != null) ? targetObject.transform.parent.name : "";
            saveData.gameObjectRootName = (targetObject.transform.root != targetObject.transform && targetObject.transform.root != targetObject.transform.parent) ? targetObject.transform.root.name : "";

            // variables
            saveData.variableInts = targetObject.heroList.ints.Save();
            saveData.variableFloats = targetObject.heroList.floats.Save();
            saveData.variableBools = targetObject.heroList.bools.Save();
            saveData.variableStrings = targetObject.heroList.strings.Save();

            // properties
            if (targetObject.heroProperties.Length > 0)
            {
                // get property count
                saveData.propertyCount = targetObject.heroProperties.Length;

                // initialize save data
                saveData.pIntCount = new int[saveData.propertyCount];
                saveData.pFloatCount = new int[saveData.propertyCount];
                saveData.pBoolCount = new int[saveData.propertyCount];
                saveData.pStringCount = new int[saveData.propertyCount];

                // get variable list sizes
                for (int i = 0; i < saveData.propertyCount; i++)
                {
                    saveData.pIntCount[i] = targetObject.heroProperties[i].itemProperties.ints.items.Count;
                    saveData.pFloatCount[i] = targetObject.heroProperties[i].itemProperties.floats.items.Count;
                    saveData.pBoolCount[i] = targetObject.heroProperties[i].itemProperties.bools.items.Count;
                    saveData.pStringCount[i] = targetObject.heroProperties[i].itemProperties.strings.items.Count;
                }

                // initialize variable list
                saveData.propertyInts = new int[0]; //saveData.pIntCount.Sum()
                saveData.propertyFloats = new float[0];
                saveData.propertyBools = new bool[0];
                saveData.propertyStrings = new string[0];

                // add values to the lists
                for (int i = 0; i < saveData.propertyCount; i++)
                {
                    saveData.propertyInts = saveData.propertyInts.Concat(targetObject.heroProperties[i].itemProperties.ints.Save()).ToArray();
                    saveData.propertyFloats = saveData.propertyFloats.Concat(targetObject.heroProperties[i].itemProperties.floats.Save()).ToArray();
                    saveData.propertyBools = saveData.propertyBools.Concat(targetObject.heroProperties[i].itemProperties.bools.Save()).ToArray();
                    saveData.propertyStrings = saveData.propertyStrings.Concat(targetObject.heroProperties[i].itemProperties.strings.Save()).ToArray();
                }
            }

            // return save data
            return saveData;
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
    public class SceneSaveData
    {
        public string sceneName;
        public HeroSaveData[] heroObjects;
    }

    /// <summary>
    /// Contains data that can be saved in a file on the player's device
    /// </summary>
    [Serializable]
    public class HeroSaveData
    {
        // transform
        public float[] heroPosition;
        public float[] heroRotation;
        public float[] heroScale;

        public string gameObjectName;
        public bool gameObjectEnabled;
        public string heroName;
        public int heroGUID;
        public int stateID;

        public string gameObjectParentName;
        public string gameObjectRootName;

        // variable list
        public int[] variableInts;
        public float[] variableFloats;
        public bool[] variableBools;
        public string[] variableStrings;

        // property list
        public int propertyCount;        // 4
        public int[] pIntCount;   // 0, 2, 1, 5
        public int[] pFloatCount;
        public int[] pBoolCount;
        public int[] pStringCount;

        public int[] propertyInts;
        public float[] propertyFloats;
        public bool[] propertyBools;
        public string[] propertyStrings;
    }
}