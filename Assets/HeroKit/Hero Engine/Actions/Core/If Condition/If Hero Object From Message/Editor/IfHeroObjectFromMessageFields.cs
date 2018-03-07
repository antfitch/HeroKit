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
    /// Compares a hero object sent by a message to another hero object.
    /// </summary>
    public static class IfHeroObjectFromMessageFields 
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 4);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            // Field: Skip (hidden, only for else if)
            GetConditionalField.BuildField(actionParams, heroAction.actionFields[0]);

            // Field: Integer A
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            SimpleLayout.Label("Hero Object A:");
            SimpleLayout.Label("[Hero object received from encounter (on collide, etc)]");
            SimpleLayout.EndVertical();

            // Field: Operator
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetDropDownField.BuildField("Operator:", actionParams, heroAction.actionFields[2], new TrueFalseField());
            SimpleLayout.EndVertical();

            // Field: Integer B
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldA("Hero Object B:", actionParams, heroAction.actionFields[3]);
            SimpleLayout.EndVertical();
        }
    }
}