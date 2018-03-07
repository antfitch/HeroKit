// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Move or turn a hero object.
    /// </summary>
    public class RpgMovement2D : MonoBehaviour
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
            if (thisIsTurn)
                TurnObjectUpdate();
            else if (thisIsJump)
                JumpObjectUpdate();
            else
                MoveObjectUpdate();
        }

        public bool thisIsJump = false;
        public bool thisIsTurn = false;
        public float jumpHeight = 0;
        int moveStep = 0;
        float totalMoveDuration;
        public Action Move;

        private void MoveObjectUpdate()
        {
            // get duration
            if (moveStep == 0)
            {
                settings.rpgMovementIsOn = true;
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
                    settings.ComputeVelocityRPG(speed);
                    settings.AnimateCharacterRPG();
                    settings.MoveCharacterRPG();
                }
            }

            // stop moving
            else if (moveStep == 2)
            {
                settings.ComputeVelocityRPG(new Vector2(0, 0));
                settings.AnimateCharacterRPG();
                settings.MoveCharacterRPG();
                if (settings.velocity == new Vector2(0, 0))
                {
                    moveStep = 0;
                    settings.rpgMovementIsOn = false;
                    enabled = false;
                }
            }
        }
        private void JumpObjectUpdate()
        {
            // get duration
            if (moveStep == 0)
            {
                settings.rpgMovementIsOn = true;
                settings.isJumping = true;
                Move();
                moveStep = 1;
            }

            // move the object
            else if (moveStep == 1)
            {
                settings.ComputeVelocityRPG(speed);
                settings.MoveCharacterRPG();
                Jumping();
            }

            // stop moving
            else if (moveStep == 2)
            {
                settings.ComputeVelocityRPG(new Vector2(0, 0));
                settings.MoveCharacterRPG();
                if (settings.velocity == new Vector2(0, 0))
                {
                    moveStep = 0;
                    settings.rpgMovementIsOn = false;
                    settings.isJumping = false;
                    enabled = false;
                }
            }
        }
        private void TurnObjectUpdate()
        {
            // step 1: set things up
            settings.rpgMovementIsOn = true;
            Move();

            // step 2: turn the object
            settings.ComputeVelocityRPG(speed);
            settings.AnimateCharacterRPG(true);

            // step 3: stop animating
            settings.ComputeVelocityRPG(new Vector2(0, 0));
            settings.AnimateCharacterRPG(true);
            settings.rpgMovementIsOn = false;

            thisIsTurn = false;
            enabled = false;
        }

        Vector2 speed = new Vector2();

        public void Jump()
        {
            settings.isJumping = true;
            thisIsJump = true;
            speed = new Vector2(0, jumpHeight);
        }
        private void Jumping()
        {
            float gravity = 2;
            speed.y *= gravity;        // Apply gravity to vertical velocity

            //Debug.Log(speed.y);
            //speed.y = speed.y / 2;
        }

        public void MoveLeft()
        {
            speed = new Vector2(-1, 0);
        }
        public void MoveRight()
        {
            speed = new Vector2(1, 0);
        }
        public void MoveUp()
        {
            speed = new Vector2(0, 1);
        }
        public void MoveDown()
        {
            speed = new Vector2(0, -1);
        }
        public void MoveLeftUp()
        {
            speed = new Vector2(-1, 1);
        }
        public void MoveLeftDown()
        {
            speed = new Vector2(-1, -1);
        }
        public void MoveRightUp()
        {
            speed = new Vector2(1, 1);
        }
        public void MoveRightDown()
        {
            speed = new Vector2(1, -1);
        }

        public HeroKitObject targetObject;
        public void MoveAwayFromObject()
        {
            // exit early if target object does not exist
            if (targetObject == null) return;

            // set direction
            speed.x = (transform.position.x > targetObject.transform.position.x) ? 1 : -1;
            speed.y = (transform.position.y > targetObject.transform.position.y) ? 1 : -1;

            // don't move diagnal if we are relativly parallel to target object
            StopDiagonalMovement();
        }
        public void MoveTowardObject()
        {
            // exit early if target object does not exist
            if (targetObject == null) return;

            // set direction
            speed.x = (transform.position.x > targetObject.transform.position.x) ? -1 : 1;
            speed.y = (transform.position.y > targetObject.transform.position.y) ? -1 : 1;

            // don't move diagnal if we are relativly parallel to target object
            StopDiagonalMovement();   
        }
        public void MoveOpposite()
        {
            switch (settings.faceDir)
            {
                case HeroSettings2D.FaceDir.none:
                    MoveUp();
                    break;
                case HeroSettings2D.FaceDir.left:
                    MoveRight();
                    break;
                case HeroSettings2D.FaceDir.right:
                    MoveLeft();
                    break;
                case HeroSettings2D.FaceDir.up:
                    MoveDown();
                    break;
                case HeroSettings2D.FaceDir.down:
                    MoveUp();
                    break;
                case HeroSettings2D.FaceDir.leftDown:
                    MoveRightUp();
                    break;
                case HeroSettings2D.FaceDir.rightDown:
                    MoveLeftUp();
                    break;
                case HeroSettings2D.FaceDir.leftUp:
                    MoveRightDown();
                    break;
                case HeroSettings2D.FaceDir.rightUp:
                    MoveLeftDown();
                    break;
            }
        }
        public void MoveForward()
        {
            switch (settings.faceDir)
            {
                case HeroSettings2D.FaceDir.none:
                    MoveDown();
                    break;
                case HeroSettings2D.FaceDir.left:
                    MoveLeft();
                    break;
                case HeroSettings2D.FaceDir.right:
                    MoveRight();
                    break;
                case HeroSettings2D.FaceDir.up:
                    MoveUp();
                    break;
                case HeroSettings2D.FaceDir.down:
                    MoveDown();
                    break;
                case HeroSettings2D.FaceDir.leftDown:
                    MoveLeftDown();
                    break;
                case HeroSettings2D.FaceDir.rightDown:
                    MoveRightDown();
                    break;
                case HeroSettings2D.FaceDir.leftUp:
                    MoveLeftUp();
                    break;
                case HeroSettings2D.FaceDir.rightUp:
                    MoveRightUp();
                    break;
            }
        }
        public void MoveRandom()
        {
            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);
            speed = new Vector2(x, y);

            // both x & y are 0, recalculate
            if (speed.x == 0 && speed.y == 0)
                MoveRandom();
        }

        public void StopDiagonalMovement()
        {            
            float yDist = Math.Abs(transform.position.y - targetObject.transform.position.y);
            float xDist = Math.Abs(transform.position.x - targetObject.transform.position.x);
            if (yDist <= .5)
                speed.y = 0;
            if (xDist <= .5)
                speed.x = 0;
        }
    }
}