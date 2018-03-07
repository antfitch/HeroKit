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
    /// Create a pool for objects in the scene.
    /// </summary>
    public static class CreatePoolFields
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
            GetStringField.BuildFieldA("The name of the pool:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetIntegerField.BuildFieldA("The number of objects to add to the pool:", actionParams, heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            string[] items = { "Hero Object", "Prefab" };
            int result = GetDropDownField.BuildField("Object type: ", actionParams, heroAction.actionFields[2], new GenericListField(items));

            // prefab
            if (result == 2)
            {
                GetPrefabValue.BuildField("Prefab used by the object pool:", actionParams, heroAction.actionFields[3]);
            }
            SimpleLayout.EndVertical();
        }
    }
}