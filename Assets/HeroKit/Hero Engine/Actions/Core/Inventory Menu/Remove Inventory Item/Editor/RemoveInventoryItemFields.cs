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
    /// Remove an item from the inventory menu.
    /// </summary>    
    public static class RemoveInventoryItemFields 
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 3);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            // assign values in the hero kit object
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldC("Item to remove:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();

            // assign values in the hero kit object
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool addMultiple = GetBoolValue.BuildField("Remove more than one item?", actionParams, heroAction.actionFields[1], true);
            if (addMultiple)
            {
                GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[2]);
            }
            SimpleLayout.EndVertical();
        }
    }
}