using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBank : MonoBehaviour
{
    int health = 40;
    public int rocks;
    public void blockDamage(int damage)
    {
        Debug.Log(health);
        Debug.Log(GameObject.Find("player").GetComponent<SandBlockDevice>().getbreaks());
        if (GameObject.Find("player").GetComponent<SandBlockDevice>().getbreaks() > 0)
        {
            health -= damage;
            if (health <= 0)
            {
                GameObject.Find("player").GetComponent<SandBlockDevice>().setbreaks(-1);
                Destroy(gameObject);
            }
        }
    }

    public int howManyRocks()
    {
        return rocks;
    }
}
