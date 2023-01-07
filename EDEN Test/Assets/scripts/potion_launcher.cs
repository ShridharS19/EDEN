using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion_launcher : MonoBehaviour
{
    public GameObject potion_projectile;
    public Transform shootpoint;
    public GameObject potion_settings;
    public float force_mag = 1f;
    private Rigidbody2D projectile_new_object_rb;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()  // launches the potions when space is pressed
    {
        if (Input.GetKeyDown("space"))
        {

          if(potion_settings.GetComponent<PotionLauncherSettings>().isNotCustomPotion()) {

            if (potion_settings.GetComponent<PotionLauncherSettings>().isCurrentActive()) // if the potion is active
            {
                int potion_id = potion_settings.GetComponent<PotionLauncherSettings>().getCurrentPotion();
                GameObject projectile_potion_new = Instantiate(potion_projectile, shootpoint.position, Quaternion.identity); // creates a new game object of type potion projectile and at the shootpoint

                projectile_potion_new.GetComponent<potion_attributes>().setItemIndex(potion_id);


                projectile_new_object_rb = projectile_potion_new.GetComponent<Rigidbody2D>(); // retreiving the rigid body of the projectile obj
                potion_settings.GetComponent<PotionLauncherSettings>().LaunchCurrentPotion();

                projectile_new_object_rb.AddForce(shootpoint.up * force_mag, ForceMode2D.Impulse);// providing a force to the object for it to move


            }

          } else {
            if (potion_settings.GetComponent<PotionLauncherSettings>().isCurrentActive()) // if the potion is active
            {
                PotionStorage ps = potion_settings.GetComponent<PotionLauncherSettings>().getPotionStorage();
                GameObject cp = potion_settings.GetComponent<PotionLauncherSettings>().getCustomPotionDisplay();

                int potion_id = potion_settings.GetComponent<PotionLauncherSettings>().getCurrentPotion();
                GameObject projectile_potion_new = Instantiate(potion_projectile, shootpoint.position, Quaternion.identity); // creates a new game object of type potion projectile and at the shootpoint

                cp.transform.parent = projectile_potion_new.transform;
                cp.transform.localPosition = new Vector3(0, 0, 0);
                cp.transform.localScale    = new Vector3(1, 1, 1);

                cp.GetComponent<potionColourSetter>().SetArrayOfStats(ps.getStats());

                projectile_potion_new.GetComponent<potion_attributes>().setItemIndex(potion_id, cp, ps);


                projectile_new_object_rb = projectile_potion_new.GetComponent<Rigidbody2D>(); // retreiving the rigid body of the projectile obj
                potion_settings.GetComponent<PotionLauncherSettings>().LaunchCurrentPotion();

                projectile_new_object_rb.AddForce(shootpoint.up * force_mag, ForceMode2D.Impulse);// providing a force to the object for it to move


            }
          }




        }
    }
}
