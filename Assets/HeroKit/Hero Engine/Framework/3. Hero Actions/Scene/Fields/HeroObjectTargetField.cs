// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// The hero kit objects that will receive an action.
    /// </summary>
    public static class HeroObjectTargetField
    {
        /// <summary>
        /// Get the hero kit objects that are the target of an action.
        /// </summary>
        /// <param name="data">Information about hero object field.</param>
        /// <returns>A list of hero kit objects.</returns>
        public static HeroKitObject[] GetValue(HeroObjectFieldData data)
        {
            HeroKitObject[] heroKitObject = new HeroKitObject[1];

            // return game object attached to this hero object
            if (data.objectType == 1)
            {
                heroKitObject[0] = data.heroKitObject;
            }

            // return a game object in this hero object's game object list (in variables)
            else if (data.objectType == 2)
            {
                heroKitObject = GetHeroList(data.heroKitObject.heroList.heroObjects, data, "Variables");
            }

            // return a game object in the scene
            else if (data.objectType == 3)
            {
                // get the game object in the scene using its GUID
                heroKitObject[0] = HeroKitDatabase.GetHeroKitObject(data.heroGUID);
            }

            // return a game object in this hero object's game object list (in properties)
            else if (data.objectType == 4)
            {
                heroKitObject = GetHeroList(data.heroKitObject.heroProperties[data.propertyID-1].itemProperties.heroObjects, data, "Properties");
            }

            // return a game object in this hero object's game object list (in globals)
            else if (data.objectType == 5)
            {
                heroKitObject = GetHeroList(HeroKitDatabase.globals.heroObjects, data, "Globals");
            }

            return heroKitObject;

        }

        /// <summary>
        /// Get the template assigned to a hero object field.
        /// </summary>
        /// <param name="data">Information about hero object field.</param>
        /// <returns>The hero object in a hero object field.</returns>
        public static HeroObject GetTemplate(HeroObjectFieldData data)
        {
            HeroObject heroObject = null;

            // return game object attached to this hero object as value (not done here)

            // return this hero object
            if (data.objectType == 2)
            {
                heroObject = data.heroKitObject.heroObject;
            }

            // return a hero object in variables
            else if (data.objectType == 3)
            {
                heroObject = GetHeroTemplate(data.heroKitObject.heroList.heroObjects, data, "Variables");
            }

            // return a hero object in the scene
            else if (data.objectType == 4)
            {
                // get the hero object in the scene using its GUID
                HeroKitObject hko = HeroKitDatabase.GetHeroKitObject(data.heroGUID);
                if (hko != null)
                    heroObject = hko.heroObject;
            }

            // return a hero object in properties
            else if (data.objectType == 5)
            {
                heroObject = GetHeroTemplate(data.heroKitObject.heroProperties[data.propertyID-1].itemProperties.heroObjects, data, "Properties");
            }

            // return a hero object in globals
            else if (data.objectType == 6)
            {
                heroObject = GetHeroTemplate(data.heroKitObject.heroGlobals.heroObjects, data, "Globals");
            }

            return heroObject;
        }

        /// <summary>
        /// Get hero kit objects in a hero object list.
        /// </summary>
        /// <param name="list">The hero object list in a hero object.</param>
        /// <param name="data">Information about hero object field.</param>
        /// <returns>The hero kit objects in a hero object list.</returns>
        private static HeroKitObject[] GetHeroList(HeroObjectList list, HeroObjectFieldData data, string listType)
        {
            int slotID = data.objectID - 1;
            HeroKitObject[] targetObject = new HeroKitObject[1];

            // exit early if there is no game object list item
            if (slotID < 0 || slotID >= list.items.Count)
            {
                Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectInListDebugInfo(data.heroKitObject, listType, slotID));
                return targetObject;
            }

            // get the target objects
            if (list.items[slotID].heroKitGameObjects != null)
                targetObject = list.items[slotID].heroKitGameObjects.ToArray();

            return targetObject;
        }

        /// <summary>
        /// Get a hero object template in a hero object list.
        /// </summary>
        /// <param name="list">The hero object list in a hero object.</param>
        /// <param name="data">Information about hero object field.</param>
        /// <returns>The hero object template in a hero object list.</returns>
        private static HeroObject GetHeroTemplate(HeroObjectList list, HeroObjectFieldData data, string listType)
        {
            int slotID = data.objectID - 1;

            // exit early if there is no game object list item
            if (slotID < 0 || slotID >= list.items.Count)
            {
                Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectInListDebugInfo(data.heroKitObject, listType, slotID));
                return null;
            }

            HeroObject targetObject = list.items[slotID].value;

            return targetObject;
        }
    }

    /// <summary>
    /// The data needed to to get a hero object.
    /// </summary>
    public struct HeroObjectFieldData
    {
        /// <summary>
        /// The hero kit object (for debugging messages)
        /// </summary>
        public HeroKitObject heroKitObject;

        /// <summary>
        /// the type of hero object we want to get (this, variable, property, scene)
        /// </summary>
        public int objectType;

        /// <summary>
        /// the hero object that contains the hero object list (used if getting hero object from a list)
        /// </summary>
        public HeroList heroList;

        /// <summary>
        /// the ID assigned to the hero object in the hero object list (used if getting hero object from a list)
        /// </summary>
        public int objectID;

        /// <summary>
        /// the ID assigned to the hero object in the scene (used if getting hero object from scene)
        /// </summary>
        public int heroGUID;

        /// <summary>
        /// the ID assigned to the hero property on the hero object
        /// </summary>
        public int propertyID;
    }
}