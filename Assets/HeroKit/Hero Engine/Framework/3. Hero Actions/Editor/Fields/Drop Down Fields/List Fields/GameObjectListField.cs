// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using UnityEngine;
using System.Collections.Generic;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get a list of game object fields.
    /// </summary>
    internal static class GameObjectListField
    {
        // create field
        public static SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        public static void PopulateField(List<GameObjectField> list)
        {
            string name = "Game Objects";
            string[] items = SimpleGUICommon.PopulateDropDownField<GameObjectField, GameObject>(list, "GO:");
            if (items.Length > 0)
                field.setValues(name, items);
            else
                field.clearValues();
        }

        // pass field into a drop down list [change first value in DropDownList]
        public static int SetValues(List<GameObjectField> list, int selectedValue, int titleWidth)
        {
            PopulateField(list);   
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth);
            result = (result == 0 && list != null && list.Count != 0) ? 1 : result;
            return result;
        }

    }
}