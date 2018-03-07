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
    /// Make an object jump.
    /// </summary>
    public static class Jump2DPlatformerFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 6);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Move a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetFloatField.BuildFieldA("Force of the jump:", actionParams, heroAction.actionFields[2]);

            string[] items = { "jump straight up", "jump left", "jump right" };
            int result = GetDropDownField.BuildField("Which directions can object jump?:", actionParams, heroAction.actionFields[3], new GenericListField(items));
            if (result > 1) GetIntegerField.BuildFieldA("Force of the direction (0 to 100):", actionParams, heroAction.actionFields[4]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Play next action before this action completes?", actionParams, heroAction.actionFields[5], true);
            SimpleLayout.EndVertical();
        }
    }
}