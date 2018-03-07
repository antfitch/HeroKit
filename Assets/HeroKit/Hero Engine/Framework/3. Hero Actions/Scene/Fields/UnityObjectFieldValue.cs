// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get or set a value in a unity object field.
    /// </summary>
    public static class UnityObjectFieldValue
    {
        /// <summary>
        /// Get a value from a unity object field.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The value from a unity object field.</returns>
        public static UnityObjectField GetValueA(HeroKitObject heroKitObject, int actionFieldID, bool requiredField = true)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the object type
            int itemType = action.actionFields[actionFieldID].ints[3];
            UnityObjectField itemValue = new UnityObjectField();
            itemValue.sceneID = -1;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                if (requiredField) Debug.LogError("Unity Object type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return itemValue;
            }
            // get object from field
            else if (itemType == 1)
            {
                itemValue.value = action.actionFields[actionFieldID].unityObjects[0];
                itemValue.sceneID = action.actionFields[actionFieldID].ints[5];
                itemValue.sceneName = action.actionFields[actionFieldID].strings[1];
            }
            // get object from variable field or property field
            else if (itemType == 2 || itemType == 3)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    if (requiredField) Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return itemValue;
                }

                // Get the slot in the list that contains the object
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the object from variable list
                if (itemType == 2)
                {
                    if (targetHKO.heroList.unityObjects.items.Count <= slotID || slotID < 0)
                    {
                        if (requiredField) Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Hero Object", slotID, 0, heroKitObject));
                        return itemValue;
                    }

                    itemValue = itemValue.Clone(targetHKO.heroList.unityObjects.items[slotID]);
                }

                // Get the object from property list
                if (itemType == 3)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[6] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                    {
                        if (requiredField) Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Hero Object", slotID, 0, heroKitObject));
                        return itemValue;
                    }

                    itemValue = itemValue.Clone(targetHKO.heroProperties[propertyID].itemProperties.unityObjects.items[slotID]);
                }
            }
            // get object from global field
            else if (itemType == 4)
            {
                // Get the slot in the list that contains the value
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().unityObjects.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Unity Object", slotID, 0, heroKitObject));
                    return itemValue;
                }
                itemValue = itemValue.Clone(HeroKitDatabase.GetGlobals().unityObjects.items[slotID]);
            }

            // Return the object
            return itemValue;
        }

        /// <summary>
        /// Get a value from a unity object field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the unity object field.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from a unity object field.</returns>
        public static UnityObjectField GetValueB(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the object type
            int itemType = action.actionFields[actionFieldID].ints[3];
            UnityObjectField itemValue = new UnityObjectField();
            itemValue.sceneID = -1;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Unity Object type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return null;
            }
            // get from variable or property
            else if (itemType == 1 || itemType == 2)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return null;
                }

                // Get the slot in the list that contains the object
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the object from variable list
                if (itemType == 1)
                {
                    if (targetHKO.heroList.unityObjects.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Unity Object", slotID, 0, heroKitObject));
                        return null;
                    }
                    itemValue = itemValue.Clone(targetHKO.heroList.unityObjects.items[slotID]);
                }

                // Get the object from property list
                if (itemType == 2)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[6] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Unity Object", slotID, 0, heroKitObject));
                        return null;
                    }

                    itemValue = itemValue.Clone(targetHKO.heroProperties[propertyID].itemProperties.unityObjects.items[slotID]);
                }
            }
            // get object from globals
            else if (itemType == 3)
            {
                // Get the slot in the list that contains the object
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().unityObjects.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Unity Object", slotID, 0, heroKitObject));
                    return null;
                }
                itemValue = itemValue.Clone(HeroKitDatabase.GetGlobals().unityObjects.items[slotID]);
            }

            // Return the object
            return itemValue;
        }

        /// <summary>
        /// Get a value from a unity object field in a hero object template.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from a unity object field.</returns>
        public static UnityObjectField GetValueC(HeroKitObject heroKitObject, int actionFieldID, HeroObject heroObject)
        {
            // exit early if object does not exist
            if (heroObject == null) return null;

            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the item type
            int itemType = action.actionFields[actionFieldID].ints[3];
            UnityObjectField itemValue = new UnityObjectField();
            itemValue.sceneID = -1;

            // Get the slot in the list that contains the item
            int slotID = action.actionFields[actionFieldID].ints[2] - 1;

            // Get the lists
            UnityObjectList targetList = null;
            string itemTypeName = "";
            string heroName = "";
            if (itemType == 0)
            {
                Debug.LogError("Unity Object type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return null;
            }
            if (itemType == 1)
            {
                heroName = heroObject.name;
                itemTypeName = "Variables";
                targetList = heroObject.lists.unityObjects;
            }
            else if (itemType == 2)
            {
                heroName = heroObject.name;
                itemTypeName = "Properties";
                int propertyID = action.actionFields[actionFieldID].ints[6] - 1;
                if (propertyID < 0) Debug.LogError("Property slot does not exist!");
                targetList = heroObject.propertiesList.properties[propertyID].itemProperties.unityObjects;
            }
            else if (itemType == 3)
            {
                heroName = "n/a";
                itemTypeName = "Globals";
                targetList = HeroKitDatabase.GetGlobals().unityObjects;
            }

            // exit early if the slot in the list does not exist
            if (targetList.items.Count <= slotID || slotID < 0)
            {
                Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, heroName, itemTypeName, "Hero Object", slotID, 0, heroKitObject));
                return null;
            }

            // get the item in the list slot
            itemValue = itemValue.Clone(targetList.items[slotID]);

            // Return the item
            return itemValue;
        }

        /// <summary>
        /// Set the value for a unity object in a unity object field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <param name="newValue">The new value for a unity object field.</param>
        public static void SetValueB(HeroKitObject heroKitObject, int actionFieldID, UnityObjectField newValue)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the object type
            int itemType = action.actionFields[actionFieldID].ints[3];

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Unity Object type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
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

                // Get the slot in the list that contains the object
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the object from the object list
                if (itemType == 1)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        if (targetHKO[i].heroList.unityObjects.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Variables", "Hero Object", slotID, 0, heroKitObject));
                            return;
                        }

                        // Set the object
                        UnityObjectField itemField = targetHKO[i].heroList.unityObjects.items[slotID];
                        targetHKO[i].heroList.unityObjects.items[slotID] = itemField.Clone(newValue);
                    }
                }

                // Get the object from the property list
                if (itemType == 2)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        int propertyID = action.actionFields[actionFieldID].ints[6] - 1;

                        if (targetHKO[i].heroProperties == null || targetHKO[i].heroProperties.Length == 0 ||
                            targetHKO[i].heroProperties.Length <= propertyID || propertyID < 0 ||
                            targetHKO[i].heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Properties", "Unity Object", slotID, 0, heroKitObject));
                            return;
                        }

                        // Set the object
                        UnityObjectField itemField = targetHKO[i].heroProperties[propertyID].itemProperties.unityObjects.items[slotID];
                        targetHKO[i].heroProperties[propertyID].itemProperties.unityObjects.items[slotID] = itemField.Clone(newValue);
                    }
                }
            }
            // set item in global list
            if (itemType == 3)
            {
                // Get the slot in the list that contains the item
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().unityObjects.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Unity Object", slotID, 0, heroKitObject));
                    return;
                }

                UnityObjectField itemField = HeroKitDatabase.GetGlobals().unityObjects.items[slotID];
                HeroKitDatabase.GetGlobals().unityObjects.items[slotID] = itemField.Clone(newValue);
            }
        }
    }
}