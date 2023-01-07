using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyMaster : MonoBehaviour

{
    private ArrayList enemys;
    // Start is called before the first frame update
    void Start()
    {
        enemys = FilterHealthEnemies(GameObject.FindGameObjectsWithTag("Enemy"));
        setAllEnemyTargets(enemys, GameObject.Find("player"));
        GameObject.Find("player").GetComponent<Health_manager>().Ondeathofobject += EnemyMaster_Ondeathofobject; // subscribe to the event of player death
    }

    private void EnemyMaster_Ondeathofobject(object sender, GameObject e)
    {
        changeTargetOfEnemies(null); // this handles for player death for enemies
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private ArrayList FilterHealthEnemies(GameObject[] array)
    {
        
        ArrayList output = new ArrayList();
        foreach(GameObject i in array)
        {
            if(i.GetComponent<Health_manager>() != null || i.GetComponent<Spawner>() != null) // if you can do damage to this entity or it is a spawner then it is an enemy
            {
                output.Add(i); // add it to the array list because it is an actual enemy and not one of its children gamobjects
            }
        }
        return output;
    }

    private void setAllEnemyTargets(ArrayList enemies, GameObject target)
    {
        Transform targetTransform;
        if (target != null)
            targetTransform = target.transform;
        else
            targetTransform = null;
        foreach(GameObject i  in enemies)
        {
            
            
            if(i.GetComponent<Health_manager>() == null) // then it is a spawner
            {
                if (i.GetComponent<Spawner>() != null)
                {
                    
                    i.GetComponent<Spawner>().SetTarget(targetTransform);
                }
                else
                {
                    Debug.LogError("there is some problem with the enemies fileter method in the enemy master"); // flags an error
                    // this should ideally never happen just put for handling
                }
                
            }
            else if (i.GetComponent<value_control>().projectileAttacker) // if it is a projectile enemy
            {
                i.GetComponent<shooting_projectiles>().setTarget(targetTransform); // change the shooting target
                i.GetComponent<ENEMYPATH>().setTarget(targetTransform);
            }
            else if(i.GetComponent<value_control>().potionAttacker) // if enemy can shoot potions
            {
                Debug.Log("TODO");
                // needs to be implemented for potion enemies
            }
            else if(i.GetComponent<value_control>().meleeAttacker)
            {
                i.GetComponent<ENEMYPATH>().setTarget(targetTransform);
            }
            
            
            
            // for sword enemies they just need to get to the player then they start attacking automatically
        }
    }
    public void changeTargetOfEnemies(GameObject target)
    {
        enemys = FilterHealthEnemies(GameObject.FindGameObjectsWithTag("Enemy")); // this is done so that if any additional enemies were spawned between the start metyhod and this call then it includes them
        setAllEnemyTargets(enemys, target); // this changes the targets of all the filteres enemies in the given array
    }
}
