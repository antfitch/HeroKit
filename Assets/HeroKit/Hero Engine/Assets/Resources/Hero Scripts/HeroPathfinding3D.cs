// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEngine.AI;

namespace HeroKit.Scene.Scripts
{
    public class HeroPathfinding3D : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The NavMeshAgent for the current scene. This let's the game object know the routes it can take to other objects in the scene.
        /// </summary>
        public NavMeshAgent navMeshAgent;
        /// <summary>
        /// The hero object that this game object should move toward.
        /// </summary>
        public HeroKitObject targetObject;
        /// <summary>
        /// What you want game object to navigate towards. (1=move toward object, 2=move toward position)
        /// </summary>
        public int navigationType;
        /// <summary>
        /// Position in the scene to which the game object should navigate.
        /// </summary>
        public Vector3 destination;

        // --------------------------------------------------------------
        // Methods 
        // --------------------------------------------------------------

        /// <summary>
        /// Call this when the component is attached to the game object
        /// </summary>
        public void Awake()
        {
            enabled = false;
        }

        /// <summary>
        /// Initialize this script.
        /// </summary>
        public void Initialize()
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = true;
            }

            // enables update methods for this class
            enabled = true;
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate ()
        {
            if (navMeshAgent != null)
            {
                // navigate to object
                switch (navigationType)
                {
                    case 1:
                        NavigateToObject();
                        break;
                    case 2:
                        NavigateToPosition();
                        break;
                }
            }
            else
            {
                enabled = false;
            }
        }

        /// <summary>
        /// Move game object towards another object in the scene.
        /// </summary>
        private void NavigateToObject()
        {
            bool result = navMeshAgent.SetDestination(targetObject.transform.position);
            if (result == false)
            {
                Debug.LogError("Object not moved. Target position on nav mesh not found. Disabling Pathfinding.");
                enabled = false;
            }
        }

        /// <summary>
        /// Move game object to a specific position in the scene.
        /// </summary>
        private void NavigateToPosition()
        {
            navMeshAgent.SetDestination(destination);
        }

        /// <summary>
        /// Stop navigating to an object or position.
        /// </summary>
        public void StopNavigation()
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }

            enabled = false;
        }
    }
}