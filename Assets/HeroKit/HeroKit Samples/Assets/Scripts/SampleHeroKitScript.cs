// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

public class SampleHeroKitScript : MonoBehaviour
{
    // Property = Health
    // usually the field called health would be private,
    // but I've left it public so that you can see the value
    // in the Unity inspector. Unity does not display properties
    // in the inspector.
    public int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    // Field = name
    public string creatureName = "Stick Thing";

    public void GoToNextLevel(string newName)
    {
        Health *= Health;
        creatureName = "Super " + newName;
    }

    public int GetHalfHealth()
    {
        return (int)Health / 2;
    }
}
