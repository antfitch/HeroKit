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
    /// Enable the third-person camera.
    /// </summary>
    
    public static class ThirdPersonCameraOnFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 12);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetCameraField.BuildField("", actionParams, heroAction.actionFields[0], heroAction.actionFields[11]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldA("The Hero Object to follow:", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();


            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool customSettings = GetBoolValue.BuildField("Change default values?", actionParams, heroAction.actionFields[2], true);
            if (customSettings)
            {
                // change smoothness of follow
                bool changeSmoothing = GetBoolValue.BuildField("Change smoothness of follow?", actionParams, heroAction.actionFields[3], true);
                if (changeSmoothing)
                {
                    GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[4]);
                }

                // change z position
                bool changeZPos = GetBoolValue.BuildField("Change Z position (ex: -1=behind object, 1=in front of object)", actionParams, heroAction.actionFields[5], true);
                if (changeZPos)
                {
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[6]);
                }

                // change y position
                bool changeYPos = GetBoolValue.BuildField("Change Y position (ex: -1=under object, 1=above object)", actionParams, heroAction.actionFields[7], true);
                if (changeYPos)
                {
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[8]);
                }

                // change angle
                bool changeAngle = GetBoolValue.BuildField("Change angle (ex: 45=slanted down, -45=slanted up)", actionParams, heroAction.actionFields[9], true);
                if (changeAngle)
                {
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[10]);
                }
            }

            SimpleLayout.EndVertical();
        }
    }
}