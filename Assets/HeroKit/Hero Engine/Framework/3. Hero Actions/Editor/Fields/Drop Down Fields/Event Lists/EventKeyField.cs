// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get input type from a keyboard.
    /// </summary>
    internal static class EventKeyField
    {
        // create field
        public static SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private static void PopulateField()
        {
            string name = "";
            string[] items = {
                "Left Arrow Key",
                "Right Arrow Key",
                "Up Arrow Key",
                "Down Arrow Key",
                "Ctrl (Left)",
                "Ctrl (Right)",
                "Shift (Left)",
                "Shift (Right)",
                "Space",
                "Enter",
                "Backspace",
                "Tab",
                "Caps Lock",
                "Escape",
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H",
                "I",
                "J",
                "K",
                "L",
                "M",
                "N",
                "O",
                "P",
                "Q",
                "R",
                "S",
                "T",
                "U",
                "V",
                "W",
                "X",
                "Y",
                "Z",

            };

            field.setValues(name, items);
        }

        // init the field [change constructor name]
        static EventKeyField() { PopulateField(); }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetValues(HeroObject heroObject, int stateIndex, int eventIndex, int listIndex, int conditionIndex, int titleWidth)
        {
            int result = SimpleLayout.DropDownList(heroObject.states.states[stateIndex].heroEvent[eventIndex].inputConditions[listIndex].items[conditionIndex].key, field, titleWidth);
            result = (result == 0) ? 1 : result;
            return result;
        }  

    }
}