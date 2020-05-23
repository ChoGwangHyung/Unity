using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerstat : MonoBehaviour
{

    public static Playerstat instance;

    public float maxhp; // 최대 hp
    public float plushp;// maxhp = starthp + (constitution/10) + str + (condition/10)
    public float hp; // hp
    public float starthp;

    public GameObject LvEffect;
    

    public float damage; // damege = startdamege + (str/10) * power * condition
    public float plusdamage;
    public float golddamage;
    public float startdamage = 10;



    public float def; // 방어력
    public float plusdef;// def = startdef + (constitution/10) + (condition/10)
    public float startdef = 1;


    public float dodge ;  //  1/10, 회피
    public float plusdodge;

    public float str;  //  1/10, 힘
    public float plusstr;

    public float luck; //  1/10, 행운
    public float plusluck;

    public float condition; // 1/10, 컨디션
    public float pluscondition;

    public float constitution; // 1/10 , 체질
    public float plusconstitution;

    public float power; // 1/100 , 파워
    public float pluspower;
    public float startpower = 1;


    public float skilldamage; // 스킬데미지
    public float plusskilldamage;
    public int startskilldamage = 10;

    public int exp; // 경험치
    public int needExp;
    public int startNeedExp = 20;


    public int charLv;// 레벨
    public int maxLv;


    public int gold; // 골드
    public bool golddamageExecution;

    public float characteristic;
    public float skillcharacteristic;



    public int _enemydamage =1;
    


    public int monstercounter;//몬스터 카운터



    public int characteristicCounter; // 특성 카운터
    public int monstercountermax;//최대 몬스터 카운터
    public int charateristicHit;// 특성 공격카운터
    public int charateristicObtain;//특성 획득 카운터

    
 


    // Start is called before the first frame update
    void Start()
    {

        instance = this;
        charLv = 1;
        plusdef = 0;
        hp = starthp;
        damage = startdamage;
        def = startdef;
        needExp = startNeedExp;
        monstercountermax = 3;
        monstercounter = 0;
        dodge = 0;
        str = 0;
        luck = 0;
        condition = 0;
        constitution = 0;
        power = 1;
        skilldamage = 0;
        characteristic = 0;
        skillcharacteristic = 0;
        maxLv = 20;
        gold = 10;
        characteristicCounter = 0;
        charateristicObtain = 0;
        charateristicHit = 0;
        plushp = 0;
        golddamage = gold / 10;
        golddamageExecution = false;
        starthp = maxhp;
        if (LoadBool.playerload)
        {
            LoadBool.playerload = false;
            GameObject.Find("Data Manager").GetComponent<DataManager>().PLoadData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        maxhp = starthp + plushp + (constitution / 10) + str + (condition / 10) + (100 * charLv);
        def = startdef + plusdef+ (constitution / 10) + (condition / 10) + (1 * charLv); 
        skilldamage = startskilldamage + damage+ plusskilldamage +skillcharacteristic;

        if (!golddamageExecution)
        {

            damage = startdamage + plusdamage + (str / 10) * power + condition + characteristic + (1 * charLv);

        }

        else if (golddamageExecution)
        {

            damage = startdamage + plusdamage + (str / 10) * power + condition + characteristic + (1 * charLv) + golddamage; 

        }

        LvUp();


    }
    public void UpDatePlayerExp(int Exp)
    {
        exp += Exp;
    }
    public void UpDatePlayerGold(int Gold)
    {
        gold += Gold;
    }



    public void HIt(float a)
    {
        float _enemydamage = a;

        float dmg;

        if (def >= _enemydamage)
        {
            dmg = 1;
        }
        else
        {
            dmg = _enemydamage - def;

        }

        hp -= dmg;
        

        if(hp > 0 )
        {

            GameObject.Find("player").GetComponent<playermovement>().isHitTime = true;

        }


        else if (hp <= 0)
        {
            GameObject.Find("player").GetComponent<playermovement>().playerDie();

          
        }
    }

    
    

    void LvUp()
    {      
        if (charLv == maxLv)
        {
            return;
        }

        if(exp >= needExp)
        {

            charLv++;  
            //needExp = startNeedExp * charLv;
            exp = exp - needExp;
            Instantiate(LvEffect, GameObject.Find("player").GetComponent<Transform>().position, Quaternion.identity);
            maxhp = starthp + plushp + (constitution / 10) + str + (condition / 10) + (100 * charLv);
            hp = maxhp;
        }

        if(charLv == 20)
        {
            needExp = 0;
            exp = 0;
        }
    }

   




}


