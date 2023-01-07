using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

This class manages the potion crafting interface

*/

public class PotionCraftingInterface : MonoBehaviour
{
    public GameObject crafting_interface; //Stores the crafting interface to open and close
    public GameObject potionDisplay; //Stores the potion display to update final potion when crafting interface is reopened
    public bool isOpen; //Stores whether the crafting interface is currently open

    public GameObject exitButton; //Stores the exit button

    public GameObject[] ingredients;
    public GameObject catalyst;

    // Start is called before the first frame update
    void Start()
    {
      if(isOpen) {
        run();
      }
    }

    // Update is called once per frame
    void Update()
    {
      if(isOpen) {
        run();
      }
    }

    //Runs code required to manage the shop when it is open
    public void run() {
      if(Input.GetKey(KeyCode.Q) || exitButton.GetComponent<PublicButton>().PressedState()) {
        closeShop();
      }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      //Debug.Log("Collision");
      if(collision.gameObject.CompareTag("Player")) {
        if(!isOpen) {
          openShop();
        }
      }
    }

    //Opens Shop inventory
    public void openShop() {
      crafting_interface.SetActive(true);
      potionDisplay.GetComponent<PotionInitialisation>().justOpened();
      //crafting_interface.transform.GetChild(1).GetComponent<ShopItemManager>().resetNumberInInventoryDisplay();
      isOpen = true;

      for(int i = 0; i < ingredients.Length; i++) {
        ingredients[i].GetComponent<IngredientSelection>().manageStart();
      }
      catalyst.GetComponent<CatalystSelector>().manageStart();

      manageOverworld();
    }

    //Write code to manage enemies, player position, etc.
    public void manageOverworld() {

      //Manages overworld when the shop is opened
      if(isOpen) {


      //Manages overworld when the shop is closed
      } else {

      }
    }

    //Closes Shop inventory
    public void closeShop() {
      crafting_interface.SetActive(false);
      isOpen = false;

      for(int i = 0; i < ingredients.Length; i++) {
        ingredients[i].GetComponent<IngredientSelection>().manageEnd();
      }
      catalyst.GetComponent<CatalystSelector>().manageEnd();
    }
}
