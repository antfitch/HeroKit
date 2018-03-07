// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get a list of methods in a script.
    /// </summary>
    internal static class MethodListField
    {
        // create field
        public static SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        public static void PopulateField(List<MethodInfo> list)
        {
            List<string> methodNames = new List<string>(from element in list select element.Name);

            string name = "Methods";
            string[] items = SimpleGUICommon.PopulateDropDownField(methodNames);
            if (items.Length > 0)
                field.setValues(name, items);
            else
                field.clearValues();
        }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetValues(List<MethodInfo> list, int selectedValue, int titleWidth)
        {
            PopulateField(list);   
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth, HeroKitCommon.GetWidthForField(60));
            result = (result == 0 && list != null && list.Count != 0) ? 1 : result;
            return result;
        }

    }
}