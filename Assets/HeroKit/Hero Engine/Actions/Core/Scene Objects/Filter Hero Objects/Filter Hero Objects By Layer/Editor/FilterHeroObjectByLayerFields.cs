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
    /// Filter hero kit objects in a hero object list field by layer.
    /// </summary>
    public static class FilterHeroObjectByLayerFields 
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

            SimpleLayout.Label("Get hero objects in the list assigned to this layer:");
            GetDropDownField.BuildField("", actionParams, heroAction.actionFields[3], new LayerListField());
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