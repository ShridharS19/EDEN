using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

This script keeps track of the number of arrows in the inventory and updates the UI accordingly.
It is concerned with the number of arrows, and whether there are any arrows or not.

*/

public class ArrowInventory : MonoBehaviour
{
    int arrow_num = 0;    //Stores the number of arrows that the player currently has
    bool active = false; //Stores whether or not the player has arrows

    public GameObject arrow_image;           //Stores the GameObject with the arrow image on UI
    public GameObject arrow_number_display; //Stores the GameObject with the text displaying the number of arrows on UI

    public Sprite active_arrow;    //Stores the sprite for avtive arrow
    public Sprite inactive_arrow; //Stores the sprite for an inactive arrow

    // Start is called before the first frame update
    void Start()
    {
      checkActive();
      manageSprites();
    }

    // Update is called once per frame
    void Update()
    {
      checkActive();
      manageSprites();

      /*if(Input.GetKeyDown("space")) {
        incrementArrows(1);
      }*/
    }
    //checks if there are any arrows and updates active accordingly
    public void checkActive() {
      active = (arrow_num != 0);
    }

    //Setter method for arrow_num
    public void setArrows(int a) {
      arrow_num = a;
    }

    //Increments the number of arrows by i
    public void incrementArrows(int i) {
      arrow_num += i;

      //Ensures that arrow number is never below 0
      if(arrow_num < 0) {
        arrow_num = 0;
      }
    }

    //Getter method for arrow_num
    public int getArrows() {
      return(arrow_num);
    }

    //Getter method for active
    public bool playerHasArrows() {
      return(active);
    }

    //Updates all sprites and text to do with arrows
    public void manageSprites() {
      //Manages sprite of arrows
      if(active) {
        arrow_image.GetComponent<Image>().sprite = active_arrow;
      } else {
        arrow_image.GetComponent<Image>().sprite = inactive_arrow;
      }

      //Manages text (Arrow number)
      arrow_number_display.GetComponent<Text>().text = arrow_num.ToString();
    }
}
