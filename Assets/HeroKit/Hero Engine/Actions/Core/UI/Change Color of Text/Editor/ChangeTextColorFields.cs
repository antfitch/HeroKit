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
    /// Change the color of a UI text field.
    /// </summary>
    public static class ChangeTextColorFields
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
            GetSceneObjectValue.BuildField("text", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetColorValue.BuildField("The color to use:", actionParams, heroAction.actionFields[2]);
            GetIntegerField.BuildFieldA("Speed of the change:", actionParams, heroAction.actionFields[3]);
            GetBoolValue.BuildField("Don't play next action until this action completes?", actionParams, heroAction.actionFields[4], true);
            SimpleLayout.EndVertical();
        }
    }
}