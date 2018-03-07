// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.Collections.Generic;

namespace HeroKit.Editor
{
    /// <summary>
    /// An interface for drop down lists in the hero kit editor.
    /// </summary>
    public interface IDropDownListB<T>
    {
        int SetValues(int selectedValue, List<T> list, int titleWidth);
    }
}
