// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;

namespace SimpleGUI.Fields
{
    /// <summary>
    /// Style for boxes that are used in the hero kit editor.
    /// </summary>
    internal static class Box
    {
        /// <summary>
        /// Initialize box styles.
        /// </summary>
        static Box()
        {
            Set_StyleDefault();
            Set_StyleMenu();
            Set_StyleMenu2();
            Set_StyleMenuSelected();
            Set_StyleMenu2Selected();
            Set_StyleCanvas();
            Set_StyleCanvasTitleBox();
            Set_StyleCanvasBox();
            Set_StyleTitle();
            Set_StyleA();
            Set_StyleB();
            Set_StyleC();
            Set_StyleImagePreview();
            Set_StyleTest();
        }

        // --------------------------------------------------------------
        // General
        // --------------------------------------------------------------

        /// <summary>
        /// Default style for boxes.
        /// </summary>   
        public static GUIStyle StyleDefault { get { return styleDefault; } }
        private static GUIStyle styleDefault;
        private static void Set_StyleDefault()
        {
            styleDefault = new GUIStyle();
        }

        // --------------------------------------------------------------
        // Content Boxes
        // --------------------------------------------------------------

        /// <summary>
        /// Style for a content box.
        /// </summary>      
        public static GUIStyle StyleA { get { return styleA; } }
        private static GUIStyle styleA;
        private static void Set_StyleA()
        {
            styleA = new GUIStyle(EditorStyles.helpBox);
            styleA.margin.left += 15;
            styleA.margin.right += 15;
            styleA.padding = new RectOffset(5, 5, 5, 5);
        }

        /// <summary>
        /// Style for a content box.
        /// </summary>       
        public static GUIStyle StyleB { get { return styleB; } }
        private static GUIStyle styleB;
        private static void Set_StyleB()
        {
            styleB = new GUIStyle(EditorStyles.helpBox);
            styleB.margin.left += 5;
            styleB.margin.right += 15;
            styleB.padding = new RectOffset(5, 5, 5, 5);
        }

        /// <summary>
        /// Style for a content box.
        /// </summary>       
        public static GUIStyle StyleC { get { return styleC; } }
        private static GUIStyle styleC;
        private static void Set_StyleC()
        {
            styleC = new GUIStyle(EditorStyles.helpBox);
            styleC.margin.left += 5;
            styleC.margin.right += 5;
            styleC.padding = new RectOffset(5, 5, 5, 5);
        }

        /// <summary>
        /// Style for the image preview for an item in the state window.
        /// </summary>    
        public static GUIStyle StyleImagePreview { get { return styleImagePreview; } }
        private static GUIStyle styleImagePreview;
        private static void Set_StyleImagePreview()
        {
            styleImagePreview = new GUIStyle(EditorStyles.textField);
            styleImagePreview.margin.left += 5;
            styleImagePreview.margin.right += 5;
            styleImagePreview.padding = new RectOffset(5, 5, 5, 5);
        }

        // --------------------------------------------------------------
        // Title Window
        // --------------------------------------------------------------

        /// <summary>
        /// Style for the title window
        /// </summary>    
        public static GUIStyle StyleTitleWindow { get { return styleTitleWindow; } }
        private static GUIStyle styleTitleWindow;
        private static void Set_StyleTitle()
        {
            styleTitleWindow = new GUIStyle();
            styleTitleWindow.alignment = TextAnchor.MiddleLeft;

            if (SimpleGUICommon.isProSkin)
                styleTitleWindow.normal.background = SimpleGUICommon.GetTextureFromColor("#000000");// SimpleGUICommon.StringToTexture(normal);
            else
                styleTitleWindow.normal.background = SimpleGUICommon.GetTextureFromColor("#222c37");// SimpleGUICommon.StringToTexture(normal);

            styleTitleWindow.border = new RectOffset(1, 1, 1, 2);
            styleTitleWindow.padding = new RectOffset(12, 12, 9, 0);
            styleTitleWindow.clipping = TextClipping.Overflow;
            styleTitleWindow.normal.textColor = SimpleGUICommon.GetColor("#FFFFFF");
            styleTitleWindow.active.textColor = SimpleGUICommon.GetColor("#FFFFFF");
        }

        // --------------------------------------------------------------
        // Menu Window
        // --------------------------------------------------------------

        /// <summary>
        /// Style for the hero kit menu.
        /// </summary>    
        public static GUIStyle StyleMenu { get { return styleMenu; } }
        private static GUIStyle styleMenu;
        private static void Set_StyleMenu()
        {
            styleMenu = new GUIStyle();
            styleMenu.padding = new RectOffset(0, 0, 0, 0);
        }

        /// <summary>
        /// Style for hero kit menu + some extra padding on the left and right. Great for a nested menu.
        /// </summary>      
        public static GUIStyle StyleMenu2 { get { return styleMenu2; } }
        private static GUIStyle styleMenu2;
        private static void Set_StyleMenu2()
        {
            styleMenu2 = new GUIStyle();

            if (SimpleGUICommon.isProSkin)
                styleMenu2.normal.background = SimpleGUICommon.GetTextureFromColor("#505050"); //818181
            else
                styleMenu2.normal.background = SimpleGUICommon.GetTextureFromColor("#ffffff");

            styleMenu2.padding = new RectOffset(20, 0, 20, 0);
        }

        /// <summary>
        /// If an item is selected in the hero kit menu, show this box around it.
        /// </summary>     
        public static GUIStyle StyleMenuSelected { get { return styleMenuSelected; } }
        private static GUIStyle styleMenuSelected;
        private static void Set_StyleMenuSelected()
        {
            styleMenuSelected = new GUIStyle();

            if (SimpleGUICommon.isProSkin)
                styleMenuSelected.normal.background = SimpleGUICommon.GetTextureFromColor("#3c3c3c");
            else
                styleMenuSelected.normal.background = SimpleGUICommon.GetTextureFromColor("#f0f0f0");
        }

        /// <summary>
        /// If an item is selected in the hero kit menu, show this box around it.
        /// </summary>     
        public static GUIStyle StyleMenu2Selected { get { return styleMenu2Selected; } }
        private static GUIStyle styleMenu2Selected;
        private static void Set_StyleMenu2Selected()
        {
            styleMenu2Selected = new GUIStyle();

            if (SimpleGUICommon.isProSkin)
                styleMenu2Selected.normal.background = SimpleGUICommon.GetTextureFromColor("#505050");
            else
                styleMenu2Selected.normal.background = SimpleGUICommon.GetTextureFromColor("#ffffff");
        }

        // --------------------------------------------------------------
        // Canvas Window
        // --------------------------------------------------------------

        /// <summary>
        /// Style for the canvas.
        /// </summary>     
        public static GUIStyle StyleCanvas { get { return styleCanvas; } }
        private static GUIStyle styleCanvas;
        private static void Set_StyleCanvas()
        {
            styleCanvas = new GUIStyle();

            if (SimpleGUICommon.isProSkin)
                styleCanvas.normal.background = SimpleGUICommon.GetTextureFromColor("#3c3c3c");
            else
                styleCanvas.normal.background = SimpleGUICommon.GetTextureFromColor("#f0f0f0");
        }

        /// <summary>
        /// Puts an invisible border around contents in the canvas.
        /// </summary>      
        public static GUIStyle StyleCanvasBox { get { return styleCanvasBox; } }
        private static GUIStyle styleCanvasBox;
        private static void Set_StyleCanvasBox()
        {
            styleCanvasBox = new GUIStyle();
            styleCanvasBox.margin.top += 10;
            styleCanvasBox.margin.bottom += 5;
            styleCanvasBox.margin.left += 5;
            styleCanvasBox.margin.right += 5;
            styleCanvasBox.padding = new RectOffset(5, 5, 5, 5);
        }

        /// <summary>
        /// Box around the title in the canvas window.
        /// </summary>    
        public static GUIStyle StyleCanvasTitleBox { get { return styleCanvasTitleBox; } }
        private static GUIStyle styleCanvasTitleBox;
        private static void Set_StyleCanvasTitleBox()
        {
            styleCanvasTitleBox = new GUIStyle();
            styleCanvasTitleBox.alignment = TextAnchor.MiddleLeft;
            //styleCanvasTitleBox.normal.background = SimpleGUICommon.GetTextureFromColor("#373737");
            styleCanvasTitleBox.border = new RectOffset(2, 2, 2, 2);
            styleCanvasTitleBox.padding = new RectOffset(9, 9, 22, 9);
            styleCanvasTitleBox.clipping = TextClipping.Overflow;
        }

        // --------------------------------------------------------------
        // Other
        // --------------------------------------------------------------

        // TEST STYLE. USE FOR TESTING
        private static GUIStyle styleTest;
        public static GUIStyle StyleTest { get { return styleTest; } }
        private static void Set_StyleTest()
        {
            styleTest = new GUIStyle(EditorStyles.helpBox);
            styleTest.alignment = TextAnchor.UpperLeft;
        }
    }
}








