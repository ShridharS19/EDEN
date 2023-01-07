using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manageInventory : MonoBehaviour
{

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        //Makes sure that all Inventory Elements are disabled in the beginning
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
          //Toggles inventory display
          image.enabled = !image.enabled;
        }
    }
}
