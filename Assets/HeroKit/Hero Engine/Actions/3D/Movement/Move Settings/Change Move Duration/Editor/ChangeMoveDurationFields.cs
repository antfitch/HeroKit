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
    /// Change the duration of a move.
    /// </summary>
    public static class ChangeMoveDurationFields
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
            GetHeroObjectField.BuildFieldE("Change move settings for a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetFloatField.BuildFieldA("Duration of move in seconds:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();
        }
    }
}