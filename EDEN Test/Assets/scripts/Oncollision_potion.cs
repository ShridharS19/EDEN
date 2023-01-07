using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oncollision_potion : MonoBehaviour
{
    private GameObject Item_Inventory;
    public LayerMask enemy_layer;
    public int potion_radius;

    private void Start()
    {
        Item_Inventory = GameObject.FindGameObjectWithTag("item_UI"); // find gameobject with this tag

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.GetComponent<Health_manager>() == null && !collision.gameObject.CompareTag("healthbar")) // if the thing it collides with is not the player or enemy or healthbar
        {

            // the potion collision for the enemies are managed from their trigger colliders
            Trigger();
        }

    }

    public void Trigger() // called from the player when it detects a collision
    {

        splash();
        Destroy(gameObject);

    }

    private void splash()
    {
        Collider2D[] enemies_hit = Physics2D.OverlapCircleAll(transform.position, potion_radius, enemy_layer);

        foreach(Collider2D i in enemies_hit)
        {
            // the below shit has been deleted since it causes a double effect of the potion if the circle is big enough to cover the enemy itself and its collider hence i changed it so that it only effects when the collider is caught in the circle
            /* if (i.gameObject.GetComponent<Health_manager>() != null && i.gameObject.GetComponent<value_control>() != null) // if it is a enemy that can walk around and take health
             {
                 useItem(i.gameObject);
             }
             */

            if (i.gameObject.GetComponentInParent<Health_manager>() != null && i.gameObject.GetComponentInParent<value_control>() != null && i.gameObject.name != "collider_projectile")
            {
                Debug.Log("EFFECT ON " + i.gameObject.name);
                useItem(i.gameObject.GetComponentInParent<Health_manager>().gameObject); // this passes the parent as the effector
            }

        }
    }
     private void OnDrawGizmosSelected() // draws the circle in the scene good for testing
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, potion_radius);
    }

    private void useItem(GameObject effector)
    {   if(!GetComponent<potion_attributes>().isCustomPotion()) {
        Debug.Log("the object hit was " + effector.name);
        int item_ID = GetComponent<potion_attributes>().GetItemIndex();
        if (item_ID == 1) // red
        {
            melee_speed_potion potion = new melee_speed_potion(effector, true, 50f, 50f, 50f, 20);
            potion.Trigger();
        }
        else if (item_ID == 2) // purple
        {

            Health_potion potion = new Health_potion(effector, true, 50);

            potion.Trigger();
        }
        else if (item_ID == 3)
        {
            tank_potion potion = new tank_potion(effector, true, 50f, 50f, 50f, 50f, 20);
            potion.Trigger();
        }
        else if (item_ID == 4)
        {
            sniper_potion potion = new sniper_potion(effector, true, 50f, 50f, 50f, 10, true, .4f); // the nulls represent the cameras since they arent required for anyone but the player hence i set them to null
            potion.Trigger();
        }
        else if (item_ID == 5)
        {
            invisibiltyPotion potion = new invisibiltyPotion(10);
            potion.Trigger();
        }

      } else {
        custompotion potion = new custompotion(GetComponent<potion_attributes>().getPotionData());
        potion.Trigger(effector);
      }
    }
}
