 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Pathfinding;
/*
 * this potion increases the field of view of the camera
 * it also increases projectile damage
 * decreases melee damage
 * decrease HP
 * increase the base charge for the the projectiles (the charge is the time before which it self destroys)
 */
public class sniper_potion : potion
{
    private Potion_type Name = Potion_type.snipe_range; // name of the potion
    private float[] melee_percent = new float[2]; // it is in a percent increase form
    private bool cameraZoom;
    private float[] range_percent = new float[2];
    private float[] Hp_percent = new float[2];
    
    float initzoom;
    float ZoomChangePercent;
    float[] baseProjectileCharge = new float[2];

    private value_control valcon;
    private Health_manager healthinst;

    public sniper_potion(GameObject effector1, bool effectType1, float melee_percent_change, float range_percent_change, float Hp_percent_change, int timer_length,bool cameraZoom, float baseProjectileCharge) : base(effectType1, effector1, timer_length) // calling the constructor of the base class
    {
        
        // all taken values are positive
        this.melee_percent[0] = melee_percent_change;
        this.baseProjectileCharge[0] = baseProjectileCharge;
        this.cameraZoom = cameraZoom;
        if (cameraZoom)
            ZoomChangePercent = 50f; // the amount we want to zoom in this value will be constant
        else
            ZoomChangePercent = 0f;
        this.range_percent[0] = range_percent_change;
        this.Hp_percent[0] = Hp_percent_change;
        
        if (cameraZoom)
        {
            if (effector1.CompareTag("Player") && PotionseffectingCamera == 0)
            {

                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VirtualCameraManager>().setIsZoomed(false); // this sets all the base values for the camera
                /*
                initzoom = mainCam.m_Lens.OrthographicSize; // gets the initial zoom value
                ZoomCam.Priority = -1; // it will hide it
                ZoomCam.m_Lens.OrthographicSize = initzoom; // setting the zoom camera to have the same zoom value as the main cam

                mainCam.Priority = 1; // this will show */
            }
            
        }
        // make sure that the main cam is the one which visible
        

    }

   

    private bool effect() // call to trigger effects of the potion
    {
        if (effectType) // this variable is from the base class and determines if the potion has a negative or a positive effect
        {
            return change_values(false, -melee_percent[0], +range_percent[0], -Hp_percent[0], +ZoomChangePercent, +baseProjectileCharge[0]);

        }
        else
        {
            return change_values(false, +melee_percent[0], -range_percent[0], +Hp_percent[0], -ZoomChangePercent, -baseProjectileCharge[0]);
        }
    }
    public override bool Triggermaking() // it is the method which is called to start the effect of the potion
    {
        if (effect())
        {
            IsInEffect = true;

            timerInstance = FuncTimer.Create(reset, timer_len, Potion_type.snipe_range.ToString());

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
    private bool change_values(bool reset_potion, float melee_percent_change = 0f, float range_percent_change = 0f, float Hp_percent_change = 0f, float zoomChangePercent = 0f, float baseProjectoleCharge = 0f) // changes health by change if possible returns true else false
    {
        valcon = effector.GetComponent<value_control>();
        healthinst = effector.GetComponent<Health_manager>();
        if (effector.GetComponent<value_control>() != null && effector.GetComponent<Health_manager>() != null)
        {
            if (!reset_potion) // if it is not called as a reset
            {
                if(effector.CompareTag("Player")) // if the character who uses the potion is the player
                {
                    if (PotionseffectingCamera == 0 && cameraZoom) // if this is the first potion that is changing the camera
                    {
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VirtualCameraManager>().setIsZoomed(true);

                        /* ZoomCam.m_Lens.OrthographicSize = initzoom + (initzoom * zoomChangePercent) / 100; // add the change to the zoom
                         ZoomCam.Priority = 2; // make the camera appear as an active Cam
                         */
                        PotionseffectingCamera++; // one potion that needs to effect the camera 
                    }
                    else if(cameraZoom)
                    {
                        PotionseffectingCamera++; // one potion that needs to effect the camera
                    }
                    baseProjectileCharge[1] = effector.GetComponent<value_control>().GetBaseProjectileCharge();
                    effector.GetComponent<value_control>().SetBaseProjectileCharge(baseProjectileCharge[1] + (baseProjectileCharge[0] * baseProjectileCharge[1])/100); // adds it in percent
                }
                else if(effector.CompareTag("Enemy"))
                {
                    if (PotionseffectingRadius == 0 && cameraZoom)
                    {
                        effector.GetComponent<ENEMYPATH>().detectionRadius += 3;
                        PotionseffectingRadius++;
                    }
                    else if (cameraZoom)
                    {
                        PotionseffectingRadius++;
                    }
                }
                
                if (valcon.meleeAttacker)
                {
                    
                    valcon.SetMeleeValue(1 + (melee_percent_change / 100));
                    melee_percent[1] = valcon.GetMeleeValue().Count - 1;// this gets the index of the 
                }
                if (valcon.projectileAttacker)
                {
                    
                    valcon.SetProjetileValue( 1 + (range_percent_change / 100)); // changes the projectile damage value
                    range_percent[1] = valcon.GetProjectileValue().Count - 1;
                }
                Hp_percent[1] = healthinst.Get_HP();
                healthinst.HP_increase((Hp_percent_change / 100) * healthinst.Get_HP()); // changes the Hp of the player by default it shows the healthbar increasing in size visually
            }
            else // this is called when the timer is run down so that the values are changed back to their original state
            {
                if (effector.CompareTag("Player")) // if the character who uses the potion is the player
                { 
                    if (cameraZoom && PotionseffectingCamera == 1)
                    {
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VirtualCameraManager>().setIsZoomed(false); // this sets all the base values for the camera
                        /*
                        mainCam.Priority = 1;
                        ZoomCam.Priority = -1; // hides*/
                        PotionseffectingCamera--;
                    }
                    else if(cameraZoom)
                    PotionseffectingCamera--;


                    if (PotionsINUse > 1)
                        effector.GetComponent<value_control>().SetBaseProjectileCharge(baseProjectileCharge[1]);
                    else
                        effector.GetComponent<value_control>().SetBaseProjectileCharge(baseProjectileRange); // this is the static variable default
                }
                else if (effector.CompareTag("Enemy"))
                {
                    if (cameraZoom && PotionseffectingRadius == 1)
                    {
                        effector.GetComponent<ENEMYPATH>().detectionRadius -= 3;
                        PotionseffectingRadius--;
                    }
                    else if(cameraZoom)
                    {
                        PotionseffectingRadius--;
                    }
                }
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
