// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
namespace HeroKit.Scene
{
    /// <summary>
    /// A container for variable lists that can be used by hero objects.
    /// </summary>
    [System.Serializable]
    public class HeroList
    {
        /// <summary>
        /// Determines whether the contents of the hero list container is visible.
        /// </summary>
        public bool visible;  
        /// <summary>
        /// List that contains integers fields.
        /// </summary>
        public IntList ints;              
        /// <summary>
        /// List that contains float fields.
        /// </summary>
        public FloatList floats; 
        /// <summary>
        /// List that contains bool fields.
        /// </summary>
        public BoolList bools; 
        /// <summary>
        /// List that contains string fields.
        /// </summary>
        public StringList strings; 
        /// <summary>
        /// List that contains game object fields.
        /// </summary>
        public GameObjectList gameObjects = new GameObjectList();
        /// <summary>
        /// List that contains hero object fields.
        /// </summary>
        public HeroObjectList heroObjects = new HeroObjectList();
        /// <summary>
        /// List that contains unity object fields.
        /// </summary>
        public UnityObjectList unityObjects = new UnityObjectList();

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeroList()
        {
            ints = new IntList();
            floats = new FloatList();
            bools = new BoolList();
            strings = new StringList();
            gameObjects = new GameObjectList();
            heroObjects = new HeroObjectList();
            unityObjects = new UnityObjectList();
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="list">The hero list to construct.</param>
        public HeroList(HeroList list)
        {
            visible = list.visible;
            ints = (list.ints == null) ? new IntList() : list.ints.Clone(list.ints);
            floats = (list.floats == null) ? new FloatList() : list.floats.Clone(list.floats);
            bools = (list.bools == null) ? new BoolList() : list.bools.Clone(list.bools);
            strings = (list.strings == null) ? new StringList() : list.strings.Clone(list.strings);
            gameObjects = (list.gameObjects == null) ? new GameObjectList() : list.gameObjects.Clone(list.gameObjects);
            heroObjects = (list.heroObjects == null) ? new HeroObjectList() : list.heroObjects.Clone(list.heroObjects);
            unityObjects = (list.unityObjects == null) ? new UnityObjectList() : list.unityObjects.Clone(list.unityObjects);
        }

        /// <summary>
        /// Clone the hero list, remove references.
        /// </summary>
        /// <param name="field">The hero list to clone.</param>
        /// <returns>The cloned hero list.</returns>
        public HeroList Clone(HeroList field)
        {
            HeroList temp = new HeroList();
            temp.visible = field.visible;
            temp.ints = (field.ints == null) ? new IntList() : field.ints.Clone(field.ints);
            temp.floats = (field.floats == null) ? new FloatList() : field.floats.Clone(field.floats);
            temp.bools = (field.bools == null) ? new BoolList() : field.bools.Clone(field.bools);
            temp.strings = (field.strings == null) ? new StringList() : field.strings.Clone(field.strings);
            temp.gameObjects = (field.gameObjects == null) ? new GameObjectList() : field.gameObjects.Clone(field.gameObjects);
            temp.heroObjects = (field.heroObjects == null) ? new HeroObjectList() : field.heroObjects.Clone(field.heroObjects);
            temp.unityObjects = (field.unityObjects == null) ? new UnityObjectList() : field.unityObjects.Clone(field.unityObjects);
            return temp;
        }
        /// <summary>
        /// Get the hero list to save.
        /// </summary>
        /// <returns>The hero list save data info.</returns>
        public HeroListSaveFile Save(HeroListSaveFile saveData)
        {
            saveData.ints = ints.Save();
            saveData.floats = floats.Save();
            saveData.bools = bools.Save();
            saveData.strings = strings.Save();
            return saveData;
        }
    }
  
    /// <summary>
    /// Contains data that can be saved in a file on the player's device
    /// </summary>
    [System.Serializable]
    public class HeroListSaveFile
    {
        /// <summary>
        /// An array that contains the ints in the int field list.
        /// </summary>
        public int[] ints;
        /// <summary>
        /// An array that contains the floats in the float field list.
        /// </summary>
        public float[] floats;
        /// <summary>
        /// An array that contains the bools in the bool field list.
        /// </summary>
        public bool[] bools;
        /// <summary>
        /// An array that contains the strings in the string field list.
        /// </summary>
        public string[] strings;
    }
}