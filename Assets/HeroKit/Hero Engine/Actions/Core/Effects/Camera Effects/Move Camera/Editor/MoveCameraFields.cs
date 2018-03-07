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
    /// Move a camera.
    /// </summary>
    public static class MoveCameraFields
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
            string[] items = { "Pan to Position", "Pan to Object" };
            int moveType = GetDropDownField.BuildField("Move the camera here:", actionParams, heroAction.actionFields[1], new GenericListField(items));

            // move to position
            if (moveType == 1)
            {
                bool changeX = GetBoolValue.BuildField("Move to X?", actionParams, heroAction.actionFields[2], true);
                if (changeX)
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[3]);

                bool changeY = GetBoolValue.BuildField("Move to Y?", actionParams, heroAction.actionFields[4], true);
                if (changeY)
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[5]);

                bool changeZ = GetBoolValue.BuildField("Move to Z?", actionParams, heroAction.actionFields[6], true);
                if (changeZ)
                    GetFloatField.BuildFieldA("", actionParams, heroAction.actionFields[7]);
            }

            // move to object
            else if (moveType == 2)
            {
                GetSceneObjectValue.BuildField("Scene Object", actionParams, heroAction.actionFields[8], heroAction.actionFields[9]);
                GetBoolValue.BuildField("Move to X?", actionParams, heroAction.actionFields[2], true);
                GetBoolValue.BuildField("Move to Y?", actionParams, heroAction.actionFields[4], true);
                GetBoolValue.BuildField("Move to Z?", actionParams, heroAction.actionFields[6], true);
            }

            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetIntegerField.BuildFieldA("Movement time: (ex: 1=fast, 100=slow)", actionParams, heroAction.actionFields[10]);
            SimpleLayout.EndVertical();

        }
    }
}