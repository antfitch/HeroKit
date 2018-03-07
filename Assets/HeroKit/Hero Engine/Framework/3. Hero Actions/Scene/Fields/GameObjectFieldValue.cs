// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get or set a value in a game object field.
    /// </summary>
    public static class GameObjectFieldValue
    {
        /// <summary>
        /// Get a value from a game object field.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The value from a game object field.</returns>
        public static GameObject GetValueA(HeroKitObject heroKitObject, int actionFieldID, bool includeInactive = false)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the data type
            int itemType = action.actionFields[actionFieldID].ints[3];
            GameObject itemValue = null;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Game Object type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return null;
            }
            // get value from Value
            else if (itemType == 1)
            {
                string name = action.actionFields[actionFieldID].strings[0];

                // get gameobjects
                // WARNING: If you use Resources.Find... this will return hidden scene objects, including prefabs that you don't want
                // to work with. Only use this option if you absolutely must. If you have any prefabs with the same name as objects
                // in your scene, the prefabs might be referenced instead of the object you want
                Transform[] transforms = (includeInactive) ? Resources.FindObjectsOfTypeAll<Transform>() : Object.FindObjectsOfType<Transform>();
                for (int i = 0; i < transforms.Length; i++)
                {
                    if (transforms[i].gameObject.name == name)
                    {
                        itemValue = transforms[i].gameObject;
                        break;
                    }
                }
            }
            // Get the item from the value list (2=variables, 3=properties, 4=this)
            else if (itemType == 2 || itemType == 3 || itemType == 4)
            {
                HeroKitObject targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return null;
                }

                // Get the slot in the list that contains the game object
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the item from the variable list
                if (itemType == 2)
                {
                    if (targetHKO.heroList.gameObjects.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Game Object", slotID, 0, heroKitObject));
                        return null;
                    }

                    itemValue = targetHKO.heroList.gameObjects.items[slotID].value;
                }

                // Get the item from the property list
                if (itemType == 3)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Game Object", slotID, 0, heroKitObject));
                        return null;
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.gameObjects.items[slotID].value;
                }

                // Get the item from target hero kit object
                if (itemType == 4)
                {
                    itemValue = targetHKO.gameObject;
                }
            }
            // get item from global field
            else if (itemType == 5)
            {
                // Get the slot in the list that contains the item
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().gameObjects.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Game Objects", slotID, 0, heroKitObject));
                    return null;
                }
                itemValue = HeroKitDatabase.GetGlobals().gameObjects.items[slotID].value;
            }

            return itemValue;
        }

        /// <summary>
        /// Get a value from a game object field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from a game object field.</returns>
        public static GameObject GetValueB(HeroKitObject heroKitObject, int actionFieldID, bool includeInactive = false)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the data type
            int itemType = action.actionFields[actionFieldID].ints[3];
            GameObject itemValue = null;

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Game Object type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return null;
            }
            // get item from variable or property list
            else if (itemType == 1 || itemType == 2)
            {
                HeroKitObject targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return null;
                }

                // Get the slot in the list that contains the game object
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the item from the variable list
                if (itemType == 1)
                {
                    if (targetHKO.heroList.gameObjects.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Game Object", slotID, 0, heroKitObject));
                        return null;
                    }

                    itemValue = targetHKO.heroList.gameObjects.items[slotID].value;
                }

                // Get the item from the property list
                if (itemType == 2)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Game Object", slotID, 0, heroKitObject));
                        return null;
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.gameObjects.items[slotID].value;
                }
            }
            // get item from globals
            else if (itemType == 3)
            {
                // Get the slot in the list that contains the item
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().gameObjects.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Game Object", slotID, 0, heroKitObject));
                    return null;
                }
                itemValue = HeroKitDatabase.GetGlobals().gameObjects.items[slotID].value;
            }

            return itemValue;
        }

        /// <summary>
        /// Set the value for a game object in a game object field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <param name="newValue">The new value for a game object field.</param>
        public static void SetValueB(HeroKitObject heroKitObject, int actionFieldID, GameObject newValue)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the data type
            int itemType = action.actionFields[actionFieldID].ints[0];

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Game Object type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
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

                // Get the slot in the list that contains the integer
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the integer from the integer list
                if (itemType == 1)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        if (targetHKO[i].heroList.gameObjects.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Variables", "Game Object", slotID, 0, heroKitObject));
                            return;
                        }

                        // Set the integer
                        targetHKO[i].heroList.gameObjects.items[slotID].value = newValue;
                    }
                }

                // Get the integer from the property list
                if (itemType == 2)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                        if (targetHKO[i].heroProperties == null || targetHKO[i].heroProperties.Length == 0 ||
                            targetHKO[i].heroProperties.Length <= propertyID || propertyID < 0 ||
                            targetHKO[i].heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Properties", "Game Object", slotID, 0, heroKitObject));
                            return;
                        }

                        // Set the integer
                        targetHKO[i].heroProperties[propertyID].itemProperties.gameObjects.items[slotID].value = newValue;
                    }
                }
            }
            // set item in global list
            if (itemType == 3)
            {
                // Get the slot in the list that contains the bool
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().gameObjects.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Game Object", slotID, 0, heroKitObject));
                    return;
                }
                HeroKitDatabase.GetGlobals().gameObjects.items[slotID].value = newValue;
            }


        }
    }
}