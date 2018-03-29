// Editor Window Code
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;

namespace HeroKit.RpgEditor
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

        /// <summary>
        /// The type of object selected in the main menu. 
        /// settings (0), items (1), affixes (2), suffixes (3), xxx (4), xxx (5), xxx (6)
        /// </summary>
        public static int typeID = 0;

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

            // get all components needed for this editor
            HeroKitCommon.LoadHeroKitRpgDatabases();
        }
        /// <summary>
        /// Display the contents of the hero kit editor.
        /// </summary>
        public void OnGUI()
        {
            EventType eventType = Event.current.type;

            // main menu (no left window)
            if (typeID == 0)
            {
                windowTitle = new Rect(0, 0, Instance.position.width - 3, titleHeight);
                windowCanvas = new Rect(0, windowTitle.height, Instance.position.width - 3, (int)Instance.position.height);

                BeginWindows();
                windowTitle = GUILayout.Window(3, windowTitle, DrawTitleWindowItem, "", Box.StyleDefault);
                windowCanvas = GUILayout.Window(2, windowCanvas, DrawCanvasWindowItem, "", Box.StyleDefault);
                EndWindows();
            }
            // items database, etc (left and right window)
            else if (typeID >= 1)
            {
                windowTitle = new Rect(0, 0, Instance.position.width - 3, titleHeight);
                windowMenu = new Rect(0, windowTitle.height, windowMenu.width, (int)Instance.position.height);
                windowCanvas = new Rect(windowMenu.width, windowTitle.height, Instance.position.width - windowMenu.width, (int)Instance.position.height);

                BeginWindows();
                windowTitle = GUILayout.Window(3, windowTitle, DrawTitleWindowItem, "", Box.StyleDefault);
                windowMenu = HorizResizer(windowMenu, eventType); //right
                windowMenu = HorizResizer(windowMenu, eventType, false); //left
                windowMenu = GUILayout.Window(1, windowMenu, DrawMenuWindowItem, "", Box.StyleMenu);
                windowCanvas = GUILayout.Window(2, windowCanvas, DrawCanvasWindowItem, "", Box.StyleDefault);
                EndWindows();
            }


            // check for key down events for main menu
            if (Event.current.type == EventType.KeyDown)
                HeroKitMenuBlock.changeSelection();

            // update GUI if it has changed
            if (GUI.changed)
            {
                if (heroObject != null)
                    EditorUtility.SetDirty(heroObject);
            }
        }

        // --------------------------------------------------------------
        // Methods (Draw Hero Object Windows)
        // --------------------------------------------------------------

        /// <summary>
        /// Draw the title window inside the hero kit editor (hero object).
        /// </summary>
        /// <param name="id">ID assigned to the GUI Window</param>
        private void DrawTitleWindowItem(int id)
        {
            string name = "";
            if (typeID == 0)
                name = "Main Menu";
            else if (typeID == 1)
                name = "Item Database";
            else if (typeID == 2)
                name = "Affix Database";
            else if (typeID == 3)
                name = "Affix Type Database";
            else if (typeID == 4)
                name = "Stats Database";
            else if (typeID == 5)
                name = "Meter Stats Databae";
            else if (typeID == 6)
                name = "Currency Type Databae";
            else if (typeID == 7)
                name = "Item Type Database";
            else if (typeID == 8)
                name = "Element Database";
            else if (typeID == 9)
                name = "Condition Database";
            else if (typeID == 10)
                name = "Weapons Type";
            else if (typeID == 11)
                name = "Weapons";
            else if (typeID == 12)
                name = "Armor Type";
            else if (typeID == 13)
                name = "Armor";
            else if (typeID == 14)
                name = "Ammunition Type";
            else if (typeID == 15)
                name = "Ammunition";
            else if (typeID == 16)
                name = "Ability Type";
            else if (typeID == 17)
                name = "Ability";
            else if (typeID == 18)
                name = "Formula";
            else if (typeID == 19)
                name = "Class";
            else if (typeID == 20)
                name = "Element Type";
            else if (typeID == 21)
                name = "Condition Type";
            else if (typeID == 22)
                name = "Subclass";
            else if (typeID == 23)
                name = "Race";
            else if (typeID == 24)
                name = "Subrace";
            else if (typeID == 25)
                name = "Alignment";
            else if (typeID == 26)
                name = "Character Type";
            else if (typeID == 27)
                name = "Character";

            SimpleLayout.BeginHorizontal(Box.StyleTitleWindow);
            TitleButtonBlock.Block();
            TitleBlock.Block(name, 0);
            SimpleLayout.Space();            
            SimpleLayout.EndHorizontal();
        }
        /// <summary>
        /// Draw the menu window inside the hero kit editor (hero object).
        /// </summary>
        /// <param name="id">ID assigned to the GUI Window</param>
        private void DrawMenuWindowItem(int id)
        {
            scrollPosMenu = EditorGUILayout.BeginScrollView(scrollPosMenu, GUILayout.Width(windowMenu.width), GUILayout.Height((int)(Instance.position.height - titleHeight)));
            SimpleLayout.BeginVertical(Box.StyleMenu2, (int)(Instance.position.height - titleHeight));
            HeroKitMenuBlock.Block(heroObject, this);
            SimpleLayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }
        /// <summary>
        /// Draw the canvas window inside the hero kit editor (hero object).
        /// </summary>
        /// <param name="id">ID assigned to the GUI Window</param>
        private void DrawCanvasWindowItem(int id)
        {
            scrollPosCanvas = EditorGUILayout.BeginScrollView(scrollPosCanvas, GUILayout.Width(windowCanvas.width), GUILayout.Height((int)(Instance.position.height - titleHeight)));

            SimpleLayout.BeginVertical(Box.StyleCanvas, (int)(Instance.position.height - titleHeight));      

            // draw settings
            if (typeID == 0)
            {
                SettingsBlock.Block();
            }
            // draw items database
            else if (typeID == 1)
            {
                ItemBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw affixes database
            else if (typeID == 2)
            {
                AffixesBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw affix type database
            else if (typeID == 3)
            {
                AffixesTypeBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw stats database
            else if (typeID == 4)
            {
                StatsBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw meter stats database
            else if (typeID == 5)
            {
                MeterBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw money stats database
            else if (typeID == 6)
            {
                MoneyBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw item type database
            else if (typeID == 7)
            {
                ItemTypeBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw elements database
            else if (typeID == 8)
            {
                ElementBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw conditions database
            else if (typeID == 9)
            {
                ConditionBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw weapons type database
            else if (typeID == 10)
            {
                WeaponTypeBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw weapons database
            else if (typeID == 11)
            {
                WeaponBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw armor type database
            else if (typeID == 12)
            {
                ArmorTypeBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw armor database
            else if (typeID == 13)
            {
                ArmorBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw ammunition type database
            else if (typeID == 14)
            {
                AmmunitionTypeBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw ammunition database
            else if (typeID == 15)
            {
                AmmunitionBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw ability type database
            else if (typeID == 16)
            {
                AbilityTypeBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw ability database
            else if (typeID == 17)
            {
                AbilityBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw formula database
            else if (typeID == 18)
            {
                FormulaBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw class database
            else if (typeID == 19)
            {
                ClassBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw element type database
            else if (typeID == 20)
            {
                ElementTypeBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw condition type database
            else if (typeID == 21)
            {
                ConditionTypeBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw subclass database
            else if (typeID == 22)
            {
                SubclassBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw race database
            else if (typeID == 23)
            {
                RaceBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw subrace database
            else if (typeID == 24)
            {
                SubraceBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw alignment database
            else if (typeID == 25)
            {
                AlignmentBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw character type database
            else if (typeID == 26)
            {
                CharacterTypeBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }
            // draw character database
            else if (typeID == 27)
            {
                CharacterBlock.Block(heroObject, HeroKitMenuBlock.itemID);
            }

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
            if (!IsOpen)
                GetWindow<HeroKitEditor>("HeroKit RPG");
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