// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Make a hero object jump.
    /// </summary>
    public class HeroJump3D : MonoBehaviour
    {
        public HeroSettings3D settings;
        public float jumpHeight;
        private bool haveJumped;
        private int step;

        private void Awake()
        {
            HeroKitObject targetObject = this.GetComponent<HeroKitObject>();
            settings = targetObject.GetHeroComponent<HeroSettings3D>("HeroSettings3D", true);
        }
        public void Initialize()
        {
            // exit early if already in a jump
            if (haveJumped) return;

            step = 0;

            // enables update methods for this class
            enabled = true;
        }
        private void FixedUpdate()
        {
            JumpObjectUpdate();
        }
        private void JumpObjectUpdate()
        {
            // start the jump
            if (step == 0)
            {
                haveJumped = true;
                step = 1;
            }

            // jumping
            if (step == 1)
            {
                settings.ComputeJumpVelocity(jumpHeight);
                settings.MoveCharacter(true);
                if (!settings.startJump)
                    step = 2;
            }

            // finish the jump
            if (step == 2)
            {
                haveJumped = false;
                enabled = false;
            }
        }
    }
}