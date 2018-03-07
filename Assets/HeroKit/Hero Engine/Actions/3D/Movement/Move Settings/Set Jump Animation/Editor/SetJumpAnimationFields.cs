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
    /// Set the jump animation for an object.
    /// </summary>
    public static class SetJumpAnimationFields
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
            HeroObject targetHeroObject = GetHeroObjectField.BuildFieldE("Animate a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeBegin = GetBoolValue.BuildField("Change jump begin animation?", actionParams, heroAction.actionFields[2], true);
            if (changeBegin)
            {
                ActionCommon.GetAnimation("The animation to show when jump begins:", actionParams, heroAction.actionFields[3], heroAction.actionFields[4], heroAction.actionFields[5], AnimatorControllerParameterType.Trigger, targetHeroObject);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeEnd = GetBoolValue.BuildField("Change jump end animation?", actionParams, heroAction.actionFields[6], true);
            if (changeEnd)
            {
                ActionCommon.GetAnimation("The animation to show when jump ends:", actionParams, heroAction.actionFields[7], heroAction.actionFields[8], heroAction.actionFields[9], AnimatorControllerParameterType.Trigger, targetHeroObject);
            }
            SimpleLayout.EndVertical();
        }
    }
}