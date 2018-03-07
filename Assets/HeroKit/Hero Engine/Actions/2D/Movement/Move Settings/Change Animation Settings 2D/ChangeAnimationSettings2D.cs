// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using HeroKit.Scene.Scripts;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Change the settings for a 2D sprite that needs to move.
    /// </summary>
    public class ChangeAnimationSettings2D : IHeroKitAction
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
        public static ChangeAnimationSettings2D Create()
        {
            ChangeAnimationSettings2D action = new ChangeAnimationSettings2D();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            bool changeMove = BoolValue.GetValue(heroKitObject, 2);
            bool changeFace = BoolValue.GetValue(heroKitObject, 3);
            string[] moveNames = new string[0];
            if (changeMove)
            {
                moveNames = new string[9];

                // movement
                moveNames[0] = StringFieldValue.GetValueA(heroKitObject, 5);
                moveNames[1] = StringFieldValue.GetValueA(heroKitObject, 6);
                moveNames[2] = StringFieldValue.GetValueA(heroKitObject, 7);
                moveNames[3] = StringFieldValue.GetValueA(heroKitObject, 8);
                moveNames[4] = StringFieldValue.GetValueA(heroKitObject, 9);
                moveNames[5] = StringFieldValue.GetValueA(heroKitObject, 10);
                moveNames[6] = StringFieldValue.GetValueA(heroKitObject, 11);
                moveNames[7] = StringFieldValue.GetValueA(heroKitObject, 12);
                moveNames[8] = StringFieldValue.GetValueA(heroKitObject, 13);
            }

            string[] faceNames = new string[0];
            if (changeFace)
            {
                faceNames = new string[9];

                // face
                faceNames[0] = StringFieldValue.GetValueA(heroKitObject, 14);
                faceNames[1] = StringFieldValue.GetValueA(heroKitObject, 15);
                faceNames[2] = StringFieldValue.GetValueA(heroKitObject, 16);
                faceNames[3] = StringFieldValue.GetValueA(heroKitObject, 17);
                faceNames[4] = StringFieldValue.GetValueA(heroKitObject, 18);
                faceNames[5] = StringFieldValue.GetValueA(heroKitObject, 19);
                faceNames[6] = StringFieldValue.GetValueA(heroKitObject, 20);
                faceNames[7] = StringFieldValue.GetValueA(heroKitObject, 21);
                faceNames[8] = StringFieldValue.GetValueA(heroKitObject, 22);
            }

            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], moveNames, faceNames);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "";
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, string[] moveNames, string[] faceNames)
        {
            // get the movement script
            HeroSettings2D moveObject = targetObject.GetHeroComponent<HeroSettings2D>("HeroSettings2D", true);

            // add the custom names for your animation 
            if (moveNames != null && moveNames.Length > 0)
            {
                if (moveNames[0] != "") moveObject.moveDefault = moveNames[0];
                if (moveNames[1] != "") moveObject.moveLeft = moveNames[1];
                if (moveNames[2] != "") moveObject.moveRight = moveNames[2];
                if (moveNames[3] != "") moveObject.moveUp = moveNames[3];
                if (moveNames[4] != "") moveObject.moveDown = moveNames[4];
                if (moveNames[5] != "") moveObject.moveUpLeft = moveNames[5];
                if (moveNames[6] != "") moveObject.moveUpRight = moveNames[6];
                if (moveNames[7] != "") moveObject.moveDownLeft = moveNames[7];
                if (moveNames[8] != "") moveObject.moveDownRight = moveNames[8];
            }

            if (faceNames != null && faceNames.Length > 0)
            { 
                if (faceNames[0] != "") moveObject.lookDefault = faceNames[0];
                if (faceNames[1] != "") moveObject.lookLeft = faceNames[1];
                if (faceNames[2] != "") moveObject.lookRight = faceNames[2];
                if (faceNames[3] != "") moveObject.lookUp = faceNames[3];
                if (faceNames[4] != "") moveObject.lookDown = faceNames[4];
                if (faceNames[5] != "") moveObject.lookUpLeft = faceNames[5];
                if (faceNames[6] != "") moveObject.lookUpRight = faceNames[6];
                if (faceNames[7] != "") moveObject.lookDownLeft = faceNames[7];
                if (faceNames[8] != "") moveObject.lookDownRight = faceNames[8];
            }
        }

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