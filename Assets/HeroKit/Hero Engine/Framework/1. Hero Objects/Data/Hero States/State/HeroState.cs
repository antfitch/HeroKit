// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;

namespace HeroKit.Scene
{
    /// <summary>
    /// A State represents a specific state of a hero object. 
    /// Only one state can be active at a time.
    /// A state contains:
    /// - Events
    /// - Conditions
    /// - Visuals
    /// </summary>
    [System.Serializable]
    public class HeroState
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The name of the state.
        /// </summary>
        public string name = "";
        /// <summary>
        /// Is this state visible in the editor?
        /// </summary>
        public bool visible;
        /// <summary>
        /// A list of hero events assigned to the state.
        /// </summary>
        public List<HeroEvent> heroEvent = new List<HeroEvent>();
        /// <summary>
        /// The visuals assigned to this state.
        /// </summary>
        public HeroVisuals heroVisuals = new HeroVisuals();
        /// <summary>
        /// The integer conditions that must be true for this state to execute.
        /// </summary>
        public List<ConditionIntFields> intConditions = new List<ConditionIntFields>();
        /// <summary>
        /// The bool conditions that must be true for this state to execute.
        /// </summary>
        public List<ConditionBoolFields> boolConditions = new List<ConditionBoolFields>();

        // --------------------------------------------------------------
        // Constructors
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeroState() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="list">The hero state to construct.</param>
        public HeroState(HeroState field)
        {
            name = field.name;
            visible = field.visible;
            heroEvent = new List<HeroEvent>(field.heroEvent.Select(x => x.Clone(x)));
            heroVisuals = field.heroVisuals.Clone(field.heroVisuals);
            intConditions = new List<ConditionIntFields>(field.intConditions.Select(x => x.Clone(x)));
            boolConditions = new List<ConditionBoolFields>(field.boolConditions.Select(x => x.Clone(x)));
        }

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Clone the hero state, remove references.
        /// </summary>
        /// <param name="field">The hero state to clone.</param>
        /// <returns>The cloned hero state.</returns>
        public HeroState Clone(HeroState field)
        {
            HeroState temp = new HeroState();
            temp.name = field.name;
            temp.visible = field.visible;
            temp.heroEvent = new List<HeroEvent>(field.heroEvent.Select(x => x.Clone(x)));
            temp.heroVisuals = field.heroVisuals.Clone(field.heroVisuals);
            temp.intConditions = new List<ConditionIntFields>(field.intConditions.Select(x => x.Clone(x)));
            temp.boolConditions = new List<ConditionBoolFields>(field.boolConditions.Select(x => x.Clone(x)));
            return temp;
        }
    }
}