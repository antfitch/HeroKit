// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get event run type.
    /// </summary>
    internal static class EventTypeField
    {
        // create field
        public static SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private static void PopulateField()
        {
            string name = "When to run:";
            string[] items = {
                "Loop (general)",
                "Loop (physics, animation, movement)",
                "Autoplay (play once when state starts)",
                "Called by an action",
                "Receive input (mouse, keyboard, joystick)",
                "Encounter another object",
            };

            field.setValues(name, items);
        }

        // init the field [change constructor name]
        static EventTypeField() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetValues(HeroObject heroObject, int stateIndex, int eventIndex, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(heroObject.states.states[stateIndex].heroEvent[eventIndex].eventType, field, titleWidth);
            result = (result == 0) ? 1 : result;
            return result;
        }
    }
}