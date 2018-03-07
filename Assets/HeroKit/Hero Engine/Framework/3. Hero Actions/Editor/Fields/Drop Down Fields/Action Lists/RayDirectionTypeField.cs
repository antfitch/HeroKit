// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get the direction to shoot a ray.
    /// </summary>
    public class RayDirectionTypeField : IDropDownList
    {
        // create field
        private SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private void PopulateField()
        {
            string name = "Direction";
            string[] items = {
                "Shoot from the front of the object",
                "Shoot from the back of the object",
                "Shoot from the top of the object",
                "Shoot from the bottom of the object",
                "Shoot from the left of the object",
                "Shoot from the right of the object",
            };

            field.setValues(name, items);
        }

        // init the field [change constructor name]
        public RayDirectionTypeField() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public int SetValues(int selectedValue, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth, 300);
            result = (result == 0) ? 1 : result;
            return result;
        }

    }
}