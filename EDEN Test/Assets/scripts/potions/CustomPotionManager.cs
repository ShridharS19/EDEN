using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomPotionManager : MonoBehaviour
{
    public GameObject[] potions;
    //public Sprite no_potion;
    public Sprite[] potion_sprites;

    public GameObject PotionPrefab;

    GameObject[] OldPotion = new GameObject[4];

    bool update1 = false;

    // Start is called before the first frame update
    void Start()
    {
      //springCleaning();
    }

    // Update is called once per frame
    void Update()
    {

      if(!update1) {
        springCleaning();
        update1 = true;
      }

      //Debug.Log(isSpace());
    }

    /*//Updates all Sprites of potions with respective potionsSprites
    public void updateImages() {
      for(int i = 0; i < potions.Length; i++) {
        potions[i].GetComponent<Image>().sprite = potion_sprites[i];
      }
    }

    //Sets the sprites in the sprite array to match the potion present there
    public void resetSpriteArray() {
      for(int i = 0; i < potions.Length; i++) {
        if(DataMaster.custom_potions[i] != null) {
          //...potion_sprites[i] = DataMaster.custom_potions[i].getImage();
          //Not tested
          //...GameObject[] sub_images = DataMaster.custom_potions[i].getPotionObject();
          for(int j = 0; j < 5; j++) {
            //...sub_images.transform.GetChild(j).parent = potions[j].transform;
          }
          //End not tested
        } else {
          potion_sprites[i] = no_potion;
        }
      }
    }*/

    //returns true if there is space in the inventory (At least one of the potions is null)
    public bool isSpace() {
      for(int i = 0; i < DataMaster.custom_potions.Length; i++) {
        if(DataMaster.custom_potions[i] == null) {
          return(true);
        }
      }

      return(false);
    }

    //Returns true if there is not a single potion in the inventory, else returns false
    public bool isEmpty() {
      for(int i = 0; i < DataMaster.custom_potions.Length; i++) {
        if(DataMaster.custom_potions[i] != null) {
          return(false);
        }
      }

      return(true);
    }

    //Shifts all potions such that there are no spare spots left in the middle
    public void reorderPotions() {

      //runs only if there is empty space. If there isn't, there isn't a need to reorder. Also, only runs if there is atleast one potion in the inventory
      if(isSpace() && !isEmpty()) {
        PotionStorage[] custom_potions = DataMaster.custom_potions;
        for(int i = 0; i < custom_potions.Length-1; i++) {

          if(custom_potions[i] == null) {

            bool found = false;

            for(int j = i; j < custom_potions.Length && !found; j++) {
              if(custom_potions[j] != null) {
                custom_potions[i] = custom_potions[j];
                custom_potions[j] = null;
                found = true;
              }
            }

          }

        }
      }
    }

    //Returns index of the next empty spot in the inventory for a custom potion
    //Returns -1 if there is no space
    public int getNextEmpty() {
      for(int i = 0; i < DataMaster.custom_potions.Length; i++) {
        if(DataMaster.custom_potions[i] == null) {
          return(i);
        }
      }

      return(-1);
    }

    //Adds custompotion potion to the inventory
    //returns true if successfully added, else returns false
    public bool addPotion(PotionStorage potion) {
      if(!isSpace()) {
        return(false);
      }

      int index = getNextEmpty();

      if(index == -1) {
        return(false);
      }

      DataMaster.custom_potions[index] = potion;

      springCleaning();
      return(true);
    }

    //Removes the potion at index
    //returns true if successfully removed, else returns false
    public bool removePotion(int index) {
      PotionStorage p = DataMaster.custom_potions[index];

      if(p == null) {
        return(false);
      }
      DataMaster.custom_potions[index] = null;

      for(int i = 4; i >= 0; i--) {
        GameObject.Destroy(potions[index].transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
      }

      springCleaning();
      applyPotion(p);

      return(true);
    }

    //Call required methods to apply the potion effects on the player
    //This method serves as an exit point to the custom potion inventory
    public void applyPotion(PotionStorage p) {
      //...p.Trigger();
    }

    //This method reorders potions, resets the sprite arrays and Updates Images of UI GameObjects
    public void springCleaning() {
      reorderPotions();
      //resetSpriteArray();
      //updateImages();
      manageFrontEnd();
    }

    public void manageFrontEnd() {

      for(int i = 0; i < 4; i++) {
        if(DataMaster.custom_potions[i] == null) {
          GameObject PotionSprite = potions[i].transform.GetChild(0).gameObject;
          //Delete all children of potionImage
          for(int j = PotionSprite.transform.childCount-1; j >= 0 ; j--) {
            Destroy(PotionSprite.transform.GetChild(j).gameObject);
          }
        } else {
          Debug.Log("Created");
          GameObject PotionSprite = potions[i].transform.GetChild(0).gameObject;
          MaterialP material = DataMaster.custom_potions[i].getStats();
          //DataMaster.custom_potions[i].logStats();

          if(OldPotion[i] != null) {
            Destroy(OldPotion[i]);
          }

          GameObject cp = Instantiate(PotionPrefab);
          cp.transform.parent = PotionSprite.transform;
          cp.transform.localPosition = new Vector3(0, 0, 0);
          cp.transform.localScale = new Vector3(100/3, 100/3, 1);
          //cp.GetComponent<rectTransform>().rect.height = 200;
          //cp.AddComponent<Image>();
          //cp.GetComponent<Image>().sprite = EmptyBottle;

          cp.GetComponent<potionColourSetter>().SetArrayOfStats(material);

          for(int j = 0; j < 5; j++) {
            cp.transform.GetChild(j).gameObject.AddComponent<Image>();
            cp.transform.GetChild(j).gameObject.GetComponent<Image>().sprite = cp.transform.GetChild(j).gameObject.GetComponent<SpriteRenderer>().sprite;
            cp.transform.GetChild(j).gameObject.GetComponent<Image>().color = cp.transform.GetChild(j).gameObject.GetComponent<SpriteRenderer>().color;
          }

          OldPotion[i] = cp;
        }
      }
    }

    //Performs all necessary permutations to ensure that potion is used and removed from inventory.
    //Ensures that the potion at index is used
    public void potionClicked(int index) {
      Debug.Log(index.ToString() + " is the index of the potion clicked.");

      PotionStorage p = DataMaster.custom_potions[index];
      custompotion potion = new custompotion(p);

      if(removePotion(index)) {
        potion.Trigger(GameObject.Find("player"));
      }
    }

}
