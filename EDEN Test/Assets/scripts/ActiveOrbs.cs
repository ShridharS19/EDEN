using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ActiveOrbs : MonoBehaviour

{
    /*
0 -> Grass
1 -> Lightning
2 -> Fire
3 -> Wind
4 -> Earth
     * 
     *
     */
    public GameObject FireEffect;
    public GameObject OrbInventory;
    public float time;
    public float timer;
    public float cooldownTime;
    public GameObject GrassCloseEffect;
    private bool[] activeOrbs;
    public GameObject Lightningeffect;
    public GameObject Groundeffect;
    public GameObject WindEffect;
    public GameObject Grasseffect;
    private drainEffect drainage;
    private lightningEffectEnemyOrb lightning;
    private FuncTimer grassOrbTimer = null;
    bool[] isCoolingDown=new bool[5];
    float[] timers = { 10, 10, 10, 10, 10 };
    float[] cooldown ={0, 0, 0, 0, 0};
    public GameObject LightningeffectRadiating;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 4; i++)
        {
            isCoolingDown[i] = false;
        }
        time = 10;
        cooldownTime = 10;
        OrbInventory.GetComponent<HideOrbs>().OnOrbPressed += ActiveOrbs_OnOrbPressed;
        activeOrbs = OrbInventory.GetComponent<HideOrbs>().active;
    }
    void Update()
    { 
        for (int i = 0; i <= 4; i++)
        {   
            if (activeOrbs[i] && timers[i]==time) 
            {
                UseOrb(i);
                isCoolingDown[i] = false;
                cooldown[i] = cooldownTime;
            }
            if (activeOrbs[i] && timers[i] <= 0)
            {
                Disable(i);
                timers[i] = time;
                isCoolingDown[i] = true;
            }
            
            if(activeOrbs[i] && timers[i]>0)
            {
                timers[i] -= Time.deltaTime;
                
            }
            if (isCoolingDown[i] && cooldown[i] > 0)
            {
                cooldown[i] -= Time.deltaTime;

            }
        }

        }
    private void Disable(int i)
    {
        OrbInventory.GetComponent<HideOrbs>().active[i] = false;
    }
   

    private void ActiveOrbs_OnOrbPressed(object sender, GameObject e)
    {
       activeOrbs = OrbInventory.GetComponent<HideOrbs>().active;
    }
    
    public bool[] getActiveOrbs()
    {
        return activeOrbs;
    }
    private void UseOrb(int i) 
    {
        switch (i) 
        {
            case 0:
                GrassOrb();
                break;
            case 1:
                LightningOrb();
                break;
            case 2:
                FireOrb();
                break;
            case 3:
                WindOrb();
                break;
            case 4:
                GroundOrb();
                break;
        }
    }
    private void GrassOrb()
    {


        if (activeOrbs[0]) // if the grass orb is active
        {
            float prob = (float)(0.9 / (1 + 5*Math.Pow(Math.Log10(cooldown[0]), 3)));
            float check = UnityEngine.Random.Range(0, 1);

            if (check <= prob)
            {
                Collider2D[] enemy = Physics2D.OverlapCircleAll(gameObject.transform.position, 4, LayerMask.GetMask("enemy_layer"));
                GameObject[] enemiesGO = new GameObject[enemy.Length];
                if (enemy != null) // if some enemy has collided
                {

                    for (int i = 0; i < enemy.Length; i++)
                    {

                        if (enemy[i].gameObject.GetComponent<Health_manager>() != null)
                        {
                            enemiesGO[i] = enemy[i].gameObject;

                        }


                    }
                }
                else
                {
                    Debug.Log("nobody");
                }

                drainage = new drainEffect(enemiesGO, gameObject, 100);
                drainage.Trigger();
            }
            else 
            {
                GameObject[] pp = { gameObject };
                drainage = new drainEffect(pp,null,100);
            }
           }
        }
    private void LightningOrb()
    {


        if (activeOrbs[1]) // if the grass orb is active
        {
            float prob = (float)(0.9 / (1 + 5 * Math.Pow(Math.Log10(cooldown[1]), 3)));
            float check = UnityEngine.Random.Range(0, 1);

            if (check <= prob)
            {
                
            }
            else
            {
                //lightningEffectEnemyOrb.chain = 2; // this resets the  number of chains because we want it to reset every shot
                lightningEffectEnemyOrb effectrad = new lightningEffectEnemyOrb(gameObject, -1, false); // this will cause a radiation lightning effect
                effectrad.Trigger();
            }
        }
    }
    private void FireOrb()
    {


        if (activeOrbs[2]) // if the grass orb is active
        {
            float prob = (float)(0.9 / (1 + 5 * Math.Pow(Math.Log10(cooldown[2]), 3)));
            float check = UnityEngine.Random.Range(0, 1);

            if (check <= prob)
            {
            }
            else
            {
                regeneration_health burnEffect = new regeneration_health(gameObject.transform.parent.gameObject, -1f, 0.5f, 10f);
                burnEffect.Trigger();
            }
        }
    }
    private void WindOrb()
    {


        if (activeOrbs[3]) // if the grass orb is active
        {
            float prob = (float)(0.9 / (1 + 5 * Math.Pow(Math.Log10(cooldown[3]), 3)));
            float check = UnityEngine.Random.Range(0, 1);

            if (check <= prob)
            {
                Debug.Log("WIND");
                WindEffect impulse = new WindEffect(gameObject,10,false);
            }
            else
            {
                WindEffect impulse = new WindEffect(gameObject, 10, true);
            }
        }
    }
    private void GroundOrb()
    {


        if (activeOrbs[4]) // if the grass orb is active
        {
            float prob = (float)(0.9 / (1 + 5 * Math.Pow(Math.Log10(cooldown[4]), 3)));
            float check = UnityEngine.Random.Range(0, 1);

            if (check <= prob)
            {
                Collider2D[] enemy = Physics2D.OverlapCircleAll(gameObject.transform.position, 4, LayerMask.GetMask("enemy_layer"));
                GameObject[] enemiesGO = new GameObject[enemy.Length];
                if (enemy != null) // if some enemy has collided
                {

                    for (int i = 0; i < enemy.Length; i++)
                    {

                        if (enemy[i].gameObject.GetComponent<Health_manager>() != null)
                        {
                            enemiesGO[i] = enemy[i].gameObject;

                        }


                    }
                }
                else
                {
                    Debug.Log("nobody");
                }

                drainage = new drainEffect(enemiesGO, gameObject, 100);
                drainage.Trigger();
            }
            else
            {

            }
        }
    }
}
    

