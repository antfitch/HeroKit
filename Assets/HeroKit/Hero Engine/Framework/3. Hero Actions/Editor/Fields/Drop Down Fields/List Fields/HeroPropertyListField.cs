// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get a list of properties in a script.
    /// </summary>
    internal static class HeroPropertyListField
    {
        // create field
        public static SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        public static void PopulateField(HeroProperties[] list)
        {
            string name = "Properties";
            List<string> itemNames = new List<string>();
            for (int i = 0; i < list.Length; i++)
            {
                string abbreviation = "[" + "P" + i + "] ";
                string heroPropertyName = (list[i].propertyTemplate != null) ? abbreviation + list[i].propertyTemplate.name : abbreviation + "[none]";
                itemNames.Add(heroPropertyName);
            }

            string[] items = SimpleGUICommon.PopulateDropDownField(itemNames);
            if (items.Length > 0)
                field.setValues(name, items);
            else
                field.clearValues();
        }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetValues(HeroProperties[] list, int selectedValue, int titleWidth)
        {
            PopulateField(list);
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth, 150);
            result = (result == 0 && list != null && list.Length != 0) ? 1 : result;
            return result;
        }

    }
}