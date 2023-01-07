using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *this is a abstract class which all potions extend from
 * enum Potion_type holds the types of potions in the game
 * effectType: if the effect of the potion is positive (true) or negative (false)
 * Trigger is the class which when called the effect of the potion is triggered
 * Get_name returns the potion name from the enum types
 * timer_len = length of time the potion is effective for -1 if it is permanant
 * IsInEffect = checkes if the potion is in effect at some point in the game
 * the class handles for an effect where if the two potions do the exact same effect then just the time doubles
 * author arnav
 *
 */
[Serializable]
public abstract class potion

{
    public int timer_len;
    public FuncTimer timerInstance; // stores the timer from the FuncTimer class
    public bool hastimer = true;
    public bool IsInEffect;

    public static float BaseHP; // by default 0 to handle more than one potions
    public static float BaseSpeed; // by default 0 to handle more than one potions
    public static float BaseDefence; // to handle more than one potions
    public static int PotionseffectingCamera; // to handle the zoom in zoom out for multiple potions only for the player
    public static float baseProjectileRange;
    public static int PotionseffectingRadius;
    public static int PotionsINUse; // by default = 0
    public GameObject effector; // the gameobject which the potion effects
    public enum Potion_type // types of potions
    {
        health,
        tank_defence,
        snipe_range,
        melee_speed,
        invisibility,


    }

    public bool effectType; // false if we want the negative effect of the potion

    public potion(bool effectType, GameObject effector, int timer_Len)
    {
        this.effectType = effectType;
        this.effector = effector;
        timer_len = timer_Len;

    }

    public abstract bool Triggermaking(); // call the function to trigger the effect of the potion
    public abstract Potion_type GetName();


    public void Trigger()
    {
        effector.GetComponent<Health_manager>().Ondeathofobject += Potion_Ondeathofobject;
        if (PotionsINUse == 0)// if it is the first potion
        {
            BaseSpeed = effector.GetComponent<value_control>().GetSpeed();
            BaseHP = effector.GetComponent<Health_manager>().Get_HP();
            BaseDefence = effector.GetComponent<Health_manager>().getDefenceVar();
            baseProjectileRange = effector.GetComponent<value_control>().GetBaseProjectileCharge();
            PotionseffectingCamera = 0;
            PotionseffectingRadius = 0;
        }
        if (hastimer) // if it is a timed potion
        {

            if (FuncTimer.secondsLeftForTimer(GetName().ToString()) <= -.01f) // if the timer is not there at all
            {
                Debug.Log("TIMER LEFT " + FuncTimer.secondsLeftForTimer(GetName().ToString()));
                PotionsINUse++;
                Triggermaking();

            }
            else
            {
                if (!FuncTimer.addSeconds(GetName().ToString(), timer_len))// in case the time it took to read this loop caused the timer to stop running
                {
                    PotionsINUse++;
                    Triggermaking();

                }


            }
        }
        else
        {
            // we do not do a potionsInUse++ since that is only designed to handle timed potions
            Triggermaking();
        }
    }

    private void Potion_Ondeathofobject(object sender, GameObject e)
    {
        if(hastimer) // if it is a timer potion
        {
            if (timerInstance != null) // if the effector has died and the timer is still on
            {
                FuncTimer.stopTimer(timerInstance); // it will stop the timer
            }
        }
    }

    public void Trigger(GameObject eff)
    {

        this.effector = eff;
        effector.GetComponent<Health_manager>().Ondeathofobject += Potion_Ondeathofobject;

        if (PotionsINUse == 0)// if it is the first potion
        {
            BaseSpeed = effector.GetComponent<value_control>().GetSpeed();
            BaseHP = effector.GetComponent<Health_manager>().Get_HP();
            BaseDefence = effector.GetComponent<Health_manager>().getDefenceVar();
            baseProjectileRange = effector.GetComponent<value_control>().GetBaseProjectileCharge();
            PotionseffectingCamera = 0;
            PotionseffectingRadius = 0;
        }
        if (hastimer) // if it is a timed potion
        {

            if (FuncTimer.secondsLeftForTimer(GetName().ToString()) <= -.01f) // if the timer is not there at all
            {
                Debug.Log("TIMER LEFT " + FuncTimer.secondsLeftForTimer(GetName().ToString()));
                PotionsINUse++;
                Triggermaking();

            }
            else
            {
                if (!FuncTimer.addSeconds(GetName().ToString(), timer_len))// in case the time it took to read this loop caused the timer to stop running
                {
                    PotionsINUse++;
                    Triggermaking();

                }


            }
        }
        else
        {
            // we do not do a potionsInUse++ since that is only designed to handle timed potions
            Triggermaking();
        }
    }

    public void EndedTimer()
    {
        PotionsINUse--;
    }


}
