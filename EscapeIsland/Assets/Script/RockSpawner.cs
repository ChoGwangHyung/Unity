using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

    public GameObject rockPrefab;
    public float interval;
    
    IEnumerator Start()
    {
        interval = 2.5f;
        while (true)
        {
            Instantiate(rockPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }

    }   
}
