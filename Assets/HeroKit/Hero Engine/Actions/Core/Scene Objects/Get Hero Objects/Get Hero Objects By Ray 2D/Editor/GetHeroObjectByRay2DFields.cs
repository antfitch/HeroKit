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
    /// Get hero kit objects from the scene. Only get objects that are hit by a ray.
    /// </summary>
    public static class GetHeroObjectByRay2DFields
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            HeroAction heroAction = actionParams.heroAction;

            //-----------------------------------------
            // create the action fields if they don't exist
            //-----------------------------------------
            ActionCommon.CreateActionFieldsOnHeroObject(heroAction, 10);

            //-----------------------------------------
            // create the fields for this action
            //-----------------------------------------

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            int rayType = GetDropDownField.BuildField("What type of object the should ray originate from?", actionParams, heroAction.actionFields[6], new RayOriginField());
            // use main camera
            if (rayType == 1)
            {
                GetDropDownField.BuildField("Direction of the ray:", actionParams, heroAction.actionFields[4], new RayDirectionTypeBField());
            }
            // use another camera
            else if (rayType == 2)
            {
                GetHeroObjectField.BuildFieldA("Origin of the ray:", actionParams, heroAction.actionFields[3]);
                GetDropDownField.BuildField("Direction of the ray:", actionParams, heroAction.actionFields[4], new RayDirectionTypeBField());
            }
            // use hero object
            else if (rayType == 3)
            {
                GetHeroObjectField.BuildFieldA("Origin of the ray:", actionParams, heroAction.actionFields[3]);
                GetChildObjectField.BuildField("Is ray coming from a child object?", actionParams, heroAction.actionFields[7], heroAction.actionFields[8]);
                GetDropDownField.BuildField("Direction of the ray:", actionParams, heroAction.actionFields[4], new RayDirectionTypeField());
                GetIntegerField.BuildFieldA("Distance the ray should travel:", actionParams, heroAction.actionFields[5]);
            }
            
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetDropDownField.BuildField("Operation:", actionParams, heroAction.actionFields[0], new HeroObjectOperatorField());
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetHeroObjectField.BuildFieldB("Save the hero objects here:", actionParams, heroAction.actionFields[1]);
            GetIntegerField.BuildFieldA("Maximum number of hero objects to save:", actionParams, heroAction.actionFields[2]);
            SimpleLayout.EndVertical();

            SimpleLayout.BeginVertical(SimpleGUI.Fields.Box.StyleB);
            GetBoolValue.BuildField("Draw a line that represents the ray? (only visible in scene editor)", actionParams, heroAction.actionFields[9]);
            SimpleLayout.EndVertical();
        }

        private static bool showContent(HeroAction heroAction, int boolID)
        {
            if (heroAction.actionFields[boolID].bools != null &&
                heroAction.actionFields[boolID].bools.Count != 0 &&
                heroAction.actionFields[boolID].bools[0] == true)
                return true;
            else
                return false;
        }

    }
}