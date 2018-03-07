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
    /// Get a child game object on a game object.
    /// </summary>
    public class GetChildOnGameObject : IHeroKitAction
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
        public static GetChildOnGameObject Create()
        {
            GetChildOnGameObject action = new GetChildOnGameObject();
            return action;
        }

        // Gets objects in a scene that match a certerin criteria
        public int Execute(HeroKitObject hko)
        {
            // Get variables
            heroKitObject = hko;

            // Get values
            GameObject targetObject = GameObjectFieldValue.GetValueB(heroKitObject, 0);
            bool useName = BoolValue.GetValue(heroKitObject, 2);
            string name = (useName) ? StringFieldValue.GetValueA(heroKitObject, 3) : "";
            bool useTag = BoolValue.GetValue(heroKitObject, 4);
            string tag = (useTag) ? TagValue.GetValue(heroKitObject, 5) : "";
            bool useLayer = BoolValue.GetValue(heroKitObject, 6);
            int layer = (useLayer) ? DropDownListValue.GetValue(heroKitObject, 7) - 1 : 0;
            GameObject childObject = null;
            bool runThis = (targetObject != null);

            if (runThis)
            {
                // get the child object
                childObject = GetChild(targetObject, useName, name, useTag, tag, useLayer, layer);

                // save the game object
                GameObjectFieldValue.SetValueB(heroKitObject, 8, childObject);
            }

            //-----------------------------------------
            // debugging stuff
            //-----------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string strLayer = (useLayer) ? layer.ToString() : "";
                string debugMessage = "Child: " + childObject + "\n" +
                                      "With Tag: " + tag + "\n" +
                                      "On Layer: " + strLayer + "\n" +
                                      "With Name: " + name;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        private GameObject GetChild(GameObject targetObject, bool useName, string name, bool useTag, string tag, bool useLayer, int layer)
        {
            // go through children and search for match
            GameObject targetGameObject = null;
            int children = targetObject.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                Transform childObject = targetObject.transform.GetChild(i);
                bool haveCorrectName = false;
                bool haveCorrectTag = false;
                bool haveCorrectLayer = false;
                bool needName = false;
                bool needTag = false;
                bool needLayer = false;

                if (useName)
                {
                    needName = true;
                    if (childObject.name == name)
                    {
                        haveCorrectName = true;
                    }
                }

                if (useTag)
                {
                    needTag = true;
                    if (childObject.tag == tag)
                    {
                        haveCorrectTag = true;
                    }
                }

                if (useLayer)
                {
                    needLayer = true;
                    if (childObject.gameObject.layer == layer)
                    {
                        haveCorrectLayer = true;
                    }
                }

                if (needName == haveCorrectName && needTag == haveCorrectTag && needLayer == haveCorrectLayer)
                {
                    targetGameObject = childObject.gameObject;
                    break;
                }
            }

            return targetGameObject;
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