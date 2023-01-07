using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

This class is intended to manage the potion end of custom potion creation.
It creates the potion and shows the sprite.

When the button it pressed, a method in this class is called which adds the potion to inventory and resets the ingredients and catalyst fields.

*/

public class PotionInitialisation : MonoBehaviour
{

    public GameObject Ingredient1;
    public GameObject Ingredient2;
    public GameObject Ingredient3;
    public GameObject Catalyst;

    public GameObject PotionSprite;
    public GameObject PotionPrefab;
    public Sprite EmptyBottle;
    public GameObject OldPotion;

    public GameObject potionInventory;

    bool updated = false;

    // Start is called before the first frame update
    void Start()
    {

      //PotionSprite.GetComponent<Image>().sprite = EmptyBottle;

      if(viablePotion()) {
        showPotion();
      } else {
        showEmpty();
      }

      updated = false;
    }

    // Update is called once per frame
    void Update()
    {
      if(!updated) {
        updatePotion();
        updated = true;
      }
    }

    //Checks if the potion can be made. Returns true if it can. Else, returns false
    public bool viablePotion() {
      bool mat1 = Ingredient1.GetComponent<IngredientSelection>().isViableMaterial();
      bool mat2 = Ingredient2.GetComponent<IngredientSelection>().isViableMaterial();
      bool mat3 = Ingredient3.GetComponent<IngredientSelection>().isViableMaterial();
      bool cat  = Catalyst.GetComponent<CatalystSelector>().isViableMaterial();

      return(mat1 && mat2 && mat3 && cat);
    }

    //Resets all of the ingredients and catalysts to get next material after the potion is made.
    private void resetAll() {
      Ingredient1.GetComponent<IngredientSelection>().reset();
      Ingredient2.GetComponent<IngredientSelection>().reset();
      Ingredient3.GetComponent<IngredientSelection>().reset();
      Catalyst.GetComponent<CatalystSelector>().reset();
    }

    //Shows the potion that will be formed in the crafting interface
    private void showPotion() {
      if(viablePotion()) {

        if(OldPotion != null) {
          Destroy(OldPotion);
        }

        GameObject cp = Instantiate(PotionPrefab);
        cp.transform.parent = PotionSprite.transform;
        cp.transform.localPosition = new Vector3(0, 0, 0);
        cp.transform.localScale = new Vector3(400/3, 400/3, 1);
        //cp.GetComponent<rectTransform>().rect.height = 200;
        cp.AddComponent<Image>();
        cp.GetComponent<Image>().sprite = EmptyBottle;

        float timer_len = 0;

        cp.GetComponent<potionColourSetter>().SetArrayOfStats(Ingredient1.GetComponent<IngredientSelection>().getMaterial(),
                                                              Ingredient2.GetComponent<IngredientSelection>().getMaterial(),
                                                              Ingredient3.GetComponent<IngredientSelection>().getMaterial(),
                                                              Catalyst.GetComponent<CatalystSelector>().getMaterial(), Catalyst.GetComponent<CatalystSelector>().getNumber(),
                                                              out timer_len);

        for(int i= 0; i < 5; i++) {
          cp.transform.GetChild(i).gameObject.AddComponent<Image>();
          cp.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = cp.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite;
          cp.transform.GetChild(i).gameObject.GetComponent<Image>().color = cp.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color;
        }

        OldPotion = cp;

      } else {
        showEmpty();
      }
    }

    //Shows empty bottle
    private void showEmpty() {
      if(OldPotion != null) {
        Destroy(OldPotion);
      }
    }

    //If the potion is viable, this method makes the potion. It handles for the reseting of all values as well.
    public void makePotionIfViable() {
      if(viablePotion() && potionInventory.GetComponent<CustomPotionManager>().isSpace()) {

        MaterialP[] mats = new MaterialP[4];
        mats[0] = Ingredient1.GetComponent<IngredientSelection>().getMaterial();
        mats[1] = Ingredient2.GetComponent<IngredientSelection>().getMaterial();
        mats[2] = Ingredient3.GetComponent<IngredientSelection>().getMaterial();
        mats[3] = Catalyst.GetComponent<CatalystSelector>().getMaterial();

        float timer_len = 0;

        MaterialP pot = PotionCreationMath.Calculate(mats, out timer_len);
        timer_len = 30 + (int)(210*timer_len);
        PotionStorage p = new PotionStorage(pot, timer_len);

        potionInventory.GetComponent<CustomPotionManager>().addPotion(p);
        resetAll();
      }
    }

    public void updatePotion() {
      if(viablePotion()) {
        showPotion();
      } else {
        showEmpty();
      }
    }

    public void justOpened() {
      updated = false;
    }
}
