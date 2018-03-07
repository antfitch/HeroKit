// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Change the settings for a 2D sprite that needs to move.
    /// </summary>
    public static class ChangeMoveSettings2DFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 23);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Change move settings for a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeSpeed = GetBoolValue.BuildField("Change move speed?", actionParams, heroAction.actionFields[5], true);
            if (changeSpeed)
            {
                SimpleLayout.Line();
                GetIntegerField.BuildFieldA("Movement Speed:", actionParams, heroAction.actionFields[2]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeMoveDirection = GetBoolValue.BuildField("Change move directions?", actionParams, heroAction.actionFields[6], true);
            if (changeMoveDirection)
            {
                SimpleLayout.Line();
                string[] items = { "4-ways (up, down, left, right)", "8-ways (up, down, left, right, up left, up right, etc)" };
                GetDropDownField.BuildField("Which directions can player move?:", actionParams, heroAction.actionFields[3], new GenericListField(items));
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeMoveFace = GetBoolValue.BuildField("Change animation type?", actionParams, heroAction.actionFields[7], true);
            if (changeMoveFace)
            {
                SimpleLayout.Line();
                string[] items2 = { "4-ways (up, down, left, right)", "8-ways (up, down, left, right, up left, up right, etc)" };
                GetDropDownField.BuildField("Which animations can player use?:", actionParams, heroAction.actionFields[4], new GenericListField(items2));
            }
            SimpleLayout.EndVertical();
        }
    }
}