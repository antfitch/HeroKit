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
    /// Get a field in a component on an object.
    /// </summary>
    public static class GetFieldInfoFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 5);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Use a script on a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            MonoScript script = GetUnityObjectField.BuildFieldA<MonoScript>("The script on the hero object:", actionParams, heroAction.actionFields[2]);
            // select the property to change
            if (script != null)
            {
                GetFieldInfoField.BuildFieldB("The field in the script to save on the hero object:", actionParams, heroAction.actionFields[3], heroAction.actionFields[4], script);
            }
            SimpleLayout.EndVertical();
        }
    }
}