using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    // Update is called once per frame
	void Update ()
    {
        Vector3 rotation = new Vector3(30, 60, 90);
        rotation *= Time.deltaTime;
        transform.Rotate(rotation);
	}
}
