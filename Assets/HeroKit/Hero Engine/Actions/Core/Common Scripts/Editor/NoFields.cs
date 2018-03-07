// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using SimpleGUI;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// Use this if there are no editor fields for an action.
    /// </summary>  
    public static class NoFields 
    {
        public static void BuildField(HeroActionParams actionParams)
        {
            SimpleLayout.Label("There are no fields for this action.");
        }
    }
}