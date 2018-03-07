// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene
{
    /// <summary>
    /// A container for all data in a hero object. This includes:
    /// - Properties for the hero object
    /// - States (and each state's actions) for the hero object
    /// - Variable Lists for the hero object
    /// </summary>
    [System.Serializable]
    public class HeroObject : ScriptableObject
    {
        /// <summary>
        /// Properties for the hero object.
        /// </summary>
        public HeroProperties properties;

        public HeroPropertiesList propertiesList;

        /// <summary>
        /// States for the hero object.
        /// </summary>
        public HeroStateObject states;
        /// <summary>
        /// Local variables for the hero object.
        /// </summary>
        public HeroList lists;
        /// <summary>
        /// Are global variables visible in the editor?
        /// </summary>
        public bool globalsVisible;
    }
}