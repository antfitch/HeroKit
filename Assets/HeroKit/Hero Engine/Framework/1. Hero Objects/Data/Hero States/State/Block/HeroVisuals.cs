// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace HeroKit.Scene
{
    /// <summary>
    /// Hero Visuals represents the visual elements for a hero kit object.
    /// You assign visuals in a hero object's state. These visuals are applied to a hero kit object at runtime.
    /// The visuals in the first state appear on the hero kit object in the Unity editor before runtime.
    /// </summary>
    [System.Serializable]
    public class HeroVisuals
    {
        // --------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------

        /// <summary>
        /// The type of visuals we are working with. (ex. 3D, 2D)
        /// </summary>
        public int visualType = 0;
        /// <summary>
        /// The type of visuals we are working with. (ex. none, prefab, use what's on the object)
        /// </summary>
        public int imageType = 0;
        /// <summary>
        /// The type of rigidbody we are working with. (ex. none, default, custom)
        /// </summary>
        public int rigidbodyType = 0;

        /// <summary>
        /// The prefab that contains the visuals. (if image type is a prefab)
        /// </summary>
        public GameObject prefab = null;
        /// <summary>
        /// Does prefab have a mesh? (if image type is a prefab)
        /// </summary>
        public bool hasMesh = false;
        /// <summary>
        /// The mesh on the prefab. (if image type is a prefab)
        /// </summary>
        public Mesh imageMesh = null;

        /// <summary>
        /// Does prefab have an animator? (if image type is a prefab)
        /// </summary>
        public bool hasAnimator = false;
        /// <summary>
        /// The animator on the prefab. (if image type is a prefab)
        /// </summary>
        public Animator animator = null;
        /// <summary>
        /// The animator controller in the animator. (if image type is a prefab)
        /// </summary>
        public RuntimeAnimatorController animatorController = null;
        /// <summary>
        /// The avatar in the animator. (if image type is a prefab)
        /// </summary>
        public Avatar avatar = null;

        /// <summary>
        /// The 3D rigidbody for the hero kit object.
        /// </summary>
        public Rigidbody rigidbody = null;

        /// <summary>
        /// The 2D rigidbody for the hero kit object.
        /// </summary>
        public Rigidbody2D rigidbody2D = null;

        // --------------------------------------------------------------
        // Constructors
        // --------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeroVisuals() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="list">The hero visuals to construct.</param>
        public HeroVisuals(HeroVisuals field)
        {
            visualType = field.visualType;
            imageType = field.imageType;
            rigidbodyType = field.rigidbodyType;
            imageMesh = field.imageMesh;
            prefab = field.prefab;
            animator = field.animator;
            animatorController = field.animatorController;
            avatar = field.avatar;
            hasMesh = field.hasMesh;
            hasAnimator = field.hasAnimator;
            rigidbody = field.rigidbody;
            rigidbody2D = field.rigidbody2D;
        }

        // --------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Clone the hero visuals, remove references.
        /// </summary>
        /// <param name="field">The hero visuals to clone.</param>
        /// <returns>The cloned hero visuals.</returns>
        public HeroVisuals Clone(HeroVisuals field)
        {
            HeroVisuals temp = new HeroVisuals();
            temp.visualType = field.visualType;
            temp.imageType = field.imageType;
            temp.rigidbodyType = field.rigidbodyType;
            temp.imageMesh = field.imageMesh;
            temp.prefab = field.prefab;
            temp.animator = field.animator;
            temp.animatorController = field.animatorController;
            temp.avatar = field.avatar;
            temp.hasMesh = field.hasMesh;
            temp.hasAnimator = field.hasAnimator;
            temp.rigidbody = field.rigidbody;
            temp.rigidbody2D = field.rigidbody2D;
            return temp;
        }
    }
}