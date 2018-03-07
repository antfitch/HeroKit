// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections.Generic;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get or set a value in a string field.
    /// </summary>
    public static class StringFieldValue
    {
        /// <summary>
        /// Get a value from a string field.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <param name="convertVariablesToText">Convert variables to text?</param>
        /// <returns>The value from a string field.</returns>
        public static string GetValueA(HeroKitObject heroKitObject, int actionFieldID, bool convertVariablesToText = false)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the data type
            int itemType = action.actionFields[actionFieldID].ints[3];
            string itemValue = "";
            HeroKitObject targetHKO = null;

            if (itemType == 0)
            {
                Debug.LogError("String type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return "";
            }
            // get string from field
            else if (itemType == 1)
            {
                itemValue = action.actionFields[actionFieldID].strings[1];
                targetHKO = heroKitObject;
            }
            // get string from variable field or property field
            else if (itemType == 2 || itemType == 3)
            {
                // Get the hero kit object
                targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return "";
                }

                // Get the slot in the list that contains the string
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the string from variable list
                if (itemType == 2)
                {
                    if (targetHKO.heroList.strings.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "String", slotID, 0, heroKitObject));
                        return "";
                    }
                    itemValue = targetHKO.heroList.strings.items[slotID].value;
                }

                // Get the string from property list
                if (itemType == 3)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.strings.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "String", slotID, 0, heroKitObject));
                        return "";
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.strings.items[slotID].value;
                }

            }
            // get string from global field
            else if (itemType == 4)
            {
                // Get the slot in the list that contains the string
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().strings.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "String", slotID, 0, heroKitObject));
                    return "";
                }
                itemValue = HeroKitDatabase.GetGlobals().strings.items[slotID].value;
            }

            // localize the text
            itemValue = HeroKitDatabase.GetLocalization(itemValue);

            // convert variables into text
            if (convertVariablesToText)
            {
                bool useVariables = (itemType > 1) ? UseVariables(heroKitObject, actionFieldID) : true;
                if (useVariables) itemValue = InsertVariablesInString(targetHKO, itemValue);
            }

            // Return the string
            return itemValue;
        }

        /// <summary>
        /// Get a value from a string field.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <param name="convertVariablesToText">Convert variables to text?</param>
        /// <returns>The value from a string field.</returns>
        public static string GetValueB(HeroKitObject heroKitObject, int actionFieldID, bool convertVariablesToText = false)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the data type
            int itemType = action.actionFields[actionFieldID].ints[3];
            string itemValue = "";
            HeroKitObject targetHKO = null;

            // item type does not exist
            if (itemType == 0)
            {
                Debug.LogError("String type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return "";
            }
            // get string from variable or property list
            else if (itemType == 1 || itemType == 2)
            {
                // Get the hero kit object
                targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return "";
                }

                // Get the slot in the list that contains the string
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // Get the string from string list
                if (itemType == 1)
                {
                    if (targetHKO.heroList.strings.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "String", slotID, 0, heroKitObject));
                        return "";
                    }
                    itemValue = targetHKO.heroList.strings.items[slotID].value;
                }

                // Get the string from property list
                if (itemType == 2)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.strings.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "String", slotID, 0, heroKitObject));
                        return "";
                    }

                    itemValue = targetHKO.heroProperties[propertyID].itemProperties.strings.items[slotID].value;
                }
            }
            // get string from globals
            else if (itemType == 3)
            {
                targetHKO = heroKitObject;

                // Get the slot in the list that contains the string
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().strings.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "String", slotID, 0, heroKitObject));
                    return "";
                }
                itemValue = HeroKitDatabase.GetGlobals().strings.items[slotID].value;
            }

            // localize the text
            itemValue = HeroKitDatabase.GetLocalization(itemValue);

            // convert variables to text
            if (convertVariablesToText)
            {
                bool useVariables = UseVariables(heroKitObject, actionFieldID);
                if (useVariables) itemValue = InsertVariablesInString(targetHKO, itemValue);
            }

            // Return the integer
            return itemValue;
        }

        /// <summary>
        /// Get a value from a string field in a hero object template.
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>The value from a string field.</returns>
        public static string GetValueC(HeroKitObject heroKitObject, int actionFieldID, HeroObject heroObject)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // exit early if object does not exist
            if (heroObject == null)
            {
                Debug.LogError(HeroKitCommonRuntime.NoHeroObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                return "";
            }

            // Get the item type
            int itemType = action.actionFields[actionFieldID].ints[3];
            string itemValue = "";

            // Get the slot in the list that contains the item
            int slotID = action.actionFields[actionFieldID].ints[2] - 1;

            // Get the lists
            StringList targetList = null;
            string itemTypeName = "";
            string heroName = "";
            if (itemType == 0)
            {
                Debug.LogError("String type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return "";
            }
            else if (itemType == 1)
            {
                heroName = heroObject.name;
                itemTypeName = "Variables";
                targetList = heroObject.lists.strings;
            }
            else if (itemType == 2)
            {
                heroName = heroObject.name;
                itemTypeName = "Properties";
                int propertyID = action.actionFields[actionFieldID].ints[5] - 1;
                if (propertyID < 0) Debug.LogError("Property slot does not exist!");
                targetList = heroObject.propertiesList.properties[propertyID].itemProperties.strings;
            }
            else if (itemType == 3)
            {
                heroName = "n/a";
                itemTypeName = "Globals";
                targetList = HeroKitDatabase.GetGlobals().strings;
            }

            // exit early if the slot in the list does not exist
            if (targetList.items.Count <= slotID || slotID < 0)
            {
                Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, heroName, itemTypeName, "String", slotID, 0, heroKitObject));
                return "";
            }

            // get the item in the list slot
            itemValue = targetList.items[slotID].value;

            // Return the item
            return itemValue;
        }

        /// <summary>
        /// Set the value for a string in a string field. 
        /// This is for a field that contains Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <param name="newValue">The new value for a string field.</param>
        /// <param name="convertVariablesToText">Convert variables to text?</param>
        public static void SetValueB(HeroKitObject heroKitObject, int actionFieldID, string newValue, bool convertVariablesToText = false)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the string type
            int itemType = action.actionFields[actionFieldID].ints[3];

            // convert variables to text
            if (convertVariablesToText)
            {
                bool useVariables = UseVariables(heroKitObject, actionFieldID);
                if (useVariables) newValue = InsertVariablesInString(heroKitObject, newValue);
            }

            // No type specified
            if (itemType == 0)
            {
                Debug.LogError("String type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
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

                // Get the string from the string list
                if (itemType == 1)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        if (targetHKO[i].heroList.strings.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Variables", "String", slotID, 0, heroKitObject));
                            return;
                        }

                        // Set the string
                        targetHKO[i].heroList.strings.items[slotID].value = newValue;
                    }
                }

                // Get the string from the property list
                if (itemType == 2)
                {
                    for (int i = 0; i < targetHKO.Length; i++)
                    {
                        int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                        if (targetHKO[i].heroProperties == null || targetHKO[i].heroProperties.Length == 0 ||
                            targetHKO[i].heroProperties.Length <= propertyID || propertyID < 0 ||
                            targetHKO[i].heroProperties[propertyID].itemProperties.strings.items.Count <= slotID || slotID < 0)
                        {
                            Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO[i].heroObject.name, "Properties", "String", slotID, 0, heroKitObject));
                            return;
                        }

                        targetHKO[i].heroProperties[propertyID].itemProperties.strings.items[slotID].value = newValue;

                    }
                }
            }
            // set item in global list
            if (itemType == 3)
            {
                // Get the slot in the list that contains the bool
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                if (HeroKitDatabase.GetGlobals().strings.items.Count <= slotID || slotID < 0)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, "n/a", "Globals", "String", slotID, 0, heroKitObject));
                    return;
                }
                HeroKitDatabase.GetGlobals().strings.items[slotID].value = newValue;
            }
        }

        /// <summary>
        /// Checks to see if a string list field has the Use Variables flag enabled.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to the action field.</param>
        /// <returns>Is the Use Variables flag enabled in a string list field.</returns>
        private static bool UseVariables(HeroKitObject heroKitObject, int actionFieldID)
        {
            bool useVariables = false;

            // Get the data type
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];
            int itemType = action.actionFields[actionFieldID].ints[3];

            if (itemType == 0)
            {
                Debug.LogError("String type was never specified for " + action.actionTemplate.name + " " + HeroKitCommonRuntime.GetHeroDebugInfo(heroKitObject));
                return false;
            }

            if (itemType == 2 || itemType == 3)
            {
                // Get the hero kit object
                HeroKitObject targetHKO = HeroObjectFieldValue.GetTargetHeroObject(heroKitObject, actionFieldID);
                if (targetHKO == null)
                {
                    Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                    return false;
                }

                // Get the slot in the list that contains the string
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;

                // check strings for variables if data type is variable
                if (itemType == 2)
                {
                    if (targetHKO.heroList.strings.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Variables", "String", slotID, 0, heroKitObject));
                        return false;
                    }

                    useVariables = targetHKO.heroList.strings.items[slotID].useVariables;
                }
                // check strings for variables if data type is property
                if (itemType == 3)
                {
                    int propertyID = action.actionFields[actionFieldID].ints[5] - 1;

                    if (targetHKO.heroProperties == null || targetHKO.heroProperties.Length == 0 ||
                        targetHKO.heroProperties.Length <= propertyID || propertyID < 0 ||
                        targetHKO.heroProperties[propertyID].itemProperties.strings.items.Count <= slotID || slotID < 0)
                    {
                        Debug.LogError(HeroKitCommonRuntime.NoVariableDebugInfo(action.actionTemplate.name, targetHKO.heroObject.name, "Properties", "String", slotID, 0, heroKitObject));
                        return false;
                    }

                    useVariables = targetHKO.heroProperties[propertyID].itemProperties.strings.items[slotID].useVariables;
                }
            }
            else if (itemType == 4)
            {
                // Get the slot in the list that contains the string
                int slotID = action.actionFields[actionFieldID].ints[2] - 1;
                // Check if there are variables to parse
                useVariables = HeroKitDatabase.GetGlobals().strings.items[slotID].useVariables;
            }

            return useVariables;
        }

        /// <summary>
        /// Replace variables with values in a string.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="text">ID assigned to the action field.</param>
        /// <returns>The parsed string.</returns>
        private static string InsertVariablesInString(HeroKitObject heroKitObject, string text)
        {
            // the string we are going to manipulate
            string newText = text;
            int startPos = 0;
            int loopCount = 0;

            // loop through text until all codes are replaced
            while (startPos < text.Length)
            {
                // search for codes in the string
                int startIndex = text.IndexOf("[", startPos);
                int endIndex = (startIndex != -1) ? text.IndexOf("]", startIndex) : -1;
                int width = endIndex - startIndex + 1; // # of characters in the substring

                // break out of loop. there are no more ]s in this string 
                if (endIndex <= startIndex)
                {
                    startPos = text.Length;
                    continue;
                }

                // skip past this code. it's not long enough to contain anything. go to next loop
                if (width < 7)
                {
                    startPos = endIndex + 1;
                    continue;
                }

                // get the substring inside the string. ex: [V-I-0]
                string subString = text.Substring(startIndex, width);
                string slotValue = "";

                // --------------------------------------------------------------
                // Variable
                // --------------------------------------------------------------

                // if substring contains a variable that is an integer, get the integer. ex: V-I-
                if (subString.IndexOf("V-I-") != -1)
                    slotValue = GetValueForCode(heroKitObject, subString, CodeType.IntegerVariable);

                // if substring contains a variable that is a bool, get the bool. ex: V-B-
                else if (subString.IndexOf("V-B-") != -1)
                    slotValue = GetValueForCode(heroKitObject, subString, CodeType.BoolVariable);

                // if substring contains a variable that is a string, get the string. ex: V-S-
                else if (subString.IndexOf("V-S-") != -1)
                    slotValue = GetValueForCode(heroKitObject, subString, CodeType.StringVariable);

                // --------------------------------------------------------------
                // Global
                // --------------------------------------------------------------

                // if substring contains a global that is an integer, get the integer. ex: G-I-
                else if (subString.IndexOf("G-I-") != -1)
                    slotValue = GetValueForCode(heroKitObject, subString, CodeType.IntegerGlobal);

                // if substring contains a global that is a bool, get the bool. ex: G-B-
                else if (subString.IndexOf("G-B-") != -1)
                    slotValue = GetValueForCode(heroKitObject, subString, CodeType.BoolGlobal);

                // if substring contains a global that is a string, get the string. ex: G-S-
                else if (subString.IndexOf("G-S-") != -1)
                    slotValue = GetValueForCode(heroKitObject, subString, CodeType.StringGlobal);

                // --------------------------------------------------------------
                // Properties
                // --------------------------------------------------------------

                // if substring contains a property, get the property. ex: P-
                else if (subString.IndexOf("P-") != -1)
                {
                    // if substring contains a property that is an integer, get the integer. ex: P-#-I-
                    if (subString.IndexOf("I-") != -1)
                        slotValue = GetValueForCode(heroKitObject, subString, CodeType.IntegerProperty, true);

                    // if substring contains a property that is a bool, get the bool. ex: P-#-B-
                    else if (subString.IndexOf("B-") != -1)
                        slotValue = GetValueForCode(heroKitObject, subString, CodeType.BoolProperty, true);

                    // if substring contains a variable that is a string, get the string. ex: P-#-S-
                    else if (subString.IndexOf("S-") != -1)
                        slotValue = GetValueForCode(heroKitObject, subString, CodeType.StringProperty, true);
                }

                // --------------------------------------------------------------

                // replace the substring with the value from the variable or property field
                newText = newText.Replace(subString, slotValue);

                // jump to the next position in the string
                startPos = endIndex + 1;

                // watch for infinite loop
                loopCount++;
                if (loopCount > 100)
                {
                    if (heroKitObject.debugHeroObject) Debug.Log("Possible infinite loop! Exiting early.");
                }            
            }

            return newText;
        }

        /// <summary>
        /// The types of variables we can parse in a string.
        /// </summary>
        private enum CodeType {IntegerVariable, BoolVariable, StringVariable, IntegerProperty, BoolProperty, StringProperty, IntegerGlobal, BoolGlobal, StringGlobal};

        /// <summary>
        /// Convert a variable into a string.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="subString">The code for the variable (ex. [V-I-0]).</param>
        /// <param name="codeType">The type of variable to parse.</param>
        /// <returns>Parsed string.</returns>
        private static string GetValueForCode(HeroKitObject heroKitObject, string subString, CodeType codeType, bool isProperty=false)
        {
            string code = "";
            int propertyID = 0;

            // convert the code into an integer.  ex: [V-I-0] = 99
            string slotValue = "";
            int slotID = 0;

            if (!isProperty)
            {
                // get the code. ex: 0
                int codeStartIndex = 5;
                int codeWidth = subString.Length - codeStartIndex - 1;
                code = subString.Substring(codeStartIndex, codeWidth);
            }

            // [P-2-I-5]
            if (isProperty)
            {      
                // [p-
                int propertyStartIndex = 3;

                // 2-I (i = -)
                int propertyIdLength = subString.IndexOf("-", propertyStartIndex) - propertyStartIndex;

                // 2
                string pID = subString.Substring(propertyStartIndex, propertyIdLength);
                bool parsed = Int32.TryParse(pID, out propertyID);
                if (!parsed) return "???";

                // get last value in sequence (5)
                int codeStartIndex = propertyStartIndex + propertyIdLength + 3;
                int codeWidth = subString.Length - codeStartIndex - 1;
                code = subString.Substring(codeStartIndex, codeWidth);
            }
            
            // --------------------------------------------------------------
            // Variables
            // --------------------------------------------------------------

            if (Int32.TryParse(code, out slotID))
            {
                if (codeType == CodeType.IntegerVariable)
                {
                    // get the value from the variable list
                    if (heroKitObject.heroList.ints.items != null &&
                        heroKitObject.heroList.ints.items.Count != 0 &&
                        heroKitObject.heroList.ints.items.Count > slotID)
                    {
                        slotValue = heroKitObject.heroList.ints.items[slotID].value.ToString();
                    }
                    else
                        if (heroKitObject.debugHeroObject) Debug.LogError("Variable Integer Slot does not exist!");
                }

                else if (codeType == CodeType.BoolVariable)
                {
                    // get the value from the variable list
                    if (heroKitObject.heroList.bools.items != null &&
                        heroKitObject.heroList.bools.items.Count != 0 &&
                        heroKitObject.heroList.bools.items.Count > slotID)
                    {
                        slotValue = heroKitObject.heroList.bools.items[slotID].value.ToString();
                    }
                    else
                        if (heroKitObject.debugHeroObject) Debug.LogError("Variable Bool Slot does not exist!");
                }

                else if (codeType == CodeType.StringVariable)
                {
                    // get the value from the variable list
                    if (heroKitObject.heroList.strings.items != null &&
                        heroKitObject.heroList.strings.items.Count != 0 &&
                        heroKitObject.heroList.strings.items.Count > slotID)
                    {
                        slotValue = heroKitObject.heroList.strings.items[slotID].value.ToString();
                    }
                    else
                        if (heroKitObject.debugHeroObject) Debug.LogError("Variable String Slot does not exist!");
                }

                // --------------------------------------------------------------
                // Properties
                // --------------------------------------------------------------

                if (codeType == CodeType.IntegerProperty)
                {
                    // get the hero property attached to hero object
                    HeroProperties heroProperties = (heroKitObject.heroProperties.Length > propertyID) ? heroKitObject.heroProperties[propertyID] : null;

                    // get the value from the variable list
                    if (heroProperties != null &&
                        heroProperties.itemProperties.ints.items != null &&
                        heroProperties.itemProperties.ints.items.Count != 0 &&
                        heroProperties.itemProperties.ints.items.Count > slotID)
                    {
                        slotValue = heroProperties.itemProperties.ints.items[slotID].value.ToString();
                    }
                    else
                        if (heroKitObject.debugHeroObject) Debug.LogError("Property Integer Slot does not exist!");
                }

                else if (codeType == CodeType.BoolProperty)
                {
                    // get the hero property attached to hero object
                    HeroProperties heroProperties = heroKitObject.heroProperties[propertyID];

                    // get the value from the variable list
                    if (heroProperties != null &&
                        heroProperties.itemProperties.bools.items != null &&
                        heroProperties.itemProperties.bools.items.Count != 0 &&
                        heroProperties.itemProperties.bools.items.Count > slotID)
                    {
                        slotValue = heroProperties.itemProperties.bools.items[slotID].value.ToString();
                    }
                    else
                        if (heroKitObject.debugHeroObject) Debug.LogError("Property Bool Slot does not exist!");
                }

                else if (codeType == CodeType.StringProperty)
                {
                    // get the hero property attached to hero object
                    HeroProperties heroProperties = heroKitObject.heroProperties[propertyID];

                    // get the value from the variable list
                    if (heroProperties != null &&
                        heroProperties.itemProperties.strings.items != null &&
                        heroProperties.itemProperties.strings.items.Count != 0 &&
                        heroProperties.itemProperties.strings.items.Count > slotID)
                    {
                        slotValue = heroProperties.itemProperties.strings.items[slotID].value.ToString();
                    }
                    else
                        if (heroKitObject.debugHeroObject) Debug.LogError("Property String Slot does not exist!");
                }

                // --------------------------------------------------------------
                // Globals
                // --------------------------------------------------------------

                if (codeType == CodeType.IntegerGlobal)
                {
                    // get the value from the variable list
                    if (HeroKitDatabase.GetGlobals().ints.items != null &&
                        HeroKitDatabase.GetGlobals().ints.items.Count != 0 &&
                        HeroKitDatabase.GetGlobals().ints.items.Count > slotID)
                    {
                        slotValue = HeroKitDatabase.GetGlobals().ints.items[slotID].value.ToString();
                    }
                    else
                        if (heroKitObject.debugHeroObject) Debug.LogError("Global Integer Slot does not exist!");
                }

                else if (codeType == CodeType.BoolGlobal)
                {
                    // get the value from the variable list
                    if (HeroKitDatabase.GetGlobals().bools.items != null &&
                        HeroKitDatabase.GetGlobals().bools.items.Count != 0 &&
                        HeroKitDatabase.GetGlobals().bools.items.Count > slotID)
                    {
                        slotValue = HeroKitDatabase.GetGlobals().bools.items[slotID].value.ToString();
                    }
                    else
                        if (heroKitObject.debugHeroObject) Debug.LogError("Global Bool Slot does not exist!");
                }

                else if (codeType == CodeType.StringGlobal)
                {
                    // get the value from the variable list
                    if (HeroKitDatabase.GetGlobals().strings.items != null &&
                        HeroKitDatabase.GetGlobals().strings.items.Count != 0 &&
                        HeroKitDatabase.GetGlobals().strings.items.Count > slotID)
                    {
                        slotValue = HeroKitDatabase.GetGlobals().strings.items[slotID].value.ToString();
                    }
                    else
                        if (heroKitObject.debugHeroObject) Debug.LogError("Global String Slot does not exist!");
                }
            }

            return slotValue;
        }
    }
}