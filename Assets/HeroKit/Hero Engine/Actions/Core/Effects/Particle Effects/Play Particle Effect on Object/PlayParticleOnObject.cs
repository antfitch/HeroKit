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
    /// Play a particle effect on an object in the scene.
    /// </summary>
    public class PlayParticleOnObject : IHeroKitAction
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
        public static PlayParticleOnObject Create()
        {
            PlayParticleOnObject action = new PlayParticleOnObject();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get object to play particles on
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            UnityObjectField unityObject = UnityObjectFieldValue.GetValueA(heroKitObject, 2);
            ParticleSystem particlesPrefab = (unityObject.value != null) ? (ParticleSystem)unityObject.value : null;
            bool changePosition = BoolValue.GetValue(heroKitObject, 3);
            bool changeRotation = BoolValue.GetValue(heroKitObject, 10);
            wait = BoolValue.GetValue(heroKitObject, 17);
            bool runThis = (targetObject != null && particlesPrefab != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], particlesPrefab, changePosition, changeRotation);

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

        public void ExecuteOnTarget(HeroKitObject targetObject, ParticleSystem particlePrefab, bool changePosition, bool changeRotation)
        {
            // create a pool for this particle if it doesn't exist
            string poolName = particlePrefab.transform.name + " particle effects" + targetObject.transform.GetInstanceID();
            if (HeroKitDatabase.GetParticlePool(poolName, false) == null)
            {
                HeroKitDatabase.AddParticlePool(poolName, particlePrefab, 1, targetObject);
            }

            // change position of particle system
            Vector3 position = new Vector3();
            if (changePosition)
            {
                position = CoordinatesValue.GetValue(heroKitObject, 4, 5, 6, 7, 8, 9, new Vector3());
            }

            // change rotation
            Quaternion rotation = new Quaternion();
            if (changeRotation)
            {
                Vector3 eulerAngles = CoordinatesValue.GetValue(heroKitObject, 11, 12, 13, 14, 15, 16, new Vector3());
                rotation = Quaternion.Euler(eulerAngles);
            }

            // spawn the particle effect
            particleObject = HeroKitDatabase.SpawnParticle(poolName, position, rotation);

            // play the particle effect
            particleObject.Stop();
            particleObject.Play();
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private ParticleSystem particleObject;
        bool wait;

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