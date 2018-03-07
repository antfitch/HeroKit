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
    /// Spawn an object in the scene.
    /// </summary>
    public class SpawnObject : IHeroKitAction
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
        public static SpawnObject Create()
        {
            SpawnObject action = new SpawnObject();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get the type of object to spawn
            int spawnType = DropDownListValue.GetValue(heroKitObject, 0);

            // where are we spawning item? from pool or in scene?
            bool usePool = BoolValue.GetValue(heroKitObject, 1);
            string poolName = (usePool) ? StringFieldValue.GetValueA(heroKitObject, 2) : "";

            // debug string
            string debugSpawn = "";

            // get the object to spawn (1=hero object, 2=prefab)
            if (spawnType == 1)
            {
                HeroObject heroSpawn = HeroObjectFieldValue.GetValueC(heroKitObject, 3);
                bool debugHeroSpawn = BoolValue.GetValue(heroKitObject, 4);
                bool dontSave = BoolValue.GetValue(heroKitObject, 5);
                Vector3 position = GetPosition();
                Quaternion rotation = GetRotation();
                HeroKitDatabase.SpawnHeroKitObject(usePool, poolName, position, rotation, heroSpawn, debugHeroSpawn, dontSave);
                if (heroKitObject.debugHeroObject)
                {
                    debugSpawn = "Hero Object: " + heroSpawn + "\n" +
                             "Debug: " + debugHeroSpawn + "\n" +
                             "Can Save: " + !dontSave + "\n" +
                             "Position: " + position + "\n" +
                             "Rotation: " + rotation + "\n" +
                             "Use Pool: " + usePool + "\n" +
                             "Pool Name: " + poolName;
                }
            }
            else if (spawnType == 2)
            {
                GameObject prefabSpawn = (!usePool) ? PrefabValue.GetValue(heroKitObject, 5) : null;
                Vector3 position = GetPosition();
                Quaternion rotation = GetRotation();
                HeroKitDatabase.SpawnPrefab(usePool, poolName, position, rotation, prefabSpawn);
                if (heroKitObject.debugHeroObject)
                {
                    debugSpawn = "Game Object: " + prefabSpawn + "\n" +
                             "Position: " + position + "\n" +
                             "Rotation: " + rotation + "\n" +
                             "Use Pool: " + usePool + "\n" +
                             "Pool Name: " + poolName;
                }
            }

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = debugSpawn;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        // get position to use
        private Vector3 GetPosition()
        {
            // get position to spawn
            bool changePosition = BoolValue.GetValue(heroKitObject, 6);
            Vector3 position = new Vector3();
            if (changePosition)
            {
                position = CoordinatesValue.GetValue(heroKitObject, 7, 8, 9, 10, 11, 12, new Vector3());
            }

            return position;
        }

        // get rotation to use
        private Quaternion GetRotation()
        {
            bool changeRotation = BoolValue.GetValue(heroKitObject, 13);
            Quaternion rotation = new Quaternion();
            if (changeRotation)
            {
                Vector3 eulerAngles = CoordinatesValue.GetValue(heroKitObject, 14, 15, 16, 17, 18, 19, new Vector3());
                rotation = Quaternion.Euler(eulerAngles);
            }

            return rotation;
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