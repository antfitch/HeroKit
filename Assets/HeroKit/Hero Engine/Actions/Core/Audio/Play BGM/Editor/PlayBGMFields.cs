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
    /// Play a background music clip.
    /// </summary>
    public static class PlayBGMFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 16);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Play audio on another object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetUnityObjectField.BuildFieldA<AudioClip>("The background music to play:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();
           
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeSettings = GetBoolValue.BuildField("Change BGM settings?", actionParams, heroAction.actionFields[3], true);
            if (changeSettings)
            {
                // this takes 11 action fields. actionField[x] to actionField[x+11]
                ActionCommon.GetAudioSettings(actionParams, heroAction, 4);
            }
            SimpleLayout.EndVertical();
        }
    }
}