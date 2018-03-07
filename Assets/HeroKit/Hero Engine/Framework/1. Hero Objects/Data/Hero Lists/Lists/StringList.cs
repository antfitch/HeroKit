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
    /// A Hero List for strings. 
    /// This list stores strings that you can use in a hero object action.
    /// This list inherits the values in the HeroListObject template.
    /// </summary>
    [System.Serializable]
    public class StringList : HeroListObject
    {
        /// <summary>
        /// The list of string fields on a hero object.
        /// </summary>
        public List<StringField> items = new List<StringField>();

        /// <summary>
        /// Clone the list, remove references.
        /// </summary>
        /// <param name="list">The string field list we are cloning.</param>
        /// <returns>The cloned string field list.</returns>
        public StringList Clone(StringList oldList)
        {
            StringList tempList = new StringList(); 
            List<StringField> tempItems = (oldList == null || oldList.items == null) ? new List<StringField>() : new List<StringField>(oldList.items.Select(x => x.Clone(x)));
            tempList.items = tempItems;
            return tempList;
        }

        /// <summary>
        /// Save the string fields in a save game file.
        /// </summary>
        /// <returns>An array that contains the strings in the string field list.</returns>
        public string[] Save()
        {
            if (items == null) return null;

            string[] saveItems = new string[items.Count];
            for (int i = 0; i < saveItems.Length; i++)
            {
                saveItems[i] = items[i].Save();
            }

            return saveItems;
        }
    }

    /// <summary>
    /// A Hero List field for a string.
    /// This field is used to store a string that you can use in a hero object action.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class StringField : HeroListObjectField<string>, IHeroListField<StringField>
    {
        /// <summary>
        /// The string has variables inside of it that need to be parsed before they are displayed during gameplay.
        /// </summary>
        public bool useVariables;

        /// <summary>
        /// The string has variables inside of it that need to be parsed before they are displayed during gameplay.
        /// </summary>
        public bool useTextField;

        /// <summary>
        /// Constructor.
        /// </summary>
        public StringField() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="field">The string field to construct.</param>
        public StringField(StringField field)
        {
            name = field.name;
            value = field.value;
            useVariables = field.useVariables;
            useTextField = field.useTextField;
        }

        /// <summary>
        /// Clone the string field, remove references.
        /// </summary>
        /// <param name="field">The string field to clone.</param>
        /// <returns>The cloned string field.</returns>
        public StringField Clone(StringField field)
        {
            StringField temp = new StringField();
            temp.name = field.name;
            temp.value = field.value;
            temp.useVariables = field.useVariables;
            temp.useTextField = field.useTextField;
            return temp;
        }

        /// <summary>
        /// Get the string field to save.
        /// </summary>
        /// <returns>The value for the string field.</returns>
        public string Save()
        {
            return value;
        }
    }
}