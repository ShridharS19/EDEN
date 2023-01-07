using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * handles all the virtual items in the world
 * you can add: potions, orbs, arrows and blocks
 * you can also add a number below it this wont work for orbs cause you cant get two of the same orb
 * if the player collides with it the thing goes into his invetory automatically
 * to use this just call void setAttributes of pickable
 *
 */
public class types_of_pickables : MonoBehaviour
{

    private Sprite currentImage;
    // Start is called before the first frame update
    private GameObject ItemsGameObject;
    private Sprite[] images_items;
    public Sprite BlockSprite;
    public Sprite ArrowSprite;
    private Sprite[] images_orbs;
    public int item_code = 1;// public only for testing make private after
    public int NumberOfItems;  // public only for testing make private after
    public ItemType typeofpickable;// public only for testing make private after
    private GameObject armour_inventory;// to access the sprite array of orbs
    public enum ItemType
    {
        Potion,
        Orb,
        Block,
        arrow


    }

    void Start()
    {
        NumberTextUpdate(); // default
        ItemsGameObject = GameObject.FindGameObjectWithTag("item_UI");
        armour_inventory = GameObject.FindGameObjectWithTag("armourInv");
        // set the defualts for the look of the pickable which is the 0th orb or the 1st item
        images_items = ItemsGameObject.GetComponent<ItemInventoryData>().images; // taking the array of images from inventory just so that all the images are stored in the same place same for orbs also
        images_orbs = armour_inventory.GetComponent<HideOrbs>().sprites;

        // the below sets the default if nothing is set later this will be adopted
        if (typeofpickable == ItemType.Potion)
        {
            currentImage = images_items[item_code]; // default

            
        }
        else if (typeofpickable == ItemType.Orb)
        {
            currentImage = images_orbs[0];
            item_code = 0;
            
            

        }
        else if (typeofpickable == ItemType.Block)
        {

            currentImage = BlockSprite; // set the image of the block
        
            
        }
        else if(typeofpickable == ItemType.arrow)
        {
            currentImage = ArrowSprite;// set the image of the arrow


        }
        GetComponent<SpriteRenderer>().sprite = currentImage; // update the image to the sprite renderer

    }
 
    /*
     * The item codes for items are as follows:
0 -> Empty
1 -> Blue Potion
2 -> Pink Potion
3 -> Red Potion
 take it from the inventory

     */
    /*
     * item codes for orbs:
     * Orb IDs:
0 -> Grass
1 -> Lightning
2 -> Fire
3 -> Wind
4 -> Earth
     *
     *
     */
    
    public ItemType GetpickableType() 
    {
        return typeofpickable;
    }
    public int GetNumberOfItems()
    {
        return NumberOfItems; 
    }
    private void set_image(int n) // gives the array index of the images array that we want for the instance of the prefab
    {
        item_code = n;
        if (typeofpickable == ItemType.Potion) // if it is not a orb
        {
           
            currentImage = images_items[n];
            GetComponent<SpriteRenderer>().sprite = currentImage;
        }
        else if(typeofpickable == ItemType.Orb)
        {
            currentImage = images_orbs[n];
            GetComponent<SpriteRenderer>().sprite = currentImage;
        }
        // we dont need to put the block and arrow here since those images are already set in the start method
    }

   
    public Sprite[] get_arrayOfItemsprites()
    {
        return images_items;
    }

    public Sprite get_currentImage()
    {
        return currentImage;
    }

    public int get_item_code()
    {
        return item_code;
    }

    public Sprite[] get_arrayoforbs()
    {
        return images_orbs;
    }

    private void NumberTextUpdate()
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        if (typeofpickable != ItemType.Orb)
            text.text = NumberOfItems.ToString();
        else // because we cant have more than one orb
            text.text = "";
    }

    public void SetAttributedOfpickable(ItemType typeOfItem, int item_index, int numberOfItems = 1) // this should be called just after the object is instatiated to set the item 
    {
        NumberOfItems = numberOfItems;
        NumberTextUpdate();
        typeofpickable = typeOfItem;
        set_image(item_index);

    }
}
