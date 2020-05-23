using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTile : MonoBehaviour
{
    bool dropon = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit2D(Collider2D col)
{
        if(col.gameObject.tag == "Player")
        {
            dropon = true;
            Destroy(gameObject,5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(dropon == true)
            transform.Translate(Vector3.up * 7f * Time.deltaTime * -1);
    }
}
