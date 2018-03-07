// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene;

/// <summary>
/// Attach this script to a game object that contains an animation event.
/// When the animation event is triggered, this script can execute an event in the parent hero game object.
/// </summary>
public class HeroKitListenerAnimationEvent : MonoBehaviour
{
    //-------------------------------------------
    // Variables
    //-------------------------------------------

    private HeroKitObject heroKitObject;

    //-------------------------------------------
    // Interaction Types
    //-------------------------------------------

    /// <summary>
    /// When this game object first loads, get the parent hero kit object.
    /// </summary>
    private void Start()
    {
        heroKitObject = HeroKitCommonRuntime.GetParentHeroKitObject(gameObject);
    }

    /// <summary>
    /// Call an event in the active state on a hero kit object.
    /// </summary>
    /// <param name="eventID">ID assigned to the event in the hero kit object.</param>
    private void CallHeroEvent(int eventID)
    {
        if (heroKitObject != null)
        {
            if (eventID > 0)
            {
                heroKitObject.PlayEvent(eventID);
            }
        }
    }

}


