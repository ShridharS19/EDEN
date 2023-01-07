using System.Collections;
using System.Collections.Generic;
//using System.Media;
using UnityEngine;
public class PickaxeBlockBreaking : MonoBehaviour
{
    public Transform sword_range;
    public float attack_radius = 1;
    public LayerMask enemy_layer;

    private int pickaxe_damage = 20;
    private bool PickaxeOwned = true;
    private bool Magharvest = false;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<swordcombat>().attack_radius = 0.7f; // CHANGE THIS BACK
    }


    // Update is called once per frame
    void Update()
    {
        if (PickaxeOwned)
        {
            if (Input.GetKeyDown(KeyCode.B)) // if space button is pressed
            {
                Collider2D enemy = Physics2D.OverlapCircle(sword_range.position, attack_radius, enemy_layer);// stores the collider for the enemy that enteres into the circle
                if (enemy != null) // if there is a enemy that was present in the circle
                {

                    if (enemy.gameObject.CompareTag("MagBlock"))
                    {
                        enemy.GetComponent<MagnetBlocks>().blockDamage(pickaxe_damage); // Break block
                        Debug.Log("damage done");
                    }

                    if (enemy.gameObject.CompareTag("ArmourBlock"))
                    {
                        enemy.GetComponent<MainMagBlock>().blockDamage(pickaxe_damage); // Break block
                        Debug.Log("Wrong Block");
                    }
                    GameObject.Find("player").GetComponent<DetectBlocks>().AddTests(2);
                }

            }


            if (Input.GetKeyDown(KeyCode.H)) // H is pressed
            {
                Collider2D enemy = Physics2D.OverlapCircle(sword_range.position, attack_radius, enemy_layer);// stores the collider for the enemy that enteres into the circle
                if (enemy != null) // if there is a enemy that was present in the circle
                {

                    if (enemy.gameObject.CompareTag("MagBlock"))
                    {
                        enemy.GetComponent<MagnetBlocks>().blockDamage(pickaxe_damage * 6); // harvest Block
                        Debug.Log("Wrong Harvest");
                    }

                    if (enemy.gameObject.CompareTag("ArmourBlock"))
                    {
                        enemy.GetComponent<MainMagBlock>().blockDamage(pickaxe_damage * 6); // harvvest Block
                        Debug.Log("Magnets obtained");
                        Magharvest = true;
                    }

                }
                PickaxeOwned = false;
            }
        }
    }



    public void setDamageAmount(int value) // sets the amount damage each attack does
    {
        pickaxe_damage = value;
    }
    public int getDamageAmount() // gets the value of how much damage each attack does
    {
        return pickaxe_damage;
    }
    public bool getPickaxe()
    {
        return PickaxeOwned;
    }
    public void setPickaxe(bool set)
    {
        PickaxeOwned = set;
    }
    public bool AreMagnetsHarvested()
    {
        return Magharvest;
    }
    private void OnDrawGizmosSelected() // draws the circle in the scene good for testing
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sword_range.position, attack_radius);
    }     


}