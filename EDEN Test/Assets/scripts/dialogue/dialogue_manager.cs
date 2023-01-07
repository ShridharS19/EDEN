using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class dialogue_manager : MonoBehaviour
{
    public Button cont_button;
    public GameObject Canvas_dialogue; // this is the entire dialogue box
    public Queue<string> conversation; // stores the text to be said
    public Queue<string> names;// stores the name of the character who says the stuff the indexes of the names and text correspond
    public TextMeshProUGUI dialogue;[SerializeField]
    public event EventHandler<GameObject> OndialogueEnd;
    void Start()
    {
        conversation = new Queue<string>();
        names = new Queue<string>();
    }

    public void StartConversation(Dialogue[] dialogue) // places all the names and  the dialogue in the queue and then displays the first sentence
    {
        GameObject.Find("player").GetComponent<movementALL>().enabled = false;
       // GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled = false;
        Debug.Log("starting conversation with " + dialogue[0]);
        cont_button.gameObject.SetActive(true);
        names.Clear();
        conversation.Clear();
        for (int i = 0; i < dialogue.Length; i++)
        {
            conversation.Enqueue(dialogue[i].conversation); // adding elements to the queue
            names.Enqueue(dialogue[i].name);
        }
        
        NextLine();
    }

    public void NextLine() // it displays the next line
    {

        if (cont_button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text == "end >")
        {
            cont_button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "continue >";
            cont_button.gameObject.SetActive(false); // make the continue button disappear
            EndDialogue();
        }


        else
        {

            dialogue.text = names.Dequeue() + ": " + conversation.Dequeue(); // emptying the queue

            if (conversation.Count == 0) // if there are no more senteces to display
            {
                cont_button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "end >";
            }

        }
             
        
    }
    public void EndDialogue()
    {
        GameObject.Find("player").GetComponent<movementALL>().enabled = true;
        Canvas_dialogue.SetActive(false);
        // GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled = true;
        Debug.Log("the conversation has ended");
        OndialogueEnd?.Invoke(this, gameObject);
    }

   
    

    
    
}
