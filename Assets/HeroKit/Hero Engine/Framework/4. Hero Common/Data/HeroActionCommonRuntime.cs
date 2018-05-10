// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene.ActionField;
using HeroKit.Scene.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace HeroKit.Scene.Actions
{
    /// <summary>
    /// Common methods used by Hero Kit Actions during runtime.
    /// </summary>
    public static class HeroActionCommonRuntime
    {
        // --------------------------------------------------------------
        // General
        // --------------------------------------------------------------

        /// <summary>
        /// Check to see if a list of actions can be removed from the Long Action list on the hero kit object.
        /// </summary>
        /// <param name="updateIsDone">Has an action that needed multiple frames completed?</param>
        /// <param name="eventID">The ID assigend to the event in which this action resides.</param>
        /// <param name="heroKitObject">The hero kit object that contains the long action list.</param>
        /// <returns>Action can be removed from the Long Action list?</returns>
		public static bool RemoveFromLongActions(bool updateIsDone, int eventID, HeroKitObject heroKitObject, bool playNextAction = true)
        {
            if (updateIsDone && playNextAction)
            {
                // this is a problem. sometimes we need event to keep waiting (ex. particle effect in loop)
                heroKitObject.heroState.heroEvent[eventID].waiting = false;
            }
            return updateIsDone;
        }

        /// <summary>
        /// Check to see if a game object is in range.
        /// </summary>
        /// <param name="useX">Should we check object's X coordinate?</param>
        /// <param name="useY">Should we check object's Y coordinate?</param>
        /// <param name="useZ">Should we check object's Z coordinate?</param>
        /// <param name="radius">Search within this radius.</param>
        /// <param name="objectPosition">The current position of the game object.</param>
        /// <returns>Is the game object inside the radius?</returns>
        public static bool InRange(bool useX, bool useY, bool useZ, Vector3 radius, Vector3 objectPosition)
        {
            bool result = false;

            // X, Y, Z
            if (useX && useY && useZ)
            {
                if (objectPosition.x <= radius.x && objectPosition.y <= radius.y && objectPosition.z <= radius.z) result = true;
            }

            // X
            else if (useX && !useY && !useZ)
            {
                if (objectPosition.x <= radius.x) result = true;
            }

            // Y
            else if (!useX && useY && !useZ)
            {
                if (objectPosition.y <= radius.y) result = true;
            }

            // Z
            else if (!useX && !useY && useZ)
            {
                if (objectPosition.z <= radius.z) result = true;
            }

            // X, Y
            else if (useX && useY && !useZ)
            {
                if (objectPosition.x <= radius.x && objectPosition.y <= radius.y) result = true;
            }

            // X, Z
            else if (useX && !useY && useZ)
            {
                if (objectPosition.x <= radius.x && objectPosition.z <= radius.z) result = true;
            }

            // Y, Z
            else if (!useX && useY && useZ)
            {
                if (objectPosition.y <= radius.y && objectPosition.z <= radius.z) result = true;
            }

            return result;
        }

        /// <summary>
        /// Assign a group of hero kit objects to a slot in a list.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="actionType">Replace or append list of existing hero kit objects in hero object list? (1=replace, 2=append)</param>
        public static void AssignObjectsToList(HeroKitObject heroKitObject, int actionFieldID, List<HeroKitObject> items, int actionType)
        {
            // assign the hero kit objects to the hero kit object list
            if (actionType == 1)
            {
                HeroObjectFieldValue.SetValueB(heroKitObject, actionFieldID, items);
            }

            // append the hero kit objects to the existing list
            else if (actionType == 2)
            {
                List<HeroKitObject> oldList = HeroObjectFieldValue.GetValueB(heroKitObject, actionFieldID);
                List<HeroKitObject> newList = new List<HeroKitObject>();

                if (oldList != null && items != null)
                {
                    newList = items.Union(oldList).ToList();
                }
                else if (oldList != null)
                {
                    newList = oldList;
                }
                else if (items != null)
                {
                    newList = items;
                }

                HeroObjectFieldValue.SetValueB(heroKitObject, actionFieldID, newList);
            }
        }

        /// <summary>
        /// Change the audio settings on a hero kit object.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="targetObject">The hero kit object that contains the audio files.</param>
        /// <param name="audioSourceName">The name of the audio source on the target object.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        public static void ChangeAudioSettings(HeroKitObject heroKitObject, HeroKitObject targetObject, string audioSourceName, int actionFieldIDA)
        {
            AudioSource audioSource = targetObject.GetHeroComponent<AudioSource>(audioSourceName, true, true);

            bool changeVolume = BoolValue.GetValue(heroKitObject, actionFieldIDA);
            if (changeVolume)
            {
                audioSource.volume = IntegerFieldValue.GetValueA(heroKitObject, actionFieldIDA + 1) * 0.01f;
            }

            bool changePitch = BoolValue.GetValue(heroKitObject, actionFieldIDA + 2);
            if (changePitch)
            {
                audioSource.pitch = IntegerFieldValue.GetValueA(heroKitObject, actionFieldIDA + 3) * 0.01f;
            }

            bool changeStereoPan = BoolValue.GetValue(heroKitObject, actionFieldIDA + 4);
            if (changeStereoPan)
            {
                float x = IntegerFieldValue.GetValueA(heroKitObject, actionFieldIDA + 5);
                float y = (x * 2);
                float z = y * 0.01f;
                audioSource.panStereo = z;
            }

            bool changeSpatialBlending = BoolValue.GetValue(heroKitObject, actionFieldIDA + 6);
            if (changeSpatialBlending)
            {
                audioSource.spatialBlend = IntegerFieldValue.GetValueA(heroKitObject, actionFieldIDA + 7) * 0.01f;
            }

            bool changeReverbZoneMix = BoolValue.GetValue(heroKitObject, actionFieldIDA + 8);
            if (changeReverbZoneMix)
            {
                float x = IntegerFieldValue.GetValueA(heroKitObject, actionFieldIDA + 9);
                float y = x * 1.1f;
                float z = y * 0.01f;
                audioSource.reverbZoneMix = z;
            }

            bool changeMixer = BoolValue.GetValue(heroKitObject, actionFieldIDA + 10);
            if (changeMixer)
            {
                audioSource.outputAudioMixerGroup = ObjectValue.GetValue<AudioMixerGroup>(heroKitObject, actionFieldIDA + 11);
            }
        }

        // --------------------------------------------------------------
        // Conditional actions
        // --------------------------------------------------------------

        /// <summary>
        /// Get the conditional action that follows this action in the if / if else / else / end sequence
        /// Items only on the same indent level are reviewed. All other items are ignored (actions inside if statement, etc)
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the action.</param>
        /// <param name="eventID">The ID of the event that contains the action.</param>
        /// <param name="actionID">The ID of the action.</param>
        /// <param name="currentIndent">The indent value of the action.</param>
        /// <returns>Get the next conditional action?</returns>
        public static int GetNextConditionAction(HeroKitObject heroKitObject, int eventID, int actionID, int currentIndent)
        {
            int nextAction = -99;

            for (int i = actionID + 1; i < heroKitObject.heroState.heroEvent[eventID].actions.Count; i++)
            {
                if (heroKitObject.heroState.heroEvent[eventID].actions[i].indent == currentIndent)
                {
                    nextAction = i - 1;
                    heroKitObject.heroState.heroEvent[eventID].actions[i].actionFields[0].bools[0] = false;
                    break;
                }
            }

            return nextAction;
        }
        /// <summary>
        /// A list of conditional actions that can be skipped.
        /// </summary>
        public static string[] actionsToSkipIf = new string[] { "Else", "Else If Integer", "Else If Float" };
        /// <summary>
        /// Check to see if the children of this conditional action should be skipped.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the conditional actions.</param>
        /// <param name="eventID">The ID of the event that contains this action.</param>
        /// <param name="actionID">The ID of the action.</param>
        /// <returns>Should the children of this conditional action be skipped?</returns>
        public static bool SkipConditionalAction(HeroKitObject heroKitObject, int eventID, int actionID)
        {
            // exit early if there are no more conditional actions
            if (actionID < 0)
                return false;

            HeroAction action = heroKitObject.heroState.heroEvent[eventID].actions[actionID];

            return action.actionFields[0].bools[0];
        }
        /// <summary>
        /// Pass in an If statement Action. 
        /// If the If statement is true, play its child actions and set flag in next "Else" statement to false. This lets Else action know to skip over its child actions.
        /// If the If statement is false, skip to the next conditional action (like Else or End)
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the conditional actions.</param>
        /// <param name="eventID">The ID of the event that contains this action.</param>
        /// <param name="actionID">The ID of the action.</param>
        /// <param name="currentIndent">The indent value of the action.</param>
        /// <param name="evaluation">The evaluation of this condition.</param>
        /// <returns>Should the children of this conditional action be executed?</returns>
        public static int RunConditionalIfAction(HeroKitObject heroKitObject, int eventID, int actionID, int currentIndent, bool evaluation)
        {
            int thisAction = 0;

            // get the conditional action that follows this action in the if / if else / else / end sequence
            int nextConditionalAction = GetNextConditionAction(heroKitObject, eventID, actionID, currentIndent);

            // if evaluation is true, execute next action in list 
            if (evaluation)
            {
                // get the next conditinal action and set its "skip me" flag if it should be skipped.
                TurnOffConditionalAction(heroKitObject, eventID, nextConditionalAction, actionsToSkipIf);
                thisAction = actionID;
            }
            // if evaluation is false, skip to next conditional action and execute it.
            else
            {
                thisAction = nextConditionalAction;
            }

            return thisAction;
        }
        /// <summary>
        /// Get a conditional action and check to see if its children should be skipped.
        /// If the actions should be skipped, set the "skip" flag.
        /// This is called by a preceeding conditional action, such as If Condition.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the conditional actions.</param>
        /// <param name="eventID">The ID of the event that contains this action.</param>
        /// <param name="actionID">The ID of the action.</param>
        /// <param name="actionToSkip">The actions to skip.</param>
        public static void TurnOffConditionalAction(HeroKitObject heroKitObject, int eventID, int actionID, string[] actionToSkip)
        {
            // exit early if there are no more conditional actions
            if (actionID < 0) return;

            // get the conditional action to turn off
            HeroAction action = heroKitObject.heroState.heroEvent[eventID].actions[actionID + 1];

            // turn it on by default
            action.actionFields[0].bools[0] = false;

            // turn off the conditional action if it matches the name of one of the actions to skip 
            for (int i = 0; i < actionToSkip.Length; i++)
            {
                if (action.actionTemplate.name == actionToSkip[i])
                    action.actionFields[0].bools[0] = true;
            }
        }

        // --------------------------------------------------------------
        // Compare Values
        // --------------------------------------------------------------

        /// <summary>
        /// Compare two integers.
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        /// <param name="value1">The first value to compare.</param>
        /// <param name="value2">The second value to compare.</param>
        /// <returns>The result of the comparison.</returns>
        public static bool CompareIntegers(int comparison, int value1, int value2)
        {
            bool success = false;

            switch (comparison)
            {
                case 1:
                    if (value1 == value2) success = true;
                    break;
                case 2:
                    if (value1 != value2) success = true;
                    break;
                case 3:
                    if (value1 < value2) success = true;
                    break;
                case 4:
                    if (value1 <= value2) success = true;
                    break;
                case 5:
                    if (value1 > value2) success = true;
                    break;
                case 6:
                    if (value1 >= value2) success = true;
                    break;
                default:
                    break;
            }

            return success;
        }
        /// <summary>
        /// Check to see if two vectors match
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        /// <param name="value1">The first value to compare.</param>
        /// <param name="value2">The second value to compare.</param>
        /// <returns>The result of the comparison.</returns>
        public static bool CompareFloats(int comparison, float value1, float value2)
        {
            bool success = false;

            float marginOfError = 0.001f;

            switch (comparison)
            {
                case 1:
                    success = ((value1 - value2) < marginOfError);
                    break;
                case 2:
                    success = ((value1 - value2) > marginOfError);
                    break;
                case 3:
                    if (value1 < value2) success = true;
                    break;
                case 4:
                    if (value1 <= value2) success = true;
                    break;
                case 5:
                    if (value1 > value2) success = true;
                    break;
                case 6:
                    if (value1 >= value2) success = true;
                    break;
            }

            return success;
        }
        /// <summary>
        /// Compare two bools.
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        /// <param name="value1">The first value to compare.</param>
        /// <param name="value2">The second value to compare.</param>
        /// <returns>The result of the comparison.</returns>
        public static bool CompareBools(int comparison, bool value1, bool value2)
        {
            bool success = false;

            switch (comparison)
            {
                case 1:
                    if (value1 == value2) success = true;
                    break;
                case 2:
                    if (value1 != value2) success = true;
                    break;
            }

            return success;
        }
        /// <summary>
        /// Compare two strings.
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        /// <param name="value1">The first value to compare.</param>
        /// <param name="value2">The second value to compare.</param>
        /// <returns>The result of the comparison.</returns>
        public static bool CompareStrings(int comparison, string value1, string value2)
        {
            bool success = false;

            switch (comparison)
            {
                case 1:
                    if (value1 == value2) success = true;
                    break;
                case 2:
                    if (value1 != value2) success = true;
                    break;
            }

            return success;
        }
        /// <summary>
        /// Compare two values.
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        /// <param name="value1">The first value to compare.</param>
        /// <param name="value2">The second value to compare.</param>
        /// <returns>The result of the comparison.</returns>
        public static bool CompareValues<T>(int comparison, T value1, T value2)
        {
            bool success = false;

            switch (comparison)
            {
                // value 1 = value 2
                case 1:
                    if (EqualityComparer<T>.Default.Equals(value1, value2)) success = true;
                    break;
                // value 1 != value 2
                case 2:
                    if (!EqualityComparer<T>.Default.Equals(value1, value2)) success = true;
                    break;
            }

            return success;
        }
        /// <summary>
        /// Check to see if two vectors match
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        /// <param name="value1">The first value to compare.</param>
        /// <param name="value2">The second value to compare.</param>
        /// <param name="marginOfError">The margin of error allowed.</param>
        /// <returns>The result of the comparison.</returns>
        public static bool CompareVectors(Vector3 value1, Vector3 value2, float marginOfError)
        {
            return Vector3.SqrMagnitude(value1 - value2) < marginOfError;
        }

        // --------------------------------------------------------------
        // Math Operations
        // --------------------------------------------------------------

        /// <summary>
        /// Perform math on two integers.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result of the operation.</returns>
        public static int PerformMathOnIntegers(int operation, int value1, int value2)
        {
            int result = 0;

            switch (operation)
            {
                case 1:
                    result = value1 + value2;
                    break;
                case 2:
                    result = value1 - value2;
                    break;
                case 3:
                    result = value1 * value2;
                    break;
                case 4:
                    if (value1 == 0 || value2 == 0)
                        result = 0;
                    else
                        result = value1 / value2;
                    break;
                case 5:
                    result = value1 % value2;
                    break;
                case 6:
                    result = value2;
                    break;
            }

            return result;
        }
        /// <summary>
        /// Perform math on two floats.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result of the operation.</returns>
        public static float PerformMathOnFloats(int operation, float value1, float value2)
        {
            float result = 0;

            switch (operation)
            {
                case 1:
                    result = value1 + value2;
                    break;
                case 2:
                    result = value1 - value2;
                    break;
                case 3:
                    result = value1 * value2;
                    break;
                case 4:
                    if (Math.Abs(value1) < 0.1f || Math.Abs(value2) < 0.1f)
                        result = 0;
                    else
                        result = value1 / value2;
                    break;
                case 5:
                    result = value1 % value2;
                    break;
                case 6:
                    result = value2;
                    break;
            }

            return result;
        }

        // --------------------------------------------------------------
        // Get Hero Objects / Filter Hero Objects
        // --------------------------------------------------------------

        /// <summary>
        /// Get a list of hero kit objects. 
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjects(HeroKitObject[] items, int objectCount)
        {
            if (items == null) return null;

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                // add the object to the new list (don't include this object in list)
                heroKitObjects.Add(items[i]);
                objectCount--;
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects. Objects must have a specific name. 
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="name">The name of the hero kit object in the scene.</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsByName(HeroKitObject[] items, int objectCount, string name)
        {
            if (items == null) return null;

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                // add the object to the new list (don't include this object in list)
                if (items[i].gameObject.name == name)
                {
                    heroKitObjects.Add(items[i]);
                    objectCount--;
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects. Objects must be colliding with a specific object in the scene. 
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="targetItem">The object that the hero kit objects must be colliding with.</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsByCollision(HeroKitObject[] items, int objectCount, HeroKitObject[] targetItem)
        {
            if (items == null) return null;
            if (targetItem == null || targetItem[0] == null) return null;

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                bool result = false;
                for (int j = 0; j < targetItem[0].collisionsList.Count; j++)
                {
                    if (items[i].name == targetItem[0].collisionsList[j].name)
                    {
                        if (items[i] == targetItem[0].collisionsList[j])
                        {
                            result = true;
                            break;
                        }
                    }
                }

                // add the object to the new list
                if (result)
                {
                    heroKitObjects.Add(items[i]);
                    objectCount--;
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects. Objects must be in the trigger area of a specific object in the scene. 
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="targetItem">The object that the hero kit objects must be colliding with.</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsByTrigger(HeroKitObject[] items, int objectCount, HeroKitObject[] targetItem)
        {
            if (items == null) return null;
            if (targetItem == null || targetItem[0] == null) return null;

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                bool result = false;
                for (int j = 0; j < targetItem[0].triggersList.Count; j++)
                {
                    if (items[i].name == targetItem[0].triggersList[j].name)
                    {
                        if (items[i] == targetItem[0].triggersList[j].GetComponent<HeroKitObject>())
                        {
                            result = true;
                            break;
                        }
                    }
                }

                // add the object to the new list
                if (result)
                {
                    heroKitObjects.Add(items[i]);
                    objectCount--;
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects. Objects must have a specific tag. 
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="tag">The name of the tag.</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsByTag(HeroKitObject[] items, int objectCount, string tag)
        {
            if (items == null) return null;

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                // add the object to the new list (don't include this object in list)
                if (items[i].gameObject.tag == tag)
                {
                    heroKitObjects.Add(items[i]);
                    objectCount--;
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects. Objects must have a specific GUID. 
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="heroGUID">The GUID for a hero kit object.</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsByGUID(HeroKitObject[] items, int objectCount, int heroGUID)
        {
            if (items == null) return null;

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                // add the object to the new list (don't include this object in list)
                if (items[i].heroGUID == heroGUID)
                {
                    heroKitObjects.Add(items[i]);
                    objectCount--;
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects. Objects must exist on a specific layer. 
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="layer">The layer.</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsByLayer(HeroKitObject[] items, int objectCount, int layer)
        {
            if (items == null) return null;

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                // add the object to the new list (don't include this object in list)
                if (items[i].gameObject.layer == layer)
                {
                    heroKitObjects.Add(items[i]);
                    objectCount--;
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects. Objects must have a specific hero object assigned to them. 
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="heroObject">The hero object.</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsByType(HeroKitObject[] items, int objectCount, HeroObject heroObject)
        {
            if (items == null) return null;

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                // add the object to the new list (don't include this object in list)
                if (items[i].heroObject == heroObject)
                {
                    heroKitObjects.Add(items[i]);
                    objectCount--;
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects. Objects must have a specific hero property assigned to them. 
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="heroProperty">The hero property.</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsByProperty(HeroKitObject[] items, int objectCount, HeroKitProperty heroProperty)
        {
            if (items == null) return null;

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                for (int j = 0; j < items[i].heroObject.propertiesList.properties.Count; j++)
                {
                    // add the object to the new list (don't include this object in list)
                    if (items[i].heroObject.propertiesList.properties[j].propertyTemplate == heroProperty)
                    {
                        heroKitObjects.Add(items[i]);
                        objectCount--;
                        break;
                    }
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects that were hit with a 3D ray. 
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="originObject">The object from which the ray originated.</param>
        /// <param name="rayType">The type of ray.</param>
        /// <param name="rayDirectionType">The direction of the ray.</param>
        /// <param name="rayDistance">The distance of the ray.</param>
        /// <param name="debugRay">Show the ray in the editor?</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsRay(int objectCount, GameObject originObject, int rayType, int rayDirectionType, float rayDistance, bool debugRay = false)
        {
            GameObject[] gameObjects = new GameObject[0];

            // origin is the main camera
            if (rayType == 1)
            {
                gameObjects = UseRayFromCamera3D(null, rayDirectionType, rayDistance, debugRay);
            }

            // origin is a camera
            else if (rayType == 2)
            {
                gameObjects = UseRayFromCamera3D(originObject, rayDirectionType, rayDistance, debugRay);
            }

            // origin is a hero object
            else if (rayType == 3)
            {
                gameObjects = UseRayFromObject3D(originObject, rayDirectionType, rayDistance, debugRay);
            }

            // get only game objects that have hero kit objects attached to them
            List<HeroKitObject> heroKitObjects = FilterHeroObjects(gameObjects);

            // exit early if there are no objects to truncate
            if (heroKitObjects == null || heroKitObjects.Count <= objectCount) return heroKitObjects;

            // get index to start deletion
            int index = objectCount;

            // get # of entries to delete
            int count = objectCount - heroKitObjects.Count;

            // delete the unneeded objects
            heroKitObjects.RemoveRange(index, count);

            // return the hero kit objects
            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects that were hit with a 2D ray. 
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="originObject">The object from which the ray originated.</param>
        /// <param name="rayType">The type of ray.</param>
        /// <param name="rayDirectionType">The direction of the ray.</param>
        /// <param name="rayDistance">The distance of the ray.</param>
        /// <param name="debugRay">Show the ray in the editor?</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsRay2D(int objectCount, HeroKitObject targetHKO, GameObject originObject, int rayType, int rayDirectionType, float rayDistance, bool debugRay = false)
        {
            GameObject[] gameObjects = new GameObject[0];

            // origin is the main camera
            if (rayType == 1)
            {
                gameObjects = UseRayFromCamera2D(null, rayDirectionType, true);
            }

            // origin is a camera
            else if (rayType == 2)
            {
                gameObjects = UseRayFromCamera2D(originObject, rayDirectionType, false);
            }

            // origin is a hero object
            else if (rayType == 3)
            {
                gameObjects = UseRayFromObject2D(targetHKO, originObject, rayDirectionType, rayDistance, debugRay);
            }

            // get only game objects that have hero kit objects attached to them
            List<HeroKitObject> heroKitObjects = FilterHeroObjects(gameObjects);

            // exit early if there are no objects to truncate
            if (heroKitObjects == null || heroKitObjects.Count <= objectCount) return heroKitObjects;

            // get index to start deletion
            int index = objectCount;

            // get # of entries to delete
            int count = objectCount - heroKitObjects.Count;

            // delete the unneeded objects
            heroKitObjects.RemoveRange(index, count);

            // return the hero kit objects
            return heroKitObjects;
        }

        ///// <summary>
        ///// Get a list of hero kit objects that were hit with a ray (field of view, specific object in scene). 
        ///// </summary>
        ///// <param name="items">The hero kit objects.</param>
        ///// <param name="objectCount">The number of objects to include in the new list.</param>
        ///// <param name="originObject">The object from which the ray originated.</param>
        ///// <param name="rayType">The type of ray.</param>
        ///// <param name="rayDirectionType">The direction of the ray.</param>
        ///// <param name="rayDistance">The distance of the ray.</param>
        ///// <param name="debugRay">Show the ray in the editor?</param>
        ///// <returns>A new list of hero kit objects.</returns>
        //public static List<HeroKitObject> GetHeroObjectsFOV(int objectCount, GameObject originObject, int rayDirectionType, float rayDistance, float rayDegrees, bool debugRay = false)
        //{
        //    GameObject[] gameObjects = new GameObject[0];

        //    gameObjects = UseRayFromObject(originObject, rayDirectionType, rayDistance, debugRay);

        //    // get only game objects that have hero kit objects attached to them
        //    List<HeroKitObject> heroKitObjects = FilterHeroObjects(gameObjects);

        //    // exit early if there are no objects to truncate
        //    if (heroKitObjects == null || heroKitObjects.Count <= objectCount) return heroKitObjects;

        //    // get index to start deletion
        //    int index = objectCount;

        //    // get # of entries to delete
        //    int count = objectCount - heroKitObjects.Count;

        //    // delete the unneeded objects
        //    heroKitObjects.RemoveRange(index, count);

        //    // return the hero kit objects
        //    return heroKitObjects;
        //}

        /// <summary>
        /// Get a list of hero kit objects. Objects must be at a specific position in the scene.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="position">The position in the scene.</param>
        /// <param name="useX">Include X coordinate?</param>
        /// <param name="useY">Include Y coordinate?</param>
        /// <param name="useZ">Include Z coordinate?</param>
        /// <param name="radius">How close do hero objects need to be to coordinates to be added to the list?</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsPosition(HeroKitObject heroKitObject, HeroKitObject[] items, int objectCount, Vector3 position, bool useX, bool useY, bool useZ, float radius)
        {
            if (items == null) return null;

            // set up some vectors
            Vector3 objectDistance = new Vector3();
            Vector3 objectRadius = new Vector3(radius, radius, radius);

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                Vector3 objectPos = items[i].gameObject.transform.position;
                Vector3 objectScale = items[i].gameObject.transform.localScale;

                // (position of object in scene * scale of object in scene) - (position where object must be at to be captured)
                objectDistance.x = (int)Math.Abs((objectPos.x * objectScale.x) - position.x);
                objectDistance.y = (int)Math.Abs((objectPos.y * objectScale.y) - position.y);
                objectDistance.z = (int)Math.Abs((objectPos.z * objectScale.z) - position.z);

                // is object in range of position?
                bool result = InRange(useX, useY, useZ, objectRadius, objectDistance);

                // add the object to the new list (don't include this object in list)
                if (result)
                {
                    if (items[i].gameObject != heroKitObject.gameObject)
                    {
                        heroKitObjects.Add(items[i]);
                        objectCount--;
                    }
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects. Objects must be within a specific distance of another object.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="radius">How close do hero objects need to be to coordinates to be added to the list?</param>
        /// <param name="useX">Include X coordinate?</param>
        /// <param name="useY">Include Y coordinate?</param>
        /// <param name="useZ">Include Z coordinate?</param>
        /// <param name="targetObject">The object that the hero kit objects must be near.</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsDistance(HeroKitObject heroKitObject, HeroKitObject[] items, int objectCount, Vector3 radius, bool useX, bool useY, bool useZ, GameObject targetObject)
        {
            // exit early if no target object or list of items
            if (items == null || targetObject == null)
            {
                return null;
            }

            // set up some vectors
            Vector3 objectDistance = new Vector3();
            Vector3 targetPos = targetObject.transform.position;
            Vector3 targetScale = targetObject.transform.localScale;

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                Vector3 objectPos = items[i].gameObject.transform.position;
                Vector3 objectScale = items[i].gameObject.transform.localScale;

                // (position of object in scene * scale of object in scene) - (position where object must be at to be captured)
                objectDistance.x = (int)Math.Abs((objectPos.x * objectScale.x) - (targetPos.x * targetScale.x));
                objectDistance.y = (int)Math.Abs((objectPos.y * objectScale.y) - (targetPos.y * targetScale.y));
                objectDistance.z = (int)Math.Abs((objectPos.z * objectScale.z) - (targetPos.z * targetScale.z));

                // is object in range of position?
                bool result = InRange(useX, useY, useZ, radius, objectDistance);

                // add the object to the new list (don't include this object in list)
                if (result)
                {
                    if (items[i].gameObject != heroKitObject.gameObject)
                    {
                        heroKitObjects.Add(items[i]);
                        objectCount--;
                    }
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects. Objects must be within the field of view of another object.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="targetObjects">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="originObject">The object that the hero kit objects must be near.</param>
        /// /// <param name="fieldOfView">How far the object can see on the sides (degrees).</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsFOV(HeroKitObject[] targetObjects, int objectCount, GameObject originObject, float fieldOfView, int rayDirectionType)
        {
            // exit early if no target object or list of items
            if (targetObjects == null || originObject == null)
            {
                return null;
            }

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < targetObjects.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                // check if target is in origin's field of view
                Vector3 rayDirection = targetObjects[i].transform.position - originObject.transform.position;
                Vector3 originDirection = GetRayDirection3D(originObject, rayDirectionType);
                float angle = Vector3.Angle(rayDirection, originDirection);
                if (angle <= fieldOfView * 0.5f)
                {
                    // get the distance between the origin and target
                    float distanceToTarget = Vector3.Distance(targetObjects[i].transform.position, originObject.transform.position);
                    GameObject[] rayObjects = UseRayFromObject3D(originObject, targetObjects[i].gameObject, distanceToTarget);

                    // get the target object if it was hit. First make sure it was hit. Then check it's name.
                    // if the name looks good, use getComponent to do the final comparison.
                    if (rayObjects != null)
                    {
                        for (int j = 0; j < rayObjects.Length; j++)
                        {
                            if (rayObjects[j].name == targetObjects[i].name)
                            {
                                if (rayObjects[j].GetComponent<HeroKitObject>() == targetObjects[i])
                                {
                                    heroKitObjects.Add(targetObjects[i]);
                                    objectCount--;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get a list of hero kit objects that are persistent or get a list of hero kit objects that are not persistent.
        /// </summary>
        /// <param name="items">The hero kit objects.</param>
        /// <param name="objectCount">The number of objects to include in the new list.</param>
        /// <param name="isPersistent">Get persistent objects (true). Get non-persistent objects (false).</param>
        /// <returns>A new list of hero kit objects.</returns>
        public static List<HeroKitObject> GetHeroObjectsByPersistence(HeroKitObject[] items, int objectCount, bool isPersistent)
        {
            if (items == null) return null;

            // get the hero kit objects in the scene that have the matching tag
            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();
            for (int i = 0; i < items.Length; i++)
            {
                // exit early if object count has been reached
                if (objectCount <= 0) break;

                // add the object to the new list 
                if (items[i].persist && isPersistent)
                {
                    heroKitObjects.Add(items[i]);
                    objectCount--;
                }
                if (!items[i].persist && !isPersistent)
                {
                    heroKitObjects.Add(items[i]);
                    objectCount--;
                }
            }

            return heroKitObjects;
        }
        /// <summary>
        /// Get all hero kit objects in the scene.
        /// </summary>
        /// <returns>All hero kit object in the scene.</returns>
        public static HeroKitObject[] GetHeroObjectsInScene()
        {
            return UnityEngine.Object.FindObjectsOfType<HeroKitObject>();
        }
        /// <summary>
        /// Get all game objects that have a hero object attached to them.
        /// </summary>
        /// <param name="gameObjects"></param>
        /// <returns></returns>
        public static List<HeroKitObject> FilterHeroObjects(GameObject[] gameObjects)
        {
            if (gameObjects == null || gameObjects.Length == 0) return null;

            List<HeroKitObject> heroKitObjects = new List<HeroKitObject>();

            for (int i = 0; i < gameObjects.Length; i++)
            {
                HeroKitObject hko = gameObjects[i].GetComponent<HeroKitObject>();
                if (hko != null) heroKitObjects.Add(hko);
            }

            return heroKitObjects;
        }

        // --------------------------------------------------------------
        // Rays
        // --------------------------------------------------------------

        /// <summary>
        /// Convert 3D raycast hits to game objects.
        /// </summary>
        /// <param name="raycastHits">The raycast hits.</param>
        /// <returns>The game objects.</returns>
        public static GameObject[] ConvertRaysToGameObjects3D(RaycastHit[] raycastHits)
        {
            int length = (raycastHits != null) ? raycastHits.Length : 0;
            GameObject[] gameObjects = new GameObject[length];

            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i] = raycastHits[i].transform.gameObject;
            }

            return gameObjects;
        }
        /// <summary>
        /// Convert 2D raycast hits to game objects.
        /// </summary>
        /// <param name="raycastHits">The raycast hits.</param>
        /// <returns>The game objects.</returns>
        public static GameObject[] ConvertRaysToGameObjects2D(RaycastHit2D[] raycastHits, GameObject origin = null)
        {
            int length = (raycastHits != null) ? raycastHits.Length : 0;
            List<GameObject> gameObjects = new List<GameObject>();

            for (int i = 0; i < raycastHits.Length; i++)
            {
                // if we are shooting ray from an object, don't let object hit itself. 
                // this is only an issue with 2D.
                if (origin != null && raycastHits[i].transform.gameObject == origin)
                    continue;

                gameObjects.Add(raycastHits[i].transform.gameObject);
            }

            return gameObjects.ToArray();
        }

        /// <summary>
        /// Get the direction of a ray.
        /// </summary>
        /// <param name="origin">The origin of the ray.</param>
        /// <param name="rayDirectionType">The direction type of the ray.</param>
        /// <returns>The direction of a ray.</returns>
        public static Vector3 GetRayDirection3D(GameObject origin, int rayDirectionType)
        {
            // exit early if no game object origin
            if (origin == null) return new Vector3();

            // get direction
            Vector3 direction = new Vector3();
            switch (rayDirectionType)
            {
                case 1: // shoot forward from object
                    direction = origin.transform.forward;
                    break;
                case 2: // shoot behind object
                    direction = -origin.transform.forward;
                    break;
                case 3: // shoot above object
                    direction = origin.transform.up;
                    break;
                case 4: // shoot below object
                    direction = -origin.transform.up;
                    break;
                case 5: // shoot from the left of object
                    direction = -origin.transform.right;
                    break;
                case 6: // shoot from the right of object
                    direction = origin.transform.right;
                    break;
            }

            return direction;
        }
        /// <summary>
        /// Get the direction of a ray.
        /// </summary>
        /// <param name="origin">The origin of the ray.</param>
        /// <param name="rayDirectionType">The direction type of the ray.</param>
        /// <returns>The direction of a ray.</returns>
        public static Vector3 GetRayDirection2D(HeroKitObject origin, int rayDirectionType)
        {
            // exit early if no game object origin
            if (origin == null) return new Vector3();

            // get direction
            Vector2 direction = new Vector2();
            switch (rayDirectionType)
            {
                case 1: // shoot forward from object
                    direction = GetFacingDirection2D(origin, rayDirectionType);
                    break;
                case 2: // shoot behind object
                    direction = GetFacingDirection2D(origin, rayDirectionType);
                    break;
                case 3: // shoot above object
                    direction = origin.transform.up;
                    break;
                case 4: // shoot below object
                    direction = -origin.transform.up;
                    break;
                case 5: // shoot from the left of object
                    direction = -origin.transform.right;
                    break;
                case 6: // shoot from the right of object
                    direction = origin.transform.right;
                    break;
            }

            return direction;
        }
        public static Vector2 GetFacingDirection2D(HeroKitObject origin, int rayDirectionType)
        {
            Vector2 direction = new Vector2();
            HeroSettings2D settings = origin.GetHeroComponent<HeroSettings2D>("HeroSettings2D", false, false, false);
            if (settings != null)
            {
                switch (settings.faceDir)
                {
                    case HeroSettings2D.FaceDir.up: // shoot above object
                        direction = origin.transform.up;
                        break;
                    case HeroSettings2D.FaceDir.down: // shoot below object
                        direction = -origin.transform.up;
                        break;
                    case HeroSettings2D.FaceDir.left: // shoot from the left of object
                        direction = -origin.transform.right;
                        break;
                    case HeroSettings2D.FaceDir.right: // shoot from the right of object
                        direction = origin.transform.right;
                        break;
                }

                // flip the direction if ray needs to face away from the direction the object is facing
                if (rayDirectionType == 2)
                    direction = -direction;
            }
            else
            {
                Debug.LogWarning("Hero Object needs you to set its facing direction before you use this action.");    
            }
            return direction;
        }

        /// <summary>
        /// Get the game objects that were hit by a 3D ray from a game object in the scene.
        /// </summary>
        /// <param name="origin">The origin of the ray.</param>
        /// <param name="rayDirectionType">The direction type of the ray.</param>
        /// <param name="distance">The distance that the ray will travel.</param>
        /// <param name="debugRay">Show the ray in the editor?</param>
        /// <returns>The game objects that were hit by the ray.</returns>
        public static GameObject[] UseRayFromObject3D(GameObject origin, int rayDirectionType, float distance, bool debugRay = false)
        {
            // exit early if no game object origin
            if (origin == null) return null;

            // get direction
            Vector3 direction = GetRayDirection3D(origin, rayDirectionType);

            // cast the ray
            RaycastHit[] hits = Physics.RaycastAll(origin.transform.position, direction, distance);

            if (debugRay)
            {
                //Debug.Log("Origin of Ray: " + origin.name + " Direction of Ray: " + direction + " Distance of Ray: " + distance);
                Debug.DrawRay(origin.transform.position, direction * distance, Color.green, 60);
            }

            //if (hits.Length != 0) Debug.Log("boom!");

            GameObject[] gameObjects = ConvertRaysToGameObjects3D(hits);

            return gameObjects;
        }
        /// <summary>
        /// Get the game objects that were hit by a 2D ray from a game object in the scene.
        /// </summary>
        /// <param name="origin">The origin of the ray.</param>
        /// <param name="rayDirectionType">The direction type of the ray.</param>
        /// <param name="distance">The distance that the ray will travel.</param>
        /// <param name="debugRay">Show the ray in the editor?</param>
        /// <returns>The game objects that were hit by the ray.</returns>
        public static GameObject[] UseRayFromObject2D(HeroKitObject targetHKO, GameObject origin, int rayDirectionType, float distance, bool debugRay = false)
        {
            // exit early if no game object origin
            if (origin == null) return null;

            // get direction
            Vector2 direction = GetRayDirection2D(targetHKO, rayDirectionType);

            // cast the ray
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin.transform.position, direction, distance);

            if (debugRay)
            {
                //Debug.Log("Origin of Ray: " + origin.name + " Direction of Ray: " + direction + " Distance of Ray: " + distance);
                Debug.DrawRay(origin.transform.position, direction * distance, Color.green, 60);
            }

            //if (hits.Length != 0) Debug.Log("boom!");

            GameObject[] gameObjects = ConvertRaysToGameObjects2D(hits, origin);

            return gameObjects;
        }

        /// <summary>
        /// Get the game objects that were hit by a 3D ray from a game object in the scene.
        /// </summary>
        /// <param name="origin">The origin of the ray.</param>
        /// <param name="rayDirectionType">The direction type of the ray.</param>
        /// <param name="distance">The distance that the ray will travel.</param>
        /// <param name="debugRay">Show the ray in the editor?</param>
        /// <returns>The game objects that were hit by the ray.</returns>
        public static GameObject[] UseRayFromObject3D(GameObject origin, GameObject target, float distance, bool debugRay = true)
        {
            // exit early if no game object origin
            if (origin == null) return null;

            // get direction
            Vector3 direction = (target.transform.localPosition - origin.transform.position);

            // cast the ray
            RaycastHit[] hits = Physics.RaycastAll(origin.transform.position, direction, distance * 100);

            if (debugRay)
            {
                //Debug.Log("Origin of Ray: " + origin.name + " Direction of Ray: " + direction + " Distance of Ray: " + distance);
                Debug.DrawRay(origin.transform.position, direction * distance, Color.green, 60);
            }

            //if (hits.Length != 0) Debug.Log("boom!");

            GameObject[] gameObjects = ConvertRaysToGameObjects3D(hits);

            return gameObjects;
        }

        /// <summary>
        /// Get the game objects that were hit by a 3D ray from a camera in the scene.
        /// </summary>
        /// <param name="origin">The origin of the ray.</param>
        /// <param name="rayDirectionType">The direction type of the ray.</param>
        /// <param name="distance">The distance that the ray will travel.</param>
        /// <param name="mainCamera">Use the main camera?</param>
        /// <returns>The game objects that were hit by the ray.</returns>
        public static GameObject[] UseRayFromCamera3D(GameObject origin, int rayDirectionType, float distance, bool mainCamera)
        {
            // get the camera
            Camera camera = null;
            if (mainCamera)
            {
                camera = Camera.main;
            }
            else
            {
                if (origin != null) camera = origin.GetComponent<Camera>();
            }

            // if no camera exists, exit early
            if (camera == null)
            {
                if (mainCamera)
                    Debug.LogError("Cannot cast ray from camera. There is no main camera assigned to the scene.");
                else
                    Debug.LogError("Cannot cast ray from camera. There was no camera attached to the game object.");

                return null;
            }

            // get the direction of the ray
            Ray direction = new Ray();
            switch (rayDirectionType)
            {
                case 1: // shoot from camera towards mouse position
                    direction = camera.ScreenPointToRay(Input.mousePosition);
                    break;
            }

            RaycastHit[] hits = Physics.RaycastAll(direction, distance);

            GameObject[] gameObjects = ConvertRaysToGameObjects3D(hits);

            return gameObjects;
        }
        /// <summary>
        /// Get the game objects that were hit by a 2D ray from a camera in the scene.
        /// </summary>
        /// <param name="origin">The origin of the ray.</param>
        /// <param name="rayDirectionType">The direction type of the ray.</param>
        /// <param name="distance">The distance that the ray will travel.</param>
        /// <param name="mainCamera">Use the main camera?</param>
        /// <returns>The game objects that were hit by the ray.</returns>
        public static GameObject[] UseRayFromCamera2D(GameObject origin, int rayDirectionType, bool mainCamera)
        {
            // get the camera
            Camera camera = null;
            if (mainCamera)
            {
                camera = Camera.main;
            }
            else
            {
                if (origin != null) camera = origin.GetComponent<Camera>();
            }

            // if no camera exists, exit early
            if (camera == null)
            {
                if (mainCamera)
                    Debug.LogError("Cannot cast ray from camera. There is no main camera assigned to the scene.");
                else
                    Debug.LogError("Cannot cast ray from camera. There was no camera attached to the game object.");

                return null;
            }

            // get the direction of the ray
            Vector2 direction = new Vector2();
            switch (rayDirectionType)
            {
                case 1: // shoot from camera towards mouse position
                    direction = camera.ScreenToWorldPoint(Input.mousePosition);
                    break;
            }

            RaycastHit2D[] hits = Physics2D.RaycastAll(direction, Vector2.zero);

            GameObject[] gameObjects = ConvertRaysToGameObjects2D(hits);

            return gameObjects;
        }
    }
}