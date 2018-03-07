// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get the location of a hero kit object.
    /// </summary>
    public class HeroObjectTypeFieldB : IDropDownList
    {
        // create field
        private SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private void PopulateField()
        {
            string name = "Object";
            string[] items = {
                "Value",
                "This Hero Object",
                "Hero Object (In Variable List)",
                "Hero Object (In Scene)",
                "Hero Object (In Property List)",             
                "Hero Object (In Global List)"
            };

            field.setValues(name, items);
        }

        // init the field [change constructor name]
        public HeroObjectTypeFieldB() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public int SetValues(int selectedValue, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth, 150);
            result = (result == 0) ? 1 : result;
            return result;
        }

    }
}