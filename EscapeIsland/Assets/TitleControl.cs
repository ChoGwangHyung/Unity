using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleControl : MonoBehaviour {

    public GameObject TitleButton;
    public GameObject InfoButton;
    public GameObject InfoBack;
    public GameObject TitleBack;

    void Start()
    {
        InfoButton.SetActive(false);
        InfoBack.SetActive(false);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void ControllerInfo()
    {
        TitleButton.SetActive(false);
        TitleBack.SetActive(false);
        InfoButton.SetActive(true);
        InfoBack.SetActive(true);
    }

    public void ControllerInfoBack()
    {
        TitleButton.SetActive(true);
        TitleBack.SetActive(true);
        InfoButton.SetActive(false);
        InfoBack.SetActive(false);
    }
}
