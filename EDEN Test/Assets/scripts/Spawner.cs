using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Spawner : MonoBehaviour
{
    
    public float spawnTime;        // The amount of time between each spawn.
    public float spawnDelay;       // The amount of time before spawning starts.
    public GameObject enemy;        // the gameobject that you want to clone 

    public int maxDistance;
    Transform target;
    Transform myTransform;

    void Awake()
    {
        myTransform = transform;

    }

    void Start()
    {
        GameObject stop = GameObject.FindGameObjectWithTag("Player");

        target = stop.transform;

       

        StartCoroutine(SpawnTimeDelay());
    }

    IEnumerator SpawnTimeDelay()
    {
        
     
            while (true)
            {
            if (target != null)
            {

                if (Vector3.Distance(target.position, myTransform.position) < maxDistance)
                {
                    Instantiate(enemy, transform.position, Quaternion.identity);
                    enemy.GetComponent<shooting_projectiles>().setTarget(target);
                    enemy.GetComponent<ENEMYPATH>().setTarget(target);
                    yield return new WaitForSeconds(spawnTime);
                }

                if (Vector3.Distance(target.position, myTransform.position) > maxDistance)
                {
                    yield return null;
                }

            }
            yield return null;

            
            }
        
    }
    public Transform getTarget()
    {
        return target;
    }

    public void SetTarget(Transform n)
    {
        target = n;
    }
}
