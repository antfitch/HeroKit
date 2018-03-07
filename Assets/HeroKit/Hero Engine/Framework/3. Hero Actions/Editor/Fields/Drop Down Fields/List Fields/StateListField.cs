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
    /// Drop down list. Get a list of states.
    /// </summary>
    public class StateListField : IDropDownListB<HeroState>
    {
        // create field
        private SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // get width
        private int width = 300;

        // assign values to the field [change string values]
        private void PopulateField(List<HeroState> list)
        {
            string name = "State";
            string[] items = SimpleGUICommon.PopulateStateDropDownField(list, "S:");
            if (items.Length > 0)
                field.setValues(name, items);
            else
                field.clearValues();
        }

        // pass field into a drop down list [change first value in DropDownList]
        public int SetValues(int selectedValue, List<HeroState> list, int titleWidth)
        {
            PopulateField(list);   
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth, width);
            return result;
        }

        // pass field into a drop down list [change first value in DropDownList]
        public int SetValuesInspector(int selectedValue, List<HeroState> list, string title)
        {
            PopulateField(list);
            int result = SimpleLayout.DropDownList(selectedValue, field, title);
            result = (result == 0 && list != null && list.Count != 0) ? 1 : result;
            return result;
        }
    }
}