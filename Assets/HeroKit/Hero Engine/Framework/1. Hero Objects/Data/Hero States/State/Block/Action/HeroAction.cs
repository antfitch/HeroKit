// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;

namespace HeroKit.Scene
{
    /// <summary>
    /// An action represents a specific action in an event. 
    /// Actions are performed in a sequence (first to last).
    /// </summary>   
    [System.Serializable]
    public class HeroAction
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// A custom name for the action.
        /// </summary>
        /// <remarks>This change appears in the editor only. Changing this name won't re-name a hero action in the Project directory.</remarks>
        public string name = "";
        /// <summary>
        /// Is this action visible in the editor?
        /// </summary>
        public bool visible;
        /// <summary>
        /// Should this action execute when the game runs?
        /// </summary>
        public bool active = true;
        /// <summary>
        /// Indent level. This is used in the HeroKitEditor's Hero Object menu. It is used for actions in conditional statements.
        /// </summary>
        public int indent = 0;
        /// <summary>
        /// The hero action object.
        /// </summary>
        public HeroKitAction actionTemplate;
        /// <summary>
        /// A list of hero action fields that are used by this action.
        /// </summary>
        public List<HeroActionField> actionFields = new List<HeroActionField>();
        /// <summary>
        /// A nested list of game objects. 
        /// </summary>
        /// <remarks>This is used to get a game object from a hero object referenced by another hero object.
        /// It's used when we need to get a value in a variable list that exists on another hero kit object.</remarks>
        public List<int> gameObjectFields = new List<int>();

        // --------------------------------------------------------------
        // Constructors
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeroAction() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="list">The hero action to construct.</param>
        public HeroAction(HeroAction field)
        {
            name = field.name;
            visible = field.visible;
            active = field.active;
            indent = field.indent;
            actionTemplate = field.actionTemplate; 
            actionFields = new List<HeroActionField>(field.actionFields.Select(x => x.Clone(x)));
            gameObjectFields = new List<int>();
        }

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Clone the hero action, remove references.
        /// </summary>
        /// <param name="field">The hero action to clone.</param>
        /// <returns>The cloned hero action.</returns>
        public HeroAction Clone(HeroAction field)
        {
            HeroAction temp = new HeroAction();
            temp.name = field.name;
            temp.visible = field.visible;
            temp.active = field.active;
            temp.indent = field.indent;
            temp.actionTemplate = field.actionTemplate; 
            temp.actionFields = new List<HeroActionField>(field.actionFields.Select(x => x.Clone(x)));
            temp.gameObjectFields = new List<int>();
            return temp;
        }
    }
}