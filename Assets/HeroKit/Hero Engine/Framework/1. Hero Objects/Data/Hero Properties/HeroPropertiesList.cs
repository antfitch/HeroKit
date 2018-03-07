// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.Collections.Generic;

namespace HeroKit.Scene
{
    /// <summary>
    /// A container for all properties assigned to a hero object.
    /// </summary>
    [System.Serializable]
    public class HeroPropertiesList
    {
        /// <summary>
        /// A list of hero properties assigned to the hero object.
        /// </summary>
        public List<HeroProperties> properties = new List<HeroProperties>();
        /// <summary>
        /// Are hero properties visible in the editor?
        /// </summary>
        public bool visible;
    }
}