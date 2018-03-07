// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using UnityEditor;
using System.Reflection;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Call a method in a component on an object.
    /// </summary>
    public static class CallMethodFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 22);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Use the method on a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            // add class
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            MonoScript script = GetUnityObjectField.BuildFieldA<MonoScript>("The script on the hero object:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();

            // add method
            if (script != null)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                SimpleLayout.Label("The method to call in the script:");
                MethodInfo method = GetMethodField.BuildFieldA(script, "", actionParams, heroAction.actionFields[3]);
                SimpleLayout.EndVertical();

                if (method != null)
                {
                    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                    // add parameters (up to 15)
                    GetParameterField.BuildFieldA("Parameters to pass into the method:", actionParams, heroAction.actionFields[4], 5, 19, method, heroAction);
                    SimpleLayout.EndVertical();

                    SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                    // add return value (1)
                    GetParameterField.BuildFieldB("Return value to save on the hero object:", actionParams, heroAction.actionFields[20], heroAction.actionFields[21], method);
                    SimpleLayout.EndVertical();
                }
            }
        }
    }
}