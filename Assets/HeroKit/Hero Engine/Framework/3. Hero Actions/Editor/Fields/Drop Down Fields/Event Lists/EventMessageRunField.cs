// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get event run settings.
    /// </summary>
    internal static class EventMessageRunField
    {
        // create field
        public static SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private static void PopulateField()
        {
            string name = "";
            string[] items = {
                "Run this event once",
                "Loop this event",
            };

            field.setValues(name, items);
        }

        // init the field [change constructor name]
        static EventMessageRunField() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetValues(HeroObject heroObject, int stateIndex, int eventIndex, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(heroObject.states.states[stateIndex].heroEvent[eventIndex].messageSettings[3], field, titleWidth);
            result = (result == 0) ? 1 : result;
            return result;
        }

    }
}