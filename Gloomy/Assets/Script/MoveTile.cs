using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    GameObject tile;
    Vector3 tilePos;
    int a;
    // Start is called before the first frame update
    void Start()
    {
        a = 1;
        tilePos = transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
       
        if (transform.localPosition.y >= tilePos.y + 14.0f)
            a = -1;
        else if (transform.localPosition.y < tilePos.y - 14.0f)
            a = 1;
 
        transform.Translate(Vector3.up * 5f * Time.deltaTime * a);
    }
}
