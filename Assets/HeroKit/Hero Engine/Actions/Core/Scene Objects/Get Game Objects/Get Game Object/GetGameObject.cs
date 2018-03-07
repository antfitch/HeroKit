// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Get a game object in the scene.
    /// </summary>
    public class GetGameObject : IHeroKitAction
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
        public static GetGameObject Create()
        {
            GetGameObject action = new GetGameObject();
            return action;
        }

        // Gets objects in a scene that match a certerin criteria
        public int Execute(HeroKitObject hko)
        {
            // Get variables
            heroKitObject = hko;

            bool useName = BoolValue.GetValue(heroKitObject, 0);
            bool useTag = BoolValue.GetValue(heroKitObject, 2);
            bool useLayer = BoolValue.GetValue(heroKitObject, 4);
            string name = "";
            string tag = "";
            int layer = 0;

            if (useName) name = StringFieldValue.GetValueA(heroKitObject, 1);
            if (useTag) tag = TagValue.GetValue(heroKitObject, 3);
            if (useLayer) layer = DropDownListValue.GetValue(heroKitObject, 5)-1;

            GameObject targetGameObject = null;
            GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

            if (useName && useTag && useLayer)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    if (gameObjects[i].name == name && gameObjects[i].layer == layer && gameObjects[i].tag == tag)
                    {
                        targetGameObject = gameObjects[i];
                        break;
                    }

                }
            }

            else if (useName && useTag && !useLayer)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    if (gameObjects[i].name == name && gameObjects[i].tag == tag)
                    {
                        targetGameObject = gameObjects[i];
                        break;
                    }

                }
            }

            else if (useName && !useTag && useLayer)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    if (gameObjects[i].name == name && gameObjects[i].layer == layer)
                    {
                        targetGameObject = gameObjects[i];
                        break;
                    }

                }
            }

            else if (!useName && useTag && useLayer)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    if (gameObjects[i].tag == tag && gameObjects[i].layer == layer)
                    {
                        targetGameObject = gameObjects[i];
                        break;
                    }
                }
            }

            else if (!useName && !useTag && useLayer)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    if (gameObjects[i].layer == layer)
                    {
                        targetGameObject = gameObjects[i];
                        break;
                    }
                }
            }

            else if (!useName && useTag && !useLayer)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    if (gameObjects[i].tag == tag)
                    {
                        targetGameObject = gameObjects[i];
                        break;
                    }
                }
            }

            else if (useName && !useTag && !useLayer)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    if (gameObjects[i].name == name)
                    {
                        targetGameObject = gameObjects[i];
                        break;
                    }
                }
            }

            GameObjectFieldValue.SetValueB(heroKitObject, 6, targetGameObject);

            //-----------------------------------------
            // debugging stuff
            //-----------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string strLayer = (useLayer) ? layer.ToString() : "";
                string debugMessage = "Game Object With Tag: " + tag + "\n" +
                                      "Game Object On Layer: " + strLayer + "\n" +
                                      "Game Object With Name: " + name + "\n" +
                                      "Game Object Found: " + targetGameObject;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }



        // Not used
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