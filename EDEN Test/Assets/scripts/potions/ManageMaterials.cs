using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageMaterials : MonoBehaviour
{


    MaterialP[] material_data;
    public Sprite[] active_sprites;
    public Sprite[] inactive_sprites;

    public GameObject[] materials;

    // Start is called before the first frame update
    void Start()
    {
      material_data = MaterialDatabase.getMaterialList();
      updateGameObjects();
    }

    // Update is called once per frame
    void Update()
    {

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

      updateGameObjects();
    }

    //Removes amount from the number of material at index index
    public void removeMaterials(int index, int amount) {
      DataMaster.material_amounts[index] -= amount;
      if(DataMaster.material_amounts[index] < 0) {
        DataMaster.material_amounts[index] = 0;
      }

      updateGameObjects();
    }

    //Sets amount of the number of material at index index
    public void setMaterialAmount(int index, int amount) {
      DataMaster.material_amounts[index] = amount;

      if(DataMaster.material_amounts[index] < 0) {
        DataMaster.material_amounts[index] = 0;
      }

      updateGameObjects();
    }

    //updates all material sprites and numbers
    public void updateGameObjects() {
      for(int i = 0; i < material_data.Length; i++) {

        if(DataMaster.material_amounts[i] == 0) {
          materials[i].GetComponent<Image>().sprite = inactive_sprites[i];
        } else {
          materials[i].GetComponent<Image>().sprite = active_sprites[i];
        }

        updateNumbers(materials[i], i);
      }
    }

    //returns entire material database
    public MaterialP[] getMaterialDatabase() {
      return(material_data);
    }
}
