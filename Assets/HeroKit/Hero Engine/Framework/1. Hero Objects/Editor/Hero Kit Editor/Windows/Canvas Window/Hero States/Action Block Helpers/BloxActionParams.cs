// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;

namespace HeroKit.Editor
{
    /// <summary>
    /// Paramaters to quickly build a hero action form.
    /// </summary>
    public struct HeroActionParams
    {
        /// <summary>
        /// A hero object.
        /// </summary>
        public HeroObject heroObject;
        /// <summary>
        /// A hero action that exists inside the hero object.
        /// </summary>
        public HeroAction heroAction;

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeroActionParams(HeroObject _heroObject, HeroAction _heroAction)
        {
            heroObject = _heroObject;
            heroAction = _heroAction;
        } 
    }
}