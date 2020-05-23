using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoStartPoint : MonoBehaviour {

   public GameObject player;

     void Start()
    {
        player = GameObject.Find("Player");
    }

    public void goStartPoint()
    {
        player.transform.position = new Vector3(26.0f, 0.2f, 13.5f);
    }
}
