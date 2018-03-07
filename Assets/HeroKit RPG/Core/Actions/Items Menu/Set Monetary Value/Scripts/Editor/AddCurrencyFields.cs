// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using HeroKit.Editor.HeroField;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Assign a monetary value to an item.
    /// </summary>
    public static class AddCurrencyFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 2);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);

            // list of values
            string[] items = new string[RpgEditor.HeroKitCommon.moneyDatabase.propertiesList.properties.Count];
            for (int i = 0; i < RpgEditor.HeroKitCommon.moneyDatabase.propertiesList.properties.Count; i++)
            {
                items[i] = RpgEditor.HeroKitCommon.moneyDatabase.propertiesList.properties[i].itemProperties.strings.items[0].value;
            }
            GetDropDownField.BuildField("Currency type:", actionParams, heroAction.actionFields[0], new GenericListField(items));
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetIntegerField.BuildFieldA("New value:", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();
        }
    }
}