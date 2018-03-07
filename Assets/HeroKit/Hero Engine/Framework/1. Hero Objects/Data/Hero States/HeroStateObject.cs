// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.Collections.Generic;

namespace HeroKit.Scene
{
    /// <summary>
    /// A container for all states assigned to a hero object.
    /// </summary>
    [System.Serializable]
    public class HeroStateObject
    {
        /// <summary>
        /// A list of hero states assigned to the hero object.
        /// </summary>
        public List<HeroState> states = new List<HeroState>();
        /// <summary>
        /// Are hero states visible in the editor?
        /// </summary>
        public bool visible;                
    }
}