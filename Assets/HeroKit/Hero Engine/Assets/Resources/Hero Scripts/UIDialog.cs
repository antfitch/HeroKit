// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Scripts
{
    /// <summary>
    /// A dialog box.
    /// </summary>
    public class UIDialog : MonoBehaviour
    {
        // --------------------------------------------------------------
        // Variables (General)
        // --------------------------------------------------------------
        #region Variables

        /// <summary>
        /// The actions that can be performed on the dialog box.
        /// </summary>
        public enum TextAction { showDialog };
        /// <summary>
        /// The action to perform on the dialog box.
        /// </summary>
        public TextAction textAction;
        /// <summary>
        /// Hero object that is using this script.
        /// </summary>
        public HeroKitObject heroKitObject;
        /// <summary>
        /// Current event ID assigned to the hero object that called this script.
        /// </summary>
        public int eventID;
        /// <summary>
        /// Current action ID assigned to the hero object that called this script.
        /// </summary>
        public int actionID;
        /// <summary>
        /// Don't hide the dialog box after you are finished using it. 
        /// </summary>
        /// <remarks>This is useful if you have a chain of messages and you don't want the dialog box to flicker
        /// as it closes and opens.</remarks>
        public bool dontHideCanvas;
        /// <summary>
        /// The message to display in the dialog box.
        /// </summary>
        public string message = "";
        /// <summary>
        /// The audio clip to play when the message is displayed.
        /// </summary>
        public AudioClip audioClip;


        // --------------------------------------------------------------
        // Variables (Write Message)
        // --------------------------------------------------------------

        /// <summary>
        /// Write the message out character-by-character.
        /// </summary>
        public bool writeMessage;
        /// <summary>
        /// Amount of time to pause between the printing of each character. (Used if writeMessage is true)
        /// </summary>
        public float pause;

        // --------------------------------------------------------------
        // Variables (Title)
        // --------------------------------------------------------------

        /// <summary>
        /// Change the title that appears above the message in the dialog box.
        /// </summary>
        public bool setTitle;
        /// <summary>
        /// The name of the title.
        /// </summary>
        public string title = "";
        /// <summary>
        /// Change the location of the title.
        /// </summary>
        public bool changeTitleAlignment;
        /// <summary>
        /// Set the location of the title.
        /// </summary>
        public int titleAlignmentType;

        // --------------------------------------------------------------
        // Variables (Choices)
        // --------------------------------------------------------------

        /// <summary>
        /// Add choice buttons to the dialog box.
        /// </summary>
        public bool addChoices;
        /// <summary>
        /// The number of choice buttons to add.
        /// </summary>
        public int numberOfChoices;
        /// <summary>
        /// The text to display on the choice buttons.
        /// </summary>
        public string[] choice;
        /// <summary>
        /// The choice button that was selected by the player.
        /// </summary>
        public int selectedChoiceID;

        // --------------------------------------------------------------
        // Variables (Portraits)
        // --------------------------------------------------------------

        /// <summary>
        /// Change a portrait.
        /// </summary>
        public bool[] changePortrait = new bool[2];
        /// <summary>
        /// Sprite for the portrait.
        /// </summary>
        public Sprite[] portraitSprite = new Sprite[2];
        /// <summary>
        /// Flip the portrait. (Mirror image of portrait)
        /// </summary>
        public bool[] flipImage = new bool[2];
        /// <summary>
        /// Change the scale of the portrait.
        /// </summary>
        public bool[] changeScale = new bool[2];
        /// <summary>
        /// New scale of the portrait.
        /// </summary>
        public float[] newScale = new float[2];
        /// <summary>
        /// Change the X position of the portrait.
        /// </summary>
        public bool[] changeXPos = new bool[2];
        /// <summary>
        /// New X position of the portrait.
        /// </summary>
        public float[] newXPos = new float[2];
        /// <summary>
        /// Change the Y position of the portrait.
        /// </summary>
        public bool[] changeYPos = new bool[2];
        /// <summary>
        /// New Y position of the portrait.
        /// </summary>
        public float[] newYPos = new float[2];
        /// <summary>
        /// Change the Z position of the portrait.
        /// </summary>
        public bool[] changeZPos = new bool[2];
        /// <summary>
        /// New Z position of the portrait.
        /// </summary>
        public float[] newZPos = new float[2];

        // --------------------------------------------------------------
        // Variables (Internals, Formatting Message)
        // --------------------------------------------------------------

        /// <summary>
        /// Position of left side of title box.
        /// </summary>
        private float messageTitleLeft;
        /// <summary>
        /// Position of bottom side of title box.
        /// </summary>
        private float messageTitlePosY;
        /// <summary>
        /// Position of right side of title box.
        /// </summary>
        private float messageTitleRight;
        /// <summary>
        /// Height of title box.
        /// </summary>
        private float messageTitleHeight;

        /// <summary>
        /// Position of left side of message box.
        /// </summary>
        private float messageTextLeft;
        /// <summary>
        /// Position of bottom side of message box.
        /// </summary>
        private float messageTextPosY;
        /// <summary>
        /// Position of right side of message box.
        /// </summary>
        private float messageTextRight;
        /// <summary>
        /// Height of message box.
        /// </summary>
        private float messageTextHeight;
        /// <summary>
        /// Grid that contains the option buttons.
        /// </summary>
        private GridLayoutGroup messageGrid;
        /// <summary>
        /// Position of the left side of the option buttons grid.
        /// </summary>
        private float messageButtonLeft;
        /// <summary>
        /// Size of cells in the option buttons grid.
        /// </summary>
        private float messageButtonCellSize;

        // --------------------------------------------------------------
        // Variables (Internals, Printing Message)
        // --------------------------------------------------------------

        /// <summary>
        /// The position in the message string that needs to be printed. (Used if message is being printed out)
        /// </summary>
        private int charIndex;
        /// <summary>
        /// An option has been selected by the player.
        /// </summary>
        private bool chooseOption;
        /// <summary>
        /// The message has been printed. (Used if message is being printed out)
        /// </summary>
        private bool updateIsDone;
        /// <summary>
        /// The current time.
        /// </summary>
        private float currentTime;
        /// <summary>
        /// The time to wait between the drawing of each character (0=fast, 5=slow)
        /// </summary>
        private float timeToWait;
        /// <summary>
        /// The time left before timeToWait is finished.
        /// </summary>
        private float timeLeft;
        /// <summary>
        /// Stores opening text code. (For example [bold])
        /// </summary>
        private Stack<string> codeA = new Stack<string>();
        /// <summary>
        /// Stores closing text code. (For example [/bold])
        /// </summary>
        private Stack<string> codeB = new Stack<string>();

        // --------------------------------------------------------------
        // Variables (Internals, General)
        // --------------------------------------------------------------

        /// <summary>
        /// The hero kit object with the dialog UI prefab attached to it.
        /// </summary>
        private HeroKitObject dialogObject;
        /// <summary>
        /// The canvas of the dialog UI.
        /// </summary>
        private Canvas messageCanvas;
        /// <summary>
        /// The title text component.
        /// </summary>
        private Text titleText;
        /// <summary>
        /// The image component that contains the left portrait.
        /// </summary>
        private Image portraitLeft;
        /// <summary>
        /// The image component that contains the right portrait.
        /// </summary>
        private Image portraitRight;
        /// <summary>
        /// The text component that contains the message.
        /// </summary>
        private Text messageText;
        /// <summary>
        /// The text components that contain the options button text.
        /// </summary>
        private readonly Text[] choiceText = new Text[3];
        /// <summary>
        /// The audio source for the audio file that plays during the display of the message.
        /// </summary>
        private AudioSource audioSource;
        /// <summary>
        /// The image component that contains the background for the message.
        /// </summary>
        private Image messageBackground;
        /// <summary>
        /// The image components that contain the option buttons.
        /// </summary>
        private readonly Image[] messageButton = new Image[3];

        #endregion

        // --------------------------------------------------------------
        // Methods (General)
        // --------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Initialize this script.
        /// </summary>
        public void Initialize()
        {
            if (dialogObject == null)
                BuildDialog();

            // reset values            
            charIndex = 0;
            chooseOption = false;
            updateIsDone = false;
            currentTime = 0f;
            timeToWait = 0f;
            timeLeft = 0f;
            codeA = new Stack<string>();
            codeB = new Stack<string>();

            writeMessage = HeroKitCommonRuntime.writeMessage;
            pause = HeroKitCommonRuntime.messageWaitTime;
            messageText.alignment = HeroKitCommonRuntime.messageAlignment;

            if (HeroKitCommonRuntime.changeMessageBackground)
            {
                HeroKitCommonRuntime.changeMessageBackground = false;
                messageBackground.sprite = HeroKitCommonRuntime.messageBackgroundImage;
            }

            if (HeroKitCommonRuntime.changeMessageButton)
            {
                HeroKitCommonRuntime.changeMessageButton = false;
                for (int i = 0; i < messageButton.Length; i++)
                    messageButton[i].sprite = HeroKitCommonRuntime.messageButtonImage;
            }

            if (HeroKitCommonRuntime.changeMessageButtonLayout)
            {
                HeroKitCommonRuntime.changeMessageButtonLayout = false;
                messageGrid.constraintCount = (HeroKitCommonRuntime.messageButtonLayout == 1) ? 1 : 3;
            }

            if (HeroKitCommonRuntime.changeMessageBackgroundTransparency)
            {
                HeroKitCommonRuntime.changeMessageBackgroundTransparency = false;
                messageBackground.color = new Color(messageBackground.color.r, messageBackground.color.g, messageBackground.color.b, HeroKitCommonRuntime.messageBackgroundAlpha);
            }

            if (HeroKitCommonRuntime.changeMessageButtonTransparency)
            {
                HeroKitCommonRuntime.changeMessageButtonTransparency = false;
                for (int i = 0; i < messageButton.Length; i++)
                    messageButton[i].color = new Color(messageButton[i].color.r, messageButton[i].color.g, messageButton[i].color.b, HeroKitCommonRuntime.messageButtonAlpha);
            }

            if (HeroKitCommonRuntime.changeMessageTextColor)
            {
                HeroKitCommonRuntime.changeMessageTextColor = false;
                messageText.color = HeroKitCommonRuntime.messageTextColor;
            }

            if (HeroKitCommonRuntime.changeMessageHeadingColor)
            {
                HeroKitCommonRuntime.changeMessageHeadingColor = false;
                titleText.color = HeroKitCommonRuntime.messageHeadingColor;
            }

            if (HeroKitCommonRuntime.changeMessageButtonTextColor)
            {
                HeroKitCommonRuntime.changeMessageButtonTextColor = false;
                for (int i = 0; i < choiceText.Length; i++)
                    choiceText[i].color = HeroKitCommonRuntime.messageButtonTextColor;
            }

            if (HeroKitCommonRuntime.changeMessageButtonActiveColor)
            {
                HeroKitCommonRuntime.changeMessageButtonActiveColor = false;
                for (int i = 0; i < messageButton.Length; i++)
                {
                    Button choiceButton = messageButton[i].gameObject.GetComponent<Button>();
                    ColorBlock colors = choiceButton.colors;
                    colors.highlightedColor = HeroKitCommonRuntime.messageButtonActiveColor;
                    choiceButton.colors = colors;
                }
            }

            // set the title
            SetTitle();

            // set the dialog
            SetMessage();

            // set the audio dialog
            SetMessageAudio();

            // set the left portrait
            SetLeftPortrait();

            // set the right portrait
            SetRightPortrait();

            // clear the current choices
            ClearChoices();

            // add choices immediately if we are not typing message
            if (!writeMessage) SetChoices();

            // set up the rect transform for the message
            SetMessageRectTransform();

            // make canvas visible
            messageCanvas.enabled = true;

            // exit early if we don't want to hide canvas
            if (dontHideCanvas) return;

            // is next action also a message? if yes, don't hide the canvas
            int actionCount = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions.Count;
            int nextActionID = heroKitObject.heroStateData.action + 1;

            // exit early if next action is greater than action count
            if (nextActionID >= actionCount) return;

            string thisAction = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action].actionTemplate.name;
            string nextAction = heroKitObject.heroState.heroEvent[heroKitObject.heroStateData.eventBlock].actions[heroKitObject.heroStateData.action + 1].actionTemplate.name;
            if (thisAction == nextAction)
                dontHideCanvas = true;
        }

        /// <summary>
        /// Build the dialog box.
        /// </summary>
        public void BuildDialog()
        {
            // get the hero kit object
            dialogObject = GetComponent<HeroKitObject>();

            // get the canvas
            messageCanvas = GetComponent<Canvas>();

            // get the text
            Text[] text = gameObject.GetComponentsInChildren<Text>();
            foreach (Text element in text)
            {
                // get the title text
                if (element.gameObject.name == "Title Text")
                {
                    titleText = element;
                }

                // get the message text
                else if (element.gameObject.name == "Message Text")
                {
                    messageText = element;
                }

                // get the choice 1 text
                else if (element.gameObject.name == "Choice Button 1 Text")
                {
                    choiceText[0] = element;
                }

                // get the choice 1 text
                else if (element.gameObject.name == "Choice Button 1 Text")
                {
                    choiceText[0] = element;
                }

                // get the choice 2 text
                else if (element.gameObject.name == "Choice Button 2 Text")
                {
                    choiceText[1] = element;
                }

                // get the choice 3 text
                else if (element.gameObject.name == "Choice Button 3 Text")
                {
                    choiceText[2] = element;
                }
            }

            // get the portraits and backround
            Image[] image = gameObject.GetComponentsInChildren<Image>();
            foreach (Image element in image)
            {
                // get the portrait on left
                if (element.gameObject.name == "Portrait Left")
                {
                    portraitLeft = element;
                }

                // get the portrait on right
                if (element.gameObject.name == "Portrait Right")
                {
                    portraitRight = element;
                }

                // get the background image
                if (element.gameObject.name == "Background Image")
                {
                    messageBackground = element;
                }

                // get the button image
                if (element.gameObject.name == "Choice Button 1")
                {
                    messageButton[0] = element;
                }

                // get the button image
                if (element.gameObject.name == "Choice Button 2")
                {
                    messageButton[1] = element;
                }

                // get the button image
                if (element.gameObject.name == "Choice Button 3")
                {
                    messageButton[2] = element;
                }
            }

            // get the grid for buttons
            GridLayoutGroup[] grid = gameObject.GetComponentsInChildren<GridLayoutGroup>();
            foreach (GridLayoutGroup element in grid)
            {
                // get the grid
                if (element.gameObject.name == "Choice Buttons")
                {
                    messageGrid = element;
                }
            }

            // get the audio source
            audioSource = GetComponent<AudioSource>();

            // set up message text rectangle
            messageTitleLeft = titleText.rectTransform.offsetMin.x;
            messageTitlePosY = titleText.rectTransform.offsetMax.y;
            messageTitleRight = -titleText.rectTransform.offsetMax.x;
            messageTitleHeight = titleText.rectTransform.sizeDelta.y;

            messageTextLeft = messageText.rectTransform.offsetMin.x;
            messageTextPosY = messageText.rectTransform.offsetMax.y;
            messageTextRight = -messageText.rectTransform.offsetMax.x;
            messageTextHeight = messageText.rectTransform.sizeDelta.y;
            messageButtonLeft = messageGrid.padding.left;
            messageButtonCellSize = messageGrid.cellSize.x;
        }

        /// <summary>
        /// Execute this method every frame.
        /// </summary>
        private void FixedUpdate ()
        {
            if (textAction == TextAction.showDialog)
                PrintMessage();

            // exit if we are done
            if (updateIsDone)
            {
                if (audioClip != null) audioSource.Stop();
                if (!dontHideCanvas) messageCanvas.enabled = false;
                enabled = false;
            }
        }

        //----------------------------------------
        // Methods (Build the message)
        //----------------------------------------

        /// <summary>
        /// Set the title in the dialog box.
        /// </summary>
        private void SetTitle()
        {
            // exit early if we don't need to set title
            if (!setTitle) return;

            // set title text component
            titleText.text = title;

            // exit early if we don't need to change the alignment of the title
            if (!changeTitleAlignment) return;

            // set alignment of title
            switch (titleAlignmentType)
            {
                case 1:
                    titleText.alignment = TextAnchor.MiddleLeft;
                    break;
                case 2:
                    titleText.alignment = TextAnchor.MiddleCenter;
                    break;
                case 3:
                    titleText.alignment = TextAnchor.MiddleRight;
                    break;
            }
        }

        /// <summary>
        /// Set the message in the dialog box.
        /// </summary>
        private void SetMessage()
        {
            // write out message character by character
            messageText.text = writeMessage ? "" : message;
        }

        /// <summary>
        /// Set the audio clip for the message.
        /// </summary>
        private void SetMessageAudio()
        {
            // if were are using localization, get the localized audio file to use if it exists
            if (HeroKitCommonRuntime.localizatonDirectory != "" && audioClip != null)
            {
                string path = HeroKitCommonRuntime.localizatonDirectory + "/" + audioClip.name;
                AudioClip newAudioClip = (AudioClip)Resources.Load(path);
                if (newAudioClip != null) audioClip = newAudioClip;
            }

            // play the audio clip
            if (audioClip != null)
            {
                audioSource.loop = false;
                audioSource.playOnAwake = false;
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }

        /// <summary>
        /// Hide option buttons.
        /// </summary>
        private void ClearChoices()
        {
            Text choiceA = choiceText[0];
            Text choiceB = choiceText[1];
            Text choiceC = choiceText[2];

            HideChoice(choiceA);
            HideChoice(choiceB);
            HideChoice(choiceC);
        }

        /// <summary>
        /// Show option buttons.
        /// </summary>
        private void SetChoices()
        {
            Text choiceA = choiceText[0];
            Text choiceB = choiceText[1];
            Text choiceC = choiceText[2];

            // exit if we are not adding choices
            if (!addChoices)
            {
                numberOfChoices = 0;
                return;
            } 

            // exit if there are no choices
            if (numberOfChoices <= 0) return;

            // if you must choose an option, turn off ability to exit message with spacebar
            chooseOption = true;

            switch (numberOfChoices)
            {
                case 1:
                    ShowChoice(choiceA, 0);
                    break;
                case 2:
                    ShowChoice(choiceA, 0);
                    ShowChoice(choiceB, 1);
                    break;
                case 3:
                    ShowChoice(choiceA, 0);
                    ShowChoice(choiceB, 1);
                    ShowChoice(choiceC, 2);
                    break;
            }
        }

        /// <summary>
        /// Show an option button.
        /// </summary>
        /// <param name="choiceText">The text for the button.</param>
        /// <param name="choiceID">The ID for the button.</param>
        private void ShowChoice(Text choiceText, int choiceID)
        {
            choiceText.transform.parent.gameObject.SetActive(true);
            choiceText.text = choice[choiceID];

            HeroKitListenerUI listener = choiceText.transform.parent.gameObject.GetComponent<HeroKitListenerUI>();

            // set the target hero kit object
            listener.sendNotificationsHere = heroKitObject;

            // set action type as "Send Values"
            listener.actionType = 2;
        }

        /// <summary>
        /// Hide an option button.
        /// </summary>
        /// <param name="choiceText">The text box assigned to the button.</param>
        private void HideChoice(Text choiceText)
        {
            choiceText.transform.parent.gameObject.SetActive(false);
        }

        /// <summary>
        /// Set the left portrait.
        /// </summary>
        private void SetLeftPortrait()
        {
            SetPortrait(portraitLeft, 0);
        }

        /// <summary>
        /// Set the right portrait.
        /// </summary>
        private void SetRightPortrait()
        {
            SetPortrait(portraitRight, 1);
        }

        /// <summary>
        /// Set a portrait.
        /// </summary>
        /// <param name="portrait">The image for the portrait.</param>
        /// <param name="portraitID">The ID of the portrait.</param>
        private void SetPortrait(Image portrait, int portraitID)
        {
            // exit early if there is no portrait
            if (!changePortrait[portraitID]) return;

            // set the portrait
            portrait.sprite = portraitSprite[portraitID];

            // get the size of portrait
            if (portrait.sprite != null)
            {
                RectTransform rectTransform = portrait.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(portrait.sprite.rect.width, portrait.sprite.rect.height);
            }

            // change the color (alpha = transparent if image is null)
            Color color = portrait.color;
            color.a = (portrait.sprite == null) ? color.a = 0 : color.a = 1;
            portrait.color = color;

            // flip the image
                
            if (flipImage[portraitID])
            {
                Vector3 eulerAngles = portrait.transform.eulerAngles;
                eulerAngles.y = 180f;
                portrait.transform.eulerAngles = eulerAngles;
            }
            else
            {
                Vector3 eulerAngles = portrait.transform.eulerAngles;
                eulerAngles.y = 0f;
                portrait.transform.eulerAngles = eulerAngles;
            }

            // change the scale of the image
            if (changeScale[portraitID])
            {
                portrait.transform.localScale = new Vector3(newScale[portraitID], newScale[portraitID], newScale[portraitID]);
            }

            // change the x position of the image
            if (changeXPos[portraitID])
            {
                portrait.transform.localPosition = new Vector3(newXPos[portraitID], portrait.transform.localPosition.y, portrait.transform.localPosition.z);
            }

            // change the y position of the image
            if (changeYPos[portraitID])
            {
                portrait.transform.localPosition = new Vector3(portrait.transform.localPosition.y, newYPos[portraitID], portrait.transform.localPosition.z);
            }

            // exit early if we don't need to change z position
            if (!changeZPos[portraitID]) return;

            // get new position for portrait (1=behind, 0=in front of message window)
            int pos = (int)newZPos[portraitID];

            // get position of portrait
            int portraitIndex = portrait.transform.GetSiblingIndex();

            // get position of message window
            int backgroundIndex = messageBackground.transform.GetSiblingIndex();

            // put behind message window if it is in front of message window
            switch (pos)
            {
                case 1:
                    if (portraitIndex > backgroundIndex)
                        portrait.transform.SetSiblingIndex(backgroundIndex - 1);
                    break;
                case 2:
                    if (portraitIndex < backgroundIndex)
                        portrait.transform.SetSiblingIndex(backgroundIndex + 1);
                    break;
            }
        }

        /// <summary>
        /// Set the rect transform for the message text.
        /// </summary>
        private void SetMessageRectTransform()
        {
            // get some variables & set up defaults
            float titleLeft = messageTitleLeft;
            float titleRight = messageTitleRight;
            float messageLeft = messageTextLeft;
            float messagePosY = messageTextPosY;
            float messageRight = messageTextRight;
            float messageHeight = messageTextHeight;
            float buttonLeft = messageButtonLeft;
            float buttonCellSize = messageButtonCellSize;
            int buttonCount = numberOfChoices;
            int buttonLayout = HeroKitCommonRuntime.messageButtonLayout;
            int buttonTop = 0;

            // get position of left portrait, right portrait, message window
            int leftPortraitIndex = portraitLeft.transform.GetSiblingIndex();
            int rightPortraitIndex = portraitRight.transform.GetSiblingIndex();
            int backgroundIndex = messageBackground.transform.GetSiblingIndex();

            // check to see which portraits are in front and are visible
            bool leftPortraitInFront = (leftPortraitIndex > backgroundIndex && portraitLeft.sprite != null);
            bool rightPortraitInFront = (rightPortraitIndex > backgroundIndex && portraitRight.sprite != null);

            // check to see if title is visible
            bool titleIsVisible = (title != "");
            bool messageIsVisible = (message != "");

            // check if buttons are being displayed
            //

            // left portrait in front
            if (leftPortraitInFront && !rightPortraitInFront)
            {
                titleLeft = -405;
                messageLeft = -405;

                // buttons = horizontal layout
                if (buttonLayout == 1)
                {
                    switch (buttonCount)
                    {
                        case 1:
                            buttonCellSize = 1350;
                            buttonLeft = 420;
                            break;
                        case 2:
                            buttonCellSize = 670;
                            buttonLeft = 420;
                            break;
                        case 3:
                            buttonCellSize = 440;
                            buttonLeft = 400;
                            break;
                    }
                }
                // buttons = vertical layout
                else if (buttonLayout == 2)
                {
                    buttonCellSize = 1350;
                    buttonLeft = 420;
                }
            }

            // right portrait in front
            else if (rightPortraitInFront && !leftPortraitInFront)
            {
                titleRight = -410;
                messageRight = -410;

                // buttons = horizontal layout
                if (buttonLayout == 1)
                {
                    switch (buttonCount)
                    {
                        case 1:
                            buttonCellSize = 1350;
                            buttonLeft = -420;
                            break;
                        case 2:
                            buttonCellSize = 670;
                            buttonLeft = -420;
                            break;
                        case 3:
                            buttonCellSize = 440;
                            buttonLeft = -420;
                            break;
                    }
                }
                // buttons = vertical layout
                else if (buttonLayout == 2)
                {
                    buttonCellSize = 1350;
                    buttonLeft = -420;
                }
            }

            // left portrait in front, right portrait in front, there are buttons
            else if (leftPortraitInFront && rightPortraitInFront)
            {
                titleLeft = -405;
                titleRight = -410;

                messageLeft = -405;
                messageRight = -410;

                // buttons = horizontal layout
                if (buttonLayout == 1)
                {
                    switch (buttonCount)
                    {
                        case 1:
                            buttonCellSize = 900;
                            break;
                        case 2:
                            buttonCellSize = 450;
                            break;
                        case 3:
                            buttonCellSize = 300;
                            break;
                    }
                }
                // buttons = vertical layout
                else if (buttonLayout == 2)
                {
                    buttonCellSize = 900;
                }
            }

            // no portraits in front
            else
            {
                // buttons = horizontal layout
                if (buttonLayout == 1)
                {
                    switch (buttonCount)
                    {
                        case 1:
                            buttonCellSize = 1760;
                            break;
                        case 2:
                            buttonCellSize = 875;
                            break;
                        case 3:
                            buttonCellSize = 580;
                            break;
                    }
                }
                // buttons = vertical layout
                else if (buttonLayout == 2)
                {
                    buttonCellSize = 1760;
                }
            }

            // title is not showing
            if (!titleIsVisible)
            {
                messagePosY = -3.5f;
                messageHeight = 390 + 4;

                // buttons = horizontal layout
                if (buttonLayout == 1)
                {
                    // adjust height for buttons
                    if (buttonCount > 0)
                        messageHeight = 300 + 4;
                }
                // buttons = vertical layout
                else if (buttonLayout == 2)
                {
                    if (messageIsVisible)
                    {
                        switch (buttonCount)
                        {
                            case 1:
                                buttonTop = 0;
                                break;
                            case 2:
                                buttonTop = -110;
                                break;
                            case 3:
                                buttonTop = -220;
                                break;
                        }
                    }
                    else
                    {
                        switch (buttonCount)
                        {
                            case 1:
                                buttonTop = -136;
                                break;
                            case 2:
                                buttonTop = -194;
                                break;
                            case 3:
                                buttonTop = -248;
                                break;
                        }
                    }
                }
            }
            // title is showing
            else
            {
                messageHeight = 330 + 72;

                // buttons = horizontal layout
                if (buttonLayout == 1)
                {
                    // adjust height for buttons
                    if (buttonCount > 0)
                        messageHeight = 250 + 72;
                }
                // buttons = vertical layout
                else if (buttonLayout == 2)
                {
                    if (messageIsVisible)
                    {
                        switch (buttonCount)
                        {
                            case 1:
                                buttonTop = 0;
                                break;
                            case 2:
                                buttonTop = -110;
                                break;
                            case 3:
                                buttonTop = -220;
                                break;
                        }
                    }
                    else
                    {
                        switch (buttonCount)
                        {
                            case 1:
                                buttonTop = -122;
                                break;
                            case 2:
                                buttonTop = -172;
                                break;
                            case 3:
                                buttonTop = -220;
                                break;
                        }
                    }
                }
            }

            // Set the rect transform for the message
            SetTextLayout(messageText.rectTransform, messageLeft, messagePosY, messageRight, messageHeight);

            // Set the rect transform for the title
            SetTextLayout(titleText.rectTransform, titleLeft, messageTitlePosY, titleRight, messageTitleHeight - 46);

            // set the grid layout group info for the message
            SetButtonLayout(buttonCellSize, buttonLeft, buttonTop);
        }

        /// <summary>
        /// Set the layout for the message text.
        /// </summary>
        /// <param name="trs">Rect transform for the message.</param>
        /// <param name="left">Left alignment for the message.</param>
        /// <param name="posY">Bottom alignment for the message.</param>
        /// <param name="right">Right alignment for the message.</param>
        /// <param name="height">Height of the message.</param>
        private void SetTextLayout(RectTransform trs, float left, float posY, float right, float height)
        {
            trs.offsetMin = new Vector2(left, -height);
            trs.offsetMax = new Vector2(-right, posY);
        }

        /// <summary>
        /// Set the layout for the option buttons grid.
        /// </summary>
        /// <param name="buttonCellWidth">The width of a button in the grid.</param>
        /// <param name="buttonLeft">The left alignment for the grid.</param>
        /// <param name="buttonTop">The top alignment for the grid.</param>
        private void SetButtonLayout(float buttonCellWidth, float buttonLeft, float buttonTop)
        {
            // set button size
            messageGrid.cellSize = new Vector2(buttonCellWidth, messageGrid.cellSize.y);

            // set button padding
            messageGrid.padding = new RectOffset((int)buttonLeft, messageGrid.padding.right, (int)buttonTop, messageGrid.padding.bottom);
        }

        //----------------------------------------
        // Methods (Type the message)
        //----------------------------------------

        /// <summary>
        /// Print a message one character at a time.
        /// </summary>
        private void PrintMessage()
        {
            if (!writeMessage)
            {
                codeA = new Stack<string>();
                codeB = new Stack<string>();
            }

            // write a message
            if (writeMessage)
            {
                if (charIndex < message.Length)
                {
                    currentTime = Time.time;
                    timeLeft = timeToWait - currentTime;

                    if (timeLeft <= 0)
                    {
                        // check if this character is a markup tag beginning
                        ParseMarkup();

                        // increment index
                        charIndex++;

                        // draw message segment
                        if (charIndex < message.Length)
                        {
                            string closingCode = "";
                            foreach (string i in codeB)
                            {
                                closingCode += i;
                            }

                            string newMessage = message.Remove(charIndex) + closingCode;
                            messageText.text = newMessage;
                        }
                        else
                        {
                            messageText.text = message;
                            writeMessage = false;
                        }

                        // reset time
                        timeToWait = (pause + currentTime);
                    }
                }
                else
                {
                    writeMessage = false;
                }

                // write out entire message early if input used
                FinishMessage();

                // if a message that was typed out finishes and there are choices, show them
                if (!writeMessage)
                {
                    SetChoices();
                }
            }

            // don't write a message
            else
            {
                CheckForInput();
            }
        }

        /// <summary>
        /// Parse markup found in the message we are printing.
        /// </summary>
        private void ParseMarkup()
        {
            // get open tag (codeA)
            // create temporary close tag (codeB)
            char charA = message[charIndex];
            char charB = (charIndex + 1 < message.Length) ? message[charIndex + 1] : new char();

            if (charA == '<')
            {
                // this is a close tag
                if (charB == '/')
                {
                    GetCloseTag();
                }
                // this is an open tag
                else
                {
                    GetOpenTag();
                }
            }
        }

        /// <summary>
        /// Get the opening markup tag.
        /// </summary>
        private void GetOpenTag()
        {
            string openTag = "";
            string closeTag = "";

            for (int i = charIndex; i < message.Length; i++)
            {
                // get the whole code, character by character
                openTag += message[i];

                // exit when whole code has been copied
                if (message[i] == '>')
                {
                    closeTag = openTag.Insert(1, "/");
                    charIndex += openTag.Length - 1;

                    codeA.Push(openTag);
                    codeB.Push(closeTag);

                    break;
                }
            }
        }

        /// <summary>
        /// Get the closing markup tag.
        /// </summary>
        private void GetCloseTag()
        {
            string openTag = "";

            for (int i = charIndex; i < message.Length; i++)
            {
                // get the whole code, character by character
                openTag += message[i];

                // exit when whole code has been copied
                if (message[i] == '>')
                {
                    charIndex += openTag.Length - 1;

                    codeA.Pop();
                    codeB.Pop();

                    break;
                }
            }
        }

        /// <summary>
        /// Finish printing a message early. This displays the entire message.
        /// </summary>
        private void FinishMessage()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                charIndex = message.Length;
                messageText.text = message;
                writeMessage = false;
            }
        }

        /// <summary>
        /// Check for input from the player.
        /// </summary>
        private void CheckForInput()
        {
            // wait until there is a click in the scene (only when no options shown)
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (!chooseOption)
                {
                    updateIsDone = true;
                }
            }

            // if dialog has options, wait until a selection is made
            if (chooseOption)
            {
                // if a button was pressed, get the info from it
                if (heroKitObject.heroListenerData.active)
                {
                    heroKitObject.heroListenerData.active = false;
                    IntegerFieldValue.SetValueB(heroKitObject, eventID, actionID, selectedChoiceID, heroKitObject.heroListenerData.itemID);
                    updateIsDone = true;
                }
            }
        }
    }

    #endregion
}