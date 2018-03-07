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
    /// Get a game object.
    /// </summary>
    public static class GetGameObjectFields 
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 20);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool useName = GetBoolValue.BuildField("Get Game Object with a specific name?", actionParams, heroAction.actionFields[0], true);
            if (useName)
            {
                GetStringField.BuildFieldA("", actionParams, heroAction.actionFields[1], true);
            }

            bool useTag = GetBoolValue.BuildField("Get Game Object with a specific tag?", actionParams, heroAction.actionFields[2], true);
            if (useTag)
            {
                GetTagField.BuildField("", actionParams, heroAction.actionFields[3], true);
            }

            bool useLayer = GetBoolValue.BuildField("Get Game Object on a specific layer?", actionParams, heroAction.actionFields[4], true);
            if (useLayer)
            {
                GetDropDownField.BuildField("", actionParams, heroAction.actionFields[5], new LayerListField(), true);
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetGameObjectField.BuildFieldB("Save the game object here:", actionParams, heroAction.actionFields[6]);
            SimpleLayout.EndVertical();
        }
    }
}