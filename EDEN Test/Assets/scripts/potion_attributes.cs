using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion_attributes : MonoBehaviour

{
    private Sprite[] images_potions;
    private GameObject ItemsGameObject;
    public int ItemData_ID;// the item that needs to be launched

    public Sprite EmptyBottle;

    bool isCP = false;
    PotionStorage potionData;
    // Start is called before the first frame update
    void Start()
    {
        ItemsGameObject = GameObject.FindGameObjectWithTag("item_UI");


        images_potions = ItemsGameObject.GetComponent<ItemInventoryData>().images; // array that stores the sprites for the potions


    }

    public void setItemIndex(int n)
    {
        ItemsGameObject = GameObject.FindGameObjectWithTag("item_UI");
        images_potions = ItemsGameObject.GetComponent<ItemInventoryData>().images;

        ItemData_ID = n;

        GetComponent<SpriteRenderer>().sprite = images_potions[ItemData_ID]; // setting the Sprite to the sprite that needs to be launched

        isCP = false;
    }

    public void setItemIndex(int n, GameObject cp, PotionStorage ps)
    {
        ItemsGameObject = GameObject.FindGameObjectWithTag("item_UI");
        images_potions = ItemsGameObject.GetComponent<ItemInventoryData>().images;

        ItemData_ID = n;

        GetComponent<SpriteRenderer>().sprite = EmptyBottle;

        isCP = true;
        potionData = ps;
    }

    public bool isCustomPotion() {
      return(isCP);
    }

    public PotionStorage getPotionData() {
      return(potionData);
    }

    public int GetItemIndex()
    {
        return ItemData_ID;
    }


}
