// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get a list of values. Values must be supplied by developer as a string array.
    /// </summary>
    public class GenericListField : IDropDownList
    {
        // create field
        private SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private void PopulateField(string[] items)
        {
            string name = "Items:";

            if (items.Length > 0)
                field.setValues(name, items);
            else
                field.clearValues();
        }

        // init the field [change constructor name]
        public GenericListField(string[] list) { PopulateField(list); }

        // pass field into a drop down list [change first value in DropDownList]
        public int SetValues(int selectedValue, int titleWidth)
        { 
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth);
            result = (result == 0) ? 1 : result;
            return result;
        }

        // pass field into a drop down list [change first value in DropDownList]
        public int SetValuesInspector(int selectedValue, string title)
        {
            int result = SimpleLayout.DropDownList(selectedValue, field, title);
            result = (result == 0) ? 1 : result;
            return result;
        }

    }
}