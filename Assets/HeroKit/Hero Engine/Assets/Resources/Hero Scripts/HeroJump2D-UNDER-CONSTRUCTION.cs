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
    public class HeroObjectJump2D : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables (Set by developer)
        // --------------------------------------------------------------

        /// <summary>
        /// The rigidbody attached to the game object. If it does not exist, one is added.
        /// </summary>
        public Rigidbody2D objectRigidbody;
        /// <summary>
        /// The animator attached to the game object.
        /// </summary>
        public Animator animator;
        /// <summary>
        /// The force of the jump.
        /// </summary>
        public float jumpForce;
        /// <summary>
        /// The animation to play when the game object begins its jump.
        /// </summary>
        public string animationJumpBegin = "";
        /// <summary>
        /// The animation to play when the game object ends its jump.
        /// </summary>
        public string animationJumpEnd = "";
        /// <summary>
        /// Use a layer mask to determine whether or not a game object is grounded. 
        /// </summary>
        public bool useLayerMask;
        /// <summary>
        /// The layers that the game object can stand on. If the game object is standing on one of these layers, it's grounded.
        /// </summary>
        public int layerMask;
        /// <summary>
        /// The game object must be standing on specified layer(s) to jump.
        /// </summary>
        public bool mustBeGrounded;

        // --------------------------------------------------------------
        // Variables (internal)
        // --------------------------------------------------------------

        /// <summary>
        /// The height of the jump.
        /// </summary>
        private readonly Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
        /// <summary>
        /// Tracks the steps in the jump. (0=start jump, 1=jumping, 2=end jump) 
        /// </summary>
        private int step;
        /// <summary>
        /// The game object has started the jump.
        /// </summary>
        public bool haveJumped;
        /// <summary>
        /// The game object is grounded.
        /// </summary>
        private bool isGrounded;

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Call this when the component is attached to the game object
        /// </summary>
        private void Awake()
        {
            objectRigidbody = gameObject.GetComponent<Rigidbody2D>();
            if (objectRigidbody == null)
                objectRigidbody = gameObject.AddComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Initialize this script.
        /// </summary>
        public void Initialize()
        {
            // exit early if already in a jump
            if (haveJumped && mustBeGrounded) return;

            haveJumped = false;
            step = 0;

            // enables update methods for this class
            enabled = true;
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate()
        {
            JumpObjectUpdate();
        }

        /// <summary>
        /// Manage the current status of the jump.
        /// </summary>
        private void JumpObjectUpdate()
        {
            Debug.Log(step);

            // start the jump
            if (step == 0)
            {
                // only go to next step if object is touching ground
                if (mustBeGrounded)
                    objectRigidbody.MovePosition(transform.localPosition);
                // go to next step if object doesn't need to touch ground
                else
                    step = 1;
            }

            // jumping
            if (step == 1)
            {
                Jump();
            }

            // finish the jump
            if (step == 2)
            {
                haveJumped = false;
                enabled = false;
            }
        }

        /// <summary>
        /// Called each frame as the game object jumps.
        /// </summary>
        public void Jump()
        {
            Debug.Log("is grounded=" + isGrounded);

            // make character jump
            if ((isGrounded && !haveJumped) || (!mustBeGrounded && !haveJumped))
            {
                Debug.Log("start jump");

                objectRigidbody.AddForce(jump * jumpForce * objectRigidbody.mass, ForceMode2D.Impulse);
                haveJumped = true;
                isGrounded = false;
                mustBeGrounded = true;
                PlayAnimation(animationJumpBegin);
            }

            // if character can't jump, exit this script.
            else if (!haveJumped)
            {
                haveJumped = true;
                step = 2;
            }
        }

        /// <summary>
        /// This monitors the start of a jump.
        /// </summary>
        /// <param name="collision">A collision with another item.</param>
        private void OnCollisionStay2D(Collision2D collision)
        {
            Debug.Log("on collision stay");
            if (!FoundCollisionBelow(collision)) return;
            if (step != 0) return;

            step = 1;
            isGrounded = true;
        }

        /// <summary>
        /// This monitors the end of a jump.
        /// </summary>
        /// <param name="collision">A collision with another item.</param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("on collision enter");

            if (!FoundCollisionBelow(collision)) return;
            if (step != 1 || !haveJumped) return;

            step = 2;
            isGrounded = true;
            PlayAnimation(animationJumpEnd);
        }

        /// <summary>
        /// Look for a collision below an object.
        /// </summary>
        /// <param name="collision">A collision with another item.</param>
        /// <returns></returns>
        private bool FoundCollisionBelow(Collision2D collision)
        {
            Debug.Log("on collision below");

            // exit early if there are no collisions
            if (collision.contacts.Length <= 0) return false;

            // get collisions
            ContactPoint2D contact = collision.contacts[0];

            // exit early if there are no collisions yet
            if (!(Vector3.Dot(contact.normal, Vector3.up) > 0.5)) return false;

            // check if there has been a collision
            bool foundCollision = !useLayerMask || FoundCollisionInLayer(collision);

            return foundCollision;
        }

        /// <summary>
        /// Look for a collision with a layer.
        /// </summary>
        /// <param name="collision">A collision with another item.</param>
        /// <returns></returns>
        private bool FoundCollisionInLayer(Collision2D collision)
        {
            bool foundCollision = (layerMask & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer;
            return foundCollision;
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
    }
}