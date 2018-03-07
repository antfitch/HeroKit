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
    /// Get a string from a hero object (default value).
    /// </summary>
    public static class GetStringFromTemplateFields 
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
            HeroObject targetHO = GetHeroObjectField.BuildFieldC("Get string from this hero object template:", actionParams, heroAction.actionFields[0]);
            if (targetHO != null)
            {
                GetStringField.BuildFieldC("The string:", actionParams, heroAction.actionFields[1], targetHO);
            }      
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetStringField.BuildFieldB("Save the string here:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();
        }
    }
}