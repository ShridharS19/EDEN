using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNPCScript : MonoBehaviour
{
    private bool isIn = false;
    private int treesUsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            if (isIn)
            {
                if (!GameObject.Find("player").GetComponent<PickaxeBlockBreaking>().getPickaxe())
                {
                    treesUsed++;
                    GameObject.Find("player").GetComponent<PickaxeBlockBreaking>().setPickaxe(true);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D colliision)
    {
        isIn = true;
    }
    
    void OnTriggerExit2D(Collider2D colliision)
    {
        isIn = false;
    }

    public int getTreesUsed()
    {
        return treesUsed;
    }
}
