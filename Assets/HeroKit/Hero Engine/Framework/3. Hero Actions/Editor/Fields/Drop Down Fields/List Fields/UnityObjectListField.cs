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
    /// Drop down list. Get a list of unity object fields.
    /// </summary>
    internal static class UnityObjectListField
    {
        // create field
        public static SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        public static void PopulateField(List<UnityObjectField> list)
        {
            string name = "Unity Objects";
            string[] items = SimpleGUICommon.PopulateDropDownField<UnityObjectField, UnityEngine.Object>(list, "UO:");

            // append the type of item stored
            for (int i = 0; i < items.Length; i++)
            {
                // create item names
                string itemType = "";
                switch (list[i].objectType)
                {
                    case 1:
                        itemType = "Audio Clip";
                        break;
                    case 2:
                        itemType = "Sprite";
                        break;
                    case 3:
                        itemType = "Scene";
                        break;
                    case 4:
                        itemType = "Particle System";
                        break;
                    case 5:
                        itemType = "Mono Script";
                        break;
                }
                items[i] += " (" + itemType + ")";
            }

            if (items.Length > 0)
                field.setValues(name, items);
            else
                field.clearValues();
        }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetValues(List<UnityObjectField> list, int selectedValue, int titleWidth)
        {
            PopulateField(list);   
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth);
            result = (result == 0 && list != null && list.Count != 0) ? 1 : result;
            return result;
        }
    }
}