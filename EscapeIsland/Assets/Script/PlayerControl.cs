using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip jumpSound;
    public AudioClip landingSound;

    Animator animator;

    public int jumpCount;
    bool isjump;

    public GameObject pause;
    public GameObject clear;
    public GameObject over;

    void Start()
    {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.loop = false;
        jumpCount = 2;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        this.audio.volume = pause.GetComponent<GamePause>().e_value;

        if (clear.GetComponent<GameClear>().clear || pause.GetComponent<GamePause>().pause || over.GetComponent<GameOver>().over)
            return;

        if (this.GetComponent<Rigidbody>().velocity == new Vector3(0.0f, 0.0f, 0.0f) && isjump)
        {
            this.audio.clip = this.landingSound;
            if (this.audio.isPlaying == false)
                this.audio.Play();

            animator.SetBool("isJump", false);
            jumpCount = 2;
            isjump = false;
        }

        if (jumpCount == 0)
            animator.SetBool("isJump", true);

        animator.SetBool("isRun", false);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward * 3.0f * Time.deltaTime);
            animator.SetBool("isRun", true);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.back * 3.0f * Time.deltaTime);
            animator.SetBool("isRun", true);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(0.0f, -90.0f * Time.deltaTime, 0.0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(0.0f, 90.0f * Time.deltaTime, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            if (jumpCount == 1)
                animator.SetBool("isJump", false);
            else
                animator.SetBool("isJump", true);

            this.audio.clip = this.jumpSound;
            if (this.audio.isPlaying == false)
                this.audio.Play();

            this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 5.0f * 1.5f, 0.0f);
            jumpCount--;
            isjump = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(true);
            pause.GetComponent<GamePause>().pause = true;
        }

    }
}

