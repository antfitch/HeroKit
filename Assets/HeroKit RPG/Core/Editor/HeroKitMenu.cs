using UnityEngine;
using UnityEditor;

//namespace HeroKit.RpgEditor
//{
    public class HeroKitMenu
    {
        [MenuItem("HeroKit/RPG Builder", false, -10)]
        private static void NewMenuOption()
        {
            HeroKit.RpgEditor.HeroKitEditor.ShowWindow();
        }
    }
//}
