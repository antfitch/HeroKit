// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEngine.EventSystems;
using HeroKit.Scene;

/// <summary>
/// Script that you can attach to any game object. You can use this script to communicate with a hero kit object in the game.
/// </summary>
public class HeroKitListenerUI : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    //-------------------------------------------
    // Variables
    //-------------------------------------------

    /// <summary>
    /// The type of input that triggers an interaction with a hero kit object.
    /// </summary>
    public enum InputType { onClick, onPress, onDrag, onDoubleClick };
    /// <summary>
    /// The input type to watch for.
    /// </summary>
    public InputType inputType;
    /// <summary>
    /// The ID assigned to the hero kit listener.
    /// </summary>
    public int itemID;
    /// <summary>
    /// A hero object assigned to the hero kit listener.
    /// </summary>
    public HeroObject item;
    /// <summary>
    /// Hero kit object that should receive hero kit objects.
    /// </summary>
    public HeroKitObject sendNotificationsHere = null;
    /// <summary>
    /// Type of action to perform.
    /// </summary>
    [HideInInspector]
    public int actionType;
    /// <summary>
    /// ID assigned to the state on the hero kit object to interact with.
    /// </summary>
    [HideInInspector]
    public int stateID = -1;
    /// <summary>
    /// ID assigned to the event on the hero kit objet to interact with.
    /// </summary>
    [HideInInspector]
    public int eventID = -1;
    /// <summary>
    /// Time between clicks.
    /// </summary>
    private float clickTime;
    /// <summary>
    /// Click count (1=single-click, 2=double-click, etc)
    /// </summary>
    private int clickCount = 0;
    /// <summary>
    /// If this interval passes between clicks, the click count is reset.
    /// </summary>
    private float clickInterval = 0.5f;

    //-------------------------------------------
    // Interaction Types
    //-------------------------------------------

    /// <summary>
    /// On click or double click.
    /// </summary>
    /// <param name="data">The pointer event data.</param>
    public void OnPointerClick(PointerEventData data)
	{	
		if (data == null || sendNotificationsHere == null) return;        

        // get interval between this click and the previous one (check for double click)
        float interval = data.clickTime - clickTime;

        // if this is double click, change click count
        if (interval < clickInterval && interval > 0 && clickCount != 2)
            clickCount = 2;
        else
            clickCount = 1;

        // reset click time
        clickTime = data.clickTime;
		
        // clicked on item
		if (inputType == InputType.onClick && clickCount == 1)
		{
            performAction();
        }
		
        // double-clicked item
		else if (inputType == InputType.onDoubleClick && clickCount == 2)
		{
            performAction();
        }		
	}
    /// <summary>
    /// Press down or begin drag.
    /// </summary>
    /// <param name="data">Pointer event data.</param>
    public void OnPointerDown(PointerEventData data)
    {		
		if (data == null || sendNotificationsHere == null) return;	
		
		if (inputType == InputType.onPress || inputType == InputType.onDrag)
		{
            performAction();

            //GameObject go = getTargetGO(data);
            //database.menuActions.sendUIAction(panelID, slotID, template, isClicked, isPressedBegin, isPressedEnd, isDrag, go);
            //if (onPress) database.inputActions.addToPickList(database.inputActions.pressDownList, go);
            //if (onDrag)	database.inputActions.addToPickList(database.inputActions.dragBeginList, go);
        }
    }
    /// <summary>
    /// Release press or end drag.
    /// </summary>
    /// <param name="data">Pointer event data.</param>
    public void OnPointerUp(PointerEventData data)
    {
        // the problem: only works on listener that was pressed down. For example,
        // if you drag icon from shortcut menu to crafting menu, the shortcut menu
        // panel calls onPointerUp. If you drag mouse over crafting menu (not slot) and
        // release mouse over a slot, nothing happens because the mouse did not start
        // dragging over the slot.

        //print ("aml: on pointer up");		

        if (data == null || sendNotificationsHere == null) return; 
		
		if (inputType == InputType.onPress || inputType == InputType.onDrag)
		{
            performAction();

            //GameObject go = getTargetGO(data);			
            //database.menuActions.sendUIAction(panelID, slotID, template, isClicked, isPressedBegin, isPressedEnd, isDrag, go);		
            //if (onPress) database.inputActions.addToPickList(database.inputActions.pressUpList, go);
            //if (onDrag) database.inputActions.addToPickList(database.inputActions.dragEndList, go);
        }	
    }
    /// <summary>
    /// Dragging.
    /// </summary>
    /// <param name="data">Pointer event data.</param>
    public void OnDrag(PointerEventData data)
	{
		if (data == null || sendNotificationsHere == null) return;
		
		if (inputType == InputType.onDrag)
		{
            performAction();

            //database.menuActions.sendUIAction(panelID, slotID, template, isClicked, isPressedBegin, isPressedEnd, isDrag, go);		
            //database.inputActions.addToPickList(database.inputActions.dragList, go);					
        }
	}

    //-------------------------------------------
    // General
    //-------------------------------------------

    /// <summary>
    /// Perform an action on the hero kit that can communicate with this listener. 
    /// </summary>
    private void performAction()
    {
        if (actionType <= 1)
        {
            callEvent(sendNotificationsHere, stateID, eventID);
        }
        else if (actionType == 2)
        {
            passValues(sendNotificationsHere);
        }
        
    }
    /// <summary>
    /// Call an event in a state on a hero kit object.
    /// </summary>
    /// <param name="heroKitObject">The hero kit object.</param>
    /// <param name="stateID">ID assigned to the state on the hero kit object.</param>
    /// <param name="eventID">ID assigned to the event in the hero kit object.</param>
    private void callEvent(HeroKitObject heroKitObject, int stateID, int eventID)
    {
        if (heroKitObject != null)
        {
            if (stateID > 0 && eventID > 0)
            {
                if (heroKitObject.heroStateData.state == stateID - 1)
                {
                    heroKitObject.heroListenerData.active = true;
                    heroKitObject.heroListenerData.itemID = itemID;
                    heroKitObject.heroListenerData.item = item;
                    heroKitObject.PlayEvent(eventID - 1, gameObject);
                }
            }
        }
    }
    /// <summary>
    /// Pass values to this listener from a hero kit object.
    /// </summary>
    /// <param name="heroKitObject">The hero kit object.</param>
    private void passValues(HeroKitObject heroKitObject)
    {
        heroKitObject.heroListenerData.active = true;
        heroKitObject.heroListenerData.itemID = itemID;
        heroKitObject.heroListenerData.item = item;
    }
}

/// <summary>
/// Data for a hero kit listener.
/// </summary>
public struct HeroKitListenerData
{
    public bool active;
    public int itemID;
    public HeroObject item;
}

