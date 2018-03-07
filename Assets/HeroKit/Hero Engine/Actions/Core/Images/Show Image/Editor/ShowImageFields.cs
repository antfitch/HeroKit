// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using UnityEngine;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Show an image.
    /// </summary>
    public static class ShowImageFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 11);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------  
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetIntegerField.BuildFieldA("Image ID:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeImage = GetBoolValue.BuildField("Change image?", actionParams, heroAction.actionFields[1], true);
            if (changeImage)
            {
                GetUnityObjectField.BuildFieldA<Sprite>("", actionParams, heroAction.actionFields[2]);           
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Flip image?", actionParams, heroAction.actionFields[3], true);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeScale = GetBoolValue.BuildField("Scale image? (By Percent)", actionParams, heroAction.actionFields[4], true);
            if (changeScale)
            {             
                GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[5]);             
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changePosX = GetBoolValue.BuildField("Change X Position?", actionParams, heroAction.actionFields[6], true);
            if (changePosX)
            {              
                GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[7]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changePosY = GetBoolValue.BuildField("Change Y Position?", actionParams, heroAction.actionFields[8], true);
            if (changePosY)
            {              
                GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[9]);
            }
            SimpleLayout.EndVertical();
        }
    }
}