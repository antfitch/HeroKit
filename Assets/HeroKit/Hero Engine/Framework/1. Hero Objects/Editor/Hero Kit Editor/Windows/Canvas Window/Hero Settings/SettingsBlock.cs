// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;
using System.IO;

namespace HeroKit.Editor
{
    /// <summary>
    /// Block for HeroKit Settings that appears in Hero Kit Editor.
    /// </summary>
    internal static class SettingsBlock
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// Name of the block.
        /// </summary>
        private static string blockName = "HeroKit Settings";

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Block to display on the canvas.
        /// </summary>
        /// <param name="heroKitAction">Hero kit action to display in the block.</param>
        public static void Block()
        {
            // draw components
            DrawBlock();
        }
        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            DrawHeader();
            DrawBody();
        }
        /// <summary>
        /// Draw the header of the block.
        /// </summary>
        private static void DrawHeader()
        {   
            HeroKitCommon.DrawBlockTitle(blockName);
        }
        /// <summary>
        /// Draw the body of the block.
        /// </summary>
        private static void DrawBody()
        {
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Refresh Hero Objects", RefreshHeroObjects, Button.StyleA);
            SimpleLayout.Label("Use this to make sure that your hero objects structure is up-to-date. \n" +
                               "For example, if you change a Hero Property that is assigned to several \n" +
                               "Hero Objects, press this button to make sure all of the hero objects have \n" +
                               "the new, updated properties.");

            SimpleLayout.Line();
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Delete Save Files", DeleteSaveFiles, Button.StyleA);
            SimpleLayout.Label("Quickly delete save files.");

            SimpleLayout.Line();
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(Box.StyleCanvasBox);
            SimpleLayout.Button("Migrate HeroKit data to Beta 1.02", UpdateHeroKit1_2, Button.StyleA);
            SimpleLayout.Label("This move your properties on hero objects into the new Property \n" +
                                "structure on hero objects. Once you've done this, you need to update your \n" +
                                "actions to point to the updated properties");

            SimpleLayout.Line();
            SimpleLayout.EndVertical();
        }

        // --------------------------------------------------------------
        // Methods (Misc)
        // --------------------------------------------------------------

        /// <summary>
        /// Clean up all hero objects in the project.
        /// </summary>
        public static void RefreshHeroObjects()
        {
            // get all hero objects in "Assets/Resources/" directories
            HeroObject[] heroObjects = Resources.LoadAll<HeroObject>("");

            // refresh all hero objects with the new data
            foreach (HeroObject o in heroObjects)
            {
                HeroKitCommon.BuildAllPropertyFields(o);
                HeroKitCommon.BuildInspectorFields(o);
                BuildActionFields(o);
                Debug.Log("Data refreshed for: " + o.name);
            }
        }

        /// <summary>
        /// Delete save files.
        /// </summary>
        public static void DeleteSaveFiles()
        {
            string filePath = Application.persistentDataPath + "/HeroSaves/";

            // check if file exists
            if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath, true);
            }

            Debug.Log("Saved game files deleted at:\n" + filePath);
        }

        // update HeroKit to 1.2
        public static void UpdateHeroKit1_2()
        {
            HeroObject[] heroObjects = Resources.LoadAll<HeroObject>("");
            for (int i = 0; i < heroObjects.Length; i++)
            {
                HeroProperties hp = heroObjects[i].properties;
                if (heroObjects[i].propertiesList.properties.Count == 0 && hp != null)
                {
                    // move properties to list
                    heroObjects[i].propertiesList.properties.Add(hp);
                    heroObjects[i].properties = null;

                    Debug.Log("Properties updated for: " + heroObjects[i].name);
                }
            }
        }

        /// <summary>
        /// This builds the action fields in the editor.
        /// </summary>
        /// <param name="heroObject">The hero object.</param>
        public static void BuildActionFields(HeroObject heroObject)
        {
            // referesh states
            if (heroObject.states.states != null && heroObject.states.states.Count != 0)
            {
                for (int stateID = 0; stateID < heroObject.states.states.Count; stateID++)
                {
                    StateBlock.Block(heroObject, stateID);

                    if (heroObject.states.states[stateID].heroEvent == null) return;
                    for (int eventID = 0; eventID < heroObject.states.states[stateID].heroEvent.Count; eventID++)
                    {
                        EventBlock.Block(heroObject, stateID, eventID);

                        if (heroObject.states.states[stateID].heroEvent[eventID].actions == null) return;
                        for (int actionID = 0; actionID < heroObject.states.states[stateID].heroEvent[eventID].actions.Count; actionID++)
                        {
                            ActionBlock.Block(heroObject, stateID, eventID, actionID);
                        }
                    }
                }
            }
        }

    }
}