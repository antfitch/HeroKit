// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using UnityEngine.UI;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Show an image.
    /// </summary>
    public class ShowImage : IHeroKitAction
    {
        // set up properties needed for all actions
        private HeroKitObject _heroKitObject;
        public HeroKitObject heroKitObject
        {
            get { return _heroKitObject; }
            set { _heroKitObject = value; }
        }
        private int _eventID;
        public int eventID
        {
            get { return _eventID; }
            set { _eventID = value; }
        }
        private bool _updateIsDone;
        public bool updateIsDone
        {
            get { return _updateIsDone; }
            set { _updateIsDone = value; }
        }

        // This is used by HeroKitCommon.GetAction() to add this action to the ActionDictionary. Don't delete!
        public static ShowImage Create()
        {
            ShowImage action = new ShowImage();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            String imageGroupPrefabName = "HeroKit Image Canvas";
            String imagePrefabName = "HeroKit Image Sprite";
            int imageID = IntegerFieldValue.GetValueA(heroKitObject, 0);

            // get the game object that contains the images
            GameObject imageGroup = AddImageGroup(imageGroupPrefabName);
            if (imageGroup != null)
            {
                // get the game object that contains the image
                GameObject targetObject = AddImage(imagePrefabName, imageID, imageGroup);
                if (targetObject != null)
                {
                    // get the image component on the game object
                    Image image = heroKitObject.GetGameObjectComponent<Image>("Image", false, targetObject);
                    if (image != null)
                    {
                        bool changeSprite = BoolValue.GetValue(heroKitObject, 1);
                        if (changeSprite)
                        {
                            UnityObjectField unityObject = UnityObjectFieldValue.GetValueA(heroKitObject, 2);
                            Sprite sprite = (unityObject.value != null) ? (Sprite)unityObject.value : null;
                            image.sprite = sprite;
                            RectTransform rectTransform =  heroKitObject.GetGameObjectComponent<RectTransform>("RectTransform", false, image.gameObject);
                            rectTransform.sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
                            rectTransform.localPosition = new Vector3();
                        }

                        bool flipSprite = BoolValue.GetValue(heroKitObject, 3);
                        if (flipSprite)
                        {
                            Vector3 eulerAngles = new Vector3();
                            eulerAngles = image.transform.eulerAngles;
                            eulerAngles.y += 180f;
                            image.transform.eulerAngles = eulerAngles;
                        }

                        bool changeScale = BoolValue.GetValue(heroKitObject, 4);
                        if (changeScale)
                        {
                            float newScale = FloatFieldValue.GetValueA(heroKitObject, 5);
                            image.transform.localScale = new Vector3(newScale, newScale, newScale) * 0.01f;
                        }

                        bool changeXPos = BoolValue.GetValue(heroKitObject, 6);
                        if (changeXPos)
                        {
                            float xPos = FloatFieldValue.GetValueA(heroKitObject, 7);
                            image.transform.localPosition = new Vector3(xPos, image.transform.localPosition.y, image.transform.localPosition.z);
                        }

                        bool changeYPos = BoolValue.GetValue(heroKitObject, 8);
                        if (changeYPos)
                        {
                            float yPos = FloatFieldValue.GetValueA(heroKitObject, 9);
                            image.transform.localPosition = new Vector3(image.transform.localPosition.x, yPos, image.transform.localPosition.z);
                        }

                    }

                    // change the color (alpha = transparent if image is null)
                    Color color = image.color;
                    color.a = (image.sprite == null) ? color.a = 0 : color.a = 1;
                    image.color = color;

                    // enable the game object
                    targetObject.SetActive(true);
                }
            }

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Image ID: " + imageID;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        private GameObject AddImageGroup(string prefabName)
        {
            GameObject gameObject = null;

            // get parent container for images. if it doesn't, add it.
            if (HeroKitDatabase.PersistentObjectDictionary.ContainsKey(prefabName))
            {
                gameObject = HeroKitDatabase.GetPersistentObject(prefabName);
            }
            else
            {
                // add image to scene if it doesn't already exist
                GameObject template = Resources.Load<GameObject>("Hero Templates/Components/" + prefabName);
                if (template == null)
                {
                    Debug.LogError("Can't add dialog box to scene because template for " + prefabName + " does not exist.");
                }

                gameObject = UnityEngine.Object.Instantiate(template, new Vector3(), new Quaternion());
                gameObject.name = prefabName;

                // add the object to the game object dictionary
                HeroKitDatabase.AddPersistentObject(prefabName, gameObject);

                // make it persistent
                HeroKitObject imageHKO = heroKitObject.GetGameObjectComponent<HeroKitObject>("HeroKitObject", false, gameObject);
                if (imageHKO == null)
                {
                    Debug.LogError("Can't make dialog box persistent because hero kit object component is missing.");
                }
                else
                {
                    MakePersistent makePersistent = new MakePersistent();
                    makePersistent.ExecuteOnTarget(imageHKO);
                }
            }

            return gameObject;
        }

        private GameObject AddImage(string prefabName, int imageID, GameObject group)
        {
            GameObject targetObject = null;

            // get parent container for images. if it doesn't, add it.
            if (HeroKitDatabase.PersistentObjectDictionary.ContainsKey(prefabName + imageID))
            {
                targetObject = HeroKitDatabase.GetPersistentObject(prefabName + imageID);
            }
            else
            {
                // add dialog box to scene if it doesn't already exist
                GameObject template = Resources.Load<GameObject>("Hero Templates/Components/" + prefabName);
                if (template == null)
                {
                    Debug.LogError("Can't add dialog box to scene because template for " + prefabName + " does not exist.");
                }

                targetObject = UnityEngine.Object.Instantiate(template, new Vector3(), new Quaternion(), group.transform);
                targetObject.name = prefabName + imageID;

                // add the object to the game object dictionary
                HeroKitDatabase.AddPersistentObject(prefabName + imageID, targetObject);
            }

            return targetObject;
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------

        // not used
        public bool RemoveFromLongActions()
        {
            throw new NotImplementedException();
        }
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}