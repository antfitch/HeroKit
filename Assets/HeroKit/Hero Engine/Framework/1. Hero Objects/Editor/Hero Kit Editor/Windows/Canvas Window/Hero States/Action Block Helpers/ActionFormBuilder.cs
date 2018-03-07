// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene;
using System.Collections.Generic;
using System;
using System.Reflection; 

namespace HeroKit.Editor
{
    /// <summary>
    /// This scripts builds the form for an action.
    /// A form contains fields.
    /// The user adds data to these fields in a Hero Object in the Hero Kit Editor.
    /// Note: Using reflection. Delegates might be faster than reflection.
    /// </summary>
    public static class ActionBlockBuilder
    {
        /// <summary>
        /// Build the fields in the form.
        /// </summary>
        /// <param name="heroObject">The hero object that contains the action.</param>
        /// <param name="heroAction">The action on the hero object.</param>
        /// <param name="template">The action form.</param>
        /// <param name="oldTemplate">The last action form loaded by this script.</param>
        public static void BuildFields(HeroObject heroObject, HeroAction heroAction, HeroKitAction template, HeroKitAction oldTemplate)
        {
            // exit early if template does not exist
            if (template == null)
            {
                Debug.Log("template does not exist.");
                return;
            }

            if (template.actionFields == null)
            {
                Debug.Log("action fields for " + template.name + " do not exist.");
                return;
            }

            // if the hero action attached to the hero object has changed, reset the fields for the hero action on the hero object
            if (template != oldTemplate)
            {
                heroAction.actionFields = new List<HeroActionField>();
            }

            // get the name of the class to use
            Type type = Type.GetType("HeroKit.Editor.ActionBlockFields." + template.actionFields.name);

            // if class exists, get the BuildField method inside the class
            if (type != null)
            {
                // get the name of the method to use
                MethodInfo method = type.GetMethod("BuildField");

                // if method exists, invoke it
                if (method != null)
                {
                    // get the parameters to pass into the method
                    HeroActionParams actionParams = new HeroActionParams(heroObject, heroAction);

                    // create a delegate to represent the method (note: 300x faster than method.Invoke)
                    Action<HeroActionParams> buildForm = (Action<HeroActionParams>)Delegate.CreateDelegate(typeof(Action<HeroActionParams>), method);

                    // execute the delegate
                    buildForm(actionParams);
                }
            }
        }
    }
}