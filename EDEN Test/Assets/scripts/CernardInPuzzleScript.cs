using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CernardInPuzzleScript : MonoBehaviour
{
    bool isIn = false;
    private int treesUsed = 0;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(isIn)
            {
                if(!GameObject.Find("player").GetComponent<SandBlockDevice>().getDevice())
                {
                    GetComponent<Cernard_Dialogue>().StartDialogue();
                    GameObject.Find("player").GetComponent<SandBlockDevice>().setDevice(true);
                    treesUsed++;
                }
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        isIn = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isIn = false;
    }
}
