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
    /// Change the state of a hero kit object.
    /// </summary>
    public static class ChangeStateFields
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
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            HeroObject targetHeroObject = GetHeroObjectField.BuildFieldE("Change a different object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            if (targetHeroObject == null)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                SimpleLayout.Label("This field does not have a hero object type assigned to it.");
                SimpleLayout.EndVertical();
                return;
            }

            if (targetHeroObject.states.states == null || targetHeroObject.states.states.Count == 0)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                SimpleLayout.Label("This hero object has no states.");
                SimpleLayout.EndVertical();
                return;
            }

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetDropDownBField.BuildField("Change hero object to this state:", actionParams, heroAction.actionFields[2], new HeroField.StateListField(), targetHeroObject.states.states);
            SimpleLayout.EndVertical();
        }
    }
}