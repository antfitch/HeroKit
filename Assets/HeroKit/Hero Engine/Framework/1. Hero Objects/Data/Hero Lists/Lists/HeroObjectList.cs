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
    /// A Hero List for hero kit objects. 
    /// This list stores hero kit objects that you can use in a hero object action.
    /// This list inherits the values in the HeroListObject template.
    /// </summary>
    [System.Serializable]
    public class HeroObjectList : HeroListObject
    {
        /// <summary>
        /// The list of hero object fields on a hero object.
        /// </summary>
        public List<HeroObjectField> items = new List<HeroObjectField>();

        /// <summary>
        /// Clone the list, remove references.
        /// </summary>
        /// <param name="list">The hero object field list we are cloning.</param>
        /// <returns>The cloned hero object field list.</returns>
        public HeroObjectList Clone(HeroObjectList oldList)
        {
            HeroObjectList tempList = new HeroObjectList();
            List<HeroObjectField> tempItems = new List<HeroObjectField>();
            tempItems = (oldList == null || oldList.items == null) ? new List<HeroObjectField>() : new List<HeroObjectField>(oldList.items.Select(x => x.Clone(x)));
            tempList.items = tempItems;
            return tempList;
        }
    }

    /// <summary>
    /// A Hero List field for a hero kit object.
    /// This field is used to store a hero kit object that you can use in a hero object action.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class HeroObjectField : HeroListObjectField<HeroObject>, IHeroListField<HeroObjectField>
    {
        /// <summary>
        /// The list of hero kit objects assigned to a hero object field.
        /// </summary>
        public List<HeroKitObject> heroKitGameObjects;

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeroObjectField() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="field">The hero object field to construct.</param>
        public HeroObjectField(HeroObjectField field)
        {
            name = field.name;
            value = field.value;
        }

        /// <summary>
        /// Clone the hero object field, remove references.
        /// </summary>
        /// <param name="field">The hero object field to clone.</param>
        /// <returns>The cloned hero object field.</returns>
        public HeroObjectField Clone(HeroObjectField field)
        {
            HeroObjectField temp = new HeroObjectField();
            temp.name = field.name;
            temp.value = field.value;
            return temp;
        }
    }
}