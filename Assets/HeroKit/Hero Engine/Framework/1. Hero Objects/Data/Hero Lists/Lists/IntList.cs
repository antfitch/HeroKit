// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using HeroKit.Editor;

namespace HeroKit.Scene
{
    /// <summary>
    /// A Hero List for integers. 
    /// This list stores integers that you can use in a hero object action.
    /// This list inherits the values in the HeroListObject template.
    /// </summary>
    [System.Serializable]
    public class IntList : HeroListObject
    {
        /// <summary>
        /// The list of int fields on a hero object.
        /// </summary>
        public List<IntField> items = new List<IntField>();

        /// <summary>
        /// Clone the list, remove references.
        /// </summary>
        /// <param name="list">The int field list we are cloning.</param>
        /// <returns>The cloned int field list.</returns>
        public IntList Clone(IntList oldList)
        {
            IntList tempList = new IntList(); 
            List<IntField> tempItems = (oldList == null || oldList.items == null) ? new List<IntField>() : new List<IntField>(oldList.items.Select(x => x.Clone(x)));
            tempList.items = tempItems;
            return tempList;
        }

        /// <summary>
        /// Save the int fields in a save game file.
        /// </summary>
        /// <returns>An array that contains the int in the int field list.</returns>
        public int[] Save()
        {
            if (items == null) return null;

            int[] saveItems = new int[items.Count];
            for (int i = 0; i < saveItems.Length; i++)
            {
                saveItems[i] = items[i].Save();
            }

            return saveItems;
        }
    }

    /// <summary>
    /// A Hero List field for an integer.
    /// This field is used to store an integer that you can use in a hero object action.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class IntField : HeroListObjectField<int>, IHeroListField<IntField>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public IntField() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="field">The int field to construct.</param>
        public IntField(IntField field)
        {
            name = field.name;
            value = field.value;
        }

        /// <summary>
        /// Clone the int field, remove references.
        /// </summary>
        /// <param name="field">The int field to clone.</param>
        /// <returns>The cloned int field.</returns>
        public IntField Clone(IntField field)
        {
            IntField temp = new IntField();
            temp.name = field.name;
            temp.value = field.value;
            return temp;
        }

        /// <summary>
        /// Get the int field to save.
        /// </summary>
        /// <returns>The value for the int field.</returns>
        public int Save()
        {
            return value;
        }
    }
}