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
    /// Get a hero kit listener component on a UI object.
    /// </summary>
    public static class GetHeroKitListenerValuesFields 
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 6);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetSceneObjectValue.BuildField("Hero Kit Listener", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool getItemID = GetBoolValue.BuildField("Get Item ID (Int)?", actionParams, heroAction.actionFields[2], true);
            if (getItemID)
            {
                GetIntegerField.BuildFieldB("", actionParams, heroAction.actionFields[3]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool getItem = GetBoolValue.BuildField("Get Item (Hero Object)?", actionParams, heroAction.actionFields[4], true);
            if (getItem)
            {
                GetHeroObjectField.BuildFieldB("", actionParams, heroAction.actionFields[5]);
            }
            SimpleLayout.EndVertical();
        }
    }
}