// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Unique movements that can be applied to a hero object.
    /// </summary>
    public class HeroSettings3D : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables (general)
        // --------------------------------------------------------------

        /// <summary>
        /// The game object that contains the visuals in the hero object.
        /// </summary>
        public GameObject visualsGO;
        /// <summary>
        /// The player's rigidbody.
        /// </summary>
        public Rigidbody rigidBody;

        /// <summary>
        /// Game object should face the direction in which it is moving.
        /// </summary>
        public bool faceDirection = true;
        /// <summary>
        /// Animate the game object when it moves.
        /// </summary>
        public bool animate = true;

        /// <summary>
        /// The type of animation to use (1=animator or 2=animation).
        /// </summary>
        public int animationType;
        /// <summary>
        /// The animator attached to the player.
        /// </summary>
        public Animator animator;
        /// <summary>
        /// The animation component attached to the player.
        /// </summary>
        public Animation animator_legacy;

        /// <summary>
        /// The duration of the move.
        /// </summary>
        public float moveDuration = 1;
        /// <summary>
        /// The duration of the turn.
        /// </summary>
        public float turnDuration;
        /// <summary>
        /// The speed of the player's movement.
        /// </summary>
        public float speed = 16;
        /// <summary>
        /// The animation to use when the player walks.
        /// </summary>
        public string moveDefault = "IsWalking";
        /// <summary>
        /// The animation to use when the player is idle. (animation component only)
        /// </summary>
        public string lookDefault = "IsLooking";

        /// <summary>
        /// Stop moving the game object if it collides with something.
        /// </summary>
        public bool finishMoveWhenCollide = true;
        /// <summary>
        /// Stop moving the game object if it collides with something in a specific layer. 
        /// </summary>
        public LayerMask layermaskCollide;

        // --------------------------------------------------------------
        // Variables (internal)
        // --------------------------------------------------------------

        /// <summary>
        /// The mouse coordinates.
        /// </summary>
        private Vector2 mouseLook;
        /// <summary>
        /// Vertical smoothing, mouse.
        /// </summary>
        private Vector2 smoothV;
        /// <summary>
        /// Sensitivity of movement, mouse.
        /// </summary>
        public float sensitivity = 5f;
        /// <summary>
        /// Smoothness of movement.
        /// </summary>
        public float smoothing = 5f;

        public Vector2 move = Vector2.zero;
        public float angle = 0f;
        public bool customMovementIsOn = false;

        // Use this for initialization
        void OnEnable()
        {
            HeroKitObject targetObject = this.GetComponent<HeroKitObject>();
            visualsGO = transform.Find(HeroKitCommonRuntime.visualsName).gameObject;
            animator = visualsGO.GetComponent<Animator>();
            animator_legacy = visualsGO.GetComponent<Animation>();
            rigidBody = GetComponent<Rigidbody>();
            grounded = CheckForCollisionOnDemand();
        }

        private void Update()
        {
            OnEnable(); //this a hack. for some reason visuals and animator are wiped out when onEnabled called. (state change?)
            enabled = false;
        }

        // ---------------------------------------------------------
        // common helper functions
        // ---------------------------------------------------------

        public void ComputeVelocity()
        {
            if (customMovementIsOn) return;

            // check to see which input keys are being used
            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");
        }
        public void ComputeVelocity(Vector2 speed, float yVelocity=0)
        {
            move.x = speed.x;
            move.y = speed.y;
            ComputeJumpVelocity(yVelocity);
        }
        public void MoveCharacter(bool moveRigidbody = false)
        {
            // exit early if there is no movement
            if (Math.Abs(move.x) < 0.1f && Math.Abs(move.y) < 0.1f && Math.Abs(jumpVelocity) < 0.1f) return;

            // Set the movement vector based on the axis input.
            Vector3 movement = new Vector3(move.x, jumpVelocity, move.y);

            // Normalise the movement vector and make it proportional to the Speed per second.
            movement = movement.normalized * (speed * 0.1f) * Time.deltaTime;

            // Move the player in the direction it is facing (rigidbody) or in the direction that the camera is facing
            if (moveRigidbody)
                rigidBody.MovePosition(rigidBody.transform.position + movement);
            else
                transform.Translate(movement);
        }
        public void AnimateCharacter()
        {
            if (animationType <= 1)
            {
                // exit early if there is no animator or walking animation 
                if (animator == null || moveDefault == "")
                    return;

                // Create a boolean that is true if either of the input axes is non-zero.
                bool walking = (Math.Abs(move.x) > 0.1f || Math.Abs(move.y) > 0.1f);

                // Tell the animator whether or not the player is walking.
                animator.SetBool(moveDefault, walking);
            }

            else if (animationType == 2)
            {
                // exit early if there is no animator or walking animation 
                if (animator_legacy == null || moveDefault == "")
                    return;

                // Create a boolean that is true if either of the input axes is non-zero.
                bool walking = (Math.Abs(move.x) > 0.1f || Math.Abs(move.y) > 0.1f);

                // Tell the animator whether or not the player is walking.
                if (walking)
                    animator_legacy.CrossFade(moveDefault, 0.1f);
                else
                    animator_legacy.CrossFade(lookDefault, 0.1f);
            }
        }

        // ---------------------------------------------------------
        // jump helper functions
        // ---------------------------------------------------------

        /// <summary>
        /// The default height of the jump.
        /// </summary>
        public readonly Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
        public bool startJump = false;
        public bool grounded = false;
        public float jumpHeight = 20;
        private float jumpVelocity = 0;
        public bool useLayerMask = false;

        /// <summary>
        /// The animation to play when the game object begins its jump.
        /// </summary>
        public string animationJumpBegin = "";
        /// <summary>
        /// The animation to play when the game object ends its jump.
        /// </summary>
        public string animationJumpEnd = "";

        public void ComputeJumpVelocity(float yVelocity)
        {
            // start jump
            if (yVelocity != 0 && grounded && !startJump)
            {
                StartJump(yVelocity);
            }
            // hit the ground
            else if (startJump && grounded)
            {
                EndJump();
            }
        }

        private void StartJump(float yVelocity)
        {
            jumpVelocity = yVelocity;
            rigidBody.AddForce(jump * jumpVelocity * rigidBody.mass, ForceMode.Impulse);           
            PlayAnimation(animationJumpBegin);
            grounded = false;
            startJump = true;
        }
        private void EndJump()
        {
            PlayAnimation(animationJumpEnd);
            jumpVelocity = 0f;
            startJump = false;
        }

        /// <summary>
        /// Play an animation.
        /// </summary>
        /// <param name="animationName">The name of the animation to play.</param>
        private void PlayAnimation(string animationName)
        {
            if (animator != null && animationName != "")
            {
                animator.SetTrigger(animationName);
            }
        }

        /// <summary>
        /// This monitors the end of a jump.
        /// </summary>
        /// <param name="collision">A collision with another item.</param>
        private void OnCollisionEnter(Collision collision)
        {
            grounded = FoundCollisionBelow(collision);
        }
        private bool FoundCollisionBelow(Collision collision)
        {
            // exit early if there are no collisions
            if (collision.contacts.Length <= 0) return false;

            // get collisions
            ContactPoint contact = collision.contacts[0];

            // exit early if there are no collisions yet
            if (!(Vector3.Dot(contact.normal, Vector3.up) > 0.5)) return false;

            // check if there has been a collision
            bool foundCollision = !useLayerMask || FoundCollisionInLayer(collision);

            return foundCollision;
        }
        private bool FoundCollisionInLayer(Collision collision)
        {
            bool foundCollision = (layermaskCollide & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer;
            return foundCollision;
        }

        public bool CheckForCollisionOnDemand()
        {
            // note: if pos is not adjusted by 0.01f, the object A does not recognize that
            // object B is below it. 

            Vector3 dir = transform.TransformDirection(Vector3.down);
            float dist = 0.1f;
            Vector3 pos = transform.position + new Vector3(0, 0.01f, 0);
            //Debug.DrawRay(transform.position, dir * dist, Color.green);

            if (useLayerMask)
                return Physics.Raycast(pos, dir, dist, layermaskCollide);
            else
                return Physics.Raycast(pos, dir, dist);
        }

        // ---------------------------------------------------------
        // rotation helper functions
        // ---------------------------------------------------------

        /// <summary>
        /// Turn the player with the camera.
        /// </summary>
        public void RotateCharacterWithCamera()
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Transform camTransorm = Camera.main.transform;

            Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            mouseLook += smoothV;

            // clamp how high and low camera can look
            mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

            // move camera up, down, left, right
            camTransorm.localEulerAngles = new Vector3(-mouseLook.y, mouseLook.x, 0);

            // target object = rotate left or right
            transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);
        }
        /// <summary>
        /// Turn the player with the mouse.
        /// </summary>
        public void RotateCharacterWithMouse()
        {
            Vector3 middleOfScreen = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
            Vector3 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 target = mousePos - middleOfScreen;
            Vector3 playerToMouse = new Vector3(target.x, 0f, target.y);

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            rigidBody.MoveRotation(newRotatation);
        }
        /// <summary>
        /// Move the game object.
        /// </summary>
        public bool moveObject;
        /// <summary>
        /// Turn the game object.
        /// </summary>
        public bool turnObject;

        // ---------------------------------------------------------
        // hero move 3d helper functions
        // ---------------------------------------------------------

        /// <summary>
        /// The rotation of the game object before it turns.
        /// </summary>
        public Quaternion turnFrom;
        /// <summary>
        /// The destination rotation of the game object.
        /// </summary>
        private Quaternion turnTo;
        /// <summary>
        /// The rotation of the game object in the previous frame.
        /// </summary>
        private Quaternion oldTurnRotation;
        /// <summary>
        /// The time that has passed since the game object started the turn.
        /// </summary>
        public float currentTurnTime;
        /// <summary>
        /// The game object is turning.
        /// </summary>
        public bool turning;
        /// <summary>
        /// The game object has turned for a move.
        /// </summary>
        public bool hasTurnedForMove;

        /// <summary>
        /// Turn the game object.
        /// </summary>
        /// <param name="x">X euler angle.</param>
        /// <param name="y">Y euler angle.</param>
        /// <param name="z">Z euler angle.</param>
        public void TurnObject()
        {
            if (faceDirection)
            {
                // turn immediately (used with move action)
                if (Math.Abs(turnDuration) < 0.1f)
                {
                    if (!hasTurnedForMove)
                    {
                        transform.eulerAngles = new Vector3(0, angle, 0);
                        hasTurnedForMove = true;
                    }
                }
                // turn slowly (used with turn action) 
                else
                {
                    turnTo = Quaternion.Euler(0, angle, 0);
                    currentTurnTime += Time.deltaTime;
                    transform.rotation = Quaternion.Slerp(turnFrom, turnTo, currentTurnTime * turnDuration * 0.1f);

                    // exit if we are at destination
                    if (oldTurnRotation == transform.rotation)
                    {
                        turning = false;
                    }

                    // record current position
                    oldTurnRotation = transform.rotation;
                }
            }
        }

        /// <summary>
        /// Use the movement animation on the game object.
        /// </summary>
        public void StartAnimating()
        {
            if (animate && animator != null && moveDefault != "")
            {
                animator.SetBool(moveDefault, true);
            }
        }
        /// <summary>
        /// Stop the movement animation the game object and return to whatever the game object was using before.
        /// </summary>
        public void StopAnimating()
        {
            if (animate && animator != null && moveDefault != "")
            {
                animator.SetBool(moveDefault, false);
            }
        }

        /// <summary>
        /// Finish move early if the game object collides with something.
        /// </summary>
        /// <param name="collision">A collision with another item.</param>
        public bool FinishCollisionMoveEarly(Collision collision)
        {
            bool result = false;

            // exit early if this component is disabled or we don't want to check for collisions
            if (!enabled || !finishMoveWhenCollide) return result;

            // look for collisions on specified layers only
            if ((layermaskCollide & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            {
                // exit movement if we should stop movement upon colliding with another object
                if (moveObject) result = true;
            }

            return result;
        }

        // ---------------------------------------------------------
        // Third-Person helper functions
        // ---------------------------------------------------------

        private Vector2 lastMove;       
        public enum FaceDir { none, left, right, up, down, leftDown, leftUp, rightDown, rightUp };
        public FaceDir faceDir = FaceDir.none;

        public void SetMoveDir()
        {
            move.x = (move.x < -0.01f) ? -1 : move.x;
            move.x = (move.x > 0.01f) ? 1 : move.x;
            move.y = (move.y < -0.01f) ? -1 : move.y;
            move.y = (move.y > 0.01f) ? 1 : move.y;
        }
        public void SetFaceDir()
        {
            // exit early if there is no movement
            if (move.x == 0 && move.y == 0) return;

            // exit early if we are moving in the same direction that we were last frame
            if (lastMove.x == move.x && lastMove.y == move.y) return;

            // get direction to face
            if (move.x == -1 && move.y == 0) faceDir = FaceDir.left;
            else if (move.x == 1 && move.y == 0) faceDir = FaceDir.right;
            else if (move.x == 0 && move.y == 1) faceDir = FaceDir.up;
            else if (move.x == 0 && move.y == -1) faceDir = FaceDir.down;
            else if (move.x == -1 && move.y == -1) faceDir = FaceDir.leftDown;
            else if (move.x == -1 && move.y == 1) faceDir = FaceDir.leftUp;
            else if (move.x == 1 && move.y == 1) faceDir = FaceDir.rightUp;
            else if (move.x == 1 && move.y == -1) faceDir = FaceDir.rightDown;

            // turn the character (this must only happen once. otherwise character will freeze if this loops)
            TurnCharacter();
        }
        public void TurnCharacter()
        {
            Vector3 turnTo = new Vector3();

            // set turn
            switch (faceDir)
            {
                case FaceDir.left:
                    turnTo = new Vector3(0f, -90f, 0f);
                    break;
                case FaceDir.right:
                    turnTo = new Vector3(0f, 90f, 0f);
                    break;
                case FaceDir.up:
                    turnTo = new Vector3(0f, 0f, 0f);
                    break;
                case FaceDir.down:
                    turnTo = new Vector3(0f, -180f, 0f);
                    break;
                case FaceDir.leftUp:
                    turnTo = new Vector3(0f, -45f, 0f);
                    break;
                case FaceDir.rightUp:
                    turnTo = new Vector3(0f, 45f, 0f);
                    break;
                case FaceDir.leftDown:
                    turnTo = new Vector3(0f, -135f, 0f);
                    break;
                case FaceDir.rightDown:
                    turnTo = new Vector3(0f, 135f, 0f);
                    break;
            }

            transform.eulerAngles = turnTo;
        }
        public void SaveLastMove()
        {
            lastMove = move;
        }

        // ---------------------------------------------------------
        // special moves helper functions
        // ---------------------------------------------------------

        /// <summary>
        /// The current time.
        /// </summary>
        private float currentTime;
        /// <summary>
        /// The time left for the move.
        /// </summary>
        private float timeLeft;

        /// <summary>
        /// Game object moves in custom direction.
        /// </summary>
        public bool CustomMove(float duration, float moveSpeed, Vector3 direction)
        {
            currentTime = Time.time;
            timeLeft = duration - currentTime;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // exit early if we still have time left
            return !(timeLeft > 0);
        }
    }
}