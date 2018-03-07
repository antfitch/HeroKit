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
    /// A Hero Action Field represents a field in an action. 
    /// An action can have one or more fields.
    /// </summary>    
    [System.Serializable]
    public class HeroActionField
    {
        /// <summary>
        /// The name of the hero action field.
        /// </summary>
        public string name = "";
        /// <summary>
        /// The ID assigned to the field. The default ID is 0.
        /// </summary>
        public int id = 0;   // id assigned field
        /// <summary>
        /// The integers used by the field.
        /// </summary>
        public List<int> ints = new List<int>();
        /// <summary>
        /// The floats used by the field.
        /// </summary>
        public List<float> floats = new List<float>();
        /// <summary>
        /// The strings used by the field.
        /// </summary>
        public List<string> strings = new List<string>();
        /// <summary>
        /// The bools used by the field.
        /// </summary>
        public List<bool> bools = new List<bool>();
        /// <summary>
        /// The game objets used by the field.
        /// </summary>
        public List<GameObject> gameObjects = new List<GameObject>();
        /// <summary>
        /// The hero objects needed to power the field.
        /// </summary>
        public List<HeroObject> heroObjects = new List<HeroObject>();
        /// <summary>
        /// The unity objects used by the field.
        /// </summary>
        public List<Object> unityObjects = new List<Object>();
        /// <summary>
        /// The colors used by the field.
        /// </summary>
        public List<Color> colors = new List<Color>();
        /// <summary>
        /// The hero properties used by the field.
        /// </summary>
        public List<HeroKitProperty> heroProperties = new List<HeroKitProperty>();
        /// <summary>
        /// The method used by the field.
        /// </summary>
        public SerializableMethodInfo method = null;
        /// <summary>
        /// An object used by the field.
        /// </summary>
        public Object component = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeroActionField() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="list">The hero action field to construct.</param>
        public HeroActionField(HeroActionField field)
        {
            name = field.name;
            id = field.id;

            ints = field.ints.Select(x => x).ToList();
            floats = field.floats.Select(x => x).ToList();
            strings = field.strings.Select(x => x).ToList();
            bools = field.bools.Select(x => x).ToList();
            gameObjects = field.gameObjects.Select(x => x).ToList();
            heroObjects = field.heroObjects.Select(x => x).ToList();
            unityObjects = field.unityObjects.Select(x => x).ToList();
            colors = field.colors.Select(x => x).ToList();
            heroProperties = field.heroProperties.Select(x => x).ToList();
            method = field.method;
            component = null;
        }

        /// <summary>
        /// Clone the hero action field, remove references.
        /// </summary>
        /// <param name="field">The hero action field to clone.</param>
        /// <returns>The cloned hero action field.</returns>
        public HeroActionField Clone(HeroActionField field)
        {
            HeroActionField temp = new HeroActionField();

            temp.name = field.name;
            temp.id = field.id;

            temp.ints = field.ints.Select(x => x).ToList();
            temp.floats = field.floats.Select(x => x).ToList();
            temp.strings = field.strings.Select(x => x).ToList();
            temp.bools = field.bools.Select(x => x).ToList();
            temp.gameObjects = field.gameObjects.Select(x => x).ToList();
            temp.heroObjects = field.heroObjects.Select(x => x).ToList();
            temp.unityObjects = field.unityObjects.Select(x => x).ToList();
            temp.colors = field.colors.Select(x => x).ToList();
            temp.heroProperties = field.heroProperties.Select(x => x).ToList();
            temp.method = field.method;
            temp.component = field.component;

            return temp;
        }
    }
}