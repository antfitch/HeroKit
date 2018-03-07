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
    /// Change options for messages.
    /// </summary>
    public class ChangeMessageOptions : IHeroKitAction
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
        public static ChangeMessageOptions Create()
        {
            ChangeMessageOptions action = new ChangeMessageOptions();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;

            // write messages?
            bool print = BoolValue.GetValue(heroKitObject, 0);
            if (print)
            {
                HeroKitCommonRuntime.writeMessage = BoolValue.GetValue(heroKitObject, 1);
                if (HeroKitCommonRuntime.writeMessage)
                {
                    float messageSpeed = SliderValue.GetValue(heroKitObject, 2);
                    if (messageSpeed < 0) messageSpeed = 0;
                    if (messageSpeed > 100) messageSpeed = 100;

                    // 0=100, 100=0. Then change to 0 to 1.
                    float waitTime = (100 - messageSpeed) * 0.01f;

                    HeroKitCommonRuntime.messageWaitTime = waitTime;
                }
            }

            // change message alignment?
            bool changeAlignment = BoolValue.GetValue(heroKitObject, 3);
            if (changeAlignment)
            {
                int alignID = DropDownListValue.GetValue(heroKitObject, 4);
                switch (alignID)
                {
                    case 1: 
                        HeroKitCommonRuntime.messageAlignment = TextAnchor.UpperLeft;
                        break;
                    case 2:
                        HeroKitCommonRuntime.messageAlignment = TextAnchor.UpperCenter;
                        break;
                    case 3:
                        HeroKitCommonRuntime.messageAlignment = TextAnchor.UpperRight;
                        break;
                    case 4:
                        HeroKitCommonRuntime.messageAlignment = TextAnchor.MiddleLeft;
                        break;
                    case 5:
                        HeroKitCommonRuntime.messageAlignment = TextAnchor.MiddleCenter;
                        break;
                    case 6:
                        HeroKitCommonRuntime.messageAlignment = TextAnchor.MiddleRight;
                        break;
                    case 7:
                        HeroKitCommonRuntime.messageAlignment = TextAnchor.LowerLeft;
                        break;
                    case 8:
                        HeroKitCommonRuntime.messageAlignment = TextAnchor.LowerCenter;
                        break;
                    case 9:
                        HeroKitCommonRuntime.messageAlignment = TextAnchor.LowerRight;
                        break;
                }
            }

            // change background alpha
            bool changeBackgroundAlpha = BoolValue.GetValue(heroKitObject, 5);
            if (changeBackgroundAlpha)
            {
                // get new alpha
                float alpha = SliderValue.GetValue(heroKitObject, 6);

                // keep alpha between 0 - 100
                if (alpha > 100)
                    alpha = 100;
                else if (alpha < 0)
                    alpha = 0;

                // switch to 0 - 1 scale
                alpha *= .01f;

                // change alpha
                HeroKitCommonRuntime.messageBackgroundAlpha = alpha;
                HeroKitCommonRuntime.changeMessageBackgroundTransparency = true;
            }

            // change background alpha
            bool changeButtonAlpha = BoolValue.GetValue(heroKitObject, 19);
            if (changeButtonAlpha)
            {
                // get new alpha
                float alpha = SliderValue.GetValue(heroKitObject, 20);

                // keep alpha between 0 - 100
                if (alpha > 100)
                    alpha = 100;
                else if (alpha < 0)
                    alpha = 0;

                // switch to 0 - 1 scale
                alpha *= .01f;

                // change alpha
                HeroKitCommonRuntime.messageButtonAlpha = alpha;
                HeroKitCommonRuntime.changeMessageButtonTransparency = true;
            }

            // change background image
            bool changeBackgroundImage = BoolValue.GetValue(heroKitObject, 7);
            if (changeBackgroundImage)
            {
                UnityObjectField unityObject = UnityObjectFieldValue.GetValueA(heroKitObject, 8);
                HeroKitCommonRuntime.messageBackgroundImage = (unityObject.value != null) ? (Sprite)unityObject.value : null;
                HeroKitCommonRuntime.changeMessageBackground = true;
            }

            // change button image
            bool changeButtonImage = BoolValue.GetValue(heroKitObject, 9);
            if (changeButtonImage)
            {
                UnityObjectField unityObject = UnityObjectFieldValue.GetValueA(heroKitObject, 10);
                HeroKitCommonRuntime.messageButtonImage = (unityObject.value != null) ? (Sprite)unityObject.value : null;
                HeroKitCommonRuntime.changeMessageButton = true;
            }

            // change button layout
            bool changeButtonLayout = BoolValue.GetValue(heroKitObject, 11);
            if (changeButtonLayout)
            {
                HeroKitCommonRuntime.messageButtonLayout = DropDownListValue.GetValue(heroKitObject, 12);
                HeroKitCommonRuntime.changeMessageButtonLayout = true;
            }

            // change text color
            bool changeTextColor = BoolValue.GetValue(heroKitObject, 13);
            if (changeTextColor)
            {
                HeroKitCommonRuntime.messageTextColor = ColorValue.GetValue(heroKitObject, 14);
                HeroKitCommonRuntime.changeMessageTextColor = true;
            }

            // change heading color
            bool changeHeadingColor = BoolValue.GetValue(heroKitObject, 15);
            if (changeHeadingColor)
            {
                HeroKitCommonRuntime.messageHeadingColor = ColorValue.GetValue(heroKitObject, 16);
                HeroKitCommonRuntime.changeMessageHeadingColor = true;
            }

            // change button text color
            bool changeButtonTextColor = BoolValue.GetValue(heroKitObject, 17);
            if (changeButtonTextColor)
            {
                HeroKitCommonRuntime.messageButtonTextColor = ColorValue.GetValue(heroKitObject, 18);
                HeroKitCommonRuntime.changeMessageButtonTextColor = true;
            }

            // change active button color
            bool changeButtonActiveColor = BoolValue.GetValue(heroKitObject, 21);
            if (changeButtonActiveColor)
            {
                HeroKitCommonRuntime.messageButtonActiveColor = ColorValue.GetValue(heroKitObject, 22);
                HeroKitCommonRuntime.changeMessageButtonActiveColor = true;
            }

            if (heroKitObject.debugHeroObject) Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, ("") ));

            return -99;
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------

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