using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script is solely intended to help configure changing between UI Co-ordinates and Global Co-ordinates
*/


public class UIMouseTracker : MonoBehaviour
{
    public GameObject tracker;
    public GameObject inventory;

    // Start is called before the first frame update
    void Start()
    {
      //Vector3 mouse = new Vector3(Input.mousePosition.x - inventory.transform.position.x + inventory.transform.localPosition.x, Input.mousePosition.y/1.6f - inventory.transform.position.y + inventory.transform.localPosition.y, 0.0f);
      Vector3 mouse = new Vector3(Input.mousePosition.x/1.6f - Screen.width/3.2f, Input.mousePosition.y/1.6f - Screen.height/3.2f, 0.0f);
      tracker.transform.localPosition = mouse;
    }

    // Update is called once per frame
    void Update()
    {
      //Vector3 mouse = new Vector3(Input.mousePosition.x - inventory.transform.position.x + inventory.transform.localPosition.x, Input.mousePosition.y/1.6f - inventory.transform.position.y + inventory.transform.localPosition.y, 0.0f);
      Vector3 mouse = new Vector3(Input.mousePosition.x/1.6f - Screen.width/3.2f, Input.mousePosition.y/1.6f - Screen.height/3.2f, 0.0f);
      tracker.transform.localPosition = mouse;
    }
}
