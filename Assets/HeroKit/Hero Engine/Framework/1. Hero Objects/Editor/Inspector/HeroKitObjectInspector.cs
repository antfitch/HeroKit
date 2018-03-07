// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using HeroKit.Scene;
using HeroKit.Editor;
using System;
using SimpleGUI;

/// <summary>
/// This is the inspector for the HeroKitObject script. 
/// This inspector is shown when you click on a game object 
/// in the scene that contains the HeroKitObject script.
/// </summary>
[CustomEditor(typeof(HeroKitObject))]
public class HeroKitObjectInspector : Editor
{
    /// <summary>
    /// The hero kit object that will use this inspector.
    /// </summary>
    private HeroKitObject heroKitObject;
    /// <summary>
    /// Show details about this hero kit object?
    /// </summary>
    private bool showDetails;

    /// <summary>
    /// Actions to perform when the hero kit object is enabled.
    /// </summary>
    private void OnEnable()
    {
        // get template
        heroKitObject = (HeroKitObject)target;

        // generate new guid when you create a new game object
        if (heroKitObject.heroGUID == 0) heroKitObject.heroGUID = HeroKit.Scene.HeroKitCommonRuntime.GetHeroGUID();

        // generate new guid if guid already exists
        if (heroKitObject.heroGUID != 0)
        {
            HeroKitObject[] heroKitObjects = Array.ConvertAll(FindObjectsOfType(typeof(HeroKitObject)), x => x as HeroKitObject);
            int idCount = 0;
            for (int i = 0; i < heroKitObjects.Length; i++)
            {
                if (heroKitObject.heroGUID == heroKitObjects[i].heroGUID)
                    idCount++;
            }
            if (idCount > 1) heroKitObject.heroGUID = HeroKit.Scene.HeroKitCommonRuntime.GetHeroGUID();
        }

        if (!Application.isPlaying)
            HeroKit.Scene.HeroKitCommonRuntime.RefreshVisuals(heroKitObject);
    }

    /// <summary>
    /// Display a custom inspector for the hero kit object.
    /// </summary>
    public override void OnInspectorGUI()
    {
        HeroObject oldHeroObject = heroKitObject.heroObject;
        heroKitObject.heroObject = SimpleLayout.ObjectField("Hero Object", 100, heroKitObject.heroObject, 200, false);
        HeroObject newHeroObject = heroKitObject.heroObject;

        // has hero object changed? if yes, refresh graphics
        if (newHeroObject != oldHeroObject)
            HeroKit.Scene.HeroKitCommonRuntime.RefreshVisuals(heroKitObject);

        if (heroKitObject.heroObject == null)
        {
            SimpleLayout.BeginVertical(EditorStyles.helpBox);
            SimpleLayout.Label("Drag the hero object you want to use from the Project Tab into the field above. Or you can choose an existing hero object by selecting the icon to the right of the field above.\n\nIf you want to create a new hero object and assign it to this field, click the button below.", true);
            SimpleLayout.EndVertical();
            if (GUILayout.Button("Create & Assign Hero Object"))
            {
                heroKitObject.heroObject = HeroKitCommon.AddHeroObjectToFolder(false);
                Selection.activeObject = heroKitObject.transform;  
            }
            
        }

        else if (heroKitObject.heroObject != null)
        {
            if (GUILayout.Button("Edit Hero Object"))
            {
                HeroKitEditor.ButtonClickHeroObjectAsset(heroKitObject.heroObject);
            }

            if (GUILayout.Button("Show Details"))
            {
                ToggleDetails();
            }

            // show existing inspector
            if (showDetails)
                base.OnInspectorGUI();
        }
    }

    /// <summary>
    /// Show or hide the details about a hero kit object.
    /// </summary>
    private void ToggleDetails()
    {
        showDetails = !showDetails;
    }
}
