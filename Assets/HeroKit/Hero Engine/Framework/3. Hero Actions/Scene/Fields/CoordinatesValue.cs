// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get X, Y, and Z coordinates.
    /// </summary>
    public static class CoordinatesValue
    {
        /// <summary>
        /// Get X, Y, and Z coordinates.
        /// This is for a field that contains Value, Variable, Property, Global.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldIDA">ID assigned to action field A.</param>
        /// <param name="actionFieldIDB">ID assigned to action field B.</param>
        /// <param name="actionFieldIDC">ID assigned to action field C.</param>
        /// <param name="actionFieldIDD">ID assigned to action field D.</param>
        /// <param name="actionFieldIDE">ID assigned to action field E.</param>
        /// <param name="actionFieldIDF">ID assigned to action field F.</param>
        /// <param name="startingCoords">ID assigned to action field G.</param>
        /// <returns>The X, Y, and Z coordinates.</returns>
        public static Vector3 GetValue(HeroKitObject heroKitObject, int actionFieldIDA, int actionFieldIDB, int actionFieldIDC, int actionFieldIDD, int actionFieldIDE, int actionFieldIDF, Vector3 startingCoords)
        {
            Vector3 pos = startingCoords;

            // get the values to update
            bool changeX = BoolValue.GetValue(heroKitObject, actionFieldIDA);
            if (changeX) pos.x = FloatFieldValue.GetValueA(heroKitObject, actionFieldIDB);
            bool changeY = BoolValue.GetValue(heroKitObject, actionFieldIDC);
            if (changeY) pos.y = FloatFieldValue.GetValueA(heroKitObject, actionFieldIDD);
            bool changeZ = BoolValue.GetValue(heroKitObject, actionFieldIDE);
            if (changeZ) pos.z = FloatFieldValue.GetValueA(heroKitObject, actionFieldIDF);

            return pos;
        }
    }
}