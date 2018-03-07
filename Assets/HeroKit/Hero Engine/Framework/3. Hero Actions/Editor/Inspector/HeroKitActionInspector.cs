// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using HeroKit.Editor;

/// <summary>
/// This is the inspector for the HeroKitAction scriptable object. 
/// This inspector is shown when you click on a Hero Action in the Project tab.
/// </summary>
[CustomEditor(typeof(HeroKitAction), true)]
public class HeroKitActionInspector : Editor
{
    /// <summary>
    /// The hero kit action that will use the inspector.
    /// </summary>
    private HeroKitAction heroKitAction;

    /// <summary>
    /// Actions to perform when the hero kit action is enabled.
    /// </summary>
    private void OnEnable()
    {
        heroKitAction = (HeroKitAction)target;
    }

    /// <summary>
    /// Display a custom inspector for the hero kit action.
    /// </summary>
    public override void OnInspectorGUI()
    {
        // show existing inspector
        base.OnInspectorGUI();

        if (GUILayout.Button("Edit Hero Action"))
        {
            HeroKitEditor.ButtonClickHeroActionAsset(heroKitAction);
        }
    }
}