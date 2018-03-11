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
    /// Change the visuals for a hero kit object.
    /// </summary>  
    public static class ChangeVisualsFields
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
            GetHeroObjectField.BuildFieldE("Change a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changePrefab = GetBoolValue.BuildField("Use a new prefab?", actionParams, heroAction.actionFields[2], true);
            if (changePrefab)
            {
                GetPrefabValue.BuildField("", actionParams, heroAction.actionFields[3]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeRigidbody = GetBoolValue.BuildField("Use a new rigidbody?", actionParams, heroAction.actionFields[4], true);
            if (changeRigidbody)
            {
                GetRigidbodyValue.BuildField("", actionParams, heroAction.actionFields[5]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeHidden = GetBoolValue.BuildField("Show or hide visuals?", actionParams, heroAction.actionFields[6], true);
            if (changeHidden)
            {
                string[] choices = { "Show Visuals", "Hide Visuals" };
                GetDropDownField.BuildField("", actionParams, heroAction.actionFields[7], new GenericListField(choices), true);
            }
            SimpleLayout.EndVertical();
        }
    }
}