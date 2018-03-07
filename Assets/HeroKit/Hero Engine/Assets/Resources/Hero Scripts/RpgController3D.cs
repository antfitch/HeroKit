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
    public class RpgController3D : MonoBehaviour
    {
        public HeroSettings3D settings;

        void OnEnable()
        {
            HeroKitObject targetObject = this.GetComponent<HeroKitObject>();
            settings = targetObject.GetHeroComponent<HeroSettings3D>("HeroSettings3D", true);
        }

        // compute horizontal velocity, compute jump velocity, animate character
        void Update()
        {
            settings.ComputeVelocity();
            settings.SetMoveDir();
            settings.SetFaceDir();
            settings.AnimateCharacter();
        }

        // move character
        void FixedUpdate()
        {
            settings.MoveCharacter(true);
            settings.SaveLastMove();
        }
    }
}