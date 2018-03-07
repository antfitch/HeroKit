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
    /// Change the rotation of an object.
    /// </summary>   
    public static class ChangeRotation2DFields
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
            GetHeroObjectField.BuildFieldE("Rotate a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetChildObjectField.BuildField("Rotate a child object?", actionParams, heroAction.actionFields[2], heroAction.actionFields[3]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeX = GetBoolValue.BuildField("X Axis", actionParams, heroAction.actionFields[4], true);
            if (changeX) GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[5]);
            bool changeY = GetBoolValue.BuildField("Y Axis", actionParams, heroAction.actionFields[6], true);
            if (changeY) GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[7]);
            bool changeZ = GetBoolValue.BuildField("Z Axis", actionParams, heroAction.actionFields[8], true);
            if (changeZ) GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[9]);
            SimpleLayout.EndVertical();            
        }
    }
}