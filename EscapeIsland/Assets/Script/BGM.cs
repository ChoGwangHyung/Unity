using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {
    private AudioSource audio;
    public AudioClip sound;
    public bool play;

    public GameObject volume;

    void Start()
    {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = sound;
        this.audio.Play();
        play = true;
    }

    void Update () {
        if (!play) this.audio.Stop();
        this.audio.volume = volume.GetComponent<GamePause>().b_value;
	}
}
