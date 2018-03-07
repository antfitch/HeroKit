// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using UnityEngine.AI;
using HeroKit.Scene.Scripts;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Use pathfinding to move an object toward a position in the scene.
    /// </summary>
    public class NavigateTowardPositionOn : IHeroKitAction
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
        public static NavigateTowardPositionOn Create()
        {
            NavigateTowardPositionOn action = new NavigateTowardPositionOn();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i]);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject)
        {
            Vector3 pos = CoordinatesValue.GetValue(heroKitObject, 2, 3, 4, 5, 6, 7, targetObject.transform.localPosition);

            // get nav mesh agent
            NavMeshAgent navMeshAgent = targetObject.GetHeroComponent<NavMeshAgent>("navMeshAgent");
            if (navMeshAgent == null)
            {
                navMeshAgent = targetObject.GetHeroComponent<NavMeshAgent>("navMeshAgent", true);
                navMeshAgent = HeroKitCommonRuntime.CreateNavMeshAgent(navMeshAgent);
            }

            // get the pathfinding script
            HeroPathfinding3D moveObject = targetObject.GetHeroComponent<HeroPathfinding3D>("HeroPathfinding3D", true);
            moveObject.navMeshAgent = navMeshAgent;
            moveObject.navigationType = 2;
            moveObject.destination = pos;
            moveObject.Initialize();
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