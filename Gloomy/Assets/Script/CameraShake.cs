using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float shakeTime;
    float shakePower;
    Vector3 firstPos;
    // Start is called before the first frame update
    void Start()
    {
        
        shakePower = 0.03f;
    }
    public void VibrateForTime(float time)
    {
        shakeTime = time;
    }

    // Update is called once per frame
    void Update()
    {

        if (shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * shakePower + firstPos;
            shakeTime -= Time.deltaTime;
        }

        else
        {
            shakeTime = 0.0f;
            transform.position = firstPos;
            //canvas.renderMode = RenderMode.ScreenSpaceCamera;
        }
    }
}
