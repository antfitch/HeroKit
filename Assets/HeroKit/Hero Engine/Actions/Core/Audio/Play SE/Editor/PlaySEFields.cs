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
    /// Play a sound effect.
    /// </summary>
    public static class PlaySEFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 17);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Play audio on another object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[4]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetUnityObjectField.BuildFieldA<AudioClip>("The sound effect to play:", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Fade BGM while sound effect plays?", actionParams, heroAction.actionFields[2], true);
            GetBoolValue.BuildField("Fade BGS while sound effect plays?", actionParams, heroAction.actionFields[3], true);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeSettings = GetBoolValue.BuildField("Change SE settings?", actionParams, heroAction.actionFields[5], true);
            if (changeSettings)
            {
                // this takes 11 action fields. actionField[x] to actionField[x+11]
                ActionCommon.GetAudioSettings(actionParams, heroAction, 6);
            }
            SimpleLayout.EndVertical();
        }
    }
}