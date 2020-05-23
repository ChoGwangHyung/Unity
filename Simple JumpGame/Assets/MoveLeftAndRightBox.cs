using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftAndRightBox : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
       
        if (this.transform.position.x > 7.0f)
            speed = -speed;
        if (this.transform.position.x < -5.0f)
            speed = -speed;

        this.transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
