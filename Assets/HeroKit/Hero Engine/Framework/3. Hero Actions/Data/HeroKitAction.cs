// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene
{
    /// <summary>
    /// Display the hero action in a scriptable object.
    /// </summary>
    [System.Serializable]
    public class HeroKitAction : ScriptableObject
    {
        /// <summary>
        /// The C# script that contains the code that should execute at runtime.
        /// </summary>
        public Object action;
        /// <summary>
        /// The C# script that contains the code that should execute inside the Unity Editor.
        /// </summary>
        public Object actionFields;
        /// <summary>
        /// The text that should appear to the left of the action in the Hero Kit Editor menu.
        /// </summary>
        public string title;
        /// <summary>
        /// A description about the action.
        /// </summary>
        [TextArea(3, 10)]
        public string description;
        /// <summary>
        /// Indent this action and actions that come after this action.
        /// </summary>
        public int indentThis;
        /// <summary>
        /// Indent actions that come after this action.
        /// </summary>
        public int indentNext;   
        /// <summary>
        /// Color of the action text in the Hero Kit Editor menu.
        /// </summary>
        public Color titleColor;
        /// <summary>
        /// Version information about this action.
        /// </summary>
        public string version;
    }
}
