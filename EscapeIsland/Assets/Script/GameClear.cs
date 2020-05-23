using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip time_plus;
    public bool play_sound;
    public GameObject pause;
    public GameObject bgm;

    GameObject obj;
    public bool clear;

    void Start()
    {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.loop = false;
        this.audio.clip = this.time_plus;
        obj = this.gameObject;
        obj.SetActive(false);
        clear = false;
        play_sound = false;
    }

    void Update()
    {
        if (play_sound)
        {
            this.audio.volume = pause.GetComponent<GamePause>().e_value;
            this.audio.Play();            
            bgm.GetComponent<BGM>().play = false;
            play_sound = false;            
        }
    }

    public void goTitle()
    {
        SceneManager.LoadScene("Title");
        obj.SetActive(false);
        clear = false;
    }

    public void quit()
    {
        Application.Quit();
    }
}
