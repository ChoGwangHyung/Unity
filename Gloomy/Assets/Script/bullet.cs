using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject Bullet;
    //Vector3 firstpos;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monsterdomain")
        {
            Destroy(Bullet);
        }

    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(this.transform.position,transform.position+ new Vector3(3f,0f,0f), Time.deltaTime*9);
        Destroy(Bullet,2f);
    }
}
