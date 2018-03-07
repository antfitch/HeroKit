// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using UnityEngine;
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Show a dialog box.
    /// </summary>  
    public static class ShowDialogFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 35);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            // title
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeTitle = GetBoolValue.BuildField("Change Title?", actionParams, heroAction.actionFields[0], true);
            if (changeTitle)
            {
                GetStringField.BuildFieldA("", actionParams, heroAction.actionFields[1]);
                bool changeAlignment = GetBoolValue.BuildField("Change Alignment?", actionParams, heroAction.actionFields[26], true);
                if (changeAlignment)
                {
                    string[] items = { "Left", "Center", "Right" };
                    GetDropDownField.BuildField("", actionParams, heroAction.actionFields[27], new GenericListField(items));
                }
            }
            SimpleLayout.EndVertical();

            // message
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetStringField.BuildFieldA("Message:", actionParams, heroAction.actionFields[2], false, true);
            GetUnityObjectField.BuildFieldA<AudioClip>("Audio (Optional):", actionParams, heroAction.actionFields[28]);
            SimpleLayout.EndVertical();

            // portrait left
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changePortraitLeft = GetBoolValue.BuildField("Change Portrait on Left?", actionParams, heroAction.actionFields[3], true);
            if (changePortraitLeft)
            {
                GetUnityObjectField.BuildFieldA<Sprite>("", actionParams, heroAction.actionFields[4]);
                GetBoolValue.BuildField("Flip image?", actionParams, heroAction.actionFields[5], true);
                bool changeScale = GetBoolValue.BuildField("Scale image? (By Percent)", actionParams, heroAction.actionFields[6], true);
                if (changeScale)
                {
                    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[7]);
                    SimpleLayout.EndVertical();
                }
                bool changePosX = GetBoolValue.BuildField("Change X Position?", actionParams, heroAction.actionFields[8], true);
                if (changePosX)
                {
                    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[9]);
                    SimpleLayout.EndVertical();
                }
                bool changePosY = GetBoolValue.BuildField("Change Y Position?", actionParams, heroAction.actionFields[10], true);
                if (changePosY)
                {
                    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[11]);
                    SimpleLayout.EndVertical();
                }
                bool changePosZ = GetBoolValue.BuildField("Put behind or in front of message window?", actionParams, heroAction.actionFields[31], true);
                if (changePosZ)
                {
                    string[] list = { "Put behind messsage window", "Put in front of message window" };
                    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                    GetDropDownField.BuildField("", actionParams, heroAction.actionFields[32], new GenericListField(list));
                    SimpleLayout.EndVertical();
                }
            }
            SimpleLayout.EndVertical();

            // portrait right
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changePortraitRight = GetBoolValue.BuildField("Change Portrait on Right?", actionParams, heroAction.actionFields[12], true);
            if (changePortraitRight)
            {
                GetUnityObjectField.BuildFieldA<Sprite>("", actionParams, heroAction.actionFields[13]);
                GetBoolValue.BuildField("Flip image?", actionParams, heroAction.actionFields[14], true);
                bool changeScale = GetBoolValue.BuildField("Scale image? (By Percent)", actionParams, heroAction.actionFields[15], true);
                if (changeScale)
                {
                    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[16]);
                    SimpleLayout.EndVertical();
                }
                bool changePosX = GetBoolValue.BuildField("Change X Position?", actionParams, heroAction.actionFields[17], true);
                if (changePosX)
                {
                    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[18]);
                    SimpleLayout.EndVertical();
                }
                bool changePosY = GetBoolValue.BuildField("Change Y Position?", actionParams, heroAction.actionFields[19], true);
                if (changePosY)
                {
                    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[20]);
                    SimpleLayout.EndVertical();
                }
                bool changePosZ = GetBoolValue.BuildField("Put behind or in front of message window?", actionParams, heroAction.actionFields[33], true);
                if (changePosZ)
                {
                    string[] list = { "Put behind messsage window", "Put in front of message window" };
                    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                    GetDropDownField.BuildField("", actionParams, heroAction.actionFields[34], new GenericListField(list));
                    SimpleLayout.EndVertical();
                }
            }
            SimpleLayout.EndVertical();

            // choices
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool addChoices = GetBoolValue.BuildField("Add Choices?", actionParams, heroAction.actionFields[21], true);
            if (addChoices)
            {
                string[] items = { "One", "Two", "Three" };
                int result = GetDropDownField.BuildField("", actionParams, heroAction.actionFields[22], new GenericListField(items));

                if (result > 0)
                {
                    GetStringField.BuildFieldA("", actionParams, heroAction.actionFields[23], true);
                }
                if (result > 1)
                {
                    GetStringField.BuildFieldA("", actionParams, heroAction.actionFields[24], true);
                }
                if (result > 2)
                {
                    GetStringField.BuildFieldA("", actionParams, heroAction.actionFields[25], true);
                }

                GetIntegerField.BuildFieldB("Save player's choice here:", actionParams, heroAction.actionFields[29]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Keep message window open after message is closed?", actionParams, heroAction.actionFields[30], true);
            SimpleLayout.EndVertical();

        }
    }
}