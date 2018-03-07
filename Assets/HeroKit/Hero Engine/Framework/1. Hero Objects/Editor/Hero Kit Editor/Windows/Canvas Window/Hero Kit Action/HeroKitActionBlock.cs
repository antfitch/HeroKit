// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using SimpleGUI.Fields;

namespace HeroKit.Editor
{
    /// <summary>
    /// Block for Hero Kit Actions that appears in Hero Kit Editor.
    /// </summary>
    internal static class HeroKitActionBlock
    {
        /// <summary>
        /// The hero kit action.
        /// </summary>
        private static HeroKitAction heroAction;

        /// <summary>
        /// Block to display on the canvas.
        /// </summary>
        /// <param name="heroKitAction">Hero kit action to display in the block.</param>
        public static void Block(HeroKitAction heroKitAction)
        {
            // exit early if object is null
            if (heroKitAction == null)
            {
                return;
            }

            // assign hero object to this class
            heroAction = heroKitAction;

            // draw components
            DrawBlock();
        }

        /// <summary>
        /// Draw the block.
        /// </summary>
        private static void DrawBlock()
        {
            DrawBody();
        }

        /// <summary>
        /// Draw the body of the block.
        /// </summary>
        private static void DrawBody()
        {
            SimpleLayout.BeginVertical(Box.StyleCanvasBox);

            SimpleLayout.BeginVertical(Box.StyleB);

            SimpleLayout.Label("Action (this is used during gameplay):");
            heroAction.action = SimpleLayout.ObjectField(heroAction.action, 400);

            SimpleLayout.Label("Action (this is used to create editor fields for action):");
            heroAction.actionFields = SimpleLayout.ObjectField(heroAction.actionFields, 400);

            SimpleLayout.EndVertical();

            // -------------

            SimpleLayout.BeginVertical(Box.StyleB);

            SimpleLayout.Label("Prefix (adds text in front of the action in the menu):");
            heroAction.title = SimpleLayout.TextField(heroAction.title);

            SimpleLayout.Label("Description (this appears below editor fields for action):");
            heroAction.description = SimpleLayout.TextArea(heroAction.description);

            SimpleLayout.EndVertical();

            // --------------

            SimpleLayout.BeginVertical(Box.StyleB);

            SimpleLayout.Label("Indent This Action (indents this action in menu. 0=no indent):");
            heroAction.indentThis = SimpleLayout.IntField(heroAction.indentThis);

            SimpleLayout.Label("Indent Next Action (indents the next action in menu. 0=no indent):");
            heroAction.indentNext = SimpleLayout.IntField(heroAction.indentNext);
            
            SimpleLayout.Label("Color (The color of this action in the menu):");
            heroAction.titleColor = SimpleLayout.ColorField(heroAction.titleColor, 400);

            SimpleLayout.EndVertical();

            // --------------

            SimpleLayout.Label("Version (version of this action + developer notes):");
            heroAction.version = SimpleLayout.TextArea(heroAction.version);

            SimpleLayout.EndVertical();
        }
    }
}