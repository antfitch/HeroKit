// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionBlockFields;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with a prefab.
    /// </summary>
    public static class GetPrefabValue
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get a prefab.
        /// </summary>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>The prefab.</returns>
        public static GameObject BuildField(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            // create the fields
            PrefabValueData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the prefab you want to work with.
            //-----------------------------------------
            data.fieldValue = SimpleLayout.ObjectField(data.fieldValue, 300);

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.gameObjects[0] = data.fieldValue;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            // return result
            return actionField.gameObjects[0];
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
        private static PrefabValueData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            PrefabValueData data = new PrefabValueData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldValue = actionField.gameObjects[0];

            return data;
        }
    }

    /// <summary>
    /// Data needed for GetPrefabValue.
    /// </summary>
    public struct PrefabValueData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.gameObjects, 1, null);
        }

        public string title { get; set; }
        public GameObject fieldValue;
    }

}