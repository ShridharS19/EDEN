using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalystSelector : MonoBehaviour
{
  int material_index;
  public GameObject material;

  int number;
  public GameObject counter;

  Sprite[] active_sprites;
  public Sprite noMaterial;

  public GameObject material_manager;
  public GameObject master_material_object;

  // Start is called before the first frame update
  void Start()
  {
    //manageStart();
  }

  public void manageStart() {
    /*active_sprites = new Sprite[material_manager.transform.childCount];
    for(int i = 0; i < material_manager.transform.childCount; i++) {
      active_sprites[i] = material_manager.transform.GetChild(i).GetComponent<Image>().sprite;
    }*/

    active_sprites = master_material_object.GetComponent<ManageMaterials>().active_sprites;

    material_index = material_manager.GetComponent<ManageMaterialsCrafting>().getNextPresent(-1);

    if(material_index != -1) {
      material_manager.GetComponent<ManageMaterialsCrafting>().removeMaterials(material_index, 1);
      number = 1;
    }

    updateSprite();
  }

  //Better Method name for some applications, but otherwise the same as manageStart()
  public void reset() {
    manageStart();
  }

  public void manageEnd() {
    material_manager.GetComponent<ManageMaterialsCrafting>().addMaterials(material_index, number);
    material_index = -1;
    number = 1;
  }

  // Update is called once per frame
  void Update()
  {
    updateSprite();
  }

  //Updates the sprite of the ingredient to the selection
  private void updateSprite() {
    if(material_index != -1) {
      material.GetComponent<Image>().sprite = active_sprites[material_index];
      counter.GetComponent<Text>().text = number.ToString();
    } else {
      material.GetComponent<Image>().sprite = noMaterial;
      counter.GetComponent<Text>().text = number.ToString();
      counter.GetComponent<Text>().text = "0";
    }
  }

  //goes to next material with amount greater than 0;
  public void next() {
    int index = material_manager.GetComponent<ManageMaterialsCrafting>().getNextPresent(material_index);
    if(index != -1) {
      material_manager.GetComponent<ManageMaterialsCrafting>().addMaterials(material_index, number);
      material_index = index;
      material_manager.GetComponent<ManageMaterialsCrafting>().removeMaterials(material_index, 1);
      number = 1;
    }
  }

  //goes to previous material with amount greater than 0;
  public void previous() {
    int index = material_manager.GetComponent<ManageMaterialsCrafting>().getPreviousPresent(material_index);
    if(index != -1) {
      material_manager.GetComponent<ManageMaterialsCrafting>().addMaterials(material_index, number);
      material_index = index;
      material_manager.GetComponent<ManageMaterialsCrafting>().removeMaterials(material_index, 1);
      number = 1;
    }
  }

  //Increases the number of catalyst selected
  public void increase() {
    if(material_manager.GetComponent<ManageMaterialsCrafting>().getNumMaterial(material_index) != 0) {
      number++;
      material_manager.GetComponent<ManageMaterialsCrafting>().removeMaterials(material_index, 1);
    }
  }

  //Decreases the number of catalyst selected
  public void decrease() {
    if(number > 1) {
      number--;
      material_manager.GetComponent<ManageMaterialsCrafting>().addMaterials(material_index, 1);
    }
  }

  //If material index is -1, returns false, else returns true
  public bool isViableMaterial() {
    return(material_index != -1);
  }

  //returns the material
  public MaterialP getMaterial() {
    if(material_index != -1) {
      return(master_material_object.GetComponent<ManageMaterials>().getMaterialDatabase()[material_index]);
    } else {
      return(null);
    }
  }

  //Getter for the number of Catalysts
  public int getNumber() {
    return(number);
  }
}
