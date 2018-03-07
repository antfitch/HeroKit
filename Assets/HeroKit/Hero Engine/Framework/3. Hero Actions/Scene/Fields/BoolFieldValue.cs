// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get or set a value in a bool field.
    /// </summary>
    public static class BoolFieldValue
    {
        /// <summary>
        /// Get a value from a bool field.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The value from a bool field.</returns>
        public static bool GetValueA(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the bool type
            int itemType = action.actionFields[actionFieldID].ints[3];
            bool itemValue = false;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Bool type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return false;
            }
            // get bool from value field
            else if (itemType == 1)
            {
                itemValue = action.actionFields[actionFieldID].bools[0]; 
            }
            // get bool from variable field or property field
            else if (itemType == 2 || itemType == 3)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return false;
                }

                // Get the slot in the list that contains the bool
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the bool from bool list
                if (itemType == 2)
                {                  
                    if (targetHKO.heroList.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Bool", slotID, 0, heroKitObject));
                        return false;
                    }
                    itemValue = targetHKO.heroList.bools.items[slotID].value;
                }

                // Get the bool from property list
                if (itemType == 3)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Bool", slotID, 0, heroKitObject));
                        return false;
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.bools.items[slotID].value;
                }
            }
            // get bool from global field
            else if (itemType == 4)
            {
                // Get the slot in the list that contains the bool
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().bools.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Bool", slotID, 0, heroKitObject));
                    return false;
                }
                itemValue = HeroKitDatabase.GetGlobals().bools.items[slotID].value;
            }

            // Return the bool
            return itemValue;
        }

        /// <summary>
        /// Get a value from a bool field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from a bool field.</returns>
        public static bool GetValueB(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the bool type
            int itemType = action.actionFields[actionFieldID].ints[3];
            bool itemValue = false;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Bool type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return false;
            }
            // get bool from variable or property
            else if (itemType == 1 || itemType == 2)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return false;
                }

                // Get the slot in the list that contains the bool
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the bool from bool list
                if (itemType == 1)
                {
                    if (targetHKO.heroList.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Bool", slotID, 0, heroKitObject));
                        return false;
                    }
                    itemValue = targetHKO.heroList.bools.items[slotID].value;
                }

                // Get the bool from property list
                else if (itemType == 2)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Bool", slotID, 0, heroKitObject));
                        return false;
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.bools.items[slotID].value;
                }
            }
            // get bool from globals
            else if (itemType == 3)
            {
                // Get the slot in the list that contains the bool
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().bools.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Bool", slotID, 0, heroKitObject));
                    return false;
                }
                itemValue = HeroKitDatabase.GetGlobals().bools.items[slotID].value;
            }

            // Return the bool
            return itemValue;
        }

        /// <summary>
        /// Get a value from a bool field in a hero object template.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from a bool field.</returns>
        public static bool GetValueC(HeroKitObject heroKitObject, int actionFieldID, HeroObject heroObject)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the item type
            int itemType = action.actionFields[actionFieldID].ints[3];
            bool itemValue = false;

            // Get the slot in the list that contains the item
            int slotID = action.actionFields[actionFieldID].ints[2] - 1;

            // Get the lists
            BoolList targetList = null;
            string itemTypeName = "";
            string heroName = "";
            if (itemType == 0)
            {
                Debug.LogError("Bool type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return false;
            }
            else if (itemType == 1)
            {
                // exit early if object does not exist
                if (heroObject == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return false;
                }

                heroName = heroObject.name;
                itemTypeName = "Variables";
                targetList = heroObject.lists.bools;
            }
            else if (itemType == 2)
            {
                // exit early if object does not exist
                if (heroObject == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return false;
                }

                heroName = heroObject.name;
                itemTypeName = "Properties";
                int propertyID = action.actionFields[actionFieldID].ints[5] - 1;
                if (propertyID < 0) Debug.LogError("Property slot does not exist!");
                targetList = heroObject.propertiesList.properties[propertyID].itemProperties.bools;
            }
            else if (itemType == 3)
            {
                heroName = "n/a";
                itemTypeName = "Globals";
                targetList = HeroKitDatabase.GetGlobals().bools;
            }

            // exit early if the slot in the list does not exist
            if (targetList.items.Count <= slotID || slotID < 0)
            {
                Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, heroName, itemTypeName, "Bool", slotID, 0, heroKitObject));
                return false;
            }

            // get the item in the list slot
            itemValue = targetList.items[slotID].value;

            // Return the item
            return itemValue;
        }

        /// <summary>
        /// Get a value from a bool field on an event.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from a bool field.</returns>
        public static bool GetValueEvent(HeroKitObject heroKitObject, ConditionBoolField eventField)
        {
            // Get the bool type
            int itemType = eventField.fieldType;
            bool itemValue = false;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Bool type was never specified for " + "event" + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return false;
            }
            // get bool from field
            else if (itemType == 1)
            {
                itemValue = eventField.fieldValue;
            }
            // get bool from variable field or property field
            else if (itemType == 2 || itemType == 3)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectFieldValue.GetValueEvent(heroKitObject, eventField);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo("event", 0, heroKitObject));
                    return false;
                }

                // Get the slot in the list that contains the bool
                int slotID = eventField.fieldID - 1;

                // Get the bool from bool list
                if (itemType == 2)
                {
                    if (targetHKO.heroList.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo("event", targetHKO.heroObject.name, "Variables", "Bool", slotID, 0, heroKitObject));
                        return false;
                    }
                    itemValue = targetHKO.heroList.bools.items[slotID].value;
                }

                // Get the bool from property list
                if (itemType == 3)
                {
                    int propertyID = eventField.propertyID - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo("event", targetHKO.heroObject.name, "Properties", "Bool", slotID, 0, heroKitObject));
                        return false;
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.bools.items[slotID].value;
                }

            }
            // get bool from global
            else if (itemType == 4)
            {
                // Get the slot in the list that contains the bool
                int slotID = eventField.fieldID - 1;

                if (HeroKitDatabase.GetGlobals().bools.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo("event", "n/a", "Globals", "Bool", slotID, 0, heroKitObject));
                    return false;
                }
                itemValue = HeroKitDatabase.GetGlobals().bools.items[slotID].value;
            }

            // Return the bool
            return itemValue;
        }

        /// <summary>
        /// Set the value for a bool in a bool field. 
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <param name="newValue">The new value for a bool field.</param>
        public static void SetValueB(HeroKitObject heroKitObject, int actionFieldID, bool newValue)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the bool type
            int itemType = action.actionFields[actionFieldID].ints[3];

            // no item
            if (itemType == 0)
            {
                Debug.LogError("Bool type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return;
            }
            // set item in variable or property list
            else if (itemType == 1 || itemType == 2)
            {
                // Get the hero kit object
                HeroKitObject[] targetHKO = HeroObjectFieldValue.GetTargetHeroObjects(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return;
                }

                // Get the slot in the list that contains the bool
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // set variable
                if (itemType == 1)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        if (targetHKO[i].heroList.bools.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Variables", "Bool", slotID, 0, heroKitObject));
                            return;
                        }

                        // Set the bool
                        targetHKO[i].heroList.bools.items[slotID].value = newValue;
                    }
                }

                // set property
                if (itemType == 2)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                        if (targetHKO[i].heroProperties == null || targetHKO[i].heroProperties.Length == 0 ||
                            targetHKO[i].heroProperties.Length <= propertyID || propertyID < 0 ||
                            targetHKO[i].heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Properties", "Bool", slotID, 0, heroKitObject));
                            return;
                        }

                        targetHKO[i].heroProperties[propertyID].itemProperties.bools.items[slotID].value = newValue;
                    }
                }
            }
            // set item in global list
            if (itemType == 3)
            {
                // Get the slot in the list that contains the bool
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().bools.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Bool", slotID, 0, heroKitObject));
                    return;
                }
                HeroKitDatabase.GetGlobals().bools.items[slotID].value = newValue;
            }
        }
    }
}