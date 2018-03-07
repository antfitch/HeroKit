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
    /// Compare two bools and return the result.
    /// </summary>
    public static class IfBoolFields 
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
            GetBoolField.BuildFieldB("Bool A:", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            // Field: Operator
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetDropDownField.BuildField("Operator:", actionParams, heroAction.actionFields[2], new TrueFalseField());
            SimpleLayout.EndVertical();

            // Field: Integer B
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolField.BuildFieldA("Bool B:", actionParams, heroAction.actionFields[3]);
            SimpleLayout.EndVertical();
        }
    }
}