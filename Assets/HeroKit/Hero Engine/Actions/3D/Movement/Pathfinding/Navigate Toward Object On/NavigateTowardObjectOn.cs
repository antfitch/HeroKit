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
    /// Use pathfinding to move an object toward another object.
    /// </summary>
    public class NavigateTowardObjectOn : IHeroKitAction
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
        public static NavigateTowardObjectOn Create()
        {
            NavigateTowardObjectOn action = new NavigateTowardObjectOn();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            HeroKitObject sceneObject = HeroObjectFieldValue.GetValueA(heroKitObject, 2)[0];
            bool runThis = (targetObject != null && sceneObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], sceneObject);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Move Towards: " + sceneObject;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, HeroKitObject sceneObject)
        {
            // get nav mesh agent
            NavMeshAgent navMeshAgent = targetObject.GetHeroComponent<NavMeshAgent>("navMeshAgent", false, false, false);
            if (navMeshAgent == null)
            {
                navMeshAgent = targetObject.GetHeroComponent<NavMeshAgent>("navMeshAgent", true);
                navMeshAgent = HeroKitCommonRuntime.CreateNavMeshAgent(navMeshAgent);
            }

            // get the pathfinding script
            HeroPathfinding3D moveObject = targetObject.GetHeroComponent<HeroPathfinding3D>("HeroPathfinding3D", true);
            moveObject.navMeshAgent = navMeshAgent;
            moveObject.targetObject = sceneObject;
            moveObject.navigationType = 1;
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