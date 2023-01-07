using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
/*
 * contains getter and setter methods return true if the action can be done and false(or -1) if the action could not be done
 * all the values for attacks and speed must be changed from this class it has all getters and setters in one place
 * handles for if it is a player or enemy
 */
public class value_control : MonoBehaviour

{
    [System.NonSerialized]
    public bool meleeAttacker = false;[System.NonSerialized]// ig it is a melee attack type
    public  bool projectileAttacker = false;[System.NonSerialized] // if it is a projectile attack type
    public bool potionAttacker = false;// need to still implement
    public bool Isplayer = false;
    public bool IsDummy = false;
    // check all or any two if it can do one of more attacks

    void Start()
    {
        if (gameObject.CompareTag("DummyPlayer"))
        {
            IsDummy = true;
        }

        else
        {
            if (gameObject.CompareTag("Player"))
            {
                Isplayer = true;
            }
            if (GetComponent<shooting_projectiles>() != null)
            {
                projectileAttacker = true;

            }

            if (GetComponent<swordcombat>() != null)
            {
                meleeAttacker = true;
            }

            if (GetComponent<potion_launcher>() != null)
            {
                potionAttacker = true;
            }
        }
        
    }


    public ArrayList GetMeleeValue()
    {
        if (meleeAttacker) return GetComponent<swordcombat>().GetMultipliers();
        else return null; // this should ideally never happen

    }

    public bool SetMeleeValue(float value)
    {
        if (meleeAttacker)
        {
            SetmeleeMult(value);
            return true;
        }
        return false;


    }

    public ArrayList GetProjectileValue()
    {

        if (projectileAttacker) return GetComponent<shooting_projectiles>().GetMultipliers();
        else return null; // this should ideally never happen
    }

    public bool SetProjetileValue(float value)
    {
        if (projectileAttacker)
        {
            SetprojectileMult(value);
            return true;
        }
        return false;
    }
    public float GetSpeed()
    {
        if (gameObject.CompareTag("Player"))
            return GetComponent<movementALL>().GetSpeed();
        
            return gameObject.GetComponent<AIPath>().maxSpeed;
    }
    public void SetSpeed(float value)
    {
        if (gameObject.CompareTag("Player"))
            GetComponent<movementALL>().SetSpeed(value);
        else
            gameObject.GetComponent<AIPath>().maxSpeed = value;
    }

    public int Getammoprojectile()
    {
        if(Isplayer)
        {
            return gameObject.GetComponent<shooting_projectiles>().Get_ammoLeft();
        }
        return -1;
    }

    public bool addammoprojectiles(int n )
    {
        if(Isplayer)
        {
            gameObject.GetComponent<shooting_projectiles>().add_ammo(n);
            return true;
        }
        return false; 
    }

    public Vector3 GetPlayerDir() // returns the player direction in vector form using cartesian unit coordinates 
    {
        if(Isplayer)
        {
           return GetComponent<movementALL>().Turn_dir();

        }
        return Vector3.zero;
    }

    private void SetmeleeMult(float n)
    {
        gameObject.GetComponent<swordcombat>().SetMultiplier(n);
    }

    private void SetprojectileMult(float n)
    {
        gameObject.GetComponent<shooting_projectiles>().setMultiplier(n);
    }

    public void SetmeleeMult(float n, int index) // this should only be used to reset a multiplier
    {
        gameObject.GetComponent<swordcombat>().setMultiplier(n, index);
            

    }

    public void SetprojectileMult(float n, int index) // this should only be used to reset a multiplier
    {
        gameObject.GetComponent<shooting_projectiles>().setMultiplier(n, index);
    }

    // base charge decides how long the projectile goes before self destroying when you press the space key instantaniously
    public float GetBaseProjectileCharge()
    {
        if(Isplayer)
        {
            return GetComponent<shooting_projectiles>().getBaseCharge();
        }
        return -1f; // if it is not a player
    }

    public void SetBaseProjectileCharge(float n)
    {
        if(Isplayer)
        {
            GetComponent<shooting_projectiles>().setBaseCharge(n);
        }
        
    }



}
