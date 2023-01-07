using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System;
public class MainTrigger_bossRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public event EventHandler<GameObject> OnStartFindingDernard;
    private bool triggerOnce = true;
    private void OnTriggerEnter2D(Collider2D collision) // if he enters then trigger the puzzle and start the dialogue
    {
        if(collision.gameObject.CompareTag("Player")) // if it has collided with the player
        {
            if (triggerOnce)
            {
                OnStartFindingDernard?.Invoke(this, gameObject);
                triggerOnce = false;
            }
            Debug.Log("entered the trigger");

            GetComponent<dialogue_trigger>().StartDialogue();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false); // stop the light
            GetComponent<dialogue_trigger>().EndDialogue();
        }
    }
}
