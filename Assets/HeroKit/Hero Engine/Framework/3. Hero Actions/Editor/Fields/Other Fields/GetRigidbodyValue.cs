// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionBlockFields;
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor.ActionField
{
    /// <summary>
    /// Action field for the hero kit editor. Work with an object.
    /// </summary>
    public static class GetRigidbodyValue
    {
        // --------------------------------------------------------------
        // Action Fields
        // --------------------------------------------------------------

        /// <summary>
        /// Get an object.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="title">Title for action field.</param>
        /// <param name="actionParams">Action field parameters.</param>
        /// <param name="actionField">Action field.</param>
        /// <param name="titleToLeft">Show the title on the left?</param>
        /// <returns>The object.</returns>
        public static Rigidbody BuildField(string title, HeroActionParams actionParams, HeroActionField actionField, bool titleToLeft = false)
        {
            // create the fields
            GetRigidbodyValueData data = CreateFieldData(title, actionField, actionParams.heroObject);

            //-----------------------------------------
            // Display this title above the field
            //-----------------------------------------
            if (data.title != "" && !titleToLeft) SimpleLayout.Label(data.title);
            SimpleLayout.BeginHorizontal();
            if (data.title != "" && titleToLeft) SimpleLayout.Label(data.title);

            //-----------------------------------------
            // Get the object you want to work with
            //-----------------------------------------
            string[] rigidbodyOptions = { "Default Rigidbody", "Custom Rigidbody", "No Rigidbody", "Heavy Rigidbody" };
            data.rigidbodyType = new GenericListField(rigidbodyOptions).SetValues(data.rigidbodyType, 0);
            data.fieldValue = GetRigidbody(data.rigidbodyType);

            //data.fieldValue = SimpleLayout.ObjectField(data.fieldValue as Rigidbody, HeroKitCommon.GetWidthForField(60));

            //-----------------------------------------
            // assign values back to hero object fields
            //-----------------------------------------
            actionField.component = data.fieldValue;
            actionField.ints[0] = data.rigidbodyType;

            //-----------------------------------------
            // Visual stuff
            //-----------------------------------------
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();

            return actionField.component as Rigidbody;
        }

        /// <summary>
        /// Choose the rigidbody for object in this state.
        /// </summary>
        private static Rigidbody GetRigidbody(int rigidbodyType)
        {
            Rigidbody rigidbody = null;

            switch (rigidbodyType)
            {
                case 0: // not set
                    break;
                case 1: // use default rigidbody 
                    GameObject template = Resources.Load<GameObject>("Hero Templates/Components/" + "HeroKit 3D Rigidbody");
                    rigidbody = (template != null) ? template.GetComponent<Rigidbody>() : null;
                    break;
                case 2: // use custom rigidbody
                    rigidbody = SimpleLayout.ObjectField(rigidbody);
                    break;
                case 3: // no rigidbody
                    rigidbody = null;
                    break;
                case 4: // heavy rigidbody
                    GameObject templateHeavy = Resources.Load<GameObject>("Hero Templates/Components/" + "HeroKit 3D Heavy Rigidbody");
                    rigidbody = (templateHeavy != null) ? templateHeavy.GetComponent<Rigidbody>() : null;
                    break;
            }

            return rigidbody;
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
        private static GetRigidbodyValueData CreateFieldData(string title, HeroActionField actionField, HeroObject heroObject)
        {
            GetRigidbodyValueData data = new GetRigidbodyValueData();
            data.Init(ref actionField);
            data.title = title;
            data.fieldValue = actionField.component as Rigidbody;
            data.rigidbodyType = actionField.ints[0];

            return data;
        }
    }

    /// <summary>
    /// Data needed for GetObjectValue
    /// </summary>
    public struct GetRigidbodyValueData : ITitle
    {
        public void Init(ref HeroActionField actionField)
        {
            ActionCommon.CreateActionField(ref actionField.ints, 1, 0);
        }

        public string title { get; set; }
        public Rigidbody fieldValue;
        public int rigidbodyType;
    }
}