// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;
using HeroKit.Scene;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// When a scene is opened, refresh visuals for all hero kit objects in the scene.
/// This class is automatically initialized by the Unity Editor when the Unity Editor loads. Do Not Delete This!
/// </summary>
public class HeroKitAutoRun
{
    /// <summary>
    /// Actions to perform when an instance of this class is created.
    /// </summary>
    [InitializeOnLoadMethod]
    static void AutoRunForHeroKit()
    {
        if (!EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Debug.Log("HeroKit loaded in Unity Editor");

            // if gizmos for herokit do not exist in Gizmos folder, add them
            AddGizmosToFolder();

            // watch for scene change.
            EditorSceneManager.sceneOpened += SceneChanged;

            // watch for hero kit objects that are double-clicked in scene view.
            SceneView.onSceneGUIDelegate += DoubleClickItemInScene;

            // add scenes to build settings (the first time hero kit loads)
            HeroKit.Editor.HeroKitCommon.AddScenesToBuildSettings();

            // Unity 2017.0 and 2017.1 will not display the preview window with OnPreviewGUI. 
            bool allowDragHO = true;
            #if (UNITY_2017_0 || UNITY_2017_1)
                allowDragHO = false;
            #endif

            // watch for hero objects that are added to the hierarchy window
            if (allowDragHO)
                EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        }
    }

    // --------------------------------------------------------------
    // Drag hero object from Project tab to Hierarchy tab
    // --------------------------------------------------------------

    /// <summary>
    /// Time between wait.
    /// </summary>
    private static readonly float waitTime = 0;
    /// <summary>
    /// Wait count (1=waiting, 2=not waiting, etc)
    /// </summary>
    private static int waitCount = 0;
    /// <summary>
    /// If this interval passes between wait, the wait count is reset.
    /// </summary>
    private static float waitInterval = 30000f;
    /// <summary>
    /// We only want to drop a hero object ONCE in the Hierarchy window if a drag perform operation occurred.
    /// </summary>
    private static EventType lastEvent = EventType.DragUpdated;

    /// <summary>
    /// Drag and drop a hero object into the hierarchy window. 
    /// (by jbarba_ballytech on Unity Answers)
    /// </summary>
    /// <param name="instanceID">(Not used) Instance ID for the icon.</param>
    /// <param name="selectionRect">(Not used) Rect for the icon.</param>
    static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        //if (Event.current.type.ToString() != "Layout" && Event.current.type.ToString() != "Repaint" && Event.current.type.ToString() != "ignore")
        //    Debug.Log(Event.current.type);

        // show "it's okay to add this" icon (instead of X)
        // note: this also stops "add hero object" bug when creator drags hero object out of hierarchy window
        DragAndDrop.visualMode = DragAndDropVisualMode.Move;

        // happens when an acceptable item is released over the GUI window
        if (Event.current.type == EventType.DragExited)
        {
            if (lastEvent == EventType.DragPerform)
            {
                //Debug.Log("drag exited");

                // if we are on mac os, use this. otherwise two hero blocks are created.
                if (Application.platform == RuntimePlatform.OSXEditor)
                {
                    // get interval between this click and the previous one (check for double click)
                    float interval = Time.realtimeSinceStartup - waitTime;

                    // if this is double click, change click count
                    if (interval < waitInterval && interval > 0 && waitCount != 2)
                        waitCount = 2;
                    else
                        waitCount = 1;

                    // reset click time
                    clickTime = Time.realtimeSinceStartup;

                    if (waitCount == 1)
                    {
                        Event.current.Use();
                        return;
                    }
                }

                DragAndDrop.AcceptDrag();
                List<GameObject> selectedObjects = new List<GameObject>();
                foreach (var item in DragAndDrop.objectReferences)
                {
                    if (item is HeroObject)
                    {
                        GameObject gameObject = new GameObject(item.name);
                        HeroKitObject heroKitObject = gameObject.AddComponent<HeroKitObject>();
                        heroKitObject.heroObject = item as HeroObject;
                        selectedObjects.Add(gameObject);

                        Camera sceneCam = Camera.current;
                        SceneView lastScene = SceneView.lastActiveSceneView;
                        if (lastScene != null)
                        {
                            sceneCam = lastScene.camera;
                            Vector3 spawnPos = sceneCam.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 2.5f));
                            gameObject.transform.position = spawnPos;
                        }
                    }
                }

                // exit early if no objects found.
                if (selectedObjects.Count == 0) return;

                // assign found object to selection.
                Selection.objects = selectedObjects.ToArray();

                // make sure this call is the only one that processes the event.
                //Event.current.Use();

                lastEvent = EventType.DragExited;
            }
        }

        if (Event.current.type == EventType.DragUpdated || Event.current.type == EventType.DragPerform)
            lastEvent = Event.current.type;
    }

    // --------------------------------------------------------------
    // Scene Changed in Scene View
    // --------------------------------------------------------------

    /// <summary>
    /// Actions to perform when a scene is changed in the Unity Editor.
    /// </summary>
    /// <param name="scene">The new scene.</param>
    /// <param name="mode">The mode used to open the scene.</param>
    private static void SceneChanged(Scene scene, OpenSceneMode mode)
    {
        // refresh visuals assigned to hero kit objects in the scene so that they match
        // the data in thier hero object templates.
        HeroKit.Scene.HeroKitCommonRuntime.RefreshAllVisuals();
    }

    // --------------------------------------------------------------
    // Double-Click Hero Kit Object in Scene View
    // --------------------------------------------------------------

    /// <summary>
    /// Time between clicks.
    /// </summary>
    private static float clickTime;
    /// <summary>
    /// Click count (1=single-click, 2=double-click, etc)
    /// </summary>
    private static int clickCount = 0;
    /// <summary>
    /// If this interval passes between clicks, the click count is reset.
    /// </summary>
    private static float clickInterval = 0.5f;
    /// <summary>
    /// Check for double-click in scene.
    /// </summary>
    /// <param name="sceneview">The scene.</param>
    private static void DoubleClickItemInScene(SceneView sceneview)
    {
        Handles.BeginGUI();

        // check for click
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            // get interval between this click and the previous one (check for double click)
            float interval = Time.realtimeSinceStartup - clickTime;

            // if this is double click, change click count
            if (interval < clickInterval && interval > 0 && clickCount != 2)
                clickCount = 2;
            else
                clickCount = 1;

            // reset click time
            clickTime = Time.realtimeSinceStartup;

            // check if there is an active game object under item that was clicked.
            if (clickCount == 2)
            {
                GameObject activeObject = Selection.activeGameObject;
                if (activeObject != null)
                {
                    Transform t = activeObject.transform;
                    HeroKitObject activeHeroKitObject = null;

                    while (t != null)
                    {
                        HeroKitObject hko = t.GetComponent<HeroKitObject>();
                        if (hko != null)
                        {
                            activeHeroKitObject = hko;
                            break;
                        }
                        else
                        {
                            t = t.parent;
                        }
                    }

                    if (activeHeroKitObject != null && activeHeroKitObject.heroObject != null)
                    {
                        HeroKit.Editor.HeroKitEditor.ButtonClickHeroObjectAsset(activeHeroKitObject.heroObject);
                        HeroKit.Editor.HeroObjectMenuBlock.changeSelection();
                    }
                }

            }
        }

        Handles.EndGUI();
    }

    /// <summary>
    /// Create and add gizmos to the project folder.
    /// </summary>
    public static bool AddGizmosToFolder()
    {
        bool gizmosExist = false;
        string GizmosFolder = "Gizmos";

        // build folder structure for gizmos if it does not exist
        if (!AssetDatabase.IsValidFolder("Assets" + "/" + GizmosFolder))
            MakeFolder("Assets", GizmosFolder);

        // move gizmos
        string[] icons = { "HeroKitAction Icon", "HeroKitProperty Icon", "HeroObject Icon" };
        foreach (string name in icons)
        {
            string oldPath = "Assets/HeroKit/Hero Engine/Assets/Resources/Gizmos/" + name + ".png";
            string newPath = "Assets/Gizmos/" + name + ".png";

            //string res = AssetDatabase.ValidateMoveAsset(oldPath, newPath);
            //Debug.Log(res);

            // only move an icon if it does not exist in the Gizmos folder. If it is already in this folder, note this.
            string[] hits = AssetDatabase.FindAssets(name, new string[] { "Assets/Gizmos" });
            if (hits == null || hits.Length == 0)
                AssetDatabase.CopyAsset(oldPath, newPath);
            else
                gizmosExist = true;
        }

        return gizmosExist;
    }

    /// <summary>
    /// Create a folder in the Project Tab.
    /// </summary>
    /// <param name="directory">The directory where this folder should be placed.</param>
    /// <param name="folder">The name of the folder to create.</param>
    /// <returns></returns>
    private static string MakeFolder(string directory, string folder)
    {
        string guid = AssetDatabase.CreateFolder(directory, folder);
        string path = AssetDatabase.GUIDToAssetPath(guid);
        return path;
    }
}