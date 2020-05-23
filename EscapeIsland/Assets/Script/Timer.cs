using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip time_plus;
    public bool play_audio;

    public Slider timeSlider;
    public Text timeText;
    float time;
    int curTime;
    public int remainTime;

    public GameObject clear;
    public GameObject pause;
    public GameObject over;

    void Start()
    {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = time_plus;
        this.audio.loop = false;
        play_audio = false;

        time = 0.0f;
        remainTime = 90;
    }

    void Update()
    {
        if (clear.GetComponent<GameClear>().clear || pause.GetComponent<GamePause>().pause || over.GetComponent<GameOver>().over)
            return;

        if (play_audio)
        {
            this.audio.volume = pause.GetComponent<GamePause>().e_value;
            this.audio.Play();
            play_audio = false;
        }

        if (Input.GetKey(KeyCode.Z)) remainTime = curTime;


        time += Time.deltaTime;
        curTime = (int)time;
        timeSlider.value = time / (float)remainTime;
        timeText.text = "" + (remainTime - curTime);

        if ((remainTime - curTime) <= 0)
        {
            over.GetComponent<GameOver>().over = true;
            over.GetComponent<GameOver>().over_sound = true;
            over.SetActive(true);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene("Main");
    }
}
