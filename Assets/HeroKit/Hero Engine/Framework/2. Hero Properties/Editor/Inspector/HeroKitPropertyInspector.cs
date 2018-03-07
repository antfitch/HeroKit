// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using HeroKit.Editor;

/// <summary>
/// This is the inspector for the HeroKitProperty scriptable object. 
/// This inspector is shown when you click on a Hero Property in the Project tab.
/// </summary>

[CustomEditor(typeof(HeroKitProperty), true)]
public class HeroKitPropertyInspector : Editor
{
    /// <summary>
    /// The hero kit property that will use the inspector.
    /// </summary>
    private HeroKitProperty heroKitProperty;

    /// <summary>
    /// Actions to perform when the hero kit property is enabled.
    /// </summary>
    private void OnEnable()
    {
        heroKitProperty = (HeroKitProperty)target;
    }

    /// <summary>
    /// Display a custom inspector for the hero kit property.
    /// </summary>
    public override void OnInspectorGUI()
    {
        // show existing inspector
        base.OnInspectorGUI();

        if (GUILayout.Button("Edit Hero Property"))
        {
            HeroKitEditor.ButtonClickHeroPropertyAsset(heroKitProperty);
        }
    }
}