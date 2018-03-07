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
    /// Play an event in the active state on a hero kit object.
    /// </summary>  
    public static class PlayEventByIDFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 4);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            HeroObject targetObject = GetHeroObjectField.BuildFieldE("Work with an event on a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            // Field: Event
            if (targetObject != null)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                GetIntegerField.BuildFieldA("State", actionParams, heroAction.actionFields[2]);
                GetIntegerField.BuildFieldA("Event", actionParams, heroAction.actionFields[3]);
                SimpleLayout.EndVertical();
            }
        }
    }
}