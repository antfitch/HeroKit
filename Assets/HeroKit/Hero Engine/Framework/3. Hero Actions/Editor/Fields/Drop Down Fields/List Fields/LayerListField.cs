// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;
using System.Collections.Generic;
using UnityEngine;

namespace HeroKit.Editor.HeroField
{
    /// <summary>
    /// Drop down list. Get a list of layers.
    /// </summary>
    public class LayerListField : IDropDownList
    {
        // create field
        private SimpleGUI.Fields.DropDownValues field = new SimpleGUI.Fields.DropDownValues();

        // assign values to the field [change string values]
        private void PopulateField()
        {
            List<string> layerName = new List<string>();
            for (int i = 0; i < 32; i++)
            { // get layer names
                layerName.Add("Layer " + i + ": " + LayerMask.LayerToName(i));
            }

            string name = "Layers";
            string[] items = SimpleGUICommon.PopulateDropDownField(layerName);
            if (items.Length > 0)
                field.setValues(name, items);
            else
                field.clearValues();
        }

        // pass field into a drop down list [change first value in DropDownList]
        public int SetValues(int selectedValue, int titleWidth)
        {
            PopulateField();   
            int result = SimpleLayout.DropDownList(selectedValue, field, titleWidth);
            result = (result == 0) ? 1 : result;
            return result;
        }

    }
}