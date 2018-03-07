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
    /// Navigate an object to a position in the scene. 
    /// </summary>
    public static class NavigateTowardPositionOnFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 8);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Move a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetCoordinatesField.BuildField("", actionParams,
                                            heroAction.actionFields[2], heroAction.actionFields[3],
                                            heroAction.actionFields[4], heroAction.actionFields[5],
                                            heroAction.actionFields[6], heroAction.actionFields[7]);
            SimpleLayout.EndVertical();
        }
    }
}