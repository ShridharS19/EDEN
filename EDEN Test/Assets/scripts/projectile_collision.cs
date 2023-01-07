using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_collision : MonoBehaviour
{
    public bool isenemy;
    public bool  isBlock;
    private bool dummyPlayer;
    
    
    
    
    // Start is called before the first frame update

    void Start()
    {
        
        
        if (gameObject.CompareTag("Enemy"))
        {
            isenemy = true;
        }
        if(gameObject.CompareTag("DummyPlayer")) // if it is a dummy player
        {
            dummyPlayer = true;
           
        }
    }
    private void Update()
    {
        
       
    }


    private void OnTriggerEnter2D(Collider2D collision) // i used a trigger so that seems like the player/enemy has only a collidable at his feet and hence the rest of its body can pass pass over things simulating normal movement
    {
        Debug.Log("DESTROYIN");
        if (isBlock)
        {
            if (collision.gameObject.name == "projectile_placeholder(Clone)") // if it is a projectile which has collided
            {

                projectileTakeDamageOnCollision(collision);
            }
            if (collision.gameObject.name == "Enemy_projectile_placeholder(Clone)") // if it is a projectile which has collided
            {
                projectileTakeDamageOnCollision(collision);

            }

        }
        else {
            if (isenemy)
            {
                if (collision.gameObject.name == "projectile_placeholder(Clone)") // if it is a projectile which has collided
                {
                    if (collision.gameObject.GetComponent<collisiondestroy>().getshooter().GetComponent<ActiveOrbs>() != null) // if the entity has a active orb component
                    {
                        
                        if (collision.gameObject.GetComponent<collisiondestroy>().getshooter().GetComponent<ActiveOrbs>().getActiveOrbs()[2]) // if the fire orb is active
                        { // this is the fireorb effect
                            
                            regeneration_health burnEffect = new regeneration_health(gameObject.transform.parent.gameObject, -1f, 0.5f, 10f); 
                            burnEffect.Trigger();
                        }
                        if(collision.gameObject.GetComponent<collisiondestroy>().getshooter().GetComponent<ActiveOrbs>().getActiveOrbs()[1]) // if the lighting orb is active
                        {
                            lightningEffectEnemyOrb.chain = 2; // this resets the  number of chains because we want it to reset every shot
                            lightningEffectEnemyOrb effectrad = new lightningEffectEnemyOrb(gameObject, -1, true); // this will cause a radiation lightning effect
                            effectrad.Trigger();
                              
                        }
                        
                    }
                    projectileTakeDamageOnCollision(collision);
                }
                if (collision.gameObject.GetComponent<blockAttributes>() != null) // if the collided object is a block and it is triggered
                {

                    if (collision.gameObject.GetComponent<blockAttributes>().GetIsTriggered())
                    {
                        blockAttributes block = collision.gameObject.GetComponent<blockAttributes>();
                        GetComponentInParent<Health_manager>().reduce_health(block.GetDamageOnCollision(), block.getBuilder().GetComponent<Health_manager>().getMyAttackVar(), block.getBuilder().GetComponent<buildblocks>().Get_multiplier());// do damage to the enemy
                        Destroy(collision.gameObject); // destroy the block
                    }


                }
                if (collision.gameObject.CompareTag("potion_projectile")) // if it is a launched potion
                {
                    collision.gameObject.GetComponent<Oncollision_potion>().Trigger(); // triggers the effect of the potion
                }



            }

            else { // if it is a player that the projectile has collided with

                if (collision.gameObject.name == "Enemy_projectile_placeholder(Clone)") // if it is a projectile which has collided
                {
                    projectileTakeDamageOnCollision(collision);
                    
                    



                }
                if (collision.gameObject.CompareTag("block")) // if the thing that enters the radius is a block
                {
                    collision.gameObject.GetComponent<blockAttributes>().SetIsTriggered(false); // deactivate the trigger
                    
                }
            }
        }

    }

    private void projectileTakeDamageOnCollision(Collider2D collision)
    {
        if (dummyPlayer)
        {
            GetComponentInParent<Health_manager>().reduce_health(collision.gameObject.GetComponent<collisiondestroy>().percent_Enemydamage); // take damage the amount doesnt matter cause dummy should just self destroy when damage is taken
            Destroy(collision.gameObject); // destroy the projectile
        }
        else
        {
            //Destroy(collision.gameObject);
            //Debug.Log(); 
            GetComponentInParent<Health_manager>().GetHealthBarObject().SetActive(true); // makes the gameObject visible
            GameObject EntityWhoShot = collision.gameObject.GetComponent<collisiondestroy>().getshooter(); // whoever shot this projectile


            Destroy(collision.gameObject); // destroy the projectile
            GetComponentInParent<Health_manager>().reduce_health(collision.gameObject.GetComponent<collisiondestroy>().percent_Enemydamage, EntityWhoShot.GetComponent<Health_manager>().getMyAttackVar(), EntityWhoShot.GetComponent<shooting_projectiles>().GetMultipliers());// minuses the projectile damage from the enemy
                                                                                                                                                                                                                                                                                // gets the multiplier from the shooting projectile script (this is set to handle for projectile damage change by potions) , and takes the attacker from the potion shooter to weigh the damage taken
        }
    }

}
