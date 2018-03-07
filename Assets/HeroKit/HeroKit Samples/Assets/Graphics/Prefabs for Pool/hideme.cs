// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

public class hideme : MonoBehaviour {
    private float t = 0;

	// Use this for initialization
    private void Start () {
	}
	
	// Update is called once per frame
    private void Update () {
        t++;
        if (t > 100)
        {
            t = 0;
            gameObject.SetActive(false);
        }
	}
}
