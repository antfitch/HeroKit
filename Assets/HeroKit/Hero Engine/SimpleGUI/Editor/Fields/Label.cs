// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;

namespace SimpleGUI.Fields
{
    internal static class Label
    {
        /// <summary>
        /// Initialize label styles.
        /// </summary>
        static Label()
        {
            Set_StyleDefault();
            Set_StyleDescription();
            Set_StyleListField();
            Set_StyleBlockTitle();
            Set_HeroTitle();
        }

        //--------------------------------------------
        // Style for text boxes and labels
        //--------------------------------------------

        /// <summary>
        /// Default style.
        /// </summary>
        private static GUIStyle styleDefault;
        public static GUIStyle StyleDefault { get { return styleDefault; } }
        private static void Set_StyleDefault()
        {
            styleDefault = new GUIStyle(EditorStyles.label);
        }

        /// <summary>
        /// Style for descriptions.
        /// </summary>
        private static GUIStyle styleDescription;
        public static GUIStyle StyleDescription { get { return styleDescription; } }
        private static void Set_StyleDescription()
        {
            styleDescription = new GUIStyle(EditorStyles.label);
            styleDescription.wordWrap = true;
        }

        /// <summary>
        /// Style for items in a hero list. (ex. Slot 1: "")
        /// </summary>   
        public static GUIStyle StyleListField { get { return styleListField; } }
        private static GUIStyle styleListField;
        private static void Set_StyleListField()
        {
            styleListField = new GUIStyle(EditorStyles.label);
            styleListField.padding = new RectOffset(3, 3, 3, 3);
        }

        /// <summary>
        /// Style for titles in the canvas window.
        /// </summary>  
        public static GUIStyle StyleBlockTitle { get { return styleBlockTitle; } }
        private static GUIStyle styleBlockTitle;
        private static void Set_StyleBlockTitle()
        {
            styleBlockTitle = new GUIStyle(EditorStyles.label);
            styleBlockTitle.alignment = TextAnchor.MiddleLeft;
			styleBlockTitle.normal.textColor = EditorStyles.label.normal.textColor;
            styleBlockTitle.font = (Font)Resources.Load("JosefinSans-Bold-20");

			// custom fonts are not showing up on OSX. for now, just show default font
			if (Application.platform == RuntimePlatform.OSXEditor) {
				styleBlockTitle.font = EditorStyles.label.font;
				styleBlockTitle.fontSize = 20;
				styleBlockTitle.fontStyle = FontStyle.Bold;
			} 
        }

        /// <summary>
        /// Style for the title in the title window.
        /// </summary>      
        public static GUIStyle StyleHeroTitle { get { return styleHeroTitle; } }
        private static GUIStyle styleHeroTitle;
        private static void Set_HeroTitle()
        {
            styleHeroTitle = new GUIStyle(EditorStyles.label);
            styleHeroTitle.alignment = TextAnchor.MiddleLeft;
            styleHeroTitle.font = (Font)Resources.Load("JosefinSans-Bold-30");

            // custom fonts are not showing up on OSX. for now, just show default font
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                styleHeroTitle.font = EditorStyles.label.font;
                styleHeroTitle.fontSize = 25;
                styleHeroTitle.fontStyle = FontStyle.Bold;
            }

            styleHeroTitle.normal.textColor = SimpleGUICommon.GetColor("#FFFFFF");
        }
    }
}










