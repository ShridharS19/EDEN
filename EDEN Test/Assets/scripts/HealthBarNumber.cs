using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

This class manages the text that shows amount of health and total health

*/

public class HealthBarNumber : MonoBehaviour
{
    public int max_health;      //Stores the maximum health of the player
    public int current_health; //Stores the current health of the player

    public GameObject Text; //Stores the gameObject that has the text for the healthbar
    public GameObject Bar; //Stores the gameObject that has the text for the healthbar

    // Start is called before the first frame update
    void Start()
    {
      update();
    }

    // Update is called once per frame
    

    //Changes the text in Text and the Length of the bar
    public void update() {
      Text.GetComponent<Text>().text = current_health.ToString() + " / " + max_health.ToString(); //Edits the text as required

      Bar.GetComponent<RectTransform>().sizeDelta = new Vector2(((float)current_health/(float)max_health) * 350, 20);           //Makes sure that the healthbar width remains proportional
      Bar.GetComponent<RectTransform>().localPosition = new Vector2((1-((float)current_health/(float)max_health)) * -175, -5); //Makes sure the healthbar starts at the right place
    }

    public int getMaxHealth() {
      return(max_health);
    }

    public void setMaxHealth(int h) {
        Debug.Log("set the health to: " + h);


        current_health = ((current_health * h) / max_health);
        Debug.Log("The current health has been set to: " + current_health);
        max_health = h;
        update();
    }

    public int getCurrentHealth() {
      return(current_health);
    }

    public void setCurrentHealth(int h) {
      if(h > max_health)
        {
            current_health = max_health;
        }
      current_health = h;
        update();
    }
}
