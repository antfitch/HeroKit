// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;
using System.Collections.Generic;
using System.Linq;
using HeroKit.Editor.ActionField;
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor
{
    /// <summary>
    /// Block for Hero State that appears in Hero Kit Editor.
    /// </summary>
    internal class StateBlock
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The hero object.
        /// </summary>
        private static HeroObject heroObject;
        /// <summary>
        /// Name of the block.
        /// </summary>
        private static string blockName = "State";
        /// <summary>
        /// The Hero State.
        /// </summary>
        private static HeroState stateBlock;
        /// <summary>
        /// The ID of the state.
        /// </summary>
        private static int stateIndex;
        /// <summary>
        /// The preview pane for the game object
        /// </summary>
        private static UnityEditor.Editor gameObjectEditor;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Block to display on the canvas.
        /// </summary>
        /// <param name="heroKitAction">Hero kit action to display in the block.</param>
        /// <param name="indexState">ID of the state.</param>
        public static void Block(HeroObject heroKitObject, int indexState)
        {
            // exit early if object is null
            if (heroKitObject == null) return;

            // exit early if state no longer exists
            if (heroKitObject.states.states == null || heroKitObject.states.states.Count - 1 < indexState) return;

            // save the id of the state that this event belongs in
            stateIndex = indexState;

            // assign hero object to this class
            heroObject = heroKitObject;
            stateBlock = heroObject.states.states[stateIndex];

            // draw components
            DrawBlock();

            // refresh hero kit objects in scene if state info has changed
            if (GUI.changed)
            {
                Scene.HeroKitCommonRuntime.RefreshAllVisuals(heroObject);
            }
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            // draw heading for block
            DrawHeader();

            // draw body for block
            DrawBody();
        }
        /// <summary>
        /// Draw the header of the block.
        /// </summary>
        private static void DrawHeader()
        {
            HeroKitCommon.DrawBlockTitle(blockName + " " + stateIndex + ": " + stateBlock.name);
        }
        /// <summary>
        /// Draw the body of the block.
        /// </summary>
        private static void DrawBody()
        {
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);

            // draw name of the block
            stateBlock.name = HeroKitCommon.DrawBlockName(stateBlock.name);

            SimpleLayout.Line();
            SimpleLayout.Space(5);
            
            // draw the conditions for the block
            DrawIntConditionFields();
            DrawBoolConditionFields();

            SimpleLayout.Space(5);
            SimpleLayout.Line();
            SimpleLayout.Space(5);

            // draw the image for the block
            DrawVisualsTypeForState();

            SimpleLayout.EndVertical();
        }

        // --------------------------------------------------------------
        // Methods (Conditional Fields)
        // --------------------------------------------------------------

        /// <summary>
        /// Integer conditions that must be satisfied for the state to run.
        /// </summary>
        private static void DrawIntConditionFields()
        {
            SimpleLayout.BeginVertical(Box.StyleB);

            // ROW: START -----------------------------------------
            SimpleLayout.BeginHorizontal();
            // COLUMN 1: START ------------------------------------
            SimpleLayout.BeginVertical();
            SimpleLayout.Label("INTEGERS (these conditions must be met before this state can run):");
            SimpleLayout.EndVertical();
            // COLUMN 1: END --------------------------------------
            SimpleLayout.Space();
            // COLUMN 2: START ------------------------------------
            SimpleLayout.BeginVertical();
            SimpleLayout.Button(Content.AddIcon, addIntCondition, Button.StyleA, 25);
            SimpleLayout.EndVertical();
            // COLUMN 2: END --------------------------------------
            SimpleLayout.EndHorizontal();
            // ROW: END -------------------------------------------

            if (stateBlock.intConditions.Count > 0)
                SimpleLayout.Line();

            // List of entries
            for (int i = 0; i < stateBlock.intConditions.Count; i++)
            {
                // ROW 1: START ---------------------------------------
                SimpleLayout.BeginHorizontal();

                // COLUMN 1: START ------------------------------------
                SimpleLayout.BeginVertical();

                // VALUE 1
                SimpleLayout.BeginHorizontal();
                GetIntegerField.BuildEventField("Value 1:", stateBlock.intConditions[i].itemA, heroObject);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // OPERATOR
                SimpleLayout.BeginHorizontal();
                SimpleLayout.Space(60);
                stateBlock.intConditions[i].operatorID = new OperatorField().SetValues(stateBlock.intConditions[i].operatorID, 0);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                //SimpleLayout.Space(4);

                // VALUE 2
                SimpleLayout.BeginHorizontal();
                GetIntegerField.BuildEventField("Value 2:", stateBlock.intConditions[i].itemB, heroObject);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                SimpleLayout.EndVertical();
                // COLUMN 1: END --------------------------------------

                // COLUMN 2: START ------------------------------------
                SimpleLayout.BeginVertical();
                SimpleLayout.Button(Content.DeleteIcon, deleteIntCondition, i, Button.StyleA, 25);
                SimpleLayout.EndVertical();
                // COLUMN 2: END --------------------------------------

                SimpleLayout.EndHorizontal();
                // ROW 1: END -----------------------------------------

                // ROW 2: START ---------------------------------------
                if (i < stateBlock.intConditions.Count - 1)
                    SimpleLayout.Line();
                // ROW 2: END -----------------------------------------

            }

            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Add a condition to the integer condition list.
        /// </summary>
        private static void addIntCondition()
        {
            HeroKitCommon.deselectField();
            stateBlock.intConditions.Add(new ConditionIntFields());
        }
        /// <summary>
        /// Delete a condition from the integer condition list. 
        /// </summary>
        /// <param name="index"></param>
        private static void deleteIntCondition(int index)
        {
            if (stateBlock.intConditions.Count > index)
            {
                HeroKitCommon.deselectField();
                stateBlock.intConditions.RemoveAt(index);
            }
        }

        /// <summary>
        /// Bool conditions that must be satisfied for the state to run.
        /// </summary>
        private static void DrawBoolConditionFields()
        {
            SimpleLayout.BeginVertical(Box.StyleB);

            // ROW: START -----------------------------------------
            SimpleLayout.BeginHorizontal();
            // COLUMN 1: START ------------------------------------
            SimpleLayout.BeginVertical();
            SimpleLayout.Label("BOOLS (these conditions must be met before this state can run):");
            SimpleLayout.EndVertical();
            // COLUMN 1: END --------------------------------------
            SimpleLayout.Space();
            // COLUMN 2: START ------------------------------------
            SimpleLayout.BeginVertical();
            SimpleLayout.Button(Content.AddIcon, addBoolCondition, Button.StyleA, 25);
            SimpleLayout.EndVertical();
            // COLUMN 2: END --------------------------------------
            SimpleLayout.EndHorizontal();
            // ROW: END -------------------------------------------

            if (stateBlock.boolConditions.Count > 0)
                SimpleLayout.Line();

            // List of entries
            for (int i = 0; i < stateBlock.boolConditions.Count; i++)
            {
                // ROW 1: START ---------------------------------------
                SimpleLayout.BeginHorizontal();

                // COLUMN 1: START ------------------------------------
                SimpleLayout.BeginVertical();

                // VALUE 1
                SimpleLayout.BeginHorizontal();
                GetBoolField.BuildEventField("Value 1:", stateBlock.boolConditions[i].itemA, heroObject);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();

                // OPERATOR
                SimpleLayout.BeginHorizontal();
                SimpleLayout.Space(60);
                stateBlock.boolConditions[i].operatorID = new TrueFalseField().SetValues(stateBlock.boolConditions[i].operatorID, 0);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                //SimpleLayout.Space(4);

                // VALUE 2
                SimpleLayout.BeginHorizontal();
                GetBoolField.BuildEventField("Value 2:", stateBlock.boolConditions[i].itemB, heroObject);
                SimpleLayout.Space();
                SimpleLayout.EndHorizontal();
                SimpleLayout.EndVertical();
                // COLUMN 1: END --------------------------------------

                // COLUMN 2: START ------------------------------------
                SimpleLayout.BeginVertical();
                SimpleLayout.Button(Content.DeleteIcon, deleteBoolCondition, i, Button.StyleA, 25);
                SimpleLayout.EndVertical();
                // COLUMN 2: END --------------------------------------

                SimpleLayout.EndHorizontal();
                // ROW 1: END -----------------------------------------

                // ROW 2: START ---------------------------------------
                if (i < stateBlock.boolConditions.Count - 1)
                    SimpleLayout.Line();
                // ROW 2: END -----------------------------------------

            }

            SimpleLayout.EndVertical();
        }
        /// <summary>
        /// Add a condition to the bool condition list.
        /// </summary>
        private static void addBoolCondition()
        {
            HeroKitCommon.deselectField();
            stateBlock.boolConditions.Add(new ConditionBoolFields());
        }
        /// <summary>
        /// Delete a condition from the bool condition list. 
        /// </summary>
        /// <param name="index"></param>
        private static void deleteBoolCondition(int index)
        {
            if (stateBlock.boolConditions.Count > index)
            {
                HeroKitCommon.deselectField();
                stateBlock.boolConditions.RemoveAt(index);
            }
        }

        // --------------------------------------------------------------
        // Methods (Visual Fields)
        // --------------------------------------------------------------
        
        /// <summary>
        /// Choose the type of visuals to use for this state.
        /// </summary>
        private static void DrawVisualsTypeForState()
        {
            // Visual Type
            SimpleLayout.Label("Visuals type:");
            stateBlock.heroVisuals.visualType = new VisualTypeField().SetValues(stateBlock.heroVisuals.visualType, 0);

            SimpleLayout.Space(5);
            SimpleLayout.Line();
            SimpleLayout.Space(5);

            switch (stateBlock.heroVisuals.visualType)
            {
                case 0: // not set
                case 1: // 3D
                    Draw3DForState();
                    break;
                case 2: // 2D
                    Draw2DForState();
                    break;
            }
        }
        /// <summary>
        /// Choose the type of 3D visuals to use for this state.
        /// </summary>
        private static void Draw3DForState()
        {
            // Image Type
            SimpleLayout.Label("Visuals for the object:");
            stateBlock.heroVisuals.imageType = new ImageTypeField().SetValues(stateBlock.heroVisuals.imageType, 0);

            switch (stateBlock.heroVisuals.imageType)
            {
                case 0: // not set
                    break;
                case 1: // image = use whatever is already on the game object                  
                    break;
                case 2: // image = get it from a prefab
                    DrawPrefabForState();
                    break;
                case 3: // image = none
                    DrawNoVisualsForState();
                    break;
            }
        }
        /// <summary>
        /// Choose the type of 2D visuals to use for this state.
        /// </summary>
        private static void Draw2DForState()
        {
            // Image Type
            SimpleLayout.Label("Visuals for the object:");
            stateBlock.heroVisuals.imageType = new ImageTypeField().SetValues(stateBlock.heroVisuals.imageType, 0);

            switch (stateBlock.heroVisuals.imageType)
            {
                case 0: // not set
                    break;
                case 1: // image = use whatever is already on the game object                  
                    break;
                case 2: // image = get it from a prefab
                    DrawPrefabForState(true);
                    break;
                case 3: // image = none
                    DrawNoVisualsForState();
                    break;
            }
        }
        /// <summary>
        /// If visuals type was prefab, draw the prefab preview.
        /// </summary>
        /// <param name="is2D">If we are working with 2D visuals, set this to true.</param>
        private static void DrawPrefabForState(bool is2D = false)
        {
            SimpleLayout.BeginVertical(Box.StyleB);

            stateBlock.heroVisuals.hasMesh = false;
            stateBlock.heroVisuals.imageMesh = null;
            stateBlock.heroVisuals.hasAnimator = false;
            stateBlock.heroVisuals.animator = null;
            stateBlock.heroVisuals.animatorController = null;
            stateBlock.heroVisuals.avatar = null;

            // display the prefab
            SimpleLayout.Label("Prefab:");
            stateBlock.heroVisuals.prefab = SimpleLayout.ObjectField(stateBlock.heroVisuals.prefab);
            if (stateBlock.heroVisuals.prefab != null && PrefabUtility.GetPrefabType(stateBlock.heroVisuals.prefab) != PrefabType.Prefab)
            {
                stateBlock.heroVisuals.prefab = null;
            }
            if (stateBlock.heroVisuals.prefab != null)
            {
                // attach mesh to hero kit object if it exists
                MeshFilter meshFilter = stateBlock.heroVisuals.prefab.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    stateBlock.heroVisuals.hasMesh = true;
                    stateBlock.heroVisuals.imageMesh = meshFilter.sharedMesh;
                }                

                // attach animator to hero kit object if it exists
                Animator animator = stateBlock.heroVisuals.prefab.GetComponent<Animator>();
                if (animator != null)
                {
                    stateBlock.heroVisuals.hasAnimator = true;
                    stateBlock.heroVisuals.animator = animator;
                    stateBlock.heroVisuals.animatorController = animator.runtimeAnimatorController;
                    stateBlock.heroVisuals.avatar = animator.avatar;
                }
            }
            SimpleLayout.EndVertical();

            // attach rigidbody
            DrawRigidbodyForState(is2D);

            // display preview
            DrawVisualPreview(stateBlock.heroVisuals.prefab);
        }
        /// <summary>
        /// If visuals type was no prefab, hide prefab preview.
        /// </summary>
        private static void DrawNoVisualsForState()
        {
            stateBlock.heroVisuals.hasMesh = false;
            stateBlock.heroVisuals.imageMesh = null;
            stateBlock.heroVisuals.hasAnimator = false;
            stateBlock.heroVisuals.animator = null;
            stateBlock.heroVisuals.animatorController = null;
            stateBlock.heroVisuals.avatar = null;
            stateBlock.heroVisuals.prefab = null;
            stateBlock.heroVisuals.rigidbody = null;
            stateBlock.heroVisuals.rigidbody2D = null;
        }        
        /// <summary>
        /// Draw the visual preview pane for a prefab.
        /// </summary>
        /// <param name="prefab">The prefab.</param>
        private static void DrawVisualPreview(GameObject prefab)
        {
            bool showPreview = true;

            // Unity 2017 will not display the preview window with OnPreviewGUI. A ticket has been submitted to them.
            //#if UNITY_2017
                //showPreview = false;
            //#endif

            // exit early if preview should not be visible
            if (!showPreview || prefab == null) return;           

            SimpleLayout.BeginVertical(Box.StyleB);
            SimpleLayout.Label("Preview:");
            gameObjectEditor = UnityEditor.Editor.CreateEditor(prefab);
            gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(200, 200), Box.StyleImagePreview);
            SimpleLayout.EndVertical();
            Object.DestroyImmediate(gameObjectEditor);
        }
        /// <summary>
        /// Choose the rigidbody for object in this state.
        /// </summary>
        /// <param name="is2D">If we are working with 2D visuals, set this to true.</param>
        private static void DrawRigidbodyForState(bool is2D = false)
        {
            // display the collider type
            SimpleLayout.BeginVertical(Box.StyleB);
            string rigidBodyName = (is2D) ? "2D" : "3D";
            SimpleLayout.Label("Rigidbody for " + rigidBodyName + " Object (drag prefab that contains the rigidbody here):");

            string[] rigidbodyOptions = null;
            // 3d rigidbody options
            if (!is2D)
                rigidbodyOptions = new string[] { "Default Rigidbody", "Custom Rigidbody", "No Rigidbody", "Heavy Rigidbody"};
            // 2d rigidbody options
            else
                rigidbodyOptions = new string[] { "Platformer Rigidbody", "RPG Rigidbody", "Custom Rigidbody",
                                                  "No Rigidbody", "Platformer Heavy Rigidbody", "RPG Heavy Rigidbody"};

            stateBlock.heroVisuals.rigidbodyType = new GenericListField(rigidbodyOptions).SetValues(stateBlock.heroVisuals.rigidbodyType, 0);

            // draw 3D rigidbody
            if (!is2D)
            {
                switch (stateBlock.heroVisuals.rigidbodyType)
                {
                    case 0: // not set
                        break;
                    case 1: // use default rigidbody 
                        GameObject template = Resources.Load<GameObject>("Hero Templates/Components/" + "HeroKit 3D Rigidbody");
                        stateBlock.heroVisuals.rigidbody = (template != null) ? template.GetComponent<Rigidbody>() : null;
                        break;
                    case 2: // use custom rigidbody
                        stateBlock.heroVisuals.rigidbody = SimpleLayout.ObjectField(stateBlock.heroVisuals.rigidbody);
                        break;
                    case 3: // no rigidbody
                        stateBlock.heroVisuals.rigidbody = null;
                        break;
                    case 4: // heavy rigidbody
                        GameObject templateHeavy = Resources.Load<GameObject>("Hero Templates/Components/" + "HeroKit 3D Heavy Rigidbody");
                        stateBlock.heroVisuals.rigidbody = (templateHeavy != null) ? templateHeavy.GetComponent<Rigidbody>() : null;
                        break;
                }
            }

            // draw 2D rigidbody
            else
            {
                switch (stateBlock.heroVisuals.rigidbodyType)
                {
                    case 0: // not set
                        break;
                    case 1: // use platformer rigidbody 
                        GameObject template = Resources.Load<GameObject>("Hero Templates/Components/" + "HeroKit 2D Platformer Rigidbody");
                        stateBlock.heroVisuals.rigidbody2D = (template != null) ? template.GetComponent<Rigidbody2D>() : null;
                        break;
                    case 2: // use rpg rigidbody
                        GameObject templateRPG = Resources.Load<GameObject>("Hero Templates/Components/" + "HeroKit 2D RPG Rigidbody");
                        stateBlock.heroVisuals.rigidbody2D = (templateRPG != null) ? templateRPG.GetComponent<Rigidbody2D>() : null;
                        break;
                    case 3: // use custom rigidbody
                        stateBlock.heroVisuals.rigidbody2D = SimpleLayout.ObjectField(stateBlock.heroVisuals.rigidbody2D);
                        break;
                    case 4: // no rigidbody
                        stateBlock.heroVisuals.rigidbody2D = null;
                        break;
                    case 5: // use platformer heavy rigidbody
                        GameObject templateHeavy = Resources.Load<GameObject>("Hero Templates/Components/" + "HeroKit 2D Heavy Platformer Rigidbody");
                        stateBlock.heroVisuals.rigidbody2D = (templateHeavy != null) ? templateHeavy.GetComponent<Rigidbody2D>() : null;
                        break;
                    case 6: // use rpg heavy rigidbody
                        GameObject templateRPGHeavy = Resources.Load<GameObject>("Hero Templates/Components/" + "HeroKit 2D Heavy RPG Rigidbody");
                        stateBlock.heroVisuals.rigidbody2D = (templateRPGHeavy != null) ? templateRPGHeavy.GetComponent<Rigidbody2D>() : null;
                        break;
                }
            }

            SimpleLayout.EndVertical();
        }
    }
}