// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields; 

namespace HeroKit.Editor
{
    /// <summary>
    /// Editor for Hero Objects, Hero Properties, and Hero Actions
    /// </summary>
    public class HeroKitEditor : EditorWindow
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// Structure this window as a singleton class (we only want one window, never more).
        /// </summary>
        public static HeroKitEditor Instance { get; private set; }

        /// <summary>
        /// The hero object data to display in the hero kit editor.
        /// </summary>
        public static HeroObject heroObject { get; set; }
        /// <summary>
        /// The hero action data to display in the hero kit editor.
        /// </summary>
        public static HeroKitAction heroKitAction { get; set; }
        /// <summary>
        /// The hero property data to display in the hero kit editor.
        /// </summary>
        public static HeroKitProperty heroKitProperty { get; set; }

        /// <summary>
        /// The type of object we are displaying in the hero kit editor.
        /// 0 = Hero Object, 1 = Hero Action, 2 = Hero Property
        /// </summary>
        public static int heroType { get; set; }

        /// <summary>
        /// Scroll position of the menu inside the hero kit editor.
        /// </summary>
        private Vector2 scrollPosMenu;
        /// <summary>
        /// Scroll position of the canvas inside the hero kit editor.
        /// </summary>
        private Vector2 scrollPosCanvas;

        /// <summary>
        /// The rectangle that contains the title window.
        /// </summary>
        public static Rect windowTitle;
        /// <summary>
        /// The rectangle that contains the menu window.
        /// </summary>
        public static Rect windowMenu;
        /// <summary>
        /// The rectangle that contains the canvas window.
        /// </summary>
        public static Rect windowCanvas;

        /// <summary>
        /// The default width of the menu window.
        /// </summary>
        private float windowMenuWidth = 330;
        /// <summary>
        /// Height of the title window.
        /// </summary>
        private float titleHeight = 60;

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------

        /// <summary>
        /// Enable the hero kit editor.
        /// </summary>
        private void OnEnable()
        {
            Instance = this;
            windowMenu.width = windowMenuWidth;
        }

        /// <summary>
        /// Display the contents of the hero kit editor.
        /// </summary>
        public void OnGUI()
        {
            EventType eventType = Event.current.type; 

            // show hero object in window
            if (heroType == 0)
            {
                if (heroObject == null)
                {
                    // attempt to retreive last hero object we worked with
                    heroObject = HeroKitCommon.LoadHeroSessionHeroObject();

                    // if nothing found, exit
                    if (heroObject == null) return;
                }           
					
				windowTitle = new Rect(0, 0, Instance.position.width - 3, titleHeight);
				windowMenu = new Rect(0, windowTitle.height, windowMenu.width, (int)Instance.position.height); 
				windowCanvas = new Rect(windowMenu.width, windowTitle.height, Instance.position.width - windowMenu.width, (int)Instance.position.height); 

                BeginWindows();
                windowTitle = GUILayout.Window(3, windowTitle, DrawTitleWindowHeroObject, "", Box.StyleDefault);
                windowMenu = HorizResizer(windowMenu, eventType); //right
                windowMenu = HorizResizer(windowMenu, eventType, false); //left
                windowMenu = GUILayout.Window(1, windowMenu, DrawMenuWindowHeroObject, "", Box.StyleMenu);
                windowCanvas = GUILayout.Window(2, windowCanvas, DrawCanvasWindowHeroObject, "", Box.StyleDefault);
                EndWindows();

                // check for key down events for main menu
                if (Event.current.type == EventType.KeyDown)
                    HeroObjectMenuBlock.changeSelection();

                // update GUI if it has changed
                if (GUI.changed)
                {
                    EditorUtility.SetDirty(heroObject);
                    HeroKitCommon.SaveHeroSession(heroObject);
                    HeroKitCommon.SaveGlobals();
                }                 
            }

            // show hero action in window
            else if (heroType == 1)
            {
                if (heroKitAction != null)
                {
					windowTitle = new Rect(0, 0, Instance.position.width - 3, titleHeight);
					windowCanvas = new Rect(0, windowTitle.height, Instance.position.width, (int)Instance.position.height);

                    BeginWindows();
                    windowTitle = GUILayout.Window(3, windowTitle, DrawTitleWindowHeroAction, "", Box.StyleDefault);
                    windowCanvas = GUILayout.Window(2, windowCanvas, DrawCanvasWindowHeroAction, "", Box.StyleDefault);
                    EndWindows();
                }

                // update GUI if it has changed
                if (GUI.changed)
                {
                    EditorUtility.SetDirty(heroKitAction);
                }
            }

            // show hero property in window
            if (heroType == 2)
            {
                if (heroKitProperty != null)
                {
					windowTitle = new Rect(0, 0, Instance.position.width - 3, titleHeight);
					windowMenu = new Rect(0, windowTitle.height, windowMenu.width, (int)Instance.position.height);
					windowCanvas = new Rect(windowMenu.width, windowTitle.height, Instance.position.width - windowMenu.width, (int)Instance.position.height);

                    BeginWindows();
                    windowTitle = GUILayout.Window(3, windowTitle, DrawTitleWindowHeroProperty, "", Box.StyleDefault);

                    windowMenu = HorizResizer(windowMenu, eventType); //right
                    windowMenu = HorizResizer(windowMenu, eventType, false); //left
                    windowMenu = GUILayout.Window(1, windowMenu, DrawMenuWindowHeroProperty, "", Box.StyleMenu);

                    windowCanvas = GUILayout.Window(2, windowCanvas, DrawCanvasWindowHeroProperty, "", Box.StyleDefault); 
                    EndWindows();

                    // check for key down events for main menu
                    if (Event.current.type == EventType.KeyDown)
                        HeroPropertyMenuBlock.changeSelection();
                }

                // update GUI if it has changed
                if (GUI.changed)
                {
                    EditorUtility.SetDirty(heroKitProperty);
                }
            }
        }

        /// <summary>
        /// When hero kit editor is closed, perform these actions.
        /// </summary>
        private void OnDestroy()
        {
            HeroKitCommon.SaveHeroSession(heroObject);
        }
        /// <summary>
        /// When the hero kit editor is no longer the window in focus, perform these actions.
        /// </summary>
        private void OnLostFocus()
        {
            HeroKitCommon.SaveHeroSession(heroObject);
        }
        /// <summary>
        /// When the hero kit editor is disabled, perform these actions.
        /// </summary>
        private void OnDisable()
        {
            HeroKitCommon.SaveHeroSession(heroObject);
        }

        // --------------------------------------------------------------
        // Methods (Draw Hero Object Windows)
        // --------------------------------------------------------------

        /// <summary>
        /// Draw the title window inside the hero kit editor (hero object).
        /// </summary>
        /// <param name="id">ID assigned to the GUI Window</param>
        private void DrawTitleWindowHeroObject(int id)
        {
            SimpleLayout.BeginHorizontal(Box.StyleTitleWindow);
            TitleBlock.Block(heroObject.name, 0);
            SimpleLayout.Space();
            TitleButtonBlock.Block();
            SimpleLayout.EndHorizontal();
        }
        /// <summary>
        /// Draw the menu window inside the hero kit editor (hero object).
        /// </summary>
        /// <param name="id">ID assigned to the GUI Window</param>
        private void DrawMenuWindowHeroObject(int id)
        {
			scrollPosMenu = EditorGUILayout.BeginScrollView(scrollPosMenu, GUILayout.Width(windowMenu.width), GUILayout.Height((int)(Instance.position.height- titleHeight)));

			SimpleLayout.BeginVertical(Box.StyleMenu2, (int)(Instance.position.height-titleHeight));
            HeroObjectMenuBlock.Block(heroObject, this);
            SimpleLayout.EndVertical();

            EditorGUILayout.EndScrollView();
        }
        /// <summary>
        /// Draw the canvas window inside the hero kit editor (hero object).
        /// </summary>
        /// <param name="id">ID assigned to the GUI Window</param>
        private void DrawCanvasWindowHeroObject(int id)
        {
			scrollPosCanvas = EditorGUILayout.BeginScrollView(scrollPosCanvas, GUILayout.Width(windowCanvas.width), GUILayout.Height((int)(Instance.position.height - titleHeight)));

			SimpleLayout.BeginVertical(Box.StyleCanvas, (int)(Instance.position.height - titleHeight));

            // draw state
            if (HeroObjectMenuBlock.typeID == 1)
            {
                if (HeroObjectMenuBlock.stateID == -1)
                {
                    // show state tutorial page
                }
                else
                {
                    StateBlock.Block(heroObject, HeroObjectMenuBlock.stateID);
                }
            }
            // draw event
            if (HeroObjectMenuBlock.typeID == 2)
            {
                EventBlock.Block(heroObject, HeroObjectMenuBlock.stateID, HeroObjectMenuBlock.eventID);
            }
            // draw action
            else if (HeroObjectMenuBlock.typeID == 3)
            {
                ActionBlock.Block(heroObject, HeroObjectMenuBlock.stateID, HeroObjectMenuBlock.eventID, HeroObjectMenuBlock.actionID);
            }
            // draw properties
            else if (HeroObjectMenuBlock.typeID == 4)
            {
                if (HeroObjectMenuBlock.propertyID == -1)
                {
                    // show properties tutorial page
                }
                else
                {
                    PropertiesBlock.Block(heroObject, HeroObjectMenuBlock.propertyID);
                }             
            }
            // draw variables
            else if (HeroObjectMenuBlock.typeID == 5)
            {
                switch (HeroObjectMenuBlock.variableID)
                {
                    case -1:
                        // show variable tutorial page
                        break;
                    case 0:
                        IntListBlock.Block(heroObject);
                        break;
                    case 1:
                        BoolListBlock.Block(heroObject);
                        break;
                    case 2:
                        StringListBlock.Block(heroObject);
                        break;
                    case 3:
                        GameObjectListBlock.Block(heroObject);
                        break;
                    case 4:
                        HeroObjectListBlock.Block(heroObject);
                        break;
                    case 5:
                        FloatListBlock.Block(heroObject);
                        break;
                    case 6:
                        UnityObjectListBlock.Block(heroObject);
                        break;
                }
            }
            // draw globals
            else if (HeroObjectMenuBlock.typeID == 6)
            {
                switch (HeroObjectMenuBlock.globalID)
                {
                    case -1:
                        // show variable tutorial page
                        break;
                    case 0:
                        IntListBlock.Block(heroObject, true);
                        break;
                    case 1:
                        BoolListBlock.Block(heroObject, true);
                        break;
                    case 2:
                        StringListBlock.Block(heroObject, true);
                        break;
                    case 3:
                        GameObjectListBlock.Block(heroObject, true);
                        break;
                    case 4:
                        HeroObjectListBlock.Block(heroObject, true);
                        break;
                    case 5:
                        FloatListBlock.Block(heroObject, true);
                        break;
                    case 6:
                        UnityObjectListBlock.Block(heroObject, true);
                        break;
                }
            }
            // draw settings
            else if (HeroObjectMenuBlock.typeID == 7)
            {
                SettingsBlock.Block();
            }

            SimpleLayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        //--------------------------------------
        // Methods (Draw Hero Property Windows)
        //--------------------------------------

        /// <summary>
        /// Draw the title window inside the hero kit editor (hero property).
        /// </summary>
        /// <param name="id">ID assigned to the GUI Window</param>
        private void DrawTitleWindowHeroProperty(int id)
        {
            SimpleLayout.BeginHorizontal(Box.StyleTitleWindow);
            TitleBlock.Block(heroKitProperty.name, 2);
            SimpleLayout.Space();
            TitleButtonBlock.Block(true);
            SimpleLayout.EndHorizontal();
        }
        /// <summary>
        /// Draw the menu window inside the hero kit editor (hero property).
        /// </summary>
        /// <param name="id">ID assigned to the GUI Window</param>
        private void DrawMenuWindowHeroProperty(int id)
        {
			scrollPosMenu = EditorGUILayout.BeginScrollView(scrollPosMenu, GUILayout.Width(windowMenu.width), GUILayout.Height((int)(Instance.position.height - titleHeight)));

			SimpleLayout.BeginVertical(Box.StyleMenu2, (int)(Instance.position.height - titleHeight));
            HeroPropertyMenuBlock.Block(heroKitProperty, this);
            SimpleLayout.EndVertical();

            EditorGUILayout.EndScrollView();
        }
        /// <summary>
        /// Draw the canvas window inside the hero kit editor (hero property).
        /// </summary>
        /// <param name="id">ID assigned to the GUI Window</param>
        private void DrawCanvasWindowHeroProperty(int id)
        {
			scrollPosCanvas = EditorGUILayout.BeginScrollView(scrollPosCanvas, GUILayout.Width(windowCanvas.width), GUILayout.Height((int)(Instance.position.height - titleHeight)));

			SimpleLayout.BeginVertical(Box.StyleCanvas, (int)(Instance.position.height - titleHeight));

            switch (HeroPropertyMenuBlock.variableID)
            {
                case -1:
                    SettingsBlock.Block();
                    break;
                case 0:
                    IntListBlock.Block(heroKitProperty);
                    break;
                case 1:
                    BoolListBlock.Block(heroKitProperty);
                    break;
                case 2:
                    StringListBlock.Block(heroKitProperty);
                    break;
                case 3:
                    GameObjectListBlock.Block(heroKitProperty);
                    break;
                case 4:
                    HeroObjectListBlock.Block(heroKitProperty);
                    break;
                case 5:
                    FloatListBlock.Block(heroKitProperty);
                    break;
                case 6:
                    UnityObjectListBlock.Block(heroKitProperty);
                    break;
            }

            SimpleLayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        //--------------------------------------
        // Methods (Draw Hero Action Windows)
        //--------------------------------------

        /// <summary>
        /// Draw the title window inside the hero kit editor (hero action).
        /// </summary>
        /// <param name="id">ID assigned to the GUI Window</param>
        private void DrawTitleWindowHeroAction(int id)
        {
            SimpleLayout.BeginHorizontal(Box.StyleTitleWindow);
            TitleBlock.Block(heroKitAction.name, 1);
            SimpleLayout.Space();
            SimpleLayout.EndHorizontal();
        }
        /// <summary>
        /// Draw the canvas window inside the hero kit editor (hero action).
        /// </summary>
        /// <param name="id">ID assigned to the GUI Window</param>
        private void DrawCanvasWindowHeroAction(int id)
        {
			scrollPosCanvas = EditorGUILayout.BeginScrollView(scrollPosCanvas, GUILayout.Width(windowCanvas.width), GUILayout.Height((int)(Instance.position.height - titleHeight)));
			SimpleLayout.BeginVertical(Box.StyleCanvas, (int)(Instance.position.height - titleHeight));
            HeroKitActionBlock.Block(heroKitAction);
            SimpleLayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        //--------------------------------------
        // Methods (Helper functions)
        //--------------------------------------

        /// <summary>
        /// Is the hero kit editor window open?
        /// </summary>
        public static bool IsOpen
        {
            get { return (Instance != null); }
        }
        /// <summary>
        /// Show hero kit editor window if it's not open.
        /// </summary>
        public static void ShowWindow()
        {
            GetWindow<HeroKitEditor>("HeroKit");
        }

        //--------------------------------------
        // Methods (Buttons, Assets)
        //--------------------------------------

        /// <summary>
        /// On double-click of asset, open asset in hero kit editor window.
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Object selectedAsset = EditorUtility.InstanceIDToObject(instanceID);
            bool foundAsset = false;

            if (selectedAsset is HeroObject)
            {
                ButtonClickHeroObjectAsset((HeroObject)selectedAsset);
                foundAsset = true;
            }

            else if (selectedAsset is HeroKitAction)
            {
                ButtonClickHeroActionAsset((HeroKitAction)selectedAsset);
                foundAsset = true;
            }

            else if (selectedAsset is HeroKitProperty)
            {
                ButtonClickHeroPropertyAsset((HeroKitProperty)selectedAsset);
                foundAsset = true;
            }

            return foundAsset;
        }
        /// <summary>
        /// Click Edit button in hero kit object inspector.
        /// </summary>
        /// <param name="selectedAsset"></param>
        public static void ButtonClickHeroObjectAsset(HeroObject selectedAsset)
        {
            heroType = 0;
            heroObject = selectedAsset;
            ShowWindow();
            HeroKitCommon.SaveHeroSession(heroObject);
        }
        /// <summary>
        /// Click Edit button in hero kit action inspector.
        /// </summary>
        /// <param name="selectedAsset"></param>
        public static void ButtonClickHeroActionAsset(HeroKitAction selectedAsset)
        {
            heroType = 1;
            heroKitAction = selectedAsset;
            ShowWindow();
        }
        /// <summary>
        /// Click Edit button in hero kit property inspector.
        /// </summary>
        /// <param name="selectedAsset"></param>
        public static void ButtonClickHeroPropertyAsset(HeroKitProperty selectedAsset)
        {
            heroType = 2;
            heroKitProperty = selectedAsset;
            ShowWindow();
        }

        //--------------------------------------
        // Methods (Window Resizing)
        //--------------------------------------

        /// <summary>
        /// Drag the window resizer left.
        /// </summary>
        private bool draggingLeft;
        /// <summary>
        /// Drag the window resizer right.
        /// </summary>
        private bool draggingRight;
        /// <summary>
        /// The window resizer. This exists between the menu and canvas windows. It resizes these windows.
        /// </summary>
        /// <param name="window">The window to resize.</param>
        /// <param name="eventType">Type of event.</param>
        /// <param name="right">Are we moving the window resizer right?</param>
        /// <param name="detectionRange">Size of the event detection area.</param>
        /// <returns>The new size of the window.</returns>
        private Rect HorizResizer(Rect window, EventType eventType, bool right = true, float detectionRange = 16f)
        {
            detectionRange *= 0.5f;
            Rect resizer = window;

            if (right)
            {
                resizer.xMin = resizer.xMax - detectionRange;
                resizer.xMax += detectionRange;
            }
            else
            {
                resizer.xMax = resizer.xMin + detectionRange;
                resizer.xMin -= detectionRange;
            }

            EditorGUIUtility.AddCursorRect(resizer, MouseCursor.ResizeHorizontal);

            // if mouse is no longer dragging, stop tracking direction of drag
            if (eventType == EventType.MouseUp)
            {
                draggingLeft = false;
                draggingRight = false;
            }

            // resize window if mouse is being dragged within resizor bounds
            if (eventType == EventType.MouseDrag)
            {
                if (Event.current.mousePosition.x > resizer.xMin && Event.current.mousePosition.x < resizer.xMax &&
                    Event.current.button == 0 || draggingLeft || draggingRight)
                {
                    if (Event.current.mousePosition.y > titleHeight)
                    {
                        if (right == !draggingLeft)
                        {
                            window.width = Event.current.mousePosition.x + Event.current.delta.x;
                            Repaint();
                            draggingRight = true;
                        }
                        else if (!right == !draggingRight)
                        {
                            window.width = window.width - (Event.current.mousePosition.x + Event.current.delta.x);
                            Repaint();
                            draggingLeft = true;
                        }
                    }
                }
            }

            return window;
        }
    }
} 