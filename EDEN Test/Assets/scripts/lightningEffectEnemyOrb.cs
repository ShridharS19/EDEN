using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningEffectEnemyOrb

{
    private GameObject effect;
    private GameObject InstanceOfEffect;
    private float attackVar;
    private GameObject effector;
    private float healthReduce;
    private bool radiationEffect;
    public static int chain = 2;
    private Collider2D[] enemy;
    FuncTimer timer;
    public lightningEffectEnemyOrb(GameObject effector, float healthReduce, bool radiationEffect, float attackVar = 1f)
    {
        this.healthReduce = healthReduce;
        this.attackVar = attackVar;
        this.effector = effector;
        this.radiationEffect = radiationEffect;
        if (GameObject.Find("player") != null)
        {

            if (!radiationEffect)
                effect = GameObject.Find("player").GetComponent<ActiveOrbs>().Lightningeffect; // gets the lightning effect Gameobject
            else
            {
                effect = GameObject.Find("player").GetComponent<ActiveOrbs>().LightningeffectRadiating;
                Debug.Log("radiation");
            }
            GameObject.Find("player").GetComponent<Health_manager>().Ondeathofobject += LightningEffectEnemyOrb_Ondeathofobject;
            


        }
        else
        {
            Debug.Log("sorry the player is null");
        }

    }

    

    private void LightningEffectEnemyOrb_Ondeathofobject(object sender, GameObject e)
    {
        if (InstanceOfEffect != null) // if the effect is still visible
        {
            Object.Destroy(InstanceOfEffect); // if the enemy dies then remove the visuals
            if (timer != null) // if the timer is not already null
                FuncTimer.stopTimer(timer); // stop the timer cause the enemy is dead
        }
    }

    public void Trigger()
    {
        if (!radiationEffect)
        {
            effector.transform.parent.gameObject.GetComponent<Health_manager>().reduce_health(healthReduce, attackVar);
            InstanceOfEffect = Object.Instantiate(effect, effector.transform); // this will create a object which is a child of the effector
            InstanceOfEffect.transform.localPosition = new Vector3(0.5f, -0.01f, 0f);
            InstanceOfEffect.transform.localScale = new Vector3(1f, 0.5f, 0f);
        }
        else
        {
            if (effector == null)
            {
                Debug.Log("it is null and there is a problem");
            }
            else
            {
                enemy = Physics2D.OverlapCircleAll(effector.transform.position, 4, LayerMask.GetMask("enemy_layer"));// stores the collider for the enemy that enteres into the circle
                if (enemy != null) // if some enemy has collided
                {

                    foreach (Collider2D i in enemy)
                    {
                        
                        if (i.gameObject.GetComponent<Health_manager>() != null && i.gameObject != effector.transform.parent.gameObject) // if it can take health ( to test that it is not a projectile of something
                        {

                            lightningEffectEnemyOrb LightningeffectLocal = new lightningEffectEnemyOrb(i.gameObject.GetComponentInChildren<projectile_collision>().gameObject, 10, false); // pass the collider of the enemy gameobject cause all the for loops do gamobject.parent
                            LightningeffectLocal.Trigger();
                        }


                    }
                }
                InstanceOfEffect = Object.Instantiate(effect, effector.transform);
                InstanceOfEffect.transform.localPosition = new Vector3(0.5f, -0.01f, 0f);
                InstanceOfEffect.transform.localScale = new Vector3(1f, 1f, 0f);
            }
        }

        
        if (!radiationEffect)
            timer = FuncTimer.Create(DestroyEffect, 0.3f);
        else
            timer = FuncTimer.Create(DestroyEffect, 0.6f); // this might need to change depending on how long the radiation effect lasts for
    }

    private void DestroyEffect()
    {

        if (InstanceOfEffect != null)
        {
            Object.Destroy(InstanceOfEffect); // if the enemy dies then remove the visuals
        }
        if(!radiationEffect && chain > 0)
        {
            chain--;
            if (effector != null)
            {
                
                lightningEffectEnemyOrb effectrad = new lightningEffectEnemyOrb(effector, -1, true); // this will cause a radiation lightning effect
                effectrad.Trigger();
            }
            

        }
    }

    

    

}
