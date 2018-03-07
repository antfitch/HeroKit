﻿// --------------------------------------------------------------
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
    /// Get hero kit objects from the scene. Only get objects at a specific position in the scene.
    /// </summary>
    public static class GetHeroObjectByPositionFields 
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 10);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            SimpleLayout.Label("Get hero objects in the scene at a specific position:");
            SimpleLayout.BeginHorizontal();
            bool x = GetBoolValue.BuildField("X", actionParams, heroAction.actionFields[3]);
            bool y = GetBoolValue.BuildField("Y", actionParams, heroAction.actionFields[4]);
            bool z = GetBoolValue.BuildField("Z", actionParams, heroAction.actionFields[5]);
            SimpleLayout.EndHorizontal();
            if (x || y || z)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                if (showContent(heroAction, 3)) GetFloatField.BuildFieldA("X:", actionParams, heroAction.actionFields[6], true);
                if (showContent(heroAction, 4)) GetFloatField.BuildFieldA("Y:", actionParams, heroAction.actionFields[7], true);
                if (showContent(heroAction, 5)) GetFloatField.BuildFieldA("Z:", actionParams, heroAction.actionFields[8], true);
                SimpleLayout.EndVertical();
            }
            GetFloatField.BuildFieldA("Radius to include around each coordinate:", actionParams, heroAction.actionFields[9]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetDropDownField.BuildField("Operation:", actionParams, heroAction.actionFields[0], new HeroObjectOperatorField());
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldB("Save the hero objects here:", actionParams, heroAction.actionFields[1]);
            GetIntegerField.BuildFieldA("Maximum number of hero objects to save:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();
        }

        private static bool showContent(HeroAction heroAction, int boolID)
        {
            if (heroAction.actionFields[boolID].bools != null &&
                heroAction.actionFields[boolID].bools.Count != 0 &&
                heroAction.actionFields[boolID].bools[0] == true)
                return true;
            else
                return false;
        }

    }
}