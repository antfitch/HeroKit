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
    /// Disable a game object in the scene.
    /// </summary>
    public static class TurnGameObjectOffFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 1);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetGameObjectField.BuildFieldA("Turn off this game object:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();
        }
    }
}