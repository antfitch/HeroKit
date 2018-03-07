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
    /// Spawn an object in the scene.
    /// </summary>
    public static class SpawnObjectFields 
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

            // spawn object from a pool?
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool usePool = GetBoolValue.BuildField("Spawn from pool? (Yes=True, No=False)", actionParams, heroAction.actionFields[1], true);
            if (usePool) GetStringField.BuildFieldA("", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();
            
            // spawn from hero object (1) or prefab (2)
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            string[] items = { "Spawn object from Hero Object", "Spawn object from Prefab" };
            int result = GetDropDownField.BuildField("Spawn the object from:", actionParams, heroAction.actionFields[0], new GenericListField(items));
            if (result == 1)
            {
                GetHeroObjectField.BuildFieldC("Hero Object:", actionParams, heroAction.actionFields[3]);
                GetBoolValue.BuildField("Debug this Hero Object?", actionParams, heroAction.actionFields[4], true);
                GetBoolValue.BuildField("Don't save this Hero Object?", actionParams, heroAction.actionFields[5], true);
            }
            else if (result == 2)
            {
                if (!usePool) GetPrefabValue.BuildField("Prefab:", actionParams, heroAction.actionFields[5]);
            }
            SimpleLayout.EndVertical();

            // change position?
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changePos = GetBoolValue.BuildField("Change position?", actionParams, heroAction.actionFields[6], true);
            if (changePos)
            {
                GetCoordinatesField.BuildField("", actionParams,
                                                heroAction.actionFields[7], heroAction.actionFields[8],
                                                heroAction.actionFields[9], heroAction.actionFields[10],
                                                heroAction.actionFields[11], heroAction.actionFields[12]);
            }
            SimpleLayout.EndVertical();

            // change rotation?
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool changeRotation = GetBoolValue.BuildField("Change rotation?", actionParams, heroAction.actionFields[13], true);
            if (changeRotation)
            {
                GetCoordinatesField.BuildField("", actionParams,
                                                heroAction.actionFields[14], heroAction.actionFields[15],
                                                heroAction.actionFields[16], heroAction.actionFields[17],
                                                heroAction.actionFields[18], heroAction.actionFields[19]);
            }
            SimpleLayout.EndVertical();
        }
    }
}