// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get a value type.
    /// </summary>
    public class ValueTypeField : IDropDownList
    {
        // create field
        private SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private void PopulateField()
        {
            string name = "Type";
            string[] items = {
                "Value",
                "Variable",
                "Property",
                "Global"
            };

            field.setValues(name, items);
        }

        // init the field [change constructor name]
        public ValueTypeField() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public int SetValues(int selectedValue, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth, 70);
            result = (result == 0) ? 1 : result;
            return result;
        }

    }
}