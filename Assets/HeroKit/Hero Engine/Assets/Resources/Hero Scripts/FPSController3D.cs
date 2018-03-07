// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System;
using UnityEngine;

namespace HeroKit.Scene.Scripts
{
    public class FPSController3D : MonoBehaviour
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
            settings.MoveCharacter();
            settings.RotateCharacterWithCamera();
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
        ///// The mouse coordinates.
        ///// </summary>
        //private Vector2 mouseLook;
        ///// <summary>
        ///// Vertical smoothing, mouse.
        ///// </summary>
        //private Vector2 smoothV;
        ///// <summary>
        ///// Sensitivity of movement, mouse.
        ///// </summary>
        //public float sensitivity = 5f;
        ///// <summary>
        ///// Smoothness of movement.
        ///// </summary>
        //public float smoothing = 5f;

        //// --------------------------------------------------------------
        //// Methods 
        //// --------------------------------------------------------------

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

        //    // rotate the player and camera
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
        //    transform.Translate(move);
        //}

        ///// <summary>
        ///// Rotate the player and the camera using the mouse.
        ///// </summary>
        //private void Rotate()
        //{
        //    // Create a ray from the mouse cursor on screen in the direction of the camera.
        //    Transform camTransorm = Camera.main.transform;

        //    Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //    md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        //    smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        //    smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        //    mouseLook += smoothV;

        //    // clamp how high and low camera can look
        //    mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        //    // move camera up, down, left, right
        //    camTransorm.localEulerAngles = new Vector3(-mouseLook.y, mouseLook.x, 0);

        //    // target object = rotate left or right
        //    transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);
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