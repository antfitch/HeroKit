// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// This script represents an Else action.
    /// There are no fields in this action, but a value
    /// must be stored that determines whether this action should be run or skipped.
    /// </summary>
    public static class EndDoWhileFields 
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

            // holds the id of the action that is the head of the loop
            ActionCommon.CreateLoopFieldData(heroAction.actionFields[0]);

            SimpleLayout.Label("There are no fields for this action.");
        }
    }
}