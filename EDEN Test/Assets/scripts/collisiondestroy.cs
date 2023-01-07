using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisiondestroy : MonoBehaviour

{
    public float max_charge_shoot = 1.0f;
    
    public float charge_time; // it is passed to this script by the shooting_projectiles script when the shot is executed by the player
    public int percent_Enemydamage;
    private bool callonce = true;
    
    

    
    private GameObject shooter;
    void Update()
    {
        
        StartCoroutine(destroydelay(charge_time));
        if (callonce) // this makes sure it is only called once
        {


            
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), shooter.GetComponent<Collider2D>()); // ignore collision between this gameobjecct and the enemy it was launched from
            
            callonce = false;
        }

    }
    IEnumerator destroydelay(float timeToWait) // this function delays before destoying a game object
    {
        yield return new WaitForSeconds(timeToWait);
        Destroy(gameObject); // destroy this game object

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != gameObject.name ) // so that projectiles do not collide with each other and the destroy when the projectile collides with a player/enemy is handled in their own scripts
        {
            if(shooter.CompareTag(collision.gameObject.tag) && shooter != collision.gameObject) // if both the thing the projectile collided with and the shooter are of the same tag but not the same object
            Destroy(gameObject);

            else if(collision.gameObject.CompareTag("Enemy"))
            {

            }
            else if(collision.gameObject.CompareTag("Player"))
            {

            }
            else
            {
                Debug.Log("IT IS DESTROYIN");
                Destroy(gameObject);
            }
        }
        // in the settings i have made it ignore collisions with the player layer so anything which the projectiles can pass through needs to be on player layer
        // for the enemy projectiles we use another prefab
         // destroys game object(deleting it from the scene)

    }
    

    public void set_damage(int value)
    {
        percent_Enemydamage = value;
    }

    public int get_damage()
    {
        return percent_Enemydamage;
    }

    public void SetChargeTime(float value)
    {
        charge_time = value;
    }
    public float GetChargeTime()
    {
        return charge_time;
    }

    public void setshooter(GameObject shooter)
    {
        this.shooter = shooter;
    }

    public GameObject getshooter()
    {
        return shooter;
    }

    
    
}
