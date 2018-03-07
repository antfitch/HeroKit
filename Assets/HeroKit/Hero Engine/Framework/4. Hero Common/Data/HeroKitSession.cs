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
    public class HeroKitSession : ScriptableObject
    {
        /// <summary>
        /// The hero object being displayed in the HeroKit Editor.
        /// </summary>
        public HeroObject heroObject;
        /// <summary>
        /// MainMenuBlock.typeID
        /// </summary>
        public int mainMenuTypeID;
        /// <summary>
        /// MainMenuState.stateID
        /// </summary>
        public int mainMenuStateID;
        /// <summary>
        /// MainMenuBlock.eventID
        /// </summary>
        public int mainMenuEventID;
        /// <summary>
        /// MainMenuBlock.actionID
        /// </summary>
        public int mainMenuActionID;
        /// <summary>
        /// MainMenuBlock.variableID
        /// </summary>
        public int mainMenuVariableID;
        /// <summary>
        /// MainMenuBlock.globalID
        /// </summary>
        public int mainMenuGlobalID;

        /// <summary>
        /// Is property window open?
        /// </summary>
        public bool mainMenuPropertyFocus;
        /// <summary>
        /// Is state window open?
        /// </summary>
        public bool mainMenuStateFocus;
        /// <summary>
        /// Is variable window open?
        /// </summary>
        public bool mainMenuVariableFocus;
        /// <summary>
        /// Is global window open?
        /// </summary>
        public bool mainMenuGlobalFocus;
        /// <summary>
        /// Is settings window open?
        /// </summary>
        public bool mainMenuSettingsFocus;
    }
}
