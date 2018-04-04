// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using HeroKit.Scene;

namespace SimpleGUI
{
    /// <summary>
    /// Layout controls for Hero Kit Editor.
    /// </summary>
    public static class SimpleLayout
    {
        /// <summary>
        /// Get the width of the field. If 0, expand width 100%.
        /// </summary>
        /// <param name="width">The width of the field.</param>
        /// <returns>The updated GUI layout option.</returns>
        private static GUILayoutOption GetFieldWidth(int width)
        {
            GUILayoutOption layout;
            if (width == 0)
                layout = GUILayout.ExpandWidth(true);
            else
                layout = GUILayout.Width(width);

            return layout;
        }

        // -----------------------------------------------------------
        // Horizontal Layout Field
        // -----------------------------------------------------------

        /// <summary>
        /// All elements after this tag are displayed on a horizontal grid.
        /// </summary>
        public static void BeginHorizontal()
        {
            GUILayout.BeginHorizontal();
        }

        /// <summary>
        /// All elements after this tag are displayed on a horizontal grid with a specific background attached to it. 
        /// </summary>
        /// <param name="boxStyle">The style assigned to the background of the vertical grid.</param>
        public static void BeginHorizontal(GUIStyle boxStyle)
        {
            GUILayout.BeginHorizontal(boxStyle);
        }

        /// <summary>
        /// All elements before this tag are displayed on a horizontal grid.
        /// </summary>
        public static void EndHorizontal()
        {
            GUILayout.EndHorizontal();
        }

        // -----------------------------------------------------------
        // Vertical Layout Field
        // -----------------------------------------------------------

        /// <summary>
        /// All elements after this tag are displayed on a vertical grid.
        /// </summary>
        public static void BeginVertical()
        {
            GUILayout.BeginVertical();
        }

        /// <summary>
        /// All elements after this tag are displayed on a vertical grid with a specific background attached to it. 
        /// </summary>
        /// <param name="boxStyle">The style assigned to the background of the vertical grid.</param>
        public static void BeginVertical(GUIStyle boxStyle)
        {
            GUILayout.BeginVertical(boxStyle);
        }

        /// <summary>
        /// All elements after this tag are displayed on a vertical grid with a specific background attached to it. 
        /// </summary>
        public static void BeginVertical(GUIStyle boxStyle, int height)
        {
            GUILayout.BeginVertical(boxStyle, GUILayout.Height(height));
        }

        /// <summary>
        /// All elements before this tag are displayed on a vertical grid.
        /// </summary>
        public static void EndVertical()
        {
            GUILayout.EndVertical();
        }

        // -----------------------------------------------------------
        // Label Field
        // -----------------------------------------------------------

        /// <summary>
        /// A label with text on it.
        /// </summary>
        public static void Label(string text, bool wordWrap = false)
        {
            text = AddTextCodes(text);

            if (wordWrap)
            {
                GUILayout.Label(text, Fields.Label.StyleDescription);
            }
            else
            {
                GUILayout.Label(text, Fields.Label.StyleDefault);
            }
        }

        /// <summary>
        /// A label with an icon and text on it.
        /// </summary>
        public static void Label(GUIContent icon)
        {
            GUILayout.Label(icon, Fields.Label.StyleDefault);
        }

        /// <summary>
        /// A label with text and a specific width.
        /// </summary>
        /// <param name="text">The text to display on the label.</param>
        /// <param name="width">The width of the label.</param>
        public static void Label(string text, int width)
        {
            text = AddTextCodes(text);
            GUILayout.Label(text, Fields.Label.StyleDefault, GUILayout.Width(width));
        }

        /// <summary>
        /// A label with text, a specific width, and style (font size, font color, text alignment, etc).
        /// </summary>
        /// <param name="text">The text to display on the label.</param>
        /// <param name="width">The width of the label.</param>
        /// <param name="style">The style assigned to the label.</param>
        public static void Label(string text, int width, GUIStyle style)
        {
            text = AddTextCodes(text);
            GUILayout.Label(text, style, GUILayout.Width(width));
        }

        /// <summary>
        /// Replace codes in a string with new values.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The parsed text.</returns>
        private static string AddTextCodes(string text)
        {
            // if new line found, replace it with correct new line (editor adds an extra \ that you don't see in text field)
            text = text.Replace("\\n", "\n");

            return text;
        }

        // -----------------------------------------------------------
        // Space Field
        // -----------------------------------------------------------

        /// <summary>
        /// A space that expands 100% between two elements.
        /// </summary>
        public static void Space()
        {
            GUILayout.FlexibleSpace();
        }

        /// <summary>
        /// A space that expands a specific amount (in pixels) between two elements.
        /// </summary>
        public static void Space(int size)
        {
            // space expands a specific amount
            GUILayout.Space(size);
        }

        // -----------------------------------------------------------
        // Horizontal Line Field
        // -----------------------------------------------------------

        /// <summary>
        /// A thin horizontal line that expands 100%.
        /// </summary>
        public static void Line()
        {
            Fields.Line.setValues();
        }

        /// <summary>
        /// A horizontal line. Color, thickness, and length are determined by the style you assign to the line.
        /// </summary>
        /// <param name="style">The style assigned to the line.</param>
        public static void Line(GUIStyle style)
        {
            Fields.Line.setValues(0, style);
        }

        // -----------------------------------------------------------
        // Button Field
        // -----------------------------------------------------------

        /// <summary>
        /// A button with text on it.
        /// </summary>
        /// <param name="text">The text on the button.</param>
        /// <param name="method">The method to execute when the button is pressed. Method has zero parameters. Ex: hello()</param>
        /// <param name="width">The width of the button.</param>
        public static void Button(string text, UnityAction method, int width = 0)
        {
            GUIContent content = Fields.Content.ContentDefault;
            content.text = text;
            GUIStyle style = Fields.Button.StyleDefault;
            Fields.Button.setValues(method, style, content, width);
        }

        /// <summary>
        /// A button with text and a specific background style assigned to it.
        /// </summary>
        /// <param name="text">The text on the button.</param>
        /// <param name="method">The method to execute when the button is pressed. Method has zero parameters. Ex: hello()</param>
        /// <param name="style">The background style assigned to the button.</param>
        /// <param name="width">The width of the button.</param>
        public static void Button(string text, UnityAction method, GUIStyle style, int width = 0)
        {
            GUIContent content = Fields.Content.ContentDefault;
            content.text = text;
            Fields.Button.setValues(method, style, content, width);
        }

        /// <summary>
        /// A button with text and a specific background style assigned to it.
        /// </summary>
        /// <typeparam name="T">The type of parameter you are passing in with the method.</typeparam>
        /// <param name="text">The text on the button.</param>
        /// <param name="method">The method to execute when the button is pressed. Method has one parameter. Ex: hello(name).</param>
        /// <param name="arg1">The first parameter to pass into the method when it is executed.</param>
        /// <param name="style">The background style assigned to the button.</param>
        /// <param name="width">The width of the button.</param>
        public static void Button<T>(string text, UnityAction<T> method, T arg1, GUIStyle style, int width = 0)
        {
            GUIContent content = Fields.Content.ContentDefault;
            content.text = text;
            Fields.Button.setValues(method, arg1, style, content, width);
        }

        /// <summary>
        /// A button with text and a specific background style assigned to it.
        /// </summary>
        /// <typeparam name="T">The parameter type used by the method.</typeparam>
        /// <param name="text"></param>
        /// <param name="methodLeftClick">The method to execute when mouse is left clicked.</param>
        /// <param name="methodRightClick">The method to execute when mouse is right clicked.</param>
        /// <param name="stateIndex">The index of the state.</param>
        /// <param name="style">The style of the button.</param>
        /// <param name="width">The width of the button.</param>
        public static void Button<T>(string text, UnityAction<T> methodLeftClick, UnityAction<T> methodRightClick, T stateIndex, GUIStyle style, int width = 0)
        {
            GUIContent content = Fields.Content.ContentDefault;
            content.text = text;
            Fields.Button.setValues(methodLeftClick, methodRightClick, stateIndex, style, content, width);
        }
        /// <summary>
        /// A button with text and a specific background style assigned to it.
        /// </summary>
        /// <typeparam name="T">The parameter type used by the method.</typeparam>
        /// <param name="text"></param>
        /// <param name="methodLeftClick">The method to execute when mouse is left clicked.</param>
        /// <param name="methodRightClick">The method to execute when mouse is right clicked.</param>
        /// <param name="stateIndex">The index of the state.</param>
        /// <param name="eventIndex">The index of the event.</param>
        /// <param name="style">The style of the button.</param>
        /// <param name="width">The width of the button.</param>
        public static void Button<T>(string text, UnityAction<T> methodLeftClick, UnityAction<T, T> methodRightClick, T stateIndex, T eventIndex, GUIStyle style, int width = 0)
        {
            GUIContent content = Fields.Content.ContentDefault;
            content.text = text;
            Fields.Button.setValues(methodLeftClick, methodRightClick, stateIndex, eventIndex, style, content, width);
        }
        /// <summary>
        /// A button with text and a specific background style assigned to it.
        /// </summary>
        /// <typeparam name="T">The parameter type used by the method.</typeparam>
        /// <param name="text"></param>
        /// <param name="methodLeftClick">The method to execute when mouse is left clicked.</param>
        /// <param name="methodRightClick">The method to execute when mouse is right clicked.</param>
        /// <param name="stateIndex">The index of the state.</param>
        /// <param name="eventIndex">The index of the event.</param>
        /// <param name="actionIndex">The index of the action.</param>
        /// <param name="style">The style of the button.</param>
        /// <param name="width">The width of the button.</param>
        public static void Button<T>(string text, UnityAction<T> methodLeftClick, UnityAction<T, T, T> methodRightClick, T stateIndex, T eventIndex, T actionIndex, GUIStyle style, int width = 0)
        {
            GUIContent content = Fields.Content.ContentDefault;
            content.text = text;
            Fields.Button.setValues(methodLeftClick, methodRightClick, stateIndex, eventIndex, actionIndex, style, content, width);
        }

        /// <summary>
        /// A button with text and a specific background style assigned to it.
        /// </summary>
        /// <typeparam name="T0">The type assigned to the first parameter you are passing in with the method.</typeparam>
        /// <typeparam name="T1">The type assigned to the second parameter you are passing in with the method.</typeparam>
        /// <param name="text">The text on the button.</param>
        /// <param name="method">The method to execute when the button is pressed. Method has one parameter. Ex: hello(firstName, lastName).</param>
        /// <param name="arg1">The first parameter to pass into the method when it is executed.</param>
        /// <param name="arg2">The second parameter to pass into the method when it is executed.</param>
        /// <param name="style">The background style assigned to the button.</param>
        /// <param name="width">The width of the button.</param>
        public static void Button<T0, T1>(string text, UnityAction<T0, T1> method, T0 arg1, T1 arg2, GUIStyle style, int width = 0)
        {
            GUIContent content = Fields.Content.ContentDefault;
            content.text = text;
            Fields.Button.setValues(method, arg1, arg2, style, content, width);
        }

        /// <summary>
        /// A button with text and a specific background style assigned to it.
        /// </summary>
        /// <typeparam name="T0">The type assigned to the first parameter you are passing in with the method.</typeparam>
        /// <typeparam name="T1">The type assigned to the second parameter you are passing in with the method.</typeparam>
        /// <typeparam name="T2">The type assigned to the third parameter you are passing in with the method.</typeparam>
        /// <param name="text">The text on the button.</param>
        /// <param name="method">The method to execute when the button is pressed. Method has one parameter. Ex: hello(firstName, lastName).</param>
        /// <param name="arg1">The first parameter to pass into the method when it is executed.</param>
        /// <param name="arg2">The second parameter to pass into the method when it is executed.</param>
        /// <param name="arg3">The third parameter to pass into the method when it is executed.</param>
        /// <param name="style">The background style assigned to the button.</param>
        /// <param name="width">The width of the button.</param>
        public static void Button<T0, T1, T2>(string text, UnityAction<T0, T1, T2> method, T0 arg1, T1 arg2, T2 arg3, GUIStyle style, int width = 0)
        {
            GUIContent content = Fields.Content.ContentDefault;
            content.text = text;
            Fields.Button.setValues(method, arg1, arg2, arg3, style, content, width);
        }

        /// <summary>
        /// A button with pre-specified content (could be text, an image, both) and a background style assigned to it.
        /// </summary>
        /// <param name="content">The GUI Content object that contains the content to display on the button.</param>
        /// <param name="method">The method to execute when the button is pressed. Method has zero parameters. Ex: hello()</param>
        /// <param name="style">The background style assigned to the button.</param>
        /// <param name="width">The width of the button.</param>
        public static void Button(GUIContent content, UnityAction method, GUIStyle style, int width = 0)
        {
            Fields.Button.setValues(method, style, content, width);
        }

        /// <summary>
        /// A button with pre-specified content (could be text, an image, both) and a background style assigned to it.
        /// </summary>
        /// <typeparam name="T">The type of parameter you are passing in with the method.</typeparam>
        /// <param name="content">The GUI Content object that contains the content to display on the button.</param>
        /// <param name="method">The method to execute when the button is pressed. Method has one parameter. Ex: hello(name).</param>
        /// <param name="arg1">The first parameter to pass into the method when it is executed.</param>
        /// <param name="style">The background style assigned to the button.</param>
        /// <param name="width">The width of the button.</param>
        public static void Button<T>(GUIContent content, UnityAction<T> method, T arg1, GUIStyle style, int width = 0)
        {
            Fields.Button.setValues(method, arg1, style, content, width);
        }

        /// <summary>
        /// A button with pre-specified content (could be text, an image, both) and a background style assigned to it.
        /// </summary>
        /// <typeparam name="T0"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="content">The GUI Content object that contains the content to display on the button.</param>
        /// <param name="method">The method to execute when the button is pressed. Method has one parameter. Ex: hello(name).</param>
        /// <param name="arg1">The first parameter to pass into the method when it is executed.</param>
        /// <param name="arg2">The second parameter to pass into the method when it is executed.</param>
        /// <param name="style">The background style assigned to the button.</param>
        /// <param name="width">The width of the button.</param>
        public static void Button<T0, T1>(GUIContent content, UnityAction<T0, T1> method, T0 arg1, T1 arg2, GUIStyle style, int width = 0)
        {
            Fields.Button.setValues(method, arg1, arg2, style, content, width);
        }

        // -----------------------------------------------------------
        // Drop-Down List Field
        // -----------------------------------------------------------

        /// <summary>
        /// A drop-down list that contains values. 
        /// Note: Only one value can be selected at a time.
        /// </summary>
        /// <param name="selectedValue">The currently selected value in the drop down list.</param>
        /// <param name="list">The list of values that populates the drop down list.</param>
        /// <param name="titleWidth">The width of the title assigned to the drop down list. If set to 0, the title is hidden.</param>
        /// <param name="fieldWidth">The width of the drop-down list.</param>
        /// <returns>The selection in the drop-down list.</returns>
        public static int DropDownList(int selectedValue, Fields.DropDownValues list, int titleWidth, int fieldWidth = 0, bool lenIsListID = false)
        {
            if (titleWidth == 0) titleWidth = -3;

            // exit early if list is null or empty
            if (list.items == null || list.items.Length == 0)
            {
                BeginHorizontal();
                Label("[No Items in List]");
                EndHorizontal();
                return 0;
            }

            // get the length of the list
            int listLength = list.ids.Length;

            // if size of list does not represent all list.ids, get the id of the last item in list
            if (lenIsListID)
                listLength = list.ids[listLength-1];

            // if the selected value is out of range, set it to 0
            if (selectedValue > listLength)
                selectedValue = 0;

            BeginHorizontal();
            Label(list.name, titleWidth);

            int result = EditorGUILayout.IntPopup(selectedValue, list.items, list.ids, GetFieldWidth(fieldWidth));

            EndHorizontal();
            return result;
        }

        /// <summary>
        /// A drop down list that contains values. 
        /// Note: Only one value can be selected at a time.
        /// </summary>
        /// <param name="selectedValue">The currently selected value in the drop down list.</param>
        /// <param name="list">The list of values that populates the drop down list.</param>
        /// <param name="title">The title for the drop-down list.</param>
        /// <returns>The selection in the drop-down list.</returns>
        public static int DropDownList(int selectedValue, Fields.DropDownValues list, string title)
        {
            // exit early if list is null or empty
            if (list.items == null || list.items.Length == 0)
            {
                return 0;
            }

            // if the selected value is out of range, set it to 0
            if (selectedValue > list.items.Length)
                selectedValue = 0;

            int result = EditorGUILayout.IntPopup(title, selectedValue, list.items, list.ids);

            return result;
        }

        // -----------------------------------------------------------
        // Text Fields, Text Areas
        // -----------------------------------------------------------

        /// <summary>
        /// A field that can contain a string of text.
        /// </summary>
        /// <param name="fieldValue">The value that appears in the field.</param>
        /// <param name="style">The style to apply to the field.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <param name="fieldHeight">The field of the field.</param>
        /// <returns>The string assigned to the field.</returns>
        public static string TextField(string fieldValue, GUIStyle style, int fieldWidth, int fieldHeight)
        {
            string text = fieldValue;
            string result = EditorGUILayout.TextField(text, style, GetFieldWidth(fieldWidth), GUILayout.Height(fieldHeight));

            return result;
        }

        /// <summary>
        /// A text field that can contain a string.
        /// </summary>
        /// <param name="fieldValue">The text field that contains the string.</param>
        /// <param name="width">The width of the text field.</param>
        /// <returns>The string assigned to the field.</returns>
        public static string TextField(string fieldValue, int width = 400)
        {
            string value = fieldValue;
            int height = 20;
            GUIStyle bodyStyle = Fields.TextBox.StyleA;
            string result = EditorGUILayout.TextField("", value, bodyStyle, GetFieldWidth(width), GUILayout.Height(height));
            return result;
        }

        /// <summary>
        /// A text field that can contain an string.
        /// </summary>
        /// <param name="title">The title that appears next to the field.</param>
        /// <param name="titleWidth">The width of the title.</param>
        /// <param name="fieldValue">The string that appears in the field.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The string assigned to the field.</returns>
        public static string TextField(string title, int titleWidth, string fieldValue, int fieldWidth)
        {
            BeginHorizontal();

            Label(title + ":", titleWidth);
            string result = EditorGUILayout.TextField("", fieldValue, GUILayout.Width(fieldWidth));

            EndHorizontal();

            return result;
        }

        /// <summary>
        /// A multi-line field that can contain a string.
        /// </summary>
        /// <param name="fieldValue">The field that contains the string.</param>
        /// <param name="width">The width of the field.</param>
        /// <returns>The string assigned to the field.</returns>
        public static string TextArea(string fieldValue, int width = 400, int height = 100)
        {
            string value = fieldValue;
            GUIStyle bodyStyle = Fields.TextBox.StyleA;
            bodyStyle.wordWrap = true;
            string result = EditorGUILayout.TextArea(value, bodyStyle, GUILayout.Width(width), GUILayout.Height(height));
            bodyStyle.wordWrap = false;
            return result;
        }

        // -----------------------------------------------------------
        // Float Fields, Integer Fields
        // -----------------------------------------------------------

        /// <summary>
        /// A field that can contain a float.
        /// </summary>
        /// <param name="fieldValue">The field that contains the float.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The float assigned to the field.</returns>
        public static float FloatField(float fieldValue, int fieldWidth = 100)
        {
            int height = 20;
            float result = EditorGUILayout.FloatField("", fieldValue, GUILayout.Width(fieldWidth), GUILayout.Height(height));
            return result;
        }

        /// <summary>
        /// A field that can contain a float.
        /// </summary>
        /// <param name="title">The title that appears next to the field.</param>
        /// <param name="titleWidth">The width of the title.</param>
        /// <param name="fieldValue">The float that appears in the field.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The float assigned to the field.</returns>
        public static float FloatField(string title, int titleWidth, float fieldValue, int fieldWidth)
        {
            BeginHorizontal();

            Label(title + ":", titleWidth);
            float result = EditorGUILayout.FloatField("", fieldValue, GUILayout.Width(fieldWidth));

            EndHorizontal();

            return result;
        }

        /// <summary>
        /// A field that can contain an integer.
        /// </summary>
        /// <param name="fieldValue">The field that contains the integer.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The IntField assigned to the field.</returns>
        public static int IntField(int fieldValue, int fieldWidth = 100)
        {
            int bodyWidth = fieldWidth;
            int height = 20;
            int value = fieldValue;
            GUIStyle bodyStyle = Fields.TextBox.StyleA;
            int result = EditorGUILayout.IntField("", value, bodyStyle, GUILayout.Width(bodyWidth), GUILayout.Height(height));

            return result;
        }

        /// <summary>
        /// A field that can contain an integer.
        /// </summary>
        /// <param name="fieldValue">The field that contains the integer.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <param name="min">The lowest value on the slider.</param>
        /// <param name="max">The highest value on the slider.</param>
        /// <returns>The IntField assigned to the field.</returns>
        public static float SliderField(float fieldValue, int fieldWidth, float min, float max)
        {
            int bodyWidth = fieldWidth;
            int height = 20;
            float value = fieldValue;
            float result = EditorGUILayout.Slider("", value, min, max, GUILayout.Width(bodyWidth), GUILayout.Height(height));

            return result;
        }

        /// <summary>
        /// A field that can contain an integer.
        /// </summary>
        /// <param name="fieldValue">The field that contains the integer.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <param name="min">The lowest value on the slider.</param>
        /// <param name="max">The highest value on the slider.</param>
        /// <returns>The IntField assigned to the field.</returns>
        public static int IntSliderField(int fieldValue, int fieldWidth, int min, int max)
        {
            int bodyWidth = fieldWidth;
            int height = 20;
            int value = fieldValue;
            int result = EditorGUILayout.IntSlider("", value, min, max, GUILayout.Width(bodyWidth), GUILayout.Height(height));

            return result;
        }

        /// <summary>
        /// A field that can contain an integer.
        /// </summary>
        /// <param name="title">The title that appears next to the field.</param>
        /// <param name="titleWidth">The width of the title.</param>
        /// <param name="fieldValue">The integer that appears in the field.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The integer assigned to the field.</returns>
        public static int IntField(string title, int titleWidth, int fieldValue, int fieldWidth)
        {
            BeginHorizontal();

            Label(title + ":", titleWidth);
            int result = EditorGUILayout.IntField("", fieldValue, GUILayout.Width(fieldWidth));

            EndHorizontal();

            return result;
        }

        // -----------------------------------------------------------
        // Bool Fields
        // -----------------------------------------------------------

        /// <summary>
        /// A field that can contain a bool.
        /// </summary>
        /// <param name="title">The title that appears next to the field.</param>
        /// <param name="titleWidth">The width of the title.</param>
        /// <param name="fieldValue">The bool that appears in the field.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The bool assigned to the field.</returns>
        public static bool BoolField(string title, int titleWidth, bool fieldValue, int fieldWidth)
        {
            BeginHorizontal();

            Label(title + ":", titleWidth);
            bool result = EditorGUILayout.Toggle("", fieldValue, GUILayout.Width(fieldWidth));

            EndHorizontal();

            return result;
        }

        /// <summary>
        /// A field that can contain a bool.
        /// </summary>
        /// <param name="fieldValue">The field that contains the bool.</param>
        /// <returns>The IntField assigned to the field.</returns>
        public static bool BoolField(bool fieldValue)
        {
            bool value = fieldValue;
            bool result = EditorGUILayout.Toggle("", value, GUILayout.MaxWidth(10.0f));
            return result;
        }

        // -----------------------------------------------------------
        // Color Fields
        // -----------------------------------------------------------

        /// <summary>
        /// A field that can contain a color.
        /// </summary>
        /// <param name="fieldValue">The field that contains the color.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The ColorField assigned to the field.</returns>
        public static Color ColorField(Color fieldValue, int fieldWidth = 100)
        {
            Color value = fieldValue;
            Color result = EditorGUILayout.ColorField("", value, GUILayout.Width(fieldWidth));
            return result;
        }

        public static string ColorField(string fieldValue, int fieldWidth = 100)
        {
            Color value = SimpleGUICommon.GetColor(fieldValue);
            try
            {              
                Color result = EditorGUILayout.ColorField(value, GUILayout.Width(fieldWidth));
                fieldValue = "#" + SimpleGUICommon.GetHexFromColor(result);
            }
            catch (System.Exception e)
            {
                //Debug.LogException(e);
                //return fieldValue;
            }

            return fieldValue;
        }


        // -----------------------------------------------------------
        // Mask Fields
        // -----------------------------------------------------------

        /// <summary>
        /// A field that can contain a layer mask.
        /// </summary>
        /// <param name="fieldValue">The layer mask.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The layer mask.</returns>
        public static int LayerMaskField(int fieldValue, int fieldWidth)
        {
            // create the layer mask
            string[] items = new string[32];

            // get layers that have names
            for (int i = 0; i < 32; i++)
            {
                items[i] = "Layer " + i + ": " + LayerMask.LayerToName(i);
            }

            fieldValue = EditorGUILayout.MaskField("", fieldValue, items, GUILayout.Width(fieldWidth));

            return fieldValue;
        }

        /// <summary>
        /// A field that can contain a tag mask.
        /// </summary>
        /// <param name="fieldValue">The tag mask.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The tag mask.</returns>
        public static int TagMaskField(int fieldValue, int fieldWidth)
        {
            // create the layer mask
            string[] items = new string[UnityEditorInternal.InternalEditorUtility.tags.Length];

            // get layers that have names
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = "Tag " + i + ": " + UnityEditorInternal.InternalEditorUtility.tags[i];
            }

            fieldValue = EditorGUILayout.MaskField("", fieldValue, items, GUILayout.Width(fieldWidth));

            return fieldValue;
        }

        // -----------------------------------------------------------
        // Game Object Fields
        // -----------------------------------------------------------

        /// <summary>
        /// A field that can contain a game object.
        /// </summary>
        /// <param name="title">The title that appears next to the field.</param>
        /// <param name="titleWidth">The width of the title.</param>
        /// <param name="fieldValue">The game object that appears in the field.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The game object assigned to the field.</returns>
        public static GameObject GameObjectField(string title, int titleWidth, GameObject fieldValue, int fieldWidth)
        {
            BeginHorizontal();

            Label(title + ":", titleWidth);

            GameObject result = EditorGUILayout.ObjectField(fieldValue, typeof(GameObject), true, GUILayout.Width(fieldWidth)) as GameObject;

            if (result != null)
            {
                if (result.scene.name != null)
                {
                    Debug.Log(result.name + " in scene " + result.scene.name);
                }
                else
                {
                    Debug.Log(result.name + " not in scene. Can't add to action.");
                    result = null;
                }
            }

            EndHorizontal();

            return result;
        }

        // -----------------------------------------------------------
        // Object Fields
        // -----------------------------------------------------------

        /// <summary>
        /// A field that can contain an object.
        /// </summary>
        /// <typeparam name="T">The field value type.</typeparam>
        /// <param name="title">The title that appears next to the field.</param>
        /// <param name="titleWidth">The width of the title.</param>
        /// <param name="fieldValue">The object that appears in the field.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The object assigned to the field.</returns>
        public static T ObjectField<T>(string title, int titleWidth, T fieldValue, int fieldWidth, bool allowSceneObjects = true) where T : Object
        {
            BeginHorizontal();

            Label(title + ":", titleWidth);
            T result = EditorGUILayout.ObjectField(fieldValue, typeof(T), allowSceneObjects, GUILayout.Width(fieldWidth)) as T;

            EndHorizontal();

            return result;
        }

        /// <summary>
        /// A field that can contain a object.
        /// </summary>
        /// <typeparam name="T">The field value type.</typeparam>
        /// <param name="fieldValue">The object that appears in the field.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <param name="allowSceneObjects"></param>
        /// <returns>The object in the field.</returns>
        public static T ObjectField<T>(T fieldValue, int fieldWidth = 0, bool allowSceneObjects = false) where T : Object
        {
            T result = EditorGUILayout.ObjectField(fieldValue, typeof(T), allowSceneObjects, GetFieldWidth(fieldWidth)) as T;
            return result;
        }

        // -----------------------------------------------------------
        // Hero List Fields
        // -----------------------------------------------------------

        /// <summary>
        /// A field that can contain an integer from an IntField object. An IntField exists inside an IntList object.
        /// </summary>
        /// <param name="intField">The IntField object that contains the integer that appears in the field.</param>
        /// <param name="index">The slot assigned to the IntField in its IntList object.</param>
        /// <returns>The IntField assigned to the field.</returns>
        public static int IntListField(IntField intField, int index)
        {
            int bodyWidth = 100;
            int height = 20;
            int value = intField.value;
            GUIStyle bodyStyle = Fields.TextBox.StyleA;

            BeginHorizontal();
            int result = EditorGUILayout.IntField("", value, bodyStyle, GUILayout.Width(bodyWidth), GUILayout.Height(height));
            EndHorizontal();

            return result;
        }

        /// <summary>
        /// A field that can contain a float from a FloatField object. A FloatField exists inside an FloatList object.
        /// </summary>
        /// <param name="floatField">The FloatField object that contains the float that appears in the field.</param>
        /// <param name="index">The slot assigned to the FloatField in its FloatList object.</param>
        /// <returns>The FloatField assigned to the field.</returns>
        public static float FloatListField(FloatField floatField, int index)
        {
            int bodyWidth = 100;
            int height = 20;
            float value = floatField.value;
            GUIStyle bodyStyle = Fields.TextBox.StyleA;

            BeginHorizontal();
            float result = EditorGUILayout.FloatField("", value, bodyStyle, GUILayout.Width(bodyWidth), GUILayout.Height(height));
            EndHorizontal();

            return result;
        }

        /// <summary>
        /// A field that can contain a bool from a BoolField object. A BoolField exists inside a BoolList object.
        /// </summary>
        /// <param name="boolField">The BoolField object that contains the bool that appears in the field.</param>
        /// <param name="index">The slot assigned to the BoolField in its BoolList object.</param>
        /// <returns>The BoolField assigned to the field.</returns>
        public static bool BoolListField(BoolField boolField, int index)
        {
            int bodyWidth = 15;
            int height = 20;
            bool value = boolField.value;

            BeginHorizontal();
            bool result = EditorGUILayout.Toggle("", value, GUILayout.Width(bodyWidth), GUILayout.Height(height));
            EndHorizontal();

            return result;
        }

        /// <summary>
        /// A field that can contain a description for a list field (IntField, BoolField, etc).
        /// </summary>
        /// <param name="stringField">The text in the field.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The text in the field.</returns>
        public static string StringListField(string stringField, int fieldWidth = 400, bool useTextBox = false)
        {
            if (!useTextBox)
                return TextField(stringField, Fields.TextBox.StyleA, fieldWidth, 20);
            else
                return TextArea(stringField, fieldWidth);
        }

        /// <summary>
        /// A field that can contain a hero object from a Hero Object List object. A GameObjectField exists inside a Hero Object List object.
        /// </summary>
        /// <param name="heroObjectField">The HeroObjectField object that contains the hero object that appears in the field.</param>
        /// <param name="index">The slot assigned to the HeroObjectField in its HeroObjectList object.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The HeroObjectField assigned to the field.</returns>
        public static HeroObject HeroObjectListField(HeroObjectField heroObjectField, int index, int fieldWidth)
        {
            HeroObject value = heroObjectField.value;

            BeginHorizontal();
            HeroObject result = EditorGUILayout.ObjectField(value, typeof(HeroObject), false, GUILayout.Width(fieldWidth)) as HeroObject;
            EndHorizontal();

            return result;
        }

        /// <summary>
        /// A list field that can contain an object.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="fieldValue">The object in the list field.</param>
        /// <param name="index">The index of the object field in the list.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The object in the list field.</returns>
        public static T ObjectListField<T>(T fieldValue, int index, int fieldWidth) where T : Object
        {
            int height = 20;
            BeginHorizontal();
            T result = EditorGUILayout.ObjectField(fieldValue, typeof(T), false, GUILayout.Width(fieldWidth), GUILayout.Height(height)) as T;
            EndHorizontal();

            return result;
        }

        /// <summary>
        /// A list field for Unity Objects.
        /// </summary>
        /// <param name="unityObjectField">The Unity Object field.</param>
        /// <param name="objectType">The type of unity object that can used in this field.</param>
        /// <param name="fieldWidth">The width of the field.</param>
        /// <returns>The unity object in the list field.</returns>
        public static Object UnityObjectListField(UnityObjectField unityObjectField, int objectType, int fieldWidth)
        {
            int height = 20;
            Object value = unityObjectField.value;

            BeginHorizontal();
            Object result = EditorGUILayout.ObjectField(value, typeof(Object), false, GUILayout.Width(fieldWidth), GUILayout.Height(height));
            EndHorizontal();

            return result;
        }
    }
}

    




