using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Card : MonoBehaviour
{
    static class UpDown
    {
        public const int Up = 1;
        public const int Down = -1;
    }
    private SpriteRenderer cardImage;
    public Sprite Cardfront; 
    public Sprite Cardback;
   
    public GameObject TakeEffect;
    public GameObject Player;
    public int monsterType;
    public int cardType;
    public int thisNum;
    int cardRebound;
    int gold = 0;
    int exp = 0;
    int itemWaitTime;
    int flyspeed;
    float flytime = 0;
    float rotatespeed;
    float positionY;
    float Coefficient;
    float expstate;
    float goldstate;

    private void Awake()
    {
        cardImage = GetComponent<SpriteRenderer>();
        cardImage.sprite = Cardfront;
        thisNum = GameObject.Find("GameManager").GetComponent<GameManager>().cardNum;
        itemWaitTime = 3;
        rotatespeed = 200f;
        flyspeed = 5;
        cardRebound = UpDown.Up;
        positionY = transform.localPosition.y;
        Coefficient = 1.0f;
        expstate = 1.0f;
        goldstate = 1.0f;
    }
    void Start()
    {
        //Instantiate(Paticle, transform.position- new Vector3(0.0f,1f,0.0f), Paticle.transform.rotation);
        //Paticle.GetComponent<Render>().sortingOrder = 5;
    }
    void CheckMonsterType()
    {
       
        switch (monsterType)
        {
            case 0:
                gold = 30/*(*stage*몬스터계수*Coefficient)*/;
                exp = 100;/*(*stage*몬스터계수*Coefficient)*/
                break;
            case 1:
                gold = 120/*(*stage*몬스터계수*Coefficient)*/;
                exp = 300/*(*stage*몬스터계수*Coefficient)*/;
                break;
            case 2:
                gold = 200/*(*stage*몬스터계수*Coefficient)*/;
                exp = 500/*(*stage*몬스터계수*Coefficient)*/;
                break;
            case 3:
                gold = 1000/*(*stage*몬스터계수*Coefficient)*/;
                exp = 1000/*(*stage*몬스터계수*Coefficient)*/;
                break;
        }
        CheckCardType();

    }
    void CheckCardType()
    {
        
        switch (cardType)
        {
            case 0:
                gold = gold * (int)goldstate;
                exp = 0;
                break;
            case 1:
                exp = exp * (int)expstate;
                gold = 0;
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            CheckMonsterType();
            GameObject.Find("player").GetComponent<Playerstat>().UpDatePlayerExp(exp);
            GameObject.Find("player").GetComponent<Playerstat>().UpDatePlayerGold(gold);
            GameObject.Find("GameManager").GetComponent<GameManager>().DestroyCard(thisNum);    
            Destroy(gameObject);
            Instantiate(TakeEffect, transform.position, TakeEffect.transform.rotation);
            //Destroy(TakeEffect, 3.0f);
        }
    }
    void RotateCard()
    {
        gameObject.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotatespeed);

        if (0.5 <= gameObject.transform.rotation.y || -0.5 >= gameObject.transform.rotation.y)// && gameObject.transform.rotation.y < 270f)
            cardImage.sprite = Cardback;
        else if (0.5 > gameObject.transform.rotation.y || -0.5 < gameObject.transform.rotation.y)
            cardImage.sprite = Cardfront;
        else
            Debug.Log(gameObject.transform.rotation.y);
    }
    void UpdownCard(float positionY)
    {
        if (transform.localPosition.y > positionY + 0.0f)
            cardRebound = UpDown.Down;
        else if (transform.localPosition.y < positionY - 0.6f)
            cardRebound = UpDown.Up;
        transform.Translate(Vector3.up * 0.6f * Time.deltaTime * cardRebound);
    }
    void GetCatd()
    {
        flytime += Time.deltaTime;
        if (itemWaitTime < flytime)
        {
            //Destroy(Paticle);
            cardImage.sprite = Cardfront;
            transform.position = Vector3.Lerp(this.transform.position, GameObject.FindWithTag("Player").transform.position, Time.deltaTime * flyspeed);
        }
    }
    void Rander()
    {
        UpdownCard(positionY);
        RotateCard();
        GetCatd();
    }
    // Update is called once per frame
    void Update()
    {
        //CheckCardType();
        Rander();
    }

}
