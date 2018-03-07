// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;

namespace HeroKit.Editor
{
    /// <summary>
    /// An interface for hero actions in the hero kit editor.
    /// </summary>
    public interface IHeroAction
    {
        void BuildField(HeroObject heroObject, int stateID, int eventID, int actionID, int fieldID);
    }
}
