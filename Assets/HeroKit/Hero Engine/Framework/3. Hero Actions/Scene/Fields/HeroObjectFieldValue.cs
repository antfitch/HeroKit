// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get or set a value in a hero object field.
    /// </summary>
    public static class HeroObjectFieldValue
    {
        /// <summary>
        /// Get a value from a hero object field.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The value from a hero object field.</returns>
        public static HeroKitObject[] GetValueA(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the data type
            int itemType = action.actionFields[actionFieldID].ints[3];
            HeroKitObject[] itemValue = new HeroKitObject[1];

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Hero Object type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return new HeroKitObject[1];
            }
            // Get item from value
            else if (itemType == 1)
            {
                HeroKitObject[] targetHKO = GetTargetHeroObjects(heroKitObject, actionFieldID);
                itemValue = targetHKO;
            }
            // Get the item from variable or property
            else if (itemType == 2 || itemType == 3)
            {
                // Get the game object
                HeroObjectFieldData goData = new HeroObjectFieldData();
                goData.heroKitObject = heroKitObject;
                goData.heroList = goData.heroKitObject.heroList;
                goData.objectType = action.actionFields[actionFieldID].ints[0];
                goData.objectID = action.actionFields[actionFieldID].ints[1];
                goData.heroGUID = action.actionFields[actionFieldID].ints[4];
                goData.propertyID = action.actionFields[actionFieldID].ints[5];

                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectTargetField.GetValue(goData)[0];
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return new HeroKitObject[1];
                }

                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get item from variable
                if (itemType == 2)
                {
                    if (targetHKO.heroList.heroObjects.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Hero Object", slotID, 0, heroKitObject));
                        return new HeroKitObject[1];
                    }

                    if (targetHKO.heroList.heroObjects.items[slotID].heroKitGameObjects != null)
                        itemValue = targetHKO.heroList.heroObjects.items[slotID].heroKitGameObjects.ToArray();
                }
                // Get item from property
                else if (itemType == 3)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Hero Object", slotID, 0, heroKitObject));
                        return new HeroKitObject[1];
                    }

                    if (targetHKO.heroProperties[propertyID].itemProperties.heroObjects.items[slotID].heroKitGameObjects != null)
                        itemValue = targetHKO.heroProperties[propertyID].itemProperties.heroObjects.items[slotID].heroKitGameObjects.ToArray();
                }

                // If we are working with a hero object in the scene or a property or variable field, make sure it matches the object we want to work with.
                if (heroKitObject.debugHeroObject && goData.objectType != 1 && itemValue != null)
                {
                    // make sure we're working with the right kind of hero object inside the hero kit object
                    HeroObject expectedHeroObject = action.actionFields[actionFieldID].heroObjects[0];
                    HeroObject thisHeroObject = itemValue[0].heroObject;
                    if (expectedHeroObject != null && thisHeroObject != null && (expectedHeroObject != thisHeroObject))
                    {
                        Debug.LogWarning("Wrong hero object for this action. Expected " + expectedHeroObject.name + " but got " + thisHeroObject.name + ". " + HeroKitCommonRuntime.GetHeroDebugInfo(goData.heroKitObject));
                    }
                }
            }
            // get from global field
            else if (itemType == 4)
            {
                // Get the slot in the list that contains the bool
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().heroObjects.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Hero Object", slotID, 0, heroKitObject));
                    return new HeroKitObject[1];
                }

                if (HeroKitDatabase.GetGlobals().heroObjects.items[slotID].heroKitGameObjects != null)
                    itemValue = HeroKitDatabase.GetGlobals().heroObjects.items[slotID].heroKitGameObjects.ToArray();
            }

            if (itemValue == null)
            {
                Debug.LogError("No hero kit object was found for " + action.actionTemplate.name + ". " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                itemValue = new HeroKitObject[1];
            }

            return itemValue;
        }

        /// <summary>
        /// Get a list of hero kit objects that is attached to a slot in the hero object list.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The list of hero kit objects.</returns>
        public static List<HeroKitObject> GetValueB(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the data type
            int itemType = action.actionFields[actionFieldID].ints[3];

            // Set up the list
            List<HeroKitObject> itemValue = new List<HeroKitObject>();

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Hero Object type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return null;
            }
            // item is variable or property
            else if (itemType == 1 || itemType == 2)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return null;
                }

                // Get the slot in the list that contains the integer
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the item from the variable list
                if (itemType == 1)
                {
                    if (targetHKO.heroList.heroObjects.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "Hero Object", slotID, 0, heroKitObject));
                        return null;
                    }

                    itemValue = targetHKO.heroList.heroObjects.items[slotID].heroKitGameObjects;
                }

                // Get the item from the property list
                if (itemType == 2)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "Hero Object", slotID, 0, heroKitObject));
                        return null;
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.heroObjects.items[slotID].heroKitGameObjects;
                }
            }
            // get object from globals
            else if (itemType == 3)
            {
                // Get the slot in the list that contains the object
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().heroObjects.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Hero Object", slotID, 0, heroKitObject));
                    return null;
                }
                itemValue = HeroKitDatabase.GetGlobals().heroObjects.items[slotID].heroKitGameObjects;
            }

            return itemValue;
        }

        /// <summary>
        /// Get the template assigned to a hero object field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The template assigned to a hero object field.</returns>
        public static HeroObject GetValueC(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // set up the target hero object type
            HeroObject targetHeroObject = null;

            // Get the game object data
            HeroObjectFieldData goData = new HeroObjectFieldData();
            goData.heroKitObject = heroKitObject;
            goData.heroList = goData.heroKitObject.heroList;
            goData.objectType = action.actionFields[actionFieldID].ints[0];
            goData.objectID = action.actionFields[actionFieldID].ints[1];
            goData.heroGUID = action.actionFields[actionFieldID].ints[4];
            goData.propertyID = action.actionFields[actionFieldID].ints[5];

            // get hero object from a file
            if (goData.objectType == 1)
            {
                targetHeroObject = action.actionFields[actionFieldID].heroObjects[0];
            }
            // get hero object from elsewhere
            else
            {
                targetHeroObject = HeroObjectTargetField.GetTemplate(goData); 
            }

            return targetHeroObject;
        }

        /// <summary>
        /// Get the GUID of the first hero kit object in a hero object field.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The GUID of the first hero kit object in a hero object field.</returns>
        public static int GetValueD(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the game object
            HeroObjectFieldData goData = new HeroObjectFieldData();
            goData.heroGUID = action.actionFields[actionFieldID].ints[4];

            return goData.heroGUID;
        }

        /// <summary>
        /// Get a list of hero kit objects in a hero object field.
        /// By default this hero kit object is used. 
        /// </summary>
        /// <remarks>By default, this method always returns an array with one value. The values in the array may be null, but the array itself will never be null.</remarks>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <param name="canReturnNull">A null array can be returned.</param>
        /// <returns>A list of hero kit objects in a hero object field.</returns>
        public static HeroKitObject[] GetValueE(HeroKitObject heroKitObject, int actionFieldIDA, int actionFieldIDB, bool canReturnNull=true)
        {
            bool useAnotherObject = BoolValue.GetValue(heroKitObject, actionFieldIDA);

            HeroKitObject[] targetObject = new HeroKitObject[1];
            if (useAnotherObject)
            {
                targetObject = GetValueA(heroKitObject, actionFieldIDB);
            }
            else
            {
                targetObject[0] = heroKitObject;
            }

            if (canReturnNull)
            {
                if (targetObject.Length == 1 && targetObject[0] == null)
                    targetObject = null;
            }

            return targetObject;
        }

        /// <summary>
        /// Get a list of hero kit objects in the scene.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>A list of hero kit objects in the scene.</returns>
        public static HeroKitObject[] GetTargetHeroObjects(HeroKitObject heroKitObject, int actionFieldID)
        {
            return GetTargetHeroObject(heroKitObject, heroKitObject.heroStateData.eventBlock, heroKitObject.heroStateData.action, actionFieldID);
        }

        /// <summary>
        /// Get one hero kit object in the scene.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>A hero kit object in the scene.</returns>
        public static HeroKitObject GetTargetHeroObject(HeroKitObject heroKitObject, int actionFieldID)
        {
            HeroKitObject targetHKO = null;

            // get the target hero kit objects
            HeroKitObject[] targets = GetTargetHeroObject(heroKitObject, heroKitObject.heroStateData.eventBlock, heroKitObject.heroStateData.action, actionFieldID);

            // if at least one target exists, get the first one
            if (targets != null)
                targetHKO = targets[0];

            return targetHKO;
        }

        /// <summary>
        /// Get a hero kit object in the scene with specific event ID and action ID.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>A hero kit object in the scene.</returns>
        public static HeroKitObject[] GetTargetHeroObject(HeroKitObject heroKitObject, int eventID, int actionID, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[eventID].actions[actionID];

            // Get the game object
            HeroObjectFieldData goData = new HeroObjectFieldData();
            goData.heroKitObject = heroKitObject;
            goData.heroList = goData.heroKitObject.heroList;
            goData.objectType = action.actionFields[actionFieldID].ints[0];
            goData.objectID = action.actionFields[actionFieldID].ints[1];
            goData.heroGUID = action.actionFields[actionFieldID].ints[4];
            goData.propertyID = action.actionFields[actionFieldID].ints[5];

            // Get the hero kit object
            HeroKitObject[] targetHKO = HeroObjectTargetField.GetValue(goData);
            if (targetHKO == null)
            {
                Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, null, goData.heroKitObject));
                return null;
            }

            // If we are working with a hero object in the scene or a property or variable field, make sure it matches the object we want to work with.
            if (heroKitObject.debugHeroObject && goData.objectType != 1)
            {
                // make sure we're working with the right kind of hero object inside the hero kit object
                HeroObject expectedHeroObject = action.actionFields[actionFieldID].heroObjects[0];
                HeroObject thisHeroObject = (targetHKO.Length == 0 || targetHKO[0] == null) ? null : targetHKO[0].heroObject;
                if (expectedHeroObject != null && thisHeroObject != null && (expectedHeroObject != thisHeroObject))
                {
                    Debug.LogWarning("Wrong hero object for this action. Expected " + expectedHeroObject.name + " but got " + thisHeroObject.name + ". " + HeroKitCommonRuntime.GetHeroDebugInfo(goData.heroKitObject));
                }
            }

            return targetHKO;
        }

        /// <summary>
        /// Get a hero kit object from a conditional bool field on an event.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>A hero kit object from a conditional bool field on an event.</returns>
        public static HeroKitObject GetValueEvent(HeroKitObject heroKitObject, ConditionBoolField itemField)
        {
            // Get the game object
            HeroObjectFieldData goData = new HeroObjectFieldData();
            goData.heroKitObject = heroKitObject;
            goData.heroList = goData.heroKitObject.heroList;
            goData.objectType = itemField.objectType;
            goData.heroGUID = itemField.heroGUID;
            goData.propertyID = itemField.propertyID;

            // Get the hero kit object
            HeroKitObject targetHKO = HeroObjectTargetField.GetValue(goData)[0];
            if (targetHKO == null)
            {
                Debug.LogError("No hero kit object was found for this event." + HeroKitCommonRuntime.GetHeroDebugInfo(goData.heroKitObject));
                return null;
            }

            // If we are working with a hero object in the scene or a property or variable field, make sure it matches the object we want to work with.
            if (heroKitObject.debugHeroObject && goData.objectType != 1)
            {
                // make sure we're working with the right kind of hero object inside the hero kit object
                HeroObject expectedHeroObject = itemField.heroObject;
                HeroObject thisHeroObject = targetHKO.heroObject;
                if (expectedHeroObject != null && (expectedHeroObject != thisHeroObject))
                {
                    Debug.LogWarning("Wrong hero object for this event. Expected " + expectedHeroObject.name + " but got " + thisHeroObject.name + ". " + HeroKitCommonRuntime.GetHeroDebugInfo(goData.heroKitObject));
                }
            }

            return targetHKO;
        }

        /// <summary>
        /// Get a hero kit object from a conditional int field on an event.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>A hero kit object from a conditional int field on an event.</returns>
        public static HeroKitObject GetValueEvent(HeroKitObject heroKitObject, ConditionIntField itemField)
        {
            // Get the game object
            HeroObjectFieldData goData = new HeroObjectFieldData();
            goData.heroKitObject = heroKitObject;
            goData.heroList = goData.heroKitObject.heroList;
            goData.objectType = itemField.objectType;
            goData.objectID = itemField.objectID;
            goData.heroGUID = itemField.heroGUID;
            goData.propertyID = itemField.propertyID;

            // Get the hero kit object
            HeroKitObject targetHKO = HeroObjectTargetField.GetValue(goData)[0];
            if (targetHKO == null)
            {
                Debug.LogError("No hero kit object was found for this event." + HeroKitCommonRuntime.GetHeroDebugInfo(goData.heroKitObject));
                return null;
            }

            // If we are working with a hero object in the scene or a property or variable field, make sure it matches the object we want to work with.
            if (heroKitObject.debugHeroObject && goData.objectType != 1)
            {
                // make sure we're working with the right kind of hero object inside the hero kit object
                HeroObject expectedHeroObject = itemField.heroObject;
                HeroObject thisHeroObject = targetHKO.heroObject;
                if (expectedHeroObject != null && (expectedHeroObject != thisHeroObject))
                {
                    Debug.LogWarning("Wrong hero object for this event. Expected " + expectedHeroObject.name + " but got " + thisHeroObject.name + ". " + HeroKitCommonRuntime.GetHeroDebugInfo(goData.heroKitObject));
                }
            }

            return targetHKO;
        }

        /// <summary>
        /// Set the value for a hero kit object list in a hero object field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the bool field.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <param name="newValue">The new value for the hero kit object list in the hero object field.</param>
        public static void SetValueB(HeroKitObject heroKitObject, int actionFieldID, List<HeroKitObject> newValue)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the data type (WARNNG was 0, but changed to 3, not sure if this is always correct. It does work for GetHeroObjectByName.cs)
            int itemType = action.actionFields[actionFieldID].ints[3];

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Hero Object type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return;
            }
            // set item in variable or property list
            else if (itemType == 1 || itemType == 2)
            {
                // Get the hero kit object
                HeroKitObject[] targetHKO = GetTargetHeroObjects(heroKitObject, actionFieldID);
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
                        if (targetHKO[i].heroList.heroObjects.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Variables", "Hero Object", slotID, 0, heroKitObject));
                            return;
                        }

                        // Set the integer
                        targetHKO[i].heroList.heroObjects.items[slotID].heroKitGameObjects = newValue;
                    }
                }

                // Get the integer from the property list
                if (itemType == 2)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        if (targetHKO[i].heroProperties == null || targetHKO[i].heroProperties.Length == 0 ||
                            targetHKO[i].heroProperties.Length <= propertyID || propertyID < 0 ||
                            targetHKO[i].heroProperties[propertyID].itemProperties.bools.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Properties", "Hero Object", slotID, 0, heroKitObject));
                            return;
                        }

                        // Set the integer
                        targetHKO[i].heroProperties[propertyID].itemProperties.heroObjects.items[slotID].heroKitGameObjects = newValue;
                    }
                }
            }
            // set item in global list
            if (itemType == 3)
            {
                // Get the slot in the list that contains the item
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().heroObjects.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Hero Object", slotID, 0, heroKitObject));
                    return;
                }
                HeroKitDatabase.GetGlobals().heroObjects.items[slotID].heroKitGameObjects = newValue;
            }
        }

        /// <summary>
        /// Set the value for a hero object template in a hero object field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the bool field.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <param name="newValue">The new value for the hero object template in the hero object field.</param>
        public static void SetValueC(HeroKitObject heroKitObject, int actionFieldID, HeroObject newValue)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the data type (WARNNG was 0, but changed to 3, not sure if this is always correct. It does work for GetHeroObjectByName.cs)
            int itemType = action.actionFields[actionFieldID].ints[3];

            // don't get item. Item type was never specified
            if (itemType == 0)
            {
                Debug.LogError("Hero Object type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return;
            }
            // item is variable or property
            else if (itemType == 1 || itemType == 2)
            {
                // Get the hero kit object
                HeroKitObject[] targetHKO = GetTargetHeroObjects(heroKitObject, actionFieldID);
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
                        if (targetHKO[i].heroList.heroObjects.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Variables", "Hero Object", slotID, 0, heroKitObject));
                            return;
                        }

                        // Set the integer
                        targetHKO[i].heroList.heroObjects.items[slotID].value = newValue;
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
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Properties", "Hero Object", slotID, 0, heroKitObject));
                            return;
                        }

                        // Set the integer
                        targetHKO[i].heroProperties[propertyID].itemProperties.heroObjects.items[slotID].value = newValue;
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
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "Hero Object", slotID, 0, heroKitObject));
                    return;
                }
                HeroKitDatabase.GetGlobals().heroObjects.items[slotID].value = newValue;
            }
        }
    }
}