// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Play a particle effect.
    /// </summary>
    public class HeroParticles : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The particle object to play.
        /// </summary>
        public ParticleSystem particleObject;

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Initialize this script and play the particle effect.
        /// </summary>
        public void Initialize()
        {
            // enables update methods for this class
            enabled = true;

            // stop any existing particle and start a new one
            particleObject.Stop();
            particleObject.Play();
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate()
        {
            if (!particleObject.isPlaying)
            {
                particleObject.gameObject.SetActive(false);
                enabled = false;
            }
        }
    }
}