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
    /// Enable the 2D third-person camera.
    /// </summary>
    
    public static class OrthoCameraOnFields
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

                // change x position
                bool changeXPos = GetBoolValue.BuildField("Change X position (ex: -1=left of object, 1=right of object)", actionParams, heroAction.actionFields[5], true);
                if (changeXPos)
                {
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[6]);
                }

                // change y position
                bool changeYPos = GetBoolValue.BuildField("Change Y position (ex: -1=below object, 1=above object)", actionParams, heroAction.actionFields[7], true);
                if (changeYPos)
                {
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[8]);
                }

                // change size
                bool changeSize = GetBoolValue.BuildField("Change size (ex: 10=zoom out, 5=default, 1=zoom in)", actionParams, heroAction.actionFields[9], true);
                if (changeSize)
                {
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[10]);
                }
            }

            SimpleLayout.EndVertical();
        }
    }
}