using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsSound : MonoBehaviour
{
    private AudioSource Attackaudio;
    private AudioSource Moveaudio;
    private AudioSource Deadaudio;

    public AudioClip AttackSound;
    public AudioClip MoveSound;
    public AudioClip DeadSound;



    // Start is called before the first frame update
    void Start()
    {

      

        this.Attackaudio = this.gameObject.AddComponent<AudioSource>();
        this.Attackaudio.clip = this.AttackSound;
        this.Attackaudio.loop = false;

        this.Moveaudio = this.gameObject.AddComponent<AudioSource>();
        this.Moveaudio.clip = this.MoveSound;
        this.Moveaudio.loop = false;

        this.Deadaudio = this.gameObject.AddComponent<AudioSource>();
        this.Deadaudio.clip = this.DeadSound;
        this.Deadaudio.loop = false;
    }
    public void StartAttackSound()
    {
        this.Attackaudio.Play();
    }
    public void StartMoveSound()
    {
        this.Moveaudio.Play();
    }
    public void StartDeadSound()
    {
        this.Deadaudio.Play();
    }
    //IEnumerator monsterGrowl(float waitTime)
    //{
    //    while (true)
    //    {

    //        yield return new WaitForSeconds(waitTime);
    //        this.Moveaudio.Play();
    //    }
    //}
    // Update is called once per frame
    void Update()
    {

        this.Attackaudio.volume = GameObject.Find("UIController").GetComponent<UIController>().Vol;
        this.Moveaudio.volume = GameObject.Find("UIController").GetComponent<UIController>().Vol;
        this.Deadaudio.volume = GameObject.Find("UIController").GetComponent<UIController>().Vol;

        // StartCoroutine(monsterGrowl(5.0f));
    }
}
