using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMagBlock : MonoBehaviour
{
    private int blockHealth = 120;
    private bool MagBroken = false;

    public void blockDamage(int damage)
    {
        blockHealth -= damage;
        if (blockHealth <= 0)
        {
            MagBroken = true;
            GameObject.Find("player").GetComponent<DetectBlocks>().AddTests(2);
            Destroy(gameObject);
        }
    }
    public bool AreMagnetsBroken()
    {
        return MagBroken;
    }
}
