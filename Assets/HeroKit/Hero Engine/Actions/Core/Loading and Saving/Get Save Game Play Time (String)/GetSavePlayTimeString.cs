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
    /// Get the amount of time that this game has been played.
    /// </summary>
    public class GetSavePlayTimeString : IHeroKitAction
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
        public static GetSavePlayTimeString Create()
        {
            GetSavePlayTimeString action = new GetSavePlayTimeString();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            // get values
            heroKitObject = hko;
            string saveGameName = StringFieldValue.GetValueA(heroKitObject, 0, true);
            GameSaveData savedGame = HeroKitCommonRuntime.GetSaveGame(saveGameName);
            string totalGameplay = "";
            bool runThis = (savedGame != null);

            // save the time as a string
            if (runThis)
            {
                // get the time format to use
                string timeFormat = StringFieldValue.GetValueA(heroKitObject, 1);

                // get the start & current date
                TimeSpan timeSpan = new TimeSpan(savedGame.playtimeDays, savedGame.playtimeHours, savedGame.playtimeMinutes, savedGame.playtimeSeconds);

                // create the string
                totalGameplay = String.Format(timeFormat, (int)timeSpan.TotalDays, (int)timeSpan.TotalHours, (int)timeSpan.TotalMinutes, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                // save the time
                StringFieldValue.SetValueB(heroKitObject, 2, totalGameplay);
            }

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Total Gameplay: " + totalGameplay + "\n" +
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