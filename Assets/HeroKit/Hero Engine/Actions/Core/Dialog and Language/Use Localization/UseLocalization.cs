// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Use localization on messages and UI text.
    /// </summary>
    public class UseLocalization : IHeroKitAction
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
        public static UseLocalization Create()
        {
            UseLocalization action = new UseLocalization();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;

            TextAsset csv = ObjectValue.GetValue<TextAsset>(heroKitObject, 0);
            bool runThis = (csv != null);

            if (runThis)
            {
                string text = csv.text;

                // save name of the localization file (used to play localized audio for messages)
                bool localizeAudio = BoolValue.GetValue(heroKitObject, 1);
                HeroKitCommonRuntime.localizatonDirectory = (localizeAudio) ? StringFieldValue.GetValueA(heroKitObject, 2) : "";

                // get each line in the file
                string[] delimitersA = { "\r\n" };
                string[] lines = text.Split(delimitersA, StringSplitOptions.RemoveEmptyEntries);

                // get key and value from each line
                List<KeyValuePair<string, string>> localizationList = new List<KeyValuePair<string, string>>();
                char[] delimitersB = { ',' };
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] textLine = lines[i].Split(delimitersB, 2);

                    if (textLine.Length >= 2)
                    {
                        string key = textLine[0];
                        string value = textLine[1];

                        // if value is more than one word, remove quotes
                        bool removeQuotes = value.Contains(" ");
                        if (removeQuotes)
                        {
                            string trimmedValue = value.Trim('\"');
                            value = trimmedValue;
                        }

                        // add the key and value to the localization list
                        localizationList.Add(new KeyValuePair<string, string>(key, value));
                    }
                }

                // add the key value pairs to the dictionary
                if (localizationList.Count > 0)
                {
                    HeroKitDatabase.LocalizationDictionary = new Dictionary<string, string>();
                    HeroKitDatabase.AddLocalization(localizationList);
                }
            }

            // show debug message
            if (heroKitObject.debugHeroObject)
            {
                string fileName = (csv != null) ? csv.name : "";
                string debugMessage = "CSV File: " + fileName;
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