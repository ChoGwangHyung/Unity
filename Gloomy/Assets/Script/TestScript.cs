using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    bool Keydown = false;
    GameObject[,] map = new GameObject[10,20];


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndPrint(2.0F));


        //for (int i = 0; i < 10; i++)
        //{ // 2행 
        //    for (int k = 0; k < 10; k++)
        //    { // 10열 
        //        map[i, k] = Instantiate();
        //        map[i, k].transform.localPosition = new Vector3(k * 50, i * 80, 0);

        ////StartCoroutine("RunFadeOut");
        //    }
        //}
    }
    IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            //Debug.Log("하하");
            yield return new WaitForSeconds(waitTime);
            if (Keydown)
            {
                Keydown = false;
                Debug.Log("ghgh" + waitTime);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Keydown = true;
        }
    }
}

