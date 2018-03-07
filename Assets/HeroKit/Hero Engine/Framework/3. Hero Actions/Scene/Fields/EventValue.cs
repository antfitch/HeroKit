// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene.ActionField
{
    /// <summary>
    /// Get an event from a hero object.
    /// </summary>
    public static class EventValue
    {
        /// <summary>
        /// Play an event.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <param name="targetObject">The heor kit object where the event should play.</param>
        public static void PlayEvent(HeroKitObject heroKitObject, int actionFieldID, HeroKitObject targetObject)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the hero kit object
            if (targetObject == null)
            {
                Debug.LogError(HeroKitCommonRuntime.NoHeroKitObjectDebugInfo(action.actionTemplate.name, 0, heroKitObject));
                return;
            }

            // Get the state ID
            int stateID = action.actionFields[actionFieldID].ints[3] - 1;

            // Get the event ID
            int eventID = action.actionFields[actionFieldID].ints[2] - 1;

            // Play the event if possible
            if (targetObject.heroStateData.state == stateID)
            {
                targetObject.PlayEvent(eventID, heroKitObject.gameObject);
            }
            else
            {
                Debug.LogWarning("Can't play event because its state is not active.");
            }
        }

        /// <summary>
        /// Get the ID of the state that contains the event.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The ID of the state that contains the event.</returns>
        public static int GetStateID(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the state ID
            int stateID = action.actionFields[actionFieldID].ints[3];

            return stateID;
        }

        /// <summary>
        /// Get the ID of the event.
        /// </summary>
        /// <param name="heroKitObject">The hero kit object that contains the data for this action.</param>
        /// <param name="actionFieldID">ID assigned to action field A.</param>
        /// <returns>The ID of the event.</returns>
        public static int GetEventID(HeroKitObject heroKitObject, int actionFieldID)
        {
            // Get the action
            HeroAction action = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action];

            // Get the state ID
            int eventID = action.actionFields[actionFieldID].ints[2];

            return eventID;
        }
    }
}