using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour
{
    public Transform targetPlayer;

    Vector3 velocity = Vector3.zero;
    Vector3 targetPos;
    Vector3 firstPos;

    public float smoothTime = 0.75f;

    public float YMaxValue = 0;
    public float YMinValue = 0;
    public float XMaxValue = 0;
    public float XMinValue = 0;

    float shakeTime;
    float shakePower;
    public bool triggerOn;
    float moveTime;

    private void Start()
    {
        shakePower = 0.15f;
        triggerOn = false;
    }
    public void VibrateForTime(float time)
    {
        shakeTime = time;
    }
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
        }
    }

    void FixedUpdate()
    {
        CheckMove();


    }

    void CheckMove()
    {
        firstPos = transform.position;

        if (triggerOn == true)
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime,20f);
    }

    public void UpDateCameraPosition()
    {
        targetPos.y = Mathf.Clamp(targetPlayer.position.y, YMinValue, YMaxValue) + 0.5f;
        targetPos.x = Mathf.Clamp(targetPlayer.position.x, XMinValue, XMaxValue);
        targetPos.z = transform.position.z;
    }
   
    void OnTriggerExit2D(Collider2D col)
    {
       // Debug.Log("떨어짐");
        if (col.gameObject.tag == "Player")
        {
            UpDateCameraPosition();
            triggerOn = true;
        }
        
    }
   
}
