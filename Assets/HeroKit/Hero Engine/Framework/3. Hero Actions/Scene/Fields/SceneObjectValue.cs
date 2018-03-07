// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Work with the scene object field.
    /// </summary>
    public static class SceneObjectValue
    {
        /// <summary>
        /// Get a scene object from the scene object field.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <param name="includeInactive">Include inactive scene objects?</param>
        /// <returns>The scene object.</returns>
        public static SceneObjectValueData GetValue(HeroKitObject heroKitObject, int actionFieldIDA, int actionFieldIDB, bool includeInactive)
        {
            SceneObjectValueData objectData = new SceneObjectValueData();
            int dataType = DropDownListValue.GetValue(heroKitObject, actionFieldIDA);

            // object is hero object
            if (dataType == 1)
            {
                objectData.heroKitObject = HeroObjectFieldValue.GetValueA(heroKitObject, actionFieldIDB);
            }

            // object is game object
            else if (dataType == 2)
            {
                GameObject gameObject = GameObjectFieldValue.GetValueA(heroKitObject, actionFieldIDB, includeInactive);
                objectData.gameObject = new GameObject[] { gameObject };
            }

            return objectData;
        }
    }

    /// <summary>
    /// The object in the scene. This can be a hero kit object or a game object.
    /// </summary>
    public struct SceneObjectValueData
    {
        /// <summary>
        /// The game object.
        /// </summary>
        public GameObject[] gameObject;
        /// <summary>
        /// The hero kit object.
        /// </summary>
        public HeroKitObject[] heroKitObject;
    }
}