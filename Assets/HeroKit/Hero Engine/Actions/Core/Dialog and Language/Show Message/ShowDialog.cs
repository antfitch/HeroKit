// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.ActionField;
using HeroKit.Scene.Scripts;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Show a message.
    /// </summary>
    public class ShowDialog : IHeroKitAction
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
        public static ShowDialog Create()
        {
            ShowDialog action = new ShowDialog();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;

            // get common values
            HeroKitObject targetObject = HeroKitCommonRuntime.GetPrefabFromAssets(HeroKitCommonRuntime.settingsInfo.dialogBox, true);
            bool runThis = (targetObject != null);

            if (runThis)
            {
                targetObject.gameObject.SetActive(true);

                // get the dialog script
                uiDialog = targetObject.GetHeroComponent<UIDialog>("UIDialog", true);
                uiDialog.textAction = UIDialog.TextAction.showDialog;
                uiDialog.heroKitObject = heroKitObject;
                uiDialog.dontHideCanvas = BoolValue.GetValue(heroKitObject, 30);
                uiDialog.eventID = heroKitObject.heroStateData.eventBlock;
                uiDialog.actionID = heroKitObject.heroStateData.action;

                // get values for title
                uiDialog.setTitle = BoolValue.GetValue(heroKitObject, 0);
                if (uiDialog.setTitle)
                {
                    uiDialog.title = StringFieldValue.GetValueA(heroKitObject, 1, true);
                    uiDialog.changeTitleAlignment = BoolValue.GetValue(heroKitObject, 26);
                    if (uiDialog.changeTitleAlignment)
                    {
                        uiDialog.titleAlignmentType = DropDownListValue.GetValue(heroKitObject, 27);
                    }
                }

                // get values for message
                uiDialog.message = StringFieldValue.GetValueA(heroKitObject, 2, true);

                // get values for audio
                UnityObjectField unityObjectAudio = UnityObjectFieldValue.GetValueA(heroKitObject, 28, false);
                uiDialog.audioClip = (unityObjectAudio.value != null) ? (AudioClip)unityObjectAudio.value : null;

                // get values for message choices
                uiDialog.addChoices = BoolValue.GetValue(heroKitObject, 21);
                if (uiDialog.addChoices)
                {
                    uiDialog.selectedChoiceID = 29;
                    uiDialog.numberOfChoices = DropDownListValue.GetValue(heroKitObject, 22);
                    uiDialog.choice = new string[uiDialog.numberOfChoices];
                    if (uiDialog.numberOfChoices >= 1)
                    {
                        uiDialog.choice[0] = StringFieldValue.GetValueA(heroKitObject, 23, true);

                        if (uiDialog.numberOfChoices >= 2)
                        {
                            uiDialog.choice[1] = StringFieldValue.GetValueA(heroKitObject, 24, true);

                            if (uiDialog.numberOfChoices >= 3)
                            {
                                uiDialog.choice[2] = StringFieldValue.GetValueA(heroKitObject, 25, true);
                            }
                        }
                    }
                }

                // get values for left portrait
                SetPortraitInfo(0, 3, 4, 5, 6, 7, 8, 9, 10, 11, 31, 32);

                // get values for right portrait
                SetPortraitInfo(1, 12, 13, 14, 15, 16, 17, 18, 19, 20, 33, 34);

                // enable the dialog script
                uiDialog.enabled = true;
                uiDialog.Initialize();

                eventID = heroKitObject.heroStateData.eventBlock;
                heroKitObject.heroState.heroEvent[eventID].waiting = true;
                updateIsDone = false;
                heroKitObject.longActions.Add(this);
            }

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string dialogText = (uiDialog != null) ? uiDialog.message : "";
                string debugMessage = "Message: " + dialogText;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        private void SetPortraitInfo(int portraitID, 
                                    int changePortrait, int portraitSprite, int flipImage, 
                                    int changeScale, int newScale, int changeXPos, 
                                    int newXPos, int changeYPos, int newYPos, int changeZPos, int newZPos)
        {
            // get values for left portrait
            uiDialog.changePortrait[portraitID] = BoolValue.GetValue(heroKitObject, changePortrait);
            if (uiDialog.changePortrait[portraitID])
            {
                UnityObjectField unityObject = UnityObjectFieldValue.GetValueA(heroKitObject, portraitSprite);
                uiDialog.portraitSprite[portraitID] = (unityObject.value != null) ? (Sprite)unityObject.value : null;
                uiDialog.flipImage[portraitID] = BoolValue.GetValue(heroKitObject, flipImage);
                uiDialog.changeScale[portraitID] = BoolValue.GetValue(heroKitObject, changeScale);
                if (uiDialog.changeScale[portraitID])
                {
                    uiDialog.newScale[portraitID] = FloatFieldValue.GetValueA(heroKitObject, newScale) * 0.01f;
                }
                uiDialog.changeXPos[portraitID] = BoolValue.GetValue(heroKitObject, changeXPos);
                if (uiDialog.changeXPos[portraitID])
                {
                    uiDialog.newXPos[portraitID] = FloatFieldValue.GetValueA(heroKitObject, newXPos);
                }
                uiDialog.changeYPos[portraitID] = BoolValue.GetValue(heroKitObject, changeYPos);
                if (uiDialog.changeYPos[portraitID])
                {
                    uiDialog.newYPos[portraitID] = FloatFieldValue.GetValueA(heroKitObject, newYPos);
                }
                uiDialog.changeZPos[portraitID] = BoolValue.GetValue(heroKitObject, changeZPos);
                if (uiDialog.changeZPos[portraitID])
                {
                    uiDialog.newZPos[portraitID] = DropDownListValue.GetValue(heroKitObject, newZPos);
                }
            }
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private UIDialog uiDialog;

        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }

        public void Update()
        {
            if (uiDialog == null || !uiDialog.enabled)
            {
                updateIsDone = true;
            }
        }
    }
}