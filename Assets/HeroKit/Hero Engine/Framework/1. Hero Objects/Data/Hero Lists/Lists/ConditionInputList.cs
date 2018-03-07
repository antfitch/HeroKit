// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;

namespace HeroKit.Scene
{
    /// <summary>
    /// A Hero List for conditional input. 
    /// This list stores input that determine whether an event can run.
    /// This list inherits the values in the HeroListObject template.
    /// </summary>
    [System.Serializable]
    public class ConditionInputList : HeroListObject
    {
        /// <summary>
        /// The list of conditional input fields on a hero object.
        /// </summary>
        public List<ConditionInputField> items = new List<ConditionInputField>();

        /// <summary>
        /// Clone the list, remove references.
        /// </summary>
        /// <param name="list">The conditional input field list we are cloning.</param>
        /// <returns>The cloned conditional input field list.</returns>
        public ConditionInputList Clone(ConditionInputList oldList)
        {
            ConditionInputList tempList = new ConditionInputList();
            List<ConditionInputField> tempItems = new List<ConditionInputField>();
            tempItems = (oldList == null || oldList.items == null) ? new List<ConditionInputField>() : new List<ConditionInputField>(oldList.items.Select(x => x.Clone(x)));
            tempList.items = tempItems;
            return tempList;
        }
    }

    /// <summary>
    /// A Hero List field for a conditional input field.
    /// This field stores a conditinal input that determines whether an event can run.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class ConditionInputField : HeroListObjectField<int>
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The type of user input to monitor (ex. mouse, keyboard)
        /// </summary>
        public int inputType;
        /// <summary>
        /// The action that was performed with the input type (ex. on press down, left-click, right-click)
        /// </summary>
        public int pressType;
        /// <summary>
        /// The key that was pressed. (only used if input type supports keys)
        /// </summary>
        public int key;

        // --------------------------------------------------------------
        // Constructors
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConditionInputField() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="field">The conditional input field to construct.</param>
        public ConditionInputField(ConditionInputField field)
        {
            name = field.name;
            value = field.value;
            inputType = field.inputType;
            pressType = field.pressType;
            key = field.key;
        }

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Clone the conditional input field, remove references.
        /// </summary>
        /// <param name="field">The conditional input field to clone.</param>
        /// <returns>The cloned conditional input field.</returns>
        public ConditionInputField Clone(ConditionInputField field)
        {
            ConditionInputField temp = new ConditionInputField();
            temp.name = field.name;
            temp.value = field.value;
            temp.inputType = field.inputType;
            temp.pressType = field.pressType;
            temp.key = field.key;
            return temp;
        }
    }
}