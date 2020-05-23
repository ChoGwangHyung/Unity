using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePlusItem : MonoBehaviour {

    GameObject obj;

	void Start () {
        obj = this.gameObject;
	}	
	
	void Update () {
        this.transform.Rotate(0.0f, -90.0f * Time.deltaTime, 0.0f);
    }

    void OnCollisionEnter(Collision other)
    {        
        GameObject.Find("TimeSlider").GetComponent<Timer>().remainTime += 10;
        GameObject.Find("TimeSlider").GetComponent<Timer>().play_audio = true;
        obj.SetActive(false);
    }
}
