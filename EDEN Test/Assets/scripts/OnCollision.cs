using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour

{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collided");
        // the enemy and the player collisions are handles from their respecitve colliders

        if(collision.gameObject.CompareTag("projectile")) // if it is a projectile
        {
            GetComponent<blockAttributes>().SetDamageOnCollision(GetComponent<blockAttributes>().GetDamageOnCollision() - collision.gameObject.GetComponent<collisiondestroy>().get_damage()); // reduces the damage that the box can do by the projectile damage
        }
        else if (collision.gameObject.CompareTag("block"))
        {
            GetComponent<blockAttributes>().SetIsTriggered(false);
        }

        else if (!collision.gameObject.CompareTag("Player") && GetComponent<blockAttributes>().GetIsTriggered() ) // if it is not a player and the block is triggered
        {
            
            GetComponent<blockAttributes>().Target.gameObject.GetComponent<buildblocks>().getUIBlockInstance().decreasePlaced(); // to control the display on the UI
            Destroy(gameObject); // if it collides with anything else then it is destroyed
        }

        
    }
    
}
