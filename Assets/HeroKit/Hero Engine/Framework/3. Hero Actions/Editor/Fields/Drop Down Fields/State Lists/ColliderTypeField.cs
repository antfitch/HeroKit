// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get collider type.
    /// </summary>
    public class ColliderTypeField : IDropDownList
    {
        // create field
        private SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // width of the field
        private int width = 200;

        // assign values to the field [change string values]
        private void PopulateField()
        {
            string name = "Collider Type: ";
            string[] items = {
                "None",
                "Box",
                "Capsule",
                "Mesh",
                "Sphere",
            };

            field.setValues(name, items);
        }

        // init the field [change constructor name]
        public ColliderTypeField() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public int SetValues(int selectedValue, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth, width);
            result = (result == 0) ? 1 : result;
            return result;
        }

    }
}