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
    /// Create a pool for objects in the scene.
    /// </summary>
    public class CreatePool : IHeroKitAction
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
        public static CreatePool Create()
        {
            CreatePool action = new CreatePool();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            string poolName = StringFieldValue.GetValueA(heroKitObject, 0);
            int itemCount = IntegerFieldValue.GetValueA(heroKitObject, 1);
            int objectType = DropDownListValue.GetValue(heroKitObject, 2);

            // ------------------------------------------------
            // Get the prefab that will populate the pool
            // ------------------------------------------------

            GameObject prefab = null;
            bool isHeroKitObject = false;

            // hero object
            if (objectType == 1)
            {
                prefab = Resources.Load<GameObject>("Hero Templates/Components/HeroKit Default Object");
                isHeroKitObject = true;
            }

            // prefab
            else if (objectType == 2)
            {
                prefab = PrefabValue.GetValue(heroKitObject, 3);
            }

            // ------------------------------------------------
            // Create the pool
            // ------------------------------------------------
            HeroKitDatabase.AddPool(poolName, prefab, itemCount, isHeroKitObject);

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Pool Name: " + poolName + "\n" +
                                      "Items to add: " + itemCount + "\n" +
                                      "Item: " + prefab;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            // normal return
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