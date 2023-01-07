using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisibiltyPotion : potion

{
    Potion_type Name = Potion_type.invisibility;
    public invisibiltyPotion(int timerLen) : base(true, GameObject.Find("player"), timerLen)
    {

    }
    public override Potion_type GetName() // getter method
    {
        return Name;
    }

    public override bool Triggermaking()
    {
        timerInstance = FuncTimer.Create(reset, timer_len, Name.ToString()); // starting a timer
        
        GameObject.Find("EnemyMaster").GetComponent<enemyMaster>().changeTargetOfEnemies(null);
        effector.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f); // make it a little invisible so it looks cool
        return true;
        // effectively hides the player from the enemies
    }

    public void reset()
    {
        if(GameObject.Find("dummyPlayer(Clone)")) // if there is a dummy in the scene 
        GameObject.Find("EnemyMaster").GetComponent<enemyMaster>().changeTargetOfEnemies(GameObject.Find("dummyPlayer(Clone)"));
        else
            GameObject.Find("EnemyMaster").GetComponent<enemyMaster>().changeTargetOfEnemies(effector);

        effector.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f); // resets the color back to original
    }

}
