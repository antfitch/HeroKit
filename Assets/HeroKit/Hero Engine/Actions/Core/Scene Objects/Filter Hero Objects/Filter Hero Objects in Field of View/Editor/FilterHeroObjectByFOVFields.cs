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
    /// Filter hero kit objects in a hero object list field that are in the field of view of another object.
    /// </summary>
    public static class FilterHeroObjectByFOVFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 9);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            SimpleLayout.Label("Get the hero objects to filter here:");
            GetHeroObjectField.BuildFieldB("", actionParams, heroAction.actionFields[4]);
            SimpleLayout.Line();
            GetHeroObjectField.BuildFieldA("The hero object which has the field of view:", actionParams, heroAction.actionFields[3]);
            GetChildObjectField.BuildField("Is field of view coming from a child object?", actionParams, heroAction.actionFields[5], heroAction.actionFields[6], false, -20);
            SimpleLayout.Line();
            GetDropDownField.BuildField("Side of the hero object which has the field of view:", actionParams, heroAction.actionFields[7], new RayDirectionTypeField());
            GetIntegerField.BuildFieldA("Size of the field of view (in degrees):", actionParams, heroAction.actionFields[8]);
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