using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottle : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip collect_this;

    GameObject obj;
    Vector3 pos;
    public Text remainBottle;

    public GameObject clear;
    public GameObject pause;

    int bottleNum;
    public int remainBottleNum;

    void Start()
    {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.loop = false;
        obj = this.gameObject;        
        pos = new Vector3(0.0f, 1.0f, 0.0f);
        bottleNum = 1;
        remainBottleNum = 3;
        clear.GetComponent<GameClear>().clear = false;
    }

    void Update()
    {
        this.transform.Rotate(0.0f, -90.0f * Time.deltaTime, 0.0f);
        if (Input.GetKey(KeyCode.X))
        {
            clear.SetActive(true);
            clear.GetComponent<GameClear>().clear = true;
            obj.SetActive(false);
            clear.GetComponent<GameClear>().play_sound = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        bottleNum++;
        remainBottle.text = "남은 병 개수 : " + --remainBottleNum;

        this.audio.clip = collect_this;
        this.audio.Play();
        this.audio.volume = pause.GetComponent<GamePause>().e_value;

        if (bottleNum == 4)
        {
            clear.SetActive(true);
            clear.GetComponent<GameClear>().clear = true;
            obj.SetActive(false);
            clear.GetComponent<GameClear>().play_sound = true;
            return;
        }
        else this.transform.position = GameObject.Find("LastFoothold" + bottleNum).transform.position + pos;
    }
}
