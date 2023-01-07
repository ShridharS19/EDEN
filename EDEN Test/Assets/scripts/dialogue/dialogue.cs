using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] // it makes the class visiblein the inspector

public class Dialogue
{
    [TextArea(3, 10)] // makes the text box bigger
    public string conversation;// the first dimension stores the text
    // the second dimension stores the name
    public string name;

    public Dialogue(string conversation, string name)
    {
        this.name = name;
        this.conversation = conversation;
    }
        

}
