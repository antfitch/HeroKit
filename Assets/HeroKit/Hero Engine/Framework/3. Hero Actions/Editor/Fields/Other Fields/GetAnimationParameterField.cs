// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene;
using SimpleGUI;
using System.Collections.Generic;
using HeroKit.Editor.ActionBlockFields;
using UnityEditor.Animations;
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with parameters on an animation controller.
    /// </summary>
    public static class GetAnimationParameterField
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get an animation controller.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="heroObject">Hero object that is the target of this action.</param>
        /// <param name="stateID">ID of the state that contains the animation controller.</param>
        /// <param name="paramType">Animation controller parameter type.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>The animation controller.</returns>
        public static RuntimeAnimatorController BuildFieldA(string title, HeroActionParams actionParams, HeroActionField actionField, HeroObject heroObject, int stateID, AnimatorControllerParameterType paramType, bool titleToLeft = false)
        {
            // create the fields
            AnimationParameterFieldData data = CreateFieldData(title, actionField);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the animation controller we are working with 
            //-----------------------------------------   
            RuntimeAnimatorController animatorController = heroObject.states.states[stateID].heroVisuals.animatorController;
            if (animatorController == null)
            {
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                return null;
            }

            //-----------------------------------------
            // Get the triggers we want to work with 
            //----------------------------------------- 
            List<string> items = GetItemsFromList(animatorController, paramType);
            data = BuildItemFieldList(data, items);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[0] = data.fieldID;
            actionField.strings[0] = data.fieldName;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return animatorController;
        }
        /// <summary>
        /// Set an animation controller.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="animatorController">The animation controller.</param>
        /// <param name="paramType">Animation controller parameter type.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>The animation controller.</returns>
        public static RuntimeAnimatorController BuildFieldB(string title, HeroActionParams actionParams, HeroActionField actionField, RuntimeAnimatorController animatorController, AnimatorControllerParameterType paramType, bool titleToLeft = false)
        {
            // create the fields
            AnimationParameterFieldData data = CreateFieldData(title, actionField);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the animation controller we are working with 
            //-----------------------------------------   
            if (animatorController == null)
            {
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                return null;
            }

            //-----------------------------------------
            // Get the triggers we want to work with 
            //----------------------------------------- 
            List<string> items = GetItemsFromList(animatorController, paramType);
            data = BuildItemFieldList(data, items);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[0] = data.fieldID;
            actionField.strings[0] = data.fieldName;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return animatorController;
        }

        /// <summary>
        /// Get an animation from a legacy animation component.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="heroObject">Hero object that is the target of this action.</param>
        /// <param name="stateID">ID of the state that contains the animation controller.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>The animation controller.</returns>
        public static Animation BuildFieldA_Legacy(string title, HeroActionParams actionParams, HeroActionField actionField, HeroObject heroObject, int stateID, bool titleToLeft = false)
        {
            // create the fields
            AnimationParameterFieldData data = CreateFieldData(title, actionField);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the animation controller we are working with 
            //-----------------------------------------   
            Animation animation = null;
            if (heroObject.states.states[stateID].heroVisuals.prefab != null)
            {
                animation = heroObject.states.states[stateID].heroVisuals.prefab.GetComponent<Animation>();
            }
       
            if (animation == null)
            {
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                return null;
            }

            //-----------------------------------------
            // Get the triggers we want to work with 
            //----------------------------------------- 
            List<string> items = GetItemsFromList_Legacy(animation);
            data = BuildItemFieldList(data, items);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[0] = data.fieldID;
            actionField.strings[0] = data.fieldName;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return animation;
        }
        /// <summary>
        /// Set an animation component.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="animation">The animation component.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>The animation component.</returns>
        public static Animation BuildFieldB_Legacy(string title, HeroActionParams actionParams, HeroActionField actionField, Animation animation, bool titleToLeft = false)
        {
            // create the fields
            AnimationParameterFieldData data = CreateFieldData(title, actionField);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the animation controller we are working with 
            //-----------------------------------------   
            if (animation == null)
            {
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                return null;
            }

            //-----------------------------------------
            // Get the triggers we want to work with 
            //----------------------------------------- 
            List<string> items = GetItemsFromList_Legacy(animation);
            data = BuildItemFieldList(data, items);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.ints[0] = data.fieldID;
            actionField.strings[0] = data.fieldName;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return animation;
        }


        // --------------------------------------------------------------
        // Helpers
        // --------------------------------------------------------------

        /// <summary>
        /// Get a list of of animation controller parameter names.
        /// </summary>
        /// <param name="animatorController">The animation controller.</param>
        /// <param name="paramType">The parameter type.</param>
        /// <returns>Animation controller parameter names.</returns>
        private static List<string> GetItemsFromList(RuntimeAnimatorController animatorController, AnimatorControllerParameterType paramType)
        {
            List<string> items = new List<string>();
            AnimatorController ac;

            // the runtime animation controller can be either an animator controller or an animator override controller. 
            // use try catch to figure out which one was passed in.
            try
            {
                ac = (AnimatorController)animatorController;
            }
            catch
            {
                AnimatorOverrideController o2 = (AnimatorOverrideController)animatorController;
                ac = (AnimatorController)o2.runtimeAnimatorController;
            }

            if (ac != null)
            {
                AnimatorControllerParameter[] parameters = ac.parameters;
                if (ac.parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i].type == paramType)
                        {
                            items.Add(parameters[i].name);
                        }
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Get a list of of animations in a legacy animation component.
        /// </summary>
        /// <param name="animation">The animation component.</param>
        /// <returns>Animation names.</returns>
        private static List<string> GetItemsFromList_Legacy(Animation animation)
        {
            List<string> items = new List<string>();

            if (animation != null)
            {
                foreach (AnimationState state in animation)
                {
                    items.Add(state.name);
                }
            }

            return items;
        }

        /// <summary>
        /// Create a list of animation controller parameters.
        /// </summary>
        /// <param name="data">Current data for this action field.</param>
        /// <param name="items">A list of animation controller parameter names.</param>
        /// <returns></returns>
        private static AnimationParameterFieldData BuildItemFieldList(AnimationParameterFieldData data, List<string> items)
        {
            data.fieldID = new GenericListField(items.ToArray()).SetValues(data.fieldID, 0);

            // if a field is selected, get the name of the field
            int itemID = data.fieldID - 1;
            if (itemID >= 0 && items != null && items.Count > itemID)
            {
                data.fieldName = items[itemID];
            }           

            return data;
        }

        // --------------------------------------------------------------
        // Initialize Action Field
        // --------------------------------------------------------------

        /// <summary>
        /// Create the subfields that we need for this action field.
        /// </summary>
        /// <param name="title">The title of the action.</param>
        /// <param name="actionField">The action field.</param>
        /// <returns>The data for this action field.</returns>
        private static AnimationParameterFieldData CreateFieldData(string title, HeroActionField actionField)
        {
            AnimationParameterFieldData data = new AnimationParameterFieldData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldID = actionField.ints[0];
            data.fieldName = actionField.strings[0];
            return data;
        }
    }

    /// <summary>
    /// Data needed to use GetAnimationTriggerField.
    /// </summary>
    public struct AnimationParameterFieldData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 1, 0);
            ActionCommon.CreateActionField(ref actionField.strings, 1, "");
        }

        public string title { get; set; }
        public int fieldID { get; set; }
        public string fieldName { get; set; }
    }
}