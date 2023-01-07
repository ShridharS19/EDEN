using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryCursor : MonoBehaviour
{
    public GameObject cursor; //Stores itself
    public GameObject items; //Stores the entire items object, to get the constants for height, width, slopes, etc.

    int height;
    int width;

    float x0;     //X Co-ordinate of the items in column 0
    float x_slope;  //Change in X co-ordinate for every column moved

    float y0;       //Y Co-ordinate of the items in row 0
    float y_slope; //Change in Y co-ordinate for every row moved

    bool inventoryVisible; //Stores whether the inventory is open
    bool mouseOver; //Stores whether the mouse is over the item inventory

    //Both of the above need to be true for the cursor to be set active. Else, it won't be.

    // Start is called before the first frame update
    void Start()
    {
      //Copying data, so it only needs to be entered in one place
      ItemInventoryData data = items.GetComponent(typeof(ItemInventoryData)) as ItemInventoryData;
      height  = data.height;
      width   = data.width;
      x0      = data.x0/1.6f - Screen.width/3.2f;//Need to convert from local co-ordinates to global co-ordinates
      x_slope = data.x_slope;//Need to convert from local co-ordinates to global co-ordinates
      y0      = data.y0/1.6f - Screen.height/3.2f;//Need to convert from local co-ordinates to global co-ordinates
      y_slope = data.y_slope;//Need to convert from local co-ordinates to global co-ordinates

      inventoryVisible = false; //Starts as invisible, because the inventory is closed.
      mouseOver = isMouseOver();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.E)) {
        //Switches inventory display
        inventoryVisible = !inventoryVisible;
      }
      mouseOver = isMouseOver();

      if(inventoryVisible && mouseOver) {
        (cursor.GetComponent(typeof(Image)) as Behaviour).enabled = true;
      } else {
        (cursor.GetComponent(typeof(Image)) as Behaviour).enabled = false;
      }
    }

    //Checks whether mouse is over the item inventory (regardless of whether the inventory is open or closed) [INCOMPLETE]
    bool isMouseOver() {
      float X = Input.mousePosition.x;
      float Y = Input.mousePosition.y;

      //Debug.Log(X >= x0);

      return(X >= x0 && X <= (x0 + x_slope*(width+1)) && Y <= y0 && Y >= (y0 + y_slope*(height+1)));
    }
}
