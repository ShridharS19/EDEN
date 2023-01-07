using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * this class is used to add the health to the effector gameobject
 * if the value of effectType is false then the potion has a negative effect:
 * instead of add health we do subtract health
 * the trigger method triggers the effect of the potion
 * author arnav
 */
public class Health_potion : potion
{
    
    private Potion_type Name = Potion_type.health; // name of the potion
	private float health_change; // this determines how much health will be increased or decreased by the effect of this potion
    public Health_potion(GameObject effector1, bool effectType1, float health_change): base(effectType1, effector1, -1) // calling the constructor of the base class
    {
        hastimer = false;
		this.health_change = health_change;
    }

    private bool effect() // call to trigger effects of the potion
    {
        if(effectType) // this variable is from the base class and determines if the potion has a negative or a positive effect
		{
             return change_healthmethod(health_change);
            
		}
        else
        {
           return change_healthmethod(-health_change);
        }
    }
    public override bool Triggermaking()
    {
        if(effect())
        {
            IsInEffect = true;
            return true;
        }
        else
        {
            IsInEffect = false;
            return false;
        }
    }
    public override Potion_type GetName() // getter method
    {
        return Name;
    }

    

    private bool change_healthmethod(float change) // changes health by change if possible returns true else false
    {
        
        if (effector.GetComponent<Health_manager>() != null) // if the effector object is a object which can take health (to prevent errors if health_manager script is missing)
        {
            if (change > 0)
            {
                effector.GetComponent<Health_manager>().add_health(change);
            }
            else
            {
                effector.GetComponent<Health_manager>().reduce_health(change);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
