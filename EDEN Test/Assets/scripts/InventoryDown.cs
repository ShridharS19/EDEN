using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDown : MonoBehaviour
{
    public RectTransform inventory;
    public bool isVisible;

    // Start is called before the first frame update
    void Start()
    {
        //inventory.transform.localPosition = new Vector3(0.0f, -5000.0f, 0.0f);
        //isVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.E)) {
        //Toggles inventory display
        if(isVisible) {
          inventory.transform.localPosition = new Vector3(0.0f, -5000.0f, 0.0f);
          isVisible = false;
        } else {
          inventory.transform.localPosition = new Vector3(0.0f, -250.0f, 0.0f);
          isVisible = true;
        }
      }
    }

    //Makes inventory Visible
    public void makeVisible() {
      inventory.transform.localPosition = new Vector3(0.0f, -250.0f, 0.0f);
      isVisible = true;
    }

    //Makes inventory Invisible
    public void makeInvisible() {
      inventory.transform.localPosition = new Vector3(0.0f, -5000.0f, 0.0f);
      isVisible = false;
    }
}
