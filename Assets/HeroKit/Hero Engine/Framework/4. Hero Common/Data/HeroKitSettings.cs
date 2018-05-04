// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene
{
    /// <summary>
    /// Display editor session settings in a scriptable object.
    /// </summary>
    [System.Serializable]
    public class HeroKitSettings : ScriptableObject
    {
        /// <summary>
        /// The prefab for the dialog box to use in menus.
        /// </summary>
        public GameObject dialogBox;

        /// <summary>
        /// The fade in / fade out screen.
        /// </summary>
        public GameObject fadeInOutScreen;

        /// <summary>
        /// The game over menu.
        /// </summary>
        public GameObject gameoverMenu;

        /// <summary>
        /// The inventory menu.
        /// </summary>
        public GameObject inventoryMenu;

        /// <summary>
        /// The journal menu.
        /// </summary>
        public GameObject journalMenu;

        /// <summary>
        /// The options menu.
        /// </summary>
        public GameObject optionsMenu;

        /// <summary>
        /// The save / load menu.
        /// </summary>
        public GameObject saveMenu;

        /// <summary>
        /// The start menu.
        /// </summary>
        public GameObject startMenu;

        /// <summary>
        /// The inventory item properties.
        /// </summary>
        public HeroKitProperty inventoryItem;

        /// <summary>
        /// The inventory slot.
        /// </summary>
        public GameObject inventorySlot;

        /// <summary>
        /// The journal item properties.
        /// </summary>
        public HeroKitProperty journalItem;

        /// <summary>
        /// The journal slot.
        /// </summary>
        public GameObject journalSlot;

        /// <summary>
        /// The save slot.
        /// </summary>
        public GameObject saveSlot;
    }
}
