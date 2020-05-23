using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Rigidbody2D rigid;
    SpriteRenderer charrenderer;
    protected Animator animator;
    private float movespeed = 3;
    private float jumppower = 6;
    private int moveOn = 0;
    private bool jumpOn = false;
    private int isjump = 0;
    public GameObject bullet;
    float PlayerExp;
    float PlayerGold;

    // Use this for initialization
    private void Awake()
    {
       
    }
    void Start () {
        animator = gameObject.GetComponentInChildren< Animator >();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        charrenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        PlayerExp = 0;
        PlayerGold = 0;

    }
   
    void jumpstate()
    {
        if (!jumpOn)
        {
            return;
        }
        isjumpimg();
    }
    void isjumpimg()
    {
        if (rigid.velocity.y > 0)
        {
            isjump = 1;
            //animator.SetFloat("Jump", isjump);
        }
        else if (rigid.velocity.y < 0)
        {
            isjump = -1;
        }
    }
    void KeyInput()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            //animator.SetFloat("Direction", 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            charrenderer.flipX = true;
            //animator.SetFloat("Direction", -1);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            charrenderer.flipX = false;
            //animator.SetFloat("Direction", 1);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            jumpOn = true;
            rigid.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, jumppower);
            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (charrenderer.flipX == false)
            {
                Instantiate(bullet, transform.position- new Vector3(-1,0,0), Quaternion.identity);
            }
            else if (charrenderer.flipX == true)
            {
                Instantiate(bullet, transform.position - new Vector3(1, 0, 0), Quaternion.identity);
            }
        }

        transform.position += moveVelocity* movespeed*Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Platform")
        {
            isjump = 0;
            //animator.SetFloat("Jump", isjump);
            jumpOn = false;
            if (col.gameObject.name == "Tilemap (1)")
            {
                Debug.Log("착지");
                this.transform.SetParent(GameObject.Find("Tilemap (1)").transform, true);
            }
            GameObject.Find("Main Camera").GetComponent<cameramove>().triggerOn = true;
           GameObject.Find("Main Camera").GetComponent<cameramove>().UpDateCameraPosition();
            
        }

    }
    public void UpDatePlayerExp(int exp)
    {
        PlayerExp += exp;
        Debug.Log("보유경험치" + PlayerExp);
    }
    public void UpDatePlayerGold(int gold)
    {
        PlayerGold += gold;
        Debug.Log("보유골드" + PlayerGold);
    }

    // Update is called once per frame
    void Update () {

        KeyInput();
        jumpstate();
        
    }
}
