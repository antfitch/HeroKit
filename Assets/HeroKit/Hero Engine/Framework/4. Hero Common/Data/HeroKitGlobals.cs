// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene
{
    /// <summary>
    /// Display the globals in a scriptable object.
    /// </summary>
    [System.Serializable]
    public class HeroKitGlobals : ScriptableObject
    {
        /// <summary>
        /// A hero list for the global variables.
        /// </summary>
        public HeroList globals;

        // Disabled because it caused error when creating build.
        ///// <summary>
        ///// If this flag is not set, the contents of the default scriptable object are destroyed. Leave this!
        ///// </summary>
        //public void OnEnable()
        //{
        //    hideFlags = HideFlags.DontSave;  
        //}
    }
}
