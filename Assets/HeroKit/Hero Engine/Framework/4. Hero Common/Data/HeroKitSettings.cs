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
        /// The game over menu controller.
        /// </summary>
        public HeroObject gameoverMenuController;

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
        /// The options menu controller.
        /// </summary>
        public HeroObject optionsMenuController;

        /// <summary>
        /// The save / load menu.
        /// </summary>
        public GameObject saveMenu;

        /// <summary>
        /// The save menu controller.
        /// </summary>
        public HeroObject saveMenuController;

        /// <summary>
        /// The start menu.
        /// </summary>
        public GameObject startMenu;

        /// <summary>
        /// The start menu controller.
        /// </summary>
        public HeroObject startMenuController;

        /// <summary>
        /// The inventory item properties.
        /// </summary>
        public HeroKitProperty inventoryItem;

        /// <summary>
        /// The inventory slot.
        /// </summary>
        public GameObject inventorySlot;

        /// <summary>
        /// The inventory slot controller.
        /// </summary>
        public HeroObject inventorySlotController;

        /// <summary>
        /// The journal item properties.
        /// </summary>
        public HeroKitProperty journalItem;

        /// <summary>
        /// The journal slot.
        /// </summary>
        public GameObject journalSlot;

        /// <summary>
        /// The journal slot controller.
        /// </summary>
        public HeroObject journalSlotController;

        /// <summary>
        /// The save slot.
        /// </summary>
        public GameObject saveSlot;

        /// <summary>
        /// The save slot controller.
        /// </summary>
        public HeroObject saveSlotController;
    }
}
