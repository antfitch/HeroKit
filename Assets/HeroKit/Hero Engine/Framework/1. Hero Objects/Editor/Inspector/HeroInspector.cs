// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using HeroKit.Editor;

/// <summary>
/// This is the inspector for the HeroObject scriptable object. 
/// This inspector is shown when you click on a hero object in the Project tab.
/// </summary>
[CustomEditor(typeof(HeroObject), true)]
public class HeroInspector : Editor
{
    /// <summary>
    /// The hero object assigned to the inspector.
    /// </summary>
    private HeroObject heroObject;
    /// <summary>
    /// Show details about this hero object?
    /// </summary>
    private bool showDetails;

    /// <summary>
    /// Assign currently open hero object to inspector.
    /// </summary>
    private void OnEnable()
    {
        heroObject = (HeroObject)target;
    }

    /// <summary>
    /// Show a button to open custom editor.
    /// </summary>
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Edit Hero Object"))
        {
            HeroKitEditor.ButtonClickHeroObjectAsset(heroObject);
        }

        if (GUILayout.Button("Show Details"))
        {
            ToggleDetails();
        }

        // show existing inspector
        if (showDetails)
            base.OnInspectorGUI();
    }

    /// <summary>
    /// Show or hide the details about a hero object.
    /// </summary>
    private void ToggleDetails()
    {
        showDetails = !showDetails;
    }
}