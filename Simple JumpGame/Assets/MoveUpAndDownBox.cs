using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDownBox : MonoBehaviour
{

    public float speed = 5.0f;

    void Update()
    {

        if (this.transform.position.y > 5.0f)
            speed = -speed;
        if (this.transform.position.y < -1.0f)
            speed = -speed;

        this.transform.Translate(0, speed * Time.deltaTime, 0);
    }
}
