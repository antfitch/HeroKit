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
    /// A Hero List for floats. 
    /// This list stores floats that you can use in a hero object action.
    /// This list inherits the values in the HeroListObject template.
    /// </summary>
    [System.Serializable]
    public class FloatList : HeroListObject
    {
        /// <summary>
        /// The list of float fields on a hero object.
        /// </summary>
        public List<FloatField> items = new List<FloatField>();

        /// <summary>
        /// Clone the list, remove references.
        /// </summary>
        /// <param name="list">The float field list we are cloning.</param>
        /// <returns>The cloned float field list.</returns>
        public FloatList Clone(FloatList oldList)
        {
            FloatList tempList = new FloatList(); 
            List<FloatField> tempItems = (oldList == null || oldList.items == null) ? new List<FloatField>() : new List<FloatField>(oldList.items.Select(x => x.Clone(x)));
            tempList.items = tempItems;
            return tempList;
        }

        /// <summary>
        /// Save the float fields in a save game file.
        /// </summary>
        /// <returns>An array that contains the floats in the float field list.</returns>
        public float[] Save()
        {
            if (items == null) return null;

            float[] saveItems = new float[items.Count];
            for (int i = 0; i < saveItems.Length; i++)
            {
                saveItems[i] = items[i].Save();
            }

            return saveItems;
        }
    }

    /// <summary>
    /// A Hero List field for a float.
    /// This field is used to store a float that you can use in a hero object action.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class FloatField : HeroListObjectField<float>, IHeroListField<FloatField>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public FloatField() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="field">The float field to construct.</param>
        public FloatField(FloatField field)
        {
            name = field.name;
            value = field.value;
        }

        /// <summary>
        /// Clone the float field, remove references.
        /// </summary>
        /// <param name="field">The float field to clone.</param>
        /// <returns>The cloned float field.</returns>
        public FloatField Clone(FloatField field)
        {
            FloatField temp = new FloatField();
            temp.name = field.name;
            temp.value = field.value;
            return temp;
        }

        /// <summary>
        /// Get the float field to save.
        /// </summary>
        /// <returns>The value for the float field.</returns>
        public float Save()
        {
            return value;
        }
    }
}