// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using UnityEngine;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Play a particle effect on a game object.
    /// </summary>
    public static class PlayParticleInSceneFields
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
            GetUnityObjectField.BuildFieldA<ParticleSystem>("The particle effect to play:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changePos = GetBoolValue.BuildField("Change position?", actionParams, heroAction.actionFields[1], true);
            if (changePos)
            {
                GetCoordinatesField.BuildField("", actionParams,
                                                heroAction.actionFields[2], heroAction.actionFields[3],
                                                heroAction.actionFields[4], heroAction.actionFields[5],
                                                heroAction.actionFields[6], heroAction.actionFields[7]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeRotation = GetBoolValue.BuildField("Change rotation?", actionParams, heroAction.actionFields[8], true);
            if (changeRotation)
            {
                GetCoordinatesField.BuildField("", actionParams,
                                                heroAction.actionFields[9], heroAction.actionFields[10],
                                                heroAction.actionFields[11], heroAction.actionFields[12],
                                                heroAction.actionFields[13], heroAction.actionFields[14]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Don't play next action until this action completes?", actionParams, heroAction.actionFields[15], true);
            SimpleLayout.EndVertical();
        }
    }
}