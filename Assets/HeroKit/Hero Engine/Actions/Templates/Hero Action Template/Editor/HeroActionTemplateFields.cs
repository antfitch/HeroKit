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
    /// [Summary about what your hero action does.]
    /// </summary>  
    public static class HeroActionTemplateFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            //--------------------------------------------------
            // HOW TO MODIFY FOR YOUR OWN ACTION
            // 1. Change the name of the class from HeroActionTemplateField to the name of your hero action field (ex. GetRayField).
            // 2. In section A, replace the 4 with the number of action fields your form is going to need. (if you don't know, use a large number and then change it when your form is complete.)
            // 3. In section B, delete the sample form fields and add your own.
            //--------------------------------------------------

            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // SECTION A (create the action fields if they don't exist)
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 4);

            //-----------------------------------------
            // SECTION B (create the fields for this action)
            //-----------------------------------------

            // Sample form field 1 (requires two action fields)
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldE("Work with a different hero object?", actionParams, heroAction.actionFields[0], heroAction.actionFields[1]);
            SimpleLayout.EndVertical();

            // Sample form field 2 (requires one action field)
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolField.BuildFieldB("Change this bool:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();

            // Sample form field 3 (requires one action field)
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolField.BuildFieldA("To this value:", actionParams, heroAction.actionFields[3]);
            SimpleLayout.EndVertical();
        }
    }
}