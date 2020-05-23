using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{


    public float movePower = 10f;
    public float jumpPower = 10f;
    public int jumpcounter = 0;
    public int attackCounter;
    public int maxjump;

    int attackMax;

    Rigidbody2D rigid;
    new SpriteRenderer renderer;
    Animator animator;

    Vector3 movement;
    Vector2 jumpment;
    bool isJumping = false;
    bool isattcking = false;
    bool isevasioning = false;
    bool dodgecool = true;
    bool attackcool = true;
    bool attackingcool = true;
    bool semicool = false;
    bool semidod = false;
   
    public bool isUnBeatTIme;

    public bool isHitTime;
    public bool highjumpactive;
    public bool chardash;

    public float dodgecooltime;
    public float isdodge;
    public float attackCoolTime;
    public float attackingtime;
    public float semicooltime;
    public float slidingSpeed;
    public float teckslidingSpeed;
    public int dodgecount;

    public float semidodging;
    public float SkillCoolTime;


    bool attackTriger;
    bool skillCool;
    public float skill;
    public int maxdodge;
    public float JumpTimeCounter;
    public float JumpTime;
    bool checkatk;
    bool ismove;
    bool wall = false;
    bool isjump;
    private int direction;
    bool atkcheck;
    bool atko;
    bool atks;
    bool atkt;

    private enum State
    {
        Normal,
        DodgeRoll,
    }
    private enum Direct
    {
        right,
        left,
    }
    private State state;
    private Direct direct;

    private Vector3 slideDir;
    public float dir;




    // Use this for initialization
    void Start()
    {

        rigid = gameObject.GetComponent<Rigidbody2D>();
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        animator = gameObject.GetComponentInChildren<Animator>();


        direct = Direct.right;
        movement = Vector3.right;
        state = State.Normal;
        isjump = false;
        dodgecooltime = 1f;
        attackCoolTime = 1f;
        attackCounter = 0;
        maxjump = 1;
        skillCool = true;
        skill = 3f;
        isdodge = 5f;
        dodgecount = 0;
        teckslidingSpeed = 20f;
        maxdodge = 1;
        semidod = true;
        highjumpactive = true;
        chardash = false;
        checkatk = false;
        isHitTime = false;
        isUnBeatTIme = false;
        ismove = false;
        attackMax = 1;
        attackTriger = false;
        atkcheck = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (checkatk)
        {
            attackCounter = 0;
            checkatk = false;
        }

        if (state == State.Normal)
        {
            isJumping = true;


            if (dodgecool || dodgecount < maxdodge && !isjump)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {

                    isevasioning = true;
                    animator.SetBool("isevasion", true);
                    animator.SetBool("isattack", false);
                    attackCounter = 0;


                }
            }

            if (rigid.velocity.y < 0)
            {
                animator.SetInteger("directiony", -1);

            }
            else
            {
                animator.SetInteger("directiony", 0);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                PlayerHIt();

            }




        }

        else if (state == State.DodgeRoll)
        {


            ismove = true;

            if (!isjump)
            {
                DodgeRolling();
            }

            else if (isjump && chardash)
            {

                animator.SetBool("iswall", true);
                dash();
            }
            else
            {
                state = State.Normal;
            }



        }




    }




    void FixedUpdate()
    {
        if (!ismove)
        {
            Move();
        }

        if (highjumpactive)
        {
            Jump();
        }

        else if (!highjumpactive)
        {
            HIghJump();
        }

        attack();
        evasion();
        // walljump();
        Skill();

    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {

            animator.SetBool("isattack", false);
            attackCounter = 0;

            moveVelocity = Vector3.left;
            direct = Direct.left;
            animator.SetBool("ismove", true);
            //renderer.flipX = true;


            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);




        }

        else if (Input.GetKey(KeyCode.RightArrow) == true)
        {


            animator.SetBool("isattack", false);
            attackCounter = 0;

            moveVelocity = Vector3.right;
            direct = Direct.right;
            animator.SetBool("ismove", true);
            // renderer.flipX = false;

            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);





        }

        else
        {
            animator.SetBool("ismove", false);

        }

        transform.position += moveVelocity * movePower * Time.deltaTime;


    }

    void Jump()
    {

        if (!isJumping && wall)
            return;


        if (jumpcounter < maxjump)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                animator.SetBool("isattack", false);
                attackCounter = 0;

                animator.SetTrigger("dojump");
                animator.SetBool("isjump", true);
                isjump = true;
                rigid.velocity = Vector2.zero;
                Vector2 jumpment = new Vector2(0, jumpPower);
                rigid.AddForce(jumpment, ForceMode2D.Impulse);
                jumpcounter++;

            }
        }



        else if (jumpcounter == maxjump)
        {
            isJumping = false;

        }



    }

    void HIghJump()
    {


        if (jumpcounter == 0 && Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("isattack", false);
            attackCounter = 0;

            isjump = true;
            JumpTimeCounter = JumpTime;
            rigid.velocity = Vector2.up * jumpPower;
            jumpcounter++;

        }

        if (Input.GetKey(KeyCode.Z) && isjump == true)
        {
            if (JumpTimeCounter > 0)
            {
                rigid.velocity = Vector2.up * jumpPower;
                JumpTimeCounter -= Time.deltaTime;
            }

            else
            {
                isjump = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            isjump = false;

        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Platform")
        {
            animator.SetBool("isjump", false);

            isJumping = false;
            isjump = false;
            jumpcounter = 0;


            wall = false;
            GameObject.Find("Main Camera").GetComponent<cameramove>().triggerOn = true; ;
            GameObject.Find("Main Camera").GetComponent<cameramove>().UpDateCameraPosition();
        }


    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "wall")
        {
            wall = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {


        if (other.gameObject.tag == "wall")
        {
            wall = false;


        }

    }

    void attack()
    {

        if (attackTriger)
            return;
        if (attackCounter == attackMax)
            return;

        if(Input.GetKeyDown(KeyCode.C))
        {
            attackCounter++;
            atko = true;
            atkcheck = true;
            animator.SetBool("isattack", true);
            //if(!atko)
            //{
            //    attackCounter++;
            //    atko = true;
            //    atkcheck = true;
            //    animator.SetBool("isattack", true);
            //    Debug.Log("f");
            //}
            //else if(atko)
            //{
            //    atkcheck = true;
            //    attackCounter++;
            //    atks = true;
            //    animator.SetBool("attack2", true);

            //    Debug.Log("s");
            //}

            //else if (atks)
            //{
            //    atkcheck = true;
            //    attackCounter++;
            //    atkt = true;
            //    animator.SetBool("attack3", true);

            //    Debug.Log("t");

            //}
        }


    }

    void startAtk()
    {
        attackTriger = true;
        if(attackMax < 3)
        attackMax++;
    }

    void endAtk()
    {
        if(atko)
        {
            Debug.Log(attackCounter);
            atko = false;
            animator.SetBool("isattack", false);
            if (attackCounter == 2)
            {
                atks = true;
                animator.SetBool("attack2", true);
            }
            Debug.Log("1");
        }
        else if (atks)
        {
            Debug.Log(attackCounter);
            animator.SetBool("attack2", false);
            atks = false;
            if (attackCounter > 3)
            {
                atkt = true;
                animator.SetBool("attack3", true);
            }
            Debug.Log("2");
        }

        else if (atkt)
        {
            Debug.Log(attackCounter);
            animator.SetBool("isattack", false);
            animator.SetBool("attack2", false);
            animator.SetBool("attack3", false);
            atko = false;
            atks = false;
            atkt = false;
            atkcheck = false;
            Debug.Log("3");
        }
    }

    void firstAttackEvnent()
    {

        
    }
    void secondAttackEvnent()
    {

     
    }
    void thirdAttackEvnent()
    {

       

    }


    void attackTrigerStart()
    {
        attackTriger = true;
        attackMax++;
    }


    void attackTrigerEnd()
    {

        StartCoroutine("ATKCool");

    }


    void Attack_reset()
    {

        checkatk = true;

    }


    void Skill()
    {
        if (!skillCool)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("aa");
            skillCool = false;
            StartCoroutine(SkillCooling(skill));
        }

    }


    void evasion()
    {
        if (!isevasioning || !dodgecool)
        {
            animator.SetBool("isevasion", false);
          
            return;
        }

        slidingSpeed = teckslidingSpeed;

        state = State.DodgeRoll;

        if (dodgecount < maxdodge)
        {
            dodgecount++;
        }
        else if(dodgecount == maxdodge)
        {
            
        }
       
        if (semidod && maxdodge == 2)
        {
            StartCoroutine(semidodge(isdodge));
            semidod = false;
            
        }

        animator.SetBool("isevasion", false);
        isevasioning = false;


    }


    void DodgeRolling()
    {
        if (direct == Direct.right)
        {
            movement = Vector3.right;
        }
        else
        {
            movement = Vector3.left;
        }
       
        transform.position += movement * slidingSpeed * Time.deltaTime;
        slidingSpeed -= slidingSpeed * 10f * Time.deltaTime;

        if (slidingSpeed < 1f)
        {
            state = State.Normal;
            
            if (dodgecount == maxdodge)
            {
                dodgecool = false;
                ismove = false;
                StartCoroutine(CoolTime(isdodge));
            }




        }
    }


    void dash()
    {
        


        if (direct == Direct.right)
        {
            movement = Vector3.right;
            dir = -1;

        }
        else
        {
            movement = Vector3.left;
            dir = 1;
        }

        transform.position += movement * slidingSpeed * Time.deltaTime;
        slidingSpeed -= slidingSpeed * 10f * Time.deltaTime;
 
        if (slidingSpeed < 1f)
        {
            state = State.Normal;

            if (dodgecount == maxdodge)
            {
                dodgecool = false;
                ismove = false;
                StartCoroutine(CoolTime(isdodge));
            }




        }
    }




    public void playerDie()
    {

        Debug.Log("die");

    }
   

    public void PlayerHIt()
    {
        Vector2 attackedVelocity = Vector2.zero;
        isUnBeatTIme = true;

        if (!isHitTime)
        {

            if (direct == Direct.right)
            {
                attackedVelocity = new Vector2(-2f, 0f);
            }
            else
            {
                attackedVelocity = new Vector2(2f, 0f);
            }

            
            animator.SetTrigger("hit");
            rigid.AddForce(attackedVelocity, ForceMode2D.Impulse);
           
            StartCoroutine("HitTime");
        }
    }


    //void walljump()
    //{


    //    if (!wall)
    //    {

    //        animator.SetBool("iswall", false);

    //        if (rigid.velocity.y < 0)
    //        {
          
          
    //        }
    //        else
    //        {
               



    //            Vector2 jumpment = new Vector2(0, -0);
    //            rigid.AddForce(jumpment, ForceMode2D.Impulse);

    //        }
    //        return;
    //    }
         


    //    if (wall)
    //    {

    //        animator.SetBool("iswall", true);

    //         rigid.velocity = new Vector2(rigid.velocity.x, -0.5f);
    //        jumpcounter = maxjump-1;

    //        if (Input.GetKeyDown(KeyCode.Z))
    //        {

               
    //            animator.SetTrigger("dojump");
    //            animator.SetBool("isjump", true);
    //            isjump = true;
    //            rigid.velocity = Vector2.zero;

    //            Vector2 jumpment = new Vector2(dir* jumpPower,jumpPower );
    //            rigid.AddForce(jumpment, ForceMode2D.Impulse);
               

    //        }


    //    }

   // }

    IEnumerator CoolTime(float dodgecooltime)
    {


        while (dodgecooltime > 1.0f)
        {
     
            dodgecooltime -= Time.deltaTime;

            if(dodgecount == 0)
            {
                dodgecool = true;
                StopCoroutine("CoolTime");
            }
            yield return new WaitForFixedUpdate();
        }
        dodgecool = true;
        dodgecount = 0;



    }

    IEnumerator attackCool(float attackCoolTime)
    {
        

        while (attackCoolTime > 1.0f)
        {

            attackCoolTime -= Time.deltaTime;
            StopCoroutine("semicooling");

            yield return new WaitForFixedUpdate();
        }

  

        attackcool = true;
        attackCounter = 0;
        animator.SetBool("isattack", false);
        animator.SetBool("attack2", false);
        animator.SetBool("attack3", false);

    }


    IEnumerator attacking(float attackingtime)
    {

      
        while (attackingtime > 1.0f)
        {

            attackingtime -= Time.deltaTime;


            yield return new WaitForFixedUpdate();
        }

        attackingcool = true;

    }

    IEnumerator semicooling(float semicooltime)
    {


        while (semicooltime > 1.0f)
        {

            semicooltime -= Time.deltaTime;
         
            yield return new WaitForFixedUpdate();
        }
        
        semicool = true;
        attackCounter = 0;
      



    }


    IEnumerator SkillCooling(float SkillCoolTime)
    {


        while (SkillCoolTime > 1.0f)
        {

            SkillCoolTime -= Time.deltaTime;
          
            yield return new WaitForFixedUpdate();
        }

        skillCool = true;
        dodgecount = 0;


    }

    IEnumerator semidodge(float semidodging)
    {


        while (semidodging > 1.0f)
        {

            semidodging -= Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        semidod = true;
        dodgecount = 0;



    }


    IEnumerator HitTime()
    {
        int countTIme = 0;

        while(countTIme < 10)
        {


            if (countTIme % 2 == 0)
                renderer.color = new Color32(255, 255, 255, 90);
            else
                renderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);

            countTIme++;
        }

        renderer.color = new Color32(255, 255, 255, 255);


        isHitTime = false;

        yield return null;

    }


    IEnumerator ATKCool()
    {
        float a = 1;

        while (a > 0)
        {

            a -= Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        attackTriger = false;
    }

    IEnumerator KeyDown()
    {
        float keyTime = 2f;
        float sedkeyTIme = 2f;
        
        if(attackCounter == 1)
        {
            animator.SetBool("isattack", true);
            while (keyTime > 0f)
            {

                keyTime -= Time.deltaTime;

                Debug.Log("1");
                if (Input.GetKeyDown(KeyCode.C))
                {

                    attackCounter++;
                  
                    Debug.Log("e");
                }
                yield return new WaitForFixedUpdate();
            }


        }

        if (attackCounter == 2)
        {
            while (sedkeyTIme > 0)
            {

                sedkeyTIme -= Time.deltaTime;

                Debug.Log("2");
                if (Input.GetKeyDown(KeyCode.C))
                {
                    
                    attackCounter++;
                  
                    Debug.Log("e");
                }
                yield return new WaitForFixedUpdate();
            }


        }

       

        yield return null;
    }


}





