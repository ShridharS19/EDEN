using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBlocks : MonoBehaviour
{
    private int blockHealth = 120;


    public void blockDamage(int damage)
    {
        blockHealth -= damage;
        if (blockHealth <= 0)
        {
            GameObject.Find("player").GetComponent<DetectBlocks>().SetTests(2);
            Destroy(gameObject);
        }
    }
}
