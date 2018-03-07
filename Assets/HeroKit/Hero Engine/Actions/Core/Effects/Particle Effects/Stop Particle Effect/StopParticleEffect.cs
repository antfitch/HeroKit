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
    /// Stop a particle effect.
    /// </summary>
    public class StopParticleEffect : IHeroKitAction
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
        public static StopParticleEffect Create()
        {
            StopParticleEffect action = new StopParticleEffect();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            HeroKitObject targetObject = heroKitObject;
            bool useDifferentObject = BoolValue.GetValue(heroKitObject, 0);

            // get the particle system
            ParticleSystem particleSystem = null;
            if (useDifferentObject)
            {
                SceneObjectValueData objectData = SceneObjectValue.GetValue(heroKitObject, 1, 2, false);

                // object is hero object
                if (objectData.heroKitObject != null)
                {
                    // execute action for all objects in list
                    for (int i = 0; i < objectData.heroKitObject.Length; i++)
                        ExecuteOnHeroObject(objectData.heroKitObject[i]);
                }

                // object is game object
                else if (objectData.gameObject != null)
                {
                    // execute action for all objects in list
                    for (int i = 0; i < objectData.gameObject.Length; i++)
                        ExecuteOnGameObject(objectData.gameObject[i]);
                }


                // object is hero object
                if (objectData.heroKitObject != null)
                {
                    particleSystem = objectData.heroKitObject[0].GetHeroComponent<ParticleSystem>("ParticleSystem");
                }

                // object is game object
                else if (objectData.gameObject != null)
                {
                    String particleName = objectData.gameObject[0].name + " particlesystem";
                    particleSystem = heroKitObject.GetGameObjectComponent<ParticleSystem>(particleName, false, objectData.gameObject[0]);
                }
            }
            else
            {
                particleSystem = heroKitObject.GetHeroComponent<ParticleSystem>("ParticleSystem");
            }

            // play the particle effect
            if (particleSystem != null)
            {
                particleSystem.Stop();
            }

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Particle System: " + particleSystem;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnHeroObject(HeroKitObject targetObject)
        {
            ParticleSystem particleSystem = targetObject.GetHeroComponent<ParticleSystem>("ParticleSystem");
            ExecuteOnTarget(particleSystem);
        }

        public void ExecuteOnGameObject(GameObject targetObject)
        {
            String particleName = targetObject.name + " particlesystem";
            ParticleSystem particleSystem = heroKitObject.GetGameObjectComponent<ParticleSystem>(particleName, false, targetObject);
            ExecuteOnTarget(particleSystem);
        }

        public void ExecuteOnTarget(ParticleSystem particleSystem)
        {
            // play the particle effect
            if (particleSystem != null)
            {
                particleSystem.Stop();
            }
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------

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