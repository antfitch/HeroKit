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
    /// Play a preexisting particle effect.
    /// </summary>
    public static class PlayParticleFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 18);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool useDifferent = GetBoolValue.BuildField("Play particle effect on a different object?", actionParams, heroAction.actionFields[0], true);
            if (useDifferent)
            {
                SimpleLayout.Line();
                GetSceneObjectValue.BuildField("particle effect", actionParams, heroAction.actionFields[1], heroAction.actionFields[2]);
            }

            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changePos = GetBoolValue.BuildField("Change position?", actionParams, heroAction.actionFields[3], true);
            if (changePos)
            {
                GetCoordinatesField.BuildField("", actionParams,
                                                heroAction.actionFields[4], heroAction.actionFields[5],
                                                heroAction.actionFields[6], heroAction.actionFields[7],
                                                heroAction.actionFields[8], heroAction.actionFields[9]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeRotation = GetBoolValue.BuildField("Change rotation?", actionParams, heroAction.actionFields[10], true);
            if (changeRotation)
            {
                GetCoordinatesField.BuildField("", actionParams,
                                                heroAction.actionFields[11], heroAction.actionFields[12],
                                                heroAction.actionFields[13], heroAction.actionFields[14],
                                                heroAction.actionFields[15], heroAction.actionFields[16]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Don't play next action until this action completes?", actionParams, heroAction.actionFields[17], true);
            SimpleLayout.EndVertical();
        }
    }
}