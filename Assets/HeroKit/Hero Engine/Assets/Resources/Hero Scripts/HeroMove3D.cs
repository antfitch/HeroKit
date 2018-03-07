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
    public class HeroMove3D : MonoBehaviour
    {
        public HeroSettings3D settings;

        private void Awake()
        {
            HeroKitObject targetObject = this.GetComponent<HeroKitObject>();
            settings = targetObject.GetHeroComponent<HeroSettings3D>("HeroSettings3D", true);
        }

        /// <summary>
        /// A delegate that represents a movement or turn (for example MoveUp() or TurnUp()). This action is applied to the game object.
        /// </summary>
        public Action Move;

        public Transform target;
        private Vector2 speed;
        public float turnAngle;

        // --------------------------------------------------------------
        // Variables (internal)
        // --------------------------------------------------------------

        /// <summary>
        /// Tracks the steps in the move. (0=start move, 1=moving, 2=end move) 
        /// </summary>
        private int moveStep;
        /// <summary>
        /// The duration of the move + current time.
        /// </summary>
        private float totalMoveDuration;
        /// <summary>
        /// Tracks the steps in the turn. (0=start turn, 1=turning, 2=end turn) 
        /// </summary>
        private int turnStep;
        /// <summary>
        /// A custom direction in which to move the game object.
        /// </summary>
        private Vector3 customDirection;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Initialize this script.
        /// </summary>
        public void Initialize()
        {
            enabled = true;
        }
        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate ()
        {
            // move an object
            if (settings.moveObject)
            {
                MoveObjectUpdate();
            }

            // turn an object
            if (settings.turnObject)
            {
                TurnObjectUpdate();
            }
        }
        /// <summary>
        /// Manage the current status of the move.
        /// </summary>
        private void MoveObjectUpdate()
        {
            // get duration
            if (moveStep == 0)
            {
                settings.customMovementIsOn = true;
                Move();
                settings.angle = turnAngle;
                settings.hasTurnedForMove = false;
                settings.TurnObject();
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
                    settings.ComputeVelocity(speed);
                    settings.AnimateCharacter();                    
                    settings.MoveCharacter(true);
                }
            }

            // stop moving
            else if (moveStep == 2)
            {
                settings.ComputeVelocity(new Vector2(0, 0));
                settings.AnimateCharacter();
                settings.MoveCharacter(true);
                //if (settings.velocity == new Vector2(0, 0))
                //{
                    moveStep = 0;
                    settings.customMovementIsOn = false;
                    enabled = false;
                //}
            }
        }
        /// <summary>
        /// Manage the current status of the turn.
        /// </summary>
        private void TurnObjectUpdate()
        {
            // snap to position
            if (Math.Abs(settings.turnDuration) < 0.1f)
            {
                Move();
                settings.angle = turnAngle;
                settings.turnObject = false;
                enabled = false;
            }

            // rotate to position
            else
            {
                // get duration
                switch (turnStep)
                {
                    case 0:
                        settings.currentTurnTime = 0;
                        settings.turnFrom = transform.rotation;
                        settings.turning = true;
                        Move();
                        settings.angle = turnAngle;
                        turnStep = 1;
                        break;
                    case 1:
                        if (!settings.turning)
                            turnStep = 2;
                        else
                            settings.TurnObject();
                        break;
                    case 2:
                        turnStep = 0;
                        settings.StopAnimating();
                        settings.turnObject = false;
                        if (!settings.moveObject)
                            enabled = false;
                        break;
                }
            }
        }

        // --------------------------------------------------------------
        // Methods (Collisions during movement)
        // --------------------------------------------------------------

        /// <summary>
        /// If game object collides with something, finish move early.
        /// </summary>
        /// <param name="collision">A collision with another item.</param>
        private void OnCollisionEnter(Collision collision)
        {
            bool result = settings.FinishCollisionMoveEarly(collision);
            // exit movement if we should stop movement upon colliding with another object
            if (result) moveStep = 2;
        }
        /// <summary>
        /// If game object is colliding with something, finish move early.
        /// </summary>
        /// <param name="collision">A collision with another item.</param>
        private void OnCollisionStay(Collision collision)
        {
            bool result = settings.FinishCollisionMoveEarly(collision);
            // exit movement if we should stop movement upon colliding with another object
            if (result) moveStep = 2;

        }

        // --------------------------------------------------------------
        // Methods (Move hero object)
        // --------------------------------------------------------------

        /// <summary>
        /// Move the game object left.
        /// </summary>
        public void MoveLeft()
        {
            TurnLeft();
            speed = new Vector2(-1, 0);
        }
        /// <summary>
        /// Move the game object right.
        /// </summary>
        public void MoveRight()
        {
            TurnRight();
            speed = new Vector2(1, 0);
        }
        /// <summary>
        /// Move the game object up.
        /// </summary>
        public void MoveUp()
        {
            TurnUp();
            speed = new Vector2(0, 1);
        }
        /// <summary>
        /// Move the game object down.
        /// </summary>
        public void MoveDown()
        {
            TurnDown();
            speed = new Vector2(0, -1);
        }
        /// <summary>
        /// Move the game object down + left.
        /// </summary>
        public void MoveLowerLeft()
        {
            TurnLowerLeft();
            speed = new Vector2(-1, -1);
        }
        /// <summary>
        /// Move the game object down + right.
        /// </summary>
        public void MoveLowerRight()
        {
            TurnLowerRight();
            speed = new Vector2(1, -1);
        }
        /// <summary>
        /// Move the game object up + left.
        /// </summary>
        public void MoveUpperLeft()
        {
            TurnUpperLeft();
            speed = new Vector2(-1, 1);
        }
        /// <summary>
        /// Move the game object up + right.
        /// </summary>
        public void MoveUpperRight()
        {
            TurnUpperRight();
            speed = new Vector2(1, 1);
        }
        /// <summary>
        /// Move game object in random direction.
        /// </summary>
        public void MoveRandom()
        {
            TurnRandom();
            GetMoveFromAngle();
        }
        /// <summary>
        /// Move game object in the opposite direction.
        /// </summary>
        public void MoveOpposite()
        {
            TurnOpposite();
            GetMoveFromAngle();
        }
        /// <summary>
        /// Move game object in the opposite direction.
        /// </summary>
        public void MoveForward()
        {
            TurnForward();
            GetMoveFromAngle();
        }
        /// <summary>
        /// Move game object away from a target object.
        /// </summary>
        public void MoveAwayFromObject()
        {
            TurnAwayFromObject();
            GetMoveFromAngle();
        }
        /// <summary>
        /// Move game object toward a target object.
        /// </summary>
        public void MoveTowardObject()
        {
            TurnTowardObject();
            GetMoveFromAngle();
        }
        /// <summary>
        /// Move game object in a custom direction.
        /// </summary>
        public void MoveCustom()
        {
            GetTurnFromAngle();
            GetMoveFromAngle();
        }

        // --------------------------------------------------------------
        // Methods (Turn hero object)
        // --------------------------------------------------------------

        /// <summary>
        /// Turn the game object left.
        /// </summary>
        public void TurnLeft()
        {
            turnAngle = -90;
        }
        /// <summary>
        /// Turn the game object right.
        /// </summary>
        public void TurnRight()
        {
            turnAngle = 90;
        }
        /// <summary>
        /// Turn the game object up.
        /// </summary>
        public void TurnUp()
        {
            turnAngle = 0;
        }
        /// <summary>
        /// Turn the game object down.
        /// </summary>
        public void TurnDown()
        {
            turnAngle = -180;
        }
        /// <summary>
        /// Turn the game object down + left.
        /// </summary>
        public void TurnLowerLeft()
        {
            turnAngle = -135;
        }
        /// <summary>
        /// Turn the game object down + right.
        /// </summary>
        public void TurnLowerRight()
        {
            turnAngle = 135;
        }
        /// <summary>
        /// Turn the game object up + left.
        /// </summary>
        public void TurnUpperLeft()
        {
            turnAngle = -45;
        }
        /// <summary>
        /// Turn the game object up + right.
        /// </summary>
        public void TurnUpperRight()
        {
            turnAngle = 45;
        }
        /// <summary>
        /// Turn the game object in random direction.
        /// </summary>
        public void TurnRandom()
        {
            turnAngle = GetRandomDirection();
            GetTurnFromAngle();
        }
        /// <summary>
        /// Turn the game object in the opposite direction.
        /// </summary>
        public void TurnOpposite()
        {
            turnAngle = GetOppositeDirection();
            GetTurnFromAngle();
        }
        /// <summary>
        /// Turn the game object in the currendly facing direction.
        /// </summary>
        public void TurnForward()
        {
            turnAngle = GetForwardDirection();
            GetTurnFromAngle();
        }
        /// <summary>
        /// Turn the game object away from a target object.
        /// </summary>
        public void TurnAwayFromObject()
        {
            turnAngle = SetAwayFromObject();
            GetTurnFromAngle();
        }
        /// <summary>
        /// Turn the game object toward a target object.
        /// </summary>
        public void TurnTowardObject()
        {
            turnAngle = SetTowardObject();
            GetTurnFromAngle();
        }
        /// <summary>
        /// Turn the game object in a custom direction.
        /// </summary>
        public void TurnCustom()
        {
            GetTurnFromAngle();
        }

        // --------------------------------------------------------------
        // Methods (Helpers)
        // --------------------------------------------------------------

        /// <summary>
        /// Move the game object in a specific direction.
        /// </summary>
        private void GetMoveFromAngle()
        {
            speed = new Vector2(customDirection.x, customDirection.z);
        }
        /// <summary>
        /// Get direction to turn from an angle.
        /// </summary>
        private void GetTurnFromAngle()
        {
            SetCustomDirection(turnAngle);
            Quaternion newRotation = Quaternion.LookRotation(customDirection);
            turnAngle = newRotation.eulerAngles.y;
        }
        /// <summary>
        /// Get a random direction in which to move the game object.
        /// </summary>
        /// <returns></returns>
        private int GetRandomDirection()
        {
            return Random.Range(0, 360);
        }
        /// <summary>
        /// Get the opposite direction in which the game object is facing.
        /// </summary>
        private int GetOppositeDirection()
        {
            return (int)transform.localEulerAngles.y + 180;
        }
        /// <summary>
        /// Set the direction in which the game object is facing.
        /// </summary>
        private int GetForwardDirection()
        {
            return (int)transform.localEulerAngles.y;
        }
        /// <summary>
        /// Get another game object and turn this game object away from it.
        /// </summary>
        /// <param name="target"></param>
        private int SetAwayFromObject()
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            return (int)targetRotation.eulerAngles.y + 180;
        }
        /// <summary>
        /// Get another game object and turn this game object toward it.
        /// </summary>
        /// <param name="target"></param>
        private int SetTowardObject()
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            return (int)targetRotation.eulerAngles.y;
        }
        /// <summary>
        /// Set the direction in which this game object is facing with a custom angle.
        /// </summary>
        /// <param name="customAngle"></param>
        private void SetCustomDirection(float customAngle)
        {
            customDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * customAngle), 0, Mathf.Cos(Mathf.Deg2Rad * customAngle));
        }
    }
}



//// --------------------------------------------------------------
//// Copyright (c) 2016-2017 Aveyond Studios. 
//// All Rights Reserved.
//// --------------------------------------------------------------
//using System;
//using UnityEngine;
//using Random = UnityEngine.Random;

//namespace HeroKit.Scene.Scripts
//{
//    /// <summary>
//    /// Move or turn a hero object.
//    /// </summary>
//    public class HeroObjectMove : MonoBehaviour
//    {
//        // --------------------------------------------------------------
//        // Variables (general)
//        // --------------------------------------------------------------

//        /// <summary>
//        /// The rigidbody attached to the game object. If it does not exist, one is added.
//        /// </summary>
//        public Rigidbody objectRigidbody;
//        /// <summary>
//        /// A delegate that represents a movement or turn (for example MoveUp() or TurnUp()). This action is applied to the game object.
//        /// </summary>
//        public Action Move;
//        /// <summary>
//        /// The animator attached to the game object.
//        /// </summary>
//        public Animator animator;
//        /// <summary>
//        /// Move the game object.
//        /// </summary>
//        public bool moveObject;
//        /// <summary>
//        /// Turn the game object.
//        /// </summary>
//        public bool turnObject;
//        /// <summary>
//        /// Stop moving the game object if it collides with something.
//        /// </summary>
//        public bool finishMoveWhenCollide = true;
//        /// <summary>
//        /// Stop moving the game object if it collides with something in a specific layer. 
//        /// </summary>
//        public LayerMask layermaskCollide;

//        // --------------------------------------------------------------
//        // Variables (settings)
//        // --------------------------------------------------------------

//        /// <summary>
//        /// Game object should face the direction in which it is moving.
//        /// </summary>
//        public bool faceDirection = true;
//        /// <summary>
//        /// Animate the game object when it moves.
//        /// </summary>
//        public bool animate = true;
//        /// <summary>
//        /// Animation to play when the game object is moving.
//        /// </summary>
//        public string animationName = "IsWalking";
//        /// <summary>
//        /// The speed of the move.
//        /// </summary>
//        public float speed = 2;
//        /// <summary>
//        /// The duration of the move.
//        /// </summary>
//        public float moveDuration = 1;
//        /// <summary>
//        /// The duration of the turn.
//        /// </summary>
//        public float turnDuration;
//        /// <summary>
//        /// A custom direction in which to move the game object.
//        /// </summary>
//        public Vector3 customDirection;

//        // --------------------------------------------------------------
//        // Variables (internal)
//        // --------------------------------------------------------------

//        /// <summary>
//        /// Tracks the steps in the move. (0=start move, 1=moving, 2=end move) 
//        /// </summary>
//        private int moveStep;
//        /// <summary>
//        /// The duration of the move + current time.
//        /// </summary>
//        private float totalMoveDuration;
//        /// <summary>
//        /// Tracks the steps in the turn. (0=start turn, 1=turning, 2=end turn) 
//        /// </summary>
//        private int turnStep;
//        /// <summary>
//        /// The rotation of the game object before it turns.
//        /// </summary>
//        private Quaternion turnFrom;
//        /// <summary>
//        /// The destination rotation of the game object.
//        /// </summary>
//        private Quaternion turnTo;
//        /// <summary>
//        /// The rotation of the game object in the previous frame.
//        /// </summary>
//        private Quaternion oldTurnRotation;
//        /// <summary>
//        /// The time that has passed since the game object started the turn.
//        /// </summary>
//        private float currentTurnTime;
//        /// <summary>
//        /// The game object is turning.
//        /// </summary>
//        private bool turning;
//        /// <summary>
//        /// The game object has turned for a move.
//        /// </summary>
//        private bool hasTurnedForMove;

//        // --------------------------------------------------------------
//        // Methods (General)
//        // --------------------------------------------------------------

//        /// <summary>
//        /// Call this when the component is attached to the game object
//        /// </summary>
//        private void Awake()
//        {
//            objectRigidbody = GetComponent<Rigidbody>();
//            if (objectRigidbody == null)
//                objectRigidbody = gameObject.AddComponent<Rigidbody>();
//        }
//        /// <summary>
//        /// Initialize this script.
//        /// </summary>
//        public void Initialize()
//        {
//            hasTurnedForMove = false;
//            // enables update methods for this class
//            enabled = true;
//        }
//        /// <summary>
//        /// Execute this method every frame.
//        /// </summary>
//        private void FixedUpdate()
//        {
//            // move an object
//            if (moveObject)
//            {
//                MoveObjectUpdate();
//            }

//            // turn an object
//            if (turnObject)
//            {
//                TurnObjectUpdate();
//            }
//        }
//        /// <summary>
//        /// Manage the current status of the move.
//        /// </summary>
//        private void MoveObjectUpdate()
//        {
//            // get duration
//            if (moveStep == 0)
//            {
//                StartAnimating();
//                totalMoveDuration = Time.time + moveDuration;
//                moveStep = 1;
//            }

//            // move the object
//            else if (moveStep == 1)
//            {
//                if (Time.time >= totalMoveDuration)
//                {
//                    moveStep = 2;
//                }
//                else
//                {
//                    Move();
//                }
//            }

//            // stop moving
//            else if (moveStep == 2)
//            {
//                moveStep = 0;
//                StopAnimating();
//                moveObject = false;
//                enabled = false;
//            }
//        }
//        /// <summary>
//        /// Manage the current status of the turn.
//        /// </summary>
//        private void TurnObjectUpdate()
//        {
//            // snap to position
//            if (Math.Abs(turnDuration) < 0.1f)
//            {
//                Move();
//                turnObject = false;
//                enabled = false;
//            }

//            // rotate to position
//            else
//            {
//                // get duration
//                switch (turnStep)
//                {
//                    case 0:
//                        currentTurnTime = 0;
//                        turnFrom = transform.rotation;
//                        speed = speed * 0.1f;
//                        turning = true;
//                        turnStep = 1;
//                        break;
//                    case 1:
//                        if (!turning)
//                            turnStep = 2;
//                        else
//                            Move();
//                        break;
//                    case 2:
//                        turnStep = 0;
//                        StopAnimating();
//                        turnObject = false;
//                        enabled = false;
//                        break;
//                }
//            }
//        }

//        // --------------------------------------------------------------
//        // Methods (Move hero object)
//        // --------------------------------------------------------------

//        /// <summary>
//        /// Move the game object in a specific direction.
//        /// </summary>
//        public void MoveCustom()
//        {
//            Quaternion newRotation = Quaternion.LookRotation(customDirection);
//            TurnObject(newRotation.eulerAngles.x, newRotation.eulerAngles.y, newRotation.eulerAngles.z);
//            MoveObject(customDirection.x, customDirection.y, customDirection.z);
//        }
//        /// <summary>
//        /// Move the game object left.
//        /// </summary>
//        public void MoveLeft()
//        {
//            TurnLeft();
//            MoveObject(-1f, 0f, 0f);
//        }
//        /// <summary>
//        /// Move the game object right.
//        /// </summary>
//        public void MoveRight()
//        {
//            TurnRight();
//            MoveObject(1f, 0f, 0f);
//        }
//        /// <summary>
//        /// Move the game object up.
//        /// </summary>
//        public void MoveUp()
//        {
//            TurnUp();
//            MoveObject(0f, 0f, 1f);
//        }
//        /// <summary>
//        /// Move the game object down.
//        /// </summary>
//        public void MoveDown()
//        {
//            TurnDown();
//            MoveObject(0f, 0f, -1f);
//        }
//        /// <summary>
//        /// Move the game object down + left.
//        /// </summary>
//        public void MoveLowerLeft()
//        {
//            TurnLowerLeft();
//            MoveObject(-1f, 0f, -1f);
//        }
//        /// <summary>
//        /// Move the game object down + right.
//        /// </summary>
//        public void MoveLowerRight()
//        {
//            TurnLowerRight();
//            MoveObject(1f, 0f, -1f);
//        }
//        /// <summary>
//        /// Move the game object up + left.
//        /// </summary>
//        public void MoveUpperLeft()
//        {
//            TurnUpperLeft();
//            MoveObject(-1f, 0f, 1f);
//        }
//        /// <summary>
//        /// Move the game object up + right.
//        /// </summary>
//        public void MoveUpperRight()
//        {
//            TurnUpperRight();
//            MoveObject(1f, 0f, 1f);
//        }

//        // --------------------------------------------------------------
//        // Methods (Turn hero object)
//        // --------------------------------------------------------------

//        /// <summary>
//        /// Turn the game object in a specific direction.
//        /// </summary>
//        public void TurnCustom()
//        {
//            Quaternion newRotation = Quaternion.LookRotation(customDirection);
//            TurnObject(newRotation.eulerAngles.x, newRotation.eulerAngles.y, newRotation.eulerAngles.z);
//        }
//        /// <summary>
//        /// Turn the game object left.
//        /// </summary>
//        public void TurnLeft()
//        {
//            TurnObject(0f, -90f, 0f);
//        }
//        /// <summary>
//        /// Turn the game object right.
//        /// </summary>
//        public void TurnRight()
//        {
//            TurnObject(0f, 90f, 0f);
//        }
//        /// <summary>
//        /// Turn the game object up.
//        /// </summary>
//        public void TurnUp()
//        {
//            TurnObject(0f, 0f, 0f);
//        }
//        /// <summary>
//        /// Turn the game object down.
//        /// </summary>
//        public void TurnDown()
//        {
//            TurnObject(0f, -180f, 0f);
//        }
//        /// <summary>
//        /// Turn the game object down + left.
//        /// </summary>
//        public void TurnLowerLeft()
//        {
//            TurnObject(0f, -135f, 0f);
//        }
//        /// <summary>
//        /// Turn the game object down + right.
//        /// </summary>
//        public void TurnLowerRight()
//        {
//            TurnObject(0f, 135f, 0f);
//        }
//        /// <summary>
//        /// Turn the game object up + left.
//        /// </summary>
//        public void TurnUpperLeft()
//        {
//            TurnObject(0f, -45f, 0f);
//        }
//        /// <summary>
//        /// Turn the game object up + right.
//        /// </summary>
//        public void TurnUpperRight()
//        {
//            TurnObject(0f, 45f, 0f);
//        }

//        // --------------------------------------------------------------
//        // Methods (Collisions during movement)
//        // --------------------------------------------------------------

//        /// <summary>
//        /// If game object collides with something, finish move early.
//        /// </summary>
//        /// <param name="collision">A collision with another item.</param>
//        private void OnCollisionEnter(Collision collision)
//        {
//            FinishCollisionMoveEarly(collision);
//        }
//        /// <summary>
//        /// If game object is colliding with something, finish move early.
//        /// </summary>
//        /// <param name="collision">A collision with another item.</param>
//        private void OnCollisionStay(Collision collision)
//        {
//            FinishCollisionMoveEarly(collision);
//        }
//        /// <summary>
//        /// Finish move early if the game object collides with something.
//        /// </summary>
//        /// <param name="collision">A collision with another item.</param>
//        private void FinishCollisionMoveEarly(Collision collision)
//        {
//            // exit early if this component is disabled or we don't want to check for collisions
//            if (!enabled || !finishMoveWhenCollide) return;

//            // look for collisions on specified layers only
//            if ((layermaskCollide & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
//            {
//                // exit movement if we should stop movement upon colliding with another object
//                if (moveObject) moveStep = 2;
//            }
//        }

//        // --------------------------------------------------------------
//        // Methods (Move & Turn)
//        // --------------------------------------------------------------

//        /// <summary>
//        /// Move the game object.
//        /// </summary>
//        /// <param name="x">X position.</param>
//        /// <param name="y">Y position.</param>
//        /// <param name="z">Z position.</param>
//        private void MoveObject(float x, float y, float z)
//        {
//            Vector3 direction = new Vector3();

//            // Set the movement vector based on the axis input.
//            direction.Set(x, y, z);

//            // Normalise the movement vector and make it proportional to the speed per second.
//            direction = direction.normalized * speed * Time.deltaTime;

//            // Move the object to it's current position plus the movement.
//            objectRigidbody.MovePosition(objectRigidbody.transform.position + direction);
//        }
//        /// <summary>
//        /// Turn the game object.
//        /// </summary>
//        /// <param name="x">X euler angle.</param>
//        /// <param name="y">Y euler angle.</param>
//        /// <param name="z">Z euler angle.</param>
//        private void TurnObject(float x, float y, float z)
//        {
//            if (faceDirection)
//            {
//                // turn immediately (used with move action)
//                if (Math.Abs(turnDuration) < 0.1f)
//                {
//                    if (!hasTurnedForMove)
//                    {
//                        transform.eulerAngles = new Vector3(x, y, z);
//                        hasTurnedForMove = true;
//                    }
//                }
//                // turn slowly (used with turn action) 
//                else
//                {
//                    turnTo = Quaternion.Euler(x, y, z);
//                    currentTurnTime += Time.deltaTime;
//                    transform.rotation = Quaternion.Slerp(turnFrom, turnTo, currentTurnTime * turnDuration * 0.1f);

//                    // exit if we are at destination
//                    if (oldTurnRotation == transform.rotation)
//                    {
//                        turning = false;
//                    }

//                    // record current position
//                    oldTurnRotation = transform.rotation;
//                }
//            }
//        }

//        // --------------------------------------------------------------
//        // Methods (Animation)
//        // --------------------------------------------------------------

//        /// <summary>
//        /// Use the movement animation on the game object.
//        /// </summary>
//        private void StartAnimating()
//        {
//            if (animate && animator != null && animationName != "")
//            {
//                animator.SetBool(animationName, true);
//            }
//        }
//        /// <summary>
//        /// Stop the movement animation the game object and return to whatever the game object was using before.
//        /// </summary>
//        private void StopAnimating()
//        {
//            if (animate && animator != null && animationName != "")
//            {
//                animator.SetBool(animationName, false);
//            }
//        }

//        // --------------------------------------------------------------
//        // Methods (Helpers)
//        // --------------------------------------------------------------

//        /// <summary>
//        /// Get a random direction in which to move the game object.
//        /// </summary>
//        /// <returns></returns>
//        public int GetRandomDirection()
//        {
//            return Random.Range(0, 360);
//        }
//        /// <summary>
//        /// Get the opposite direction in which the game object is facing.
//        /// </summary>
//        public int GetOppositeDirection()
//        {
//            return (int)transform.localEulerAngles.y + 180;
//        }
//        /// <summary>
//        /// Set the opposite direction in which the game object is facing.
//        /// </summary>
//        public void SetOppositeDirection()
//        {
//            int angle = (int)transform.localEulerAngles.y + 180;
//            SetCustomDirection(angle);
//        }
//        /// <summary>
//        /// Set the direction in which the game object is facing.
//        /// </summary>
//        public void SetForwardDirection()
//        {
//            int angle = (int)transform.localEulerAngles.y;
//            SetCustomDirection(angle);
//        }
//        /// <summary>
//        /// Get another game object and turn this game object away from it.
//        /// </summary>
//        /// <param name="target"></param>
//        public void SetAwayFromObject(Transform target)
//        {
//            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
//            int angle = (int)targetRotation.eulerAngles.y + 180;
//            SetCustomDirection(angle);
//        }
//        /// <summary>
//        /// Get another game object and turn this game object toward it.
//        /// </summary>
//        /// <param name="target"></param>
//        public void SetTowardObject(Transform target)
//        {
//            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
//            int angle = (int)targetRotation.eulerAngles.y;
//            SetCustomDirection(angle);
//        }
//        /// <summary>
//        /// Set the direction in which this game object is facing with a custom angle.
//        /// </summary>
//        /// <param name="customAngle"></param>
//        public void SetCustomDirection(int customAngle)
//        {
//            customDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * customAngle), 0, Mathf.Cos(Mathf.Deg2Rad * customAngle));
//        }
//    }
//}