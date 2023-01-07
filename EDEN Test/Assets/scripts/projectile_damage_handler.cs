using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_damage_handler : MonoBehaviour
{




    private void Start()
    {
        Debug.Log(gameObject.name);
    }
    // below was the non path finding code for enemy AI it just lerps towards the player
    /* void Update()
     {
         Follow();
     }
     IEnumerator Follow()
     {
         Vector3 startPos = transform.position;
         yield return new WaitForSeconds(1f); //  pauses for 1 second
         Vector3 finalPos = transform.position;

                 //Vector3 velocity = Vector3.Lerp(transform.position, player.GetComponent<Transform>().position, speed);
         //transform.position = velocity; // it draws a line between the
         // two points and follows that line with the given speed
         if (startPos.x == finalPos.x || startPos.y == finalPos.y
             || startPos.z == finalPos.z)
         {
             Vector3 velocity = Vector3.Lerp(transform.position, player.GetComponent<Transform>().position, speed);
             transform.position = velocity;
             current_velocity = velocity;
         }
         //current_velocity = velocity;
     }*/
    private void OnCollisionEnter2D(Collision2D collision)  // if there is a collision
    {
        if (collision.gameObject.name == "projectile_placeholder(Clone)") // check if the thing which collided was a projectile
        {

            this.GetComponent<Health_manager>().GetHealthBarObject().SetActive(true); // makes the gameObject visible


            this.GetComponent<Health_manager>().reduce_health(collision.gameObject.GetComponent<collisiondestroy>().get_damage());// minuses the projectile damage from the enemy





        }





    }
}