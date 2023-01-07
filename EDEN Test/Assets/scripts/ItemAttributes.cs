using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

This class stores all of the attributes of each item in arrays except for the sprites. The array index is the same as the item id of each item.
Keep in mind, item ID 0 is null.

Item IDs:
0 -> null
1 -> Blue Potion
2 -> Pink Potion
3 -> Red Potion
4 -> Green Potion
5 -> Orange Potion
6 -> Arrow
7 -> Block

*/

public class ItemAttributes : MonoBehaviour
{
    //Stores the titles of each item
    public static string[] titles = {"", "Blue Potion", "Pink Potion", "Red Potion", "Green Potion", "Orange Potion", "Arrow", "Block"};

    //Stores the descriptions of each item
    public static string[] descriptions = {
      "",
      "Does Blue thing and stuff like that",
      "Does pink stuff, hey this is only dummy text okay",
      "This is the red potion thing. Dummy, this isn\'t final",
      "Umm, IDK, GRRREEEEEENNNNN!!!!!!",
      "Okay so since the orange is last as of the 19th of June, 2020, I am going to try and make this long. Just to see what happens...",
      "The arrow can be shot out of a bow to do damage etc etc and this is why I think, the price is right!!",
      "The block is super duper expensive, but rightfully so. This ensures that you don\'t overuse it. Be careful!! You never know when you'll need it."
    };

    //Stores the prices of each item
    public static int[] prices = {0, 5, 10, 20, 50, 100, 2, 1000};
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Returns title of item with Item ID of ID
    public static string getTitle(int ID) {
      return(titles[ID]);
    }

    //Returns description of item with Item ID of ID
    public static string getDescription(int ID) {
      return(descriptions[ID]);
    }

    //Returns price of item with Item ID of ID
    public static int getPrice(int ID) {
      return(prices[ID]);
    }
}
