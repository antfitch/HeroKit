// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.Collections.Generic;
using System;
using System.IO;
using HeroKit.Scene.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HeroKit.Scene
{
    /// <summary>
    /// Common fields, methods, and other class members used by HeroKit scenes.
    /// </summary>
    public static class HeroKitCommonScene
    {
        //-------------------------------------------
        // Variables
        //-------------------------------------------

        /// <summary>
        /// Load the default scene that opens when you start a new game.
        /// </summary>
        private static bool useDefaultScene;
        /// <summary>
        /// If a persistent object has same ID and name of non-persistent object, delete non-persistent object.
        /// </summary>
        private static bool removeNonPersistent;

        /// <summary>
        /// The new position of the main camera.
        /// </summary>
        private static Vector3 cameraPosition;
        /// <summary>
        /// The new rotation of the main camera.
        /// </summary>
        private static Vector3 cameraEuler;
        /// <summary>
        /// Should we change the position of the main camera when we load the new scene?
        /// </summary>
        private static bool changeCameraPosition;
        /// <summary>
        /// Should we change the rotation of the main camera when we load the new scene?
        /// </summary>
        private static bool changeCameraRotation;
        /// <summary>
        /// The modified position of the camra based on which coordinates to ignore.
        /// </summary>
        private static Vector3 cameraNewPos;
        /// <summary>
        /// The new rotation of the camera based on which coordinates to ignore.
        /// </summary>
        private static Vector3 cameraNewEuler;

        /// <summary>
        /// An object that should move from one scene to another.
        /// </summary>
        private static HeroKitObject targetObject;
        /// <summary>
        /// The position for the object in the new scene.
        /// </summary>
        private static Vector3 targetPosition;
        /// <summary>
        /// The rotation of the object in the new scene.
        /// </summary>
        private static Vector3 targetEuler;
        /// <summary>
        /// Modified target position based on which coordinates to ignore.
        /// </summary>
        private static Vector3 targetObjectNewPos;
        /// <summary>
        /// Modified target rotation based on which coordinates to ignore.
        /// </summary>
        private static Vector3 targetObjectNewEuler;

        /// <summary>
        /// The name of the scene to load.
        /// </summary>
        private static string sceneName;
        /// <summary>
        /// The currently active scene.
        /// </summary>
        private static UnityEngine.SceneManagement.Scene activeScene;
        /// <summary>
        /// The scene to load.
        /// </summary>
        private static UnityEngine.SceneManagement.Scene nextScene;
        /// <summary>
        /// The new scene has been loaded.
        /// </summary>
        public static bool sceneIsLoaded;
        /// <summary>
        /// All objects have been loaded in the new scene.
        /// </summary>
        public static bool sceneObjectsLoaded;

        // ---------------------------------------
        // Load a scene: Begin
        // ---------------------------------------

        /// <summary>
        /// Load a scene.
        /// </summary>
        /// <param name="_sceneName">Name of the new scene.</param>
        /// <param name="_useDefaultScene">Use the default state of this scene (don't load cached data).</param>
        /// <param name="_removeNonPersistent">Remove non-persistent clones in the new scene.</param>
        /// <param name="_cameraPosition">New camera position.</param>
        /// <param name="_cameraEuler">New camera rotation.</param>
        public static void LoadScene(string _sceneName, bool _useDefaultScene, bool _removeNonPersistent, Vector3 _cameraPosition, Vector3 _cameraEuler)
        {
            sceneIsLoaded = false;
            sceneObjectsLoaded = false;
            useDefaultScene = _useDefaultScene;
            sceneName = _sceneName;
            removeNonPersistent = _removeNonPersistent;
            cameraPosition = _cameraPosition;
            cameraEuler = _cameraEuler;

            // remove pools from the old scene
            HeroKitDatabase.PoolDictionary = new Dictionary<string, List<GameObject>>();

            // get the current scene and next scene
            activeScene = SceneManager.GetActiveScene();
            nextScene = SceneManager.GetSceneByName(sceneName);

            // Load the new scene
            if (activeScene != nextScene)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }

            // Call these methods when scene is loaded
            SceneManager.sceneLoaded += SceneLoaded;
        }
        /// <summary>
        /// Load a scene and take an object with you from the old scene. 
        /// </summary>
        /// <param name="_sceneName">Name of the new scene.</param>
        /// <param name="_useDefaultScene">Use the default state of this scene (don't load cached data).</param>
        /// <param name="_removeNonPersistent">Remove non-persistent clones in the new scene.</param>
        /// <param name="targetPos">Target position for the object we are moving to the new scene.</param>
        /// <param name="targetRot">Target rotation for the object we are moving to the new scene.</param>
        /// <param name="targetObj">Object we are moving to the new scene.</param>
        /// <param name="changeCameraPos">Should the position of the camera change in the new scene?</param>
        /// <param name="changeCameraRot">Should the rotation of the camera change in the new scene?</param>
        public static void LoadSceneWithObject(string _sceneName, bool _useDefaultScene, bool _removeNonPersistent,  
                                               Vector3 targetPos, Vector3 targetRot, HeroKitObject targetObj,
                                               bool changeCameraPos, bool changeCameraRot)
        {
            // set up variables
            sceneIsLoaded = false;
            sceneObjectsLoaded = false;
            useDefaultScene = _useDefaultScene;
            removeNonPersistent = _removeNonPersistent;
            changeCameraPosition = changeCameraPos;
            changeCameraRotation = changeCameraRot;

            targetPosition = targetPos;
            targetEuler = targetRot;
            targetObject = targetObj;
            sceneName = _sceneName;

            // position of things in the current scene
            Vector3 currentTargetPosition = targetObject.transform.localPosition;
            Vector3 currentTargetEuler = targetObject.transform.localEulerAngles;
            Vector3 currentCameraPosition = Camera.main.transform.localPosition;

            // position of things in the scene we are loading
            targetObjectNewPos = GetObjectPosition(currentTargetPosition);
            targetObjectNewEuler = GetObjectRotation(currentTargetEuler);
            cameraNewPos = GetCameraObjectPosition(currentTargetPosition, currentCameraPosition);
            cameraNewEuler = GetCameraObjectRotation(currentTargetEuler);

            // remove pools from the old scene
            HeroKitDatabase.PoolDictionary = new Dictionary<string, List<GameObject>>();

            // get the current active scene
            activeScene = SceneManager.GetActiveScene();
            nextScene = SceneManager.GetSceneByName(sceneName);

            // Load the new scene
            if (activeScene != nextScene)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }

            // Call these methods when scene is loaded
            SceneManager.sceneLoaded += SceneLoadedObject;
        }

        // ---------------------------------------
        // Load a scene: End
        // ---------------------------------------

        /// <summary>
        /// Perform these actions once a scene has loaded. (for scenes that didn't have an object moved to them)
        /// </summary>
        /// <param name="newScene">The scene that was loaded.</param>
        /// <param name="loadMode">The scene load type.</param>
        public static void SceneLoaded(UnityEngine.SceneManagement.Scene newScene, LoadSceneMode loadMode)
        {
            SceneManager.sceneLoaded -= SceneLoaded;

            // unload the old scene if it's not the current scene
            if (activeScene != nextScene)
            {
                SceneManager.SetActiveScene(newScene);
                SceneManager.UnloadSceneAsync(activeScene);
            }        

            // set position & rotation of the main camera
            SetCameraPosition();
            SetCameraRotation();

            // Load cached data for the scene
            if (!useDefaultScene)
                LoadCachedSceneData(newScene);

            // if persistent object has same id and name of non-persistent object, delete non-persistent object
            if (removeNonPersistent)
                DeleteNonPersistentClones();

            sceneIsLoaded = true;
            sceneObjectsLoaded = true;
        }
        /// <summary>
        /// Perform these actions once a scene has loaded. (for scenes that had an object moved to them)
        /// </summary>
        /// <param name="newScene">The scene that was loaded.</param>
        /// <param name="loadMode">The scene load type.</param>
        public static void SceneLoadedObject(UnityEngine.SceneManagement.Scene newScene, LoadSceneMode loadMode)
        {
            SceneManager.sceneLoaded -= SceneLoadedObject;

            // get the scene we just loaded into the background and make it active
            if (activeScene != nextScene)
                SceneManager.SetActiveScene(newScene);

            // if we are working with a non-persistent object, move the object now
            if (!targetObject.persist)
            {
                // move the gameobject from scene A to scene B
                SceneManager.MoveGameObjectToScene(targetObject.gameObject, newScene);
            }

            // unload scene A
            if (activeScene != nextScene)
            {
                SceneManager.UnloadSceneAsync(activeScene);
                SceneManager.sceneUnloaded += SceneUnloadedObject;
            }

            // if our object already exists in this scene, delete original (you started here, left with object, and returned)
            DeleteDuplicate();

            // Load cached data for the scene
            if (!useDefaultScene)
                LoadCachedSceneData(newScene);

            // if persistent object has same id and name of non-persistent object, delete non-persistent object
            if (removeNonPersistent)
                DeleteNonPersistentClones();

            // SceneUnloadedObject
            if (activeScene == nextScene)
            {
                SetObjectPosition();
                SetObjectRotation();

                SetCameraObjectPosition();
                SetCameraObjectRotation();

                sceneObjectsLoaded = true;
            }

            sceneIsLoaded = true;
        }
        /// <summary>
        /// Perform these actions when the previous scene has been unloaded.
        /// </summary>
        /// <param name="newScene">The scene that was loaded.</param>
        public static void SceneUnloadedObject(UnityEngine.SceneManagement.Scene newScene)
        {
            SceneManager.sceneUnloaded -= SceneUnloadedObject;

            SetObjectPosition();
            SetObjectRotation();

            SetCameraObjectPosition();
            SetCameraObjectRotation();

            sceneObjectsLoaded = true;
        }

        // ---------------------------------------
        // Remove clones from scene
        // ---------------------------------------

        /// <summary>
        /// If an item is already in the scene, and you are re-adding it, delete the original.
        /// </summary>
        public static void DeleteDuplicate()
        {
            HeroKitObject[] heroObjects = HeroActionCommonRuntime.GetHeroObjectsInScene();
            for (int i = 0; i < heroObjects.Length; i++)
            {
                // make sure we're looking at unique objects
                if (heroObjects[i].gameObject != targetObject.gameObject)
                {
                    // delete the clone
                    if (heroObjects[i].heroGUID == targetObject.heroGUID)
                        UnityEngine.Object.Destroy(heroObjects[i].gameObject);
                }
            }
        }
        /// <summary>
        /// Delete non-persistent clones in a scene.
        /// </summary>
        public static void DeleteNonPersistentClones()
        {
            // get all objects in the scene
            HeroKitObject[] sortedObjects = HeroActionCommonRuntime.GetHeroObjectsInScene();

            // sort objects by hero GUID
            QuicksortHeroObjects(sortedObjects, 0, sortedObjects.Length - 1);

            // if a non-persistent object has same ID and name as persistent object, delete it
            for (int i = 0; i < sortedObjects.Length - 1; i++)
            {
                if (sortedObjects[i] != null)
                {
                    // two items have same GUID
                    if (sortedObjects[i].heroGUID == sortedObjects[i + 1].heroGUID)
                    {
                        // if one is persisten and one is not, delete the non-persistent object
                        if (!sortedObjects[i].persist && sortedObjects[i + 1].persist)
                        {
                            UnityEngine.Object.Destroy(sortedObjects[i].gameObject);
                        }
                        else if (sortedObjects[i].persist && !sortedObjects[i + 1].persist)
                        {
                            UnityEngine.Object.Destroy(sortedObjects[i + 1].gameObject);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Quicksort with 3 way partitioning. Used to delete persistent clones.
        /// </summary>
        /// <param name="heroObjects"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>        
        private static void QuicksortHeroObjects(HeroKitObject[] heroObjects, int left, int right)
        {
            if (left <= right)
            {
                int lt = left, gt = right;
                int pivot = heroObjects[left].heroGUID;
                int i = left;
                while (i <= gt)
                {
                    if (heroObjects[i].heroGUID < pivot)
                        SwapHeroObjects(heroObjects, lt++, i++);
                    else if (heroObjects[i].heroGUID > pivot)
                        SwapHeroObjects(heroObjects, i, gt--);
                    else
                        i++;
                }

                QuicksortHeroObjects(heroObjects, left, lt - 1);
                QuicksortHeroObjects(heroObjects, gt + 1, right);
            }
        }
        private static void SwapHeroObjects(HeroKitObject[] heroObjects, int i, int j)
        {
            HeroKitObject tmp = heroObjects[i];
            heroObjects[i] = heroObjects[j];
            heroObjects[j] = tmp;
        }

        // ---------------------------------------
        // Set positions of camera and object
        // ---------------------------------------

        /// <summary>
        /// Set the camera position in the new scene.
        /// </summary>
        public static void SetCameraPosition()
        {
            // move the main camera
            Vector3 newCameraPosition = Camera.main.transform.localPosition;
            if (cameraPosition.x != -999999) newCameraPosition.x = cameraPosition.x;
            if (cameraPosition.y != -999999) newCameraPosition.y = cameraPosition.y;
            if (cameraPosition.z != -999999) newCameraPosition.z = cameraPosition.z;
            Camera.main.transform.localPosition = newCameraPosition;
        }
        /// <summary>
        /// Set the camera rotation in the new scene.
        /// </summary>
        public static void SetCameraRotation()
        {
            // move the main camera
            Vector3 newCameraEuler = Camera.main.transform.localEulerAngles;
            if (cameraEuler.x != -999999) newCameraEuler.x = cameraEuler.x;
            if (cameraEuler.y != -999999) newCameraEuler.y = cameraEuler.y;
            if (cameraEuler.z != -999999) newCameraEuler.z = cameraEuler.z;
            Camera.main.transform.localRotation = Quaternion.Euler(newCameraEuler);
        }

        /// <summary>
        /// Get the distance between the camera and object that was moved to the new scene.
        /// </summary>
        /// <param name="currentTargetPosition">The position of the object.</param>
        /// <param name="currentCameraPosition">The position of the camera.</param>
        /// <returns>The distance between the camera and object.</returns>
        public static Vector3 GetCameraObjectPosition(Vector3 currentTargetPosition, Vector3 currentCameraPosition)
        {
            Vector3 newCameraPosition = currentCameraPosition;

            // move camera to object's position?
            if (changeCameraPosition)
            {
                // calculate initial distance between camera and object
                Vector3 cameraDistance = GetDistanceBetweenObjects(currentTargetPosition, currentCameraPosition);              
                newCameraPosition = currentTargetPosition + cameraDistance;

                // it's possible that object moved to a new location. adjust position for this
                if (targetObjectNewPos != currentTargetPosition)
                {
                    Vector3 distance = GetDistanceBetweenObjects(targetObjectNewPos, currentTargetPosition);
                    newCameraPosition -= distance;
                }
            }

            return newCameraPosition;
        }
        /// <summary>
        /// Set the position in the new scene where the object was moved.
        /// </summary>
        public static void SetCameraObjectPosition()
        {
            // move camera to object's position?
            if (changeCameraPosition)
            {
                Camera.main.transform.localPosition = cameraNewPos;
            }
        }

        /// <summary>
        /// Get the rotation of the camera in the new scene where the object was moved.
        /// </summary>
        /// <param name="currentTargetEuler">The rotation of the object.</param>
        /// <returns>The new rotation for the camera.</returns>
        public static Vector3 GetCameraObjectRotation(Vector3 currentTargetEuler)
        {
            Vector3 newCameraEuler = Camera.main.transform.localEulerAngles;

            if (changeCameraRotation)
            {
                if (cameraEuler.x != -999999) newCameraEuler.x = currentTargetEuler.x;
                if (cameraEuler.y != -999999) newCameraEuler.y = currentTargetEuler.y;
                if (cameraEuler.z != -999999) newCameraEuler.z = currentTargetEuler.z;
            }

            return newCameraEuler;
        }
        /// <summary>
        /// Set the rotation of the camera in the new scene where the object was moved.
        /// </summary>
        public static void SetCameraObjectRotation()
        {
            if (changeCameraRotation)
            {
                Camera.main.transform.rotation = Quaternion.Euler(cameraNewEuler);
            }
        }

        /// <summary>
        /// Get the position of the object in the new scene where the object was moved.
        /// </summary>
        /// <param name="currentTargetPosition">The position of the object.</param>
        /// <returns>The new position for the object.</returns>
        public static Vector3 GetObjectPosition(Vector3 currentTargetPosition)
        {
            Vector3 newPosition = currentTargetPosition;
            if (targetPosition.x != -999999) newPosition.x = targetPosition.x;
            if (targetPosition.y != -999999) newPosition.y = targetPosition.y;
            if (targetPosition.z != -999999) newPosition.z = targetPosition.z;
            return newPosition;
        }
        /// <summary>
        /// Set the position of the object in the new scene where the object was moved.
        /// </summary>
        public static void SetObjectPosition()
        {
            targetObject.transform.localPosition = targetObjectNewPos;
        }

        /// <summary>
        /// Get the rotation of the object in the new scene where the object was moved.
        /// </summary>
        /// <param name="currentTargetEuler">The rotation of the object.</param>
        /// <returns>The new rotation for the object.</returns>
        public static Vector3 GetObjectRotation(Vector3 currentTargetEuler)
        {
            Vector3 newEuler = currentTargetEuler;
            if (targetEuler.x != -999999) newEuler.x = targetEuler.x;
            if (targetEuler.y != -999999) newEuler.y = targetEuler.y;
            if (targetEuler.z != -999999) newEuler.z = targetEuler.z;

            return newEuler;
        }
        /// <summary>
        /// Set the rotation of the object in the new scene where the object was moved.
        /// </summary>
        public static void SetObjectRotation()
        {
            targetObject.transform.localRotation = Quaternion.Euler(targetObjectNewEuler);
        }

        /// <summary>
        /// Get the distance between an object and the camera.
        /// </summary>
        /// <param name="target">The position of the object.</param>
        /// <param name="camera">The position of the camera.</param>
        /// <returns>The distance between the object and camera.</returns>
        public static Vector3 GetDistanceBetweenObjects(Vector3 target, Vector3 camera)
        {
            Vector3 distance = new Vector3();
            distance.x = GetDistanceBetweenFloats(target.x, camera.x);
            distance.y = GetDistanceBetweenFloats(target.y, camera.y);
            distance.z = GetDistanceBetweenFloats(target.z, camera.z);

            return distance;
        }
        /// <summary>
        /// Get the distance between a coordinate of the object and camera.
        /// </summary>
        /// <param name="targetPos">Coordinate on the object.</param>
        /// <param name="camPos">Coordinate on the camera.</param>
        /// <returns>Distance between the coordinate on the object and camera.</returns>
        public static float GetDistanceBetweenFloats(float targetPos, float camPos)
        {
            float distance = Math.Abs(targetPos - camPos);
            int cameraSign = (targetPos > camPos) ? -1 : 1;
            float newCameraPosition = cameraSign * distance;

            return newCameraPosition;
        }

        // ---------------------------------------
        // Load cached data
        // ---------------------------------------

        /// <summary>
        /// Load the cached data for a scene after the scene has loaded.
        /// </summary>
        /// <param name="newScene">The scene that was loaded.</param>
        public static void LoadCachedSceneData(UnityEngine.SceneManagement.Scene newScene)
        {
            // get the path where you want to load cached scene data
            string path = Application.temporaryCachePath + "/HeroScenes/" + newScene.name + ".json";

            // get the json data in the file if it exists
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                LoadSceneData(jsonText);
            }
        }
        /// <summary>
        /// Load temporarily cached data for the current scene.
        /// </summary>
        /// <param name="jsonText">The cached data.</param>
        /// <returns>Was the cached data loaded?</returns>
        private static bool LoadSceneData(string jsonText)
        {
            SceneSaveData saveData = JsonUtility.FromJson<SceneSaveData>(jsonText);

            // load hero objects
            for (int i = 0; i < saveData.heroObjects.Length; i++)
                LoadHeroKitObject(saveData.heroObjects[i]);

            return true;
        }
        /// <summary>
        /// Load a hero kit object.
        /// </summary>
        /// <param name="saveData">Save state data for the hero kit object.</param>
        public static void LoadHeroKitObject(HeroSaveData saveData)
        {
            // get the hero kit object to update
            HeroKitObject targetObject = HeroKitDatabase.GetHeroKitObject(saveData.heroGUID);

            // if the hero kit object does not exist, add it to the scene
            if (targetObject == null)
            {
                targetObject = CreateHeroKitObject(saveData);
                targetObject.gameObject.name = saveData.gameObjectName;
            }
            else
            {
                // change name of game object in scene
                targetObject.gameObject.name = saveData.gameObjectName;
            }

            // update transform data
            AddTransform(targetObject, saveData);

            // add hero object if needed
            AddHeroObject(targetObject, saveData);

            // set the state of the hero object
            targetObject.heroStateData.state = saveData.stateID;

            // set the variables for the hero object
            AddVariables(targetObject.heroList.ints.items, saveData.variableInts);
            AddVariables(targetObject.heroList.floats.items, saveData.variableFloats);
            AddVariables(targetObject.heroList.bools.items, saveData.variableBools);
            AddVariables(targetObject.heroList.strings.items, saveData.variableStrings);

            // set the properties for the hero object  
            int pIntCount = 0;
            int pFloatCount = 0;
            int pBoolCount = 0;
            int pStringCount = 0;
            for (int i = 0; i < saveData.propertyCount; i++)
            {               
                AddProperties<IntField, int>(targetObject.heroProperties[i].itemProperties.ints.items, saveData.propertyInts, pIntCount, saveData.pIntCount[i]);
                AddProperties<FloatField, float>(targetObject.heroProperties[i].itemProperties.floats.items, saveData.propertyFloats, pFloatCount, saveData.pFloatCount[i]);
                AddProperties<BoolField, bool>(targetObject.heroProperties[i].itemProperties.bools.items, saveData.propertyBools, pBoolCount, saveData.pBoolCount[i]);                
                AddProperties<StringField, string>(targetObject.heroProperties[i].itemProperties.strings.items, saveData.propertyStrings, pStringCount, saveData.pStringCount[i]);

                pIntCount += saveData.pIntCount[i];
                pFloatCount += saveData.pFloatCount[i];
                pBoolCount += saveData.pBoolCount[i];
                pStringCount += saveData.pStringCount[i];
            }

            // set state of hero kit object
            targetObject.gameObject.SetActive(saveData.gameObjectEnabled);
        }

        /// <summary>
        /// Create the hero kit object if it does not exist.
        /// </summary>
        /// <param name="saveData">Save state data for the hero kit object.</param>
        /// <returns>The hero kit object that was created.</returns>
        private static HeroKitObject CreateHeroKitObject(HeroSaveData saveData)
        {
            // get hero kit object template
            GameObject template = Resources.Load<GameObject>("Hero Templates/Components/HeroKit Default Object");

            // get the root object if it exists
            Transform parentObject = null;
            if (saveData.gameObjectRootName != "")
            {
                Transform rootObject = null;
                GameObject[] roots = SceneManager.GetActiveScene().GetRootGameObjects();
                for (int i = 0; i < roots.Length; i++)
                {
                    if (roots[i].name == saveData.gameObjectRootName)
                    {
                        rootObject = roots[i].transform;
                        break;
                    }
                }

                // get the parent object if it exists
                if (rootObject != null && saveData.gameObjectParentName != "")
                {
                    Transform[] children = rootObject.GetComponentsInChildren<Transform>();
                    for (int i = 0; i < children.Length; i++)
                    {
                        if (children[i].name == saveData.gameObjectParentName)
                        {
                            parentObject = children[i];
                            break;
                        }
                    }

                    if (parentObject == null) parentObject = rootObject;
                }
            }

            // create the game object
            GameObject gameObject = null;
            if (parentObject != null)
            {
                gameObject = UnityEngine.Object.Instantiate(template, new Vector3(), new Quaternion(), parentObject);
            }
            else
            {
                gameObject = UnityEngine.Object.Instantiate(template, new Vector3(), new Quaternion());
            }

            // set up hero kit stuff          
            HeroKitObject targetObject = gameObject.GetComponent<HeroKitObject>();
            targetObject.heroGUID = saveData.heroGUID;

            return targetObject;
        }
        /// <summary>
        /// Add hero object to hero kit object.
        /// </summary>
        /// <param name="targetObject">The hero kit object.</param>
        /// <param name="saveData">The save data for the hero kit object.</param>
        private static void AddHeroObject(HeroKitObject targetObject, HeroSaveData saveData)
        {
            // alert: if there are two objects with same name, wrong data could be loaded
            HeroObject[] heroObjects = Resources.LoadAll<HeroObject>("");
            HeroObject heroObject = null;
            for (int i = 0; i < heroObjects.Length; i++)
            {
                if (heroObjects[i].name == saveData.heroName)
                    heroObject = heroObjects[i];
            }

            if (targetObject.heroObject != heroObject)
            {
                targetObject.ChangeHeroObject(heroObject);
            }
        }
        /// <summary>
        /// Add transform data from save file to hero object.
        /// </summary>
        /// <param name="targetObject">The hero kit object.</param>
        /// <param name="saveData">The save data for the hero kit object.</param>
        private static void AddTransform(HeroKitObject targetObject, HeroSaveData saveData)
        {
            Vector3 heroPosition = new Vector3(saveData.heroPosition[0], saveData.heroPosition[1], saveData.heroPosition[2]);
            targetObject.transform.localPosition = heroPosition;

            //Vector3 heroRotation = new Vector3(saveData.heroRotation[0], saveData.heroRotation[1], saveData.heroRotation[2]);
            //targetObject.transform.localEulerAngles = heroRotation;

            Vector3 heroScale = new Vector3(saveData.heroScale[0], saveData.heroScale[1], saveData.heroScale[2]);
            targetObject.transform.localScale = heroScale;
        }
        /// <summary>
        /// Add variable from save file to hero object.
        /// </summary>
        /// <typeparam name="T">Hero list field type.</typeparam>
        /// <typeparam name="U">Hero list field value type.</typeparam>
        /// <param name="itemsA">The hero list on the hero kit object.</param>
        /// <param name="itemsB">The saved list in the save file.</param>
        public static void AddVariables<T, U>(List<T> itemsA, U[] itemsB) where T : HeroListObjectField<U>
        {
            // set the variables for the hero object
            if (itemsA.Count == itemsB.Length)
            {
                for (int i = 0; i < itemsB.Length; i++)
                {
                    itemsA[i].value = itemsB[i];
                }
            }
            else
            {
                Debug.LogError("Can't add variables. itemsA.Count != itemsB.Length");
            }
        }
        /// <summary>
        /// Add property from save file to hero object.
        /// </summary>
        /// <typeparam name="T">Hero list field type.</typeparam>
        /// <typeparam name="U">Hero list field value type.</typeparam>
        /// <param name="itemsA">The hero list on the hero kit object.</param>
        /// <param name="itemsB">The saved list in the save file.</param>
        public static void AddProperties<T, U>(List<T> itemsA, U[] itemsB, int index, int count) where T : HeroListObjectField<U>
        {
            // index = 3
            // length = 4
            // 1 2 3 [4 4 2 3] 0 0 8

            // set the property for the hero object
            if (itemsA.Count <= itemsB.Length)
            {
                for (int i = 0; count > 0; i++, count--)
                {
                    itemsA[i].value = itemsB[i+index];
                }
            }
            else
            {
                Debug.LogError("Can't add properties. itemsA.Count < itemsB.Length");
            }
        }
    }
}
