// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get or set the value of a parameter in a script.
    /// </summary>
    public static class ParameterValue
    {
        /// <summary>
        /// Get multiple parameters from a method in a script.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <param name="method">The method that contains the parameters.</param>
        public static System.Object[] GetValueA(HeroKitObject heroKitObject, int actionFieldID, MethodInfo method)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // something we need isn't available. exit early.
            if (method == null)
            {
                Debug.LogError(HeroKitCommonRuntime.NoComponentDebugInfo(action.actionTemplate.name, "MethodInfo", 0, heroKitObject));
                return null;
            }

            // Get the start field, end field
            int paramFieldStart = action.actionFields[actionFieldID].ints[1];
            int paramFieldEnd = action.actionFields[actionFieldID].ints[2];

            // Get the parameter from the method
            ParameterInfo[] parameters = GetParameters(method);

            // Get the parameter from the hero object
            int availableParamSlots = paramFieldEnd - paramFieldStart;
            System.Object[] objects = new System.Object[parameters.Length]; 
            if (parameters != null && parameters.Length != 0)
            {
                for (int i = 0; i < parameters.Length && i < availableParamSlots; i++)
                {
                    // Get the parameter
                    ParameterInfo parameter = parameters[i];

                    // Set the parameter
                    objects[i] = GetParameter(heroKitObject, paramFieldStart + i, parameter);
                }
            }

            return objects;
        }

        /// <summary>
        /// Get a parameter from a script and save it in a hero object list.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <param name="parameter">The parameter to save in a hero object list.</param>
        public static void SetValueB(HeroKitObject heroKitObject, int actionFieldIDA, int actionFieldIDB, System.Object parameter)
        {
            // Get the property
            if (parameter != null)
            {
                // Set the property
                SetParameter(heroKitObject, actionFieldIDB, parameter);
            }
        }

        /// <summary>
        /// Get all parameters in a method.
        /// </summary>
        /// <param name="method">The method that contains the parameters.</param>
        /// <returns>The parameters in the method.</returns>
        public static ParameterInfo[] GetParameters(MethodInfo method)
        {
            if (method == null) return new ParameterInfo[0];

            return method.GetParameters();
        }

        /// <summary>
        /// Get a value from a hero object list that will be assigned to a parameter in a script.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The value from the hero object list.</returns>
        public static System.Object GetParameter(HeroKitObject heroKitObject, int actionFieldIDB, ParameterInfo parameter)
        {
            // Get value on the target instance.
            object value = parameter.ParameterType;
            System.Object item = null;

            // Test value type.
            if (value == typeof(int))
            {
                item = IntegerFieldValue.GetValueA(heroKitObject, actionFieldIDB);
            }
            else if (value == typeof(float))
            {
                item = FloatFieldValue.GetValueA(heroKitObject, actionFieldIDB);
            }
            else if (value == typeof(string))
            {
                item = StringFieldValue.GetValueA(heroKitObject, actionFieldIDB);
            }
            else if (value == typeof(bool))
            {
                item = BoolFieldValue.GetValueA(heroKitObject, actionFieldIDB);
            }
            else if (value == typeof(HeroKitObject))
            {
                item = HeroObjectFieldValue.GetValueA(heroKitObject, actionFieldIDB)[0];
            }
            else if (value == typeof(GameObject))
            {
                item = GameObjectFieldValue.GetValueA(heroKitObject, actionFieldIDB);
            }

            return item;
        }

        /// <summary>
        /// Set a value in a hero object list. Use the value of the parameter in a script.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <param name="parameter">The parameter.</param>
        public static void SetParameter(HeroKitObject heroKitObject, int actionFieldIDB, System.Object parameter)
        {
            // Get value on the target instance.
            object value = parameter.GetType();

            // Test value type.
            if (value == typeof(int))
            {
                IntegerFieldValue.SetValueB(heroKitObject, actionFieldIDB, (int)parameter);
            }
            else if (value == typeof(float))
            {
                FloatFieldValue.SetValueB(heroKitObject, actionFieldIDB, (float)parameter);
            }
            else if (value == typeof(string))
            {
                StringFieldValue.SetValueB(heroKitObject, actionFieldIDB, (string)parameter);
            }
            else if (value == typeof(bool))
            {
                BoolFieldValue.SetValueB(heroKitObject, actionFieldIDB, (bool)parameter);
            }
            else if (value == typeof(HeroKitObject))
            {
                List<HeroKitObject> hko = new List<HeroKitObject>();
                hko.Add((HeroKitObject)parameter);
                HeroObjectFieldValue.SetValueB(heroKitObject, actionFieldIDB, hko);
            }
            else if (value == typeof(GameObject))
            {
                GameObjectFieldValue.SetValueB(heroKitObject, actionFieldIDB, (GameObject)parameter);
            }
        }
    }
}