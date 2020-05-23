using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillscript : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {


        startani();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void des()
    {
        Destroy(this.gameObject);
    }

    void startani()
    {
        animator.Play("Skill1");
    }
  
    
}
