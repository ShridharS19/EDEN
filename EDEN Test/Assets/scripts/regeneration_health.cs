using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regeneration_health


    /*
     * it will perform a mega drain or a regen depending on the sign of the amounthealth variable,
     * you can set the time limit for this effect. The effect will wait for the last regen timer to stop before stopping
     * 
     */
{
    GameObject effector;
    float TimeBetweenConsIncrease; // the time between two health increases
    float AmountHealthIncrease; // the amount health is increased
    float totalTime; // the total time this regens happen for
    bool isActive = true;
    float attackVar;
    private FuncTimer timer;  // to store the timer
    private bool fireEffect;
    private GameObject Fire;


    public regeneration_health(GameObject effector, float AmountHealthIncrease, float TimeBetweenConsIncrease, float totalTime, float attackVar = 1f)
    {
        effector.GetComponent<Health_manager>().Ondeathofobject += Regeneration_health_Ondeathofobject; // to delete the timer if the enemy dies midway event
        this.effector = effector;
        this.AmountHealthIncrease = AmountHealthIncrease;
        this.TimeBetweenConsIncrease = TimeBetweenConsIncrease;
        this.totalTime = totalTime;
        this.attackVar = attackVar;
        if (AmountHealthIncrease < 0) // the health is being decreased
        {
            fireEffect = true;
            // start the fire effect
        }
        
        
    }
    
    private void Regeneration_health_Ondeathofobject(object sender, GameObject e) // if the enemy that is going throught the effect dies
    {
        isActive = false;
        if(timer != null) // if the timer is still on
        {
            FuncTimer.stopTimer(timer); // stop the timer
        }
        if(Fire != null)
        Object.Destroy(Fire);

    }

    public void Trigger()
    {
          
        FuncTimer.Create(deactivate, totalTime); // this is the timer for the total regen effect
        // create the fire visual
        if (fireEffect)
        {
            if (GameObject.Find("player") != null)
                Fire = Object.Instantiate(GameObject.Find("player").GetComponent<ActiveOrbs>().FireEffect, effector.transform);
            else
                Debug.Log("sorry the player is null");
            Fire.transform.localPosition = new Vector3(0.5f, -0.01f, 0f); // just so that it is created the near the feet of the enemy
            
        }


        healthchange();
        
    }

    private void deactivate()
    {
        isActive = false;
        if (Fire != null)
            Object.Destroy(Fire);
    }

    private void healthchange()
    {
        if (AmountHealthIncrease > 0)
            effector.GetComponent<Health_manager>().add_health(AmountHealthIncrease); // handles for a regen effect
        else
        {
            if (effector == null)
            {
                Debug.Log("there is a error");
            }
            effector.GetComponent<Health_manager>().reduce_health(-AmountHealthIncrease, attackVar); // this handles for a mega drain effect
          
        }


        if(isActive)
        timer = FuncTimer.Create(healthchange, TimeBetweenConsIncrease);
    }
}
