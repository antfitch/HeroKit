// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.IO;
using HeroKit.Scene.Actions;
using UnityEngine;
using UnityEngine.AI;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene
{
    /// <summary>
    /// Common fields, methods, and other class members used by HeroKit during runtime.
    /// </summary>
    public static class HeroKitCommonRuntime
    {
        //-------------------------------------------
        // System
        //-------------------------------------------

        /// <summary>
        /// Has the game been initialized?
        /// </summary>
        private static bool initGame;

        /// <summary>
        /// Run this once when the first hero kit object in the game is initialized.
        /// </summary>
        public static void initializeGame()
        {
            if (!initGame)
            {
                initGame = true;

                // if key dictionary hasn't been built, do this now
                HeroKitDatabase.BuildKeyDictionary();

                // add event listener to game if it doesn't exist
                UnityEngine.EventSystems.EventSystem[] eventSystem = UnityEngine.Object.FindObjectsOfType<UnityEngine.EventSystems.EventSystem>();
                if (eventSystem == null || eventSystem.Length == 0)
                {
                    string directory = "Hero Templates/Components/";
                    string prefabName = "HeroKit Event System";
                    GameObject template = Resources.Load<GameObject>(directory + prefabName);
                    if (template == null)
                    {
                        Debug.LogError("Can't add prefab to scene because template for " + prefabName + " does not exist.");
                        return;
                    }
                    GameObject gameObject = Object.Instantiate(template, new Vector3(), new Quaternion());
                    gameObject.name = prefabName;
                    UnityEngine.Object.DontDestroyOnLoad(gameObject);
                }

                // create directories for save data
                Directory.CreateDirectory(Application.persistentDataPath + "/HeroSaves/");
                Directory.CreateDirectory(Application.temporaryCachePath + "/HeroScenes/");

                // save the settings scriptable object to a variable
                GetHeroKitSettings();
            }
        }
        /// <summary>
        /// Is the game paused?
        /// </summary>
        public static bool gameIsPaused;
        /// <summary>
        /// Has game terminated?
        /// </summary>
        private static bool gameTerminated;
        /// <summary>
        /// Quit the game.
        /// </summary>
        public static void QuitGame()
        {
            // exit early if game has already been terminated
            if (gameTerminated) return;

            // stop this function from getting called in the future
            gameTerminated = true;

            // delete all files in the temporary cache directory
            DeleteCache();
        }
        /// <summary>
        /// Delete the cache for this game. (The cache contains scene data and global data that was stored for this game session).
        /// </summary>
        public static void DeleteCache()
        {
            string path = Application.temporaryCachePath + "/HeroScenes/";

            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
        /// <summary>
        /// Get a saved game.
        /// </summary>
        /// <param name="saveGameName">The name of the save game file.</param>
        /// <returns>The saved game data.</returns>
        public static GameSaveData GetSaveGame(string saveGameName)
        {
            GameSaveData saveData = null;

            string path = Application.persistentDataPath + "/HeroSaves/" + saveGameName + ".json";

            // get the json data in the file
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);

                // get the game save class
                saveData = JsonUtility.FromJson<GameSaveData>(jsonText);
            }

            return saveData;
        }
        /// <summary>
        /// The hero kit settings scriptable object.
        /// </summary>
        public static HeroKitSettings settingsInfo;
        /// <summary>
        /// Get the hero kit settings scriptable object. This object stores
        /// the menu prefabs and general settings stored in the 
        /// HeroKit Editor's Settings page.
        /// </summary>
        /// <returns>The settings scriptable object</returns>
        public static HeroKitSettings GetHeroKitSettings()
        {
            HeroKitSettings hkoSettings = Resources.Load<HeroKitSettings>("Hero Settings/HeroKitSettings");


            // if session still does not exist, raise error
            if (hkoSettings == null)
            {
                Debug.LogError("HeroKitSettings file not found in HeroKit/Hero Engine/Assets/Hero Settings directory. Reinstall this file.");
                return null;
            }

            // copy data from hkoSettings. this is important because if you don't, the actual asset will be update.
            settingsInfo = new HeroKitSettings();
            settingsInfo.dialogBox = hkoSettings.dialogBox;
            settingsInfo.fadeInOutScreen = hkoSettings.fadeInOutScreen;
            settingsInfo.gameoverMenu = hkoSettings.gameoverMenu;
            settingsInfo.inventoryItem = hkoSettings.inventoryItem;
            settingsInfo.inventoryMenu = hkoSettings.inventoryMenu;
            settingsInfo.inventorySlot = hkoSettings.inventorySlot;
            settingsInfo.journalItem = hkoSettings.journalItem;
            settingsInfo.journalMenu = hkoSettings.journalMenu;
            settingsInfo.journalSlot = hkoSettings.journalSlot;
            settingsInfo.optionsMenu = hkoSettings.optionsMenu;
            settingsInfo.saveMenu = hkoSettings.saveMenu;
            settingsInfo.saveSlot = hkoSettings.saveSlot;
            settingsInfo.startMenu = hkoSettings.startMenu;

            return settingsInfo;
        }

        //-------------------------------------------
        // General
        //-------------------------------------------

        /// <summary>
        /// Create a NavMeshAgent for a hero kit object that needs to use pathfinding.
        /// </summary>
        /// <param name="navMeshAgent">The nav mesh agent.</param>
        /// <returns>The nav mesh agent.</returns>
        public static NavMeshAgent CreateNavMeshAgent(NavMeshAgent navMeshAgent)
        {
            // get nav mesh agent from prefab
            string prefabName = "HeroKit Pathfinding";
            GameObject prefab = Resources.Load<GameObject>("Hero Templates/Components/" + prefabName);
            if (prefab == null)
            {
                Debug.LogError("Can't add dialog box to scene because template for " + prefabName + " does not exist.");
                return null;
            }

            // get nav mesh agent from prefab
            NavMeshAgent prefabAgent = prefab.GetComponent<NavMeshAgent>();

            // port settings from prefab to game object
            navMeshAgent.radius = prefabAgent.radius;
            navMeshAgent.height = prefabAgent.height;
            navMeshAgent.baseOffset = prefabAgent.baseOffset;
            navMeshAgent.speed = prefabAgent.speed;
            navMeshAgent.acceleration = prefabAgent.acceleration;
            navMeshAgent.stoppingDistance = prefabAgent.stoppingDistance;
            navMeshAgent.autoBraking = prefabAgent.autoBraking;
            navMeshAgent.obstacleAvoidanceType = prefabAgent.obstacleAvoidanceType;
            navMeshAgent.avoidancePriority = prefabAgent.avoidancePriority;
            navMeshAgent.autoTraverseOffMeshLink = prefabAgent.autoTraverseOffMeshLink;
            navMeshAgent.autoRepath = prefabAgent.autoRepath;
            navMeshAgent.areaMask = prefabAgent.areaMask;

            // return navMeshAgent
            return navMeshAgent;
        }
        /// <summary>
        /// What direction is the target game object facing?
        /// </summary>
        /// <param name="gameObjectB">Source game object.</param>
        /// <param name="gameObjectA">Target game object.</param>
        /// <returns>Direction that the target game object is facing. (in comparison to source game object)</returns>
        public static int getDirection(GameObject gameObjectB, GameObject gameObjectA)
        {
            int position = -1;

            // get stuff to check if object is in front or behind
            Vector3 direction = gameObjectA.transform.forward;
            Vector3 toOther = (gameObjectB.transform.localPosition - gameObjectA.transform.localPosition).normalized;
            float VerticalDot = Vector3.Dot(direction, toOther);

            // behind
            if (VerticalDot < -0.7f)
            {
                position = 2;
            }

            // in front of
            else if (VerticalDot > 0.7f)
            {
                position = 1;
            }

            // get stuff to check if object is to left or to right
            direction = gameObjectA.transform.right;
            toOther = (gameObjectB.transform.localPosition - gameObjectA.transform.localPosition).normalized;
            VerticalDot = Vector3.Dot(direction, toOther);

            // to right of
            if (VerticalDot < -0.7f)
            {
                position = 3;
            }

            // to left of
            else if (VerticalDot > 0.7f)
            {
                position = 4;
            }

            return position;
        }

        //-------------------------------------------
        // Random numbers
        //-------------------------------------------

        /// <summary>
        /// Stores a random number.
        /// </summary>
        /// <remarks>This is a seed. We must have one seed for random number generation otherwise the same number will be generated in same frame</remarks>
        private static System.Random random = new System.Random();
        /// <summary>
        /// Get a random GUID for a Hero Kit Object.
        /// </summary>
        /// <returns>The GUID.</returns>
        public static int GetHeroGUID()
        {
            return random.Next(int.MinValue, int.MaxValue);
        }
        /// <summary>
        /// Get a random integer.
        /// </summary>
        /// <param name="bottom">The smallest number possible.</param>
        /// <param name="top">The largest number possible.</param>
        /// <returns>A number.</returns>
        public static int GetRandomInt(int bottom, int top)
        {
            return random.Next(bottom, top);
        }
        /// <summary>
        /// Get a random float.
        /// </summary>
        /// <param name="bottom">The smallest number possible.</param>
        /// <param name="top">The largest number possible.</param>
        /// <returns>A number.</returns>
        public static float GetRandomFloat(float bottom, float top)
        {
            return random.Next((int)bottom, (int)top);
        }
        /// <summary>
        /// Get a random bool.
        /// </summary>
        /// <returns>True or false.</returns>
        public static bool GetRandomBool()
        {
            int result = random.Next(1,2);
            return result == 1;
        }



        //-------------------------------------------
        // Comparison
        //-------------------------------------------

        /// <summary>
        /// Check to see if two colors match.
        /// </summary>
        /// <param name="color1">The first value.</param>
        /// <param name="color2">The second value.</param>
        /// <returns>Do the values match?</returns>
        public static bool DoColorsMatch(Color color1, Color color2)
        {
            bool result = false;

            // once color is back to normal, exit
            bool r = (Mathf.Abs(color1.r - color2.r) <= 0.0001);
            bool g = (Mathf.Abs(color1.g - color2.g) <= 0.0001);
            bool b = (Mathf.Abs(color1.b - color2.b) <= 0.0001);
            bool a = (Mathf.Abs(color1.a - color2.a) <= 0.0001);
            if (r && g && b && a)
            {
                result = true;
            }

            return result;
        }
        /// <summary>
        /// Check to see if two floats match.
        /// </summary>
        /// <param name="float1">The first value.</param>
        /// <param name="float2">The second value.</param>
        /// <returns>Do the values match?</returns>
        public static bool DoFloatsMatch(float float1, float float2)
        {
            bool result = (Mathf.Abs(float1 - float2) <= 0.0001);
            return result;
        }

        //-------------------------------------------
        // Debugging
        //-------------------------------------------

        private static string variableDoesNotExist = "\nVariable no longer exists within the Variable List. Please check this action to make sure it is referencing a variable that exists on the hero object.";
        /// <summary>
        /// Display information about a hero kit object.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object.</param>
        /// <returns>Information about the hero kit object.</returns>
        public static string GetHeroDebugInfo(HeroKitObject heroKitObject)
        {
            string objectName = heroKitObject.gameObject.name;
            string heroKitObjectName = heroKitObject.heroObject.name;
            int stateID = heroKitObject.heroStateData.state;
            string stateName = heroKitObject.heroState.name;
            int eventID = heroKitObject.heroStateData.eventBlock;
            string eventName = heroKitObject.heroState.heroEvent[eventID].name;
            int actionID = heroKitObject.heroStateData.action;
            string actionName = heroKitObject.heroState.heroEvent[eventID].actions[actionID].actionTemplate.name;

            return "\n\n---------------------------------------------" + "\n" +
                   "Game Object: " + objectName + "\n" +
                   "Hero Object: " + heroKitObjectName + "\n" +
                   "State " + stateID + ": " + stateName + "\n" +
                   "Event " + eventID + ": " + eventName + "\n" +
                   "Action " + actionID + ": " + actionName + "\n" + 
                   "---------------------------------------------\n";
        }
        /// <summary>
        /// Display information about an action.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object.</param>
        /// <returns>Information about the action.</returns>
        public static string GetActionDebugInfo(HeroKitObject heroKitObject, string message="", string actionSuffix="")
        {
            int stateID = heroKitObject.heroStateData.state;
            int eventID = heroKitObject.heroStateData.eventBlock;
            int actionID = heroKitObject.heroStateData.action;
            string name = heroKitObject.heroState.heroEvent[eventID].actions[actionID].name + actionSuffix;
            string actionTitle = heroKitObject.heroState.heroEvent[eventID].actions[actionID].actionTemplate.title;
            string actionName = heroKitObject.heroState.heroEvent[eventID].actions[actionID].actionTemplate.name;

            if (name == "") name = actionName; 

            return heroKitObject.gameObject.name + " | State " + stateID + " | Event " + eventID + " | Action " + actionID + " | " + actionTitle + name +
                   "\n\n---------------------------------------------\n" +
                   "Action Name: " + actionName + "\n" + message +
                   "\n---------------------------------------------" +
                   GetHeroDebugInfo(heroKitObject);
        }
        /// <summary>
        /// Display information about a game object.
        /// </summary>
        /// <param name="heroKitObject">The game object.</param>
        /// <returns>Information about the game object.</returns>
        public static string GetGameObjectDebugInfo(GameObject gameObject)
        {
            string objectName = gameObject.name;

            return "\n---------------------------------------------" + "\n" +
                   "Game Object: " + objectName + "\n" +
                   "X: " + gameObject.transform.position.x + "\n" +
                   "Y: " + gameObject.transform.position.y + "\n" +
                   "Z: " + gameObject.transform.position.z + "\n" +
                   "---------------------------------------------";
        }
        /// <summary>
        /// Display information about a variable.
        /// </summary>
        /// <param name="heroName">The name of the hero object that contains the variable.</param>
        /// <param name="listType">The type of variable list.</param>
        /// <param name="variableType">The type of variable.</param>
        /// <param name="variableID">The ID assigned to the variable.</param>
        /// <returns>Information about the variable</returns>
        public static string GetVariableDebugInfo(string heroName, string listType, string variableType, int variableID)
        {
            return "\n\n---------------------------------------------" + "\n" +
                   "Hero Object: " + heroName + "\n" +
                   "List: " + listType + "\n" +
                   "Type: " + variableType + "\n" +
                   "Slot ID: " + variableID + "\n" +
                   "---------------------------------------------";
        }
        /// <summary>
        /// Display a message that says hero kit object was not found.
        /// </summary>
        /// <param name="heroName">The name of the hero kit object that was not found.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="heroKitObject">The hero kit object from which this message originates.</param>
        /// <returns>A message that says the hero kit object was not found.</returns>
        public static string NoHeroKitObjectDebugInfo(string heroName, System.Object returnValue, HeroKitObject heroKitObject)
        {
            return "Hero Kit Object was not found for " + heroName + ". Returning " + returnValue + ". " + variableDoesNotExist + GetHeroDebugInfo(heroKitObject);
        }
        /// <summary>
        /// Display a message that says no target hero kit object was found.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object from which this message originates.</param>
        /// <returns>A message that says no target hero kit object was found.</returns>
        public static string NoTargetHeroKitObjectDebugInfo(HeroKitObject heroKitObject)
        {
            return "Target hero kit object does not exist. Can't perform action." + GetHeroDebugInfo(heroKitObject);
        }
        /// <summary>
        /// Display a message that says hero object was not found.
        /// </summary>
        /// <param name="heroName">The name of the hero object that was not found.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="heroKitObject">The hero kit object from which this message originates.</param>
        /// <returns>A message that says the hero object was not found.</returns>
        public static string NoHeroObjectDebugInfo(string heroName, System.Object returnValue, HeroKitObject heroKitObject)
        {
            return "Hero Object was not found for " + heroName + ". Returning " + returnValue + ". " + variableDoesNotExist + GetHeroDebugInfo(heroKitObject);
        }
        /// <summary>
        /// Display a message that says a variable was not found.
        /// </summary>
        /// <param name="heroName">Name of the hero object that contains the variable.</param>
        /// <param name="targetHeroName">Target hero object.</param>
        /// <param name="listType">Variable list type.</param>
        /// <param name="variableType">Variable type.</param>
        /// <param name="variableID">ID assigned to the variable.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="heroKitObject">The hero kit object from which this message originates.</param>
        /// <returns>A message that says the variable was not found.</returns>
        public static string NoVariableDebugInfo(string heroName, string targetHeroName, string listType, string variableType, int variableID, System.Object returnValue, HeroKitObject heroKitObject)
        {
            return variableType + " was not found for " + heroName + ". Returning " + returnValue + ". " + variableDoesNotExist + 
                GetVariableDebugInfo(targetHeroName, listType, variableType, variableID) + 
                GetHeroDebugInfo(heroKitObject);
        }
        /// <summary>
        /// Display a message that says hero kit object was not found in a hero object list.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object from which this message originates.</param>
        /// <param name="listType">The type of hero list.</param>
        /// <param name="slotID">The ID assigned to the hero kit object in the hero object list.</param>
        /// <returns>A message that says the hero kit object was not found.</returns>
        public static string NoHeroKitObjectInListDebugInfo(HeroKitObject heroKitObject, string listType, int slotID)
        {
            string heroKitObjectName = heroKitObject.heroObject.name;
            int eventID = heroKitObject.heroStateData.eventBlock;
            int actionID = heroKitObject.heroStateData.action;
            string actionName = heroKitObject.heroState.heroEvent[eventID].actions[actionID].actionTemplate.name;

            return "Hero kit object" + " was not found for " + actionName + ". Returning " + "null" + "." +
                GetVariableDebugInfo(heroKitObjectName, listType, "Hero Kit Object", slotID) +
                GetHeroDebugInfo(heroKitObject);
        }
        /// <summary>
        /// Display a message that says a component on a child game object was not found.
        /// </summary>
        /// <param name="componentName">The name of the component.</param>
        /// <param name="gameObjectName">The name of the child game object.</param>
        /// <param name="heroKitObject">The hero kit object from which this message originates.</param>
        /// <returns>A message that says a component on a child game object was not found.</returns>
        public static string NoComponentOnChildDebugInfo(string componentName, string gameObjectName, HeroKitObject heroKitObject)
        {
            return "Can't perform action because " + componentName + " does not exist. \n" +
                   "Your hero kit object should have a child game object called \n" +
                   gameObjectName + " attached to it." +
                   GetHeroDebugInfo(heroKitObject);
        }
        /// <summary>
        /// Display a message that says a component was not found on a hero kit object.
        /// </summary>
        /// <param name="componentName">The name of the component.</param>
        /// <param name="heroKitObject">The hero kit object from which this message originates.</param>
        /// <returns>A message that says a component was not found on a hero kit object.</returns>
        public static string NoComponentDebugInfo(string componentName, HeroKitObject heroKitObject)
        {
            return "Can't perform action because " + componentName + " does not exist." +
                   GetHeroDebugInfo(heroKitObject);
        }
        /// <summary>
        /// Display a message that says a component was not found on a hero kit object.
        /// </summary>
        /// <param name="heroName">The name of the hero object.</param>
        /// <param name="componentName">The name of the component.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="heroKitObject">The hero kit object from which this message originates.</param>
        /// <returns>A message that says a component was not found on a hero kit object.</returns>
        public static string NoComponentDebugInfo(string heroName, string componentName, System.Object returnValue, HeroKitObject heroKitObject)
        {
            return "Can't perform action because " + componentName + " does not exist for " + heroName + ". It appears that this variable no longer exists. Returning " + returnValue + "." +
                   GetHeroDebugInfo(heroKitObject);
        }

        //-------------------------------------------
        // Visuals
        //-------------------------------------------

        /// <summary>
        /// The name to apply to the child game object which contains the visuals for its parent hero kit object.
        /// </summary>
        public static string visualsName = "visuals";
        /// <summary>
        /// Get the first state on a hero kit object and add its visual information to the game object.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object.</param>
        public static void RefreshVisuals(HeroKitObject heroKitObject)
        {
            // exit early if there is no hero object
            if (heroKitObject == null || heroKitObject.heroObject == null) return;

            // exit early if there is no default state on the hero object
            if (heroKitObject.heroObject.states.states == null || heroKitObject.heroObject.states.states.Count == 0) return;

            // make a shortcut for the first state
            HeroState state = heroKitObject.heroObject.states.states[0];

            UpdateGraphicsForState(heroKitObject.gameObject, state, true);
        }
        /// <summary>
        /// Refresh visuals for all hero objects of a specific type in the scene.
        /// </summary>
        /// <param name="heroObject">The type of hero object to refresh in the scene.</param>
        public static void RefreshAllVisuals(HeroObject heroObject)
        {
            HeroKitObject[] items = Object.FindObjectsOfType<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].heroObject == heroObject)
                {
                    RefreshVisuals(items[i]);
                }
            }
        }
        /// <summary>
        /// Refresh visuals for all hero objects in the scene.
        /// </summary>
        public static void RefreshAllVisuals()
        {
            HeroKitObject[] items = Object.FindObjectsOfType<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                RefreshVisuals(items[i]);
            }
        }
        /// <summary>
        /// Get the child game object which contains the visuals for a hero kit object.
        /// </summary>
        /// <param name="HKOGameObject">The game object for the hero kit object.</param>
        /// <returns>The visuals game object for a hero kit object.</returns>
        public static GameObject GetVisualsGameObject(GameObject HKOGameObject)
        {
            // get the transform attached to the child
            Transform child = HKOGameObject.transform.Find(visualsName);

            // exit early if there is no child object
            if (child == null) return null;

            // return game object attached to child transform
            return child.gameObject;
        }
        /// <summary>
        /// Turn the renderer on a game object on or off.
        /// Notes: the renderer determines whether visuals on a game object are visible.
        /// </summary>
        /// <param name="transform">Transform of the game object.</param>
        /// <param name="enabled">Turn visuals on (true), or turn visuals off (false).</param>
        public static void toggleRenderer(Transform transform, bool enabled)
        {
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    toggleRenderer(child, enabled);
                }
            }

            Renderer renderer = transform.gameObject.GetComponent<Renderer>();
            if (renderer != null)
                transform.gameObject.GetComponent<Renderer>().enabled = enabled;
        }

        //-------------------------------------------
        // State Changes
        //-------------------------------------------

        /// <summary>
        /// Update the visuals for a specific state on a hero kit object.
        /// </summary>
        /// <param name="HKOGameObject">The game object for the hero kit object.</param>
        /// <param name="heroState">The state of the hero kit object.</param>
        /// <param name="editMode">Are we refreshing the visuals via the editor?</param>
        public static void UpdateGraphicsForState(GameObject HKOGameObject, HeroState heroState, bool editMode=false)
        {
            int imageType = heroState.heroVisuals.imageType;

            switch (imageType)
            {
                case 0: // no value
                    break;
                case 1: // use what exists on game object
                    break;
                case 2: // use prefab
                case 3: // use no image
                    AddPrefab(HKOGameObject, heroState.heroVisuals.prefab, editMode, visualsName);
                    AddRigidbody(heroState.heroVisuals.rigidbody, HKOGameObject);
                    AddRigidbody2D(heroState.heroVisuals.rigidbody2D, HKOGameObject);
                    break;
            }
        }
        /// <summary>
        /// Hide the visuals for a specific state on a hero kit object.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object.</param>
        public static void HideGraphicsForState(HeroKitObject heroKitObject)
        {
            // remove the rigidbody
            RemoveRigidbody(heroKitObject.gameObject);

            // hide the visuals
            AddPrefab(heroKitObject.gameObject, null, false, visualsName);
        }

        //-------------------------------------------
        // Prefabs
        //-------------------------------------------

        /// <summary>
        /// Add a prefab to another game object as a child.
        /// </summary>
        /// <param name="parentGameObject">The parent game object.</param>
        /// <param name="prefab">The prefab that will be added as the child.</param>
        /// <param name="editMode">Are we refreshing the visuals via the editor?</param>
        /// <param name="childName">The new name for the child game object.</param>
        /// <returns>The child game object.</returns>
        public static GameObject AddPrefab(GameObject parentGameObject, GameObject prefab, bool editMode, string childName)
        {
            // get the transform attached to the child
            Transform child = parentGameObject.transform.Find(childName);

            // remove the child if it exists
            if (child != null)
            {
                // get the game object that contains the visuals & delete it
                if (!editMode)
                    Object.Destroy(child.gameObject);
                else
                    Object.DestroyImmediate(child.gameObject);
            }

            // exit early if there is no prefab to add
            if (prefab == null) return null;

            // add the prefab as a child object to the hero kit object
            child = Object.Instantiate(prefab.transform, parentGameObject.transform);
            child.name = childName;
            child.position = parentGameObject.transform.position;

            // get the new game object that contains the visuals
            return parentGameObject.transform.Find(childName).gameObject;
        }
        /// <summary>
        /// Get a prefab from an assets/resources folder.
        /// </summary>
        /// <param name="prefabName">The name of the prefab.</param>
        /// <param name="directory">The directory of the prefabb.</param>
        /// <param name="persistent">Should this prefab be made persistent when added to the scene?</param>
        /// <returns>The hero kit object attached to the newly created game object.</returns>
        public static HeroKitObject GetPrefabFromAssets(GameObject prefab, bool persistent)
        {
            // add prefab to scene if it doesn't exist
            if (!HeroKitDatabase.PersistentObjectDictionary.ContainsKey(prefab.name))
            {
                AddPrefabToScene(prefab, persistent);
            }

            // get the hero kit object attached to the prefab
            GameObject targetGameObject = HeroKitDatabase.GetPersistentObject(prefab.name);
            HeroKitObject targetObject = targetGameObject.GetComponent<HeroKitObject>();

            return targetObject;
        }
        /// <summary>
        /// If it doesn't exist, add a prefab to the scene.
        /// </summary>
        /// <param name="prefabName">The name of the prefab.</param>
        /// <param name="directory">The directory of the prefab.</param>
        /// <param name="persistent">Should this prefab be made persistent when added to the scene?</param>
        public static void AddPrefabToScene(GameObject template, bool persistent)
        {
            // add prefab to scene if it doesn't already exist
            if (template == null)
            {
                Debug.LogError("Can't add prefab to scene because template does not exist.");
                return;
            }

            GameObject gameObject = Object.Instantiate(template, new Vector3(), new Quaternion());
            gameObject.name = template.name;

            // add the object to the game object dictionary
            HeroKitDatabase.AddPersistentObject(template.name, gameObject);

            // hide it
            gameObject.SetActive(false);

            // make prefab persistent
            if (persistent)
            {
                HeroKitObject prefabHKO = gameObject.GetComponent<HeroKitObject>();
                if (prefabHKO == null)
                {
                    Debug.LogError("Can't make prefab persistent because hero kit object component is missing.");
                    return;
                }

                MakePersistent makePersistent = new MakePersistent();
                makePersistent.ExecuteOnTarget(prefabHKO);
            }
        }
        /// <summary>
        /// Check if a prefab exists in the scene.
        /// </summary>
        /// <param name="prefab">The prefab.</param>
        /// <param name="persistent">Is the prefab a persistent object?</param>
        /// <returns>True or false.</returns>
        public static bool IsPrefabInScene(GameObject prefab, bool persistent)
        {
            // add prefab to scene if it doesn't exist
            if (HeroKitDatabase.PersistentObjectDictionary.ContainsKey(prefab.name))
                return true;
            else
                return false;
        }
        /// <summary>
        /// If it exist, delete a prefab from the scene.
        /// </summary>
        /// <param name="prefabName">The name of the prefab.</param>
        /// <param name="directory">The directory of the prefab.</param>
        /// <param name="persistent">Is this a persistent object in the scene?</param>
        public static void DeletePrefabFromScene(GameObject prefab, bool persistent)
        {
            // add prefab to scene if it doesn't already exist
            if (prefab == null)
            {
                Debug.LogError("Can't delete prefab from scene because it does not exist.");
                return;
            }

            if (IsPrefabInScene(prefab, true))
            {
                // delete the old menu in the scene
                GameObject prefabInScene = HeroKitDatabase.DeletePersistentObject(prefab.name);
                UnityEngine.Object.Destroy(prefabInScene);
            }
        }

        //-------------------------------------------
        // 3D Rigidbodies
        //-------------------------------------------

        /// <summary>
        /// Add a 3D ridigbody to a game object.
        /// </summary>
        /// <param name="newRigidbody">The rigidbody to add (leave this empty of you want to delete the rigidbody on a game object).</param>
        /// <param name="gameObject">The game object.</param>
        public static void AddRigidbody(Rigidbody newRigidbody, GameObject gameObject)
        {
            // if no rigidbody needed, delete existing one and exit early
            if (newRigidbody == null)
            {
                Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    Object.DestroyImmediate(rigidbody);
                }
            }
            else
            {
                Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
                if (rigidbody == null)
                    rigidbody = gameObject.AddComponent<Rigidbody>();

                rigidbody.mass = newRigidbody.mass;
                rigidbody.drag = newRigidbody.drag;
                rigidbody.angularDrag = newRigidbody.angularDrag;
                rigidbody.useGravity = newRigidbody.useGravity;
                rigidbody.isKinematic = newRigidbody.isKinematic;
                rigidbody.interpolation = newRigidbody.interpolation;
                rigidbody.collisionDetectionMode = newRigidbody.collisionDetectionMode;
                rigidbody.constraints = newRigidbody.constraints;
            }
        }
        /// <summary>
        /// Remove a 3D rigidbody from a game object.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        public static void RemoveRigidbody(GameObject gameObject)
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                Object.DestroyImmediate(rigidbody);
            }
        }

        //-------------------------------------------
        // 2D Rigidbodies
        //-------------------------------------------

        /// <summary>
        /// Add a 2D ridigbody to a game object.
        /// </summary>
        /// <param name="newRigidbody">The rigidbody to add (leave this empty of you want to delete the rigidbody on a game object).</param>
        /// <param name="gameObject">The game object.</param>
        public static void AddRigidbody2D(Rigidbody2D newRigidbody, GameObject gameObject)
        {
            // if no rigidbody needed, delete existing one and exit early
            if (newRigidbody == null)
            {
                Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
                if (rigidbody != null)
                {
                    Object.DestroyImmediate(rigidbody);
                }
            }
            else
            {
                Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
                if (rigidbody == null)
                    rigidbody = gameObject.AddComponent<Rigidbody2D>();

                rigidbody.mass = newRigidbody.mass;
                rigidbody.drag = newRigidbody.drag;
                rigidbody.angularDrag = newRigidbody.angularDrag;
                rigidbody.gravityScale = newRigidbody.gravityScale;
                rigidbody.isKinematic = newRigidbody.isKinematic;
                rigidbody.interpolation = newRigidbody.interpolation;
                rigidbody.collisionDetectionMode = newRigidbody.collisionDetectionMode;
                rigidbody.constraints = newRigidbody.constraints;
            }
        }
        /// <summary>
        /// Remove a 2D rigidbody from a game object.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        public static void RemoveRigidbody2D(GameObject gameObject)
        {
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                Object.DestroyImmediate(rigidbody);
            }
        }

        //-------------------------------------------
        // Messages and localization
        //-------------------------------------------

        /// <summary>
        /// Write a message character-by-character.
        /// </summary>
        public static bool writeMessage;
        /// <summary>
        /// Time to wait between drawing each character in the message.
        /// </summary>
        public static float messageWaitTime;
        /// <summary>
        /// Position of the message.
        /// </summary>
        public static TextAnchor messageAlignment = TextAnchor.UpperLeft;
        /// <summary>
        /// Alpha for the message background.
        /// </summary>
        public static float messageBackgroundAlpha = 1f;
        /// <summary>
        /// Change the message background transparency?
        /// </summary>
        public static bool changeMessageBackgroundTransparency;
        /// <summary>
        /// Alpha for the message button.
        /// </summary>
        public static float messageButtonAlpha = 1f;
        /// <summary>
        /// Change the message button transparency?
        /// </summary>
        public static bool changeMessageButtonTransparency;
        /// <summary>
        /// Change the message background?
        /// </summary>
        public static bool changeMessageBackground;
        /// <summary>
        /// The image to use for the message background.
        /// </summary>
        public static Sprite messageBackgroundImage;
        /// <summary>
        /// Change the message button background?
        /// </summary>
        public static bool changeMessageButton;
        /// <summary>
        /// The image to use for the message button background.
        /// </summary>
        public static Sprite messageButtonImage;
        /// <summary>
        /// Layout style for message buttons. (horizontal layout or vertical layout)
        /// </summary>
        public static int messageButtonLayout = 1;
        /// <summary>
        /// Change the message button layout?
        /// </summary>
        public static bool changeMessageButtonLayout;
        /// <summary>
        /// Directory where localized audio is stored for messages.
        /// </summary>
        public static string localizatonDirectory;
        /// <summary>
        /// color for text in the dialog box.
        /// </summary>
        public static Color messageTextColor = new Color();
        /// <summary>
        /// Change the message text color?
        /// </summary>
        public static bool changeMessageTextColor;
        /// <summary>
        /// color for heading in the dialog box.
        /// </summary>
        public static Color messageHeadingColor = new Color();
        /// <summary>
        /// Change the message heading color?
        /// </summary>
        public static bool changeMessageHeadingColor;
        /// <summary>
        /// color for button text in the dialog box.
        /// </summary>
        public static Color messageButtonTextColor = new Color();
        /// <summary>
        /// Change the message button text color?
        /// </summary>
        public static bool changeMessageButtonTextColor;
        /// <summary>
        /// color for active button in the dialog box.
        /// </summary>
        public static Color messageButtonActiveColor = new Color();
        /// <summary>
        /// Change the active button color?
        /// </summary>
        public static bool changeMessageButtonActiveColor;

        //-------------------------------------------
        // Game Objects & Hero Kit Objects
        //-------------------------------------------

        /// <summary>
        /// Get game objects from scene objects.
        /// </summary>
        /// <param name="data">The scene object data.</param>
        /// <returns>Game objects.</returns>
        public static GameObject[] GetGameObjectsFromSceneObjects(SceneObjectValueData data)
        {
            // get the game object to work with
            GameObject[] targetObject;
            if (data.heroKitObject != null)
            {
                targetObject = new GameObject[data.heroKitObject.Length];
                for (int i = 0; i < data.heroKitObject.Length; i++)
                    targetObject[i] = data.heroKitObject[i].gameObject;
            }
            else
            {
                targetObject = data.gameObject;
            }

            return targetObject;
        }
        /// <summary>
        /// Get a child game object from a parent game object.
        /// </summary>
        /// <param name="parent">The name of the parent game object.</param>
        /// <param name="childName">The name of the child game object.</param>
        /// <param name="includeInactive">Get child, even if it is inactive?</param>
        /// <returns>The child game object.</returns>
        public static GameObject GetChildGameObject(GameObject parent, string childName, bool includeInactive=false)
        {
            Transform[] children = parent.GetComponentsInChildren<Transform>(includeInactive);
            Transform child = null;
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].name == childName)
                {
                    child = children[i];
                    break;
                }
            }

            return (child != null) ? child.gameObject : null;
        }
        /// <summary>
        /// Traverse a child object's parents until its HeroKitObject is found.
        /// </summary>
        /// <param name="child">The game object that is the child of the hero kit object.</param>
        /// <returns>The hero kit object.</returns>
        public static HeroKitObject GetParentHeroKitObject(GameObject child)
        {
            HeroKitObject heroKitObject = null;
            Transform t = child.transform;
            while (t.parent != null)
            {
                heroKitObject = t.parent.GetComponent<HeroKitObject>();
                if (heroKitObject != null)
                {
                    return heroKitObject;
                }
                t = t.parent.transform;
            }

            // return null if no hero kit object found.
            return heroKitObject;
        }
        /// <summary>
        /// Get a component to a game object using the name of the mono script it came from.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that made the request.</param>
        /// <param name="targetObject">The hero kit object where the component should be placed.</param>
        /// <param name="scriptName">The name of the mono script.</param>
        /// <returns>The component on the game object.</returns>
        public static MonoBehaviour GetComponentFromScript(HeroKitObject heroKitObject, HeroKitObject targetObject, string scriptName)
        {
            MonoBehaviour component = (MonoBehaviour)targetObject.gameObject.GetComponent(System.Type.GetType(scriptName));

            if (component == null)
            {
                Debug.LogError(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, "A script with a class named " + scriptName + " does not exist. The name of the script must be the same name as the class in the script."));
            }

            return component;
        }
        /// <summary>
        /// Set a component to a game object using the name of the mono script it came from.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that made the request.</param>
        /// <param name="targetObject">The hero kit object where the component should be placed.</param>
        /// <param name="scriptName">The name of the mono script.</param>
        /// <returns>The component on the game object.</returns>
        public static MonoBehaviour AddComponentFromScript(HeroKitObject heroKitObject, HeroKitObject targetObject, string scriptName)
        {
            // check to see if component already exists.
            MonoBehaviour component = (MonoBehaviour)targetObject.gameObject.GetComponent(System.Type.GetType(scriptName));
            
            // only add component if it doesen't already exist.
            if (component == null)
                component = (MonoBehaviour)targetObject.gameObject.AddComponent(System.Type.GetType(scriptName));

            // debug message if we could not all component.
            if (component == null)
                Debug.LogError(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, "A script with a class named " + scriptName + " does not exist. The name of the script must be the same name as the class in the script."));

            return component;
        }
        /// <summary>
        /// (3D) Get the game object assigned to a hero kit object we've collided with.
        /// </summary>
        /// <param name="collider">The collider that was hit.</param>
        /// <returns>The game object assigned to a hero kit object.</returns>
        public static HeroKitObject GetCollisionGameObject(Collision collider)
        {
            HeroKitObject hko = null;

            // if collider has rigidbody, get game object assigned to hero block this way.
            // otherwise look for hero kit object component in parent 
            hko = (collider.rigidbody != null) ? collider.gameObject.GetComponent<HeroKitObject>() : collider.gameObject.GetComponentInParent<HeroKitObject>();

            return hko;
        }
        /// <summary>
        /// (2D) Get the game object assigned to a hero kit object we've collided with.
        /// </summary>
        /// <param name="collider">The collider that was hit.</param>
        /// <returns>The game object assigned to a hero kit object.</returns>
        public static HeroKitObject GetCollisionGameObject2D(Collision2D collider)
        {
            HeroKitObject hko = null;

            // if collider has rigidbody, get game object assigned to hero block this way.
            // otherwise look for hero kit object component in parent 
            hko = (collider.rigidbody != null) ? collider.gameObject.GetComponent<HeroKitObject>() : collider.gameObject.GetComponentInParent<HeroKitObject>();

            return hko;
        }
        /// <summary>
        /// (3D) Get the game object assigned to a hero kit object that contains a trigger area we've entered.
        /// </summary>
        /// <param name="collider">The trigger that was hit.</param>
        /// <returns>The game object assigned to a hero kit object.</returns>
        public static HeroKitObject GetTriggerGameObject(Collider collider)
        {
            HeroKitObject hko = null;

            // if collider has rigidbody, get game object assigned to hero block this way.
            // otherwise look for hero kit object component in parent 
            hko = (collider.attachedRigidbody != null) ? collider.attachedRigidbody.gameObject.GetComponent<HeroKitObject>() : collider.gameObject.GetComponentInParent<HeroKitObject>();

            return hko;
        }
        /// <summary>
        /// (2D) Get the game object assigned to a hero kit object that contains a trigger area we've entered.
        /// </summary>
        /// <param name="collider">The trigger that was hit.</param>
        /// <returns>The game object assigned to a hero kit object.</returns>
        public static HeroKitObject GetTriggerGameObject2D(Collider2D collider)
        {
            HeroKitObject hko = null;

            // if collider has rigidbody, get game object assigned to hero block this way.
            // otherwise look for hero kit object component in parent 
            hko = (collider.attachedRigidbody != null) ? collider.gameObject.GetComponent<HeroKitObject>() : collider.gameObject.GetComponentInParent<HeroKitObject>();

            return hko;
        }
    }
}


