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
    /// Set the movement animation for an object.
    /// </summary>
    public static class SetMoveAnimationFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 5);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            HeroObject targetHeroObject = GetHeroObjectField.BuildFieldE("Animate another hero object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            ActionCommon.GetAnimation("", actionParams, heroAction.actionFields[2], heroAction.actionFields[3], heroAction.actionFields[4], AnimatorControllerParameterType.Bool, targetHeroObject);
        }
    }
}