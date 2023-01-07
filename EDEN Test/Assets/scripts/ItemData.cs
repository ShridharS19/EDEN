using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

This class stores data about an item (its position in the item inventory and its item ID)

*/


public class ItemData : MonoBehaviour
{
    public int row;
    public int column;

    public int id;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Initialises the object
    void init(int r, int c, int i) {
      row = r;
      column = c;
      id = i;
    }
}
