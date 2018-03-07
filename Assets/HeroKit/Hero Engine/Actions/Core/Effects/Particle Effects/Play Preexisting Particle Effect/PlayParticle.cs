// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using HeroKit.Scene.Scripts;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Play a particle effect that already exists on an object in the scene.
    /// </summary>
    public class PlayParticle : IHeroKitAction
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
        public static PlayParticle Create()
        {
            PlayParticle action = new PlayParticle();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            bool useDifferentObject = BoolValue.GetValue(heroKitObject, 0);
            bool changePosition = BoolValue.GetValue(heroKitObject, 3);
            bool changeRotation = BoolValue.GetValue(heroKitObject, 10);
            wait = BoolValue.GetValue(heroKitObject, 17);
            ParticleSystem particleObject = null;

            // get the particle system
            if (useDifferentObject)
            {
                SceneObjectValueData objectData = SceneObjectValue.GetValue(heroKitObject, 1, 2, false);

                // object is hero object
                if (objectData.heroKitObject != null)
                {
                    // execute action for all objects in list
                    for (int i = 0; i < objectData.heroKitObject.Length; i++)
                        ExecuteOnHeroObject(objectData.heroKitObject[i], changePosition, changeRotation);
                }

                // object is game object
                else if (objectData.gameObject != null)
                {
                    // execute action for all objects in list
                    for (int i = 0; i < objectData.gameObject.Length; i++)
                        ExecuteOnGameObject(objectData.gameObject[i], changePosition, changeRotation);
                }
            }
            else
            {
                particleObject = heroKitObject.GetHeroComponent<ParticleSystem>("ParticleSystem");
                if (particleObject != null)
                    ExecuteOnTarget(particleObject, changePosition, changeRotation);
            }

            // set up the long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = wait;
            updateIsDone = false;
            heroKitObject.longActions.Add(this);

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Particle System: " + particleObject;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnHeroObject(HeroKitObject targetObject, bool changePosition, bool changeRotation)
        {
            ParticleSystem particleObject = targetObject.GetHeroComponent<ParticleSystem>("ParticleSystem");
            particles = targetObject.GetHeroComponent<HeroParticles>("HeroParticles", true);
            ExecuteOnTarget(particleObject, changePosition, changeRotation);
        }

        public void ExecuteOnGameObject(GameObject targetObject, bool changePosition, bool changeRotation)
        {
            String particleName = targetObject.name + " particlesystem";
            ParticleSystem particleSystem = heroKitObject.GetGameObjectComponent<ParticleSystem>(particleName, false, targetObject);
            particles = heroKitObject.GetGameObjectComponent<HeroParticles>("HeroParticles" + particleName, false, targetObject);
            ExecuteOnTarget(particleSystem, changePosition, changeRotation);
        }

        public void ExecuteOnTarget(ParticleSystem particleSystem, bool changePosition, bool changeRotation)
        {
            // get default rotation and positon
            Vector3 position = particleSystem.transform.localPosition;
            Quaternion rotation = particleSystem.transform.localRotation;

            // set the postion of the particle system           
            if (changePosition)
            {
                position = CoordinatesValue.GetValue(heroKitObject, 4, 5, 6, 7, 8, 9, particleSystem.transform.localPosition);
                particleSystem.transform.localPosition = position;
            }

            // set the rotation of the particle system           
            if (changeRotation)
            {
                Vector3 eulerAngles = CoordinatesValue.GetValue(heroKitObject, 11, 12, 13, 14, 15, 16, particleSystem.transform.localEulerAngles);
                rotation = Quaternion.Euler(eulerAngles);
                particleSystem.transform.localRotation = rotation;
            }

            // set the particle system on the script
            particles.particleObject = particleSystem;
            particleSystem.gameObject.SetActive(true);
            particles.Initialize();
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private HeroParticles particles;
        private bool wait;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject, wait);
        }

        // update the action
        public void Update()
        {
            if (particles == null || !particles.enabled)
                updateIsDone = true;
        }
    }
}