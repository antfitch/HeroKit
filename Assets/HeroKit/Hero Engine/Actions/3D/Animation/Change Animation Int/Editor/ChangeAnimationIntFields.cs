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
    /// Change an integer assigned to an animator.
    /// </summary>
    public static class ChangeAnimationIntFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 6);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            HeroObject targetObject = GetHeroObjectField.BuildFieldE("Update a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            if (targetObject != null)
            {
                ActionCommon.GetAnimation("", actionParams, heroAction.actionFields[2], heroAction.actionFields[3], heroAction.actionFields[4], AnimatorControllerParameterType.Int, targetObject);

                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                GetIntegerField.BuildFieldA("New value for the integer:", actionParams, heroAction.actionFields[5]);
                SimpleLayout.EndVertical();
            }
        }
    }
}