﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    // Update is called once per frame
	void Update ()
    {
        Vector3 rotation = new Vector3(15, 30, 45);
        rotation *= Time.deltaTime;
        transform.Rotate(rotation);
	}
}
