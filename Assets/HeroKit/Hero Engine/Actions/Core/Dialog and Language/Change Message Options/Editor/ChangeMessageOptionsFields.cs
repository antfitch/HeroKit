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
    /// Change options for messages.
    /// </summary>
    public static class ChangeMessageOptionsFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 24);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changePrintSettings = GetBoolValue.BuildField("Change print message settings?", actionParams, heroAction.actionFields[0], true);
            if (changePrintSettings)
            {
                bool printMessage = GetBoolValue.BuildField("Print message character by character?", actionParams, heroAction.actionFields[1], true);
                if (printMessage)
                {
                    GetSliderValue.BuildFieldB("Speed:", actionParams, heroAction.actionFields[2], 0, 100, true);
                }
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeAlignment = GetBoolValue.BuildField("Change alignment of message?", actionParams, heroAction.actionFields[3], true);
            if (changeAlignment)
            {
                string[] alignmentTypes = { "Top Left", "Top Center", "Top Right",
                                            "Middle Left", "Middle Center", "Middle Right",
                                            "Bottom Left", "Bottom Center", "Bottom Right",
                                          };
                GetDropDownField.BuildField("", actionParams, heroAction.actionFields[4], new GenericListField(alignmentTypes));
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeBackgroundSettings = GetBoolValue.BuildField("Change background alpha?", actionParams, heroAction.actionFields[5], true);
            if (changeBackgroundSettings)
            {
                GetSliderValue.BuildFieldB("", actionParams, heroAction.actionFields[6], 0, 100, true);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeBackgroundImage = GetBoolValue.BuildField("Change background image?", actionParams, heroAction.actionFields[7], true);
            if (changeBackgroundImage)
            {
                GetUnityObjectField.BuildFieldA<Sprite>("", actionParams, heroAction.actionFields[8]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeButtonImage = GetBoolValue.BuildField("Change button image?", actionParams, heroAction.actionFields[9], true);
            if (changeButtonImage)
            {
                GetUnityObjectField.BuildFieldA<Sprite>("", actionParams, heroAction.actionFields[10]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeButtonLayout = GetBoolValue.BuildField("Change button layout?", actionParams, heroAction.actionFields[11], true);
            if (changeButtonLayout)
            {
                string[] items = { "Horizontal", "Vertical"};
                GetDropDownField.BuildField("", actionParams, heroAction.actionFields[12], new GenericListField(items));
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeTextColor = GetBoolValue.BuildField("Change text body color?", actionParams, heroAction.actionFields[13], true);
            if (changeTextColor)
            {
                GetColorValue.BuildField("", actionParams, heroAction.actionFields[14]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeHeadingColor = GetBoolValue.BuildField("Change text heading color?", actionParams, heroAction.actionFields[15], true);
            if (changeHeadingColor)
            {
                GetColorValue.BuildField("", actionParams, heroAction.actionFields[16]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeButtonTextColor = GetBoolValue.BuildField("Change button text color?", actionParams, heroAction.actionFields[17], true);
            if (changeButtonTextColor)
            {
                GetColorValue.BuildField("", actionParams, heroAction.actionFields[18]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeButtonAlpha = GetBoolValue.BuildField("Change background button alpha?", actionParams, heroAction.actionFields[19], true);
            if (changeButtonAlpha)
            {
                GetSliderValue.BuildFieldB("", actionParams, heroAction.actionFields[20], 0, 100, true);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeButtonActiveColor = GetBoolValue.BuildField("Change selected button color?", actionParams, heroAction.actionFields[21], true);
            if (changeButtonActiveColor)
            {
                GetColorValue.BuildField("", actionParams, heroAction.actionFields[22]);
            }
            SimpleLayout.EndVertical();
        }
    }
}