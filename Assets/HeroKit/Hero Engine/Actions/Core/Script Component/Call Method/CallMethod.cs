// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;
using System.Reflection;

namespace HeroKit.Scene.Actions
{
    /// <summary>
    /// Call a method in a component on an object.
    /// </summary>
    public class CallMethod : IHeroKitAction
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
        public static CallMethod Create()
        {
            CallMethod action = new CallMethod();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get the hero object where the parameters are stored
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            UnityObjectField objectData = UnityObjectFieldValue.GetValueA(heroKitObject, 2);
            string scriptName = (objectData.value != null) ? objectData.value.name : "";
            MethodInfo method = MethodValue.GetValue(heroKitObject, 3);
            string[] debugInfo = new string[targetObject.Length];
            bool runThis = (targetObject != null && scriptName != "" && method != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                debugInfo[i] = ExecuteOnTarget(targetObject[i], scriptName, method);


            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string strDebugInfo = (debugInfo != null && debugInfo.Length > 0) ? debugInfo[0] : "";
                string debugMessage = "MonoScript: " + scriptName + "\n" +
                                      "Method: " + method + "\n" +
                                      "Parameters: " + strDebugInfo;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public string ExecuteOnTarget(HeroKitObject targetObject, string scriptName, MethodInfo method)
        {
            //------------------------------------
            // get the component that contains the script
            //------------------------------------

            MonoBehaviour component = HeroKitCommonRuntime.GetComponentFromScript(heroKitObject, targetObject, scriptName);

            //------------------------------------
            // get the parameters for the script
            //------------------------------------
            System.Object[] parameters = new System.Object[0];
            if (component != null)
            {
                parameters = ParameterValue.GetValueA(heroKitObject, 4, method);
            }
            else
            {
                Debug.LogError(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, "Component does not exist on the game object."));
            }

            //------------------------------------
            // invoke the method in the script
            //------------------------------------
            System.Object returnValue = method.Invoke(component, parameters);

            //------------------------------------
            // save the return value in the hero object
            //------------------------------------
            ParameterValue.SetValueB(heroKitObject, 20, 21, returnValue);

            // debug string
            string debugInfo = "";
            if (heroKitObject.debugHeroObject)
            {
                string argumentNames = "";
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] != null)
                        argumentNames += ", arg[" + i + "]=" + parameters[i].ToString();
                }
                string returnName = "";
                if (returnValue != null)
                {
                    returnName += ", return=" + returnValue.ToString();
                }

                debugInfo = argumentNames + returnName;
            }
            return debugInfo;
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