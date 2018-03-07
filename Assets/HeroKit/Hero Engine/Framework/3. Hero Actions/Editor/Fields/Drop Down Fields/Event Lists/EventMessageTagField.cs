// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get contact with an object for an event.
    /// </summary>
    internal static class EventMessageTagField
    {
        // create field
        public static SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private static void PopulateField()
        {
            string name = "Other Object:";
            string[] items = {
                "Player (Tag on object is Player)",
                "Any Object But Player (Tag on object is not Player)",
                "Any Object (Tag on object is ignored)",
                "Specific Object (Tag on object is ?)"
            };

            field.setValues(name, items);
        }

        // init the field [change constructor name]
        static EventMessageTagField() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetValues(HeroObject heroObject, int stateIndex, int eventIndex, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(heroObject.states.states[stateIndex].heroEvent[eventIndex].messageSettings[0], field, titleWidth);
            result = (result == 0) ? 1 : result;
            return result;
        }

    }
}