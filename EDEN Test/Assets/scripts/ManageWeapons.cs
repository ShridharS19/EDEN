using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

This class manages the UI Region of the weapons and changes the active weapon.
Q can be used as a hot key to change weapons. Alternatively, the player can click on the weapon icon and it will be activated.

Weapon IDs:
1 -> Sword
2 -> Bow
3 -> Weapon Launcher

Note: Weapon ID starts at 1. This is because it might be necessary in the future to store something like no weapons as weaponcode 0

*/

public class ManageWeapons : MonoBehaviour
{
    int total_weapons = 3;          //Stores the total number of weapons in the game
    
    public int weapon_num;         //Stores the total number of weapons the player has acquired
    public int current_weapon;    //Stores the item ID of the currently active weapon

    public GameObject[] weapons; //Stores the GameObjects of each weapon, can be used to access the public button as well as the Image Renderer (To change the sprite)

    public Sprite[] inactive_sprites;  //Stores Sprites for weapons when they are inactive
    public Sprite[] active_sprites;   //Stores sprites for weapons when they are active

    bool[] wasPressed; //Deals with button being pressed for multiple frames

    // Start is called before the first frame update
    void Start()
    {
      wasPressed = new bool[weapon_num];
      for(int i = 0; i < weapon_num; i++) {
        wasPressed[i] = false;
      }

      enableRequired();
      setActiveWeapon();
      setSprites();
    }

    // Update is called once per frame
    void Update()
    {
      enableRequired();
      setActiveWeapon();
      setSprites();
    }

     //Sets the sprites of the weapons
    //Ensures that all the Inactive weapon sprites are set to inactive and the active sprite is set to active
    public void setSprites() {
      //first loops through all the weapons and sets them to inactive, later the active weapon is set to active
      for(int i = 0; i < weapon_num; i++) {
        (weapons[i].GetComponent(typeof(Image)) as Image).sprite = inactive_sprites[i];
      }

      //Setting active weapon sprite
      (weapons[current_weapon-1].GetComponent(typeof(Image)) as Image).sprite = active_sprites[current_weapon-1]; //Note: The index is current_weapon-1 since current weapon is the weapon id, which starts at 1

    }

    //Sets the current active weapon based on player input
    public void setActiveWeapon() {

      //Checks if hotkey is pressed. If it is, then it changes to next weapon. Else, checks if a button is pressed and makes changes accordingly
      if(hotHotkeyPressed()) {
        current_weapon++;
        if(current_weapon > weapon_num) {
          current_weapon = 1;
        }
      } else {
        //Sets the current_weapon based on if an icon is pressed
        checkIcons();
      }
    }

    //Checks if Hotkey is Pressed. Returns true if it is, else it returns false
    public bool hotHotkeyPressed() {
      return(Input.GetKeyDown(KeyCode.Q));
    }

    //Sets the current_weapon based on if an icon is pressed
    public void checkIcons() {

      //Loops through all the weapons and checks if they have been pressed. If a button has been pressed, it sets the weapon
      for(int i = 0; i < weapon_num; i++) {
        if((weapons[i].GetComponent(typeof(PublicButton)) as PublicButton).PressedState() && !wasPressed[i]) {
          wasPressed[i] = true;
          current_weapon = i + 1;
          if(current_weapon > weapon_num) {
            weapon_num = 1;
          }
        } else {
          wasPressed[i] = false;
        }
      }

    }

    //Enables all weapons that the player has acquired, disables everything else
    public void enableRequired() {

      //Loops through all of the weapons to activate weapons that have index less than the active number, disables the rest
      for(int i = 0; i < total_weapons; i++) {
        if(i < weapon_num) {
          weapons[i].SetActive(true);
        } else {
          weapons[i].SetActive(false);
        }
      }
    }

    //Returns the weapon ID of the currently active weapon (Getter Method)
    public int getActiveWeapon() {
      return(current_weapon);
    }
}
