// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using HeroKit.Editor;

namespace HeroKit.Scene
{
    /// <summary>
    /// A Hero List for game objects. 
    /// This list stores game objects that you can use in a hero object action.
    /// This list inherits the values in the HeroListObject template.
    /// </summary>
    [System.Serializable]
    public class GameObjectList : HeroListObject
    {
        /// <summary>
        /// The list of game object fields on a hero object.
        /// </summary>
        public List<GameObjectField> items = new List<GameObjectField>();

        /// <summary>
        /// Clone the list, remove references.
        /// </summary>
        /// <param name="list">The game object field list we are cloning.</param>
        /// <returns>The cloned game object field list.</returns>
        public GameObjectList Clone(GameObjectList oldList)
        {
            GameObjectList tempList = new GameObjectList();
            List<GameObjectField> tempItems = new List<GameObjectField>();
            tempItems = (oldList == null || oldList.items == null) ? new List<GameObjectField>() : new List<GameObjectField>(oldList.items.Select(x => x.Clone(x)));
            tempList.items = tempItems;
            return tempList;
        }
    }

    /// <summary>
    /// A Hero List field for a game object.
    /// This field is used to store a game object that you can use in a hero object action.
    /// This field inherits the values in the HeroListObjectField template.
    /// </summary>
    [System.Serializable]
    public class GameObjectField : HeroListObjectField<GameObject>, IHeroListField<GameObjectField>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GameObjectField() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="field">The game object field to construct.</param>
        public GameObjectField(GameObjectField field)
        {
            name = field.name;
            value = field.value;
        }

        /// <summary>
        /// Clone the game object field, remove references.
        /// </summary>
        /// <param name="field">The game object field to clone.</param>
        /// <returns>The cloned game object field.</returns>
        public GameObjectField Clone(GameObjectField field)
        {
            GameObjectField temp = new GameObjectField();
            temp.name = field.name;
            temp.value = field.value;
            return temp;
        }
    }
}