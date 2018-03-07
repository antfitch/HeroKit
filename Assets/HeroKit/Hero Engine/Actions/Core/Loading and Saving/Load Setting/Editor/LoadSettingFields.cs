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
    /// Load a value from PlayerPrefs.
    /// </summary>
    public static class LoadSettingFields
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
            int valueType = GetDropDownField.BuildField("Type of value to load:", actionParams, heroAction.actionFields[0], new PlayerPrefTypeField());

            if (valueType != 0)
            {
                SimpleLayout.Label("Save the value here:");
            }

            if (valueType == 1)
            {
                GetIntegerField.BuildFieldB("", actionParams, heroAction.actionFields[1]);
            }
            else if (valueType == 2)
            {
                GetFloatField.BuildFieldB("", actionParams, heroAction.actionFields[1]);
            }
            else if (valueType == 3)
            {
                GetBoolField.BuildFieldB("", actionParams, heroAction.actionFields[1]);
            }
            else if (valueType == 4)
            {
                GetStringField.BuildFieldB("", actionParams, heroAction.actionFields[1]);
            }
            SimpleLayout.EndVertical();
        }
    }
}