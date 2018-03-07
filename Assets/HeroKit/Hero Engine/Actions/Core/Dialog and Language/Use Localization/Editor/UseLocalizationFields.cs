// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using SimpleGUI;
using HeroKit.Editor.ActionField;
using UnityEngine;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Use localized text.
    /// </summary>   
    public static class UseLocalizationFields
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
            GetObjectValue.BuildField<TextAsset>("The CSV file to localize:", actionParams, heroAction.actionFields[0], false);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            bool localizeAudio = GetBoolValue.BuildField("Localize audio in messages? (If yes, enter path)", actionParams, heroAction.actionFields[1], true);
            if (localizeAudio)
            {
                GetStringField.BuildFieldA("", actionParams, heroAction.actionFields[2]);
            }
            SimpleLayout.EndVertical();
        }
    }
}