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
    /// Compare two game objects and return the result.
    /// </summary>
    public static class IfGameObjectFields 
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
            GetGameObjectField.BuildFieldB("Game Object A:", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            // Field: Operator
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetDropDownField.BuildField("Operator:", actionParams, heroAction.actionFields[2], new TrueFalseField());
            SimpleLayout.EndVertical();

            // Field: Integer B
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetGameObjectField.BuildFieldA("Game Object B:", actionParams, heroAction.actionFields[3]);
            SimpleLayout.EndVertical();
        }
    }
}