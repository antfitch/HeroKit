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
    /// Rotate an image.
    /// </summary>
    public static class RotateImageFields
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
            GetIntegerField.BuildFieldA("Image ID:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetIntegerField.BuildFieldA("Speed:", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            string[] choices = { "Rotate X number of degrees", "Rotate clockwise for X amount of time", "Rotate counterclockwise for X amount of time" };
            int rotateType = GetDropDownField.BuildField("Type:", actionParams, heroAction.actionFields[2], new GenericListField(choices));

            // rotate x number of degrees
            if (rotateType == 1)
            {
                GetIntegerField.BuildFieldA("Degrees to Rotate:", actionParams, heroAction.actionFields[3]);
            }

            // rotate clockwise / counterclockwise
            else if (rotateType == 2 || rotateType == 3)
            {
                GetIntegerField.BuildFieldA("Length of time (seconds):", actionParams, heroAction.actionFields[4]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Don't play next action until this action completes?", actionParams, heroAction.actionFields[5], true);
            SimpleLayout.EndVertical();
        }
    }
}