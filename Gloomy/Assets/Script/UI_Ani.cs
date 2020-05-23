using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ani : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameObject.Find("UIController").GetComponent<UIController>().AniBool)
            {
                animator.SetBool("ani", true);
                GameObject.Find("UIController").GetComponent<UIController>().AniBool = false;
            }
        }
    }

    void aniStop()
    {
        animator.SetBool("ani", false);
    }
}
