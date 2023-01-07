using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localDialogueTrigger : MonoBehaviour
{
    private bool inradius;
   
    void Update()
    {
        if(inradius && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("entered");
            
            GetComponent<dialogue_trigger>().StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("DummyPlayer"))
        {
            inradius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("DummyPlayer"))
        {
            Debug.Log("out");
            inradius = false;
            //GetComponent<dialogue_trigger>().EndDialogue();
        }
    }

}
