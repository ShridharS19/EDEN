using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * increases the defence variable and the HP
 * assuming the defence var is only effected by armours (this class and no two armours can be used simultaneously)
 * handled for potions effects for HP increase
 */
public class armour

{
    private GameObject effector; // the object the armour will effect
    private float[] DefenceVarIncreasePercent = new float[2]; // the amount the defence variable of the entity changes by armour
    private float[] HPIncreasePercent = new float[2]; // the amount of HP increase by armour the first[0] is a percent the second is the health it needs to reset back to
    armour(GameObject effector, int HPIncreasePercent, int DefenceVarIncreasePercent) 
    {
        this.effector = effector;
        this.HPIncreasePercent[0] = HPIncreasePercent;
        this.DefenceVarIncreasePercent[0] = DefenceVarIncreasePercent;
    }

    void Equip() // to equip
    {
        if (potion.PotionsINUse >= 1)
        {
            HPIncreasePercent[1] = potion.BaseHP; // incase a potion is in use then take the base as the revert effect
            DefenceVarIncreasePercent[1] = potion.BaseDefence;
        }
        else
        {
            HPIncreasePercent[1] = effector.GetComponent<Health_manager>().Get_HP();
            DefenceVarIncreasePercent[1] = effector.GetComponent<Health_manager>().getDefenceVar();

        }
        effector.GetComponent<Health_manager>().HP_increase(HPIncreasePercent[1] + (HPIncreasePercent[0]*HPIncreasePercent[1])/100);
        effector.GetComponent<Health_manager>().SetDefencVar(DefenceVarIncreasePercent[1] + (DefenceVarIncreasePercent[1] * DefenceVarIncreasePercent[0]) / 100);
        potion.BaseHP = effector.GetComponent<Health_manager>().Get_HP(); // so that all the potions revert their effect to this base
        potion.BaseDefence = effector.GetComponent<Health_manager>().getDefenceVar();
        
        
    }

    void unequip() // to unequip
    {
        if(potion.PotionsINUse >= 1)
        {
            potion.BaseHP = HPIncreasePercent[1];
            potion.BaseDefence = DefenceVarIncreasePercent[1];
        }
        else
        {
            effector.GetComponent<Health_manager>().HP_increase(HPIncreasePercent[1] - effector.GetComponent<Health_manager>().Get_HP());
            effector.GetComponent<Health_manager>().SetDefencVar(DefenceVarIncreasePercent[1]);
        }
        potion.BaseHP = effector.GetComponent<Health_manager>().Get_HP(); // so that potions all revert their effects to the new base without the armour effect
        potion.BaseDefence = effector.GetComponent<Health_manager>().getDefenceVar();

    }
}
