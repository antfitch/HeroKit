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
    /// A Hero List for unity objects. 
    /// This list stores unity objects that you can use in a hero object action.
    /// This list inherits the values in the HeroListObject template.
    /// </summary>
    [System.Serializable]
    public class UnityObjectList : HeroListObject
    {
        /// <summary>
        /// The list of unity object fields on a hero object.
        /// </summary>
        public List<UnityObjectField> items = new List<UnityObjectField>();

        /// <summary>
        /// Clone the list, remove references.
        /// </summary>
        /// <param name="list">The unity object field list we are cloning.</param>
        /// <returns>The cloned unity object field list.</returns>
        public UnityObjectList Clone(UnityObjectList oldList)
        {
            UnityObjectList tempList = new UnityObjectList();
            List<UnityObjectField> tempItems = new List<UnityObjectField>();
            tempItems = (oldList == null || oldList.items == null) ? new List<UnityObjectField>() : new List<UnityObjectField>(oldList.items.Select(x => x.Clone(x)));
            tempList.items = tempItems;
            return tempList;
        }
    }

    /// <summary>
    /// A Hero List field for a unity object.
    /// This field is used to store a unity object that you can use in a hero object action.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class UnityObjectField : HeroListObjectField<UnityEngine.Object>, IHeroListField<UnityObjectField>
    {
        /// <summary>
        /// The type of unity object. (ex. 1=audio clip, 2=sprite, 3=scene)
        /// </summary>
        public int objectType;
        /// <summary>
        /// ID assigned to a scene. (ex. 1=audio clip, 2=sprite, 3=scene)
        /// </summary>
        public int sceneID;
        /// <summary>
        /// name assigned to a scene. (ex. 1=audio clip, 2=sprite, 3=scene)
        /// </summary>
        public string sceneName;

        /// <summary>
        /// Constructor.
        /// </summary>
        public UnityObjectField() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="field">The unity object field to construct.</param>
        public UnityObjectField(UnityObjectField field)
        {
            name = field.name;
            value = field.value;
            objectType = field.objectType;
            sceneID = field.sceneID;
            sceneName = field.sceneName;
        }

        /// <summary>
        /// Clone the unity object field, remove references.
        /// </summary>
        /// <param name="field">The unity object field to clone.</param>
        /// <returns>The cloned unity object field.</returns>
        public UnityObjectField Clone(UnityObjectField field)
        {
            UnityObjectField temp = new UnityObjectField();
            temp.name = field.name;
            temp.value = field.value;
            temp.objectType = field.objectType;
            temp.sceneID = field.sceneID;
            temp.sceneName = field.sceneName;
            return temp;
        }
    }
}