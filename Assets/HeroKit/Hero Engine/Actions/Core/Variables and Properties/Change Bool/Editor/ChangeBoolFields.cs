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
    /// Change a bool on a hero kit object.
    /// </summary>
    public static class ChangeBoolFields 
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 2);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolField.BuildFieldB("The bool to change:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolField.BuildFieldA("Change it to:", actionParams, heroAction.actionFields[1]); 
            SimpleLayout.EndVertical();
        }
    }
}