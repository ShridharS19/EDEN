using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Manages the height and position (Mostly y value) of the slider

*/

public class ShopSlider : MonoBehaviour
{

    public GameObject slider;  //Stores the reference to the slider itself
    public GameObject stack;  //Stores the reference to the stack to get size and current item

    double h;  //Stores the height of the slider
    double y; //Stores the y position of the slider

    // Start is called before the first frame update
    void Start()
    {
      h = (610.0/stack.GetComponent<ShopItemManager>().items.Length);
      y = 255 - h/2 - h*(stack.GetComponent<ShopItemManager>().currentItem);

      updateSlider();
    }

    // Update is called once per frame
    void Update()
    {
      h = (int)(610/stack.GetComponent<ShopItemManager>().items.Length);
      y = 255 - h/2 - h*(stack.GetComponent<ShopItemManager>().currentItem);

      updateSlider();
    }

    //Changes the dimensions of the slider to the correct values
    public void updateSlider() {
      slider.transform.localPosition = new Vector2(625, (float)y);
      slider.GetComponent<RectTransform>().sizeDelta = new Vector2(20, (float)h);
    }
}
