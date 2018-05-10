// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// Unique movements that can be applied to a hero object.
    /// </summary>
    public class HeroSettings2D : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables (general)
        // --------------------------------------------------------------

        /// <summary>
        /// The rigidbody attached to the game object. If it does not exist, one is added.
        /// </summary>
        public Rigidbody2D rigidBody;
        /// <summary>
        /// The animator attached to the game object.
        /// </summary>
        public Animator animator;
        /// <summary>
        /// The game object that contains the visuals in the hero object.
        /// </summary>
        public GameObject visualsGO;
        /// <summary>
        /// The sprite renderer attached to the hero object.
        /// </summary>
        public SpriteRenderer spriteRenderer;

        // --------------------------------------------------------------
        // Variables (settings)
        // --------------------------------------------------------------

        /// <summary>
        /// Stop moving the game object if it collides with something.
        /// </summary>
        public bool finishMoveWhenCollide = true;
        /// <summary>
        /// Stop moving the game object if it collides with something in a specific layer. 
        /// </summary>
        public LayerMask collisionLayers;
        /// <summary>
        /// Game object should face the direction in which it is moving.
        /// </summary>
        public bool faceDirectionOnMove = true;
        /// <summary>
        /// Animate the game object when it moves.
        /// </summary>
        public bool animateOnMove = true;
        /// <summary>
        /// The speed of the move.
        /// </summary>
        public float moveSpeed = 10;
        /// <summary>
        /// The height of the jump.
        /// </summary>
        public float jumpHeight = 20;

        public float moveDuration = 0.25f;
        public ContactFilter2D contactFilter;
        public bool useJumpLayermask;
        public LayerMask jumpLayermask;


        // --------------------------------------------------------------
        // Variables (2D specific - animation bools and triggers)
        // --------------------------------------------------------------

        public enum FaceDir { none, left, right, up, down, leftDown, leftUp, rightDown, rightUp };
        public FaceDir faceDir = FaceDir.none;
        public FaceDir lastFaceDir = FaceDir.none;

        public string lookDefault = "Look Default";
        public string lookLeft = "Look Left";
        public string lookRight = "Look Right";
        public string lookUp = "Look Up";
        public string lookDown = "Look Down";
        public string lookUpLeft = "Look Up Left";
        public string lookUpRight = "Look Up Right";
        public string lookDownLeft = "Look Down Left";
        public string lookDownRight = "Look Down Right";

        public string moveDefault = "Move Default";
        public string moveLeft = "Move Left";
        public string moveRight = "Move Right";
        public string moveUp = "Move Up";
        public string moveDown = "Move Down";
        public string moveUpLeft = "Move Up Left";
        public string moveUpRight = "Move Up Right";
        public string moveDownLeft = "Move Down Left";
        public string moveDownRight = "Move Down Right";

        public string jumpBegin = "Jump Begin";
        public string jumping = "Jumping";
        public string jumpEnd = "Jump End";

        // Use this for initialization
        void OnEnable()
        {
            HeroKitObject targetObject = this.GetComponent<HeroKitObject>();
            animator = targetObject.GetHeroChildComponent<Animator>("Animator", HeroKitCommonRuntime.visualsName);
            rigidBody = GetComponent<Rigidbody2D>();
            spriteRenderer = targetObject.GetHeroChildComponent<SpriteRenderer>("SpriteRenderer", HeroKitCommonRuntime.visualsName);
            visualsGO = transform.Find(HeroKitCommonRuntime.visualsName).gameObject;

            collisionLayers = Physics2D.GetLayerCollisionMask(gameObject.layer);
            contactFilter.useTriggers = false;
            contactFilter.SetLayerMask(collisionLayers);
            contactFilter.useLayerMask = true;

            enabled = false;
        }

        // ---------------------------------------
        // 2D platform game movement variables
        // ---------------------------------------
        public float minGroundNormalY = .65f;
        public float gravityModifier = 1f;
        protected Vector2 groundNormal;
        protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
        protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
        protected float minMoveDistance = 0.001f;
        protected float shellRadius = 0.01f;
        public string groundedParam = "grounded";
        public string xVelocityParam = "velocityX";
        public string jumpParam = "jump";
        public Vector2 move = Vector2.zero;
        protected Vector2 targetVelocity;
        protected bool grounded;
        public Vector2 velocity;

        // ---------------------------------------------------------
        // Platformer helper functions
        // ---------------------------------------------------------

        public void ComputeVelocityP()
        {
            targetVelocity = Vector2.zero;
            move = Vector2.zero;
            move.x = Input.GetAxis("Horizontal");
            ComputeJumpVelocityP();
            targetVelocity = move * moveSpeed;
        }
        public void ComputeVelocityP(Vector2 speed)
        {
            targetVelocity = Vector2.zero;
            move = Vector2.zero;
            move.x = speed.x;
            ComputeJumpVelocityP(speed.y);
            targetVelocity = move * moveSpeed;
        }

        public bool jumped = false;
        private void ComputeJumpVelocityP()
        {
            // start jump
            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpHeight;
                AnimateJumpStartP();
                jumped = true;             
            }
            // slow down jump
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }
            // stop jump
            else if (jumped && velocity.y < 0.1)
            {
                AnimateJumpStopP();
                jumped = false;
            }
        }       
        private void ComputeJumpVelocityP(float yVelocity)
        {
            if (yVelocity != 0 && grounded && !jumped)
            {
                velocity.y = yVelocity * 10;
                AnimateJumpStartP();
                jumped = true;
            }
            else if (jumped && velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }

            if (jumped && velocity.y < 0.1)
            {
                AnimateJumpStopP();
                jumped = false;
            }
        }

        public void AnimateCharacterP()
        {
            TurnCharacterP(move.x);
            animator.SetBool(groundedParam, grounded);
            animator.SetFloat(xVelocityParam, Mathf.Abs(velocity.x) / moveSpeed);
        }
        public void TurnCharacterP(float direction)
        {
            if (direction < 0)
                faceDir = FaceDir.left;
            else if (direction > 0)
                faceDir = FaceDir.right;

            bool flipSprite = (spriteRenderer.flipX ? (direction > 0) : (direction < 0));
            if (flipSprite)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
        public void AnimateCharacterStopP()
        {
            animator.SetBool(groundedParam, grounded);
            animator.SetFloat(xVelocityParam, 0);
        }
        public void AnimateJumpStartP()
        {
            animator.SetBool(jumpParam, true);
        }
        public void AnimateJumpStopP()
        {
            animator.SetBool(jumpParam, false);
        }

        public void MoveCharacterP()
        {
            velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
            velocity.x = targetVelocity.x;

            grounded = false;

            Vector2 deltaPosition = velocity * Time.deltaTime;

            Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

            Vector2 move = moveAlongGround * deltaPosition.x;

            AddMovementP(move, false);

            move = Vector2.up * deltaPosition.y;

            AddMovementP(move, true);
        }
        private void AddMovementP(Vector2 move, bool yMovement)
        {
            float distance = move.magnitude;

            if (distance > minMoveDistance)
            {
                int count = rigidBody.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
                hitBufferList.Clear();
                for (int i = 0; i < count; i++)
                {
                    hitBufferList.Add(hitBuffer[i]);
                }

                for (int i = 0; i < hitBufferList.Count; i++)
                {
                    Vector2 currentNormal = hitBufferList[i].normal;
                    if (currentNormal.y > minGroundNormalY)
                    {
                        grounded = true;
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot(velocity, currentNormal);
                    if (projection < 0)
                    {
                        velocity = velocity - projection * currentNormal;
                    }

                    float modifiedDistance = hitBufferList[i].distance - shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }

            rigidBody.position = rigidBody.position + move.normalized * distance;
        }

        // ---------------------------------------------------------
        // Top-down RPG game movement variables
        // ---------------------------------------------------------

        /// <summary>
        /// Which animations can this sprite use?
        /// One Way = Sprite has one animation that will be flipped when sprite changes direction.
        /// Two Way = Sprite has a left and right animation.
        /// Four Way = Sprite has a left, right, top, down animation.
        /// Eight Way = Sprite has a left, right, top, down, top left, top right, down left, down right animation.
        /// </summary>
        public enum AnimType { fourWay, eightWay };
        public AnimType animType = AnimType.eightWay;
        /// <summary>
        /// Which ways can this sprite move?
        /// One Way = Sprite has one animation that will be flipped when sprite changes direction.
        /// Two Way = Sprite has a left and right animation.
        /// Four Way = Sprite has a left, right, top, down animation.
        /// Eight Way = Sprite has a left, right, top, down, top left, top right, down left, down right animation.
        /// </summary>
        public enum MoveType { fourWay, eightWay };
        public MoveType moveType = MoveType.eightWay;

        public string animationName = "Move Default";
        public string lastAnimationName = "";

        // ---------------------------------------------------------
        // Top-down RPG helper functions
        // ---------------------------------------------------------

        Vector2 lastMove;
        public bool rpgMovementIsOn = false;
        public bool isJumping = false;
        public void ComputeVelocityRPG()
        {
            // exit early if movement from RpgMovement2D is being used
            if (rpgMovementIsOn) return;

            // check to see which input keys are being used
            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");

            // get the direction to move in (4 way or 8 way)
            SetMoveDir();
        }
        public void ComputeVelocityRPG(Vector2 speed)
        {
            // check to see which input keys are being used
            move.x = speed.x;
            move.y = speed.y;

            // get the direction to move in (4 way or 8 way)
            SetMoveDir();

            //ComputeJumpVelocityRPG(speed.y);
        }
        private void ComputeJumpVelocityRPG(float yVelocity)
        {
            if (yVelocity != 0 && grounded && !jumped)
            {
                velocity.y = yVelocity * 10;
                //AnimateJumpStartP();
                jumped = true;
            }
            else if (jumped && velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }

            if (jumped && velocity.y < 0.1)
            {
                //AnimateJumpStopP();
                jumped = false;
            }
        }

        private void SetMoveDir()
        {
            // get move direction (8 direction movement)
            move.x = (move.x < -0.01f) ? -1 : move.x;
            move.x = (move.x > 0.01f) ? 1 : move.x;
            move.y = (move.y < -0.01f) ? -1 : move.y;
            move.y = (move.y > 0.01f) ? 1 : move.y;

            // get move direction (4 direction movement)
            if (moveType == MoveType.fourWay)
            {
                if (move.x != 0 && move.y != 0)
                {
                    if (lastMove.x != 0) move.y = 0;
                    else if (lastMove.y != 0) move.x = 0;
                }
            }
        }

        public void MoveCharacterRPG()
        {
            AddMovementRPG(move.x, move.y);
        }
        private void AddMovementRPG(float x, float y)
        {
            // exit early if there is no movement
            if (x == 0 && y == 0) return;

            // Set the movement vector based on the axis input.
            Vector3 move = new Vector3(x, y, 0.0f);

            // Normalise the movement vector and make it proportional to the Speed per second.
            move = move.normalized *  moveSpeed * Time.deltaTime;

            // Move the player to it's current position plus the movement.
            rigidBody.MovePosition(transform.position + move);

            // save the last move
            lastMove = move;
        }

        public void AnimateCharacterRPG(bool thisIsTurn=false)
        {
            // exit early if character is jumping
            if (isJumping) return;

            // is the character moving? if no, turn on last movement animation and exit early
            bool moving = !(move.x == 0 && move.y == 0);
            if (!moving)
            {
                if (lastAnimationName != "")
                    animator.SetBool(lastAnimationName, false);
                return;
            }           

            // get animation direction
            SetAnimDir(thisIsTurn);

            // turn off last movement
            if (lastAnimationName != animationName && lastAnimationName != "")
                animator.SetBool(lastAnimationName, false);

            // turn on movement if it is a new movement
            bool result = animator.GetBool(animationName);
            if (result == false)
                animator.SetBool(animationName, true);

            lastFaceDir = faceDir;
            lastAnimationName = animationName;
        }
        private void SetAnimDir(bool thisIsTurn)
        {
            // exit early if there is no movement
            if (move.x == 0 && move.y == 0) return;

            // exit early if we are moving in the same direction that we were last frame
            if (lastMove.x == move.x && lastMove.y == move.y) return;

            // get animation direction (8 direction movement)
            if (move.x == -1 && move.y == 0) faceDir = FaceDir.left;
            else if (move.x == 1 && move.y == 0) faceDir = FaceDir.right;
            else if (move.x == 0 && move.y == 1) faceDir = FaceDir.up;
            else if (move.x == 0 && move.y == -1) faceDir = FaceDir.down;
            else if (move.x == -1 && move.y == -1) faceDir = FaceDir.leftDown;
            else if (move.x == -1 && move.y == 1) faceDir = FaceDir.leftUp;
            else if (move.x == 1 && move.y == 1) faceDir = FaceDir.rightUp;
            else if (move.x == 1 && move.y == -1) faceDir = FaceDir.rightDown;

            // get move direction (4 direction movement)
            if (animType == AnimType.fourWay)
            {
                switch (faceDir)
                {
                    case FaceDir.leftUp: // face upper left
                        if (lastFaceDir == FaceDir.left) faceDir = FaceDir.left;
                        else if (lastFaceDir == FaceDir.up) faceDir = FaceDir.up;
                        else faceDir = FaceDir.left;
                        break;
                    case FaceDir.rightUp: // face upper right
                        if (lastFaceDir == FaceDir.right) faceDir = FaceDir.right;
                        else if (lastFaceDir == FaceDir.up) faceDir = FaceDir.up;
                        else faceDir = FaceDir.right;
                        break;
                    case FaceDir.leftDown: // face lower left
                        if (lastFaceDir == FaceDir.left) faceDir = FaceDir.left;
                        else if (lastFaceDir == FaceDir.down) faceDir = FaceDir.down;
                        else faceDir = FaceDir.left;
                        break;
                    case FaceDir.rightDown: // face lower right
                        if (lastFaceDir == FaceDir.right) faceDir = FaceDir.right;
                        else if (lastFaceDir == FaceDir.down) faceDir = FaceDir.down;
                        else faceDir = FaceDir.right;
                        break;
                }
            }

            // set animation
            switch (faceDir)
            {
                case FaceDir.left:
                    animationName = (thisIsTurn) ? lookLeft : moveLeft;
                    break;
                case FaceDir.right:
                    animationName = (thisIsTurn) ? lookRight : moveRight;
                    break;
                case FaceDir.up:
                    animationName = (thisIsTurn) ? lookUp : moveUp;
                    break;
                case FaceDir.down:
                    animationName = (thisIsTurn) ? lookDown : moveDown;
                    break;
                case FaceDir.leftUp:
                    animationName = (thisIsTurn) ? lookUpLeft : moveUpLeft;
                    break;
                case FaceDir.rightUp:
                    animationName = (thisIsTurn) ? lookUpRight : moveUpRight;
                    break;
                case FaceDir.leftDown:
                    animationName = (thisIsTurn) ? lookDownLeft : moveDownLeft;
                    break;
                case FaceDir.rightDown:
                    animationName = (thisIsTurn) ? lookDownRight : moveDownRight;
                    break;
            }
        }

        /// <summary>
        /// Check to see if a parameter exists in an animator.
        /// </summary>
        /// <param name="paramName">Name of the parameter for which to look.</param>
        /// <param name="animator">The animator to search.</param>
        /// <returns>Was the parameter found?</returns>
        private bool AnimatorHasParameter(string paramName)
        {
            foreach (AnimatorControllerParameter param in animator.parameters)
            {
                if (param.name == paramName)
                    return true;
            }
            return false;
        }
    }
}