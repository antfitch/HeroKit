// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------

namespace HeroKit.Scene.Actions
{
    /// <summary>
    /// All actions must use this template. The HeroKit engine access actions through the memebers of this interface.
    /// </summary>
    public interface IHeroKitAction
    {
        /// <summary>
        /// The hero kit object that controls this action.
        /// </summary>
        HeroKitObject heroKitObject { get; set; }

        /// <summary>
        /// Execute the action.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that called this action.</param>
        /// <returns>The return code for the action.</returns>
        int Execute(HeroKitObject heroKitObject);

        // --------------------------------------
        // The following items are necessary for Long Actions
        // (actions that need more than one frame to complete)
        // --------------------------------------

        /// <summary>
        /// Bool that determines whether this action is finished (only used if this is Long Action).
        /// </summary>
        bool updateIsDone { get; set; }

        /// <summary>
        /// The ID assigned to this event on the hero kit object.
        /// </summary>
        int eventID { get; set; }

        /// <summary>
        /// Called each frame. Used by a Long Action.
        /// </summary>
        void Update();

        /// <summary>
        /// Check to see if a long action has completed
        /// </summary>
        /// <returns>Has this long action completed?</returns>
        bool RemoveFromLongActions();
    }
}