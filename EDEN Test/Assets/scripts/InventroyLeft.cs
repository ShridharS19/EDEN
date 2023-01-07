using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventroyLeft : MonoBehaviour
{
    public RectTransform inventory;
    bool isVisible;

    // Start is called before the first frame update
    void Start()
    {
      //inventory.transform.localPosition = new Vector3(-715.0f, 100.0f, 0.0f);
      //isVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.E)) {
        //Toggles inventory display
        if(isVisible) {
          inventory.transform.localPosition = new Vector3(-715.0f, 100.0f, 0.0f);
          isVisible = false;
        } else {
          inventory.transform.localPosition = new Vector3(-552.0f, 100.0f, 0.0f);
          isVisible = true;
        }
      }
    }

    //Makes the Inventory visible
    public void makeVisible() {
      inventory.transform.localPosition = new Vector3(-552.0f, 100.0f, 0.0f);
      isVisible = true;
    }

    //Makes inventory Invisible
    public void makeInvisible() {
      inventory.transform.localPosition = new Vector3(-715.0f, 100.0f, 0.0f);
      isVisible = false;
    }
}
