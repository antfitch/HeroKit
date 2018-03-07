// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Change a float on a hero kit object.
    /// </summary>
    public static class ChangeFloatBFields
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
            GetFloatField.BuildFieldB("Float A:", actionParams, heroAction.actionFields[0]);
            GetDropDownField.BuildField("Operator:", actionParams, heroAction.actionFields[1], new MathOperatorField());
            GetFloatField.BuildFieldA("Float B:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();
        }
    }
}