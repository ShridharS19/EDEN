using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleDialogue : MonoBehaviour
{
    public GameObject Canvas_dialogue;
    public Dialogue[] dialogue;[SerializeField] // the field appears in the inspector

    private void Start_Dialogue() // triggers the start of a conversation
    {
        FindObjectOfType<dialogue_manager>().StartConversation(dialogue); // it triggers the start of the conversation in the dialogue manager
    }

    public void StartDialogue(int noOfRocks)
    {
        Start_Dialogue();
        dialogue[0].conversation = dialogue[0].conversation + noOfRocks;
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