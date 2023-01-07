using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

Manages the shop, dictates what items are available and manages the movement of itemslots

Items are potions etc.
Item slots are the slot that contains the description etc. of each item
Item stack is the set of all item slots in a shop

*/

public class ShopItemManager : MonoBehaviour
{
    public int[] items;      //Array that stores item IDs of the items avasilable in the shop in the order they are available.
    GameObject[] itemStack; //Stores the GameObjects that display the entire stack of items available in the shop.

    public GameObject ItemSlotPrefab; //Stores the prefab for each item slot.
    public GameObject ItemSlotList; //Parent of each slot in the stack

    public GameObject inventory;   //stores item inventory
    ItemInventoryData data;       //Stores the data of items in the inventory, from inventory
    Sprite[] itemImages;  //Stores images of all sprites, gets it from items inventory

    public int currentItem = 0; //Stores the current item that is selected by the player

    static int[] cost_of_items = {0, 50, 100, 200, 500, 1000}; //Stores the cost of each item. The index of the cost is the item it is the cost of. The 0th index represents the null, empty item.

    bool buyPressed = false;

    public GameObject moneyError;  //Stores GameObject that displays money Error
    public GameObject spaceError; //Stores GameObject that displays space Error

    int timer = -1; //int to store frames since last error message

    // Start is called before the first frame update
    void Start()
    {
      data = inventory.GetComponent<ItemInventoryData>();
      itemImages = data.images;

      itemStack = new GameObject[items.Length];
      instantiateSlots();

      manageCurrentItem();
      manageBuy();
    }

    // Update is called once per frame
    void Update()
    {
      manageCurrentItem();
      manageBuy();
      controlTimer();
    }

    //Method that instantiates all of the item slots in their correct positions based on the items in items[]. [COMPLETE FOR POSITIONING AND ITEM IMAGE AND IN INVENTORY, NEED TO CORRECT ITEM INFO]
    public void instantiateSlots() {
      //Sets up first slot, as shop must have atleast 1 item
      itemStack[0] = Instantiate(ItemSlotPrefab);
      itemStack[0].transform.SetParent(ItemSlotList.transform, false);

      itemStack[0].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemImages[items[0]];
      itemStack[0].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = ItemAttributes.getTitle(items[0]);
      itemStack[0].transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = ItemAttributes.getDescription(items[0]);
      itemStack[0].transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "$" + ItemAttributes.getPrice(items[0]).ToString();
      itemStack[0].transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = data.getNumberItems(items[0]).ToString();

      //Loops through all the elements in the items Array
      for(int i = 1; i < items.Length; i++) {
        itemStack[i] = Instantiate(ItemSlotPrefab);
        itemStack[i].transform.SetParent(ItemSlotList.transform, false);

        itemStack[i].transform.localPosition = new Vector2(0, -200*i - 25);
        itemStack[i].transform.localScale = new Vector2(0.7f, 0.7f);

        itemStack[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemImages[items[i]];
        itemStack[i].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = ItemAttributes.getTitle(items[i]);
        itemStack[i].transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = ItemAttributes.getDescription(items[i]);
        itemStack[i].transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "$" + ItemAttributes.getPrice(items[i]).ToString();
        itemStack[i].transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = data.getNumberItems(items[i]).ToString();
      }

    }

    //Changes Current Item based on user input
    public void manageCurrentItem() {
      if(Input.GetKeyDown(KeyCode.UpArrow)) {
        currentItem--;
        if(currentItem < 0) {
          currentItem = 0;
        }

        resetSlotPositions();
      }
      if(Input.GetKeyDown(KeyCode.DownArrow)) {
        currentItem++;
        if(currentItem >= items.Length) {
          currentItem = items.Length - 1;
        }

        resetSlotPositions();
      }
    }

    //Changes the local positions and scales of each slot based on the currentItem
    public void resetSlotPositions() {
      //Deals with currentItem
      itemStack[currentItem].transform.localPosition = new Vector2(0, 0);
      itemStack[currentItem].transform.localScale = new Vector2(1f, 1f);

      //Deals with all items before CurrentItem
      for(int i = 0; i < currentItem; i++) {
        itemStack[i].transform.localPosition = new Vector2(0, 200*(currentItem-i) + 25);
        itemStack[i].transform.localScale = new Vector2(0.7f, 0.7f);
      }

      //Deals with all items after CurrentItem
      for(int i = currentItem+1; i < items.Length; i++) {
        itemStack[i].transform.localPosition = new Vector2(0, -200*(i-currentItem) - 25);
        itemStack[i].transform.localScale = new Vector2(0.7f, 0.7f);
      }
    }

    //Checks if the buy button is pressed on the current item and add it to inventory of it is
    public void manageBuy() {
      bool pressed = itemStack[currentItem].gameObject.transform.GetChild(5).GetComponent<PublicButton>().PressedState();
      if(pressed && !buyPressed) {
        if(data.isSpace() || (items[currentItem] == 6 || items[currentItem] == 7)) {
          //Debug.Log("Buy " + items[currentItem].ToString());
          if(DataMaster.money >= ItemAttributes.getPrice(items[currentItem])) {
            if(data.addItem(items[currentItem])) {
              itemStack[currentItem].transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = data.getNumberItems(items[currentItem]).ToString();
              DataMaster.money -= ItemAttributes.getPrice(items[currentItem]);
            }
          } else {
            displayMoneyError();
          }
        } else {
          displaySpaceError();
        }


        buyPressed = true;
      }

      if(!pressed) {
        buyPressed = false;
      }
    }

    //This method handles what is to be done when the player tries to buy something but there is no space in the inventory [INCOMPLETE]
    public void displaySpaceError() {
      //Fill in code to add an error message somehow
      Debug.Log("Not Enough Space");
      spaceError.SetActive(true);
      timer = 0;
    }

    //This method handles what is to be done when the player tries to buy something but doesn't have enough money [INCOMPLETE]
    public void displayMoneyError() {
      //Fill in code to add an error message somehow
      Debug.Log("Not Enough Money");
      moneyError.SetActive(true);
      timer = 0;
    }

    //Manages when to hide error messages based on timer
    public void controlTimer() {
      //If timer is up, reset error message
      if(timer > 300) {

        timer = -1; //Deactivates timer

        moneyError.SetActive(false);
        spaceError.SetActive(false);

      //If timer has been activated but is not up.
      } else if(timer >= 0) {
        timer++;
      }

    }

    //Resets the number of a given item in inventory for all items in the shop display. This method is to be called when inventory is opened
    public void resetNumberInInventoryDisplay() {
      if(itemStack != null) {
        for(int i = 0; i < items.Length; i++) {
          itemStack[i].transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = data.getNumberItems(items[i]).ToString();
        }
      }
    }
}
