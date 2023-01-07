using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*

This script ensures that only the required number of orbs can be seen.

Each gameobject symbolises one orb. The number indicates the number of orbs that are
visible.

This script is being updated to also manage which orbs to show where.

Orb IDs:
0 -> Grass
1 -> Lightning
2 -> Fire
3 -> Wind
4 -> Earth

*/

public class HideOrbs : MonoBehaviour
{
    public int orb_number;   //stores the total number of orbs

    public int[] order;     //stores the order of the orbs, can be used to set initial conditions, but is always updates nevertheless
                           //Every index number there is the Orb ID of an orb. After all the orbs are accounted for, the rest of the spaces are filled with -1
    public bool[] active; //Stores whether each orb is active or not (true for active, false otherwise)

    public bool[] wasPressed; //stores whether the orb was pressed in the previous frame, indicating that only one click took place. (Effectively helps button act as a listener)

    public Sprite[] sprites;           //Stores the sprites for all the orbs so that it can correctly assign them
    public Sprite[] active_sprites;   //Stores the sprites for all the orbs when they are activated
    public Sprite default_sprite;    //Stores the default sprite for unassigned orbs
    public GameObject[] orbs;       //Stores the Actual gameobjects of the orbs, used to edit the sprites, and enable/disable them as required
    public event EventHandler<GameObject> OnOrbPressed; // event that is flagged when 

    // Start is called before the first frame update
    //Sets orb number, which is controlled by the members of order
    void Start()
    {
      //Algorithm to correctly calculate orb number based on the order.
      orb_number = 0;

      for(int j = 0; j < orbs.Length && j < order.Length; j++) {
        if(order[j] == -1) {
          break;
        } else {
          orb_number = j+1;
        }
      }

      //Sets all the sprites of the orbs to the correct sprite according to the order
      setSprites();

      int i = 0; //Counter variable

      //Enables orbs that are lower than orb number
      for(; i < orb_number && i < orbs.Length; i++) {
        orbs[i].SetActive(true);
      }

      //Disables all other orbs
      for(; i < orbs.Length; i++) {
        orbs[i].SetActive(false);
      }
    }



     // Update is called once per frame
    //Sets orb number, which is controlled by the members of order
    void Update()
    {
      //Algorithm to correctly calculate orb number based on the order.
      orb_number = 0;

      for(int j = 0; j < orbs.Length && j < order.Length; j++) {
        if(order[j] == -1) {
          break;
        } else {
          orb_number = j+1;
        }
      }

      //Checks if any of the orbs are clicked. If they are, it changes the activity, keeping in mind that ony 2 can be active at a time.
      changeOrbActivity();

      //Sets all the sprites of the orbs to the correct sprite according to the order
      setSprites();

      int i = 0; //Counter variable

      //Enables orbs that are lower than orb number
      for(; i < orb_number && i < orbs.Length; i++) {
        orbs[i].SetActive(true);
      }

      //Disables all other orbs
      for(; i < orbs.Length; i++) {
        orbs[i].SetActive(false);
      }

      /*int[] act = getActiveOrbIDs();

      string s = " ";
      for(int j = 0; j < 2; j++) {
        s = s + act[j].ToString() + " ";
      }

      Debug.Log(s);*/
    }

     //Sets the sprites of the Orbs based on the members of order
    //After -1 starts appearing in order, the rest of the orbs are given the default sprite.
    public void setSprites() {
      int i = 0; //Counter variable

      //Sets all obtained orb sprites
      while(i < orbs.Length) {
        //If else and break is used to avoid index out of bounds error that occurs due to code trying to access order[i] when i = orbs.Length
        if(order[i] != -1) {
          //Checks whether the orb is active. If it is, sets sprite to active sprite. Else, sets it to regular sprite
          if(active[i]) {
            (orbs[i].GetComponent(typeof(Image)) as Image).sprite = active_sprites[order[i]];
          } else {
            (orbs[i].GetComponent(typeof(Image)) as Image).sprite = sprites[order[i]];
          }
          i++;
        } else {
          break;
        }
      }

      //Sets all unobtained orb containers to default sprite
      while(i < orbs.Length) {
        (orbs[i].GetComponent(typeof(Image)) as Image).sprite = default_sprite;
        i++;
      }
    }

    //Checks if any of the orbs are clicked. If they are, it changes the activity, keeping in mind that ony 2 can be active at a time.
    public void changeOrbActivity() {

      int active_number = 0; //Stores how many orbs are active

      //Algorithm to find how many orbs are currently active
      for(int i = 0; i < active.Length; i++) {
        if(active[i]) {
          active_number++;
        }
      }

      //Loops through all the orbs
      for(int i = 0; i < orbs.Length; i++) {

        //Checks if the public button is being clicked
        if((orbs[i].GetComponent(typeof(PublicButton)) as PublicButton).PressedState()) {

          //Makes sure that button wasn't clicked in the previous frame
          if(!wasPressed[i]) {

            //Checks if this orb is currently active
            if(active[i]) {
              //Case in which orb is currently active, can simply deactivate as follows:
              active[i] = false;
              active_number--; //Updates active_number
              OnOrbPressed?.Invoke(this, gameObject); // calls an event and handles for no subscribers
            } else {
              //Case in which orb is currently inactive, need to make sure that there are less than two currently active
              if(active_number < 2) {
                //No problem, simply activate the orb.
                
                active[i] = true;
                active_number++; //Updates active_number
                OnOrbPressed?.Invoke(this, gameObject); // calls an event and handles for no subscribers
              } else {
                //Don't activate the orb.
                //The function called below handles this case.
                handleTooManyActive();
              }
            }

          }

          wasPressed[i] = true; //Shows that orb was pressed
        } else {
          wasPressed[i] = false; //Shows that orb was not pressed
        }
      }
        
    }

      //Method that adds an orb to the order.
     //Only intended to be used when a new orb is acquired by the player
    //returns true if orb is successfully added, else returns false (false should never practically occur but it is just a check)
    public bool addOrb(int orb_ID) {
      int i = 0;

      //Numerates through until there is no orb assigned to the ith index of order
      for(int j = 0; j < orbs.Length; j++) {
        if(order[i] != -1) {
          i++;
        }
      }

        //Corrects for index out of bounds error that occurs if i is greater than orbs.Length
       //There is another chack later to ensure that this method doesn't override the last orb, if one is ever present
      //The above case should never practically occur in the game, however, it is still controlled for
      if(i >= orbs.Length) {
        i = orbs.Length - 1;
      }

      //Checks if order is full. If it is not, then new orb is added, true is returned. Else, no change made to order, false is returned
      if(order[i] == -1) {
        order[i] = orb_ID;
        return(true);
      } else {
        return(false);
      }
    }

     //Getter method, returns the order
    //Order can be used to determine number of orbs received
    public int[] getOrder() {
      return(order);
    }

    //Setter method, sets new order, intended to be called only directly after loading a new scene
    public void setOrder(int[] o) {
      order = o;
    }

     //Getter method, returns active
    //Active can be used to determine which orbs are active
   //Note: Active contains the active orbs in terms of order, not in terms of orb ID. Therefore, active will need to be used in conjunction with order to determine effects.
   public bool[] getActiveOrbs() {
     return(active);
   }

   //Getter method, gets all activated orbs, intended to be called only directly after loading a new scene
   public void setOrder(bool[] a) {
     active = a;
   }

    //This method handles when too many orbs are attempted to be activated at once. (Displays message etc) [INCOMPLETE]
    public void handleTooManyActive() {
      Debug.Log("Too many orbs attempted");
    }

    //Returns an integer array with the orb IDs of the active orbs.
    public int[] getActiveOrbIDs() {
      int[] ids = new int[2];
      ids[0] = -1; ids[1] = -1;

      int index = 0;

      //Loops through all orbs
      for(int i = 0; i < order.Length; i++) {

        //If the current orb is active
        if(active[i]) {
          ids[index] = order[i];
          index++;
        }

        //Breaks if there are no more orbs left
        if(order[i] == -1) {
          break;
        }
      }

      return(ids);
    }
}
