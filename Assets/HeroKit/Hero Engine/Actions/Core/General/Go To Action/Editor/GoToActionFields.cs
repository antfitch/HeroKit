// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using HeroKit.Editor.ActionField;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Go to an action in this event.
    /// </summary>
    public static class GoToActionFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 1);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            GetIntegerField.BuildFieldA("Action ID:", actionParams, heroAction.actionFields[0]);
        }
    }
}