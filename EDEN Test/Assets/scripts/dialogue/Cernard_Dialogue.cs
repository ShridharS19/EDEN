using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cernard_Dialogue : MonoBehaviour
{
    public GameObject Canvas_dialogue;
    public Dialogue[] dialogue;[SerializeField] // the field appears in the inspector

    private void Start_Dialogue() // triggers the start of a conversation
    {
        FindObjectOfType<dialogue_manager>().StartConversation(dialogue); // it triggers the start of the conversation in the dialogue manager
    }
    public void StartDialogue()
    {
        Start_Dialogue();
        Canvas_dialogue.SetActive(true);
    }

    public void EndDialogue()
    {
        Canvas_dialogue.SetActive(false);
    }
    // this needs to be called before you start the conversation obviosly or else if will take defualt values for the strings
    public void inputdialogue(Dialogue[] array)
    {
        this.dialogue = array;
    }
}
