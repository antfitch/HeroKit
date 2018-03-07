// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Play a particle effect at a specific position in the scene.
    /// </summary>
    public class PlayParticleInScene : IHeroKitAction
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
        public static PlayParticleInScene Create()
        {
            PlayParticleInScene action = new PlayParticleInScene();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get the particle to play
            UnityObjectField unityObject = UnityObjectFieldValue.GetValueA(heroKitObject, 0);
            ParticleSystem particlePrefab = (unityObject.value != null) ? (ParticleSystem)unityObject.value : null;
            bool runThis = (particlePrefab != null);

            if (runThis)
            {
                // create a pool for this particle if it doesn't exist
                string poolName = particlePrefab.transform.name + " particle effects";
                if (HeroKitDatabase.GetParticlePool(poolName, false) == null)
                {
                    HeroKitDatabase.AddParticlePool(poolName, particlePrefab);
                }

                // get the positon to use for the particle
                Vector3 position = particlePrefab.transform.localPosition;
                bool changePosition = BoolValue.GetValue(heroKitObject, 1);
                if (changePosition)
                {
                    position = CoordinatesValue.GetValue(heroKitObject, 2, 3, 4, 5, 6, 7, particlePrefab.transform.localPosition);
                }

                // get the rotation to use for the particle
                Quaternion rotation = particlePrefab.transform.localRotation;
                bool changeRotation = BoolValue.GetValue(heroKitObject, 8);
                if (changeRotation)
                {
                    Vector3 eulerAngles = CoordinatesValue.GetValue(heroKitObject, 9, 10, 11, 12, 13, 14, particlePrefab.transform.localEulerAngles);
                    rotation = Quaternion.Euler(eulerAngles);
                }

                // spawn the particle effect
                particleObject = HeroKitDatabase.SpawnParticle(poolName, position, rotation);

                // should next action wait until this action is complete?
                wait = BoolValue.GetValue(heroKitObject, 15);

                // play the particle effect
                particleObject.Stop();
                particleObject.Play();

                // set up the long action
                eventID = heroKitObject.heroStateData.eventBlock;
                heroKitObject.heroState.heroEvent[eventID].waiting = wait;
                updateIsDone = false;
                heroKitObject.longActions.Add(this);
            }

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Particle System: " + particleObject;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private ParticleSystem particleObject;
        private bool wait;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject, wait);
        }

        // update the action
        public void Update()
        {
            if (particleObject == null || !particleObject.isPlaying)
            {
                particleObject.gameObject.SetActive(false);
                updateIsDone = true;
            }
                
        }
    }
}