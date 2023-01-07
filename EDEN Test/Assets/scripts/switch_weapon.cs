using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_weapon : MonoBehaviour
{
    // the order of switch will be sword -> projectile -> launcher
    // starts with the defualt = sword

    public GameObject weapon_inv;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
          if((weapon_inv.GetComponent(typeof(ManageWeapons)) as ManageWeapons).getActiveWeapon() == 1)
            {
                //switchenablesword();
                //switchenableprojectile();
                gameObject.GetComponent<swordcombat>().enabled = true;
                gameObject.GetComponent<shooting_projectiles>().enabled = false;
                gameObject.GetComponent<potion_launcher>().enabled = false;

            }
          else if((weapon_inv.GetComponent(typeof(ManageWeapons)) as ManageWeapons).getActiveWeapon() == 2)
            {
                //switchenableprojectile();
                //switchenablelauncher();
                gameObject.GetComponent<swordcombat>().enabled = false;
                gameObject.GetComponent<shooting_projectiles>().enabled = true;
                gameObject.GetComponent<potion_launcher>().enabled = false;
            }
          else if((weapon_inv.GetComponent(typeof(ManageWeapons)) as ManageWeapons).getActiveWeapon() == 3)
            {
                //switchenablelauncher();
                //switchenablesword();
                gameObject.GetComponent<swordcombat>().enabled = false;
                gameObject.GetComponent<shooting_projectiles>().enabled = false;
                gameObject.GetComponent<potion_launcher>().enabled = true;
            }
            else
            {
                Debug.Log("-1");
            }
    }

    /*public void switchenablesword()
    {
        gameObject.GetComponent<swordcombat>().enabled = !gameObject.GetComponent<swordcombat>().isActiveAndEnabled;
    }
    public void switchenableprojectile()
    {
        gameObject.GetComponent<shooting_projectiles>().enabled = !gameObject.GetComponent<shooting_projectiles>().isActiveAndEnabled;
    }

    public void switchenablelauncher()
    {
        gameObject.GetComponent<potion_launcher>().enabled = !gameObject.GetComponent<potion_launcher>().isActiveAndEnabled;
    }*/


    // the indexes are 1-> sword, 2 -> projectile , 3-> launcher, if none then -1
    /*public int Get_enabled()
    {
        if (gameObject.GetComponent<swordcombat>().isActiveAndEnabled)
            return 1;
        if (gameObject.GetComponent<shooting_projectiles>().isActiveAndEnabled)
            return 2;
        if (gameObject.GetComponent<potion_launcher>().isActiveAndEnabled)
            return 3;

        return -1;
    }*/



}
