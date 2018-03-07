// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Work with a sound effect on a game object.
    /// </summary>
    public class HeroSoundEffect : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables (general)
        // --------------------------------------------------------------

        /// <summary>
        /// Audio clip to work with.
        /// </summary>
        public AudioClip audioClip; 
        /// <summary>
        /// Fade out background music before we play this audio clip.
        /// </summary>
        public bool fadeBGM;
        /// <summary>
        /// Fade out background sound before we play this audio clip.
        /// </summary>
        public bool fadeBGS;
        /// <summary>
        /// The audio source component for the sound effect.
        /// </summary>
        public AudioSource audioSourceSE;
        /// <summary>
        /// The audio source component for the background music.
        /// </summary>
        public AudioSource audioSourceBGM;
        /// <summary>
        /// The audio source component for the background sound.
        /// </summary>
        public AudioSource audioSourceBGS;

        // --------------------------------------------------------------
        // Variables (internal)
        // --------------------------------------------------------------

        /// <summary>
        /// Initial volume of the background music.
        /// </summary>
        private float startVolumeBGM;
        /// <summary>
        /// Initial volume of the background sound.
        /// </summary>
        private float startVolumeBGS;
        /// <summary>
        /// Length of time the sound effect will play.
        /// </summary>
        private float durationSE;
        /// <summary>
        /// The current volume of the background music each frame.
        /// </summary>
        private float currentVolumeBGM;
        /// <summary>
        /// the current volume of the background sound each frame.
        /// </summary>
        private float currentVolumeBGS;
        /// <summary>
        /// The duration to fade out background music and sound before sound effect is played.
        /// </summary>
        private float duration;
        /// <summary>
        /// Strength of lerp between two positions.
        /// </summary>
        private float t;
        /// <summary>
        /// Tracks the steps to play a sound effect. (0=fade out bgm/bgs, 1=play se, 2=fade in bgm/bgs) 
        /// </summary>
        private int step;

        // --------------------------------------------------------------
        // Methods 
        // --------------------------------------------------------------

        /// <summary>
        /// Initialize this script.
        /// </summary>
        public void Initialize()
        {
            startVolumeBGM = (audioSourceBGM != null) ? audioSourceBGM.volume : 0f;
            startVolumeBGS = (audioSourceBGS != null) ? audioSourceBGS.volume : 0f;
            currentVolumeBGM = 0;
            currentVolumeBGS = 0;
            duration = 0.5f;
            t = 0.0f;
            step = 0;

            // exit early if we are fading in or out
            if ((fadeBGM || audioSourceBGM != null) && (fadeBGS || audioSourceBGS != null)) return;

            // enable update methods for this class
            enabled = true;

            // if no fade in / fade out, play audio
            PlayAudioClip();

            // disable update methods for this class
            enabled = false;
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate()
        {
            // fade out BGM/BGS
            if (step == 0)
                FadeOutBackground();

            // play SE
            if (step == 1)
                PlaySoundEffect();

            // wait for SE to complete
            if (step == 2)
                WaitForSoundEffect();

            // fade in BGM/BGS
            if (step == 3)
                FadeInBackground();
        }

        /// <summary>
        /// Fade out the background music/sound before the sound effect plays.
        /// </summary>
        private void FadeOutBackground()
        {
            if (fadeBGM)
            {
                audioSourceBGM.volume = Mathf.Lerp(startVolumeBGM, 0, t += Time.deltaTime / duration);
                currentVolumeBGM = audioSourceBGM.volume;
            }

            if (fadeBGS)
            {
                audioSourceBGS.volume = Mathf.Lerp(startVolumeBGM, 0, t += Time.deltaTime / duration);
                currentVolumeBGS = audioSourceBGS.volume;
            }

            if (currentVolumeBGM <= 0 && currentVolumeBGS <= 0)
            {
                step = 1;
            }
        }

        /// <summary>
        /// Fade in the background music/sound after the sound effect plays.
        /// </summary>
        private void FadeInBackground()
        {
            if (fadeBGM)
            {
                audioSourceBGM.volume = Mathf.Lerp(0, startVolumeBGM, t += Time.deltaTime / duration);
                currentVolumeBGM = audioSourceBGM.volume;
            }

            if (fadeBGS)
            {
                audioSourceBGS.volume = Mathf.Lerp(0, startVolumeBGS, t += Time.deltaTime / duration);
                currentVolumeBGS = audioSourceBGS.volume;
            }

            if (currentVolumeBGM >= 1f && currentVolumeBGS >= 1f)
            {
                enabled = false;
            }
        }

        /// <summary>
        /// Play the sound effect after the background music/sound is done fading out.
        /// </summary>
        private void PlaySoundEffect()
        {
            PlayAudioClip();
            durationSE = audioClip.length + Time.time;
            step = 2;
        }

        /// <summary>
        /// Monitor current state of the sound effect to see if it is time to fade in the background music/sound.
        /// </summary>
        private void WaitForSoundEffect()
        {
            // exit early if there is still more time
            if (Time.time < durationSE) return;

            // go to next stage if time is up
            step = 3;
            t = 0f;
            if (!fadeBGM) currentVolumeBGM = 1f;
            if (!fadeBGS) currentVolumeBGS = 1f;
        }

        /// <summary>
        /// Play the sound effect.
        /// </summary>
        private void PlayAudioClip()
        {
            audioSourceSE.loop = false;
            audioSourceSE.playOnAwake = false;
            audioSourceSE.clip = audioClip;
            audioSourceSE.Play();
        }
    }
}