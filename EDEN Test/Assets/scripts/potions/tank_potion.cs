using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
 * the potions effects are :
 * it increased HP by a certain percent
 * increase melee attacks
 * decrease speed
 * decrease range attacks
 * if false = effectType it will do the complete opposite of the above
 * only use the Trigger method to trigger the potion
 * and the reset method to reset the values in case u want to stop the effects of the potion before the timer runs out
 */
public class tank_potion : potion
{
    
    private Potion_type Name = Potion_type.tank_defence; // name of the potion
    private float[] melee_percent = new float[2]; // it is in a percent increase form
    private float[] speed_percent = new float[2];
    private float[] range_percent = new float[2]; 
    private float[] Hp_percent = new float[2];
  
   
    private value_control valcon;
    private Health_manager healthinst;

    public tank_potion(GameObject effector1, bool effectType1, float melee_percent_change,float speed_percent_change,float range_percent_change,float Hp_percent_change, int timer_length) : base(effectType1, effector1, timer_length) // calling the constructor of the base class
    {
        // all taken values are positive
        this.melee_percent[0] = melee_percent_change;
        this.speed_percent[0] = speed_percent_change;
        this.range_percent[0] = range_percent_change;
        this.Hp_percent[0] = Hp_percent_change;
      
    }

    private bool effect() // call to trigger effects of the potion
    {
        if (effectType) // this variable is from the base class and determines if the potion has a negative or a positive effect
        {
            return change_values(false, +melee_percent[0], -range_percent[0], -speed_percent[0], +Hp_percent[0]);

        }
        else
        {
            return change_values(false, -melee_percent[0], +range_percent[0], +speed_percent[0], -Hp_percent[0]); 
        }
    }
    public override bool Triggermaking() // it is the method which is called to start the effect of the potion
    {
        if (effect())
        {
            IsInEffect = true;

            timerInstance = FuncTimer.Create(reset, timer_len, Potion_type.tank_defence.ToString());
            
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
    

    public int timer_len_get()
    {
        return timer_len;
    }
   public void reset()
    {
        change_values(true);
        EndedTimer();
    }

    // the below method revieves percents without signs 
    private bool change_values(bool reset_potion, float melee_percent_change = 0f, float range_percent_change = 0f, float speed_percent_change = 0f, float Hp_percent_change = 0f) // changes health by change if possible returns true else false
    {
        valcon = effector.GetComponent<value_control>();
        healthinst = effector.GetComponent<Health_manager>();
        if (effector.GetComponent<value_control>() != null && effector.GetComponent<Health_manager>() != null )
        {
            if (!reset_potion) // if it is not called as a reset
            {

                speed_percent[1] = valcon.GetSpeed();
                valcon.SetSpeed(valcon.GetSpeed() * (1 + (speed_percent_change / 100)));
                if (valcon.meleeAttacker)
                {
                    valcon.SetMeleeValue(1 + (melee_percent_change / 100));
                    melee_percent[1] = valcon.GetMeleeValue().Count - 1;// this gets the index of the array list where the multipliers are stored
                }
                if (valcon.projectileAttacker)
                {
                    

                    valcon.SetProjetileValue(1 + (range_percent_change / 100)); // changes the projectile damage value
                    range_percent[1] = valcon.GetProjectileValue().Count - 1; // this stores the index in the array list where the multiplier is stored
                }
                Hp_percent[1] = healthinst.Get_HP();
                healthinst.HP_increase((Hp_percent_change / 100) * healthinst.Get_HP());
            }
            else // this is called when the timer is run down so that the values are changed back to their original state
            {
                if (PotionsINUse > 1)
                    valcon.SetSpeed(speed_percent[1]);
                else
                    valcon.SetSpeed(BaseSpeed);
                

                
                if (valcon.meleeAttacker)
                    valcon.SetmeleeMult(1, (int)melee_percent[1]);
                if (valcon.projectileAttacker)
                    valcon.SetprojectileMult(1, (int)(range_percent[1]));
                if(PotionsINUse > 1)
                    healthinst.HP_increase(Hp_percent[1] - healthinst.Get_HP());
                else
                    healthinst.HP_increase(BaseHP - healthinst.Get_HP());
            }


            return true;
        }
        else
        {
            return false;
        }
    }

}
