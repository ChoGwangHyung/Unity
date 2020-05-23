using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour
{    
    public GameObject[] SelectList = new GameObject[4];
    public GameObject IntroText;
    public Text LoadText;
    public GameObject LoadWindow;
    int index;
    bool intro;

    void Start()
    {
        SelectList[0] = GameObject.Find("Game Start");
        SelectList[1] = GameObject.Find("Game Continue");
        SelectList[2] = GameObject.Find("Game Introduction");
        SelectList[3] = GameObject.Find("Game Over");
        index = 0;
        intro = false;
        IntroText.SetActive(false);
        SelectList[index].transform.Find("Text").GetComponent<Text>().color = Color.red;
        LoadWindow.SetActive(false);
        LoadGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index > 0) SelectList[index--].transform.Find("Text").GetComponent<Text>().color = Color.black;
            else
            {
                SelectList[index].transform.Find("Text").GetComponent<Text>().color = Color.black;
                index = 3;
            }
            SelectList[index].transform.Find("Text").GetComponent<Text>().color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index < 3) SelectList[index++].transform.Find("Text").GetComponent<Text>().color = Color.black;
            else
            {
                SelectList[index].transform.Find("Text").GetComponent<Text>().color = Color.black;
                index = 0;
            }
            SelectList[index].transform.Find("Text").GetComponent<Text>().color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (index == 0)
            {
                LoadWindow.SetActive(true);
                SceneManager.LoadScene("Main");
            }
            if (index == 1)
            {
                if (System.IO.File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
                {
                    LoadBool.playerload = true;
                    LoadBool.uiload = true;
                    LoadWindow.SetActive(true);
                    SceneManager.LoadScene("Main");
                }
            }
            if (index == 2)
            {
                if (!intro)
                {
                    IntroText.SetActive(true);
                }
                else
                {
                    IntroText.SetActive(false);
                }
                intro = !intro;
            }
            if (index == 3)
            {
                Application.Quit();
            }
        }
      
    }

    void LoadGame()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/playerInfo.dat") == false)
        {
            LoadText.text = "데이터가 없습니다";
        }
        else
        {
            LoadText.text = "이어하기";
        }
    }
}
