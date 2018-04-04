// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

namespace SimpleGUI.Fields
{
    /// <summary>
    /// Style for buttons that are used in the hero kit editor.
    /// </summary>
    internal static class Button
    {
        /// <summary>
        /// Initialize button styles.
        /// </summary>
        static Button()
        {
            Set_StyleDefault();
            Set_StyleA();
            Set_StyleB();
            Set_StyleBig();
            Set_StyleAddMenuItem();      

            Set_StyleFoldoutClosed();
            Set_StyleFoldoutOpen();
            Set_StyleFoldoutText();
            Set_StyleFoldoutTextB();
            Set_StyleFoldoutHeading();
            Set_StyleFoldoutHeadingB();
            Set_StyleFoldoutDisabledText();   
        }

        //--------------------------------------
        // Button values
        //--------------------------------------

        /// <summary>
        /// Button that has the same action for left and right click. (no parameters)
        /// </summary>
        /// <param name="method">The method to execute when button is clicked.</param>
        /// <param name="buttonStyle">Background style for this button.</param>
        /// <param name="buttonContent">Content style for this button.</param>
        /// <param name="buttonWidth">Width of this button.</param>
        public static void setValues(UnityAction method, GUIStyle buttonStyle, GUIContent buttonContent, int buttonWidth = 0) 
        {
            // set the position of the button
            buttonStyle.fixedWidth = buttonWidth;
            Rect position = GUILayoutUtility.GetRect(buttonContent, buttonStyle);

            // draw the button
            if (GUI.Button(position, buttonContent, buttonStyle))
            {
                if (Event.current.button == 0)
                    method();
            }
        }
        /// <summary>
        /// Button that has the same action for left and right click. (one parameter)
        /// </summary>
        /// <typeparam name="T">Parameter type to pass into the method.</typeparam>
        /// <param name="method">The method to execute when button is clicked.</param>
        /// <param name="arg">The parameter to pass into the method.</param>
        /// <param name="buttonStyle">Background style for this button.</param>
        /// <param name="buttonContent">Content style for this button.</param>
        /// <param name="buttonWidth">Width of this button.</param>
        public static void setValues<T>(UnityAction<T> method, T arg, GUIStyle buttonStyle, GUIContent buttonContent, int buttonWidth=0)
        {
            // set the position of the button
            buttonStyle.fixedWidth = buttonWidth;
            Rect position = GUILayoutUtility.GetRect(buttonContent, buttonStyle);

            // draw the button
            if (GUI.Button(position, buttonContent, buttonStyle))
            {
                if (Event.current.button == 0)
                    method(arg);
            }
        }
        /// <summary>
        /// Button that has the same action for left and right click. (two parameters)
        /// </summary>
        /// <typeparam name="T0">First parameter type to pass into the method.</typeparam>
        /// <typeparam name="T1">Second parameter type to pass into the method.</typeparam>
        /// <typeparam name="T2">Third parameter type to pass into the method.</typeparam>
        /// <param name="method">The method to execute when button is clicked.</param>
        /// <param name="arg1">The first parameter to pass into the method.</param>
        /// <param name="arg2">The second parameter to pass into the method.</param>
        /// <param name="buttonStyle">Background style for this button.</param>
        /// <param name="buttonContent">Content style for this button.</param>
        /// <param name="buttonWidth">Width of this button.</param>
        public static void setValues<T0, T1>(UnityAction<T0, T1> method, T0 arg1, T1 arg2, GUIStyle buttonStyle, GUIContent buttonContent, int buttonWidth = 0)
        {
            // set the position of the button
            buttonStyle.fixedWidth = buttonWidth;
            Rect position = GUILayoutUtility.GetRect(buttonContent, buttonStyle);

            // draw the button
            if (GUI.Button(position, buttonContent, buttonStyle))
            {
                if (Event.current.button == 0)
                    method(arg1, arg2);
            }
        }

        /// <summary>
        /// Button that has the same action for left and right click. (two parameters)
        /// </summary>
        /// <typeparam name="T0">First parameter type to pass into the method.</typeparam>
        /// <typeparam name="T1">Second parameter type to pass into the method.</typeparam>
        /// <param name="method">The method to execute when button is clicked.</param>
        /// <param name="arg1">The first parameter to pass into the method.</param>
        /// <param name="arg2">The second parameter to pass into the method.</param>
        /// <param name="arg3">The third parameter to pass into the method.</param>
        /// <param name="buttonStyle">Background style for this button.</param>
        /// <param name="buttonContent">Content style for this button.</param>
        /// <param name="buttonWidth">Width of this button.</param>
        public static void setValues<T0, T1, T2>(UnityAction<T0, T1, T2> method, T0 arg1, T1 arg2, T2 arg3, GUIStyle buttonStyle, GUIContent buttonContent, int buttonWidth = 0)
        {
            // set the position of the button
            buttonStyle.fixedWidth = buttonWidth;
            Rect position = GUILayoutUtility.GetRect(buttonContent, buttonStyle);

            // draw the button
            if (GUI.Button(position, buttonContent, buttonStyle))
            {
                if (Event.current.button == 0)
                    method(arg1, arg2, arg3);
            }
        }

        /// <summary>
        /// Button for menu item that has a different action for left click (open item in canvas) and right click (open context menu for item).
        /// </summary>
        /// <typeparam name="T">Parameter type to pass into the method.</typeparam>
        /// <param name="methodLeftClick">Method to execute if there was a left click.</param>
        /// <param name="methodRightClick">Method to execute if there was a right click.</param>
        /// <param name="stateIndex">The index assigned to a state.</param>
        /// <param name="buttonStyle">Background style for this button.</param>
        /// <param name="buttonContent">Content style for this button.</param>
        /// <param name="buttonWidth">Width of this button.</param>
        public static void setValues<T>(UnityAction<T> methodLeftClick, UnityAction<T> methodRightClick, T stateIndex, GUIStyle buttonStyle, GUIContent buttonContent, int buttonWidth = 0)
        {
            // set the position of the button
            buttonStyle.fixedWidth = buttonWidth;
            Rect position = GUILayoutUtility.GetRect(buttonContent, buttonStyle);

            // draw the button
            if (GUI.Button(position, buttonContent, buttonStyle))
            {
                if (Event.current.button == 1 || (Event.current.button == 0 && Event.current.shift && Event.current.control))
                {
                    methodRightClick(stateIndex);
                }

                else if (Event.current.button == 0)
                    methodLeftClick(stateIndex);
            }
        }
        /// <summary>
        /// Button for menu item that has a different action for left click (open item in canvas) and right click (open context menu for item).
        /// </summary>
        /// <typeparam name="T">Parameter type to pass into the method.</typeparam>
        /// <param name="methodLeftClick">Method to execute if there was a left click.</param>
        /// <param name="methodRightClick">Method to execute if there was a right click.</param>
        /// <param name="stateIndex">The index assigned to a state.</param>
        /// <param name="eventIndex">The index assigned to an event.</param>
        /// <param name="buttonStyle">Background style for this button.</param>
        /// <param name="buttonContent">Content style for this button.</param>
        /// <param name="buttonWidth">Width of this button.</param>
        public static void setValues<T>(UnityAction<T> methodLeftClick, UnityAction<T,T> methodRightClick, T stateIndex, T eventIndex, GUIStyle buttonStyle, GUIContent buttonContent, int buttonWidth = 0)
        {
            // set the position of the button
            buttonStyle.fixedWidth = buttonWidth;
            Rect position = GUILayoutUtility.GetRect(buttonContent, buttonStyle);

            // draw the button
            if (GUI.Button(position, buttonContent, buttonStyle))
            {
                if (Event.current.button == 1 || (Event.current.button == 0 && Event.current.shift && Event.current.control))
                {
                    methodRightClick(stateIndex, eventIndex);
                }

                else if (Event.current.button == 0)
                    methodLeftClick(eventIndex);
            }
        }
        /// <summary>
        /// Button for menu item that has a different action for left click (open item in canvas) and right click (open context menu for item).
        /// </summary>
        /// <typeparam name="T">Parameter type to pass into the method.</typeparam>
        /// <param name="methodLeftClick">Method to execute if there was a left click.</param>
        /// <param name="methodRightClick">Method to execute if there was a right click.</param>
        /// <param name="stateIndex">The index assigned to a state.</param>
        /// <param name="eventIndex">The index assigned to an event.</param>
        /// <param name="actionIndex">The index assigned to an action.</param>
        /// <param name="buttonStyle">Background style for this button.</param>
        /// <param name="buttonContent">Content style for this button.</param>
        /// <param name="buttonWidth">Width of this button.</param>
        public static void setValues<T>(UnityAction<T> methodLeftClick, UnityAction<T, T, T> methodRightClick, T stateIndex, T eventIndex, T actionIndex, GUIStyle buttonStyle, GUIContent buttonContent, int buttonWidth = 0)
        {
            // set the position of the button
            buttonStyle.fixedWidth = buttonWidth;
            Rect position = GUILayoutUtility.GetRect(buttonContent, buttonStyle);

            // draw the button
            if (GUI.Button(position, buttonContent, buttonStyle))
            {
                if (Event.current.button == 1 || (Event.current.button == 0 && Event.current.shift && Event.current.control))
                {
                    methodRightClick(stateIndex, eventIndex, actionIndex);
                }

                else if (Event.current.button == 0)
                    methodLeftClick(actionIndex);
            }
        }

        //--------------------------------------
        // Style for buttons
        //--------------------------------------

        /// <summary>
        /// Default style for buttons.
        /// </summary>      
        public static GUIStyle StyleDefault { get { return styleDefault; } }
        private static GUIStyle styleDefault;
        private static void Set_StyleDefault()
        {
            styleDefault = new GUIStyle();
        }

        /// <summary>
        /// Button style alternative A.
        /// </summary>
        public static GUIStyle StyleA { get { return styleA; } }
        private static GUIStyle styleA;
        private static void Set_StyleA()
        {
            string normal = "";
            string active = "";

            if (SimpleGUICommon.isProSkin)
            {
                normal = "iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAMAAADzjKfhAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6MzQyNTI1N0U0NTZEMTFFNzhFNTZBNjg1Mjg0QTdDNTgiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MzQyNTI1N0Q0NTZEMTFFNzhFNTZBNjg1Mjg0QTdDNTgiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIj4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5kaWQ6QUEyNTFCMTg2RDQ1RTcxMUI5NUI4NjA2QUNBNTJGODMiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6QUEyNTFCMTg2RDQ1RTcxMUI5NUI4NjA2QUNBNTJGODMiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz6LMGcgAAAAD1BMVEU9PT23t7eCgoKUlJT////v6xIUAAAABXRSTlP/////APu2DlMAAAAiSURBVHjaYmBhAAEgyczIyMgMZDIyMTExYqGh8lD1AAEGAAaQADumF6mwAAAAAElFTkSuQmCC";
                active = "iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAMAAADzjKfhAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAABVQTFRF8fHx8PDw+vr6+Pj45+fncnJy////in2hCgAAAAd0Uk5T////////ABpLA0YAAAAkSURBVHjaYmBjBQE2BlYGJmZmRlYGViYWFhZmLDRUHqoeIMAAFUgAwkR90JsAAAAASUVORK5CYII=";
            }
            else
            {
                normal = "iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAYAAADEUlfTAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAFBJREFUeNpi/P//P0NxcfF/BjTQ29vLyFBUVPT/4cOH/z99+gTHID5InAWkSlBQkAEoANclLy8PppkY8AC8kmBj379/DzcKxgcBRnyuBQgwACVNLqBePwzmAAAAAElFTkSuQmCC";
                active = "iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAMAAADzjKfhAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAABVQTFRF8fHx8PDw+vr6+Pj45+fncnJy////in2hCgAAAAd0Uk5T////////ABpLA0YAAAAkSURBVHjaYmBjBQE2BlYGJmZmRlYGViYWFhZmLDRUHqoeIMAAFUgAwkR90JsAAAAASUVORK5CYII=";
            }

            styleA = new GUIStyle();
            styleA.alignment = TextAnchor.MiddleCenter;
            styleA.normal.background = SimpleGUICommon.StringToTexture(normal); //CreatePixelTexture("test", GetColor("#ffffff"));
            styleA.active.background = SimpleGUICommon.StringToTexture(active);
            styleA.border = new RectOffset(2, 2, 2, 2);
            styleA.padding = new RectOffset(2, 2, 2, 2);
            styleA.margin = new RectOffset(2, 2, 2, 2);
            styleA.clipping = TextClipping.Overflow;
        }

        /// <summary>
        /// Button style alternative B.
        /// </summary>
        public static GUIStyle StyleB { get { return styleB; } }
        private static GUIStyle styleB;
        private static void Set_StyleB()
        {
            string normal = "iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAYAAADEUlfTAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAFBJREFUeNpi/P//P0NxcfF/BjTQ29vLyFBUVPT/4cOH/z99+gTHID5InAWkSlBQkAEoANclLy8PppkY8AC8kmBj379/DzcKxgcBRnyuBQgwACVNLqBePwzmAAAAAElFTkSuQmCC";
            string active = "iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAMAAADzjKfhAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAABVQTFRF8fHx8PDw+vr6+Pj45+fncnJy////in2hCgAAAAd0Uk5T////////ABpLA0YAAAAkSURBVHjaYmBjBQE2BlYGJmZmRlYGViYWFhZmLDRUHqoeIMAAFUgAwkR90JsAAAAASUVORK5CYII=";

            styleB = new GUIStyle();
            styleB.alignment = TextAnchor.MiddleLeft;
            styleB.normal.background = SimpleGUICommon.StringToTexture(normal); //CreatePixelTexture("test", GetColor("#ffffff"));
            styleB.active.background = SimpleGUICommon.StringToTexture(active);
            styleB.border = new RectOffset(2, 2, 2, 2);
            styleB.padding = new RectOffset(2, 2, 2, 2);
            styleB.margin = new RectOffset(2, 2, 2, 2);
            styleB.clipping = TextClipping.Overflow;
            styleB.fontSize = 10;
        }

        /// <summary>
        /// Button style alternative A.
        /// </summary>
        public static GUIStyle StyleBig { get { return styleBig; } }
        private static GUIStyle styleBig;
        private static void Set_StyleBig()
        {
            string normal = "";
            string active = "";

            if (SimpleGUICommon.isProSkin)
            {
                normal = "iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAMAAADzjKfhAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6MzQyNTI1N0U0NTZEMTFFNzhFNTZBNjg1Mjg0QTdDNTgiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MzQyNTI1N0Q0NTZEMTFFNzhFNTZBNjg1Mjg0QTdDNTgiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIj4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5kaWQ6QUEyNTFCMTg2RDQ1RTcxMUI5NUI4NjA2QUNBNTJGODMiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6QUEyNTFCMTg2RDQ1RTcxMUI5NUI4NjA2QUNBNTJGODMiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz6LMGcgAAAAD1BMVEU9PT23t7eCgoKUlJT////v6xIUAAAABXRSTlP/////APu2DlMAAAAiSURBVHjaYmBhAAEgyczIyMgMZDIyMTExYqGh8lD1AAEGAAaQADumF6mwAAAAAElFTkSuQmCC";
                active = "iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAMAAADzjKfhAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAABVQTFRF8fHx8PDw+vr6+Pj45+fncnJy////in2hCgAAAAd0Uk5T////////ABpLA0YAAAAkSURBVHjaYmBjBQE2BlYGJmZmRlYGViYWFhZmLDRUHqoeIMAAFUgAwkR90JsAAAAASUVORK5CYII=";
            }
            else
            {
                normal = "iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAYAAADEUlfTAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAFBJREFUeNpi/P//P0NxcfF/BjTQ29vLyFBUVPT/4cOH/z99+gTHID5InAWkSlBQkAEoANclLy8PppkY8AC8kmBj379/DzcKxgcBRnyuBQgwACVNLqBePwzmAAAAAElFTkSuQmCC";
                active = "iVBORw0KGgoAAAANSUhEUgAAAAcAAAAHCAMAAADzjKfhAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAABVQTFRF8fHx8PDw+vr6+Pj45+fncnJy////in2hCgAAAAd0Uk5T////////ABpLA0YAAAAkSURBVHjaYmBjBQE2BlYGJmZmRlYGViYWFhZmLDRUHqoeIMAAFUgAwkR90JsAAAAASUVORK5CYII=";
            }

            styleBig = new GUIStyle();
            styleBig.alignment = TextAnchor.MiddleCenter;
            styleBig.normal.background = SimpleGUICommon.StringToTexture(normal); //CreatePixelTexture("test", GetColor("#ffffff"));
            styleBig.active.background = SimpleGUICommon.StringToTexture(active);
            styleBig.border = new RectOffset(2, 2, 2, 2);
            styleBig.padding = new RectOffset(12, 12, 12, 12);
            styleBig.margin = new RectOffset(2, 2, 2, 2);
            styleBig.clipping = TextClipping.Overflow;
            styleBig.fontSize = 14;
            //styleBig.fontStyle = FontStyle.Bold;
        }

        /// <summary>
        /// [+ "Your Text Here"] button. Used to add new states, events, and actions in hero object menu.
        /// </summary>
        public static GUIStyle StyleAddMenuItem { get { return styleAddMenuItem; } }
        private static GUIStyle styleAddMenuItem;
        private static void Set_StyleAddMenuItem()
        {
            styleAddMenuItem = new GUIStyle();
            styleAddMenuItem.alignment = TextAnchor.MiddleLeft;
            styleAddMenuItem.border = new RectOffset(1, 1, 1, 1);
            styleAddMenuItem.padding = new RectOffset(4, 6, 2, 2);
            styleAddMenuItem.clipping = TextClipping.Overflow;

            if (SimpleGUICommon.isProSkin)
                styleAddMenuItem.normal.textColor = SimpleGUICommon.GetColor("#c4c4c4");
            else
                styleAddMenuItem.normal.textColor = SimpleGUICommon.GetColor("#595959");

            styleAddMenuItem.active.textColor = SimpleGUICommon.GetColor("#595959");
        }

        // --------------------------------------------------------------
        // Foldout Buttons
        // --------------------------------------------------------------

        /// <summary>
        /// Closed foldout.
        /// </summary>
        public static GUIStyle StyleFoldoutClosed { get { return styleFoldoutClosed; } }
        private static GUIStyle styleFoldoutClosed;
        private static void Set_StyleFoldoutClosed()
        {
            styleFoldoutClosed = new GUIStyle(EditorStyles.foldout);
            styleFoldoutClosed.padding = new RectOffset(2, 2, 2, 2);
        }

        /// <summary>
        /// Open foldout.
        /// </summary>
        public static GUIStyle StyleFoldoutOpen { get { return styleFoldoutOpen; } }
        private static GUIStyle styleFoldoutOpen;
        private static void Set_StyleFoldoutOpen()
        {
            styleFoldoutOpen = new GUIStyle(EditorStyles.foldout);
            StyleFoldoutOpen.normal.background = StyleFoldoutOpen.onNormal.background;
            StyleFoldoutOpen.active.background = StyleFoldoutOpen.onActive.background;
            StyleFoldoutOpen.padding = new RectOffset(2, 2, 2, 2);
        }

        /// <summary>
        /// Text to the right of the foldout.
        /// </summary>
        public static GUIStyle StyleFoldoutText { get { return styleFoldoutText; } }
        private static GUIStyle styleFoldoutText;
        private static void Set_StyleFoldoutText()
        {
            styleFoldoutText = new GUIStyle();
            StyleFoldoutText.padding = new RectOffset(2, 20, 2, 2);       

            if (SimpleGUICommon.isProSkin)
            {
                styleFoldoutText.normal.textColor = SimpleGUICommon.GetColor("#ffffff");
                styleFoldoutText.active.textColor = SimpleGUICommon.GetColor("#f4bc02");
                styleFoldoutText.active.background = SimpleGUICommon.GetTextureFromColor("#505050");
            }              
            else
            {
                styleFoldoutText.normal.textColor = SimpleGUICommon.GetColor("#000000");
                styleFoldoutText.active.textColor = SimpleGUICommon.GetColor("#8f0000");
                styleFoldoutText.active.background = SimpleGUICommon.GetTextureFromColor("#ffffff");
            }
        }

        /// <summary>
        /// Text to the right of the foldout. Selected + Right Clicked. 
        /// </summary>       
        public static GUIStyle StyleFoldoutTextB { get { return styleFoldoutTextB; } }
        private static GUIStyle styleFoldoutTextB;
        private static void Set_StyleFoldoutTextB()
        {
            styleFoldoutTextB = new GUIStyle(styleFoldoutText);
            styleFoldoutTextB.padding = new RectOffset(2, 2, 2, 2);

            if (SimpleGUICommon.isProSkin)
                styleFoldoutTextB.active.background = SimpleGUICommon.GetTextureFromColor("#3c3c3c");
            else
                styleFoldoutTextB.active.background = SimpleGUICommon.GetTextureFromColor("#f0f0f0");

        }

        /// <summary>
        /// Heading to the right of the foldout (Properties, States, Variables, etc)
        /// </summary>     
        public static GUIStyle StyleFoldoutHeading { get { return styleFoldoutHeading; } }
        private static GUIStyle styleFoldoutHeading;
        private static void Set_StyleFoldoutHeading()
        {
            styleFoldoutHeading = new GUIStyle(styleFoldoutText);
            styleFoldoutHeading.font = (Font)Resources.Load("JosefinSans-Bold-12");

			// custom fonts are not showing up on OSX. for now, just show default font
			if (Application.platform == RuntimePlatform.OSXEditor) {
				styleFoldoutHeading.font = EditorStyles.label.font;
				styleFoldoutHeading.fontSize = 15;
			} 

			// text color should reflect editor we are in
            if (SimpleGUICommon.isProSkin)  
                styleFoldoutHeading.normal.textColor = SimpleGUICommon.GetColor("#ffffff");
            else
                styleFoldoutHeading.normal.textColor = SimpleGUICommon.GetColor("#000000");
        }

        /// <summary>
        /// Heading to the right of the foldout. Selected + Right Clicked. 
        /// </summary>
        public static GUIStyle StyleFoldoutHeadingB { get { return styleFoldoutHeadingB; } }
        private static GUIStyle styleFoldoutHeadingB;
        private static void Set_StyleFoldoutHeadingB()
        {
            styleFoldoutHeadingB = new GUIStyle(styleFoldoutTextB);
            styleFoldoutHeadingB.font = (Font)Resources.Load("JosefinSans-Bold-12");

			// custom fonts are not showing up on OSX. for now, just show default font
			if (Application.platform == RuntimePlatform.OSXEditor) {
				styleFoldoutHeadingB.font = EditorStyles.label.font;
				styleFoldoutHeadingB.fontSize = 15;
			} 

        }

        /// <summary>
        /// Text to the right of the foldout. Disabled.
        /// </summary>  
        public static GUIStyle StyleFoldoutDisabledText { get { return styleFoldoutDisabledText; } }
        private static GUIStyle styleFoldoutDisabledText;
        private static void Set_StyleFoldoutDisabledText()
        {
            styleFoldoutDisabledText = new GUIStyle();
            styleFoldoutDisabledText.padding = new RectOffset(2, 2, 2, 2);
            styleFoldoutDisabledText.normal.textColor = SimpleGUICommon.GetColor("#A7A7A7");
        }
    }
}










