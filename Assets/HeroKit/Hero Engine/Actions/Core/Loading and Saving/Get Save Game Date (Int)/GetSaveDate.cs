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
    /// Get the date when game was last saved (integer format).
    /// </summary>
    public class GetSaveDate : IHeroKitAction
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
        public static GetSaveDate Create()
        {
            GetSaveDate action = new GetSaveDate();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            string saveGameName = StringFieldValue.GetValueA(heroKitObject, 12, true);
            string path = Application.persistentDataPath + "/HeroSaves/" + saveGameName + ".json";
            DateTime date = new DateTime();
            bool runThis = (File.Exists(path));

            // get the json data in the file
            if (runThis)
            {
                date = File.GetLastWriteTime(path);

                // set year
                if (BoolValue.GetValue(heroKitObject, 0))
                    IntegerFieldValue.SetValueB(heroKitObject, 1, date.Year);

                // set month
                if (BoolValue.GetValue(heroKitObject, 2))
                    IntegerFieldValue.SetValueB(heroKitObject, 3, date.Month);

                // set day
                if (BoolValue.GetValue(heroKitObject, 4))
                    IntegerFieldValue.SetValueB(heroKitObject, 5, date.Day);

                // set hour
                if (BoolValue.GetValue(heroKitObject, 6))
                    IntegerFieldValue.SetValueB(heroKitObject, 7, date.Hour);

                // set minute
                if (BoolValue.GetValue(heroKitObject, 8))
                    IntegerFieldValue.SetValueB(heroKitObject, 9, date.Minute);

                // set second
                if (BoolValue.GetValue(heroKitObject, 10))
                    IntegerFieldValue.SetValueB(heroKitObject, 11, date.Second);
            }

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Save Game Name: " + saveGameName + "\n" +
                                      "Save Date: " + date;
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