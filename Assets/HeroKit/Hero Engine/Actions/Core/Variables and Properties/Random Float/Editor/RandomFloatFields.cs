// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using HeroKit.Editor.ActionField;
using SimpleGUI;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Get a random float and save it in a hero kit object.
    /// </summary>
    public static class RandomFloatFields 
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

            // Field: bottom value
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetFloatField.BuildFieldA("Lowest value:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();

            // Field: top value
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetFloatField.BuildFieldA("Highest value:", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            // Field: save result here
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetFloatField.BuildFieldB("Save result here:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();
        }
    }
}