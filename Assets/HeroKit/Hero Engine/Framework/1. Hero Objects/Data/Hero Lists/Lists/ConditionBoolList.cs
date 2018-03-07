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
    /// A Hero List for conditional bools. 
    /// This list stores bools that determine whether an event or state can be used.
    /// This list inherits the values in the HeroListObject template.
    /// </summary>
    [System.Serializable]
    public class ConditionBoolList : HeroListObject
    {
        /// <summary>
        /// The list of conditional bool fields on a hero object.
        /// </summary>
        public List<ConditionBoolFields> items = new List<ConditionBoolFields>();

        /// <summary>
        /// Clone the list, remove references.
        /// </summary>
        /// <param name="list">The conditional bool field list we are cloning.</param>
        /// <returns>The cloned conditional bool field list.</returns>
        public ConditionBoolList Clone(ConditionBoolList list)
        {
            ConditionBoolList tempList = new ConditionBoolList();
            List<ConditionBoolFields> tempItems = new List<ConditionBoolFields>();
            tempItems = (list == null || list.items == null) ? new List<ConditionBoolFields>() : new List<ConditionBoolFields>(list.items.Select(x => x.Clone(x)));
            tempList.items = tempItems;
            return tempList;
        }
    }

    /// <summary>
    /// A Hero List field for a conditional bool.
    /// This field is used to store a bool that you can use in a hero object state or event.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class ConditionBoolFields
    {
        /// <summary>
        /// First conditional bool.
        /// </summary>
        public ConditionBoolField itemA = new ConditionBoolField();
        /// <summary>
        /// The operator to compare the first and second conditional bools.
        /// </summary>
        public int operatorID;
        /// <summary>
        /// Second conditional bool.
        /// </summary>
        public ConditionBoolField itemB = new ConditionBoolField();

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConditionBoolFields() { }

        /// <summary>
        /// Clone the conditional bool field, remove references.
        /// </summary>
        /// <param name="field">The conditional bool field to clone.</param>
        /// <returns>The cloned conditional bool field.</returns>
        public ConditionBoolFields Clone(ConditionBoolFields field)
        {
            ConditionBoolFields temp = new ConditionBoolFields();
            temp.itemA = field.itemA.Clone(field.itemA);
            temp.operatorID = field.operatorID;
            temp.itemB = field.itemB.Clone(field.itemB);
            return temp;
        }
    }

    /// <summary>
    /// A Hero List field for a conditional bool.
    /// This field stores a conditinal bool that determines whether an event can run.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class ConditionBoolField : HeroListObjectField<int>
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The type of object that contains the value we want to compare (ex. this object, object in list, object in scene).
        /// </summary>
        public int objectType;
        /// <summary>
        /// The ID assigned to the object if it exists in a list (ex. variable, property). 
        /// </summary>
        public int objectID;
        /// <summary>
        /// The name assigned to the object if it exists in a list (ex. variable, property).
        /// </summary>
        public string objectName;
        /// <summary>
        /// The GUID of the hero kit object.
        /// </summary>
        /// <remarks>This is used if we are getting an object in the scene.</remarks>
        public int heroGUID;
        /// <summary>
        /// The ID of the hero property.
        /// </summary>
        /// <remarks>This is used if we are getting a value from a hero property.</remarks>
        public int propertyID;
        /// <summary>
        /// The hero object to work with.
        /// </summary>
        public HeroObject targetHeroObject;
        /// <summary>
        /// The type of hero object that we want to work with.
        /// </summary>
        public HeroObject heroObject;
        /// <summary>
        /// The type of value to work with (ex. value, variable, property).
        /// </summary>
        public int fieldType;
        /// <summary>
        /// ID assigned to the bool field in the bool list.
        /// </summary>
        public int fieldID;
        /// <summary>
        /// The value in the bool field.
        /// </summary>
        public bool fieldValue;

        // --------------------------------------------------------------
        // Constructors
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConditionBoolField() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="field">The conditional bool field to construct.</param>
        public ConditionBoolField(ConditionBoolField field)
        {
            name = field.name;
            value = field.value;
            objectType = field.objectType;
            objectID = field.objectID;
            objectName = field.objectName;
            heroGUID = field.heroGUID;
            propertyID = field.propertyID;
            targetHeroObject = field.targetHeroObject;
            fieldType = field.fieldType;
            fieldID = field.fieldID;
            fieldValue = field.fieldValue;
            heroObject = field.heroObject;
        }

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Clone the condition bool field, remove references.
        /// </summary>
        /// <param name="field">The condition bool field to clone.</param>
        /// <returns>The cloned condition bool field.</returns>
        public ConditionBoolField Clone(ConditionBoolField field)
        {
            ConditionBoolField temp = new ConditionBoolField();
            temp.name = field.name;
            temp.value = field.value;
            temp.objectType = field.objectType;
            temp.objectID = field.objectID;
            temp.objectName = field.objectName;
            temp.heroGUID = field.heroGUID;
            temp.propertyID = field.propertyID;
            temp.targetHeroObject = field.targetHeroObject;
            temp.fieldType = field.fieldType;
            temp.fieldID = field.fieldID;          
            temp.fieldValue = field.fieldValue;
            temp.heroObject = field.heroObject;
            return temp;
        }
    }
}