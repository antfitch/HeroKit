// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Fade an audio file in our out.
    /// </summary>
    public class HeroAudioFade : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The audio that we want to fade.
        /// </summary>
        public AudioSource audioSource;
        /// <summary>
        /// The duration of the fade.
        /// </summary>
        public float duration;
        /// <summary>
        /// The destination volume of the fade.
        /// </summary>
        public float targetVolume = 1f;
        /// <summary>
        /// Fade in volume = True. Fade out volume = False.
        /// </summary>
        public bool fadeIn;

        /// <summary>
        /// Strength of lerp between to values (0 to 1)
        /// </summary>
        private float t;
        /// <summary>
        /// The default volume of the audio.
        /// </summary>
        private float startVolume;

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Initialize this script.
        /// </summary>
        public void Initialize()
        {
            if (audioSource != null)
            {
                // reset time
                t = 0.0f;

                // unpause if paused
                audioSource.UnPause();
                startVolume = audioSource.volume;
                targetVolume = fadeIn ? 1f : 0f;

                // enables update methods for this class
                enabled = true;
            }
            else
            {
                enabled = false;
            }
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate()
        {
            if (fadeIn)
                FadeIn();
            else
                FadeOut();
        }

        /// <summary>
        /// Fade out the volume of the audio.
        /// </summary>
        private void FadeOut()
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t += Time.deltaTime / duration);

            // exit early. volume is not at 0 yet
            if (!(audioSource.volume <= 0)) return;

            audioSource.Pause();
            audioSource.volume = startVolume;
            enabled = false;
        }

        /// <summary>
        /// Fade in the volume of the audio.
        /// </summary>
        private void FadeIn()
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t += Time.deltaTime / duration);
            if (audioSource.volume >= 1f)
            {
                enabled = false;
            }
        }
    }
}