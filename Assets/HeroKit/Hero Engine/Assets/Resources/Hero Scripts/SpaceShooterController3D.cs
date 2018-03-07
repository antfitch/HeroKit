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
    /// Mouse rotates the player.
    /// </summary>
    public class SpaceShooterController3D : MonoBehaviour
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
            settings.AnimateCharacter();
        }

        // move character
        void FixedUpdate()
        {
            settings.MoveCharacter(true);
            settings.RotateCharacterWithMouse();
        }

        //// --------------------------------------------------------------
        //// Variables (general)
        //// --------------------------------------------------------------

        ///// <summary>
        ///// The type of animation to use (1=animator or 2=animation).
        ///// </summary>
        //public int animationType;
        ///// <summary>
        ///// The animator attached to the player.
        ///// </summary>
        //public Animator animator;
        ///// <summary>
        ///// The animation component attached to the player.
        ///// </summary>
        //public Animation animator_legacy;
        ///// <summary>
        ///// The speed of the player's movement.
        ///// </summary>
        //public float speed = 8;
        ///// <summary>
        ///// The animation to use when the player walks.
        ///// </summary>
        //public string walkingAnimationName = "";
        ///// <summary>
        ///// The animation to use when the player is idle.
        ///// </summary>
        //public string idleAnimationName = "";

        //// --------------------------------------------------------------
        //// Variables (internal)
        //// --------------------------------------------------------------

        ///// <summary>
        ///// The player's rigidbody.
        ///// </summary>
        //private Rigidbody playerRigidBody;

        //// --------------------------------------------------------------
        //// Methods 
        //// --------------------------------------------------------------

        ///// <summary>
        ///// Call this when the component is attached to the game object
        ///// </summary>
        //private void Awake()
        //{
        //    playerRigidBody = gameObject.GetComponent<Rigidbody>();
        //    if (playerRigidBody == null)
        //        playerRigidBody = gameObject.AddComponent<Rigidbody>();
        //}

        ///// <summary>
        ///// Initialize this script.
        ///// </summary>
        //public void Initialize()
        //{
        //    // enables update methods for this class
        //    enabled = true;
        //}

        ///// <summary>
        ///// Disable this script.
        ///// </summary>
        //public void Disable()
        //{
        //    // disables update methods for this class
        //    enabled = false;
        //}

        ///// <summary>
        ///// Execute this method every frame.
        ///// </summary>
        //private void FixedUpdate()
        //{
        //    // check to see which input keys are being used
        //    float h = Input.GetAxis("Horizontal");
        //    float v = Input.GetAxis("Vertical");

        //    // Move the player
        //    Move(h, v);

        //    // animate the player
        //    Animate(h, v);

        //    // rotate the player
        //    Rotate();
        //}

        ///// <summary>
        ///// Move the player with the input keys.
        ///// </summary>
        ///// <param name="h">Horizontal movement direction.</param>
        ///// <param name="v">Vertical movement direction.</param>
        //private void Move(float h, float v)
        //{
        //    // exit early if there is no movement
        //    if (Math.Abs(h) < 0.1f && Math.Abs(v) < 0.1f) return;

        //    // Set the movement vector based on the axis input.
        //    Vector3 move = new Vector3(h, 0.0f, v);

        //    // Normalise the movement vector and make it proportional to the Speed per second.
        //    move = move.normalized * (speed * 0.01f) * Time.deltaTime;

        //    // Move the player to it's current position plus the movement.
        //    playerRigidBody.MovePosition(transform.position + move);
        //}

        ///// <summary>
        ///// Turn the player with the mouse.
        ///// </summary>
        //private void Rotate()
        //{
        //    Vector3 middleOfScreen = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        //    Vector3 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //    Vector3 target = mousePos - middleOfScreen;
        //    Vector3 playerToMouse = new Vector3(target.x, 0f, target.y);

        //    // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
        //    Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

        //    // Set the player's rotation to this new rotation.
        //    playerRigidBody.MoveRotation(newRotatation);
        //}

        ///// <summary>
        ///// Animate the player when it moves.
        ///// </summary>
        ///// <param name="h">Horizontal movement direction.</param>
        ///// <param name="v">Vertical movement direction.</param>
        //private void Animate(float h, float v)
        //{
        //    if (animationType <= 1)
        //    {
        //        // exit early if there is no animator or walking animation 
        //        if (animator == null || walkingAnimationName == "")
        //            return;

        //        // Create a boolean that is true if either of the input axes is non-zero.
        //        bool walking = (Math.Abs(h) > 0.1f || Math.Abs(v) > 0.1f);

        //        // Tell the animator whether or not the player is walking.
        //        animator.SetBool(walkingAnimationName, walking);
        //    }

        //    else if (animationType == 2)
        //    {
        //        // exit early if there is no animator or walking animation 
        //        if (animator_legacy == null || walkingAnimationName == "")
        //            return;

        //        // Create a boolean that is true if either of the input axes is non-zero.
        //        bool walking = (Math.Abs(h) > 0.1f || Math.Abs(v) > 0.1f);

        //        // Tell the animator whether or not the player is walking.
        //        if (walking)
        //            animator_legacy.CrossFade(walkingAnimationName, 0.1f);
        //        else
        //            animator_legacy.CrossFade(idleAnimationName, 0.1f);
        //    }
        //}
    }
}