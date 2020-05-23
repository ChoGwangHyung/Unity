using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTriger : MonoBehaviour
{


    Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        



    }




   public void TrigerOn()
    {

        collider.enabled = true;



    }

    public void TrigerOff()
    {

        collider.enabled = false;



    }
}
