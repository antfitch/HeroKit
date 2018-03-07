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
    /// Rotate object a specific number of degrees.
    /// </summary>  
    public static class RotateXDegreesFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 9);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Rotate a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeX = GetBoolValue.BuildField("X Axis", actionParams, heroAction.actionFields[2], true);
            if (changeX) GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[3]);
            bool changeY = GetBoolValue.BuildField("Y Axis", actionParams, heroAction.actionFields[4], true);
            if (changeY) GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[5]);
            bool changeZ = GetBoolValue.BuildField("Z Axis", actionParams, heroAction.actionFields[6], true);
            if (changeZ) GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[7]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetIntegerField.BuildFieldA("Speed:", actionParams, heroAction.actionFields[8]);
            SimpleLayout.EndVertical();
            
        }
    }
}