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
    /// Get the rotation of an object.
    /// </summary>
    public static class GetRotationFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 8);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetSceneObjectValue.BuildField("rotation", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeX = GetBoolValue.BuildField("Save X Angle?", actionParams, heroAction.actionFields[2], true);
            if (changeX) GetFloatField.BuildFieldB("", actionParams, heroAction.actionFields[3]);
            bool changeY = GetBoolValue.BuildField("Save Y Angle?", actionParams, heroAction.actionFields[4], true);
            if (changeY) GetFloatField.BuildFieldB("", actionParams, heroAction.actionFields[5]);
            bool changeZ = GetBoolValue.BuildField("Save Z Angle?", actionParams, heroAction.actionFields[6], true);
            if (changeZ) GetFloatField.BuildFieldB("", actionParams, heroAction.actionFields[7]);
            SimpleLayout.EndVertical();
        }
    }
}