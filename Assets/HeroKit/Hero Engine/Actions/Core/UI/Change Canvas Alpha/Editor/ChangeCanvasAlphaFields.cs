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
    /// Change the alpha of a UI canvas.
    /// </summary>
    public static class ChangeCanvasAlphaFields
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
            GetSceneObjectValue.BuildField("canvas", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetSliderValue.BuildFieldB("The new alpha for the canvas:", actionParams, heroAction.actionFields[2]);
            GetIntegerField.BuildFieldA("Duration:", actionParams, heroAction.actionFields[3]);
            GetBoolValue.BuildField("Don't play next action until this action completes?", actionParams, heroAction.actionFields[4], true);
            SimpleLayout.EndVertical();
        }
    }
}