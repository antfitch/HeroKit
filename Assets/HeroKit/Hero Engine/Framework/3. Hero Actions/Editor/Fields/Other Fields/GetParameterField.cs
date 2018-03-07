// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionBlockFields;
using System.Reflection;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with parameters in a script.
    /// </summary>
    public static class GetParameterField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Set the parameters to be passed into a method in a script.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="paramFieldStart">Action field ID (start of range).</param>
        /// <param name="paramFieldEnd">Action field ID (end of range).</param>
        /// <param name="method">The method that contains the parameters.</param>
        /// <param name="heroAction"></param>
        public static void BuildFieldA(string title, HeroActionParams actionParams, HeroActionField actionField, int paramFieldStart, int paramFieldEnd, MethodInfo method, HeroAction heroAction)
        {
            // create the fields
            ParameterFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Get the list you want to work with.
            //-----------------------------------------
            ParameterInfo[] parameters = GetParameters(method);
            int availablePropertySlots = paramFieldEnd - paramFieldStart;

            if (parameters != null && parameters.Length != 0)
            {
                if (data.title != "") SimpleLayout.Label(data.title);

                SimpleLayout.Line();

                for (int i = 0; i < parameters.Length && i < availablePropertySlots; i++)
                {
                    ParameterInfo parameter = parameters[i];
                    GetParameter(parameter, actionParams, heroAction.actionFields[paramFieldStart + i], false);
                }
            }
            else
            {
                SimpleLayout.Label("There are not parameters for this method.");
            }

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[1] = paramFieldStart;
            actionField.ints[2] = paramFieldEnd;
        }

        /// <summary>
        /// Get a return value from a method and assign the value to a hero object variable.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="actionFieldB">Action field.</param>
        /// <param name="method">The method that contains the parameters.</param>
        public static void BuildFieldB(string title, HeroActionParams actionParams, HeroActionField actionField, HeroActionField actionFieldB, MethodInfo method)
        {
            // create the fields
            ParameterFieldData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Get the list you want to work with.
            //-----------------------------------------
            ParameterInfo returnValue = method.ReturnParameter;

            if (returnValue != null)
            {
                if (data.title != "") SimpleLayout.Label(data.title);

                SimpleLayout.Line();
                GetParameter(returnValue, actionParams, actionFieldB, true);
            }
        }

        // --------------------------------------------------------------
        // Helpers
        // --------------------------------------------------------------

        /// <summary>
        /// Get all parameters in a method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The parameters in the method.</returns>
        private static ParameterInfo[] GetParameters(MethodInfo method)
        {
            if (method == null) return new ParameterInfo[0];

            return method.GetParameters();
        }

        /// <summary>
        /// Create a field in the hero kit editor for a parameter.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionFieldB">Action field.</param>
        /// <param name="setHeroValue">True=user can set a value on a hero object. False=user can set a value in a script.</param>
        private static void GetParameter(ParameterInfo param, HeroActionParams actionParams, HeroActionField actionFieldB, bool setHeroValue)
        {
            // Get value on the target instance.
            object value = param.ParameterType;

            // Test value type.
            if (value == typeof(int))
            {
                if (!setHeroValue)
                    GetIntegerField.BuildFieldA(param.Name + " (Int): ", actionParams, actionFieldB);
                else
                    GetIntegerField.BuildFieldB(param.Name + " (Int): ", actionParams, actionFieldB);
            }
            else if (value == typeof(float))
            {
                if (!setHeroValue)
                    GetFloatField.BuildFieldA(param.Name + " (Float): ", actionParams, actionFieldB);
                else
                    GetFloatField.BuildFieldB(param.Name + " (Float): ", actionParams, actionFieldB);
            }
            else if (value == typeof(string))
            {
                if (!setHeroValue)
                    GetStringField.BuildFieldA(param.Name + " (String): ", actionParams, actionFieldB, false, false, -10);
                else
                    GetStringField.BuildFieldB(param.Name + " (String): ", actionParams, actionFieldB);
            }
            else if (value == typeof(bool))
            {
                if (!setHeroValue)
                    GetBoolField.BuildFieldA(param.Name + " (Bool): ", actionParams, actionFieldB);
                else
                    GetBoolField.BuildFieldB(param.Name + " (Bool): ", actionParams, actionFieldB);
            }
            else if (value == typeof(HeroKitObject))
            {
                if (!setHeroValue)
                    GetHeroObjectField.BuildFieldA(param.Name + " (HeroObject): ", actionParams, actionFieldB);
                else
                    GetHeroObjectField.BuildFieldB(param.Name + " (HeroObject): ", actionParams, actionFieldB);
            }
            else if (value == typeof(GameObject))
            {
                if (!setHeroValue)
                    GetGameObjectField.BuildFieldA(param.Name + " (GameObject): ", actionParams, actionFieldB);
                else
                    GetGameObjectField.BuildFieldB(param.Name + " (GameObject): ", actionParams, actionFieldB);
            }
            else
            {
                string valueType = value.ToString();
                int index = valueType.LastIndexOf('.');
                if (valueType.Length >= 2)
                    valueType = valueType.Substring(index + 1, valueType.Length - index - 1);

                SimpleLayout.Label(valueType + " " + param.Name + ": [Value can't be stored by HeroKit]");
            }
        }

        // --------------------------------------------------------------
        // Initialize Action Field
        // --------------------------------------------------------------

        /// <summary>
        /// Create the subfields that we need for this action field.
        /// </summary>
        /// <param name="title">The title of the action.</param>
        /// <param name="actionField">The action field.</param>
        /// <param name="heroObject">The hero object that contains this action field.</param>
        /// <returns>The data for this action field.</returns>
        private static ParameterFieldData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            ParameterFieldData data = new ParameterFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.startID = actionField.ints[1];
            data.endID = actionField.ints[2];
            return data;
        }
    }

    /// <summary>
    /// Data needed to use the GetParameterField.
    /// </summary>
    public struct ParameterFieldData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 3, 0);
        }

        public string title { get; set; }
        public int startID { get; set; }
        public int endID { get; set; }
    }
}