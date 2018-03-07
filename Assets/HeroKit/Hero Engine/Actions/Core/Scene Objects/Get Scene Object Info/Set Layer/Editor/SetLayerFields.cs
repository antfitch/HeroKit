// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using HeroKit.Editor.HeroField;
using UnityEngine;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Assign an object to a new layer.
    /// </summary>
    public static class SetLayerFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 3);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetSceneObjectValue.BuildField("layer", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            string[] items = new string[32];
            for (int i = 0; i < 32; i++)
            {
                items[i] = "Layer " + i + ": " + LayerMask.LayerToName(i);
            }
            GetDropDownField.BuildField("Use this layer:", actionParams, heroAction.actionFields[2], new GenericListField(items));
            SimpleLayout.EndVertical();
        }
    }
}