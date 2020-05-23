using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{

    Rigidbody2D rigid;
    new SpriteRenderer renderer;
    Animator animator;
    Vector3 movement;
    Vector2 jumpment;
    private AudioSource audio;
    private AudioSource attackaudio;
    private AudioSource hitaudio;

    public Vector2 monsterpos;


    public GameObject AttackEffect;
    public GameObject Skill1Effect;
    public GameObject Skill2Effect;

    public bool gameset;
    GameObject child;

    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip moveSound;
    public AudioClip dashSound;
    public AudioClip attackSound;


    public bool skillup;
    public bool skilatk;
    private enum State // 상태체크
    {
        Normal,
        DodgeRoll,
    }
    private enum Direct // 방향 
    {
        right,
        left,
    }
    private State state;
    private Direct direct;

    //이동
    public float movePower; // 무브파워
    bool ismove;  //이동체크
    private int direction; //  y축 체크

    //점프
    public float jumpPower; // 점프 파워
    public int jumpcounter; // 현재 점프 카운트
    public int maxjump; // 최대 점프
    bool isJumping = false; // 점프 확인
    public float JumpTime; //하이점프
    public float JumpTimeCounter; // 하이점픔
    bool isjump; // 

    //공격
    public int attackCounter; // 현재 공격 카운트
    bool isattcking = false; // 공격 확인
    bool attackTriger; // 공격 확인
    bool keycheck; // 키입력 확인
    public bool ATKcooling; // 쿨타임
    public bool isattack; // 이동시 공격 막기
    public bool atksound;

    //회피및 대쉬

    bool isevasioning = false; // 대쉬 확인
    bool semicool = false; // max 가 2이상일때 대쉬 쿨타임 확인
    bool dodgecool = true; // 대쉬 쿨타임 확인
    bool semidod; // 회피 쿨타임동안 키입력 막기
    public float dodgecooltime; // 회피 쿨 타임
    public float isdodge;// 회피 확인
    public float slidingSpeed;// 회피 속도
    public float teckslidingSpeed; // 회피 속도
    public int dodgecount;// 회피 카운트
    private Vector3 slideDir; // 회피방향
    public float dir; // 회피 방향
    public int maxdodge; // 최대 회피 카운트 
    public float semidodging; // 쿨타임 체크 

    //피격
    public bool isHitTime;  // 피격 

    //특성
    public bool highjumpactive; // 하이점프 활성화
    public bool chardash;// 공중대쉬 활성화

    // 스킬
    bool skillCool;
    public float skill;
    public float SkillCoolTime;


    //조작 멈춤
    public bool KeyControl;

    // 죽음

    public bool death;
    //bool wall = false; // 벽점프 벽 활성화



    void Start()
    {
            
        rigid = gameObject.GetComponent<Rigidbody2D>();
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        animator = gameObject.GetComponentInChildren<Animator>();
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.hitaudio = this.gameObject.AddComponent<AudioSource>();
        this.attackaudio = this.gameObject.AddComponent<AudioSource>();
        direct = Direct.right;
        state = State.Normal;
        ////
        movement = Vector3.right;
        movePower = 8f; // 이동 파워
        jumpPower = 8f; // 점프 파워
       ////
        isjump = false;
         dodgecooltime = 1f; // 회피 쿨타임
        attackCounter = 0; // 공격 카운트
        maxjump = 1; // 최대 점프 
        skillCool = true;
        skill = 3f; // 스킬 쿨타임
        isdodge = 5f; // 회피 쿨타임 
        dodgecount = 0; // 회피 카운트 
        teckslidingSpeed = 20f; // 회피 속도
        maxdodge = 1; // 최대 회피 
        semidod = true;
        highjumpactive = true;
        chardash = false;
        isHitTime = false;
        ismove = false;
        attackTriger = false;
        keycheck = false;
        isattack = false;
        ATKcooling = true;
        KeyControl = true;
        jumpcounter = 0; // 점프 카운트
        death = false;
        Vector2 monsterpos = Vector2.zero;
        atksound = false;
        skillup = false;
        skilatk = false;
        gameset = false;
    }

    // Update is called once per frame
    void Update()
    {

       
        if (KeyControl)
        {
            if (state == State.Normal)
            {
                isJumping = true;
                if (dodgecool || dodgecount < maxdodge && !isjump)
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                       
                        isattack = false;
                        isevasioning = true;
                        animator.SetBool("isevasion", true);
                        animator.SetBool("isattack", false);
                        attackCounter = 0;
                    }
                }

                if (isattack && ATKcooling && !isjump)
                {
                    if (attackCounter == 3)
                        return;

                    else if (Input.GetKeyDown(KeyCode.C))
                    {
                        keycheck = true;
                        ismove = true;
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


                attack();
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

        this.audio.volume = GameObject.Find("UIController").GetComponent<UIController>().Vol;
        this.attackaudio.volume = GameObject.Find("UIController").GetComponent<UIController>().Vol;
        this.hitaudio.volume = GameObject.Find("UIController").GetComponent<UIController>().Vol;


    }

    void FixedUpdate()
    {
        if (KeyControl)
        {
            Move(); //이동

            if (highjumpactive)
            {
                //하이점프 비활성화
                Jump();
            }
            else if (!highjumpactive)
            {
                //하이점프 활성화
                HIghJump();
            }
            evasion(); //회피
         // walljump(); //벽점프
            Skill(); //스킬
        }
    }

    void Move() // 이동
    {
        Vector3 moveVelocity = Vector3.zero; // 움직임 백터 초기하

        isattack = false; // 공격 체크  false
    
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {

            animator.SetBool("isattack", false);
            attackCounter = 0;

            
               
          
            
            moveVelocity = Vector3.left;
            direct = Direct.left;
            animator.SetBool("ismove", true);
            //renderer.flipX = true;
            Attack_reset();
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
            Attack_reset();
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        }
        else
        {
            animator.SetBool("ismove", false);
            isattack = true;
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }


    void PlayMoveSound()
    {

        this.audio.clip = this.moveSound;
        this.audio.Play();

    }

    void Jump() // 점프
    {
        if (!isJumping)
            return;
        if (jumpcounter < maxjump)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                animator.SetBool("isattack", false);
                animator.SetTrigger("dojump");
                animator.SetBool("isjump", true);
                isjump = true;

                this.audio.clip = this.jumpSound;
                this.audio.Play();

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
    void HIghJump() // 하이점프
    {
        if (jumpcounter == 0 && Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("isattack", false);
            animator.SetBool("isjump", true);
            animator.SetTrigger("dojump");
            attackCounter = 0;
            isjump = true;
            this.audio.clip = this.jumpSound;
            this.audio.Play();
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
            //  wall = false;
            GameObject.Find("Main Camera").GetComponent<cameramove>().triggerOn = true; ;
            GameObject.Find("Main Camera").GetComponent<cameramove>().UpDateCameraPosition();
        }
        if(other.gameObject.tag == "deadzone")
        {
            playerDie();
        }
    } 

    void OnTriggerStay2D(Collider2D other)
    {
        //if (other.gameObject.tag == "wall")
        //{
        //    wall = true;
        //}
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //if (other.gameObject.tag == "wall")
        //{
        //    wall = false;
        //}

    }

    void attack() //공격
    {

        if (attackTriger)
            return;

        if(keycheck)
        {
            if (!atksound)
            {
                this.attackaudio.clip = this.attackSound;
                this.attackaudio.Play();
            }
            else
            {

            }
            attackCounter++;
            animator.SetBool("isattack", true);
            keycheck = false; 
        }
    }

   public void On ()
    {
        // 공격 판정 트리거 On
        GameObject.Find("attacktriger").GetComponent<PlayerAttackTriger>().TrigerOn();
        this.attackaudio.clip = this.attackSound;
        this.attackaudio.Play();
    } // 공격 트리거 켜짐

    public void Off()
    {
        //공격 판정 트리거 Off
        GameObject.Find("attacktriger").GetComponent<PlayerAttackTriger>().TrigerOff();
    } // 공격 트리거 꺼짐

    void startAtk()
    {
        attackTriger = true;
    } // 공격시작 이벤트

    void endAtk()
    {
        ismove = false;
    } // 공격 끝 이벤트


    void SetTirger()
    {
        ATKcooling = false;

    } // 공격 쿨타임 종료 

    void thirdAttackEvnent()
    {
        animator.Play("idle");
    } // 공격 쿨타임 시작


    void attackTrigerStart()
    {
        StartCoroutine("KeyDown");
        
        attackTriger = true;
    } // keydown코루틴 시작

    void attackTrigerEnd()
    {
        StartCoroutine("ATKCool");
        ismove = false;
    } // 공격 쿨타임 시작

    void Attack_reset()
    {
        animator.SetBool("attack3", false);
        animator.SetBool("attack2", false);
        animator.SetBool("isattack", false);
        attackTriger = false;
        attackCounter = 0;

    } // 공격 리셋 

    void Skill()
    {
        if (!skillCool)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {

            if (!skillup)
            {
                if(direct == Direct.right)
                {
                    Skill2Effect.GetComponent<SpriteRenderer>().flipX = false;
                    Instantiate(Skill2Effect, GameObject.Find("skillpos").GetComponent<Transform>().position, Quaternion.identity);
                    
                }
                else
                {
                    Skill2Effect.GetComponent<SpriteRenderer>().flipX = true;
                    Instantiate(Skill2Effect, GameObject.Find("skillpos").GetComponent<Transform>().position, Quaternion.identity);
                   
                }
                
            }
            else if (skillup)
            {

                if (direct == Direct.right)
                {
                    Skill1Effect.GetComponent<SpriteRenderer>().flipX = false;
                    Instantiate(Skill1Effect, GameObject.Find("skillpos").GetComponent<Transform>().position, Quaternion.identity);

                }
                else
                {
                    Skill1Effect.GetComponent<SpriteRenderer>().flipX = true;
                    Instantiate(Skill1Effect, GameObject.Find("skillpos").GetComponent<Transform>().position, Quaternion.identity);

                }
               
            }
          
            skillCool = false;
            StartCoroutine(SkillCooling(skill));
        }
    } // 스킬

    void skillattack()
    {
        if (skilatk)
        {


            if (direct == Direct.right)
            {
                Skill1Effect.GetComponent<SpriteRenderer>().flipX = false;
                Instantiate(Skill1Effect, GameObject.Find("skillatkpos").GetComponent<Transform>().position, Quaternion.identity);

            }
            else
            {
                Skill1Effect.GetComponent<SpriteRenderer>().flipX = true;
                Instantiate(Skill1Effect, GameObject.Find("skillatkpos").GetComponent<Transform>().position, Quaternion.identity);

            }
        }
    }
    void evasion()  // 회피 초기화 
    {
        if (!isevasioning || !dodgecool)
        {
            animator.SetBool("isevasion", false);
          
            return;
        }

        slidingSpeed = teckslidingSpeed;
        isHitTime = true;
        isattack = false;
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

    void DodgeRolling() // 회피 진행
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
            isHitTime = false;
           
            if (dodgecount == maxdodge)
            {
                dodgecool = false;
                ismove = false;
                StartCoroutine(CoolTime(isdodge));
            }




        }
    }

    void dash() // 공중 대쉬 진행 
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
            isHitTime = false;
            isattack = true;
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

       animator.SetBool("isdie",true);
       
        death = true;
        KeyControl = false;
     
    } // 죽음


    void DieEvent()
    {
        animator.SetBool("isdie", false);
    }
   
    public void PlayerHIt() // 피격 
    {
        Vector2 attackedVelocity = Vector2.zero;
        

        if (!isHitTime)
        {

            attackedVelocity = PlayerCheckPosition(monsterpos.x, this.transform.position.x);

            this.hitaudio.clip = this.hitSound;
            this.hitaudio.Play();
            animator.SetTrigger("hit");
            rigid.AddForce(attackedVelocity, ForceMode2D.Impulse);          
            StartCoroutine("HitTime");
        }
    }



    Vector2  PlayerCheckPosition(float otherXposition, float thisXposition)
    {
        Vector2 attackedVelocity = Vector2.zero;
        if (otherXposition == thisXposition)
        {
            return attackedVelocity = new Vector2(0, 2f);
        }    
        else if (otherXposition < thisXposition)
        {
            return attackedVelocity = new Vector2(2f, 2f);
        }    
        else
        {
            
            return attackedVelocity = new Vector2(-2f, 2f);
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

    IEnumerator CoolTime(float dodgecooltime)  // 회피 쿨타임
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

    IEnumerator SkillCooling(float SkillCoolTime) // 스킬 쿨타임
    {


        while (SkillCoolTime > 1.0f)
        {

            SkillCoolTime -= Time.deltaTime;
          
            yield return new WaitForFixedUpdate();
        }

        skillCool = true;
        dodgecount = 0;


    }

    IEnumerator semidodge(float semidodging) // max회피 2이상일때 쿨타임
    {


        while (semidodging > 1.0f)
        {

            semidodging -= Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        semidod = true;
        dodgecount = 0;



    }

    IEnumerator HitTime() // 피격 중 시간 체크
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

    IEnumerator ATKCool() // 공격 쿨타임
    {
        float a = 1.5f;

        while (a > 0)
        {

            a -= Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        ATKcooling = true;
    }

    IEnumerator KeyDown() // 공격 루틴 
    {
        float keyTime = 0.7f;
        float sedkeyTIme = 0.7f;
        
        if(attackCounter == 1)
        {
            
            while (keyTime > 0f)
            {

                keyTime -= Time.deltaTime;
           
           
                if (keycheck)
                {
                    attackCounter++;
                    animator.SetBool("attack2", true);
                    StopCoroutine("KeyDown");
                    keycheck = false;
                     yield return null;
                }

                yield return new WaitForFixedUpdate();
            }
            animator.SetBool("isattack", false);
        
    

        }

        else if (attackCounter == 2)
        {
            while (sedkeyTIme > 0)
            {

                sedkeyTIme -= Time.deltaTime;
                if (keycheck)
                {
                    attackCounter++;
                    animator.SetBool("attack3",true);
                    StopCoroutine("KeyDown");
                  
                    keycheck = false;
                   yield return null;
                }
                yield return new WaitForFixedUpdate();
            }
            animator.SetBool("isattack", false);
            animator.SetBool("attack2", false);
          

        }
        ATKcooling = false;
        animator.SetBool("attack3", false);
        StartCoroutine("ATKCool");
        attackCounter = 0;
        attackTriger = false;
        yield return null;

    }

    
}





