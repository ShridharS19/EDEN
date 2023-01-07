using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

This class is intended to manage the potion launcher settings. It manages everything to do with potion selection on the UI.

The item codes for potions are as follows:
1 -> Blue Potion
2 -> Pink Potion
3 -> Red Potion

*/

public class PotionLauncherSettings : MonoBehaviour
{
    public GameObject potion_image; //Contains gameobject with Image of Potion

    public int current_potion; //Stores the item ID of the current potion

    Sprite[] potion_images_active;    //Stores the Sprites for the potions
    Sprite[] potion_images_inactive; //Stores the Sprites for the potions

    public GameObject right_button; //Stores the GameObject with the right button
    public GameObject left_button; //Stores the GameObject with the left button
    bool rightPressed = false;    //Stores whether right key was pressed last frame (avoid double pressing)
    bool leftPressed  = false;   //Stores whether left key was pressed last frame (avoid double pressing)

    public GameObject counter; //Stores the gameObject with the counter

    public GameObject items;          //Stores the GameObject with the items
    int current_potion_num = 0;      //Stores the number of the current potions in inventory
    bool current_active = false;    //Stores whether there are any potions of the current type in inventory
    public GameObject display_num; //Stores the gameobject that displays the number of the current potion

    public int[] potion_order; //Stores the order of potions in the launcher settings

    public Sprite EmptyBottle;              //Stores sprite for an empty bottle for custom potion
    public GameObject CustomPotionPrefab;  //Stores the prefab of the custom potion
    GameObject customPotion;              //Stores the instantiated Gameobject so that it can be destroyed later

    public GameObject CustomPotionManager; //Stores the GameObject that Manages Custom Potions in inventory so that it can be removed

    //bool x = true;

    // Start is called before the first frame update
    void Start()
    {
      potion_images_active = items.GetComponent<ItemInventoryData>().images;            //gets active images from item inventory
      potion_images_inactive = items.GetComponent<ItemInventoryData>().inactive_images;//gets inactive images from item inventory

      manageButtons();       //Manages everything to do with switching current_potion
      managePotionNumbers(); //Updates number of potions and whether it is active
      updateSprite();        //Updates sprites based on current_potion and whether it is active

      if((isNotCustomPotion() || (!isNotCustomPotion() && !isCurrentActive())) && customPotion != null) {
        GameObject.Destroy(customPotion);
      }
    }

    // Update is called once per frame
    void Update()
    {
      manageButtons();       //Manages everything to do with switching current_potion
      managePotionNumbers(); //Updates number of potions and whether it is active
      updateSprite();        //Updates sprites based on current_potion and whether it is active

     // Debug.Log(current_potion.ToString());

      if((isNotCustomPotion() || (!isNotCustomPotion() && !isCurrentActive())) && customPotion != null) {
        GameObject.Destroy(customPotion);
      }
    }

    //Manages the entire system of buttons, abstracts for entire functionality of changing buttons
    public void manageButtons() {
      //Gets the button that is pressed
      int pressed = getPressed();
      current_potion += pressed;

      //Deals with edge cases
      if(current_potion < 1) {
        current_potion = potion_order.Length+3;
      }
      if(current_potion > potion_order.Length+3) {
        current_potion = 1;
      }
    }

    //Returns negative one if left button is pressed, returns 1 if right button is pressed, returns 0 if neither are pressed
    public int getPressed() {

      //Deals with right button
      if(right_button.GetComponent<PublicButton>().PressedState() && !rightPressed) {
        rightPressed = true;
        return(1);
      }
      if(!right_button.GetComponent<PublicButton>().PressedState()) {
        rightPressed = false;
      }

      //Deals with left Button
      if(left_button.GetComponent<PublicButton>().PressedState() && !leftPressed) {
        leftPressed = true;
        return(-1);
      }
      if(!left_button.GetComponent<PublicButton>().PressedState()) {
        leftPressed = false;
      }

      //No new button is pressed
      return(0);
    }

    //Returns the current potion
    //If custom potion, returns -1
    public int getCurrentPotion() {
      if(isNotCustomPotion()) {
        return(potion_order[current_potion]);
      } else {
        return(-1);
      }
    }

    public bool LaunchCurrentPotion()
    {   if(isNotCustomPotion()) {
          Debug.Log("launched");
          if (items.GetComponent<ItemInventoryData>().removeItem(potion_order[current_potion]))
              return true;
            return false;
        } else {
          if(CustomPotionManager.GetComponent<CustomPotionManager>().removePotion(current_potion-potion_order.Length)) {
            return(true);
          }
          return(false);
        }

    }

    //Updates the Image of this with the correct potion sprite
    public void updateSprite() {
      //Sets sprite of potion itself
      if(isNotCustomPotion()) {
        if(isCurrentActive()) {
          potion_image.GetComponent<Image>().sprite = potion_images_active[potion_order[current_potion]];
        } else {
          potion_image.GetComponent<Image>().sprite = potion_images_inactive[potion_order[current_potion]];
        }
      } else {
        potion_image.GetComponent<Image>().sprite = EmptyBottle;
        if(current_active) {
          if(customPotion != null) {
            GameObject.Destroy(customPotion);
          }

          customPotion = GameObject.Instantiate(CustomPotionPrefab);
          customPotion.transform.parent = gameObject.transform;
          customPotion.transform.localPosition = new Vector3(0, 0, 0);
          customPotion.transform.localScale    = new Vector3(50, 50, 1);

          customPotion.GetComponent<potionColourSetter>().SetArrayOfStats(DataMaster.custom_potions[current_potion-potion_order.Length].getStats());

          for(int j = 0; j < 5; j++) {
            customPotion.transform.GetChild(j).gameObject.AddComponent<Image>();
            customPotion.transform.GetChild(j).gameObject.GetComponent<Image>().sprite = customPotion.transform.GetChild(j).gameObject.GetComponent<SpriteRenderer>().sprite;
            customPotion.transform.GetChild(j).gameObject.GetComponent<Image>().color = customPotion.transform.GetChild(j).gameObject.GetComponent<SpriteRenderer>().color;
          }
          //x = false;
      }

      }

      //Sets number of potions on UI display
      display_num.GetComponent<Text>().text = current_potion_num.ToString();
    }

    //Manages the number of each potion in inventory
    public void managePotionNumbers() {
      if(isNotCustomPotion()) {
        current_potion_num = items.GetComponent<ItemInventoryData>().getNumberItems(potion_order[current_potion]);

        if(current_potion_num <= 0) {
          current_active = false;
        } else {
          current_active = true;
        }
      } else {
        //Debug.Log(current_potion-potion_order.Length);
        current_active = (DataMaster.custom_potions[current_potion-potion_order.Length] != null);
        if(current_active) {
          current_potion_num = 1;
        } else {
          current_potion_num = 0;
        }
      }
    }

    //Getter for current_active
    public bool isCurrentActive() {
      return(current_active);
    }

    //Sets current Potion Number to p
    public void setCurrentPotionNum(int p) {
      current_potion_num = p;
    }

    //returns whether the current potion is a custom potion or a regular potion
    public bool isNotCustomPotion() {
      //Debug.Log(current_potion.ToString() + "     " + (potion_order.Length-1).ToString());
      if(current_potion < potion_order.Length) {
        return(true);
      } else {
        return(false);
      }
    }

    //returns PotionStorage of custom potion if the current potion is a custom potion. Else returns null
    public PotionStorage getPotionStorage() {
      if(isNotCustomPotion()) {
        return(null);
      } else {
        return(DataMaster.custom_potions[current_potion-potion_order.Length]);
      }
    }

    //returns gameobject of prefab with the custom potion display if the current potion is a custom potion. Else returns null
    public GameObject getCustomPotionDisplay() {
      if(isNotCustomPotion()) {
        return(null);
      } else {
        GameObject cp = GameObject.Instantiate(CustomPotionPrefab);
        cp.GetComponent<potionColourSetter>().SetArrayOfStats(DataMaster.custom_potions[current_potion-potion_order.Length].getStats());
        return(cp);
      }
    }
}
