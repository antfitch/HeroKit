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
    /// [Summary about what your long hero action does.]
    /// </summary>    
    public static class HeroActionTemplateLongFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            //--------------------------------------------------
            // HOW TO MODIFY FOR YOUR OWN LONG ACTION
            // 1. Change the name of the class from HeroActionTemplateLongField to the name of your hero action field (ex. GetRayField).
            // 2. In section A, replace the 1 with the number of action fields your form is going to need. (if you don't know, use a large number and then change it when your form is complete.)
            // 3. In section B, delete the sample form fields and add your own.
            //--------------------------------------------------

            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // SECTION A (create the action fields if they don't exist)
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 1);

            //-----------------------------------------
            // SECTION B (create the fields for this action)
            //-----------------------------------------
            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetIntegerField.BuildFieldA("Seconds to wait:", actionParams, heroAction.actionFields[0]);
            SimpleLayout.EndVertical();
        }
    }
}