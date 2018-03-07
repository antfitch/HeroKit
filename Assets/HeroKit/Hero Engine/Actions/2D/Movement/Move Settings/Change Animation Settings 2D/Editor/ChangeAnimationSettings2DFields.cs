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
    public static class ChangeAnimationSettings2DFields
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
            bool changeAnims = GetBoolValue.BuildField("Change Movement Animation?", actionParams, heroAction.actionFields[1], true);
            if (changeAnims)
            {
                SimpleLayout.Line();
                SimpleLayout.Label("Enter the name of the bool for each animation you want to change. The bools are stored in the " +
                                   "animator controller that is assigned to the hero object. " +
                                   "If you leave a field blank, the animation won't be changed for that field.", true);
                SimpleLayout.Line();

                GetStringField.BuildFieldA("Move Default", actionParams, heroAction.actionFields[5]);

                GetStringField.BuildFieldA("Move Left", actionParams, heroAction.actionFields[6]);
                GetStringField.BuildFieldA("Move Right", actionParams, heroAction.actionFields[7]);
                GetStringField.BuildFieldA("Move Up", actionParams, heroAction.actionFields[8]);
                GetStringField.BuildFieldA("Move Down", actionParams, heroAction.actionFields[9]);

                GetStringField.BuildFieldA("Move Up Left", actionParams, heroAction.actionFields[10]);
                GetStringField.BuildFieldA("Move Up Right", actionParams, heroAction.actionFields[11]);
                GetStringField.BuildFieldA("Move Down Left", actionParams, heroAction.actionFields[12]);
                GetStringField.BuildFieldA("Move Down Right", actionParams, heroAction.actionFields[13]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeFace = GetBoolValue.BuildField("Change Face Direction Animation?", actionParams, heroAction.actionFields[2], true);
            if (changeFace)
            {
                SimpleLayout.Line();
                SimpleLayout.Label("Enter the name of the bool for each animation you want to change. The bools are stored in the " +
                                   "animator controller that is assigned to the hero object. " +
                                   "If you leave a field blank, the animation won't be changed for that field.", true);
                SimpleLayout.Line();

                GetStringField.BuildFieldA("Face Default", actionParams, heroAction.actionFields[14]);

                GetStringField.BuildFieldA("Face Left", actionParams, heroAction.actionFields[15]);
                GetStringField.BuildFieldA("Face Right", actionParams, heroAction.actionFields[16]);
                GetStringField.BuildFieldA("Face Up", actionParams, heroAction.actionFields[17]);
                GetStringField.BuildFieldA("Face Down", actionParams, heroAction.actionFields[18]);

                GetStringField.BuildFieldA("Face Up Left", actionParams, heroAction.actionFields[19]);
                GetStringField.BuildFieldA("Face Up Right", actionParams, heroAction.actionFields[20]);
                GetStringField.BuildFieldA("Face Down Left", actionParams, heroAction.actionFields[21]);
                GetStringField.BuildFieldA("Face Down Right", actionParams, heroAction.actionFields[22]);
            }
            SimpleLayout.EndVertical();
        }
    }
}