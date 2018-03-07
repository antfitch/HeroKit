// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using HeroKit.Editor.HeroField;
using UnityEditor;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Moves an object from one scene to another.
    /// </summary>   
    public static class GoToSceneFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 21);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.Label("Move a hero object:");
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Move a different hero object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.Label("To this scene:");
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetUnityObjectField.BuildFieldA<SceneAsset>("", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();

            SimpleLayout.Label("At this location:");
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetCoordinatesField.BuildField("", actionParams, heroAction.actionFields[3], heroAction.actionFields[4], heroAction.actionFields[5], heroAction.actionFields[6], heroAction.actionFields[7], heroAction.actionFields[8]);
            SimpleLayout.EndVertical();

            SimpleLayout.Label("Facing this direction:");
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            string[] items = { "Retain", "Left", "Right", "Up", "Down", "Custom" };
            int result = GetDropDownField.BuildField("", actionParams, heroAction.actionFields[9], new GenericListField(items));
            // custom
            if (result == 6)
            {
                GetCoordinatesField.BuildField("", actionParams, heroAction.actionFields[10], heroAction.actionFields[11], heroAction.actionFields[12], heroAction.actionFields[13], heroAction.actionFields[14], heroAction.actionFields[15]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.Label("Other settings:");
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Move camera to object's position?", actionParams, heroAction.actionFields[16], true);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Move camera to object's rotation?", actionParams, heroAction.actionFields[17], true);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Use default scene state?", actionParams, heroAction.actionFields[18], true);
            GetBoolValue.BuildField("Destroy clones of persistent objects?", actionParams, heroAction.actionFields[19], true);
            GetBoolValue.BuildField("Don't save state of current scene?", actionParams, heroAction.actionFields[20], true);
            SimpleLayout.EndVertical();
        }
    }
}