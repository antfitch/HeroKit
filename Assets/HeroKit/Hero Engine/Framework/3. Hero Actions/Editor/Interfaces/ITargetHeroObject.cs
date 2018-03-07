// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using HeroKit.Scene;
using UnityEngine;

namespace HeroKit.Editor.ActionBlockFields
{
    /// <summary>
    /// These properties are needed to create the target hero object field. 
    /// </summary>
    public interface ITargetHeroObject
    {
        HeroObject heroObject { get; set; }
        HeroObject targetHeroObject { get; set; }
        GameObject gameObject { get; set; }
        int objectType { get; set; }
        int objectID { get; set; }
        int heroGUID { get; set; }
        int propertyID { get; set; }
        string objectName { get; set; }
        int fieldID { get; set; }
        int fieldType { get; set; }
    }
}
