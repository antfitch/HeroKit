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
    /// Attach a script to an object.
    /// </summary>
    public static class AttachScriptFields
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

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Attach the script as a component on a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            MonoScript script = GetUnityObjectField.BuildFieldA<MonoScript>("The script:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolField.BuildFieldA("Enable the component?", actionParams, heroAction.actionFields[3]);
            SimpleLayout.EndVertical();

            // add method
            if (script != null)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                bool result = GetBoolValue.BuildField("Update properties for this script component?", actionParams, heroAction.actionFields[4], true);
                if (result) GetPropertyField.BuildFieldA("", actionParams, heroAction.actionFields[5], 6, 20, script, heroAction);
                SimpleLayout.EndVertical();
            }
        }
    }
}