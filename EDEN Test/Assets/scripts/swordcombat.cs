using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordcombat : MonoBehaviour

{
    public Transform sword_range;
    public float attack_radius;
    public LayerMask enemy_layer;
    
    private float time_btw_attack_counter;
    public float time_btw_attack;
    public int sword_damage;
    private bool pause;
    private ArrayList multipliers = new ArrayList();

    public bool IsAI;
    
    // Start is called before the first frame update
    void Start()
    {
        
        /*  if (IsAI)
          {
              StartCoroutine(onCoroutine());
          } */
        if (gameObject.CompareTag("Enemy"))
        {
            IsAI = true;
            GetComponent<Health_manager>().Ondeathofobject += Swordcombat_Ondeathofobject;
        }

        else if (gameObject.CompareTag("Player"))
        {
            IsAI = false;
        }

    }

    private void Swordcombat_Ondeathofobject(object sender, GameObject e)
    {
        FuncTimer.stopTimer("AIsword"); // stop all the sword attack timers
    }



    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            if (IsAI)
            {


                pause = true;
                FuncTimer.Create(delay_ai_sword, time_btw_attack, "AIsword");


            }
        }

        if (!IsAI)
        {



            if (Input.GetKeyDown(KeyCode.Space)) // if space button is pressed
            {
                Collider2D enemy = Physics2D.OverlapCircle(sword_range.position, attack_radius, enemy_layer);// stores the collider for the enemy that enteres into the circle
                if (enemy != null) // if there is a enemy that was present in the circle
                {

                    if (enemy.gameObject.CompareTag("Enemy") || enemy.gameObject.CompareTag("block"))
                    {
                        if (enemy.GetComponent<Health_manager>() == null) //handles for if it collides with the collider of the enemy
                        {

                            enemy.GetComponentInParent<Health_manager>().reduce_health(sword_damage, gameObject.GetComponent<Health_manager>().getMyAttackVar(), multipliers);
                        }
                        else
                        {
                            enemy.GetComponent<Health_manager>().reduce_health(sword_damage, gameObject.GetComponent<Health_manager>().getMyAttackVar(), multipliers); // damage the enemy
                        }

                    }
                }

            }
        }




    }
    public void setDamageAmount(int value) // sets the amount damage each attack does
    {
        sword_damage = value;
    }
    public int getDamageAmount() // gets the value of how much damage each attack does
    {
        return sword_damage;
    }
    
    private void OnDrawGizmosSelected() // draws the circle in the scene good for testing
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sword_range.position, attack_radius);
    }

    
    private void delay_ai_sword()
    {
        pause = false;
        AI_sword();
    }
    public void AI_sword()
    {


        Collider2D player = Physics2D.OverlapCircle(sword_range.position, attack_radius, enemy_layer);
        if (player != null && (player.CompareTag("Player") || player.gameObject.CompareTag("block"))) // if there is a enemy that was present in the circle
        {
            if (player.GetComponent<Health_manager>() == null) // this means that it has collided with the collider of the player and not the player itself
            {
                player.GetComponentInParent<Health_manager>().reduce_health(sword_damage, gameObject.GetComponent<Health_manager>().getMyAttackVar(), multipliers);
            }
            else
            {

                player.GetComponent<Health_manager>().reduce_health(sword_damage, gameObject.GetComponent<Health_manager>().getMyAttackVar(), multipliers); // damage the enemy

            }

            // to be implemented shridhar
        }
    }

    public void SetMultiplier(float n)
    {
        if (multipliers != null)
        {
            while (multipliers.IndexOf(1) != -1)
            {
                multipliers.Remove(1);
            }
        }
        multipliers.Add(n);
    }
    public void setMultiplier(float n, int index)
    {
        multipliers[index] = n;
    }

    public ArrayList GetMultipliers()
    {
        return multipliers;
    }


        
        
}

