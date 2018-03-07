// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace HeroKit.Scene
{
    /// <summary>
    /// An Event represents a block of actions assigned to a state. 
    /// Multiple events can be active at a time.
    /// A event contains:
    /// - Conditions (when and how often the event can execute)
    /// - Actions
    /// </summary>
    [System.Serializable]
    public class HeroEvent
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The name of the event.
        /// </summary>
        public string name = "";
        /// <summary>
        /// Is this event visible in the editor?
        /// </summary>
        public bool visible;
        /// <summary>
        /// A list of hero actions assigned to the event.
        /// </summary>
        public List<HeroAction> actions = new List<HeroAction>();
        /// <summary>
        /// The type of event. (ex. loop, on mouse event, etc)
        /// </summary>
        public int eventType; 
        /// <summary>
        /// The settings for a message. A message can have up to 4 settings. Each setting is represented by an integer. 
        /// (ex. event can only run if it's hero kit object is touching another object with the Player tag on the Player layer)
        /// </summary>
        public int[] messageSettings = new int[4];
        /// <summary>
        /// The name of a tag. This can be used by a message that needs to reference a tag.
        /// </summary>
        public string messageTag = "";
        /// <summary>
        /// The game object that sent a message to this event.
        /// </summary>
        public GameObject messenger;
        /// <summary>
        /// This determines which action is executing in the event at runtime.
        /// </summary>
        public int currentPosition;
        /// <summary>
        /// This is enabled if current action requires multiple updates to complete.
        /// </summary>
        public bool waiting; 
        /// <summary>
        /// The integer conditions that must be true for this event to execute.
        /// </summary>
        public List<ConditionIntFields> intConditions = new List<ConditionIntFields>();
        /// <summary>
        /// The bool conditions that must be true for this event to execute.
        /// </summary>
        public List<ConditionBoolFields> boolConditions = new List<ConditionBoolFields>();
        /// <summary>
        /// The input conditions that must be true for this event to execute.
        /// </summary>
        public List<ConditionInputList> inputConditions = new List<ConditionInputList>();

        // --------------------------------------------------------------
        // Constructors
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeroEvent() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="list">The hero event to construct.</param>
        public HeroEvent(HeroEvent field)
        {
            name = field.name;
            visible = field.visible;
            actions = new List<HeroAction>(field.actions.Select(x => x.Clone(x)));
            eventType = field.eventType;
            messageSettings = field.messageSettings;
            messageTag = field.messageTag;
            messenger = field.messenger;
            currentPosition = field.currentPosition;
            waiting = field.waiting;
            intConditions = new List<ConditionIntFields>(field.intConditions.Select(x => x.Clone(x)));
            boolConditions = new List<ConditionBoolFields>(field.boolConditions.Select(x => x.Clone(x)));
            inputConditions = new List<ConditionInputList>(field.inputConditions.Select(x => x.Clone(x)));
        }

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Clone the hero event, remove references.
        /// </summary>
        /// <param name="field">The hero event to clone.</param>
        /// <returns>The cloned hero event.</returns>
        public HeroEvent Clone(HeroEvent field)
        {
            HeroEvent temp = new HeroEvent();
            temp.name = field.name;
            temp.visible = field.visible;
            temp.actions = new List<HeroAction>(field.actions.Select(x => x.Clone(x)));
            temp.eventType = field.eventType;
            temp.messageSettings = field.messageSettings;
            temp.messageTag = field.messageTag;
            temp.messenger = field.messenger;
            temp.currentPosition = field.currentPosition;
            temp.waiting = field.waiting;
            temp.intConditions = new List<ConditionIntFields>(field.intConditions.Select(x => x.Clone(x)));
            temp.boolConditions = new List<ConditionBoolFields>(field.boolConditions.Select(x => x.Clone(x)));
            temp.inputConditions = new List<ConditionInputList>(field.inputConditions.Select(x => x.Clone(x)));
            return temp;
        }

    }
}