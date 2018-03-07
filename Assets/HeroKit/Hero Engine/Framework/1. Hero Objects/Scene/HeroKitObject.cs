// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using HeroKit.Scene.Actions;
using HeroKit.Scene.ActionField;
using System.Linq;

namespace HeroKit.Scene
{
    /// <summary>
    /// The hero kit object script that is attached to a game object.
    /// </summary>
    public class HeroKitObject : MonoBehaviour
    {
        // -------------------------------------------------------------
        // Variables (External Stuff visible on game object)
        // -------------------------------------------------------------

        /// <summary>
        /// The hero object assigned to a hero kit object component on a game object.
        /// </summary>
        public HeroObject heroObject;
        /// <summary>
        /// Should we show debug messages when this hero kit object is enabled in the scene?
        /// </summary>
        public bool debugHeroObject;
        /// <summary>
        /// A unique identifier assigned to the hero kit object in the scene.
        /// </summary>
        public int heroGUID;
        /// <summary>
        /// Do not save this hero kit object when the game or scene is saved?
        /// </summary>
        public bool doNotSave;
        /// <summary>
        /// Do not pause this hero kit object then the game is paused?
        /// </summary>
        public bool doNotPause;
        /// <summary>
        /// The current state assigned to this hero kit object at runtime.
        /// </summary>
        public HeroState heroState;
        /// <summary>
        /// The current properties assigned to this hero kit object at runtime.
        /// </summary>
        public HeroProperties[] heroProperties = new HeroProperties[0];
        /// <summary>
        /// The current list of local variables assigned to this hero kit object at runtime.
        /// </summary>
        public HeroList heroList;
        /// <summary>
        /// The current list of global variables for all hero kit objects at runtime.
        /// </summary>
        public HeroList heroGlobals;

        // -------------------------------------------------------------
        // Variables (Internal Stuff)
        // -------------------------------------------------------------  

        /// <summary>
        /// Should this hero kit object persist in all scenes?
        /// </summary>
        [HideInInspector]
        public bool persist;
        /// <summary>
        /// Temporary data assigned to the hero object (ex. current state, event, and action IDs)
        /// </summary>
        public HeroStateData heroStateData;
        /// <summary>
        /// Data that was passed from a hero kit listener to this object.
        /// </summary>
        public HeroKitListenerData heroListenerData = new HeroKitListenerData();
        /// <summary>
        /// Messages from other objects can impact performance. we only want to watch messages we know we are going to use in the current state.
        /// </summary>
        private bool[] eventsToWatch;
        /// <summary>
        /// A list of gameobjects that are currently colliding with this hero kit object.
        /// </summary>
        public List<HeroKitObject> collisionsList = new List<HeroKitObject>();
        /// <summary>
        /// This hero kit object is currently inside the trigger area of these game objects.
        /// </summary>
        public List<HeroKitObject> triggersList = new List<HeroKitObject>();
        /// <summary>
        /// An action key was pressed (spacebar, enter, return, left-click)
        /// </summary>
        private bool actionKeyPressed;
        /// <summary>
        /// This dictionary collects components on this game object.
        /// </summary>
        public Dictionary<string, Component> ComponentDictionary = new Dictionary<string, Component>();
        /// <summary>
        /// Any actions put in here require more than one frame to complete (wait, character movement, etc)
        /// these actions will be updated every frame until they complete.
        /// while they are incomplete, no other action in thier origianal events will run
        /// when an action completes, it is removed from this list.
        /// </summary>
        /// <remarks>Long actions are run with Update.</remarks>
        public List<IHeroKitAction> longActions;
        /// <summary>
        /// See description for longActions.
        /// </summary>
        /// <remarks>Long actions are run with Fixed Update. Fixed update should be used with physics.</remarks>
        public List<IHeroKitAction> longActionsFixed;
        /// <summary>
        /// The currently active action
        /// </summary>
        public IHeroKitAction currentAction;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// This is called once when hero kit object is enabled.
        /// </summary>
        private void Awake()
        {
            HeroKitCommonRuntime.initializeGame(); // initialize game when first hero object loads
            ChangeHeroObject(heroObject); // load hero object data onto game object
            heroGlobals = HeroKitDatabase.GetGlobals(); // get the global variables
        }
        /// <summary>
        /// This is called once when the game is closed.
        /// </summary>
        private void OnApplicationQuit()
        {
            HeroKitCommonRuntime.QuitGame(); 
        }
        /// <summary>
        /// This is called every frame (best for math).
        /// </summary>
        private void Update()
        {
            if (heroObject == null) return;

            CheckState(); // check to see which state can be active

            actionKeyPressed = IsActionKeyPressed(); // needed for any interaction events

            if (eventsToWatch[5]) OnTriggerStay(); // check for trigger event
            if (eventsToWatch[8]) OnCollisionStay(); // check for collisiont
            if (eventsToWatch[2]) Actions_Run_When_Key_Pressed();
            if (eventsToWatch[0]) Actions_Loop_General();
            Actions_Long();

            actionKeyPressed = false;
        }
        /// <summary>
        /// This is called every x number of milliseconds. (best for physics)
        /// </summary>
        private void FixedUpdate()
        {
            if (heroObject == null) return;
            if (eventsToWatch[1]) Actions_Loop_FixedUpdate();
            Actions_LongFixed();
        }

        /// <summary>
        /// (3D) The hero kit object has encountered a trigger area on another object.
        /// </summary>
        /// <param name="collider">The collider on the other object.</param>
        private void OnTriggerEnter(Collider collider)
        {
            // get game object assigned to hero kit object
            HeroKitObject colliderHKO = HeroKitCommonRuntime.GetTriggerGameObject(collider);

            // exit early if trigger was not with a hero kit object
            if (colliderHKO == null) return;

            // add objects to trigger list
            if (!triggersList.Contains(colliderHKO))
            {
                // Objects that have triggers but no rigidbodies never have OnTriggerEntered called.
                // To get around this, have the object with the ridigbody update the trigger list
                // for the object that does not have a ridigbody.
                if (collider.attachedRigidbody == null) colliderHKO.triggersList.Add(this);

                // Add the item with the trigger area to this hero object's trigger list.
                triggersList.Add(colliderHKO);
            }

            if (eventsToWatch[3])
            {
                // play enter trigger on object w/o ridigbody (if it exists)
                if (collider.attachedRigidbody == null) Actions_Run_When_Message_Received(gameObject, 1);

                // play enter trigger on this object
                Actions_Run_When_Message_Received(colliderHKO.gameObject, 1);
            } 
        }
        /// <summary>
        /// (2D) The hero kit object has encountered a trigger area on another object.
        /// </summary>
        /// <param name="collider">The collider on the other object.</param>
        private void OnTriggerEnter2D(Collider2D collider)
        {
            // get game object assigned to hero kit object
            HeroKitObject colliderHKO = HeroKitCommonRuntime.GetTriggerGameObject2D(collider);

            // exit early if trigger was not with a hero kit object
            if (colliderHKO == null) return;

            // add objects to trigger list
            if (!triggersList.Contains(colliderHKO))
            {
                // Objects that have triggers but no rigidbodies never have OnTriggerEntered called.
                // To get around this, have the object with the ridigbody update the trigger list
                // for the object that does not have a ridigbody.
                if (collider.attachedRigidbody == null) colliderHKO.triggersList.Add(this);

                // Add the item with the trigger area to this hero object's trigger list.
                triggersList.Add(colliderHKO);
            }

            if (eventsToWatch[3])
            {
                // play enter trigger on object w/o ridigbody (if it exists)
                if (collider.attachedRigidbody == null) Actions_Run_When_Message_Received(gameObject, 1);

                // play enter trigger on this object
                Actions_Run_When_Message_Received(colliderHKO.gameObject, 1);
            }
        }
        /// <summary>
        /// (3D) The hero kit object has left a trigger area on another object.
        /// </summary>
        /// <param name="collider">The collider on the other object.</param>
        private void OnTriggerExit(Collider collider)
        {
            // get game object assigned to hero kit object
            HeroKitObject colliderHKO = HeroKitCommonRuntime.GetTriggerGameObject(collider);

            // exit early if exit trigger was not with a hero kit object
            if (colliderHKO == null) return;

            // remove objects to trigger list
            if (collisionsList.Contains(colliderHKO))
            {
                // Objects that have trigger but no rigidbodies never have OnTriggerExit called.
                // To get around this, have the object with the ridigbody update the trigger list
                // for the object that does not have a ridigbody.
                if (collider.attachedRigidbody == null) colliderHKO.triggersList.Remove(this);

                // Remove the item with the trigger area.
                collisionsList.Remove(colliderHKO);
            }

            if (eventsToWatch[4])
            {
                // play exit trigger on object w/o ridigbody (if it exists)
                if (collider.attachedRigidbody == null) Actions_Run_When_Message_Received(gameObject, 2);

                // play exit trigger on this object
                Actions_Run_When_Message_Received(colliderHKO.gameObject, 2);
            } 
        }
        /// <summary>
        /// (2D) The hero kit object has left a trigger area on another object.
        /// </summary>
        /// <param name="collider">The collider on the other object.</param>
        private void OnTriggerExit2D(Collider2D collider)
        {
            // get game object assigned to hero kit object
            HeroKitObject colliderHKO = HeroKitCommonRuntime.GetTriggerGameObject2D(collider);

            // exit early if exit trigger was not with a hero kit object
            if (colliderHKO == null) return;

            // remove objects to trigger list
            if (collisionsList.Contains(colliderHKO))
            {
                // Objects that have trigger but no rigidbodies never have OnTriggerExit called.
                // To get around this, have the object with the ridigbody update the trigger list
                // for the object that does not have a ridigbody.
                if (collider.attachedRigidbody == null) colliderHKO.triggersList.Remove(this);

                // Remove the item with the trigger area.
                collisionsList.Remove(colliderHKO);
            }

            if (eventsToWatch[4])
            {
                // play exit trigger on object w/o ridigbody (if it exists)
                if (collider.attachedRigidbody == null) Actions_Run_When_Message_Received(gameObject, 2);

                // play exit trigger on this object
                Actions_Run_When_Message_Received(colliderHKO.gameObject, 2);
            }
        }
        /// <summary>
        /// (2D and 3D) The hero kit object is in a trigger area on another object.
        /// </summary>
        /// <param name="collider">The collider on the other object.</param>
        private void OnTriggerStay()
        {
            for (int i = 0; i < triggersList.Count; i++)
            {
                Actions_Run_When_Message_Received(triggersList[i].gameObject, 3);
            }

            // clean up collisions (remove any empties)
            for (int i = 0; i < triggersList.Count; i++)
            {
                if (triggersList[i] == null || !triggersList[i].gameObject.activeSelf)
                    triggersList.Remove(triggersList[i]);
            }
        }

        /// <summary>
        /// (3D) The hero kit object has entered a collision with another object.
        /// </summary>
        /// <param name="collider">The collider on the other object.</param>
        private void OnCollisionEnter(Collision collider)
        {
            // get game object assigned to hero kit object
            HeroKitObject colliderHKO = HeroKitCommonRuntime.GetCollisionGameObject(collider);

            // exit early if collision was not with a hero kit object
            if (colliderHKO == null) return;

            // add collision to lists
            if (!collisionsList.Contains(colliderHKO))
            {
                // Objects that have colliders but no rigidbodies never have OnCollisionEntered called.
                // To get around this, have the object with the ridigbody update the collision list
                // for the object that does not have a ridigbody.
                if (collider.rigidbody == null) colliderHKO.collisionsList.Add(this);

                // Add the item this hero object collided with to this hero object's collision list.
                collisionsList.Add(colliderHKO);
            }

            if (eventsToWatch[6])
            {
                // play collision on object w/o ridigbody (if it exists)
                if (collider.rigidbody == null) Actions_Run_When_Message_Received(gameObject, 4);

                // play collision on this object
                Actions_Run_When_Message_Received(colliderHKO.gameObject, 4);
            }
                
        }
        /// <summary>
        /// (2D) The hero kit object has entered a collision with another object.
        /// </summary>
        /// <param name="collider">The collider on the other object.</param>
        private void OnCollisionEnter2D(Collision2D collider)
        {
            // get game object assigned to hero kit object
            HeroKitObject colliderHKO = HeroKitCommonRuntime.GetCollisionGameObject2D(collider);

            // exit early if collision was not with a hero kit object
            if (colliderHKO == null) return;

            // add collision to lists
            if (!collisionsList.Contains(colliderHKO))
            {
                // Objects that have colliders but no rigidbodies never have OnCollisionEntered called.
                // To get around this, have the object with the ridigbody update the collision list
                // for the object that does not have a ridigbody.
                if (collider.rigidbody == null) colliderHKO.collisionsList.Add(this);

                // Add the item this hero object collided with to this hero object's collision list.
                collisionsList.Add(colliderHKO);
            }

            if (eventsToWatch[6])
            {
                // play collision on object w/o ridigbody (if it exists)
                if (collider.rigidbody == null) Actions_Run_When_Message_Received(gameObject, 4);

                // play collision on this object
                Actions_Run_When_Message_Received(colliderHKO.gameObject, 4);
            }

        }
        /// <summary>
        /// (3D) The hero kit object has left a collision with another object.
        /// </summary>
        /// <param name="collider">The collider on the other object.</param>
        private void OnCollisionExit(Collision collider)
        {
            // get game object assigned to hero kit object
            HeroKitObject colliderHKO = HeroKitCommonRuntime.GetCollisionGameObject(collider);

            // exit early if collision was not with a hero kit object
            if (colliderHKO == null) return;

            // remove collision from lists
            if (collisionsList.Contains(colliderHKO))
            {
                // Objects that have colliders but no rigidbodies never have OnCollisionExit called.
                // To get around this, have the object with the ridigbody update the collision list
                // for the object that does not have a ridigbody.
                if (collider.rigidbody == null) colliderHKO.collisionsList.Remove(this);

                // Remove the item this hero object collided with to this hero object's collision list.
                collisionsList.Remove(colliderHKO);
            }

            // if we are watching for collision exit...
            if (eventsToWatch[7])
            {
                // play exit collision on object w/o ridigbody (if it exists)
                if (collider.rigidbody == null) Actions_Run_When_Message_Received(gameObject, 4);

                // play exit collision on this object
                Actions_Run_When_Message_Received(colliderHKO.gameObject, 5);
            }                       
        }
        /// <summary>
        /// (2D) The hero kit object has left a collision with another object.
        /// </summary>
        /// <param name="collider">The collider on the other object.</param>
        private void OnCollisionExit2D(Collision2D collider)
        {
            // get game object assigned to hero kit object
            HeroKitObject colliderHKO = HeroKitCommonRuntime.GetCollisionGameObject2D(collider);

            // exit early if collision was not with a hero kit object
            if (colliderHKO == null) return;

            // remove collision from lists
            if (collisionsList.Contains(colliderHKO))
            {
                // Objects that have colliders but no rigidbodies never have OnCollisionExit called.
                // To get around this, have the object with the ridigbody update the collision list
                // for the object that does not have a ridigbody.
                if (collider.rigidbody == null) colliderHKO.collisionsList.Remove(this);

                // Remove the item this hero object collided with to this hero object's collision list.
                collisionsList.Remove(colliderHKO);
            }

            // if we are watching for collision exit...
            if (eventsToWatch[7])
            {
                // play exit collision on object w/o ridigbody (if it exists)
                if (collider.rigidbody == null) Actions_Run_When_Message_Received(gameObject, 4);

                // play exit collision on this object
                Actions_Run_When_Message_Received(colliderHKO.gameObject, 5);
            }
        }
        /// <summary>
        /// (2D and 3D) The hero kit object is colliding with another object.
        /// </summary>
        /// <param name="collider">The collider on the other object.</param>
        private void OnCollisionStay()
        {
            // play viable collisions
            for (int i = 0; i < collisionsList.Count; i++)
            {
                if (collisionsList[i] != null && collisionsList[i].gameObject.activeSelf)
                    Actions_Run_When_Message_Received(collisionsList[i].gameObject, 6);
            }

            // clean up collisions (remove any empties)
            for (int i = 0; i < collisionsList.Count; i++)
            {
                if (collisionsList[i] == null || !collisionsList[i].gameObject.activeSelf)
                    collisionsList.Remove(collisionsList[i]);
            }
        }

        /// <summary>
        /// Called when a non-looping event pauses.
        /// </summary>
        /// <param name="eventID">The ID of the event that is paused.</param>
        private void Continue(int eventID)
        {
            // exit early if game is paused and this item should be paused
            if (HeroKitCommonRuntime.gameIsPaused && !doNotPause) return;

            PlayEvent(eventID);
        }


        // ------------------------------------------
        // Methods (action types)
        // ------------------------------------------

        /// <summary>
        /// Plays actions assigned to Loop (General).
        /// </summary>
        private void Actions_Loop_General()
        {
            PlayEvents(1);
        }

        /// <summary>
        /// Plays actions that require more than one frame to complete. Called each frame.
        /// </summary>
        private void Actions_Long()
        {
            PlayLongActions(longActions); 
        }

        /// <summary>
        /// Plays actions that require more than one frame to complete. Called each fixed frame.
        /// </summary>
        private void Actions_LongFixed()
        {
            PlayLongActions(longActionsFixed);
        }

        /// <summary>
        /// Plays actions assigned to Loop (FixedUpdate).
        /// </summary>
        private void Actions_Loop_FixedUpdate()
        {
            PlayEvents(2);
        }

        /// <summary>
        /// Plays actions assigned to Run Once.
        /// </summary>
        private void Actions_Run_Once()
        {
            PlayEvents(3);
        }

        /// <summary>
        /// Plays actions assigned to Run When Key Pressed.
        /// </summary>
        private void Actions_Run_When_Key_Pressed()
        {
            PlayEvents(5);
        }

        /// <summary>
        /// Plays actions when this hero object receives a message from another object.
        /// </summary>
        /// <param name="messenger">The game object that sent the message.</param>
        /// <param name="messageID">The ID attached to the message.</param>
        private void Actions_Run_When_Message_Received(GameObject messenger, int messageID)
        {
            PlayEvents(6, true, messageID, messenger);
        }

        // ------------------------------------------
        // Methods (Initilization)
        // ------------------------------------------

        /// <summary>
        /// Morph the hero kit object into a new hero kit object.
        /// </summary>
        /// <param name="newHeroObject">The hero object the hero kit object should morph into.</param>
        public void ChangeHeroObject(HeroObject newHeroObject)
        {
            heroObject = newHeroObject;

            // exit early if there is no hero object
            if (heroObject == null)
            {
                if (debugHeroObject) Debug.LogWarning("No hero object assigned to " + gameObject.name + " GameObject. " + HeroKitCommonRuntime.GetGameObjectDebugInfo(gameObject));
                return;
            }

            // reset hero state data
            heroStateData = new HeroStateData();

            // reset dictionary
            ComponentDictionary = new Dictionary<string, Component>();

            // reset collisions list (warning: if enabled, things this object is currently colliding with are ignored)
            //collisionsList = new List<HeroKitObject>();
            //triggersList = new List<HeroKitObject>();

            // reset long actions
            longActions = new List<IHeroKitAction>();
            longActionsFixed = new List<IHeroKitAction>();

            // reset the current action
            currentAction = null;

            // get hero state
            if (heroObject.states.states.Count == 0)
                heroState = new HeroState();
            else
                heroState = heroObject.states.states[heroStateData.state].Clone(heroObject.states.states[heroStateData.state]);

            // update graphics for new state
            HeroKitCommonRuntime.UpdateGraphicsForState(gameObject, heroState);

            // get hero properties
            heroProperties = new HeroProperties[heroObject.propertiesList.properties.Count];
            for (int i = 0; i < heroObject.propertiesList.properties.Count; i++)
            {
                heroProperties[i] = heroObject.propertiesList.properties[i].Clone(heroObject.propertiesList.properties[i]);
            }

            // get hero list
            heroList = heroObject.lists.Clone(heroObject.lists);

            // get the messages we need to watch
            eventsToWatch = WatchEvents(heroState);

            // run actions that should automatically execute for the new state
            Actions_Run_Once();
        }

        /// <summary>
        /// Change the state of a hero kit object.
        /// </summary>
        /// <param name="stateID">The ID assigned to the state to load.</param>
        public void ChangeState(int stateID)
        {
            // exit early if the state does not exist
            if (stateID < 0 || heroObject.states.states == null || stateID >= heroObject.states.states.Count)
            {
                Debug.LogError("State " + stateID + " does not exist for this hero object(" + heroObject.name + ")" );
                return;
            }

            // change the state ID
            heroStateData = new HeroStateData();
            heroStateData.state = stateID;

            // get the new hero state
            if (heroObject.states.states.Count == 0)
                heroState = new HeroState();
            else
                heroState = heroObject.states.states[heroStateData.state].Clone(heroObject.states.states[heroStateData.state]);

            // update graphics for new state
            HeroKitCommonRuntime.UpdateGraphicsForState(gameObject, heroState);

            // Go through dictionary and delete any null components
            ComponentDictionary = (from x in ComponentDictionary where x.Value != null select x).ToDictionary(x => x.Key, x => x.Value);

            // reset collisions list (warning: if enabled, things this object is currently colliding with are ignored)
            //collisionsList = new List<HeroKitObject>();
            //triggersList = new List<HeroKitObject>();

            // reset long actions
            longActions = new List<IHeroKitAction>();
            longActionsFixed = new List<IHeroKitAction>();

            // reset the current action
            currentAction = null;

            // get the messages we need to watch
            eventsToWatch = WatchEvents(heroState);

            // run actions that should automatically execute for the new state
            Actions_Run_Once();
        }

        /// <summary>
        /// If no states should be used, hide them.
        /// </summary>
        public void HideAllStates()
        {
            // disable current state
            heroState = null;

            // hide visuals
            HeroKitCommonRuntime.HideGraphicsForState(this);
        }

        //------------------------------------------------------------
        // Methods (Action Engine) 
        //------------------------------------------------------------

        /// <summary>
        /// Make sure we can use the current state. 
        /// If we can't, show a subsequent state that meets all conditions.
        /// If no states meet all conditions, don't use any state
        /// </summary>
        public void CheckState()
        {
            debugHeroObject = true;

            // if current state can be used, great. exit early.
            if (heroState != null && BoolConditionsMet(heroState.boolConditions) && IntConditionsMet(heroState.intConditions))
                return;

            // go through all states and get the first one that can be used. change to this state
            bool foundState = false;
            for (int i = 0; i < heroObject.states.states.Count; i++)
            {
                if (BoolConditionsMet(heroObject.states.states[i].boolConditions) && IntConditionsMet(heroObject.states.states[i].intConditions))
                {
                    ChangeState(i);
                    foundState = true;
                    break;
                }
            }

            // if no state found, disable all states
            if (!foundState)
            {
                HideAllStates();
            }
        }

        /// <summary>
        /// Play events that are of a specific type.
        /// </summary>
        /// <param name="eventType">Type of event to play. (ex. Loop, Autostart, etc)</param>
        /// <param name="useConditions">Should conditions on an event be used to determine whether or not the event can play?</param>
        /// <param name="messageID">ID attached to the message (if a messenger from another object called this method).</param>
        /// <param name="messenger">Game object that called this message (if there is one)</param>
        public void PlayEvents(int eventType, bool useConditions = true, int messageID = 0, GameObject messenger = null)
        {
            // exit early if there is no hero object
            if (heroState == null) return;

            // exit early if game is paused and this item should be paused
            if (HeroKitCommonRuntime.gameIsPaused && !doNotPause) return;

            // go through each event in the state
            for (int i = 0; i < heroState.heroEvent.Count; i++)
            {
                // go through each event that matches the event type passed in to this method
                if (heroState.heroEvent[i].eventType == eventType)
                {
                    PlayEvent(i, messenger, useConditions, eventType, messageID);
                }
            }
        }

        /// <summary>
        /// Play a specific event.
        /// </summary>
        /// <param name="eventID">The ID assigned to the event.</param>
        /// <param name="messenger">The game object which called this method (if there is one).</param>
        /// <param name="useConditions">Should conditions on an event be used to determine whether or not the event can play?</param>
        /// <param name="eventType">Type of event to play. (ex. Loop, Autostart, etc)</param>
        /// <param name="messageID">ID attached to the message (if a messenger from another object called this method).</param>
        public void PlayEvent(int eventID, GameObject messenger = null, bool useConditions = true, int eventType = 0, int messageID = 0)
        {
            // set ID for event in data struct (used in action scripts)
            heroStateData.eventBlock = eventID;

            // If a message was sent (trigger, collision, ui), get game object that sent the message.
            if (messenger != null)
                heroState.heroEvent[eventID].messenger = messenger;

            // most of the time, we only want to run an event if its conditions are met,
            // but sometimes we need to bypass this (after action that requires actions to pause like Wait Action) 
            if (useConditions)
            {
                // if this is a "key press" event and the right key hasn't been pressed, skip don't play this event.
                if (eventType == 5 && !IsKeyPressed()) return;

                // if this is a "message" event and the wrong message has been passed in, skip and don't play this event. 
                if (eventType == 6 && !UseMessage(messenger, messageID)) return;

                // if a condition hasn't been met, skip this event
                if (!BoolConditionsMet(heroState.heroEvent[eventID].boolConditions)) return;
                if (!IntConditionsMet(heroState.heroEvent[eventID].intConditions)) return;               
            }

            // execute the actions assigned to the event
            PlayActions(eventID);
        }

        /// <summary>
        /// Play actions in a specific event.
        /// </summary>
        /// <param name="eventID">The ID assigned to the event.</param>
        public void PlayActions(int eventID)
        {
            // exit early if this event is waiting for a long action to complete
            if (heroState.heroEvent[eventID].waiting) return;

            // get the action to start with
            int firstAction = heroState.heroEvent[eventID].currentPosition;

            // get the number of actions in the event
            int actionCount = heroState.heroEvent[eventID].actions.Count;

            // execute the actions assigned to the event
            for (int i = firstAction; i < actionCount; i++)
            {
                //if (debugHeroObject) Debug.Log("Event " + eventID + " | Action " + i);

                // set current position in event
                heroState.heroEvent[eventID].currentPosition = i;

                // set ID for action in data struct (used in action scripts)
                heroStateData.action = i;

                // if this action is disabled, or there is no template, skip this action
                if (!heroState.heroEvent[eventID].actions[i].active ||
                    heroState.heroEvent[eventID].actions[i].actionTemplate == null)
                {
                    ResetCurrentPosition(eventID, i); // check & reset next pos in event if necessary
                    continue;
                }

                // if this action has no scene script attached to it, show warning and skip this action
                if (heroState.heroEvent[eventID].actions[i].actionTemplate.action == null)
                {
                    Debug.LogError("The action script assigned to this action does not exist. Did you forget to add the script to the Action object?");
                    continue;
                }

                // get the name of the action
                string actionName = heroState.heroEvent[eventID].actions[i].actionTemplate.action.name;

                // get the action from the action dictionary
                currentAction = HeroKitDatabase.GetAction(actionName);

                // execute the action
                int goToActionID = ExecuteAction(eventID, i);

                // if we should break out of actions completely, do this here (state change, etc)
                if (goToActionID == -89) return;

                // if we should go to a specific action next, select it here
                if (goToActionID != -99) i = goToActionID;

                // reset next position to 0 if we are at end of event
                ResetCurrentPosition(eventID, i);

                // if this action was a long action, exit early
                if (heroState.heroEvent[eventID].waiting) return;
            }
        }

        /// <summary>
        /// Execute the action on this game object, another game object, or multiple game objects.
        /// </summary>
        /// <param name="eventID">ID assigned to the event in which this action resides.</param>
        /// <param name="actionID">ID assigned to this action.</param>
        /// <returns></returns>
        public int ExecuteAction(int eventID, int actionID)
        {
            // get the target game object ID on the action. if it's -1, use this.
            // if it's another number, you must loop through all game objects assigned to the game object ID
            // and use the action on those game objects.
            // if it's a list, only track the first game object in the list for long actions.
            // Long actions will likely need to be updated to handle this type of data.

            int goToActionID = -99;
            int depth = (heroState.heroEvent[eventID].actions[actionID].gameObjectFields != null) ? heroState.heroEvent[eventID].actions[actionID].gameObjectFields.Count : 0;

            // game object to target = this game object
            if (depth == 0)
            {
                goToActionID = currentAction.Execute(this);
            }
            // game object to target = a game object in a list on this hero object
            else if (depth != 0)
            {
                goToActionID = PlayOnListObject(eventID, actionID);
            }

            return goToActionID;
        }

        /// <summary>
        /// Play an action on one or more game objects attached to a slot in a hero object list.
        /// Hero object list resides on this game object.
        /// </summary>
        /// <param name="eventID">ID assigned to the event in which this action resides.</param>
        /// <param name="actionID">ID assigned to this action.</param>
        /// <returns></returns>
        public int PlayOnListObject(int eventID, int actionID)
        {
            int goToActionID = -99;
            int heroFieldCount = (heroState.heroEvent[eventID].actions[actionID].gameObjectFields != null) ? heroState.heroEvent[eventID].actions[actionID].gameObjectFields.Count : 0;
            int lastDepthLevel = heroFieldCount - 1;
            HeroKitObject targetHKO = this;
            HeroList list = targetHKO.heroList;

            for (int i = 0; i < heroFieldCount; i++)
            {
                List<int> gameObjectField = heroState.heroEvent[eventID].actions[actionID].gameObjectFields;
                if (gameObjectField != null)
                {
                    int objectID = gameObjectField[i];

                    // Action is embedded in a list within a list. Burrow down to the next level. Get the game object assigned to the next level.
                    if (i < lastDepthLevel)
                    {
                        targetHKO = GetObjectFromList(list, objectID);
                    }

                    // We've burrowed down to the final level. This level contains the list of game objects that the action should be used on.
                    // Play these actions.
                    else
                        goToActionID = PlayObjectInList(list, objectID);
                }
            }

            return goToActionID; 
        }

        //------------------------------------------------------------
        // Methods (Helper functions)
        //------------------------------------------------------------

        /// <summary>
        /// The game object that should receive an action can be embedded many levels deep inside a hero object list.
        /// Use this method to dig down into these levels to get this game object.
        /// The game object can exist in a game object slot in a list OR it can be obtained from its game object GUID.
        /// </summary>
        /// <param name="targetHeroList">The hero object list to traverse.</param>
        /// <param name="slotID">ID assigned to the slot in the hero object list.</param>
        /// <returns></returns>
        public HeroKitObject GetObjectFromList(HeroList targetHeroList, int slotID)
        {
            HeroKitObject newHKO = null;

            // id is a slot in a game object list
            if (slotID < 1000000)
            {
                // exit if there are no slots in the game object list
                if (targetHeroList.heroObjects.items == null || targetHeroList.heroObjects.items.Count == 0)
                {
                    if (debugHeroObject) Debug.LogWarning(HeroKitCommonRuntime.GetActionDebugInfo(this) + "There are no items in the hero list on " + name);
                    return null;
                }

                // exit if there are no game objects attached to a specific slot in the game object list
                if (targetHeroList.heroObjects.items[slotID].heroKitGameObjects == null || targetHeroList.heroObjects.items[slotID].heroKitGameObjects.Count == 0)
                {
                    if (debugHeroObject) Debug.LogWarning(HeroKitCommonRuntime.GetActionDebugInfo(this) + "There are no game objects attached to hero list slot 0 on " + name);
                    return null;
                }

                newHKO = targetHeroList.heroObjects.items[slotID].heroKitGameObjects[0];
            }

            // id is a game object GUID
            else if (slotID >= 1000000 && slotID < 9999999)
            {
                newHKO = HeroKitDatabase.GetHeroKitObject(slotID);
            }

            return newHKO;
        }

        /// <summary>
        /// If the ID passed in is a slot, get the slot from the hero object list, and play action on all game objects in that list.
        /// </summary>
        /// <param name="targetHeroList">The hero object list to traverse.</param>
        /// <param name="slotID">ID assigned to the slot in the hero object list.</param>
        /// <returns></returns>
        public int PlayObjectInList(HeroList targetHeroList, int slotID)
        {            
            int goToActionID = -99;

            // id is a slot in a game object list
            if (slotID < 1000000)
            {

                // exit early if there are no game objects attached to hero object field
                if (targetHeroList.heroObjects.items == null || targetHeroList.heroObjects.items.Count == 0)
                {
                    if (debugHeroObject) Debug.LogWarning(HeroKitCommonRuntime.GetActionDebugInfo(this) + "The target hero kit object (" + name + ") does not have any hero object fields.");
                    return goToActionID;
                }

                // get the game objects attached to the hero object field
                List<HeroKitObject> heroItemField = targetHeroList.heroObjects.items[slotID].heroKitGameObjects;

                // exit early if there are no game objects attached to hero object field
                if (heroItemField == null || heroItemField.Count == 0)
                {
                    if (debugHeroObject) Debug.LogWarning(HeroKitCommonRuntime.GetActionDebugInfo(this) + "The target hero kit object (" + name + ") does not have any game objects attached to it in hero object field " + slotID);
                    return goToActionID;
                }

                // play action on each game object in hero object field
                for (int i = 0; i < heroItemField.Count; i++)
                {
                    goToActionID = currentAction.Execute(heroItemField[i]);
                }
            }

            //// id is a game object GUID
            //else if (id >= 1000000 && id < 9999999)
            //{
            //    goToActionID = currentAction.Execute(HeroKitDatabase.GetHeroKitObject(id));
            //}

            return goToActionID;
        }

        /// <summary>
        /// Play actions that need more than one frame to complete, using FixedUpdate()
        /// </summary>
        /// <param name="list">The list of actions to execute.</param>
        public void PlayLongActions(List<IHeroKitAction> list)
        {
            // exit early if there is no hero kit object for the long actions
            if (list == null) return;

            // execute the actions assigned to the event
            for (int i = 0; i < list.Count; i++)
            {
                // execute the action
                list[i].Update();

                if (list[i].RemoveFromLongActions())
                {
                    // get data about the event
                    int eventID = list[i].eventID;
                    HeroKitObject hko = list[i].heroKitObject;

                    // remove action from long actions loop
                    list.RemoveAt(i);

                    // if event type is not a looping type, execute the rest of it.
                    // note: looping event will automatically update in the update loop.
                    int eventType = hko.heroState.heroEvent[eventID].eventType;

                    if (eventType != 0 && eventType != 1 && hko.heroState.heroEvent[eventID].currentPosition != 0)
                        hko.Continue(eventID);
                }
            }
        }

        /// <summary>
        /// Reset next position to 0 if we are at end of event.
        /// </summary>
        /// <param name="eventID">ID assigned to the event that contains the actions.</param>
        /// <param name="actionID">ID assigned to the current action.</param>
        public void ResetCurrentPosition(int eventID, int actionID)
        {
            heroState.heroEvent[eventID].currentPosition = actionID + 1;
            if (heroState.heroEvent[eventID].currentPosition >= (heroState.heroEvent[eventID].actions.Count))
                heroState.heroEvent[eventID].currentPosition = 0;
        }

        /// <summary>
        /// Get a component from this hero kit object or a child of this hero kit object. 
        /// First look in the dictionary to see if it exists.
        /// If this value is null, attempt to get the component from the game object.
        /// If the value does not exist on the game object, add it if desired.
        /// </summary>
        /// <typeparam name="T">Type of component that we want to get.</typeparam>
        /// <param name="key">The dictionary key for the component.</param>
        /// <param name="addComponent">Add the component if it doesn't exist.</param>
        /// <param name="forceCreate">If dictionary does not contain component, add it without attempting to get it from this hero kit object first.</param>
        /// <returns>The component on the hero kit object.</returns>
        public T GetHeroComponent<T>(string key, bool addComponent = false, bool forceCreate = false, bool showLog = true) where T : Component
        {
            // if component not in dictionary, add it
            if (!ComponentDictionary.ContainsKey(key))
            {
                T component = null;

                // get component that already exists if you don't to force-create one if the component is not in the dictionary
                if (!forceCreate)
                    component = GetComponent<T>();

                // if component doesn't exist, create it
                if (component == null && addComponent)
                    component = gameObject.AddComponent<T>();

                // exit early if the component does not exist on gameobject
                if (component == null)
                {
                    if (showLog) Debug.LogError(HeroKitCommonRuntime.NoComponentDebugInfo(key, this));
                    return null;
                }
                    
                // add the component to the dictionary
                ComponentDictionary.Add(key, component);
            }

            // get the component from the dictionary
            return (T)ComponentDictionary[key];
        }

        /// <summary>
        /// Gets a component from this hero kit object or a child of this hero kit object. 
        /// First it looks in the dictionary to see if it exists.
        /// If this value is null, we attempt to get the component from the gameobject.
        /// If the value does not exist on the gameobject, you can add it.
        /// </summary>
        /// <typeparam name="T">Type of component that we want to get.</typeparam>
        /// <param name="key">The dictionary key for the component.</param>
        /// <param name="childName">The name of the child game object that contains the component.</param>
        /// <param name="addComponent">Add the component if it doesn't exist.</param>
        /// <param name="forceCreate">If dictionary does not contain component, add it without attempting to get it from this hero kit object first.</param>
        /// <returns>The component on the hero kit object.</returns>
        public T GetHeroChildComponent<T>(string key, string childName, bool addComponent = false, bool forceCreate = false) where T : Component
        {
            key = childName + key;

            // if component not in dictionary, add it
            if (!ComponentDictionary.ContainsKey(key))
            {
                T component = null;

                // get component from a child of this gameobject
                Transform[] children = GetComponentsInChildren<Transform>(true);
                Transform childObject = null;
                for (int i = 0; i < children.Length; i++)
                {
                    if (children[i].name == childName)
                    {
                        childObject = children[i];

                        // get component that already exists if you don't to force-create one if the component is not in the dictionary
                        if (!forceCreate)
                            component = childObject.GetComponent<T>();

                        // if component doesn't exist, create it
                        if (component == null && addComponent)
                            component = childObject.gameObject.AddComponent<T>();
                    }
                }

                if (childObject == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoComponentOnChildDebugInfo(key, childName, this));
                    return null;
                }

                // exit early if the component does not exist on gameobject
                if (component == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoComponentDebugInfo(key, this));
                    return null;
                }

                // add the component to the dictionary
                ComponentDictionary.Add(key, component);
            }

            // get the component from the dictionary
            return (T)ComponentDictionary[key];
        }

        /// <summary>
        /// Gets a component from a specific game object. 
        /// First it looks in the dictionary to see if it exists.
        /// If this value is null, we attempt to get the component from the gameobject.
        /// If the value does not exist on the gameobject, you can add it.
        /// </summary>
        /// <remarks>The reference to the external component exists on this hero kit object.</remarks>
        /// <typeparam name="T">Type of component that we want to get.</typeparam>
        /// <param name="key">The dictionary key for the component.</param>
        /// <param name="addComponent">Add the component if it doesn't exist.</param>
        /// <param name="targetGameObject">The game object that contains the component.</param>
        /// <returns>The component on the game object.</returns>
        public T GetGameObjectComponent<T>(string key, bool addComponent, GameObject targetGameObject) where T : Component
        {
            if (targetGameObject == null) return null;

            // give key a unique identifier
            key = targetGameObject.GetInstanceID() + key;

            // if component not in dictionary, add it
            if (!ComponentDictionary.ContainsKey(key))
            {
                // get the component
                T component = targetGameObject.GetComponent<T>();
                if (component == null && addComponent) component = targetGameObject.AddComponent<T>();

                // exit early if the component does not exist on gameobject
                if (component == null)
                    return null;

                // add the component to the dictionary
                ComponentDictionary.Add(key, component);
            }

            // get the component from the dictionary
            return (T)ComponentDictionary[key];
        }

        /// <summary>
        /// Delete a component from this hero kit object and from the dictionary.
        /// </summary>
        /// <typeparam name="T">Type of component that we want to delete.</typeparam>
        /// <param name="key">The dictionary key for the component.</param>
        public void DeleteHeroComponent<T>(string key) where T : Component
        {
            // get the component from the game object
            T component = GetHeroComponent<T>(key);

            // remove component from game object if it exists on game object
            if (component != null)
            {
                Destroy(component);
            }

            // remove component from dictionary if it exsits in dictionary
            if (ComponentDictionary.ContainsKey(key))
            {
                ComponentDictionary.Remove(key);
            }
        }

        /// <summary>
        /// Has a key been pressed? 
        /// This is used when "When to Run"  > "Receive Input" is selected on the event's page. 
        /// </summary>
        /// <returns>Has a key been pressed.</returns>
        private bool IsKeyPressed()
        {
            bool playEvent = false;
            int eventID = heroStateData.eventBlock;

            // go through each array. if one returns true, exit early.
            for (int listID = 0; listID < heroState.heroEvent[eventID].inputConditions.Count; listID++)
            {
                int counter = 0;

                for (int condID = 0; condID < heroState.heroEvent[eventID].inputConditions[listID].items.Count; condID++)
                {
                    counter += (GetInputStatus(listID, condID)) ? 1 : 0;
                }

                if (counter == heroState.heroEvent[eventID].inputConditions[listID].items.Count)
                {
                    playEvent = true;
                }
            }

            return playEvent;
        }

        /// <summary>
        /// Has a an action key been pressed?
        /// An action key is tied to the "When to Run" > "Another object interacts with this object" > 
        /// "Other object interacts with this object" drop-down lists on the event's page.
        /// One of the keys in the method below must be pressed for the interaction event to occur.  
        /// </summary>
        /// <returns>Has an action key been pressed.</returns>
        private bool IsActionKeyPressed()
        {
            bool result = (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0));
            return result;
        }

        /// <summary>
        /// Is this hero kit object facing a target object?
        /// An action key is tied to the "When to Run" > "Another object interacts with this object" > 
        /// "Other object interacts with this object" drop-down lists on the event's page.
        /// </summary>
        /// <param name="targetGO">The target game object.</param>
        /// <returns>The hero kit object is facing the target object.</returns>
        private bool IsFacingTarget(GameObject targetGO)
        {
            bool result = false;

            float dot = Vector3.Dot(targetGO.transform.forward, (transform.localPosition - targetGO.transform.localPosition).normalized);
            if (dot > 0.7f)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Should an event that requires a message run?
        /// If yes, attach the source hero kit object and
        /// let the event loop know it's okay to run this.
        /// </summary>
        /// <param name="messenger">The game object that sent the message.</param>
        /// <param name="messageID">The ID assigned to the message.</param>
        /// <returns>Event can execute.</returns>
        private bool UseMessage(GameObject messenger, int messageID)
        {
            // exit early if messenger is not available
            if (messenger == null) return false;

            bool result = false;
            int eventID = heroStateData.eventBlock;

            // make sure we have correct tag
            if (TagIsCorrect(messenger))
            {
                // message IDs
                // 1 = OnTriggerEnter
                // 2 = OnTriggerExit
                // 3 = OnTriggerStay
                // 4 = OnCollisionEnter
                // 5 = OnCollisionExit
                // 6 = OnCollisionStay

                // interaction type
                // 1 = interact collision
                // 2 = interact trigger
                // 3 = touch collision
                // 4 = touch trigger
                // 5 = exit collision
                // 6 = exit trigger
                int interactType = heroState.heroEvent[eventID].messageSettings[2]; // 1=interact collision, 2=interact trigger


                //-----------------------------------------------
                // interact with object
                //-----------------------------------------------

                // interact with an object we are colliding with
                if (interactType == 1)
                {
                    // if one of the action keys is being pressed, interact with the object
                    if (actionKeyPressed)
                    {
                        // make sure the object that wants to interact with this object is facing this object
                        if (IsFacingTarget(messenger))
                        {
                            // on collision stay
                            if (messageID == 6)
                            {
                                result = true;
                            }
                        }   
                    }
                }

                // interact with an object if we are inside its trigger area
                if (interactType == 2)
                {
                    // if one of the action keys is being pressed, interact with the object
                    if (actionKeyPressed)
                    {
                        // make sure the object that wants to interact with this object is facing this object
                        if (IsFacingTarget(messenger))
                        {
                            // on trigger stay
                            if (messageID == 3)
                                result = true;
                        }
                    }
                }

                //-----------------------------------------------
                // touch object
                //-----------------------------------------------

                // how often the event should run
                // 1 = run once
                // 2 = loop
                int runType = heroState.heroEvent[eventID].messageSettings[3];

                // touch object with collision
                if (interactType == 3)
                {
                    // run once on collision enter
                    if (runType == 1)
                    {
                        // on collision enter
                        if (messageID == 4)
                            result = true;
                    }

                    // loop on collision stay
                    else if (runType == 2)
                    {
                        // on collision stay
                        if (messageID == 6)
                            result = true;
                    }
                }

                // touch object with trigger
                else if (interactType == 4)
                {
                    // run once on trigger enter
                    if (runType == 1)
                    {
                        // on trigger enter
                        if (messageID == 1)
                            result = true;
                    }

                    // loop on trigger stay
                    else if (runType == 2)
                    {
                        // on trigger stay
                        if (messageID == 3)
                            result = true;
                    }
                }

                //-----------------------------------------------
                // leave object
                //-----------------------------------------------

                // leave object with collision
                if (interactType == 5)
                {
                    // on collision exit
                    if (messageID == 5)
                        result = true;
                }

                // leave object with trigger
                else if (interactType == 6)
                {
                    // on trigger exit
                    if (messageID == 2)
                        result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Make sure the message is coming from an object with the correct tag.
        /// </summary>
        /// <param name="messenger">The game object that sent the message.</param>
        /// <returns>The messenger has the correct tag.</returns>
        private bool TagIsCorrect(GameObject messenger)
        {
            bool result = false;
            int eventID = heroStateData.eventBlock;
            int tagType = heroState.heroEvent[eventID].messageSettings[0];

            switch (tagType)
            {
                case 1: // player tag
                    result = (messenger.tag == "Player");
                    break;
                case 2:  // not player tag
                    result = (messenger.tag != "Player");
                    break;
                case 3: // any tag is OK
                    result = true;
                    break;
                case 4: // specific tag
                    result = (messenger.tag == heroState.heroEvent[eventID].messageTag);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Check to see if all bool conditions for an event were met.
        /// </summary>
        /// <param name="boolConditions">The bool conditions to test.</param>
        /// <returns>Bool conditions were met.</returns>
        private bool BoolConditionsMet(List<ConditionBoolFields> boolConditions)
        {
            // exit early of there are no bool conditions
            if (boolConditions == null || boolConditions.Count == 0) return true;

            // make sure each result is true. if something is false, exit early
            for (int i = 0; i < boolConditions.Count; i++)
            {
                int comparison = DropDownListValue.GetValueEvent(this, boolConditions[i]);
                bool value1 = BoolFieldValue.GetValueEvent(this, boolConditions[i].itemA);
                bool value2 = BoolFieldValue.GetValueEvent(this, boolConditions[i].itemB);
                bool evaluation = HeroActionCommonRuntime.CompareBools(comparison, value1, value2);
                if (!evaluation) return false;
            }

            return true;
        }

        /// <summary>
        /// Check to see if all integer conditions for an event were met.
        /// </summary>
        /// <param name="intConditions">The integer conditions to test.</param>
        /// <returns>Integer conditions were met.</returns>
        private bool IntConditionsMet(List<ConditionIntFields> intConditions)
        {
            // exit early of there are no bool conditions
            if (intConditions == null || intConditions.Count == 0) return true;

            // make sure each result is true. if something is false, exit early
            for (int i = 0; i < intConditions.Count; i++)
            {
                int comparison = DropDownListValue.GetValueEvent(this, intConditions[i]);
                int value1 = IntegerFieldValue.GetValueEvent(this, intConditions[i].itemA);
                int value2 = IntegerFieldValue.GetValueEvent(this, intConditions[i].itemB);
                bool evaluation = HeroActionCommonRuntime.CompareIntegers(comparison, value1, value2);
                if (!evaluation) return false;
            }

            return true;
        }

        /// <summary>
        /// For the sake of performance, we only want to watch for events and messages that we know will 
        /// be used by an event in the active state. Return the messages to watch.
        /// </summary>
        /// <param name="state">The state that contains the events.</param>
        /// <returns>Events that should be watched and ignored.</returns>
        private bool[] WatchEvents(HeroState state)
        {
            bool[] eventID = new bool[9];
            for (int i = 0; i < heroState.heroEvent.Count; i++)
            {
                // loop event
                switch (state.heroEvent[i].eventType)
                {
                    case 1:
                        eventID[0] = true;
                        break;
                    case 2:
                        eventID[1] = true;
                        break;
                    case 5:
                        eventID[2] = true;
                        break;
                    case 6:
                        // only enable message listeners that might be used
                        if (state.heroEvent[i].messageSettings[0] != 0)
                        {
                            int interactType = heroState.heroEvent[i].messageSettings[2];

                            switch (interactType)
                            {
                                case 1: // interact with an object we are colliding with
                                    eventID[8] = true;
                                    break;
                                case 2: // interact with an object if we are inside its trigger area
                                    eventID[5] = true;
                                    break;
                                case 3: // touch object with collision
                                    eventID[6] = true;
                                    eventID[8] = true;
                                    break;
                                case 4: // touch object with trigger
                                    eventID[3] = true;
                                    eventID[5] = true;
                                    break;
                                case 5: // leave object with collision
                                    eventID[7] = true;
                                    break;
                                case 6: // leave object with trigger
                                    eventID[4] = true;
                                    break;
                            }
                        }
                        break;
                }
            }

            return eventID;
        }

        //------------------------------------------------------------
        // Methods (Input functions)
        //------------------------------------------------------------

        /// <summary>
        /// Time between clicks.
        /// </summary>
        private float clickTime;
        /// <summary>
        /// Click count (1=single-click, 2=double-click, etc)
        /// </summary>
        private int clickCount = 0;
        /// <summary>
        /// If this interval passes between clicks, the click count is reset.
        /// </summary>
        private float clickInterval = 0.25f;
        /// <summary>
        /// The current position of the mouse on the screen.
        /// </summary>
        private Vector3 mousePos = new Vector3();
        /// <summary>
        /// The position of the mouse on the screen during the last frame.
        /// </summary>
        private Vector3 mousePosLastFrame = new Vector3();

        /// <summary>
        /// Get the status of an input key (active, inactive)
        /// </summary>
        /// <param name="listID">ID assigned to the input list.</param>
        /// <param name="condID">ID assigned to the condition in the input list.</param>
        /// <returns>The status of an input key (true if active, false if inactive).</returns>
        private bool GetInputStatus(int listID, int condID)
        {
            bool status = false;
            int keyPressType = 0;
            KeyCode key = KeyCode.Mouse6;

            // get ID assigned to input type
            int keyType = heroState.heroEvent[heroStateData.eventBlock].inputConditions[listID].items[condID].inputType;

            // get ID assigned to key code
            int keyID = heroState.heroEvent[heroStateData.eventBlock].inputConditions[listID].items[condID].key;

            // get the key
            switch (keyType)
            {
                case 1: // mouse
                    key = HeroKitDatabase.MouseKeyDictionary[keyID];
                    break;
                case 2: // keyboard
                    key = HeroKitDatabase.KeyboardKeyDictionary[keyID];
                    break;
                case 3: // joystick
                    key = HeroKitDatabase.JoystickKeyDictionary[keyID];
                    break;
                case 4: // touch
                    key = HeroKitDatabase.MouseKeyDictionary[keyID];
                    break;
            }

            // get the type of key press we are looking for
            keyPressType = heroState.heroEvent[heroStateData.eventBlock].inputConditions[listID].items[condID].pressType;

            // check to see if player is pressing specific keys and how 
            // (1=begin press, 2=pressing, 3=end press, 4=double press, 5=dragging)
            if (keyPressType == 4 && Input.GetKeyDown(key))
            {
                status = OnClickType(keyPressType);
            }
            else if (keyPressType == 1 && Input.GetKeyDown(key))
            {
                status = true;
            }
            else if (keyPressType == 2 && Input.GetKey(key) || keyPressType == 3 && Input.GetKeyUp(key))
            {
                status = true;
            }
            else if (keyPressType == 5 && Input.GetKey(key))
            {
                mousePos = Input.mousePosition;

                if (mousePos != mousePosLastFrame)
                {
                    status = true;
                }

                mousePosLastFrame = mousePos;
            }

            return status;
        }

        /// <summary>
        /// On click or double click.
        /// </summary>
        /// <param name="data">The pointer event data.</param>
        public bool OnClickType(int pressType)
        {
            bool status = false;

            // get interval between this click and the previous one (check for double click)
            float interval = Time.realtimeSinceStartup - clickTime;

            // if this is double click, change click count
            if (interval < clickInterval && interval > 0 && clickCount != 2)
                clickCount = 2;
            else
                clickCount = 1;

            // reset click time
            clickTime = Time.realtimeSinceStartup;

            // clicked on item
            //if (pressType == 1 && clickCount == 1)
            //{
            //    status = true;
            //}

            // double-clicked item
            if (pressType == 4 && clickCount == 2)
            {
                status = true;
            }

            return status;
        }
    }

    /// <summary>
    /// Container that holds current state of the hero object.
    /// </summary>
    public struct HeroStateData
    {
        /// <summary>
        /// The ID assigned to the current state of a hero kit object.
        /// </summary>
        public int state;

        /// <summary>
        /// The ID assigned to the current event in focus on a hero kit object.
        /// </summary>
        public int eventBlock;

        /// <summary>
        /// The ID assigned to the current action in focus on a hero kit object.
        /// </summary>
        public int action;
    }
}
