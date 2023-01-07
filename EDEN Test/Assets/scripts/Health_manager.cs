using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* any health related action or changes in the health bar must relay through this class
 * health formula:
 * reduce health = (actual value * enemyattack * multiplier(only for potions))/defenceplayer
 * add health = actual value/defenceplayer
 * author: arnav
 */
public class Health_manager : MonoBehaviour
{
    // has attack and defence variables for this gameobject itself

	// this class manages the health of the enemy or any entity
	public GameObject Healthbar_prefab;
	public float y_offset_position; // how high above the centre of the gamobject is the healthbar
	public float x_offset_position; // how much horizontally offset from the centre of the gamobject is the healthbar
	private GameObject healthbar;
    private bool isDummy;

	private bool isPlayer;
	// private bool isdead; // instead of this i used events
	private float MyAttackVar = 1; // the weighted health system with getters and setters
	private float DefenceVar = 1; // the weighted health system with getters and setters

	public GameObject HealthBar_UI;
    
	public event EventHandler<GameObject> Ondeathofobject; // this is an event which is triggered when a object dies
                                                           // Start is called before the first frame update
    

	void Start()
	{
        if(gameObject.CompareTag("DummyPlayer"))
        {
            isDummy = true;
        }
        if (!isDummy)
        {
            healthbar = Instantiate(Healthbar_prefab, new Vector3(transform.position.x + x_offset_position, transform.position.y + y_offset_position, 0), Quaternion.identity); // instatiates the healthbar
            healthbar.transform.SetParent(transform);
        }

		if (gameObject.CompareTag("Player")) // checks if the tag of the gameobject is player
		{
			isPlayer = true;
		}

	}

    public void reduce_health(float health, float attackVar = 1f, ArrayList multipliers = null) // to reduce the health accounts for death of the gameobject
    {
        if (isDummy)
        {
            Debug.Log("entered");
            
            DeathCleanUP();
        }
        else
        {
            float Wreduce = health; // stores the weighted reduce value
            Wreduce = (Wreduce * attackVar) / DefenceVar;
            if (multipliers != null && multipliers.Count != 0)
            {
                foreach (float i in multipliers)
                {
                    Wreduce *= i;
                }
            }



            if (healthbar.GetComponentInChildren<healthBar_control>().GetHealth() - Wreduce <= 0)
            {
                if (isPlayer)
                    HealthBar_UI.GetComponent<HealthBarNumber>().setCurrentHealth(0);
                DeathCleanUP();
                Destroy(gameObject);
            }
            healthbar.GetComponentInChildren<healthBar_control>().setHealth(healthbar.GetComponentInChildren<healthBar_control>().GetHealth() - Wreduce);// use getters and setters to reduce health
            Debug.Log(healthbar.GetComponentInChildren<healthBar_control>().GetHealth());// debug
            if (isPlayer)
                HealthBar_UI.GetComponent<HealthBarNumber>().setCurrentHealth((int)healthbar.GetComponentInChildren<healthBar_control>().GetHealth()); // for the UI display
            healthbar.GetComponentInChildren<healthBar_control>().Refresh();// it calls a method which causes the health bar to display its new health
        }
    
	}


	public void add_health(float health) // handles for if health after adding is over the max limit
	{
        if (!isDummy)
        {

            if (health + GetComponentInChildren<healthBar_control>().GetHealth() <= GetComponentInChildren<healthBar_control>().max_health)
            {
                healthbar.GetComponentInChildren<healthBar_control>().setHealth(healthbar.GetComponentInChildren<healthBar_control>().GetHealth()
                    + health); // adds the health using the setter method
                if (isPlayer)
                    HealthBar_UI.GetComponent<HealthBarNumber>().setCurrentHealth((int)healthbar.GetComponentInChildren<healthBar_control>().GetHealth()); // health display UI
            }
            else
            {

                healthbar.GetComponentInChildren<healthBar_control>().setHealth(healthbar.GetComponentInChildren<healthBar_control>().max_health); // adds the health making it full
                if (isPlayer)
                {



                    HealthBar_UI.GetComponent<HealthBarNumber>().setCurrentHealth((int)healthbar.GetComponentInChildren<healthBar_control>().max_health);// health display UI
                }
            }
            healthbar.GetComponentInChildren<healthBar_control>().Refresh();// it calls a method which causes the health bar to display its new health
        }
	}

	public void HP_increase(float value, bool update_look = true) //increases the HP of the current gameobject
	{
        if (!isDummy)
        {
            healthbar.GetComponentInChildren<healthBar_control>().IncreaseHP(value, update_look); // updates the healthbar with the new Hp
            if (isPlayer)
                HealthBar_UI.GetComponent<HealthBarNumber>().setMaxHealth((int)healthbar.GetComponentInChildren<healthBar_control>().getHp()); // updates the UI with the new HP
        }

	}

    public float getCurrentHealth()
    {
        if (!isDummy)
        {
            return GetComponentInChildren<healthBar_control>().GetHealth();
        }
        return -1;
    }

		public void setCurrentHealth(float health) {
        if (!isDummy)
        {
            healthbar.GetComponentInChildren<healthBar_control>().setHealth(health);
            if (isPlayer)
                HealthBar_UI.GetComponent<HealthBarNumber>().setCurrentHealth((int)health);
        }
        
        
		}

	public float Get_HP() // gets how much Hp the current gameObject possesses
	{
        if (!isDummy)
        {
            return healthbar.GetComponentInChildren<healthBar_control>().getHp();
        }
        else
        return -1;
	}

	//Sets the HP of the current gameObject
	public void Set_HP(float HP) {
        if (!isDummy)
        {
            healthbar.GetComponentInChildren<healthBar_control>().setHp(HP);
            if (isPlayer)
                HealthBar_UI.GetComponent<HealthBarNumber>().setMaxHealth((int)HP);
        }
            


    }

    public GameObject GetHealthBarObject() // returns the health bar object for other classes in order to set the healthbar active
	{
        if(!isDummy)
		return healthbar;
        return null; // if it is a dummy
	}

	public void DeathCleanUP()
	{
		// to handle for death just subscribe the methods that you want called to this event
		Ondeathofobject?.Invoke(this, gameObject); // the  ? handles for if the event is null : has no subscribers
	}

	public float getMyAttackVar()
	{
        if (!isDummy)
        {
            return MyAttackVar;
        }
        return -1;
	}

	public float getDefenceVar()
	{
        if (!isDummy)
        {
            return DefenceVar;
        }
        return -1;
	}

	public void SetMyAttackVar(float n)
	{
        if (!isDummy)
        {
            MyAttackVar = n;
        }
        
	}

	public void SetDefencVar(float n)
	{
        if (!isDummy)
        {
            DefenceVar = n;
        }
	}








}
