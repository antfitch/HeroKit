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
    /// Stop a particle effect.
    /// </summary>
    public static class StopParticleEffectFields
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
            bool useDifferent = GetBoolValue.BuildField("Stop particle effect on a different object?", actionParams, heroAction.actionFields[0], true);
            if (useDifferent)
            {
                SimpleLayout.Line();
                GetSceneObjectValue.BuildField("particle effect", actionParams, heroAction.actionFields[1], heroAction.actionFields[2]);
            }
            SimpleLayout.EndVertical();
        }
    }
}