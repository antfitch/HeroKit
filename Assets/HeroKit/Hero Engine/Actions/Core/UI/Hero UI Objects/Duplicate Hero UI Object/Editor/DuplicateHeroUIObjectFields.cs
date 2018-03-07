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
    /// Clone a UI hero kit object.
    /// </summary>
    public static class DuplicateHeroUIObjectFields 
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

            // get the prefab
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetPrefabValue.BuildField("Prefab:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();

            // get the menu
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetGameObjectField.BuildFieldA("Parent Game Object:", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            // how many instances do we need?
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetIntegerField.BuildFieldA("How many duplicates do we need?", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();

            //// should we allow these instances to be saved?
            //SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleA1);
            //GetBoolValue.BuildField("Allow these items to be saved?", actionParams, heroAction.actionField[6], true);
            //SimpleLayout.EndVertical();

            // increment item id?
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Increment Item ID on Hero Kit Listener?", actionParams, heroAction.actionFields[3], true);
            SimpleLayout.EndVertical();

            // where to send notifications
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            HeroObject targetObject = GetHeroObjectField.BuildFieldA("Send notifications to this hero object:", actionParams, heroAction.actionFields[4]);
            if (targetObject != null)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                GetEventField.BuildField("", actionParams, heroAction.actionFields[5], targetObject);
                SimpleLayout.EndVertical();
            }
            SimpleLayout.EndVertical();
        }
    }
}