// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get image type.
    /// </summary>
    public class ImageTypeField : IDropDownList
    {
        // create field
        private SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // width of the field
        private int width = HeroKitCommon.GetWidthForField(42);

        // assign values to the field [change string values]
        private void PopulateField()
        {
            string name = "Image Type: ";
            string[] items = {
                "Use what is already on the GameObject",              
                "Import from a Prefab", 
                "None"
            };

            field.setValues(name, items);
        }

        // init the field [change constructor name]
        public ImageTypeField() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public int SetValues(int selectedValue, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth, width);
            result = (result == 0) ? 1 : result;
            return result;
        }

    }
}