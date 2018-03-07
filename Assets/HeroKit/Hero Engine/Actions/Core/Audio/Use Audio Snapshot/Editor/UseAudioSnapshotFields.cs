// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using UnityEngine.Audio;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Change settings for background music on an object.
    /// </summary>  
    public static class UseAudioSnapshotFields
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
            GetObjectValue.BuildField<AudioMixerSnapshot>("Audio Mixer Snapshot:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();
        }
    }
}