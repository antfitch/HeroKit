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
    /// Compare the direction of an object to a specified direction and return the result.
    /// </summary>
    public static class IfHeroObjectFacingFields 
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
            GetHeroObjectField.BuildFieldA("If this hero object:", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            // Field: Operator
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            string[] items = { "in front of", "behind", "to the right of", "to the left of" };
            GetDropDownField.BuildField("is:", actionParams, heroAction.actionFields[2], new GenericListField(items), true);
            SimpleLayout.EndVertical();

            // Field: Integer B
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldA("this hero object:", actionParams, heroAction.actionFields[3]);
            SimpleLayout.EndVertical();
        }
    }
}