// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Get a float from a hero object (default value).
    /// </summary>
    public static class GetFloatFromTemplateFields 
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
            HeroObject targetHO = GetHeroObjectField.BuildFieldC("Get float from this hero object template:", actionParams, heroAction.actionFields[0]);
            if (targetHO != null)
            {
                GetFloatField.BuildFieldC("The float:", actionParams, heroAction.actionFields[1], targetHO);
            }      
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetFloatField.BuildFieldB("Save the float here:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();
        }
    }
}