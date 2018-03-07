// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEditor;
using HeroKit.Editor.HeroField;

/// <summary>
/// This is the inspector for the HeroKitListener script. 
/// This inspector is shown when you click on a game object 
/// in the scene that contains the HeroKitListener script.
/// </summary>
[CustomEditor(typeof(HeroKitListenerUI))]
public class HeroKitListenerUIInspector : Editor
{
    /// <summary>
    /// The hero kit listener that will use the inspector.
    /// </summary>
    private HeroKitListenerUI heroKitListener;

    /// <summary>
    /// Actions to perform when the hero kit listener is enabled.
    /// </summary>
    private void OnEnable()
    {
        heroKitListener = (HeroKitListenerUI)target;
    }

    /// <summary>
    /// Display a custom inspector for the hero kit listener.
    /// </summary>
    public override void OnInspectorGUI()
    {
        // show existing inspector
        base.OnInspectorGUI();

        if (heroKitListener != null && heroKitListener.sendNotificationsHere != null && heroKitListener.sendNotificationsHere.heroObject != null)
        {
            ChooseAction();       
        }
    }

    /// <summary>
    /// What should happen when a game object with a hero kit listener receives input (mouse click, key press, etc)
    /// </summary>
    public void ChooseAction()
    {
        string[] items = { "Send Values and Play Event", "Send Values"};
        GenericListField genericList = new GenericListField(items);
        heroKitListener.actionType = genericList.SetValuesInspector(heroKitListener.actionType, "Action Type");

        // play event
        if (heroKitListener.actionType <= 1)
        {
            PlayEvent();
        }
    }

    /// <summary>
    /// Play an event on a hero kit object.
    /// </summary>
    public void PlayEvent()
    {
        // show states and events
        if (heroKitListener.sendNotificationsHere.heroObject.states != null &&
            heroKitListener.sendNotificationsHere.heroObject.states.states != null &&
            heroKitListener.sendNotificationsHere.heroObject.states.states.Count != 0)
        {
            StateListField states = new StateListField();
            heroKitListener.stateID = states.SetValuesInspector(heroKitListener.stateID, heroKitListener.sendNotificationsHere.heroObject.states.states, "State");

            if (heroKitListener.stateID > 0)
            {
                EventListField events = new EventListField();
                heroKitListener.eventID = events.SetValuesInspector(heroKitListener.eventID, heroKitListener.sendNotificationsHere.heroObject.states.states[heroKitListener.stateID - 1].heroEvent, "Event To Execute");
            }
        }
        else
        {
            if (heroKitListener != null)
            {
                heroKitListener.stateID = -1;
                heroKitListener.eventID = -1;
            }
        }
    }
}