// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene
{
    /// <summary>
    /// Display the hero properties in a scriptable object.
    /// </summary>
    [System.Serializable]
    public class HeroKitProperty : ScriptableObject
    {
        /// <summary>
        /// Description for the property.
        /// </summary>
        public string description;
        /// <summary>
        /// A hero list for the property.
        /// </summary>
        public HeroList properties;
    }
}
