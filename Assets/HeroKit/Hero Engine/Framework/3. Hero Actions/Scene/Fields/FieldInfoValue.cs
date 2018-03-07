// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Work with fields in a script.
    /// </summary>
    public static class FieldInfoValue
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Set one field in a script.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param> 
        /// <param name="component">The script that contains the field.</param>
        public static void SetValueA(HeroKitObject heroKitObject, int actionFieldIDA, int actionFieldIDB, MonoBehaviour component)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // something we need isn't available. exit early.
            if (component == null)
            {
                Debug.LogError(HeroKitCommonRuntime.NoComponentDebugInfo(action.actionTemplate.name, "MonoScript", 0, heroKitObject));
                return;
            }

            // Get the field ID
            int valueID = action.actionFields[actionFieldIDA].ints[0];

            // Get the fields
            FieldInfo[] fields = GetFields(component);

            // Get the field
            if (fields != null && fields.Length != 0 && valueID > 0)
            {
                // Get the field
                FieldInfo field = fields[valueID-1];

                // Set the field
                SetFieldOnScript(heroKitObject, actionFieldIDB, field, component);
            }
        }

        /// <summary>
        /// Get a value from a field in a script and save this value on a hero kit object.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param> 
        /// <param name="component">The script that contains the field.</param>
        public static void SetValueB(HeroKitObject heroKitObject, int actionFieldIDA, int actionFieldIDB, MonoBehaviour component)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // something we need isn't available. exit early.
            if (component == null)
            {
                Debug.LogError(HeroKitCommonRuntime.NoComponentDebugInfo(action.actionTemplate.name, "MonoScript", 0, heroKitObject));
                return;
            }

            // Get the field ID
            int valueID = action.actionFields[actionFieldIDA].ints[0];

            // Get the fields
            FieldInfo[] fields = GetFields(component);

            // Get the field
            if (fields != null && fields.Length != 0 && valueID > 0)
            {
                // Get the field
                FieldInfo field = fields[valueID - 1];

                // Set the field
                SetFieldOnHero(heroKitObject, actionFieldIDB, field, component);
            }
        }

        // --------------------------------------------------------------
        // Helpers
        // --------------------------------------------------------------

        /// <summary>
        /// Get a value from a hero object list and set this value for a field in a script.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <param name="field">The field in the script.</param>
        /// <param name="component">The script component on the hero kit object.</param>
        private static void SetFieldOnScript(HeroKitObject heroKitObject, int actionFieldIDB, FieldInfo field, MonoBehaviour component)
        {
            // Get value on the target instance.
            object value = field.FieldType;

            // Test value type.
            if (value == typeof(int))
            {
                int newValue = IntegerFieldValue.GetValueA(heroKitObject, actionFieldIDB);
                field.SetValue(component, newValue);
            }
            else if (value == typeof(float))
            {
                float newValue = FloatFieldValue.GetValueA(heroKitObject, actionFieldIDB);
                field.SetValue(component, newValue);
            }
            else if (value == typeof(string))
            {
                string newValue = StringFieldValue.GetValueA(heroKitObject, actionFieldIDB);
                field.SetValue(component, newValue);
            }
            else if (value == typeof(bool))
            {
                bool newValue = BoolFieldValue.GetValueA(heroKitObject, actionFieldIDB);
                field.SetValue(component, newValue);
            }
            else if (value == typeof(HeroKitObject))
            {
                HeroKitObject newValue = HeroObjectFieldValue.GetValueA(heroKitObject, actionFieldIDB)[0];
                field.SetValue(component, newValue);
            }
            else if (value == typeof(GameObject))
            {
                GameObject newValue = GameObjectFieldValue.GetValueA(heroKitObject, actionFieldIDB);
                field.SetValue(component, newValue);
            }
        }

        /// <summary>
        /// Get a field from a script and set this value in a hero object list.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <param name="field">The field in the script.</param>
        /// <param name="component">The script component on the hero kit object.</param>
        private static void SetFieldOnHero(HeroKitObject heroKitObject, int actionFieldIDB, FieldInfo field, MonoBehaviour component)
        {
            // Get value type on the target instance.
            object valueType = field.FieldType;

            // Test value type.
            if (valueType == typeof(int))
            {
                int value = (int)field.GetValue(component);
                IntegerFieldValue.SetValueB(heroKitObject, actionFieldIDB, value);
            }
            else if (valueType == typeof(float))
            {
                float value = (float)field.GetValue(component); ;
                FloatFieldValue.SetValueB(heroKitObject, actionFieldIDB, value);
            }
            else if (valueType == typeof(string))
            {
                string value = (string)field.GetValue(component); ;
                StringFieldValue.SetValueB(heroKitObject, actionFieldIDB, value);
            }
            else if (valueType == typeof(bool))
            {
                bool value = (bool)field.GetValue(component); ;
                BoolFieldValue.SetValueB(heroKitObject, actionFieldIDB, value);
            }
            else if (valueType == typeof(HeroKitObject))
            {
                HeroKitObject value = (HeroKitObject)field.GetValue(component); ;
                List<HeroKitObject> hko = new List<HeroKitObject>();
                hko.Add((HeroKitObject)value);
                HeroObjectFieldValue.SetValueB(heroKitObject, actionFieldIDB, hko);
            }
            else if (valueType == typeof(GameObject))
            {
                GameObject value = (GameObject)field.GetValue(component); ;
                GameObjectFieldValue.SetValueB(heroKitObject, actionFieldIDB, value);
            }
        }

        /// <summary>
        /// Get all fields in a script.
        /// </summary>
        /// <param name="script">The script that contains the fields.</param>
        /// <returns>The fields in the script.</returns>
        private static FieldInfo[] GetFields(MonoBehaviour script)
        {
            if (script == null) return new FieldInfo[0];

            // get the class attached to the monoscript
            Type classType = script.GetType();

            // exit early of there is no class attached to monoscript
            if (classType == null) return new FieldInfo[0];

            FieldInfo[] fieldInfo = classType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            return fieldInfo;
        }
    }
}