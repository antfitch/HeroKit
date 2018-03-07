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
    /// Set values in a hero kit listener component on a UI object.
    /// </summary>
    public static class SetHeroKitListenerValuesFields 
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 11);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetSceneObjectValue.BuildField("Hero Kit Listener", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool setItemID = GetBoolValue.BuildField("Set Item ID?", actionParams, heroAction.actionFields[2], true);
            if (setItemID)
            {
                GetIntegerField.BuildFieldA("", actionParams, heroAction.actionFields[3]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool setItem = GetBoolValue.BuildField("Set Item?", actionParams, heroAction.actionFields[9], true);
            if (setItem)
            {
                GetHeroObjectField.BuildFieldC("", actionParams, heroAction.actionFields[10]);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            HeroObject targetObject = null;
            bool setNotify = GetBoolValue.BuildField("Set object to receive notifications and the event to play?", actionParams, heroAction.actionFields[6], true);
            if (setNotify)
            {
                targetObject = GetHeroObjectField.BuildFieldA("", actionParams, heroAction.actionFields[7]);
                if (targetObject != null)
                {
                    GetEventField.BuildField("", actionParams, heroAction.actionFields[8], targetObject);
                }
            }
            SimpleLayout.EndVertical();
        }
    }
}