
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    public class PlatformerMovement2D : MonoBehaviour
    {
        public HeroSettings2D settings;

        private void Awake()
        {
            HeroKitObject targetObject = this.GetComponent<HeroKitObject>();
            settings = targetObject.GetHeroComponent<HeroSettings2D>("HeroSettings2D", true);
        }

        public void Initialize()
        {
            // enables update methods for this class
            enabled = true;
        }

        // move character
        void FixedUpdate()
        {
            MoveObjectUpdate();
        }

        int moveStep = 0;
        float totalMoveDuration;
        public Action Move;

        private void MoveObjectUpdate()
        {
            // get duration
            if (moveStep == 0)
            {
                Move();
                totalMoveDuration = Time.time + settings.moveDuration;
                moveStep = 1;
            }

            // move the object
            else if (moveStep == 1)
            {
                if (Time.time >= totalMoveDuration)
                {
                    moveStep = 2;
                }
                else
                {
                    // x = horiz movement, y = vertical movement
                    settings.ComputeVelocityP(speed);
                    settings.AnimateCharacterP();
                    settings.MoveCharacterP();
                }
            }

            // stop moving
            else if (moveStep == 2)
            {
                settings.ComputeVelocityP(new Vector2(0,0));
                settings.AnimateCharacterP();
                settings.MoveCharacterP();
                if (settings.velocity == new Vector2(0, 0))
                {
                    settings.AnimateCharacterStopP();
                    jumpHeight = 0;
                    moveStep = 0;
                    enabled = false;
                }
            }
        }

        Vector2 speed = new Vector2();
        public void MoveLeft()
        {
            speed = new Vector2(-1, jumpHeight);
        }

        public void MoveRight()
        {
            speed = new Vector2(1, jumpHeight);
        }

        public float jumpHeight = 0;
        public float directionForce = 0;
        public void Jump()
        {
            speed = new Vector2(directionForce, jumpHeight);        
        }
    }
}
