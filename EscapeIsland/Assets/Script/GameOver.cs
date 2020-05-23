using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    private AudioSource audio;
    public AudioClip overSound;

    GameObject obj;    
    public bool over;
    public GameObject pause;
    public GameObject bgm;
    public bool over_sound;

    void Start()
    {
        obj = this.gameObject;
        
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.loop = false;
        this.audio.clip = this.overSound;

        obj.SetActive(false);
        over = false;
        over_sound = false;
    }

    void Update()
    {
        if (over_sound)
        {
            this.audio.volume = pause.GetComponent<GamePause>().e_value;
            this.audio.Play();
            bgm.GetComponent<BGM>().play = false;
            over_sound = false;
        }

    }

    public void reGame()
    {
        SceneManager.LoadScene("Main");
        obj.SetActive(false);
        over = false;
    }

    public void quit()
    {
        Application.Quit();
    }
}
