using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRock : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        transform.Translate(-3.0f * Time.deltaTime, 0.0f, 0.0f);
    }
}
