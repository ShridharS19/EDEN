using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// manages the look of the health bar (shared by all damageable entities)
// all health related stuff should not be managed through this class PLEASE USE THE HEALTH_MANAGER CLASS
public class healthBar_control : MonoBehaviour
{
    public float health = 100f; // store percentage of heath remaining
    public float max_health = 100f;
    float health_left;
    public Transform healthbar_container;



	void Start()
    {
        health_left = health / max_health; // converting from percentage into number between 0 and 1 since our scale of the healthbar is 1 in the x direction
                                           //if(health_left < transform.localScale.x)



        transform.localScale = new Vector3(health_left, transform.localScale.y, 0);// changing the scale of the health bar which causes the effect health change visually




    }

    public float GetHealth() // gets the value of the current health
    {
        return health;
    }


    public void setHealth(float value) // allow to set health can only be used by the health manager class all other changes in health must take place from there
    {
        health = value;
    }

    public void Refresh() // refresh the look of the healthbar on the scene
    {
        health_left = health / max_health; // calculates the percent to show since the health bar displays a value between 0 and 1
        Debug.Log("health " + health_left);
        transform.localScale = new Vector3(health_left, transform.localScale.y, 0);// changing the scale of the health bar which causes the effect health change visually
    }
    // work on the below code need to debug

    public void IncreaseHP(float value, bool update_look) // if update look is false it will increase HP without increasinig the actual size of the healthbar
	{
        if (update_look)
      {
            healthbar_container.localScale = new Vector3(healthbar_container.localScale.x + (value / 100), healthbar_container.localScale.y, 0);// make the health bar larger
            healthbar_container.localPosition = new Vector3(healthbar_container.localPosition.x - (float)(value / 200), healthbar_container.localPosition.y, 0); // reposition the healthbar to center it
        }
        max_health += value;
        health *= max_health/(max_health - value); // change the present health to the same percent of the maxhealth that it was before the HP boost

        Refresh(); // update the look of the healthbar such that the percentage of health remains the same if HP is increased when the health is not full



	}
    public float getHp() // get the value of the Hp only used by the health manager class all other classes must use that to edit any health related stuff
    {
        return max_health;
    }

    public void setHp(float value) // set the value of the Hp only used by the health manager class all other classes must use that to edit any health related stuff
    {
        max_health = value;
    }

}
