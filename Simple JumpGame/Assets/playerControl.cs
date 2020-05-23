using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerControl : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip jumpSound;
    public AudioClip startSound;
    public AudioClip landingSound;

    public Slider jumeGauge;

    public Text Life;
    public Text Success;

    public float power = 0.0f;
    public float POWERPLUS = 300.0f;
    public float MAXPOWER = 300.0f;

    public static int playerLife = 5;
    public static int successNum = 0;

    public float goalTimer;

    private Vector3 gaugePos = new Vector3(0.0f, 2.5f, 0.0f);

    void Start()
    {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.loop = false;
        this.audio.clip = this.startSound;
        this.audio.Play();
    }

    void Update()
    {
        //점프 게이지
        jumeGauge.value = power / MAXPOWER;
        jumeGauge.transform.position = gaugePos + this.transform.position;

        //목숨 텍스트
        Life.text = "Life : " + playerLife;
        //성공 텍스트
        Success.text = "Clear Number : " + successNum;

        // 골에 위치하면
        if (GameObject.Find("Goal").GetComponent<goalControl>().is_collided)
        {
            if (goalTimer > 1.5)
            {
                successNum++;
                SceneManager.LoadScene("Main");
            }
            goalTimer += Time.deltaTime;

            this.audio.clip = this.landingSound;
            if (this.audio.isPlaying == false)
                this.audio.Play();
        }


        // 왼쪽 버튼이 눌려있는 동안
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            if (power >= MAXPOWER)
            {
                POWERPLUS = -POWERPLUS;
                power = MAXPOWER;
            }
            if (power <= 0)
            {
                POWERPLUS = -POWERPLUS;
                power = 0;
            }

            power += POWERPLUS * Time.deltaTime;
        }

        // 왼쪽 버튼을 때 놓으면
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
        {
            this.audio.clip = this.jumpSound;
            if (this.audio.isPlaying == false)
                this.audio.Play();

            // 비축한 힘을 x와 y에 반영해서 오른쪽 위로 점프!
            this.GetComponent<Rigidbody>().AddForce(new Vector3(power, power * 1.5f, 0));
            power = 0.0f; // 힘을 0으로
        }

        // 지면보다 아래(-5.0f)로 떨어지면
        if (this.transform.position.y < -5.0f)
        {
            playerLife--;
            SceneManager.LoadScene("Main"); // 씬을 다시 로드
        }

        // esc를 누르면 게임 나가기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // R키를 누르면 다시하기
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerLife--;
            SceneManager.LoadScene("Main");
        }

        //목숨이 끝나면
        if (playerLife == 0)
        {
            SceneManager.LoadScene("Game Over");
            playerLife = 5;
            successNum = 0;
        }
    }
}
