using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverControl : MonoBehaviour
{

    public Text countText;
    public float count = 10.0f;

    void Start()
    {

    }

    void Update()
    {
        count -= Time.deltaTime;

        countText.text = string.Format("{0:N1}", count);


        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }

        if (Input.GetKeyDown(KeyCode.Escape) || count <= 0)
        {
            Application.Quit();
        }
    }
}
