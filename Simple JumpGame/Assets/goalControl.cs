using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goalControl : MonoBehaviour
{
    // 충돌했는가(true), 충돌하지 않았는가(false)를 나타낸다.
    public bool is_collided = false;

    public Text successText;

    void Start()
    {
        successText.text = " ";
    }

    void OnCollisionStay(Collision other)
    {
        this.is_collided = true;
    }

    void Update()
    {
        if (is_collided)
        { // 충돌했으면
          // 화면에 '성공'이라고 표시
            successText.text = "성공";
        }
        else
            successText.text = " ";
    }
}
