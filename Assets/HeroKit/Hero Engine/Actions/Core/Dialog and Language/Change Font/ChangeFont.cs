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
    /// Change the font used by a text field.
    /// </summary>
    public class ChangeFont : IHeroKitAction
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
        public static ChangeFont Create()
        {
            ChangeFont action = new ChangeFont();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;

            SceneObjectValueData objectData = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            Font font = ObjectValue.GetValue<Font>(heroKitObject, 2);

            // get the text box
            Text text = null;
            if (objectData.heroKitObject != null)
            {
                text = objectData.heroKitObject[0].GetHeroComponent<Text>("Text");
            }
            else if (objectData.gameObject != null)
            {
                text = heroKitObject.GetGameObjectComponent<Text>("Text", false, objectData.gameObject[0]);
            }

            // change the font
            if (text != null)
            {
                text.font = font;
            }

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string textName = (text != null) ? text.gameObject.name : "";
                string debugMessage = "Font: " + font +
                                      "Text Object: " + textName;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
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