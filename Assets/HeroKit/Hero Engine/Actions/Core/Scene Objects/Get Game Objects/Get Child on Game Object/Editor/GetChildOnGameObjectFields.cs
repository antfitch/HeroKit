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
    /// Get a child game object on a parent game object.
    /// </summary>   
    public static class GetChildOnGameObjectFields 
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 9);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetGameObjectField.BuildFieldB("The game object that contains the child game object:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool useName = GetBoolValue.BuildField("Get child game object with a specific name?", actionParams, heroAction.actionFields[2], true);
            if (useName)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                GetStringField.BuildFieldA("Name:", actionParams, heroAction.actionFields[3], true, false, -80);
                SimpleLayout.EndVertical();
            }

            bool useTag = GetBoolValue.BuildField("Get child game object with a specific tag?", actionParams, heroAction.actionFields[4], true);
            if (useTag)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                GetTagField.BuildField("Tag:", actionParams, heroAction.actionFields[5], true);
                SimpleLayout.EndVertical();
            }

            bool useLayer = GetBoolValue.BuildField("Get child game object on a specific layer?", actionParams, heroAction.actionFields[6], true);
            if (useLayer)
            {
                SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
                GetDropDownField.BuildField("Layer", actionParams, heroAction.actionFields[7], new LayerListField(), true);
                SimpleLayout.EndVertical();
            }
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetGameObjectField.BuildFieldB("Save the child game object here:", actionParams, heroAction.actionFields[8]);
            SimpleLayout.EndVertical();
        }
    }
}