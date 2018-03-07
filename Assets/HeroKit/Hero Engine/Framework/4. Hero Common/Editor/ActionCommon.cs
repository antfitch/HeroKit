// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;
using System.Collections.Generic;
using HeroKit.Editor.ActionField;
using System.Linq;
using UnityEngine.Audio;
using UnityEngine.Events;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Common actions needed for Hero Actions when a Hero Object is loaded in the Hero Kit Editor.
    /// </summary>
    public static class ActionCommon
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Create the action fields on a hero object if they don't exist. 
        /// </summary>
        /// <remarks>This is used by HeroKit.Editor.ActionBlockFields classes.</remarks>
        /// <param name="heroAction">The Hero Action to update (part of Hero Object).</param>
        /// <param name="fieldCount">The number of Action fields to add to the Hero Action.</param>
        public static void CreateActionFieldsOnHeroObject(HeroAction heroAction, int fieldCount)
        {
            // only create the fields if they don't already exist
            if (heroAction.actionFields == null || heroAction.actionFields.Count != fieldCount)
            {
                // create new list if it doesn't exist or if new list should be empty
                if (heroAction.actionFields == null || fieldCount == 0)
                {
                    heroAction.actionFields = new List<HeroActionField>();
                }

                // if there are more fields in the old list, cut out fields no longer needed
                else if (heroAction.actionFields.Count > fieldCount)
                {
                    // 12 in old list, need 10 in new list
                    int index = fieldCount;
                    int count = heroAction.actionFields.Count - fieldCount;
                    heroAction.actionFields.RemoveRange(index, count);
                }

                // if there are fewer fields in the old list, insert number of fields needed
                else if (heroAction.actionFields.Count < fieldCount)
                {
                    // 3 in old list, need 5 in new list
                    int index = heroAction.actionFields.Count - 1;
                    for (int i = index; i < fieldCount; i++)
                    {
                        heroAction.actionFields.Add(new HeroActionField());
                    }
                }
            }
        }
        /// <summary>
        /// Initialize a list of values for an action field.
        /// </summary>
        /// <typeparam name="T">The type of list.</typeparam>
        /// <param name="items">The list items.</param>
        /// <param name="slotCount">Number of slots to create.</param>
        /// <param name="defaultValue">Default value to add to each slot.</param>
        public static void CreateActionField<T>(ref List<T> items, int slotCount, T defaultValue)
        {
            // only create slots if they don't already exist
            if (items == null)
            {
                items = new List<T>();
                for (int i = 0; i < slotCount; i++)
                {
                    items.Add(defaultValue);
                }
            }

            // don't wipe out slots if they already exist. add extra values if they are needed.
            if (items.Count < slotCount)
            {
                for (int i = items.Count; i < slotCount; i++)
                {
                    items.Add(defaultValue);
                }
            }

            // don't wipe out slots if they already exist. remove the ones that aren't needed.
            else if (items.Count > slotCount)
            {
                // itemCount = 6
                // slotCount = 5
                while (items.Count > slotCount && items.Count > 0)
                {
                    items.RemoveAt(items.Count - 1);
                }
            }
        }
        /// <summary>
        /// Checks if a list can be created.
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <typeparam name="R">The list type.</typeparam>
        /// <param name="data">The data for the action field.</param>
        /// <param name="items">The list of items.</param>
        /// <param name="name">Name of the type of items in the list.</param>
        /// <returns>Does the list have any values in it?</returns>
        public static bool InitItemFieldList<T, R>(ref T data, List<R> items, string name) where T : ITargetHeroObject
        {
            bool hasValues = false;

            // hero object does not exist on game object or int list is empty
            // data.targetHeroObject == null || 
            if (items.Count == 0)
            {
                SimpleLayout.Label("[No " + name + "]");
                data.fieldID = 0;
            }
            // everything looks okay. draw list.
            else
            {
                // if we are referencing a field that no longer exists in list, reset condition field
                if (items.Count < data.fieldID)
                    data.fieldID = 0;

                hasValues = true;
            }

            return hasValues;
        }

        // --------------------------------------------------------------
        // Get Hero Object
        // --------------------------------------------------------------

        /// <summary>
        /// Hero Object = A hero object attached to this game object.
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <param name="data">The data for the action field.</param>
        /// <returns>The data for the action field.</returns>
        public static T GetThisHeroObject<T>(T data) where T : ITargetHeroObject
        {
            data.targetHeroObject = data.heroObject;
            return data;
        }
        /// <summary>
        /// Hero Object = A hero object attached to another game object in variable list.
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <param name="data">The data for the action field.</param>
        /// <returns>The data for the action field.</returns>
        public static T GetHeroObjectInVariables<T>(T data) where T : ITargetHeroObject
        {
            // display list of game object fields in this hero object
            data.objectID = HeroField.HeroObjectListField.SetValues(data.heroObject.lists.heroObjects.items, data.objectID, 0);

            // get the hero object assigned to the game object field if field has been selected
            if (data.objectID > 0)
            {
                data.targetHeroObject = data.heroObject.lists.heroObjects.items[data.objectID - 1].value;
            }
            else
            {
                data.targetHeroObject = null;
            }

            return data;
        }
        /// <summary>
        /// Hero Object = A hero object attached to another game object in global list.
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <param name="data">The data for the action field.</param>
        /// <returns>The data for the action field.</returns>
        public static T GetHeroObjectInGlobals<T>(T data) where T : ITargetHeroObject
        {
            // display list of game object fields in this hero object
            data.objectID = HeroField.HeroObjectListField.SetValues(HeroKitCommon.GetGlobals().globals.heroObjects.items, data.objectID, 0);

            // get the hero object assigned to the game object field if field has been selected
            if (data.objectID > 0)
            {
                data.targetHeroObject = HeroKitCommon.GetGlobals().globals.heroObjects.items[data.objectID - 1].value;
            }
            else
            {
                data.targetHeroObject = null;
            }

            return data;
        }
        /// <summary>
        /// Hero Object = A hero object attached to another game object in global list.
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <param name="data">The data for the action field.</param>
        /// <returns>The data for the action field.</returns>
        public static T GetHeroObjectInGlobalsB<T>(T data) where T : ITargetHeroObject
        {
            // get the hero object assigned to the game object field if field has been selected
            if (data.fieldID > 0)
            {
                data.targetHeroObject = HeroKitCommon.GetGlobals().globals.heroObjects.items[data.fieldID - 1].value;
            }
            else
            {
                data.targetHeroObject = null;
            }

            return data;
        }
        /// <summary>
        /// Hero Object = A hero object attached to a game object in a scene.
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <param name="data">The data for the action field.</param>
        /// <returns>The data for the action field.</returns>
        public static T GetHeroObjectInScene<T>(T data) where T : ITargetHeroObject
        {
            GameObject go = null;
            HeroKitObject hkoField = null;

            // if there is no game object attached to action, do this here...
            if (data.heroGUID == 0)
            {
                hkoField = SimpleLayout.ObjectField<HeroKitObject>(hkoField, 300, true);
                if (hkoField != null)
                    go = hkoField.gameObject;
            }

            // if a game object was attached to action, update fields here...
            if (data.heroGUID != 0)
            {
                HeroKitObject hko = HeroKitDatabase.GetHeroKitObject(data.heroGUID);
                if (hko != null)
                    go = hko.gameObject;

                oldGUID = data.heroGUID;
                newGUID = data.heroGUID;

                SimpleLayout.Space(5);
                SimpleLayout.Button(" [X] " + data.objectName, new UnityAction<T>(DeleteGameObject), data, Button.StyleB, 205);

                if (oldGUID != newGUID)
                {
                    data.heroGUID = newGUID;
                    data.objectName = "";
                    data.targetHeroObject = null;
                    go = null;
                }
            }

            // if there is a game object attached to the action field, populate it here
            if (go != null)
            {
                HeroKitObject hko = go.GetComponent<HeroKitObject>();
                if (hko == null)
                {
                    Debug.LogError("Game Object can't be added because it doesn't have a Hero Kit Object component. Add this component and re-add the game object.");
                    return data;
                }

                data.heroGUID = hko.heroGUID;
                data.objectName = go.name;

                // get the hero kit objects in the open scene(s)
                HeroKitObject[] heroKitObjects = Resources.FindObjectsOfTypeAll<HeroKitObject>();

                // get the gameobject that has the GUID we need
                for (int i = 0; i < heroKitObjects.Count(); i++)
                {
                    if (heroKitObjects[i].heroGUID == data.heroGUID)
                    {
                        data.targetHeroObject = heroKitObjects[i].heroObject;
                        break;
                    }
                }
            }
            else
            {
                data.targetHeroObject = null;
            }

            return data;
        }
        /// <summary>
        /// Hero Object = A hero object attached to another game object in property list.
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <param name="data">The data for the action field.</param>
        /// <returns>The data for the action field.</returns>
        public static T GetHeroObjectInProperties<T>(T data) where T : ITargetHeroObject
        {
            // display list of game object fields in this hero object
            data.objectID = HeroField.HeroObjectListField.SetValues(data.heroObject.propertiesList.properties[data.propertyID].itemProperties.heroObjects.items, data.objectID, 0);

            // get the hero object assigned to the game object field if field has been selected
            if (data.objectID > 0)
            {
                if (data.heroObject.propertiesList.properties[data.propertyID].itemProperties.heroObjects.items.Count > 0)
                {
                    data.targetHeroObject = data.heroObject.propertiesList.properties[data.propertyID].itemProperties.heroObjects.items[data.objectID - 1].value;
                }
            }

            return data;
        }
        /// <summary>
        /// Get the hero object you want to work with.
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <param name="data">The data for the action field.</param>
        /// <returns>The data for the action field.</returns>
        public static T GetTargetHeroObject<T>(T data) where T : ITargetHeroObject
        {
            // if we're not working with a global, get the object type. otherwise, set it manually.
            if (data.fieldType != 4) 
                data.objectType = new HeroField.HeroObjectTypeField().SetValues(data.objectType, 0);
            else
                data.objectType = 6;

            switch (data.objectType)
            {
                case 1:
                    data = GetThisHeroObject(data);
                    break;
                case 2:
                    data = GetHeroObjectInVariables(data);
                    break;
                case 3:
                    data = GetHeroObjectInScene(data);
                    break;
                case 4:
                    data = GetHeroObjectInProperties(data);
                    break;
                case 5: // global from secondary list
                    data = GetHeroObjectInGlobals(data);
                    break;
                case 6: // global from primary list
                    data = GetHeroObjectInGlobalsB(data);
                    break;
            }

            return data;
        }
        /// <summary>
        /// Get the hero object template you want to work with.
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <param name="data">The data for the action field.</param>
        /// <returns>The data for the action field.</returns>
        public static T GetTargetHeroTemplate<T>(T data) where T : ITargetHeroObject
        {
            data.objectType = new HeroField.HeroObjectTypeFieldB().SetValues(data.objectType, -5);
            switch (data.objectType)
            {
                case 1:
                    data.targetHeroObject = SimpleLayout.ObjectField(data.targetHeroObject, HeroKitCommon.GetWidthForField(213));
                    break;
                case 2:
                    data = GetThisHeroObject(data);
                    break;
                case 3:
                    data = GetHeroObjectInVariables(data);
                    break;
                case 4:
                    data = GetHeroObjectInScene(data);
                    break;
                case 5:
                    data = GetHeroObjectInProperties(data);
                    break;
                case 6:
                    data = GetHeroObjectInGlobals(data);
                    break;
            }

            return data;
        }

        // --------------------------------------------------------------
        // Get Game Object
        // --------------------------------------------------------------

        /// <summary>
        /// Game Object = A game object attached to another game object in variable list.
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <param name="data">The data for the action field.</param>
        /// <returns>The data for the action field.</returns>
        public static T GetGameObjectInVariables<T>(T data) where T : ITargetHeroObject
        {
            // display list of game object fields in this hero object
            data.objectID = HeroField.GameObjectListField.SetValues(data.heroObject.lists.gameObjects.items, data.objectID, 0);

            // get the hero object assigned to the game object field if field has been selected
            if (data.objectID > 0)
            {
                data.gameObject = data.heroObject.lists.gameObjects.items[data.objectID - 1].value;
            }
            else
            {
                data.gameObject = null;
            }

            return data;
        }
        /// <summary>
        /// Game Object = A game object attached to another game object in property list
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <param name="data">The data for the action field.</param>
        /// <returns>The data for the action field.</returns>
        public static T GetGameObjectInProperties<T>(T data) where T : ITargetHeroObject
        {
            // display list of game object fields in this hero object
            data.objectID = HeroField.GameObjectListField.SetValues(data.heroObject.propertiesList.properties[data.propertyID].itemProperties.gameObjects.items, data.objectID, 0);

            // get the hero object assigned to the game object field if field has been selected
            if (data.objectID > 0)
            {
                if (data.heroObject.propertiesList.properties[data.propertyID].itemProperties.gameObjects.items.Count > 0)
                {
                    data.gameObject = data.heroObject.propertiesList.properties[data.propertyID].itemProperties.gameObjects.items[data.objectID - 1].value;
                }
            }

            return data;
        }

        // --------------------------------------------------------------
        // Delete Hero Kit Object from Action Field (action field where you drag game object into slot)
        // --------------------------------------------------------------

        /// <summary>
        /// The GUID of the hero kit object in the action field (last frame)
        /// </summary>
        private static int oldGUID;
        /// <summary>
        /// The GUID of the hero kit object in the action field (this frame)
        /// </summary>
        private static int newGUID;
        /// <summary>
        /// Check to see if the GUID has changed in the action field. If it has, update the layout of this field.
        /// </summary>
        /// <typeparam name="T">The type of data for the action field.</typeparam>
        /// <param name="data">The data for the action field.</param>
        public static void DeleteGameObject<T>(T data) where T : ITargetHeroObject
        {
            newGUID = 0;
        }

        // --------------------------------------------------------------
        // General
        // --------------------------------------------------------------

        /// <summary>
        /// Save the ID of the action that is the head of the loop.
        /// </summary>
        /// <param name="actionField">The action field.</param>
        /// <returns>Data needed to use loops inside an event.</returns>
        public static LoopFieldData CreateLoopFieldData(HeroActionField actionField)
        {
            LoopFieldData data = new LoopFieldData();
            CreateActionField(ref actionField.ints, data.intSlots, 0);
            CreateActionField(ref actionField.bools, data.boolSlots, false);
            data.startLoopID = actionField.ints[0];
            data.success = actionField.bools[0];
            return data;
        }

        /// <summary>
        /// Get Audio Settings Fields. This requires the use of 12 action fields.
        /// </summary>
        /// <param name="actionParams">The action parameters.</param>
        /// <param name="heroAction">The hero action.</param>
        /// <param name="firstActionFieldID">The first action field that uses this method. (12 are used).</param>
        public static void GetAudioSettings(HeroActionParams actionParams, HeroAction heroAction, int firstActionFieldID)
        {
            // change volume
            SimpleLayout.BeginVertical(Box.StyleB);
            GetBoolValue.BuildField("Volume (0 = low, 100 = high):", actionParams, heroAction.actionFields[firstActionFieldID], true);
            if (heroAction.actionFields[firstActionFieldID].bools[0])
            {
                GetSliderValue.BuildFieldB("", actionParams, heroAction.actionFields[firstActionFieldID+1]);
            }
            SimpleLayout.EndVertical();

            // change pitch
            SimpleLayout.BeginVertical(Box.StyleB);
            GetBoolValue.BuildField("Pitch (low = -300, normal = 0, high = 300):", actionParams, heroAction.actionFields[firstActionFieldID+2], true);
            if (heroAction.actionFields[firstActionFieldID+2].bools[0])
            {
                GetSliderValue.BuildFieldB("", actionParams, heroAction.actionFields[firstActionFieldID+3], -300, 300);
            }
            SimpleLayout.EndVertical();

            // change stereo pan
            SimpleLayout.BeginVertical(Box.StyleB);
            GetBoolValue.BuildField("Stereo Pan (left = -100, middle = 0, right = 100):", actionParams, heroAction.actionFields[firstActionFieldID+4], true);
            if (heroAction.actionFields[firstActionFieldID+4].bools[0])
            {
                GetSliderValue.BuildFieldB("", actionParams, heroAction.actionFields[firstActionFieldID+5], -100);
            }
            SimpleLayout.EndVertical();

            // change spatial blending
            SimpleLayout.BeginVertical(Box.StyleB);
            GetBoolValue.BuildField("Spatial Blending (2D = 0, 3D = 100):", actionParams, heroAction.actionFields[firstActionFieldID+6], true);
            if (heroAction.actionFields[firstActionFieldID+6].bools[0])
            {
                GetSliderValue.BuildFieldB("", actionParams, heroAction.actionFields[firstActionFieldID+7]);
            }
            SimpleLayout.EndVertical();

            // change reverb zone mix
            SimpleLayout.BeginVertical(Box.StyleB);
            GetBoolValue.BuildField("Reverb Zone Mix (No Reverb = 0, Most Reverb = 100):", actionParams, heroAction.actionFields[firstActionFieldID+8], true);
            if (heroAction.actionFields[firstActionFieldID+8].bools[0])
            {
                GetSliderValue.BuildFieldB("", actionParams, heroAction.actionFields[firstActionFieldID+9]);
            }
            SimpleLayout.EndVertical();

            // use mixer
            SimpleLayout.BeginVertical(Box.StyleB);
            GetBoolValue.BuildField("Add to Audio Mixer Group?", actionParams, heroAction.actionFields[firstActionFieldID+10], true);
            if (heroAction.actionFields[firstActionFieldID+10].bools[0])
            {
                GetObjectValue.BuildField<AudioMixerGroup>("", actionParams, heroAction.actionFields[firstActionFieldID+11]);
            }
            SimpleLayout.EndVertical();
        }

        /// <summary>
        /// Get an animation to play. This requires the use of action fields 0-3.
        /// </summary>
        /// <param name="title">Title of the action field.</param>
        /// <param name="actionParams">Action parameters.</param>
        /// <param name="actionFieldA">Action field.</param>
        /// <param name="actionFieldB">Action field.</param>
        /// <param name="actionFieldC">Action field.</param>
        /// <param name="paramType">Animation controller parameter type.</param>
        /// <param name="targetHeroObject">The target hero object.</param>
        public static void GetAnimation(string title, HeroActionParams actionParams, HeroActionField actionFieldA, HeroActionField actionFieldB, HeroActionField actionFieldC, AnimatorControllerParameterType paramType, HeroObject targetHeroObject)
        {
            if (title != "")
                SimpleLayout.Label(title);

            SimpleLayout.BeginVertical(Box.StyleB);

            if (targetHeroObject == null)
            {
                SimpleLayout.Label("Select a hero object to see its animation parameters.");
                SimpleLayout.EndVertical();
                return;
            }

            if (targetHeroObject.states.states == null || targetHeroObject.states.states.Count == 0)
            {
                SimpleLayout.Label("This hero object has no states.");
                SimpleLayout.EndVertical();
                return;
            }

            SimpleLayout.Label("The state that contains the animation controller:");
            int stateID = GetDropDownBField.BuildField("", actionParams, actionFieldA, new HeroField.StateListField(), targetHeroObject.states.states) - 1;

            if (stateID >= 0)
            {
                string paramTypeName = paramType.ToString();

                SimpleLayout.Label(paramTypeName + " assigned to the animation controller:");
                RuntimeAnimatorController animatorController = GetAnimationParameterField.BuildFieldA("", actionParams, actionFieldB, targetHeroObject, stateID, paramType);

                if (animatorController == null)
                {
                    SimpleLayout.BeginVertical(Box.StyleA);
                    SimpleLayout.Label("This state has no animation controller.\nIf you are 100% certain a specific animation controller\nwill be available at runtime, drag the prefab \nthat contains the controller here:");
                    GameObject prefab = GetPrefabValue.BuildField("", actionParams, actionFieldC);
                    if (prefab != null)
                    {
                        Animator animator = prefab.GetComponent<Animator>();
                        if (animator != null)
                        {
                            animatorController = animator.runtimeAnimatorController;
                            if (animatorController != null)
                            {
                                GetAnimationParameterField.BuildFieldB("", actionParams, actionFieldB, animatorController, paramType);
                            }
                        }

                    }

                    SimpleLayout.EndVertical();
                }
            }

            SimpleLayout.EndVertical();
        }

        /// <summary>
        /// Get an animation to play. This requires the use of action fields 0-3.
        /// </summary>
        /// <param name="title">Title of the action field.</param>
        /// <param name="actionParams">Action parameters.</param>
        /// <param name="actionFieldA">Action field.</param>
        /// <param name="actionFieldB">Action field.</param>
        /// <param name="actionFieldC">Action field.</param>
        /// <param name="paramType">Animation controller parameter type.</param>
        /// <param name="targetHeroObject">The target hero object.</param>
        public static void GetAnimation_Legacy(string title, HeroActionParams actionParams, HeroActionField actionFieldA, HeroActionField actionFieldB, HeroActionField actionFieldC, HeroObject targetHeroObject)
        {
            if (title != "")
                SimpleLayout.Label(title);

            SimpleLayout.BeginVertical(Box.StyleB);

            if (targetHeroObject == null)
            {
                SimpleLayout.Label("Select a hero object to see its animations.");
                SimpleLayout.EndVertical();
                return;
            }

            if (targetHeroObject.states.states == null || targetHeroObject.states.states.Count == 0)
            {
                SimpleLayout.Label("This hero object has no states.");
                SimpleLayout.EndVertical();
                return;
            }

            SimpleLayout.Label("The state that contains the animations:");
            int stateID = GetDropDownBField.BuildField("", actionParams, actionFieldA, new HeroField.StateListField(), targetHeroObject.states.states) - 1;

            if (stateID >= 0)
            {
                SimpleLayout.Label("The animation to play:");
                Animation animation = GetAnimationParameterField.BuildFieldA_Legacy("", actionParams, actionFieldB, targetHeroObject, stateID);

                if (animation == null)
                {
                    SimpleLayout.BeginVertical(Box.StyleA);
                    SimpleLayout.Label("This state has no animation component.\nIf you are 100% certain a specific animation component\nwill be available at runtime, drag the prefab \nthat contains the component here:");
                    GameObject prefab = GetPrefabValue.BuildField("", actionParams, actionFieldC);
                    if (prefab != null)
                    {
                        animation = prefab.GetComponent<Animation>();
                        if (animation != null)
                        {
                            GetAnimationParameterField.BuildFieldB_Legacy("", actionParams, actionFieldB, animation);
                        }
                    }

                    SimpleLayout.EndVertical();
                }
            }

            SimpleLayout.EndVertical();
        }

    }

    /// <summary>
    /// Data needed to use loops inside an event (like Do While loop)
    /// </summary> 
    public struct LoopFieldData
    {
        public int intSlots
        {
            get { return 2; }
        }
        public int boolSlots
        {
            get { return 1; }
        }

        public int startLoopID;
        public int breakCount;
        public bool success;
    }
}