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
    /// Work with properties in a script.
    /// </summary>
    public static class PropertyValue
    {
        /// <summary>
        /// Set multiple properties in a script.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <param name="component">The script that contains the properties.</param>
        public static void SetValueA(HeroKitObject heroKitObject, int actionFieldID, MonoBehaviour component)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // something we need isn't available. exit early.
            if (component == null)
            {
                Debug.LogError(HeroKitCommonRuntime.NoComponentDebugInfo(action.actionTemplate.name, "MonoScript", 0, heroKitObject));
                return;
            }

            // Get the start field, end field
            int propertyFieldStart = action.actionFields[actionFieldID].ints[1];
            int propertyFieldEnd = action.actionFields[actionFieldID].ints[2];

            // Get the properties
            PropertyInfo[] properties = GetProperties(component);

            // Set the properties
            int availablePropertySlots = propertyFieldEnd - propertyFieldStart;

            if (properties != null && properties.Length != 0)
            {
                for (int i = 0; i < properties.Length && i < availablePropertySlots; i++)
                {
                    // Get the property
                    PropertyInfo property = properties[i];

                    // Set the property
                    SetPropertyOnScript(heroKitObject, propertyFieldStart + i, property, component);
                }
            }
        }

        /// <summary>
        /// Get a value from a property in a script and save this value on a hero kit object.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param> 
        /// <param name="component">The script that contains the property.</param>
        public static void SetValueB(HeroKitObject heroKitObject, int actionFieldID, int actionFieldIDB, MonoBehaviour component)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // something we need isn't available. exit early.
            if (component == null)
            {
                Debug.LogError(HeroKitCommonRuntime.NoComponentDebugInfo(action.actionTemplate.name, "MonoScript", 0, heroKitObject));
                return;
            }

            // Get the property ID
            int valueID = action.actionFields[actionFieldID].ints[0];

            // Get the properties
            PropertyInfo[] properties = GetProperties(component);

            // Get the property
            if (properties != null && properties.Length != 0 && valueID > 0)
            {
                // Get the property
                PropertyInfo property = properties[valueID - 1];

                // Set the property
                SetPropertyOnHero(heroKitObject, actionFieldIDB, property, component);
            }
        }

        /// <summary>
        /// Set one property in a script.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param> 
        /// <param name="script">The script that contains the property.</param>
        public static void SetValueC(HeroKitObject heroKitObject, int actionFieldID, int actionFieldIDB, MonoBehaviour component)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // something we need isn't available. exit early.
            if (component == null)
            {
                Debug.LogError(HeroKitCommonRuntime.NoComponentDebugInfo(action.actionTemplate.name, "MonoScript", 0, heroKitObject));
                return;
            }

            // Get the property ID
            int valueID = action.actionFields[actionFieldID].ints[0];

            // Get the properties
            PropertyInfo[] properties = GetProperties(component);

            // Get the property
            if (properties != null && properties.Length != 0 && valueID > 0)
            {
                // Get the property
                PropertyInfo property = properties[valueID-1];

                // Set the property
                SetPropertyOnScript(heroKitObject, actionFieldIDB, property, component);
            }
        }

        /// <summary>
        /// Get a value from a hero object list and set this value for a property in a script.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <param name="property">The property in the script.</param>
        /// <param name="component">The script component on the hero kit object.</param>
        public static void SetPropertyOnScript(HeroKitObject heroKitObject, int actionFieldIDB, PropertyInfo property, MonoBehaviour component)
        {
            // Get value on the target instance.
            object value = property.PropertyType;

            // Test value type.
            if (value == typeof(int))
            {
                int newValue = IntegerFieldValue.GetValueA(heroKitObject, actionFieldIDB);
                property.SetValue(component, newValue, null);
            }
            else if (value == typeof(float))
            {
                float newValue = FloatFieldValue.GetValueA(heroKitObject, actionFieldIDB);
                property.SetValue(component, newValue, null);
            }
            else if (value == typeof(string))
            {
                string newValue = StringFieldValue.GetValueA(heroKitObject, actionFieldIDB);
                property.SetValue(component, newValue, null);
            }
            else if (value == typeof(bool))
            {
                bool newValue = BoolFieldValue.GetValueA(heroKitObject, actionFieldIDB);
                property.SetValue(component, newValue, null);
            }
            else if (value == typeof(HeroKitObject))
            {
                HeroKitObject newValue = HeroObjectFieldValue.GetValueA(heroKitObject, actionFieldIDB)[0];
                property.SetValue(component, newValue, null);
            }
            else if (value == typeof(GameObject))
            {
                GameObject newValue = GameObjectFieldValue.GetValueA(heroKitObject, actionFieldIDB);
                property.SetValue(component, newValue, null);
            }
        }

        /// <summary>
        /// Get a property from a script and set this value in a hero object list.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <param name="property">The property in the script.</param>
        /// <param name="component">The script component on the hero kit object.</param>
        public static void SetPropertyOnHero(HeroKitObject heroKitObject, int actionFieldIDB, PropertyInfo property, MonoBehaviour component)
        {
            // Get value type on the target instance.
            object valueType = property.PropertyType;

            // Test value type.
            if (valueType == typeof(int))
            {
                int value = (int)property.GetValue(component, null);
                IntegerFieldValue.SetValueB(heroKitObject, actionFieldIDB, value);
            }
            else if (valueType == typeof(float))
            {
                float value = (float)property.GetValue(component, null); ;
                FloatFieldValue.SetValueB(heroKitObject, actionFieldIDB, value);
            }
            else if (valueType == typeof(string))
            {
                string value = (string)property.GetValue(component, null); ;
                StringFieldValue.SetValueB(heroKitObject, actionFieldIDB, value);
            }
            else if (valueType == typeof(bool))
            {
                bool value = (bool)property.GetValue(component, null); ;
                BoolFieldValue.SetValueB(heroKitObject, actionFieldIDB, value);
            }
            else if (valueType == typeof(HeroKitObject))
            {
                HeroKitObject value = (HeroKitObject)property.GetValue(component, null); ;
                List<HeroKitObject> hko = new List<HeroKitObject>();
                hko.Add((HeroKitObject)value);
                HeroObjectFieldValue.SetValueB(heroKitObject, actionFieldIDB, hko);
            }
            else if (valueType == typeof(GameObject))
            {
                GameObject value = (GameObject)property.GetValue(component, null); ;
                GameObjectFieldValue.SetValueB(heroKitObject, actionFieldIDB, value);
            }
        }

        /// <summary>
        /// Get all properties in a script.
        /// </summary>
        /// <param name="script">The script that contains the properties.</param>
        /// <returns>The properties in the script.</returns>
        public static PropertyInfo[] GetProperties(MonoBehaviour script)
        {
            if (script == null) return new PropertyInfo[0];

            // get the class attached to the monoscript
            Type classType = script.GetType();

            // exit early of there is no class attached to monoscript
            if (classType == null) return new PropertyInfo[0];

            PropertyInfo[] propertyInfo = classType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            return propertyInfo;
        }
    }
}