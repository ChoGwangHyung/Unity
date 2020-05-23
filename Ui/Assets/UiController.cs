using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameObject Login;

    public InputField input_id;
    public Text id_text;
    public InputField input_password;
    public Text password_text;

    public GameObject PlayGame;

    public GameObject Pause;
    public Slider musicVol;
    AudioSource audio;
    public AudioClip sound;
    bool mute;

    bool pauseUi;

    void Start()
    {
        musicVol.value = 0.5f;
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = sound;
        mute = false;

        id_text.enabled = false;
        password_text.enabled = false;

        pauseUi = false;
        Pause.SetActive(pauseUi);

        PlayGame.SetActive(false);
    }

    void Update()
    {
        if (!mute)
            this.audio.volume = musicVol.value;
        else
            this.audio.volume = 0.0f;

        if (Input.GetKeyDown(KeyCode.Escape))
            pauseUi = !pauseUi;

        Pause.SetActive(pauseUi);
    }

    public void InputId()
    {
        id_text.text = input_id.text;
    }

    public void EndOfInputId()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            id_text.text = input_id.text;
        }
    }

    public void InputPassword()
    {
        password_text.text = input_password.text;
    }

    public void EndOfInputPassword()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            password_text.text = input_password.text;
        }
    }

    public void ClickLogin()
    {
        Login.SetActive(false);
        PlayGame.SetActive(true);
        password_text.enabled = true;
        id_text.enabled = true;
        this.audio.Play();
    }

    public void ClickReturn()
    {
        pauseUi = false;
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    public void ClickMute()
    {
        mute = !mute;
    }
}
