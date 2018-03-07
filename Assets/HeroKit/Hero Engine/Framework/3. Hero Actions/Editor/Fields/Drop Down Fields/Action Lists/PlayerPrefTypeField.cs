// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get the type of player pref value to work with.
    /// </summary>
    public class PlayerPrefTypeField : IDropDownList
    {
        // create field
        private SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private void PopulateField()
        {
            string name = "Value to Change:";
            string[] items = {
                "Integer",
                "Float",
                "Bool",
                "String",
            };
            
            field.setValues(name, items);
        }

        // init the field [change constructor name]
        public PlayerPrefTypeField() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public int SetValues(int selectedValue, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth, 70);
            result = (result == 0) ? 1 : result;
            return result;
        }

    }
}