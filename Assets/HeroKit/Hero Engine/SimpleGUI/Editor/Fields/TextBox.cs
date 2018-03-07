// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;

namespace SimpleGUI.Fields
{
    /// <summary>
    /// Style for text boxes that are used in the hero kit editor.
    /// </summary>
    internal static class TextBox
    {
        /// <summary>
        /// Initialize text box styles.
        /// </summary>
        static TextBox()
        {
            Set_StyleDefault();
            Set_StyleA();
        }

        /// <summary>
        /// Default style for text boxes.
        /// </summary>
        public static GUIStyle StyleDefault { get { return styleDefault; } }
        private static GUIStyle styleDefault;
        private static void Set_StyleDefault()
        {
            styleDefault = new GUIStyle(EditorStyles.textField);
        }

        /// <summary>
        /// Text box style alternative A.
        /// </summary>
        public static GUIStyle StyleA { get { return styleA; } }
        private static GUIStyle styleA;
        private static void Set_StyleA()
        {
            styleA = new GUIStyle(EditorStyles.textField);
            styleA.padding = new RectOffset(3, 3, 3, 3);
        }
    }
}










