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
    /// Save a value in PlayerPrefs.
    /// </summary>
    public static class SaveSettingFields
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
            GetStringField.BuildFieldA("Name of the setting:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            int valueType = GetDropDownField.BuildField("Type of value to save:", actionParams, heroAction.actionFields[0], new PlayerPrefTypeField());

            if (valueType != 0)
            {
                SimpleLayout.Label("The value to save on the player's machine:");
            }

            if (valueType == 1)
            {
                GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[1]);
            }
            else if (valueType == 2)
            {
                GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[1]);
            }
            else if (valueType == 3)
            {
                GetBoolField.BuildFieldA("", actionParams, heroAction.actionFields[1]);
            }
            else if (valueType == 4)
            {
                GetStringField.BuildFieldA("", actionParams, heroAction.actionFields[1]);
            }
            SimpleLayout.EndVertical();
        }
    }
}