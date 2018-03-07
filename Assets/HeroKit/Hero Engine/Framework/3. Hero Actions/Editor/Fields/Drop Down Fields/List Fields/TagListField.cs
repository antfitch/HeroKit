// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using System.Collections.Generic;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get a list of tags.
    /// </summary>
    internal static class TagListField
    {
        // create field
        public static SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        public static void PopulateField()
        {
            List<string> tagName = new List<string>();
            for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
            {
                // get tag names
                tagName.Add("Tag " + i + ": " + UnityEditorInternal.InternalEditorUtility.tags[i]);
            }

            string name = "";
            string[] items = SimpleGUICommon.PopulateDropDownField(tagName);
            if (items.Length > 0)
                field.setValues(name, items);
            else
                field.clearValues();
        }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetValues(int selectedValue, int titleWidth)
        {
            PopulateField();   
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth);
            result = (result == 0) ? 1 : result;
            return result;
        }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetEventValues(HeroObject heroObject, int stateIndex, int eventIndex, int titleWidth)
        {
            PopulateField();
            int result = SimpleLayout.DropDownList(heroObject.states.states[stateIndex].heroEvent[eventIndex].messageSettings[1], field, titleWidth);
            return result;
        }

    }
}