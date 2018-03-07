// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get or set a value in a float field.
    /// </summary>
    public static class FloatFieldValue
    {
        /// <summary>
        /// Get a value from a float field.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The value from a float field.</returns>
        public static float GetValueA(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the Float type
            int itemType = action.actionFields[actionFieldID].ints[3];
            float itemValue = 0;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Float type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return 0f;
            }
            // get Float from field
            else if (itemType == 1)
            {
                itemValue = action.actionFields[actionFieldID].floats[0];
            }
            // get Float from Float field or property field
            else if (itemType == 2 || itemType == 3)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return 0f;
                }

                // Get the slot in the list that contains the Float
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the Float from Float list
                if (itemType == 2)
                {
                    if (targetHKO.heroList.floats.items.Count <= slotID)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Float", slotID, 0, heroKitObject));
                        return 0;
                    }
                    itemValue = targetHKO.heroList.floats.items[slotID].value;
                }

                // Get the Float from property list
                if (itemType == 3)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Float", slotID, 0, heroKitObject));
                        return 0;
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.floats.items[slotID].value;
                }

            }
            // get Float from global field
            else if (itemType == 4)
            {
                // Get the slot in the list that contains the float
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().floats.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Float", slotID, 0, heroKitObject));
                    return 0;
                }
                itemValue = HeroKitDatabase.GetGlobals().floats.items[slotID].value;
            }

            // Return the Float
            return itemValue;
        }

        /// <summary>
        /// Get a value from a float field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from a float field.</returns>
        public static float GetValueB(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the Float type
            int itemType = action.actionFields[actionFieldID].ints[3];
            float itemValue = 0;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Float type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return 0f;
            }
            // get item from variable or property list
            else if (itemType == 1 || itemType == 2)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return 0f;
                }

                // Get the slot in the list that contains the Float
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the Float from Float list
                if (itemType == 1)
                {
                    if (targetHKO.heroList.floats.items.Count <= slotID)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Float", slotID, 0, heroKitObject));
                        return 0;
                    }
                    itemValue = targetHKO.heroList.floats.items[slotID].value;
                }

                // Get the Float from property list
                if (itemType == 2)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Float", slotID, 0, heroKitObject));
                        return 0;
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.floats.items[slotID].value;
                }
            }
            // get float from globals
            else if (itemType == 3)
            {
                // Get the slot in the list that contains the bool
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().floats.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Float", slotID, 0, heroKitObject));
                    return 0f;
                }
                itemValue = HeroKitDatabase.GetGlobals().floats.items[slotID].value;
            }

            // Return the Float
            return itemValue;
        }

        /// <summary>
        /// Get a value from a float field in a hero object template.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from a float field.</returns>
        public static float GetValueC(HeroKitObject heroKitObject, int actionFieldID, HeroObject heroObject)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // exit early if object does not exist
            if (heroObject == null)
            {
                Debug.LogError(HeroKitCommonRuntime.NoHeroObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                return 0f;
            }

            // Get the item type
            int itemType = action.actionFields[actionFieldID].ints[3];
            float itemValue = 0f;

            // Get the slot in the list that contains the item
            int slotID = action.actionFields[actionFieldID].ints[2] - 1;

            // Get the lists
            FloatList targetList = null;
            string itemTypeName = "";
            string heroName = "";
            if (itemType == 0)
            {
                Debug.LogError("Float type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return 0f;
            }
            else if (itemType == 1)
            {
                heroName = heroObject.name;
                itemTypeName = "Variables";
                targetList = heroObject.lists.floats;
            }
            else if (itemType == 2)
            {
                heroName = heroObject.name;
                itemTypeName = "Properties";
                int propertyID = action.actionFields[actionFieldID].ints[7] - 1;
                if (propertyID < 0) Debug.LogError("Property slot does not exist!");
                targetList = heroObject.propertiesList.properties[propertyID].itemProperties.floats;
            }
            else if (itemType == 3)
            {
                heroName = "n/a";
                itemTypeName = "Globals";
                targetList = HeroKitDatabase.GetGlobals().floats;
            }

            // exit early if the slot in the list does not exist
            if (targetList.items.Count <= slotID || slotID < 0)
            {
                Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, heroName, itemTypeName, "Float", slotID, 0, heroKitObject));
                return 0f;
            }

            // get the item in the list slot
            itemValue = targetList.items[slotID].value;

            // Return the item
            return itemValue;
        }

        /// <summary>
        /// Set the value for a float in a float field. 
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <param name="newValue">The new value for a float field.</param>
        public static void SetValueB(HeroKitObject heroKitObject, int actionFieldID, float newValue)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the Float type
            int itemType = action.actionFields[actionFieldID].ints[3];

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Float type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
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

                // Get the slot in the list that contains the Float
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the Float from the Float list
                if (itemType == 1)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        if (targetHKO[i].heroList.floats.items.Count <= slotID)
                        {
                            Debug.LogError("a float slot assigned to this action no longer exists." + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                        }

                        // Set the Float
                        targetHKO[i].heroList.floats.items[slotID].value = newValue;
                    }
                }

                // Get the Float from the property list
                if (itemType == 2)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                        if (targetHKO[i].heroProperties == null || targetHKO[i].heroProperties.Length == 0 ||
                            targetHKO[i].heroProperties.Length <= propertyID || propertyID < 0 ||
                            targetHKO[i].heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError("An property slot assigned to this action no longer exists." + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                        }

                        // Set the Float
                        targetHKO[i].heroProperties[propertyID].itemProperties.floats.items[slotID].value = newValue;
                    }
                }
            }
            // set item in global list
            if (itemType == 3)
            {
                // Get the slot in the list that contains the item
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().floats.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Float", slotID, 0, heroKitObject));
                    return;
                }
                HeroKitDatabase.GetGlobals().floats.items[slotID].value = newValue;
            }
        }
    }
}