// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using UnityEngine;
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Enable space shooter player controller.
    /// </summary>
    public static class SpaceShooterControllerOnFields
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
            HeroObject targetHeroObject = GetHeroObjectField.BuildFieldE("Move a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetIntegerField.BuildFieldA("Movement Speed:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            string[] items = { "Object has animatior controller", "Object has animation component" };
            int animationType = GetDropDownField.BuildField("Animation system:", actionParams, heroAction.actionFields[3], new GenericListField(items));
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            if (animationType <= 1)
                ActionCommon.GetAnimation("Walk Animation:", actionParams, heroAction.actionFields[4], heroAction.actionFields[5], heroAction.actionFields[6], AnimatorControllerParameterType.Bool, targetHeroObject);
            else
            {
                ActionCommon.GetAnimation_Legacy("Walk Animation:", actionParams, heroAction.actionFields[4], heroAction.actionFields[5], heroAction.actionFields[6], targetHeroObject);
                ActionCommon.GetAnimation_Legacy("Idle Animation:", actionParams, heroAction.actionFields[7], heroAction.actionFields[8], heroAction.actionFields[9], targetHeroObject);
            }
            SimpleLayout.EndVertical();

        }
    }
}