using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MonsertCount
{
    public int Girl;
    public int Humandoll;
    public int Muppet;
}

public class GameManager : MonoBehaviour {
 
    public GameObject monsterGirl;
    public GameObject monsterHumandoll;
    public GameObject monsterMuppet;
    public GameObject monsterballerina;
    public GameObject goldcard;
    public GameObject expcard;
    public GameObject Droptile;
    GameObject TempMonster;

    public int maxCount = 0;
 
    public int cardNum = 0;
    int stageNum;

    Vector3 MonsterSpot;

    List<GameObject> MonsterList = new List<GameObject>();
    List<GameObject> CardList = new List<GameObject>();
    float RandomCreate ;
    MonsertCount monsertCount;

    // Use this for initialization
    void Start () {
        
        stageNum = 1;
        switch(stageNum)
        {
            case 1:
                monsertCount.Girl = 5;
                monsertCount.Humandoll = 3;
                monsertCount.Muppet = 1;
                break;
            default:
                break;
        }

        maxCount = monsertCount.Girl + monsertCount.Humandoll + monsertCount.Muppet;

        MonsterSpot.y = MonsterSpot.y + 5.0f;
        //for(int i = 0; i<9;i++)
        // {
        //     SelectMonsterType();
        //     //maxCount = monsertCount.Girl + monsertCount.Humandoll + monsertCount.Muppet;
        // }
        for (int i = 0; i < 5; i++)
        {
            Instantiate(Droptile, new Vector3(28+8*i, 16-2*i, 0), Quaternion.identity);
        }
        MonsterListSet();
        CreateMonster();
        
    }
	
    public void CreateCard(Vector3 pos,int monstertype,int cardtype)                                        // 카드 생성
    {

        GameObject.Find("player").GetComponent<Playerstat>().monstercounter++;
        GameObject.Find("player").GetComponent<Playerstat>().characteristicCounter++;

        if (cardtype == 0)
        {
            GameObject _obj = Instantiate(goldcard, pos, Quaternion.identity) as GameObject;
            CardList.Add(_obj);
        }
        else if (cardtype == 1)
        {
            GameObject _obj = Instantiate(expcard, pos, Quaternion.identity) as GameObject;
            CardList.Add(_obj);
        }
        CardList[cardNum].GetComponent<Card>().thisNum = cardNum;
        CardList[cardNum].GetComponent<Card>().monsterType = monstertype;
        CardList[cardNum].GetComponent<Card>().cardType = cardtype;
        cardNum++;
    }
    public void DestroyCard(int thisNum)                                                                    //카드 제거
    {
        cardNum--;
        CardList.RemoveAt(thisNum);
        for (int i = thisNum; i < cardNum; i++)
        { 
            CardList[i].GetComponent<Card>().thisNum--;
        }
    }
    void CreateMonster()                                                                                    //몬스터 생성
    {
        for (int i = 0; i < maxCount; i++)
        {
            MonsterSpot = GameObject.Find("MonsterSpot" + i).transform.position;
            Instantiate(MonsterList[i], MonsterSpot, Quaternion.identity);
        }

    }
    
    void MonsterListSet()
    {
        for (int i = 0; i < monsertCount.Girl; i++)
        { MonsterList.Add(monsterGirl); }

        for (int i = 0; i < monsertCount.Humandoll; i++)
        { MonsterList.Add(monsterHumandoll); }

        for (int i = 0; i < monsertCount.Muppet; i++)
        { MonsterList.Add(monsterMuppet); }

        for (int i = 0; i < maxCount-1 ; i++)
        {
            int a = Random.Range(0, maxCount-1);            
            TempMonster = MonsterList[i];
            MonsterList[i] = MonsterList[a];
            MonsterList[a] = TempMonster;
        }
    }

    // Update is called once per frame
    void Update () {
        //RandomCreate = Random.Range(0f, 8f);
       
    }
}
