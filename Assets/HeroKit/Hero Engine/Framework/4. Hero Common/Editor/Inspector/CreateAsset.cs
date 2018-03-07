using UnityEngine;
using UnityEditor;
using System.IO;

public static class CreateCustomAsset
{
    public static T CreateAsset<T>(string assetName, bool focusOnObject = true, string createPath="") where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = (createPath == "") ? AssetDatabase.GetAssetPath(Selection.activeObject) : createPath;
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + assetName + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();

        if (focusOnObject)
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }

        return asset;
    }
}