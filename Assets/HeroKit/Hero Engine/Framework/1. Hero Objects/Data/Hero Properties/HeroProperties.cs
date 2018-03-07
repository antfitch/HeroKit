// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
namespace HeroKit.Scene
{
    /// <summary>
    /// A container for properties that can be used by hero objects.
    /// </summary>
    [System.Serializable]
    public class HeroProperties
    {
        /// <summary>
        /// A hero property object.
        /// </summary>
        public HeroKitProperty propertyTemplate;
        /// <summary>
        /// The hero list inside the hero property object.
        /// </summary>
        public HeroList itemProperties;

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeroProperties() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="list">The hero property to construct.</param>
        public HeroProperties(HeroProperties field)
        {
            propertyTemplate = field.propertyTemplate;
            itemProperties = field.itemProperties.Clone(field.itemProperties);
        }

        /// <summary>
        /// Clone the hero properties, remove references.
        /// </summary>
        /// <param name="field">The hero properties to clone.</param>
        /// <returns>The cloned hero properties.</returns>
        public HeroProperties Clone(HeroProperties field)
        {
            HeroProperties temp = new HeroProperties();
            temp.propertyTemplate = field.propertyTemplate;
            temp.itemProperties = field.itemProperties.Clone(field.itemProperties);  
            return temp;
        }
        /// <summary>
        /// Get the hero properties to save.
        /// </summary>
        /// <returns>The hero properties save data info.</returns>
        public HeroListSaveFile Save(HeroListSaveFile saveData)
        {
            saveData.ints = itemProperties.ints.Save();
            saveData.floats = itemProperties.floats.Save();
            saveData.bools = itemProperties.bools.Save();
            saveData.strings = itemProperties.strings.Save();
            return saveData;
        }
    }
}