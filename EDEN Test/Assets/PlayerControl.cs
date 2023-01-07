using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject weapon_inv;
    public GameObject Collider; // this will prevent the player from being damaged
    
    

    public void disablecontrol()
    {
        GetComponent<movementALL>().enabled = false;
        
        GetComponent<swordcombat>().enabled = false;
        GetComponent<shooting_projectiles>().enabled = false;
        Collider.SetActive(false);
        

    }
    public void enablecontrol()
    {
        GetComponent<movementALL>().enabled = true;
        if ((weapon_inv.GetComponent(typeof(ManageWeapons)) as ManageWeapons).getActiveWeapon() == 1)
        {
            //switchenablesword();
            //switchenableprojectile();
            gameObject.GetComponent<swordcombat>().enabled = true;
            gameObject.GetComponent<shooting_projectiles>().enabled = false;
            gameObject.GetComponent<potion_launcher>().enabled = false;

        }
        else if ((weapon_inv.GetComponent(typeof(ManageWeapons)) as ManageWeapons).getActiveWeapon() == 2)
        {
            //switchenableprojectile();
            //switchenablelauncher();
            gameObject.GetComponent<swordcombat>().enabled = false;
            gameObject.GetComponent<shooting_projectiles>().enabled = true;
            gameObject.GetComponent<potion_launcher>().enabled = false;
        }
        else if ((weapon_inv.GetComponent(typeof(ManageWeapons)) as ManageWeapons).getActiveWeapon() == 3)
        {
            //switchenablelauncher();
            //switchenablesword();
            gameObject.GetComponent<swordcombat>().enabled = false;
            gameObject.GetComponent<shooting_projectiles>().enabled = false;
            gameObject.GetComponent<potion_launcher>().enabled = true;
        }
        Collider.SetActive(true);
        
    }
}
