using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Shows and hides shop whenever required.

*/

public class ManageShop : MonoBehaviour
{

    public GameObject shop; //Stores the shop interface to open and close
    bool isOpen = false;   //Stores whether the store is currently open

    public GameObject exitButton; //Stores the exit button


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
      shop.SetActive(true);
      shop.transform.GetChild(1).GetComponent<ShopItemManager>().resetNumberInInventoryDisplay();
      isOpen = true;

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
      shop.SetActive(false);
      isOpen = false;
    }
}
