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
    /// Filter hero kit objects in a hero object list field that are in the trigger area of a specific hero kit object in the scene.
    /// </summary>
    public static class FilterHeroObjectByTriggerFields
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
            SimpleLayout.Label("Get the hero objects here:");
            GetHeroObjectField.BuildFieldB("", actionParams, heroAction.actionFields[4]);

            SimpleLayout.Label("Get hero objects in the list that are in the trigger area of this hero object:");
            GetHeroObjectField.BuildFieldA("", actionParams, heroAction.actionFields[3]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            SimpleLayout.Label("Operation:");
            GetDropDownField.BuildField("", actionParams, heroAction.actionFields[0], new HeroObjectOperatorField());
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            SimpleLayout.Label("Save the hero objects here:");
            GetHeroObjectField.BuildFieldB("", actionParams, heroAction.actionFields[1]);

            SimpleLayout.Label("Maximum number of hero objects to save:");
            GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();
        }
    }
}