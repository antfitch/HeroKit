// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using System.IO;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Get the date when game was last saved (string format).
    /// </summary>
    public class GetSaveDateString : IHeroKitAction
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
        public static GetSaveDateString Create()
        {
            GetSaveDateString action = new GetSaveDateString();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            string saveGameName = StringFieldValue.GetValueA(heroKitObject, 0, true);
            string path = Application.persistentDataPath + "/HeroSaves/" + saveGameName + ".json";
            string date = "";
            bool runThis = (File.Exists(path));

            // get the json data in the file
            if (runThis)
            {
                DateTime saveDate = File.GetLastWriteTime(path);

                // get the date format to use
                string dateFormat = StringFieldValue.GetValueA(heroKitObject, 1);

                // get the date
                date = saveDate.ToString(dateFormat);

                // save the date
                StringFieldValue.SetValueB(heroKitObject, 2, date);
            }

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Save Game Date: " + date + "\n" +
                                      "Save Game Name: " + saveGameName;
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