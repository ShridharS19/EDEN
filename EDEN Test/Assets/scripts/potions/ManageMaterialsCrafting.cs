using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageMaterialsCrafting : MonoBehaviour
{

  MaterialP[] material_data;

  Sprite[] active_sprites;
  Sprite[] inactive_sprites;
  public GameObject master_material_object;

  public GameObject[] materials;

  // Start is called before the first frame update
  void Start()
  {
    material_data = MaterialDatabase.getMaterialList();
    active_sprites = master_material_object.GetComponent<ManageMaterials>().active_sprites;
    inactive_sprites = master_material_object.GetComponent<ManageMaterials>().inactive_sprites;

    updateGameObjects();
  }

  // Update is called once per frame
  void Update()
  {
    updateGameObjects();
  }

  //Updates number the material with the correct number from static variable
  private void updateNumbers(GameObject material, int mat_index) {
    GameObject counter = material.transform.GetChild(0).gameObject;

    counter.GetComponent<Text>().text = DataMaster.material_amounts[mat_index].ToString();
  }

  //Returns the amount of the material indexed at index
  public int getNumMaterial(int index) {
    return(DataMaster.material_amounts[index]);
  }

  //Adds amount to the number of material at index index
  public void addMaterials(int index, int amount) {
    DataMaster.material_amounts[index] += amount;

    if(DataMaster.material_amounts[index] < 0) {
      DataMaster.material_amounts[index] = 0;
    }

    master_material_object.GetComponent<ManageMaterials>().updateGameObjects();
  }

  //Removes amount from the number of material at index index
  public void removeMaterials(int index, int amount) {
    DataMaster.material_amounts[index] -= amount;

    if(DataMaster.material_amounts[index] < 0) {
      DataMaster.material_amounts[index] = 0;
    }

    master_material_object.GetComponent<ManageMaterials>().updateGameObjects();
  }

  //Sets amount of the number of material at index index
  public void setMaterialAmount(int index, int amount) {
    DataMaster.material_amounts[index] = amount;

    if(DataMaster.material_amounts[index] < 0) {
      DataMaster.material_amounts[index] = 0;
    }

    master_material_object.GetComponent<ManageMaterials>().updateGameObjects();
  }

  //updates all material sprites and numbers
  private void updateGameObjects() {
    for(int i = 0; i < material_data.Length; i++) {
      if(DataMaster.material_amounts[i] == 0) {
        materials[i].GetComponent<Image>().sprite = inactive_sprites[i];
      } else {
        materials[i].GetComponent<Image>().sprite = active_sprites[i];
      }

      updateNumbers(materials[i], i);
    }
  }

  //Returns the index of the next element after index that has amount greater than 0. This loops. If index is -1, it gives the first material with amount greater than 0
  //If empty, returns -1
  public int getNextPresent(int index) {
    if(isEmpty()) {
      return(-1);
    }

    int[] material_amounts = DataMaster.material_amounts;

    //Checks from after index to end
    for(int i = index+1; i < material_amounts.Length; i++) {
      if(material_amounts[i] != 0) {
        return(i);
      }
    }

    //checks from 0 to index
    for(int i = 0; i <= index; i++) {
      if(material_amounts[i] != 0) {
        return(i);
      }
    }

    return(-1); //Should ideally never run, but the compiler is too dumb! ;)

  }

  //Returns the index of the previous element before index that has amount greater than 0. This loops. If index is -1, it gives the last material with amount greater than 0
  //If empty, returns -1
  public int getPreviousPresent(int index) {
    if(isEmpty()) {
      return(-1);
    }

    int[] material_amounts = DataMaster.material_amounts;

    //Checks from after index to end
    for(int i = index-1; i >= 0; i--) {
      if(material_amounts[i] != 0) {
        return(i);
      }
    }

    //checks from 0 to index
    for(int i = material_amounts.Length-1; i >= index; i--) {
      if(material_amounts[i] != 0) {
        return(i);
      }
    }

    return(-1); //Should ideally never run, but the compiler is too dumb! ;)

  }

  //returns true if there are no materials of any kind, else returns false
  public bool isEmpty() {
    int[] material_amounts = DataMaster.material_amounts;

    for(int i = 0; i < material_amounts.Length; i++) {
      if(material_amounts[i] != 0) {
        return(false);
      }
    }

    return(true);
  }
}
