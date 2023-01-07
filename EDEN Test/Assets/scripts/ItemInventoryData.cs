using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

/*

This class stores data about the items that are in the bottom main inventory.

height stores the number of rows in the inventory.
width stores the number of items in each row.

The 2D array contains one int for each space in the inventory. The int will store the
item code for each item.

The item codes for items are as follows:
0 -> Empty
1 -> Blue Potion: melee speed potion
2 -> Pink Potion: health potion
3 -> Red Potion: tank potion
4 -> green potion: sniper potion
5 -> orange potion: invisibility not implemented yet

*/

public class ItemInventoryData : MonoBehaviour
{
    public int height;
    public int width;

    public float x0;        //X Co-ordinate of the items in column 0
    public float x_slope;  //Change in X co-ordinate for every column moved

    public float y0;       //Y Co-ordinate of the items in row 0
    public float y_slope; //Change in Y co-ordinate for every row moved

    public GameObject dummy;  //Stores the dummy object, which will be replicated
    public GameObject parent; //Stores the parent of all objects
    ArrayList items = new ArrayList();          //Stores all the items, including the dummy object

    int[,] inventory;

    //Stores images of different items
    public Sprite[] images;
    public Sprite[] inactive_images;

    public GameObject player; // for the effect of potions
    public CinemachineVirtualCamera base_cam;
    public CinemachineVirtualCamera zoom_cam;

    public GameObject arrowInventory;
    public GameObject blockInventory;

    // Start is called before the first frame update
    void Start() //Initialises the array
    {
        //inventory = new int[height, width];

        dummy.transform.localPosition = new Vector3(-586.0f, 134.0f, 0.0f);
        dummy.SetActive(false);
        items.Add(dummy);
    }

    // Update is called once per frame
    void Update()
    {
      //Algorithm to get the next free slot available
      /*int[] free = getNextFreeSlot();
      if(free[0] == -1) {
        free[0] = 0;
        free[1] = 0;
      }*/

      //For testing purposes, adds a potion to the next free slot in inventory
      /*if(Input.GetKeyDown("space")) {
        //setItem(free[0], free[1], 2);
        addItem(2);
      }*/

      //Checks if the items are clicked on (interaction with the button). If it is, then the item is removed
      for(int i = 1; i < items.Count; i++) {
        PublicButton b = (items[i] as GameObject).GetComponent(typeof(PublicButton)) as PublicButton;
        //Debug.Log(b);
        if(b.PressedState()) {
          //Rounds first instead of directly typecasting incase some float imprecision results in value being 1 less than intended value
          int h = (int)Mathf.Round(((items[i] as GameObject).transform.localPosition.y - y0)/y_slope);
          int w = (int)Mathf.Round(((items[i] as GameObject).transform.localPosition.x - x0)/x_slope);

          int item_ID = getItem(h, w); //stores the item id of the item incase it needs to be used.

          //Checks if item was actually removed before reseting counter variable, so that it is not caught in an infinte loop.
          if(removeItem(h, w)) {
            i--;

            //Calls function to use the correct item based on the item ID
            useItem(item_ID); //Practically shouldn't matter, but can be removed from if
          }

        }
      }
    }

    //Sets the next free slot in the inventory to item_id
    public bool addItem(int item_id) {
      //handles for the addition of arrows or blocks. Can add more item ids to exclude later
      if(item_id != 6 && item_id != 7) {
        int[] free = getNextFreeSlot();

        if(free[0] == -1) {
          free[0] = 0;
          free[1] = 0;
        }

        bool success = setItem(free[0], free[1], item_id);
        return(success);
      } else {
        //Arrow
        if(item_id == 6) {
          arrowInventory.GetComponent<ArrowInventory>().incrementArrows(1);
          return(true);
        //Block
        } else {
          blockInventory.GetComponent<BlockUI>().incrementBlocks(1);
          return(true);
        }
      }
    }

    //sets the item at index h, w to the item with Item ID item_id
    //Only does this if the container is empty
    //If successful, returns true, else returns false
    public bool setItem(int h, int w, int item_id) {
      int current = inventory[h, w];

      if(current != 0) {
        return(false);
      }
      inventory[h, w] = item_id;

      //Duplicate dummy object and change position
      GameObject obj = Instantiate(dummy, parent.transform);
      obj.transform.localPosition = new Vector3(x_slope*w + x0, y_slope*h + y0, 0.0f);

      //change sprite
      Image img = obj.GetComponent(typeof(Image)) as Image;
      img.sprite = images[item_id];

      obj.SetActive(true);
      items.Add(obj);

      return(true);
    }

    //Removes Item from the Inventory [INCOMPLETE]
    //If successful, returns true, else returns false
    public bool removeItem(int h, int w) {

      //Ensures that the spot in inventory is not empty
      if(!isEmpty(h, w)) {

        GameObject obj = new GameObject();
        Vector3 target_pos = new Vector3(x_slope*w + x0, y_slope*h + y0, 0.0f); //Calculates the position vector of the target item

        //Gets the element at the given index and removes it from the ArrayList
        for(int i = 0; i < items.Count; i++) {;
          Vector3 pos = ((GameObject)items[i]).transform.localPosition;

          //If the item at the index i of the ArrayList is the same as the required item (Since positions are unique)
          if(pos == target_pos) {
            //Destroys the item to be removed and removes item from ArrayList
            Destroy(obj);
            obj = (GameObject)items[i];
            items.RemoveAt(i);
            Destroy(obj);

            break;
          }
        }

        //Updates the Item ID of the inventory
        inventory[h, w] = 0;
        return(true);

      } else {
        return(false);
      }
    }

    //returns Item ID of the item at inventory index h, w
    public int getItem(int h, int w) {
      return(inventory[h, w]);
    }

    //returns true if there is no item at the given inventory index, else returns false
    public bool isEmpty(int h, int w) {
      return(getItem(h, w) == 0);
    }

    //Method that checks the item ID of the item that was removed from inventory and calls the correct method to perform the correct function [INCOMPLETE]
    public void useItem(int item_ID) {
        //This method should ideally only have an if-else structure that that calls a required function. That function should do everything item specific.
        if (item_ID == 1) // red
        {
            melee_speed_potion potion = new melee_speed_potion(player, true, 50f, 50f, 50f, 20);
            potion.Trigger();
        }
        else if (item_ID == 2) // purple
        {

            Health_potion potion = new Health_potion(player, true, 50);
            //melee_speed_potion potion = new melee_speed_potion(player, true, 50f, 50f, 50f, 20);
            //tank_potion potion = new tank_potion(player, true, 50f, 50f, 50f, 50f, 20);
            //sniper_potion potion = new sniper_potion(player, true, 50f, 50f, 50f, 10, base_cam, zoom_cam, 1);
            potion.Trigger();
        }
        else if (item_ID == 3)
        {
            tank_potion potion = new tank_potion(player, true, 50f, 50f, 50f, 50f, 20);
            potion.Trigger();
        }
        else if (item_ID == 4)
        {
            sniper_potion potion = new sniper_potion(player, true, 50f, 50f, 50f, 10, true, 50f);
            potion.Trigger();
        }


        else if(item_ID == 5)
        {
            invisibiltyPotion potion = new invisibiltyPotion(10);
            potion.Trigger();
        }
    }

    //This method returns an array with indices of the next free slot in the inventory
    public int[] getNextFreeSlot() {
      int[] indices = new int[2];
      indices[0] = -1;
      indices[1] = -1;
      for(int i = 0; i < height; i++) {
        for(int j = 0; j < width; j++) {
          if(inventory[i, j] == 0) {
            indices[0] = i;
            indices[1] = j;
            return(indices);
          }
        }
      }

      return(indices);

    }

      //This method recreates all GameObjects by duplicating dummy
     //Only intended to be called when a new scene is loaded
    //inventory still has to be intact
    public void reloadGameObjects() {
      items.Add(dummy);

      for(int i = 0; i < height; i++) {
        for(int j = 0; j < width; j++) {
          if(inventory[i,j] != 0) {
            //Duplicate dummy object and change position
            GameObject obj = Instantiate(dummy, parent.transform);
            obj.transform.localPosition = new Vector3(x_slope*j + x0, y_slope*i + y0, 0.0f);

            //change sprite
            Image img = obj.GetComponent(typeof(Image)) as Image;
            img.sprite = images[inventory[i,j]];

            obj.SetActive(true);
            items.Add(obj);
          }
        }
      }
    }

    //Getter method for inventory
    public int[,] getInventory() {
      return(inventory);
    }

    //Setter method for inventory
    public void setInventory(int[,] i) {
      inventory = i;
    }

    //Method to return inventory array as a string, only for Debugging purposes
    public string toString() {
      string inv_str = "[[";
      for(int i = 0; i < height; i++) {
        for(int j = 0; j < width; j++) {
          inv_str = inv_str + inventory[i, j].ToString();
          if(j != width - 1) {
            inv_str = inv_str + ", ";
          } else {
            inv_str = inv_str + "] ";
          }
        }
        if(i != height - 1) {
          inv_str = inv_str + ",\n";
        } else {
          inv_str = inv_str + "]";
        }
      }

      return(inv_str);
    }

    //Returns the number of items in the inventory that have a particular Item ID (if its an arrow or a block, it gets it from weapon inventory as well)
    public int getNumberItems(int id) {
      int count = 0;

      for(int i = 0; i < height; i++) {
        for(int j = 0; j < width; j++) {
          if(inventory[i,j] == id) {
            count++;
          }
        }
      }

      //Arrows
      if(id == 6) {
        count += arrowInventory.GetComponent<ArrowInventory>().getArrows();

      //Blocks
      } else if(id == 7) {
        count += blockInventory.GetComponent<BlockUI>().getBlocks();
      }

      return(count);
    }

    //Overloaded Method
    //Returns the total number of items in the inventory (ignores all arrows and blocks in weapon inventory)
    public int getNumberItems() {
      int count = 0;

      for(int i = 0; i < height; i++) {
        for(int j = 0; j < width; j++) {
          if(inventory[i,j] != 0) {
            count++;
          }
        }
      }

      return(count);
    }

    //Returns true if the inventory is not full, else returns false
    public bool isSpace() {
      int storage = (height * width) - getNumberItems();
      return(storage > 0);
    }

    // method removes the first item of the given item type in the inventory overload
    public bool removeItem(int id)
	{
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				if (inventory[i, j] == id)
				{
                    if(removeItem(i, j))
					return true;
				}
			}
		}
		return false;
	}
}
