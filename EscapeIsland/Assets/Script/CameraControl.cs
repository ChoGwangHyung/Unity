using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    
	
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Rotate(-90.0f * Time.deltaTime, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Rotate(90.0f * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}
