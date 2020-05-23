using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MonsterType                                    // 몬스터 타입
{
    girl = 0,
    humandoll = 1,
    muppet = 2,
    ballerina = 3

}
enum Direction
{                                     //이동 방향
    none = 0,
    Left,
    Right,
}
enum State
{                                         //몬스터 상태
    Stop = 0,
    Move,
    Attack,
    Hit,
    Die
}
enum CardType                                       // 생성되는 카드의 종류
{
    Gold = 0,
    Exp
}
public struct Status                                       //몬스터 스텟
{
    public int Hp;
    public int Damage;
    public float Movespeed;
    public float Attackspeed;
    public float Attackrange;
    public float Stance;
    public float KnockBack;
}

public struct Sound                                       //몬스터 사운드
{
    public AudioClip Attack;
    public AudioClip Damage;
    public AudioClip Move;
    public AudioClip Dead;
    public float Attackrange;
    public float Stance;
    public float KnockBack;
}
public class monsMove : MonoBehaviour
{




    MonsterType monsterType;
    Direction direction;
    Direction knockBackdirection;
    Vector3 moveVelocity = Vector3.zero;
    Vector3 monsPosition = Vector3.zero;
    State state;
    Status status;
    CardType cardtype;
    public GameObject AttackEffect;

    private AudioSource audio;
    protected Animator animator;
    GameObject player;
    int CognitiveScope;
    int DirectionSettingTime;
    public float distanceMons;
    float attackTime;
    float moveTime;
    float cardProbability;
    float goldcardPercentage;
    public AudioClip AttackSound;
    
    bool IsAttack = false;



    public void CheckType()                                            //몬스터 타입 확인
    {
        if (gameObject.name == "girl(Clone)")
        {
            monsterType = 0;
        }
        if (gameObject.name == "humandoll(Clone)")
        {
            monsterType = (MonsterType)1;
        }
        if (gameObject.name == "muppet(Clone)")
        {
            monsterType = (MonsterType)2;
        }
        if (gameObject.name == "ballerina(Clone)")
        {
            monsterType = (MonsterType)3;
        }
    }
    public void SetStatus()                                            //몬스터 스텟 적용
    {
        switch ((int)monsterType)
        {
            case 0:
                status.Hp = 30;
                status.Damage = 10;
                status.Attackrange = 1.5f;
                status.Movespeed = 2.0f;
                status.Attackspeed = 3.0f;
                status.Stance = 8.0f;
                break;

            case 1:
                status.Hp = 50;
                status.Damage = 20;
                status.Attackrange = 4.2f;
                status.Movespeed = 2.0f;
                status.Attackspeed = 5.0f;
                status.Stance = 2.0f;
                break;

            case 2:
                status.Hp = 100;
                status.Damage = 50;
                status.Attackrange = 9.0f;
                status.Movespeed = 0f;
                status.Attackspeed = 3.0f;
                status.Stance = 0f;
                break;

            case 3:
                status.Hp = 2000;
                status.Damage = 50;
                status.Attackrange = 20.0f;
                status.Movespeed = 4.0f;
                status.Attackspeed = 5.0f;
                status.Stance = 0f;
                break;
        }
    }
    void Awake()
    {                                               // 초기화
        CheckType();
        SetStatus();
        CognitiveScope = 8;
        DirectionSettingTime = 5;
        goldcardPercentage = 3.0f;
        attackTime = 10.0f;                                    // 최초 모든 몬스터 공격 on
        moveTime = 16.0f;
    }

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindWithTag("Player");
        animator = gameObject.GetComponentInChildren<Animator>();
        knockBackdirection = Direction.none;
        cardProbability = Random.Range(0f, 9f);
        CheckDirection(player.transform.position.x, this.transform.position.x);
       

        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.AttackSound;
        this.audio.loop = false;

    }
    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlatformFirst")
            ChangeDirection(Direction.Right);

        else if (col.gameObject.tag == "PlatformLast")
            ChangeDirection(Direction.Left);

        else if (col.gameObject.tag == "Monsterdomain")
            CrashMonster(col.transform.position.x, transform.position.x);


        else if (col.gameObject.tag == "PlayerAttack")
        {

            GameObject.Find("Main Camera").GetComponent<cameramove>().VibrateForTime(0.2f);
            monsPosition = transform.position;

            Instantiate(AttackEffect, GameObject.Find("attacktriger").GetComponent<Transform>().position, Quaternion.identity);

            CheckDirection(col.transform.position.x, transform.position.x);
            status.Hp = status.Hp - (int)GameObject.Find("player").GetComponent<Playerstat>().damage;
            if (status.Hp > 0)
            {
                if (state == State.Attack)
                    return;
                //Debug.Log(status.Hp);
                state = State.Hit;
            }
            else
            {
                //Debug.Log(status.Hp);
                state = State.Die;
            }
        }
        else if (col.gameObject.tag == "Player")
        {
            if (!GameObject.Find("player").GetComponent<playermovement>().isHitTime)
            {

                GameObject.Find("player").GetComponent<playermovement>().monsterpos = this.transform.position;
                GameObject.Find("player").GetComponent<playermovement>().PlayerHIt();
                GameObject.Find("player").GetComponent<Playerstat>().HIt(status.Damage);




            }
        }

    }
    void OnTriggerStay2D(Collider2D col)
    {
        moveTime += Time.deltaTime;
        if (state == State.Attack)
            return;
        if (moveTime > DirectionSettingTime)
        {
            if (distanceMons < CognitiveScope)
                CheckDirection(player.transform.position.x, this.transform.position.x);

            else if (distanceMons >= CognitiveScope)
                return;

            moveTime = 0;
        }
    }

    int CheckPosition(float otherXposition, float thisXposition)
    {
        if (otherXposition == thisXposition)
            return 0;
        else if (otherXposition < thisXposition)
            return 1;
        else
            return 2;
    }
    void ChangeDirection(Direction dr)
    {
        if (dr == Direction.Left)
        {
            knockBackdirection = Direction.Right;
            direction = Direction.Left;
            moveVelocity = Vector3.left;
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else if (dr == Direction.Right)
        {
            knockBackdirection = Direction.Left;
            direction = Direction.Right;
            moveVelocity = Vector3.right;
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
    }
    void CheckDirection(float otherXposition, float thisXposition)
    {
        int Positioninfo;
        Positioninfo = CheckPosition(otherXposition, thisXposition);

        if (state == State.Attack)
            return;

        if (Positioninfo == 0)
        {
            direction = Direction.none;
            moveVelocity = Vector3.zero;
        }
        else if (Positioninfo == 1)
            ChangeDirection(Direction.Left);

        else if (Positioninfo == 2)
            ChangeDirection(Direction.Right);
    }
    void CrashMonster(float otherXposition, float thisXposition)
    {
        int Positioninfo;
        Positioninfo = CheckPosition(otherXposition, thisXposition);

        if (state == State.Attack)
            return;

        if (Positioninfo == 2)
            ChangeDirection(Direction.Left);

        else
            ChangeDirection(Direction.Right);
    }
    void CheckKnockBack()
    {
        if (state == State.Hit)
        {
            if (knockBackdirection == Direction.Left)
                transform.position = Vector3.Lerp(transform.position, (monsPosition - new Vector3(0.8f, 0, 0)), Time.deltaTime * status.Stance);

            else if (knockBackdirection == Direction.Right)
                transform.position = Vector3.Lerp(transform.position, (monsPosition + new Vector3(0.8f, 0, 0)), Time.deltaTime * status.Stance);

        }
        else
            knockBackdirection = Direction.none;
    }
    void SetMove()
    {
        int Positioninfo;
        Positioninfo = CheckPosition(player.transform.position.x, transform.position.x);

        distanceMons = Vector3.Distance(player.transform.position, transform.position);
        attackTime += Time.deltaTime;
        if (distanceMons < 20)
        {
            if (distanceMons > status.Attackrange && (state != State.Attack) && (state != State.Hit) && (state != State.Die))
            {
                state = State.Move;
            }
            else if (distanceMons < status.Attackrange || state == State.Attack)
            {
                if (attackTime < status.Attackspeed)
                    return;

                if ((direction == Direction.Left && Positioninfo == 1) || (direction == Direction.Right && Positioninfo == 2))
                    state = State.Attack;
            }
            else if (distanceMons == status.Attackrange)
                state = State.Stop;
        }
    }
    void SetAnimatoin()
    {

        switch ((int)state)
        {
            case 0:  //stop
                StartStopMotion();
                break;
            case 1:  //move
                StartMoveMotion();
                break;
            case 2:  //attack
                StartAttackMotion();
                break;
            case 3:  //hit
                StartHitMotion();
                break;
            case 4:  //dead
                StartDeadMotion();
                break;
        }
    }
    void StartStopMotion()
    {
        animator.SetFloat("Direction", 0);
    }
    void StartMoveMotion()
    {
        switch ((int)direction)
        {
            case 0:
                animator.SetFloat("Direction", 0);
                break;

            default:
                animator.SetFloat("Direction", -1);
                break;
        }
        transform.position += moveVelocity * status.Movespeed * Time.deltaTime;
    }

    void StartAttackMotion()
    {
        animator.SetFloat("Attack", 1);
        //IsAttack = true;
    }
    void StartAttackSound()
    {
        this.GetComponent<MonsSound>().StartAttackSound();
    }
    void EndAttackMotion()
    {
        animator.SetFloat("Attack", -1);
        attackTime = 0.0f;
        state = State.Move;
        SetAnimatoin();
    }
    void StartHitMotion()
    {
        animator.SetFloat("Hit", 1);
    }
    void EndHitMotion()
    {
        if (status.Hp > 0)
        {
            //Debug.Log(state);
            animator.SetFloat("Hit", -1);
            state = State.Move;
        }
        else
            state = State.Die;                                                                                              // 죽음
    }
    void StartDeadMotion()                                                                                                                              //죽음모션 실행 함수
    {
        this.transform.Find("Monsterdomain").gameObject.GetComponent<BoxCollider2D>().enabled = false;
        animator.SetFloat("Dead", 1);
    }
    void EndDeadMotion()                                                                                                                                // 죽음 모션이 끝나고 실행
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().maxCount--;
        CreateCard();
        Destroy(gameObject);
    }
    void CreateCard()                                                                                                                                    //카드 생성 함수
    {
        if (cardProbability < goldcardPercentage)
            cardtype = CardType.Gold;
        else
            cardtype = CardType.Exp;

        GameObject.Find("GameManager").GetComponent<GameManager>().CreateCard(transform.position, (int)monsterType, (int)cardtype);
    }
    void UpdateCollider()                                                                                                                                // 폴리곤 콜라이더 업데이트
    {
        if (state == State.Attack)
        {
            PolygonCollider2D pc = gameObject.AddComponent<PolygonCollider2D>();
            pc.isTrigger = true;
            Destroy(pc, 0.015f);
        }
    }
    void Rander()                                                                                                                                         // 게임 화면 그리기
    {

        SetMove();
        SetAnimatoin();
        CheckKnockBack();
        UpdateCollider();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("player").GetComponent<playermovement>().KeyControl)
            Rander();

    }


    void baldie()
    {

        GameObject.Find("player").GetComponent<playermovement>().KeyControl = true;
        GameObject.Find("player").GetComponent<playermovement>().gameset = true;
    }
}