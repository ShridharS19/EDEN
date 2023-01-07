using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

This class deals with placing and attracting blocks by pressing the buttons on the UI
It also deals with the number of blocks the player has.

*/

public class BlockUI : MonoBehaviour
{

    public GameObject place_block;
    public GameObject blocks_clone;
    bool place_pressed = false;
    public GameObject pull_blocks;
    bool pull_pressed = false;
    public GameObject player_Gobj;
    private GameObject instance_block;
    int blocks = 10;
    public GameObject BlocksDisplay;
    private int placed;
    public GameObject PlacedBlocksDisplay;

    // Start is called before the first frame update
    void Start()
    {
      placed = 0;
      managePress();
      updateDisplays();
    }

    // Update is called once per frame
    void Update()
    {
      managePress();
      updateDisplays();
    }

    //Defines what needs to be done when place button is pressed
    public void place(GameObject builder) {
        instance_block = Instantiate(blocks_clone, player_Gobj.transform.position + (2 * player_Gobj.GetComponent<value_control>().GetPlayerDir()), Quaternion.identity);// creates a block gameobject
        instance_block.GetComponent<blockAttributes>().SetTarget(player_Gobj.transform); // sets the target(where it is attracted to when trigger is pressed) of the block
        instance_block.GetComponent<blockAttributes>().setBuilder(builder);
        instance_block.gameObject.tag = "block";
        Health_manager hm = instance_block.AddComponent(typeof(Health_manager)) as Health_manager;
        BoxCollider2D sc = instance_block.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        sc.isTrigger = true;
        projectile_collision r = instance_block.AddComponent(typeof(projectile_collision)) as projectile_collision;
        r.isBlock = true;
        hm.Healthbar_prefab = GameObject.Find("healthbarsprite");
        hm.HealthBar_UI = GameObject.Find("Health");
        
        blocks--;
        placed++;
        
    }

    //Defines what needs to be done when pull button is pressed
    public void pull() {
        Debug.Log("PULLLLL");
        foreach(GameObject i in GameObject.FindGameObjectsWithTag("block"))
        {
            i.GetComponent<blockAttributes>().SetIsTriggered(true);
        }
      //placed = 0;
    }

    //Calls required methods if a button is pressed
    public void managePress() {

      if(place_block.GetComponent<PublicButton>().PressedState() && !place_pressed) {
        place_pressed = true;
        //Makes sure that the player has blocks before attempting to place them
        if(blocks > 0) {
          place(player_Gobj);
        }
      } else if(!place_block.GetComponent<PublicButton>().PressedState()) {
        place_pressed = false;
      }

      if(pull_blocks.GetComponent<PublicButton>().PressedState() && !pull_pressed) {
        pull_pressed = true;
        pull();
      } else if(!pull_blocks.GetComponent<PublicButton>().PressedState()) {
        pull_pressed = false;
      }
    }

    //Updates the UI to display the correct number of blocks
    public void updateDisplays() {
      BlocksDisplay.GetComponent<Text>().text       = blocks.ToString();
      PlacedBlocksDisplay.GetComponent<Text>().text = placed.ToString();
    }

    //Returns the number of blocks in the inventory
    public int getBlocks() {
      return(blocks);
    }

    //Increments the number of blocks by i
    public void incrementBlocks(int i) {
      blocks += i;
    }

    //Returns the number of blocks that the player has actively placed in the world
    public int getPlaced() {
      return(placed);
    }

    //Increments the number of blocks placed by i
    public void incrementPlaced(int i) {
      blocks += i;
    }

    public void decreasePlaced()
    {
        placed--;
        
    }

    
    // the below is only for testing by arnav
    public void setTotalBlocks(int n) // set the total number of blocks
    {
        blocks = n;
    }
}
