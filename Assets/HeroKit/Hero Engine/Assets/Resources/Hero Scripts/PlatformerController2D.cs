// credits: Unity Labs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    public class PlatformController2D : MonoBehaviour
    {
        public HeroSettings2D settings;

        void OnEnable()
        {
            HeroKitObject targetObject = this.GetComponent<HeroKitObject>();
            settings = targetObject.GetHeroComponent<HeroSettings2D>("HeroSettings2D", true);
        }

        // compute horizontal velocity, compute jump velocity, animate character
        void Update()
        {
            settings.ComputeVelocityP();
            settings.AnimateCharacterP();
        }

        // move character
        void FixedUpdate()
        {
            settings.MoveCharacterP();
        }
    }
}
