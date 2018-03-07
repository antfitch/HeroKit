// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using UnityEditor;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Load a scene.
    /// </summary>  
    public static class LoadSceneFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 18);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetUnityObjectField.BuildFieldA<SceneAsset>("The scene to load:", actionParams, heroAction.actionFields[0]);
            GetBoolValue.BuildField("Use default scene state?", actionParams, heroAction.actionFields[1], true);
            GetBoolValue.BuildField("Destroy clones of persistent objects?", actionParams, heroAction.actionFields[2], true);
            GetBoolValue.BuildField("Don't save state of current scene?", actionParams, heroAction.actionFields[3], true);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool setLocation = GetBoolValue.BuildField("Go to a specific location?", actionParams, heroAction.actionFields[4], true);
            if (setLocation)
            {
                GetCoordinatesField.BuildField("", actionParams, heroAction.actionFields[5], heroAction.actionFields[6], heroAction.actionFields[7], heroAction.actionFields[8], heroAction.actionFields[9], heroAction.actionFields[10]);

            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool setRotation = GetBoolValue.BuildField("Use a specific rotation?", actionParams, heroAction.actionFields[11], true);
            if (setRotation)
            {
                GetCoordinatesField.BuildField("", actionParams, heroAction.actionFields[12], heroAction.actionFields[13], heroAction.actionFields[14], heroAction.actionFields[15], heroAction.actionFields[16], heroAction.actionFields[17]);

            }
            SimpleLayout.EndVertical();
        }
    }
}