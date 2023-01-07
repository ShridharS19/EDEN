using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapDoorControl : MonoBehaviour
{
    Animator animationDoor;
    void Start()
    {
        animationDoor = this.GetComponent<Animator>(); // gets the animator component from this gamobject 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // if it is the player
            animationDoor.SetBool("entered", true); // open door animation
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) // if it is the player
        animationDoor.SetBool("entered", false); // close door animation
    }
}
