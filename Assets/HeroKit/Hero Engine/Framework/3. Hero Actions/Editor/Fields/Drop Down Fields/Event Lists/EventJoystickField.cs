// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get input type from a joystick.
    /// </summary>
    internal static class EventJoystickField
    {
        // create field
        public static SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private static void PopulateField()
        {
            string name = "";
            string[] items = {
                "Button 0",
                "Button 1",
                "Button 2",
                "Button 3",
                "Button 4",
                "Button 5",
                "Button 6",
                "Button 7",
                "Button 8",
                "Button 9",
                "Button 10",
                "Button 11",
                "Button 12",
                "Button 13",
                "Button 14",
                "Button 15",
                "Button 16",
                "Button 17",
                "Button 18",
                "Button 19",
            };

            field.setValues(name, items);
        }

        // init the field [change constructor name]
        static EventJoystickField() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetValues(HeroObject heroObject, int stateIndex, int eventIndex, int listIndex, int conditionIndex, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(heroObject.states.states[stateIndex].heroEvent[eventIndex].inputConditions[listIndex].items[conditionIndex].key, field, titleWidth);
            result = (result == 0) ? 1 : result;
            return result; 
        }

    }
}