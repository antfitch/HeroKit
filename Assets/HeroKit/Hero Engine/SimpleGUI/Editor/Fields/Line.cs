// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;

namespace SimpleGUI.Fields
{
    /// <summary>
    /// Style for lines (horizontal rules) that are used in the hero kit editor.
    /// </summary>
    internal static class Line
    {
        /// <summary>
        /// Initialize line styles.
        /// </summary>
        static Line()
        {
            Set_StyleDefault();
        }

        //--------------------------------------
        // Color for lines
        //--------------------------------------

        /// <summary>
        /// Color of the line.
        /// </summary>
        private static readonly Color color = SimpleGUICommon.GetColor("#000000");

        //--------------------------------------
        // Style for lines
        //--------------------------------------

        /// <summary>
        /// Default style for lines.
        /// </summary>
        public static GUIStyle StyleDefault { get { return styleDefault; } }
        private static GUIStyle styleDefault;
        private static void Set_StyleDefault()
        {
            styleDefault = new GUIStyle();
            styleDefault.normal.background = EditorGUIUtility.whiteTexture;
            styleDefault.stretchWidth = true;
            styleDefault.margin = new RectOffset(0, 0, 7, 7);
        }

        //--------------------------------------
        // Methods for lines
        //--------------------------------------

        /// <summary>
        /// Create line with specific height, using default colors.
        /// </summary>
        /// <param name="itemHeight">Height of the line.</param>
        public static void setValues(float itemHeight = 1)
        {
            GUILayoutOption height = GUILayout.Height(itemHeight);
            Rect position = GUILayoutUtility.GetRect(GUIContent.none, styleDefault, height);
            paintLine(color, position, styleDefault);
        }

        /// <summary>
        /// Create line that is a color and height of your choosing.
        /// </summary>
        /// <param name="itemColor">Color of the line.</param>
        /// <param name="itemHeight">Height of the line.</param>
        public static void setValues(Color itemColor, float itemHeight = 1)
        {
            GUILayoutOption height = GUILayout.Height(itemHeight);
            Rect position = GUILayoutUtility.GetRect(GUIContent.none, styleDefault, height);
            paintLine(itemColor, position, styleDefault);
        }

        /// <summary>
        /// Create line with specific height and position from previous GUI object.
        /// </summary>
        /// <param name="itemPosition">Distance from previous GUI object.</param>
        /// <param name="itemHeight">Height of the line.</param>
        public static void setValues(float itemPosition, float itemHeight)
        {
            GUILayoutOption height = GUILayout.Height(itemHeight);
            Rect position = GUILayoutUtility.GetRect(GUIContent.none, styleDefault, height);
            position.yMin += itemPosition; 
            paintLine(color, position, styleDefault);
        }

        /// <summary>
        /// Create line with specific height, position from previous GUI object, and style.
        /// </summary>
        /// <param name="itemPosition">Distance from previous GUI object.</param>
        /// <param name="style">GUI Style to use on the line.</param>
        /// <param name="itemHeight">Height of the line.</param>
        public static void setValues(float itemPosition, GUIStyle style, float itemHeight = 1)
        {
            GUILayoutOption height = GUILayout.Height(itemHeight);
            Rect position = GUILayoutUtility.GetRect(GUIContent.none, style, height);
            position.yMin += itemPosition;
            paintLine(color, position, style);
        }

        /// <summary>
        /// Paint the line.
        /// </summary>
        /// <param name="itemColor">Color of the line.</param>
        /// <param name="itemPosition">Distance from previous GUI object.</param>
        /// <param name="style">GUI Style to use on the line.</param>
        private static void paintLine(Color itemColor, Rect itemPosition, GUIStyle style)
        {
            if (Event.current.type == EventType.Repaint)
            {
                Color restoreColor = GUI.color;
                GUI.color = itemColor;
                style.Draw(itemPosition, false, false, false, false);
                GUI.color = restoreColor;
            }
        }
    }
}








