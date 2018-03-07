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
    /// A Hero List for bools. 
    /// This list stores bools that you can use in a hero object action.
    /// This list inherits the values in the HeroListObject template.
    /// </summary>
    [System.Serializable]
    public class BoolList : HeroListObject
    {
        /// <summary>
        /// The list of bool fields on a hero object.
        /// </summary>
        public List<BoolField> items = new List<BoolField>();

        /// <summary>
        /// Clone the list, remove references.
        /// </summary>
        /// <param name="list">The bool field list we are cloning.</param>
        /// <returns>The cloned bool field list.</returns>
        public BoolList Clone(BoolList list)
        {
            BoolList tempList = new BoolList();
            List<BoolField> tempItems = new List<BoolField>();
            tempItems = (list == null || list.items == null) ? new List<BoolField>() : new List<BoolField>(list.items.Select(x => x.Clone(x)));
            tempList.items = tempItems;
            return tempList;
        }

        /// <summary>
        /// Save the bool fields in a save game file.
        /// </summary>
        /// <returns>An array that contains the bools in the bool field list.</returns>
        public bool[] Save()
        {
            if (items == null) return null;

            bool[] saveItems = new bool[items.Count];
            for (int i = 0; i < saveItems.Length; i++)
            {
                saveItems[i] = items[i].Save();
            }

            return saveItems;
        }
    }

    /// <summary>
    /// A Hero List field for a bool.
    /// This field is used to store a bool that you can use in a hero object action.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class BoolField : HeroListObjectField<bool>, IHeroListField<BoolField>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public BoolField() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="field">The bool field to construct.</param>
        public BoolField(BoolField field)
        {
            name = field.name;
            value = field.value;
        }

        /// <summary>
        /// Clone the bool field, remove references.
        /// </summary>
        /// <param name="field">The bool field to clone.</param>
        /// <returns>The cloned bool field.</returns>
        public BoolField Clone(BoolField field)
        {
            BoolField temp = new BoolField();
            temp.name = field.name;
            temp.value = field.value;
            return temp;
        }

        /// <summary>
        /// Get the bool field to save.
        /// </summary>
        /// <returns>The value for the bool field.</returns>
        public bool Save()
        {
            return value;
        }
    }
}