// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System;
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// A player controller attached to a game object.
    /// Keys move the player.
    /// Direction the player is moving determines the direction the player is facing.
    /// </summary>
    public class RpgController2D : MonoBehaviour
    {
        public HeroSettings2D settings;

        void OnEnable()
        {
            HeroKitObject targetObject = this.GetComponent<HeroKitObject>();
            settings = targetObject.GetHeroComponent<HeroSettings2D>("HeroSettings2D", true);
            settings.rigidBody.gravityScale = 0;
        }

        // compute horizontal velocity, compute jump velocity, animate character
        void Update()
        {
            settings.ComputeVelocityRPG();
            settings.AnimateCharacterRPG();
        }

        // move character
        void FixedUpdate()
        {
            settings.MoveCharacterRPG();
        }
    }
}