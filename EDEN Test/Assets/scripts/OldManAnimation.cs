using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManAnimation : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
          animator.SetTrigger("Right");
        } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
          animator.SetTrigger("Left");
        } else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetTrigger("Back");
        } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetTrigger("Forward");
        }  else {
            animator.SetTrigger("Stop");
        }
    }

    // Update is called once per frame
    void Update()
    { //Old order of controller
      /*if (Input.GetKey(KeyCode.W))
        {
            animator.SetTrigger("Back");
        } else if(Input.GetKey(KeyCode.A))
        {
            animator.SetTrigger("Left");
        } else if(Input.GetKey(KeyCode.S))
        {
            animator.SetTrigger("Forward");
        } else if(Input.GetKey(KeyCode.D))
        {
            animator.SetTrigger("Right");
        } else {
            animator.SetTrigger("Stop");
        }*/

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
          animator.SetTrigger("Right");
        } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
          animator.SetTrigger("Left");
        } else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetTrigger("Back");
        } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetTrigger("Forward");
        }  else {
            animator.SetTrigger("Stop");
        }
    }
}
