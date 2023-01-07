using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildblocks : MonoBehaviour

{// not implemented yet
    public GameObject blocks;
    private GameObject instance_block;
    private int initial_blocks = 3;
    public GameObject blocks_UI;
    private BlockUI instance;
    
    private ArrayList multipliers = new ArrayList();
    // for now to build a block we need to press X
    // Start is called before the first frame update
    void Start()
    {

        instance = blocks_UI.GetComponent<BlockUI>(); // to get the script from the UI
        instance.setTotalBlocks(initial_blocks);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && instance.getBlocks() > 0) // if the button X is pressed
        {


            //Debug.Log("entered");
            // build a block in the direction the player is facing
           // instance_block = Instantiate(blocks, transform.position + (2 * GetComponent<value_control>().GetPlayerDir()), Quaternion.identity);// creates a block gameobject
            //instance_block.GetComponent<blockAttributes>().SetTarget(transform); // sets the target(where it is attracted to when trigger is pressed) of the block
           
            //i have put the above two lines of code into place()

            instance.place(gameObject);

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            instance.pull();
        }
    }

    public void add_blocks_ammo(int n)
    {
        instance.incrementBlocks(n);
    }


    

    public int get_blocks_ammo()
    {
        return instance.getBlocks();

    }

    public BlockUI getUIBlockInstance()
    {
        return instance;
    }

    public ArrayList Get_multiplier()
    {
        return multipliers;
    }

    public void setMultiplier(float n) // for potions it takes out all the multipliers that have a value 1 since they make no difference this allows more than one potion uses at the same time
    {
        if (multipliers != null)
        {
            while (multipliers.IndexOf(1) != -1) // while 1 is still in the multipliers
            {
                multipliers.Remove(1f);
            }
        }
        multipliers.Add(n);
    }

    public void setMultiplier(float n, int index)
    {
        multipliers[index] = n;
    }


}
