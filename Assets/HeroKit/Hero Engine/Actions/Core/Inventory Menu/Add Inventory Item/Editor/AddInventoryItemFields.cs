// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Add an item to the inventory menu.
    /// </summary>
    public static class AddInventoryItemFields 
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 7);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            // assign values in the hero kit object
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldC("Item to add:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();

            // assign values in the hero kit object
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool addMultiple = GetBoolValue.BuildField("Add more than one item?", actionParams, heroAction.actionFields[1], true);
            if (addMultiple)
            {
                GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[2]);
            }
            SimpleLayout.EndVertical();

            // where to send notifications
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            HeroObject targetObject = GetHeroObjectField.BuildFieldA("If item used, send notifications to this hero object:", actionParams, heroAction.actionFields[3]);
            if (targetObject != null)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleC);
                GetEventField.BuildField("", actionParams, heroAction.actionFields[4], targetObject);
                SimpleLayout.EndVertical();
            }
            SimpleLayout.EndVertical();
        }
    }
}