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
    /// A Hero List for conditional ints. 
    /// This list stores ints that determine whether an event or state can be used.
    /// This list inherits the values in the HeroListObject template.
    /// </summary>
    [System.Serializable]
    public class ConditionIntList : HeroListObject
    {
        /// <summary>
        /// The list of conditional int fields on a hero object.
        /// </summary>
        public List<ConditionIntFields> items = new List<ConditionIntFields>();

        /// <summary>
        /// Clone the list, remove references.
        /// </summary>
        /// <param name="list">The conditional int field list we are cloning.</param>
        /// <returns>The cloned conditional int field list.</returns>
        public ConditionIntList Clone(ConditionIntList oldList)
        {
            ConditionIntList tempList = new ConditionIntList();
            List<ConditionIntFields> tempItems = new List<ConditionIntFields>();
            tempItems = (oldList == null || oldList.items == null) ? new List<ConditionIntFields>() : new List<ConditionIntFields>(oldList.items.Select(x => x.Clone(x)));
            tempList.items = tempItems;
            return tempList;
        }
    }

    /// <summary>
    /// A Hero List field for a conditional int.
    /// This field is used to store an int that you can use in a hero object state or event.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class ConditionIntFields
    {
        /// <summary>
        /// First conditional int.
        /// </summary>
        public ConditionIntField itemA = new ConditionIntField();
        /// <summary>
        /// The operator to compare the first and second conditional ints.
        /// </summary>
        public int operatorID;
        /// <summary>
        /// Second conditional int.
        /// </summary>
        public ConditionIntField itemB = new ConditionIntField();

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConditionIntFields() { }

        /// <summary>
        /// Clone the conditional int field, remove references.
        /// </summary>
        /// <param name="field">The conditional int field to clone.</param>
        /// <returns>The cloned conditional int field.</returns>
        public ConditionIntFields Clone(ConditionIntFields field)
        {
            ConditionIntFields temp = new ConditionIntFields();
            temp.itemA = field.itemA.Clone(field.itemA);
            temp.operatorID = field.operatorID;
            temp.itemB = field.itemB.Clone(field.itemB);
            return temp;
        }
    }

    /// <summary>
    /// A Hero List field for a conditional integer.
    /// This field stores a conditinal integer that determines whether an event can run.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class ConditionIntField : HeroListObjectField<int>
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
        /// <remarks>This is used if we are getting an hero property from a list.</remarks>
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
        /// ID assigned to the int field in the bool list.
        /// </summary>
        public int fieldID;
        /// <summary>
        /// The value in the int field.
        /// </summary>
        public int fieldValue;

        // --------------------------------------------------------------
        // Constructors
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConditionIntField() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="field">The conditional int field to construct.</param>
        public ConditionIntField(ConditionIntField field)
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
        /// Clone the condition int field, remove references.
        /// </summary>
        /// <param name="field">The condition int field to clone.</param>
        /// <returns>The cloned condition int field.</returns>
        public ConditionIntField Clone(ConditionIntField field)
        {
            ConditionIntField temp = new ConditionIntField();
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