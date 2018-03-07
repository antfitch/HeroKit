// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get key/mouse/button press type.
    /// </summary>
    internal static class EventPressTypeField
    {
        // create field
        public static SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private static void PopulateField()
        {
            string name = "";
            string[] items = {
                "On press begin",
                "On pressing",
                "On press end",
                "On double press",
                "On drag"
            };

            field.setValues(name, items);
        }

        // init the field [change constructor name]
        static EventPressTypeField() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetValues(HeroObject heroObject, int stateIndex, int eventIndex, int listIndex, int conditionIndex, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(heroObject.states.states[stateIndex].heroEvent[eventIndex].inputConditions[listIndex].items[conditionIndex].pressType, field, titleWidth);
            result = (result == 0) ? 1 : result;
            return result;
        }

    }
}