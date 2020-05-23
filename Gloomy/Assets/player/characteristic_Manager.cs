using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characteristic_Manager : MonoBehaviour
{


    public bool firststic;
    public bool secondstic;
    public bool thirdstic;

    // Start is called before the first frame update
    void Start()
    {
        firststic = false;
        secondstic = false;
        secondstic = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(firststic)
        {
            a_a();
        }
        if(secondstic)
        {
            a_b();
        }
        if(secondstic)
        {
            a_c();
        }
    
    }

    //Lv1

    public void a_a()
    {

        if (GameObject.Find("player").GetComponent<Playerstat>().characteristicCounter < 20)
            return;

        if(GameObject.Find("player").GetComponent<Playerstat>().characteristicCounter == 20)
        {

            GameObject.Find("player").GetComponent<Playerstat>().characteristic += 10;

        }


    }

    public void a_b()
    {

        if (GameObject.Find("player").GetComponent<Playerstat>().characteristicCounter < 20)
            return;

        if (GameObject.Find("player").GetComponent<Playerstat>().characteristicCounter == 20)
        {

            GameObject.Find("player").GetComponent<Playerstat>().maxhp += 10;

            GameObject.Find("player").GetComponent<Playerstat>().plusdef += 10;
        }

    }

    public void a_c()
    {

        if (GameObject.Find("player").GetComponent<Playerstat>().characteristicCounter < 20)
            return;

        if (GameObject.Find("player").GetComponent<Playerstat>().characteristicCounter == 20)
        {

            GameObject.Find("player").GetComponent<Playerstat>().plusluck += 10;

        }

    }


    //Lv3
    public void b_a()
    {

        GameObject.Find("player").GetComponent<playermovement>().teckslidingSpeed -= 7;
        GameObject.Find("player").GetComponent<playermovement>().maxdodge++;



    }
    public void b_b()
    {

        GameObject.Find("player").GetComponent<Playerstat>().plusdodge += 10;
        GameObject.Find("player").GetComponent<Playerstat>().plusluck += 10;



    }

    public void b_c()
    {

        GameObject.Find("player").GetComponent<Playerstat>().plushp += 10;
        GameObject.Find("player").GetComponent<Playerstat>().plusdef += 10;


    }


    //Lv8

    public void c_a()
    {
        GameObject.Find("player").GetComponent<playermovement>().maxjump += 1;
    }

    public void c_b()
    {
        
        GameObject.Find("player").GetComponent<playermovement>().highjumpactive = false;
        GameObject.Find("player").GetComponent<playermovement>().jumpPower += 2.5f;

    }

    public void c_c()
    {


        GameObject.Find("player").GetComponent<playermovement>().chardash = true;

    }


    //Lv13

    public void d_a()
    {
        GameObject.Find("player").GetComponent<Playerstat>().gold += 10;

    }

    public void d_b()
    {
        GameObject.Find("player").GetComponent<playermovement>().skillup = true;
    }

    public void d_c()
    {
        GameObject.Find("player").GetComponent<Playerstat>().monstercountermax -= 1;
    }


    //Lv17

    public void e_a()
    {
        GameObject.Find("player").GetComponent<Playerstat>().damage += 20;
    }
    public void e_b()
    {
        GameObject.Find("player").GetComponent<playermovement>().skilatk = true;
    }
    public void e_c()
    {
        GameObject.Find("player").GetComponent<Playerstat>().skilldamage += 15;
    }

    //Lv20

    public void f_a()
    {
        GameObject.Find("player").GetComponent<Playerstat>().damage = GameObject.Find("player").GetComponent<Playerstat>().damage * 2;
    }

    public void f_b()
    {
        GameObject.Find("player").GetComponent<Playerstat>().golddamageExecution = true;
    }

    public void f_c()
    {
        GameObject.Find("player").GetComponent<Playerstat>().maxhp += 300;
        GameObject.Find("player").GetComponent<Playerstat>().def += 30;
        GameObject.Find("player").GetComponent<Playerstat>().damage += 20;
        GameObject.Find("player").GetComponent<Playerstat>().luck += 10;
        GameObject.Find("player").GetComponent<Playerstat>().power += 1.3f;
    }



}
