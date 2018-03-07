// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get or set a value in an int field.
    /// </summary>
    public static class IntegerFieldValue
    {
        /// <summary>
        /// Get a value from an int field.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The value from an int field.</returns>
        public static int GetValueA(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the integer type
            int itemType = action.actionFields[actionFieldID].ints[3];
            int itemValue = 0;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Integer type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return 0;
            }
            // get integer from field
            if (itemType == 1)
            {
                itemValue = action.actionFields[actionFieldID].ints[5]; 
            }
            // get integer from integer field or property field
            else if (itemType == 2 || itemType == 3)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return 0;
                }

                // Get the slot in the list that contains the integer
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the integer from integer list
                if (itemType == 2)
                {                  
                    if (targetHKO.heroList.ints.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Integer", slotID, 0, heroKitObject));
                        return 0;
                    }
                    itemValue = targetHKO.heroList.ints.items[slotID].value;
                }

                // Get the integer from property list
                if (itemType == 3)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[6] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.ints.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Int", slotID, 0, heroKitObject));
                        return 0;
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.ints.items[slotID].value;
                }

            }
            // get integer from global field
            else if (itemType == 4)
            {
                // Get the slot in the list that contains the string
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().ints.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Integer", slotID, 0, heroKitObject));
                    return 0;
                }
                itemValue = HeroKitDatabase.GetGlobals().ints.items[slotID].value;
            }

            // Return the integer
            return itemValue;
        }

        /// <summary>
        /// Get a value from an int field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from an int field.</returns>
        public static int GetValueB(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the integer type
            int itemType = action.actionFields[actionFieldID].ints[3];
            int itemValue = 0;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Integer type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return 0;
            }
            // get item from variable or property list
            else if (itemType == 1 || itemType == 2)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return 0;
                }

                // Get the slot in the list that contains the integer
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the integer from integer list
                if (itemType == 1)
                {
                    if (targetHKO.heroList.ints.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Integer", slotID, 0, heroKitObject));
                        return 0;
                    }

                    itemValue = targetHKO.heroList.ints.items[slotID].value;
                }

                // Get the integer from property list
                if (itemType == 2)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[6] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.ints.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Int", slotID, 0, heroKitObject));
                        return 0;
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.ints.items[slotID].value;
                }
            }
            // get int from globals
            else if (itemType == 3)
            {
                // Get the slot in the list that contains the int
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().ints.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Integer", slotID, 0, heroKitObject));
                    return 0;
                }
                itemValue = HeroKitDatabase.GetGlobals().ints.items[slotID].value;
            }

            // Return the integer
            return itemValue;
        }

        /// <summary>
        /// Get a value from an int field in a hero object template.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from an int field.</returns>
        public static int GetValueC(HeroKitObject heroKitObject, int actionFieldID, HeroObject heroObject)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // exit early if object does not exist
            if (heroObject == null)
            {
                Debug.LogError(HeroKitCommonRuntime.NoHeroObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                return 0;
            }

            // Get the item type
            int itemType = action.actionFields[actionFieldID].ints[3];
            int itemValue = 0;

            // Get the slot in the list that contains the item
            int slotID = action.actionFields[actionFieldID].ints[2] - 1;

            // Get the lists
            IntList targetList = null;
            string itemTypeName = "";
            string heroName = "";
            if (itemType == 0)
            {
                Debug.LogError("Integer type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return 0;
            }
            if (itemType == 1)
            {
                heroName = heroObject.name;
                itemTypeName = "variable";
                targetList = heroObject.lists.ints;
            }
            else if (itemType == 2)
            {          
                heroName = heroObject.name;
                itemTypeName = "property";
                int propertyID = action.actionFields[actionFieldID].ints[6] - 1;
                if (propertyID < 0) Debug.LogError("Property slot does not exist!");
                targetList = heroObject.propertiesList.properties[propertyID].itemProperties.ints;
            }
            else if (itemType == 3)
            {
                heroName = "n/a";
                itemTypeName = "Globals";
                targetList = HeroKitDatabase.GetGlobals().ints;
            }

            // exit early if the slot in the list does not exist
            if (targetList.items.Count <= slotID || slotID < 0)
            {
                Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, heroName, itemTypeName, "Integer", slotID, 0, heroKitObject));
                return 0;
            }

            // get the item in the list slot
            itemValue = targetList.items[slotID].value;

            // Return the item
            return itemValue;
        }

        /// <summary>
        /// Get a value from an int field on an event.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from an int field.</returns>
        public static int GetValueEvent(HeroKitObject heroKitObject, ConditionIntField eventField)
        {
            // Get the integer type
            int itemType = eventField.fieldType;
            int itemValue = 0;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Integer type was never specified for " + "event" + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return 0;
            }
            // get integer from field
            else if (itemType == 1)
            {
                itemValue = eventField.fieldValue;
            }
            // get integer from integer field or property field
            else if (itemType == 2 || itemType == 3)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectFieldValue.GetValueEvent(heroKitObject, eventField);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo("event", 0, heroKitObject));
                    return 0;
                }

                // Get the slot in the list that contains the integer
                int slotID = eventField.fieldID - 1;

                // Get the integer from integer list
                if (itemType == 2)
                {
                    if (targetHKO.heroList.ints.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo("event", targetHKO.heroObject.name, "Variables", "Integer", slotID, 0, heroKitObject));
                        return 0;
                    }
                    itemValue = targetHKO.heroList.ints.items[slotID].value;
                }

                // Get the integer from property list
                if (itemType == 3)
                {
                    int propertyID = eventField.propertyID - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.ints.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo("event", targetHKO.heroObject.name, "Properties", "Integer", slotID, 0, heroKitObject));
                        return 0;
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.ints.items[slotID].value;
                }
            }
            // get integer from global
            else if (itemType == 4)
            {
                // Get the slot in the list that contains the integer
                int slotID = eventField.fieldID - 1;

                if (HeroKitDatabase.GetGlobals().ints.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo("event", "n/a", "Globals", "Integer", slotID, 0, heroKitObject));
                    return 0;
                }
                itemValue = HeroKitDatabase.GetGlobals().ints.items[slotID].value;
            }

            // Return the integer
            return itemValue;
        }

        /// <summary>
        /// Set a value in an int field. 
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from an int field.</returns>
        public static void SetValueB(HeroKitObject heroKitObject, int actionFieldID, int newValue)
        {
            SetValueB(heroKitObject, heroKitObject.heroStateData.eventBlock, heroKitObject.heroStateData.action, actionFieldID, newValue);        
        }

        /// <summary>
        /// Set a value for an int field. 
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="eventID">The ID assigned to the event.</param>
        /// <param name="actionID">The ID assigned to the action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <param name="newValue">The value from an int field.</param>
        public static void SetValueB(HeroKitObject heroKitObject, int eventID, int actionID, int actionFieldID, int newValue)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[eventID].actions[actionID];

            // Get the integer type
            int itemType = action.actionFields[actionFieldID].ints[3];

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Integer type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return;
            }
            // set item in variable or property list
            else if (itemType == 1 || itemType == 2)
            {
                // Get the hero kit object
                HeroKitObject[] targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, eventID, actionID, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return;
                }

                // Get the slot in the list that contains the integer
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the integer from the integer list
                if (itemType == 1)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        if (targetHKO[i].heroList.ints.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Variables", "Integer", slotID, 0, heroKitObject));
                            return;
                        }

                        // Set the integer
                        targetHKO[i].heroList.ints.items[slotID].value = newValue;
                    }
                }

                // Get the integer from the property list
                if (itemType == 2)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        int propertyID = action.actionFields[actionFieldID].ints[6] - 1;

                        if (targetHKO[i].heroProperties == null || targetHKO[i].heroProperties.Length == 0 ||
                            targetHKO[i].heroProperties.Length <= propertyID || propertyID < 0 ||
                            targetHKO[i].heroProperties[propertyID].itemProperties.ints.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Properties", "Int", slotID, 0, heroKitObject));
                            return;
                        }

                        targetHKO[i].heroProperties[propertyID].itemProperties.ints.items[slotID].value = newValue;
                    }
                }
            }
            // set item in global list
            if (itemType == 3)
            {
                // Get the slot in the list that contains the item
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().ints.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Integer", slotID, 0, heroKitObject));
                    return;
                }
                HeroKitDatabase.GetGlobals().ints.items[slotID].value = newValue;
            }
        }
    }
}