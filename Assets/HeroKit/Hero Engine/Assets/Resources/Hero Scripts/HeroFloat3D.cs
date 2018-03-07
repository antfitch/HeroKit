// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Unique movements that can be applied to a hero object.
    /// </summary>
    public class HeroFloat3D : MonoBehaviour
    {
        public HeroSettings3D settings;

        /// <summary>
        /// The duration of the movement.
        /// </summary>
        public float duration;
        /// <summary>
        /// The speed of the movement.
        /// </summary>
        public float speed;

        private void Awake()
        {
            HeroKitObject targetObject = this.GetComponent<HeroKitObject>();
            settings = targetObject.GetHeroComponent<HeroSettings3D>("HeroSettings3D", true);
        }

        /// <summary>
        /// Initialize this script.
        /// </summary>
        public void Initialize()
        {
            settings.rigidBody.useGravity = false;
            enabled = true;
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate()
        {
            bool finished = settings.CustomMove(duration, speed, Vector3.up);
            if (finished)
                enabled = false;
        }
    }
}