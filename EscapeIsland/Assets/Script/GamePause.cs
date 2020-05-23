using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    GameObject obj;
    public bool pause;

    public Slider effectSlider;
    public Slider bgmSlider;
    public float b_value;
    public float e_value;

    public GameObject button;
    public GameObject soundControll;

    void Start()
    {
        bgmSlider.value = 1f;
        effectSlider.value = 1f;
        b_value = bgmSlider.value;
        e_value = effectSlider.value;

        obj = this.gameObject;
        obj.SetActive(false);
        soundControll.SetActive(false);
        pause = false;
    }

    void Update()
    {
        b_value = bgmSlider.value;
        e_value = effectSlider.value;
    }

    public void goBack()
    {
        obj.SetActive(false);
        pause = false;
    }

    public void reStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void sound()
    {
        button.SetActive(false);
        soundControll.SetActive(true);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void completeSound()
    {
        button.SetActive(true);
        soundControll.SetActive(false);
    }
}