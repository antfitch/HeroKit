// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Change the position of a hero kit object or one of its children.
    /// </summary>
    public static class ChangePosition2DFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 10);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Change position of a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetChildObjectField.BuildField("Change position of a child object?", actionParams, heroAction.actionFields[2], heroAction.actionFields[3]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeX = GetBoolValue.BuildField("Change X Pos?", actionParams, heroAction.actionFields[4], true);
            if (changeX) GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[5]);
            bool changeY = GetBoolValue.BuildField("Change Y Pos?", actionParams, heroAction.actionFields[6], true);
            if (changeY) GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[7]);
            bool changeZ = GetBoolValue.BuildField("Change Z Pos?", actionParams, heroAction.actionFields[8], true);
            if (changeZ) GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[9]);
            SimpleLayout.EndVertical();

        }
    }
}