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
    /// Get the date in a save game file and save it as integers.
    /// </summary> 
    public static class GetSaveDateFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 13);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetStringField.BuildFieldA("Name of the save game file:", actionParams, heroAction.actionFields[12]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool saveYear = GetBoolValue.BuildField("Save year?", actionParams, heroAction.actionFields[0], true);
            if (saveYear) GetIntegerField.BuildFieldB("", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool saveMonth = GetBoolValue.BuildField("Save month?", actionParams, heroAction.actionFields[2], true);
            if (saveMonth) GetIntegerField.BuildFieldB("", actionParams, heroAction.actionFields[3]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool saveDay = GetBoolValue.BuildField("Save day?", actionParams, heroAction.actionFields[4], true);
            if (saveDay) GetIntegerField.BuildFieldB("", actionParams, heroAction.actionFields[5]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool saveHour = GetBoolValue.BuildField("Save hour?", actionParams, heroAction.actionFields[6], true);
            if (saveHour) GetIntegerField.BuildFieldB("", actionParams, heroAction.actionFields[7]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool saveMinute = GetBoolValue.BuildField("Save minute?", actionParams, heroAction.actionFields[8], true);
            if (saveMinute) GetIntegerField.BuildFieldB("", actionParams, heroAction.actionFields[9]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool saveSecond = GetBoolValue.BuildField("Save second?", actionParams, heroAction.actionFields[10], true);
            if (saveSecond) GetIntegerField.BuildFieldB("", actionParams, heroAction.actionFields[11]);
            SimpleLayout.EndVertical();
        }
    }
}