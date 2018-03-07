// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEditor;
using UnityEngine;
using HeroKit.Scene;
using HeroKit.Editor;

namespace HeroKit.Menu
{
    /// <summary>
    /// Create a hero object (scriptable object + this appears in Unity toolbar).
    /// </summary>
    public class CreateHeroObject
    {
        [MenuItem("HeroKit/Create Hero Object (Asset)", false, 12)]
        public static HeroObject Create()
        {
            return HeroKitCommon.AddHeroObjectToFolder(true);
        }
    }

    /// <summary>
    /// Create a hero object inline.
    /// </summary>
    public class CreateHeroObjectInline
    {
        [MenuItem("Assets/Create/HeroKit/Hero Object")]
        public static HeroObject Create()
        {
            return HeroKitCommon.CreateHeroObject();
        }
    }

    // Create a hero property (scriptable object + this appears in Unity toolbar)
    public class CreatePropertyObject
    {
        [MenuItem("HeroKit/Create Hero Property (Asset)", false, 13)]
        public static HeroKitProperty Create()
        {
            return HeroKitCommon.AddHeroPropertyToFolder(true);
        }
    }

    // Create a hero property (scriptable object + this appears in Unity toolbar)
    public class CreatePropertyObjectInline
    {
        [MenuItem("Assets/Create/HeroKit/Hero Property")]
        public static HeroKitProperty Create()
        {
            return CreateCustomAsset.CreateAsset<HeroKitProperty>("Hero Property");
        }
    }

    // Create a hero action (scriptable object + this appears in Unity toolbar)
    public class CreateActionObject
    {
        [MenuItem("HeroKit/Create Hero Action (Asset)", false, 14)]
        public static HeroKitAction Create()
        {
            return HeroKitCommon.AddHeroActionToFolder(true);
        }
    }

    // Create a hero action (scriptable object + this appears in Unity toolbar)
    public class CreateActionObjectInline
    {
        [MenuItem("Assets/Create/HeroKit/Hero Action")]
        public static HeroKitAction Create()
        {
            return CreateCustomAsset.CreateAsset<HeroKitAction>("Hero Action");
        }
    }

    /// <summary>
    /// Create a hero kit object (scriptable object + this appears in Unity toolbar).
    /// </summary>
    public class CreateHeroKitObject
    {
        [MenuItem("GameObject/HeroKit/Hero Block", false, 10)]
        [MenuItem("HeroKit/Create Hero Block (GameObject)", false, 30)]
        public static GameObject Create()
        {
            GameObject go = new GameObject("Hero Block");
            go.AddComponent<HeroKitObject>();

            Camera sceneCam = SceneView.lastActiveSceneView.camera;
            Vector3 spawnPos = sceneCam.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 2.5f));
            go.transform.position = spawnPos;
            return go;
        }
    }

    /// <summary>
    /// Create a hero kit object (scriptable object + this appears in Unity toolbar).
    /// </summary>
    public class CreateHeroKitObjectAndHO
    {
        [MenuItem("GameObject/HeroKit/Hero Block (+Hero Object)", false, 10)]
        [MenuItem("HeroKit/Create Hero Block (GameObject + Hero Object)", false, 31)]
        public static GameObject Create()
        {
            GameObject go = new GameObject("Hero Block");
            HeroKitObject heroKitObject = go.AddComponent<HeroKitObject>();

            heroKitObject.heroObject = HeroKitCommon.AddHeroObjectToFolder(false);

            Camera sceneCam = SceneView.lastActiveSceneView.camera;
            Vector3 spawnPos = sceneCam.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 2.5f));
            go.transform.position = spawnPos;
            return go;
        }
    }

    /// <summary>
    /// Create a hero kit object (scriptable object + this appears in Unity toolbar).
    /// </summary>
    public class CreateHeroProject
    {
        [MenuItem("HeroKit/Create Hero Project (Project Folder)", false, 50)]
        public static void CreateMaterial()
        {
            HeroKitCommon.CreateHeroKitProjectFolders();
        }
    }

    // Create a link for community support (open URL + this appears in Unity toolbar)
    public class CreateCommunity
    {
        [MenuItem("HeroKit/Community (Forum)", false, 70)]
        public static string Create()
        {
            Application.OpenURL("http://aveyond.com/forums/index.php?/forum/325-herokit-game-maker/");
            return "";
        }
    }

    // Create a link for community support (open URL + this appears in Unity toolbar)
    public class CreateDocs
    {
        [MenuItem("HeroKit/Tutorials and Docs (Forum)", false, 70)]
        public static string Create()
        {
            Application.OpenURL("http://aveyond.com/forums/index.php?/forum/329-herokit-tutorials/");
            return "";
        }
    }

    // Create a link for community support (open URL + this appears in Unity toolbar)
    public class CreateGetAnswers
    {
        [MenuItem("HeroKit/Get Answers (Forum)", false, 70)]
        public static string Create()
        {
            Application.OpenURL("http://aveyond.com/forums/index.php?/forum/330-herokit-answers/");
            return "";
        }
    }

    // Create a link for community support (open URL + this appears in Unity toolbar)
    public class CreateDownloadActions
    {
        [MenuItem("HeroKit/Download Actions (Forum)", false, 70)]
        public static string Create()
        {
            Application.OpenURL("http://aveyond.com/forums/index.php?/forum/332-herokit-actions-hacks/");
            return "";
        }
    }

    // Create a link for community support (open URL + this appears in Unity toolbar)
    public class CreateSubmitBugs
    {
        [MenuItem("HeroKit/Submit Bugs (Forum)", false, 70)]
        public static string Create()
        {
            Application.OpenURL("http://aveyond.com/forums/index.php?/forum/334-report-bugs/");
            return "";
        }
    }
}