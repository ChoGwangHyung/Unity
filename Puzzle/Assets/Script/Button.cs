using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("Main");
    }
}
