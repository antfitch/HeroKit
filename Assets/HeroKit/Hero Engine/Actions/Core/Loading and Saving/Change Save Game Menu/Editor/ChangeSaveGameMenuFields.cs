﻿// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Change the save and load game menu.
    /// </summary>
    public static class ChangeSaveGameMenuFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 2);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetPrefabValue.BuildField("Save Menu template to use:", actionParams, heroAction.actionFields[0]);
            GetPrefabValue.BuildField("Save Slot template to use:", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();
        }
    }
}