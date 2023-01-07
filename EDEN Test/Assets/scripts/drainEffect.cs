using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class drainEffect
// this will last for ten seconds
{
    private GameObject[] drainedFrom;
    private GameObject drainedTo;
    private float AmountDrainedperTimerPeriodPerEntity;
    private GameObject closeEffect;
    private GameObject effect;
    private GameObject[] effectInstant;
    private float share;
    private Collider2D[] enemy;
    private MonobehaviourLinkDrainage instance;


    public drainEffect(GameObject[] drainedFrom, GameObject drainedTo, float AmountDrainedTotal)
    {

        if (drainedFrom.Length == 0)
        {
            Debug.Log("effect nobody");

        }
        else
        {
            effectInstant = new GameObject[drainedFrom.Length]; // there are same number of effects as the drained froms
            this.drainedFrom = drainedFrom;
            this.drainedTo = drainedTo;
            
            this.AmountDrainedperTimerPeriodPerEntity = AmountDrainedTotal / (20 * drainedFrom.Length);

            if (GameObject.Find("player") != null)
            {
                effect = GameObject.Find("player").GetComponent<ActiveOrbs>().Grasseffect;
                closeEffect = GameObject.Find("player").GetComponent<ActiveOrbs>().GrassCloseEffect;
            }
            else
                Debug.Log("the player is null");
        }

    }
    

    
    public void Trigger()
    {
        if (drainedFrom == null)
        {

        }
        
        else
        {
            for (int i = 0; i < drainedFrom.Length; i++)
            {
                if (drainedFrom[i] != null)
                {

                    effectInstant[i] = Object.Instantiate(effect, drainedFrom[i].transform);
                    effectInstant[i].transform.localPosition = new Vector3(0.5f, -0.01f, 0f); // just so that it is created the near the feet of the enemy
                }
            }


            GameObject monoLink = new GameObject("drainMonobehaviourLink", typeof(MonobehaviourLinkDrainage));

            monoLink.GetComponent<MonobehaviourLinkDrainage>().InitiateDrain(drainedFrom, drainedTo, AmountDrainedperTimerPeriodPerEntity, effectInstant, closeEffect);
        }
        

    }


    public bool exclude(GameObject i)
    {
        return instance.exclude(i);

    }






}

public class MonobehaviourLinkDrainage : MonoBehaviour

{
    private GameObject[] drainedFrom;
    private GameObject closingEffect;
    private GameObject[] effectsinstant;
    private float AmountDrainedperTimerPeriodPerEntity;
    private GameObject drainedTo;
    private bool startDrain;
    private void Update()
    {
  
        if(startDrain)
        {
    
            StartCoroutine("drainerCont");          
            startDrain = false;
        }
    }

    public void InitiateDrain(GameObject[] drainedFrom, GameObject drainedTo, float AmountDrainedperTimerPeriodPerEntity, GameObject[] effectInstant, GameObject closingEffect)
    {
        this.effectsinstant = effectInstant;
        this.closingEffect = closingEffect;
        this.drainedFrom = drainedFrom;
        this.drainedTo = drainedTo;
        this.AmountDrainedperTimerPeriodPerEntity = AmountDrainedperTimerPeriodPerEntity;
        startDrain = true;
    }

    IEnumerator drainerCont()
    {
        

        for (int i = 0; i < 20; i++)
        {
            foreach (GameObject obj in drainedFrom)
            {
                if (obj != null)
                {
                    obj.GetComponent<ENEMYPATH>().setTarget(null);
                    drain(obj, drainedTo, AmountDrainedperTimerPeriodPerEntity);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
        
        for(int a = 0; a < effectsinstant.Length; a++) // go through all the effects
        {
            Destroy(effectsinstant[a]);
            effectsinstant[a] = Instantiate(closingEffect);
            
            Destroy(effectsinstant[a]);
            
        }
        yield return new WaitForSeconds(1f); // to show the entire animation
        for (int a = 0; a < effectsinstant.Length; a++) // go through all the effects and destroy them
        {
            
            Destroy(effectsinstant[a]);
            
        }
        Destroy(gameObject);
        






    }

    public bool exclude(GameObject i)
    {
        for (int l = 0; l < drainedFrom.Length; l++)
        {
            if (drainedFrom[l] != null && drainedFrom[l] == i)
            {
                drainedFrom[l] = null;
                return true;
            }
        }
        return false;
    }

    public void drain(GameObject from, GameObject to, float amount) // takes the amount of health from from and gives it to to
    {
        if (to.GetComponent<Health_manager>()==null && from.GetComponent<Health_manager>() != null) 
        {
            from.GetComponent<Health_manager>().reduce_health(amount);
        } 
        if (from.GetComponent<Health_manager>() != null && to.GetComponent<Health_manager>()) // if both to and from have a health system
        {
     
            to.GetComponent<Health_manager>().add_health(amount);
            from.GetComponent<Health_manager>().reduce_health(amount);
        }
        else
        {
            Debug.Log("SORRY CANNOT BE DRAINED");
        }
    }
}
