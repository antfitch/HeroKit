// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.Collections.Generic;
using System;
using System.Reflection;
using HeroKit.Scene.Actions;
using UnityEngine;

namespace HeroKit.Scene
{
    /// <summary>
    /// Data storage for hero kit objects.
    /// </summary>
    public static class HeroKitDatabase
    {
        //-------------------------------------------
        // Get hero kit globals
        //-------------------------------------------

        /// <summary>
        /// Global variables for HeroKit.
        /// </summary>
        public static HeroList globals;
        /// <summary>
        /// Has the hero kit global variables been created? (we only want this to happen once).
        /// </summary>
        private static bool checkedForGlobals;
        /// <summary>
        /// Get the HeroKit global variables.
        /// </summary>
        /// <returns></returns>
        public static HeroList GetGlobals()
        {
            if (globals == null && !checkedForGlobals)
            {
                // get the template
                HeroKitGlobals template = Resources.Load<HeroKitGlobals>("Hero Globals/HeroKitGlobals");
                if (template == null)
                    Debug.Log("No global variables found for game!");

                // copy value from template into globals
                if (template != null)
                    globals = template.globals.Clone(template.globals);

                // alert game that we've checked for globals
                checkedForGlobals = true;
            }

            if (globals == null)
            {
                Debug.LogError("Global Variables not found!");
            }

            return globals;
        }

        // -----------------------------------------------------------------
        // The dictionary that stores actions in hero kit. 
        // if an action has been used, you can grab it quickly from this dictionary.
        // if an action hasn't been used, we'll add it to this dictionary first.
        // -----------------------------------------------------------------

        /// <summary>
        /// This dictionary collects actions that have been used by HeroKit.
        /// </summary>
        public static Dictionary<string, Func<IHeroKitAction>> ActionDictionary = new Dictionary<string, Func<IHeroKitAction>>();
        /// <summary>
        /// Get a hero action from the action database.
        /// </summary>
        /// <param name="key">The key for the action.</param>
        /// <returns>The action (via it's interface).</returns>
        public static IHeroKitAction GetAction(string key)
        {
            // if action does not exist in dictionary, add it
            if (!ActionDictionary.ContainsKey(key))
            {
                // get the type (several ways exist, this is an eays one)
                Type type = Type.GetType("HeroKit.Scene.Actions." + key);

                // works with public instance/static methods
                MethodInfo method = type.GetMethod("Create");

                // the "magic", turn it into a delegate
                Func<IHeroKitAction> action = (Func<IHeroKitAction>)Delegate.CreateDelegate(typeof(Func<IHeroKitAction>), method);

                // store for future reference
                ActionDictionary.Add(key, action);
            }

            // returns a new instance of the class stored in the dictionary
            return ActionDictionary[key].Invoke();
        }

        // -----------------------------------------------------------------
        // The dictionary that stores game objects that contian hero kit objects in the scene. 
        // if a game object has been used, you can grab it quickly from this dictionary.
        // if a game object hasn't been used, we'll add it to this dictionary first.
        // -----------------------------------------------------------------

        /// <summary>
        /// This dictionary collects game objects with hero kit object scripts on them that have been used by HeroKit.
        /// </summary>
        public static Dictionary<int, HeroKitObject> HeroKitObjectDictionary = new Dictionary<int, HeroKitObject>();
        /// <summary>
        /// Retreive a hero kit object from the HeroKitObject Dictionary.
        /// If the object doesn't exist within the dictionary, add it to the dictionary.
        /// </summary>
        /// <param name="key">GUID assigned to the hero kit object</param>
        /// <returns>The hero kit object.</returns>
        public static HeroKitObject GetHeroKitObject(int key)
        {
            // if object does not exist in dictionary, add it
            if (!HeroKitObjectDictionary.ContainsKey(key))
            {
                // get all game objects with hero kit objects attached to them.
                AddHeroKitObjectKey(key);

                if (!HeroKitObjectDictionary.ContainsKey(key))
                {
                    Debug.LogWarning("No game object in this scene has the Instance ID " + key + ". Game object will be ignored in actions.");
                    return null;
                }

            }
            // if object exists in dictionary, make sure it is not null.
            else if (HeroKitObjectDictionary[key] == null)
            {
                AssignHeroKitObject(key);
            }

            // returns a new instance of the class stored in the dictionary
            return HeroKitObjectDictionary[key];
        }
        /// <summary>
        /// Add a new key and value to the HeroKitObject Dictionary.
        /// </summary>
        /// <param name="key">The key for the hero kit object (it's GUID).</param>
        private static void AddHeroKitObjectKey(int key)
        {
            // get all game objects with hero kit objects attached to them.
            HeroKitObject[] heroKitObjects = Resources.FindObjectsOfTypeAll<HeroKitObject>();

            // get the game object with a GUID that matches the key
            for (int i = 0; i < heroKitObjects.Length; i++)
            {
                if (heroKitObjects[i].heroGUID == key)
                {
                    HeroKitObjectDictionary.Add(key, heroKitObjects[i]);
                    break;
                }
            }
        }
        /// <summary>
        /// Add all hero kit objects in the scene to the HeroKitObject Dictionary.
        /// </summary>
        public static void AddAllHeroKitObjects()
        {
            // get all game objects with hero kit objects attached to them.
            HeroKitObject[] heroKitObjects = Resources.FindObjectsOfTypeAll<HeroKitObject>();

            // get the game object with a GUID that matches the key
            for (int i = 0; i < heroKitObjects.Length; i++)
            {
                if (heroKitObjects[i] != null && heroKitObjects[i].heroObject != null)
                {
                    if (!HeroKitObjectDictionary.ContainsKey(heroKitObjects[i].heroGUID))
                    {
                        HeroKitObjectDictionary.Add(heroKitObjects[i].heroGUID, heroKitObjects[i]);
                    }
                }
            }
        }
        /// <summary>
        /// Adds a hero kit object to the dictionary if it doesn't exist in the dictionary.
        /// </summary>
        /// <param name="key">Key for the hero kit object. (it's GUID).</param>
        public static void AddHeroKitObject(int key)
        {
            // if object does not exist in dictionary, add it
            if (!HeroKitObjectDictionary.ContainsKey(key))
            {
                // get all game objects with hero kit objects attached to them.
                AddHeroKitObjectKey(key);
            }
        }
        /// <summary>
        /// Assign a new hero kit object to a key that already exists within the HeroKitObject Dictionary.
        /// </summary>
        /// <param name="key">Key for the hero kit object. (it's GUID).</param>
        private static void AssignHeroKitObject(int key)
        {
            // get all game objects with hero kit objects attached to them.
            HeroKitObject[] heroKitObjects = Resources.FindObjectsOfTypeAll<HeroKitObject>();

            // get the game object with a GUID that matches the key
            for (int i = 0; i < heroKitObjects.Length; i++)
            {
                if (heroKitObjects[i].heroGUID == key)
                {
                    HeroKitObjectDictionary[key] = heroKitObjects[i];
                    break;
                }
            }
        }

        // -----------------------------------------------------------------
        // The dictionary that stores hero kit key presses. 
        // -----------------------------------------------------------------

        /// <summary>
        /// Has the key dictionary been created? (Only build the key dictionary once).
        /// </summary>
        private static bool keyDictionaryBuilt;
        /// <summary>
        /// This dictionary that contains the key codes for events. These values are stored in EventKeyField.cs.
        /// </summary>
        public static Dictionary<int, KeyCode> KeyboardKeyDictionary = new Dictionary<int, KeyCode>();
        /// <summary>
        /// This dictionary that contains the mouse button codes for events. These values are stored in EventKeyField.cs.
        /// </summary>
        public static Dictionary<int, KeyCode> MouseKeyDictionary = new Dictionary<int, KeyCode>();
        /// <summary>
        /// This dictionary that contains the joystick button codes for events. These values are stored in EventKeyField.cs.
        /// </summary>
        public static Dictionary<int, KeyCode> JoystickKeyDictionary = new Dictionary<int, KeyCode>();
        /// <summary>
        /// Adds input keys into the key dictionary.
        /// </summary>
        public static void BuildKeyDictionary()
        {
            // exit early if dictionary already built
            if (keyDictionaryBuilt) return;

            keyDictionaryBuilt = true;

            KeyboardKeyDictionary.Add(1, KeyCode.LeftArrow);
            KeyboardKeyDictionary.Add(2, KeyCode.RightArrow);
            KeyboardKeyDictionary.Add(3, KeyCode.UpArrow);
            KeyboardKeyDictionary.Add(4, KeyCode.DownArrow);
            KeyboardKeyDictionary.Add(5, KeyCode.LeftControl);
            KeyboardKeyDictionary.Add(6, KeyCode.RightControl);
            KeyboardKeyDictionary.Add(7, KeyCode.LeftShift);
            KeyboardKeyDictionary.Add(8, KeyCode.RightShift);
            KeyboardKeyDictionary.Add(9, KeyCode.Space);
            KeyboardKeyDictionary.Add(10, KeyCode.Return);
            KeyboardKeyDictionary.Add(11, KeyCode.Backspace);
            KeyboardKeyDictionary.Add(12, KeyCode.Tab);
            KeyboardKeyDictionary.Add(13, KeyCode.CapsLock);
            KeyboardKeyDictionary.Add(14, KeyCode.Escape);

            KeyboardKeyDictionary.Add(15, KeyCode.Alpha0);
            KeyboardKeyDictionary.Add(16, KeyCode.Alpha1);
            KeyboardKeyDictionary.Add(17, KeyCode.Alpha2);
            KeyboardKeyDictionary.Add(18, KeyCode.Alpha3);
            KeyboardKeyDictionary.Add(19, KeyCode.Alpha4);
            KeyboardKeyDictionary.Add(20, KeyCode.Alpha5);
            KeyboardKeyDictionary.Add(21, KeyCode.Alpha6);
            KeyboardKeyDictionary.Add(22, KeyCode.Alpha7);
            KeyboardKeyDictionary.Add(23, KeyCode.Alpha8);
            KeyboardKeyDictionary.Add(24, KeyCode.Alpha9);

            KeyboardKeyDictionary.Add(25, KeyCode.A);
            KeyboardKeyDictionary.Add(26, KeyCode.B);
            KeyboardKeyDictionary.Add(27, KeyCode.C);
            KeyboardKeyDictionary.Add(28, KeyCode.D);
            KeyboardKeyDictionary.Add(29, KeyCode.E);
            KeyboardKeyDictionary.Add(30, KeyCode.F);
            KeyboardKeyDictionary.Add(31, KeyCode.G);

            KeyboardKeyDictionary.Add(32, KeyCode.H);
            KeyboardKeyDictionary.Add(33, KeyCode.I);
            KeyboardKeyDictionary.Add(34, KeyCode.J);
            KeyboardKeyDictionary.Add(35, KeyCode.K);
            KeyboardKeyDictionary.Add(36, KeyCode.L);
            KeyboardKeyDictionary.Add(37, KeyCode.M);
            KeyboardKeyDictionary.Add(38, KeyCode.N);
            KeyboardKeyDictionary.Add(39, KeyCode.O);
            KeyboardKeyDictionary.Add(40, KeyCode.P);
            KeyboardKeyDictionary.Add(41, KeyCode.Q);

            KeyboardKeyDictionary.Add(42, KeyCode.R);
            KeyboardKeyDictionary.Add(43, KeyCode.S);
            KeyboardKeyDictionary.Add(44, KeyCode.T);
            KeyboardKeyDictionary.Add(45, KeyCode.U);
            KeyboardKeyDictionary.Add(46, KeyCode.V);
            KeyboardKeyDictionary.Add(47, KeyCode.W);
            KeyboardKeyDictionary.Add(48, KeyCode.X);
            KeyboardKeyDictionary.Add(49, KeyCode.Y);
            KeyboardKeyDictionary.Add(50, KeyCode.Z);

            // MOUSE
            MouseKeyDictionary.Add(1, KeyCode.Mouse0);
            MouseKeyDictionary.Add(2, KeyCode.Mouse1);

            // JOYSTICK
            JoystickKeyDictionary.Add(1, KeyCode.Joystick1Button0);
            JoystickKeyDictionary.Add(2, KeyCode.Joystick1Button1);
            JoystickKeyDictionary.Add(3, KeyCode.Joystick1Button2);
            JoystickKeyDictionary.Add(4, KeyCode.Joystick1Button3);
            JoystickKeyDictionary.Add(5, KeyCode.Joystick1Button4);
            JoystickKeyDictionary.Add(6, KeyCode.Joystick1Button5);
            JoystickKeyDictionary.Add(7, KeyCode.Joystick1Button6);
            JoystickKeyDictionary.Add(8, KeyCode.Joystick1Button7);
            JoystickKeyDictionary.Add(9, KeyCode.Joystick1Button8);
            JoystickKeyDictionary.Add(10, KeyCode.Joystick1Button9);
            JoystickKeyDictionary.Add(11, KeyCode.Joystick1Button10);
            JoystickKeyDictionary.Add(12, KeyCode.Joystick1Button11);
            JoystickKeyDictionary.Add(13, KeyCode.Joystick1Button12);
            JoystickKeyDictionary.Add(14, KeyCode.Joystick1Button13);
            JoystickKeyDictionary.Add(15, KeyCode.Joystick1Button14);
            JoystickKeyDictionary.Add(16, KeyCode.Joystick1Button15);
            JoystickKeyDictionary.Add(17, KeyCode.Joystick1Button16);
            JoystickKeyDictionary.Add(18, KeyCode.Joystick1Button17);
            JoystickKeyDictionary.Add(19, KeyCode.Joystick1Button18);
            JoystickKeyDictionary.Add(20, KeyCode.Joystick1Button19);         
        }

        // -----------------------------------------------------------------
        // The dictionary that stores hero kit pools. 
        // -----------------------------------------------------------------

        /// <summary>
        /// The dictionary that contains pools.
        /// </summary>
        public static Dictionary<string, List<GameObject>> PoolDictionary = new Dictionary<string, List<GameObject>>();
        /// <summary>
        /// Add a pool to the Pool Dictionary.
        /// </summary>
        /// <param name="key">The key for the pool.</param>
        /// <param name="poolObject">The pool game object.</param>
        /// <param name="objectCount">The number of items in the pool.</param>
        /// <param name="isHeroKitObject">Is the pool for hero kit objects?</param>
        public static void AddPool(string key, GameObject poolObject, int objectCount, bool isHeroKitObject)
        {
            if (!PoolDictionary.ContainsKey(key))
            {
                GameObject parent = new GameObject();
                parent.name = key;

                List<GameObject> poolObjects = new List<GameObject>();

                for (int i = 0; i < objectCount; i++)
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate(poolObject, parent.transform);
                    gameObject.SetActive(false);
                    poolObjects.Add(gameObject);

                    if (isHeroKitObject)
                    {
                        HeroKitObject heroKitObject = poolObjects[i].GetComponent<HeroKitObject>();
                        if (heroKitObject != null)
                        {
                            heroKitObject.heroGUID = HeroKitCommonRuntime.GetHeroGUID();
                        }
                    }
                }

                PoolDictionary.Add(key, poolObjects);
            }
            else
            {
                Debug.LogError("Could not add pool. A pool called " + key + " already exists.");
            }
        }
        /// <summary>
        /// Retreive a pool from the Pool Dictionary.
        /// </summary>
        /// <param name="key">The key assigned to the pool.</param>
        /// <returns>The pool.</returns>
        public static List<GameObject> GetPool(string key)
        {
            List<GameObject> pool = null;

            // make sure pool exists
            if (PoolDictionary.ContainsKey(key))
            {
                pool = PoolDictionary[key];
            }
            else
            {
                Debug.LogError("Could not get pool. A pool called " + key + " does not exist.");
            }

            // returns a new instance of the class stored in the dictionary
            return pool;
        }
        /// <summary>
        /// Spawn a prefab.
        /// </summary>
        /// <param name="usePool">Spawn the prefab from a pool?</param>
        /// <param name="poolName">The pool name.</param>
        /// <param name="position">The position in the scene to spawn the prefab.</param>
        /// <param name="rotation">The rotation of the prefab to spawn.</param>
        /// <param name="prefab">The prefab to spawn.</param>
        public static void SpawnPrefab(bool usePool, string poolName, Vector3 position, Quaternion rotation, GameObject prefab=null)
        {
            // spawn directly in scene
            if (usePool)
            {
                SpawnFromPool(poolName, position, rotation);
            }
            else
            {
                // exit early if prefab does not exist
                if (prefab == null) return;

                GameObject gameObject = UnityEngine.Object.Instantiate(prefab, position, rotation);
                gameObject.name = prefab.name;
            }
        }
        /// <summary>
        /// Spawn a hero kit object.
        /// </summary>
        /// <param name="usePool">Spawn the hero kit object from a pool?</param>
        /// <param name="poolName">The pool name.</param>
        /// <param name="position">The position in the scene to spawn the hero kit object.</param>
        /// <param name="rotation">The rotation of the hero kit object.</param>
        /// <param name="heroObject">The type of hero kit object to spawn.</param>
        /// <param name="showLog">Enable debugging for the hero kit object?</param>
        /// <param name="dontSave">Don't allow hero kit object to be saved?</param>
        /// <returns>The hero kit object.</returns>
        public static HeroKitObject SpawnHeroKitObject(bool usePool, string poolName, Vector3 position, Quaternion rotation, HeroObject heroObject, bool showLog, bool dontSave)
        {
            HeroKitObject newHKO;

            // spawn directly in scene
            if (usePool)
            {
                newHKO = SpawnFromPool(poolName, position, rotation, true, heroObject);
            }
            else
            {
                GameObject template = Resources.Load<GameObject>("Hero Templates/Components/HeroKit Default Object");
                GameObject gameObject = UnityEngine.Object.Instantiate(template, position, rotation);
                gameObject.name = heroObject.name;
                newHKO = gameObject.GetComponent<HeroKitObject>();
                newHKO.heroGUID = HeroKitCommonRuntime.GetHeroGUID();
            }

            newHKO.ChangeHeroObject(heroObject);

            // debug object?
            newHKO.debugHeroObject = showLog;

            // ignore object during save?
            newHKO.doNotSave = dontSave;

            return newHKO;
        }
        /// <summary>
        /// Spawn a hero kit object or prefab from a pool.
        /// </summary>
        /// <param name="poolName">The name of the pool.</param>
        /// <param name="position">The position in the scene to spawn the object.</param>
        /// <param name="rotation">The rotation of the object.</param>
        /// <param name="useHKO">Are you spawning a hero kit object?</param>
        /// <param name="heroObject">The type of hero kit object to spawn.</param>
        /// <returns>The hero kit object (if you are spawning a hero kit object). Otherwise, this is always null.</returns>
        public static HeroKitObject SpawnFromPool(string poolName, Vector3 position, Quaternion rotation, bool useHKO = false, HeroObject heroObject = null)
        {
            // increase pool size if it's too small
            bool growPool = false;
            HeroKitObject newHKO = null;

            List<GameObject> poolObjects = GetPool(poolName);
            if (poolObjects != null)
            {
                for (int i = 0; i < poolObjects.Count; i++)
                {
                    if (!poolObjects[i].activeInHierarchy)
                    {
                        if (useHKO)
                        {
                            newHKO = poolObjects[i].GetComponent<HeroKitObject>();
                            newHKO.heroObject = heroObject;
                            if (heroObject != null)
                              poolObjects[i].name = heroObject.name;
                        }

                        poolObjects[i].transform.position = position;
                        poolObjects[i].transform.rotation = rotation;
                        poolObjects[i].SetActive(true);

                        // if this was the last active item in the list, grow the pool
                        if (i == poolObjects.Count - 1)
                        {
                            growPool = true;
                        }

                        break;
                    }
                }

                // double the size of the pool
                if (growPool)
                {
                    int itemsToAdd = poolObjects.Count;

                    for (int i = 0; i < itemsToAdd; i++)
                    {
                        GameObject gameObject = UnityEngine.Object.Instantiate(poolObjects[itemsToAdd - 1], poolObjects[itemsToAdd - 1].transform.parent);
                        gameObject.SetActive(false);
                        poolObjects.Add(gameObject);

                        if (useHKO)
                        {
                            HeroKitObject heroKitObject = gameObject.GetComponent<HeroKitObject>();
                            if (heroKitObject != null)
                            {
                                heroKitObject.heroGUID = HeroKitCommonRuntime.GetHeroGUID();
                            }
                        }
                    }
                }
            }

            return newHKO;
        }

        // -----------------------------------------------------------------
        // The dictionary that stores particle effect pools. 
        // -----------------------------------------------------------------

        /// <summary>
        /// The dictionary that contains particle pools.
        /// </summary>
        public static Dictionary<string, List<ParticleSystem>> ParticlePoolDictionary = new Dictionary<string, List<ParticleSystem>>();
        /// <summary>
        /// Add a particle to a particle pool.
        /// </summary>
        /// <param name="key">The key for the particle.</param>
        /// <param name="poolObject">The pool.</param>
        /// <param name="objectCount">The number of objects to add to the pool.</param>
        public static void AddParticlePool(string key, ParticleSystem poolObject, int objectCount=1, HeroKitObject heroKitObject=null)
        {
            if (!ParticlePoolDictionary.ContainsKey(key))
            {

                GameObject poolContainer = new GameObject();
                poolContainer.name = key;

                ///if particle is inside a heor kit object...
                if (heroKitObject != null)
                {
                    poolContainer.transform.parent = heroKitObject.transform;
                    poolContainer.transform.localPosition = new Vector3();
                    poolContainer.transform.localRotation = new Quaternion();
                }          

                List<ParticleSystem> poolObjects = new List<ParticleSystem>();

                for (int i = 0; i < objectCount; i++)
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate(poolObject.gameObject, poolContainer.transform);
                    gameObject.name = poolObject.gameObject.name;                
                    gameObject.SetActive(false);
                    ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
                    poolObjects.Add(particleSystem);
                }

                ParticlePoolDictionary.Add(key, poolObjects);
            }
            else
            {
                Debug.LogError("Could not add pool. A pool called " + key + " already exists.");
            }
        }
        /// <summary>
        /// Retreive a particle pool from the Pool Dictionary.
        /// </summary>
        /// <param name="key">The key for the particle pool.</param>
        /// <returns>The particle pool.</returns>
        public static List<ParticleSystem> GetParticlePool(string key, bool showLog = true)
        {
            List<ParticleSystem> pool = null;

            // make sure pool exists
            if (ParticlePoolDictionary.ContainsKey(key))
            {
                pool = ParticlePoolDictionary[key];
            }
            else
            {
                if (showLog) Debug.LogError("Could not get pool. A pool called " + key + " does not exist.");
            }

            // returns a new instance of the class stored in the dictionary
            return pool;
        }
        /// <summary>
        /// Spawn a particle from a particle pool.
        /// </summary>
        /// <param name="poolName">The name of the particle pool.</param>
        /// <param name="position">The position in the scene where the particle should spawn.</param>
        /// <param name="rotation">The rotation of the particle.</param>
        /// <returns>The particle.</returns>
        public static ParticleSystem SpawnParticle(string poolName, Vector3 position, Quaternion rotation)
        {
            ParticleSystem newParticleSystem = null;

            // increase pool size if it's too small
            bool growPool = false;

            List<ParticleSystem> poolObjects = GetParticlePool(poolName);
            if (poolObjects != null)
            {
                for (int i = 0; i < poolObjects.Count; i++)
                {
                    if (!poolObjects[i].gameObject.activeInHierarchy)
                    {
                        poolObjects[i].transform.localPosition = position;
                        poolObjects[i].transform.localRotation = rotation;
                        poolObjects[i].gameObject.SetActive(true);
                        newParticleSystem = poolObjects[i];

                        // if this was the last active item in the list, grow the pool
                        if (i == poolObjects.Count - 1)
                        {
                            growPool = true;
                        }

                        break;
                    }
                }

                // double the size of the pool
                if (growPool)
                {
                    int itemsToAdd = poolObjects.Count;

                    for (int i = 0; i < itemsToAdd; i++)
                    {
                        GameObject gameObject = UnityEngine.Object.Instantiate(poolObjects[itemsToAdd - 1].gameObject, poolObjects[itemsToAdd - 1].transform.parent);
                        gameObject.name = poolObjects[itemsToAdd - 1].gameObject.name;
                        gameObject.SetActive(false);
                        ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
                        poolObjects.Add(particleSystem);
                    }
                }
            }

            return newParticleSystem;
        }

        // -----------------------------------------------------------------
        // The dictionary that stores persistent game objects. 
        // -----------------------------------------------------------------

        /// <summary>
        /// This dictionary stores commonly used game objects (persistent objects)
        /// </summary>
        public static Dictionary<string, GameObject> PersistentObjectDictionary = new Dictionary<string, GameObject>();
        /// <summary>
        /// Add a persistent game object to the persistent object dictionary. 
        /// </summary>
        /// <param name="key">The key for the persistent game object.</param>
        /// <param name="gameObject">The persistent game object.</param>
        public static void AddPersistentObject(string key, GameObject gameObject)
        {
            if (!PersistentObjectDictionary.ContainsKey(key))
            {
                PersistentObjectDictionary.Add(key, gameObject);
            }
        }
        /// <summary>
        /// Retreive a persistent game object from the persistent object dictionary.
        /// </summary>
        /// <param name="key">The key for the persistent object.</param>
        /// <returns>The persistent game object.</returns>
        public static GameObject GetPersistentObject(string key)
        {
            GameObject gameObject = null;

            // make sure object exists
            if (PersistentObjectDictionary.ContainsKey(key))
            {
                gameObject = PersistentObjectDictionary[key];
            }
            else
            {
                Debug.LogError("Could not get GameObject. A GameObject called " + key + " does not exist.");
            }

            // returns a new instance of the class stored in the dictionary
            return gameObject;
        }
        /// <summary>
        /// Delete a persistent game object from the persistent object dictionary.
        /// </summary>
        /// <param name="key">The key for the persistent object.</param>
        /// <returns>The persistent game object.</returns>
        public static GameObject DeletePersistentObject(string key)
        {
            GameObject deletedObject = null;

            // make sure object exists
            if (PersistentObjectDictionary.ContainsKey(key))
            {
                deletedObject = PersistentObjectDictionary[key];
                PersistentObjectDictionary.Remove(key);
            }
            else
            {
                Debug.LogError("Could not delete GameObject. A GameObject called " + key + " does not exist.");
            }

            return deletedObject;
        }

        // -----------------------------------------------------------------
        // The dictionary that stores localizatons. 
        // -----------------------------------------------------------------

        /// <summary>
        /// This dictionary stores localization strings.
        /// </summary>
        public static Dictionary<string, string> LocalizationDictionary = new Dictionary<string, string>();
        /// <summary>
        /// Add localization to localization dictionary.
        /// </summary>
        /// <param name="text">The localization key/value pair.</param>
        public static void AddLocalization(List<KeyValuePair<string, string>> text)
        {
            for (int i = 0; i < text.Count; i++)
            {
                if (!LocalizationDictionary.ContainsKey(text[i].Key))
                {
                    LocalizationDictionary.Add(text[i].Key, text[i].Value);
                }
            }
        }
        /// <summary>
        /// Get a localization.
        /// </summary>
        /// <param name="key">The key for the localization.</param>
        /// <returns>The localization.</returns>
        public static string GetLocalization(string key)
        {
            string localization = key;

            if (LocalizationDictionary.ContainsKey(key))
            {
                localization = LocalizationDictionary[key];
            }

            return localization;
        }
    }
}