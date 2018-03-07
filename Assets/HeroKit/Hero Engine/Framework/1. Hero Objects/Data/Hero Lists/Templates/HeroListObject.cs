// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
namespace HeroKit.Scene
{
    /// <summary>
    /// Template for a Hero List. 
    /// A hero list is used to store values that you can use in a hero object action.
    /// Any hero list you create will inherit the values in this template (ex. visible)
    /// </summary>
    [System.Serializable]
    public class HeroListObject
    {
        /// <summary>
        /// Determines whether the hero list object is visible in the hero object editor.
        /// </summary>
        public bool visible;
    }

    /// <summary>
    /// Template for a field inside a Hero List.
    /// This field is used to store a value that you can use in a hero object action.
    /// Each field inside a hero list contains the values in this template (ex. name)
    /// </summary>
    [System.Serializable]
    public class HeroListObjectField<T>
    {
        /// <summary>
        /// Name assigned to the field.
        /// </summary>
        public string name = "[Item Description]";
        /// <summary>
        /// Value assigned to the field.
        /// </summary>
        public T value;
    }
}