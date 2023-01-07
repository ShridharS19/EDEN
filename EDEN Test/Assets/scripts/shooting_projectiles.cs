using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting_projectiles : MonoBehaviour


{
    public Transform target; // this needs to change into private after testing
    // Start is called before the first frame update
    public GameObject projectile;
    public Transform shootpoint_object;
    public GameObject arrowammo_display_UI;
    public float force_mag = 1.0f;
    public float charge_time_set;
    private int projectile_damage = 20;
    public bool isAI;
    private ArrayList multipliers = new ArrayList(); // this will be set by the value function for potion effects
    private float baseShotCharge = 0.3f;
    private ArrowInventory instance;

    private int start_ammo = 10; // defualt value just for testing

    void Start()
    {
        if(gameObject.CompareTag("Enemy")) // this automatically checks if it is an enemy and transfers control to the AI method if it is
        {
            isAI = true;
        }
        if (isAI)
        {
            StartCoroutine(onCoroutine());
        }
        if (!isAI)
        {
            Debug.Log("entered" + gameObject.name);
            instance = arrowammo_display_UI.GetComponent<ArrowInventory>();
            instance.setArrows(start_ammo); // set the arrows to the initial value as controlled by this class
        }
        
        
      
    }
    IEnumerator onCoroutine()
    {
        
        while (true)
        {
            if (target != null)
            {
                if ((shootpoint_object.position - target.position).magnitude <= 10)
                {

                    AI_shooting();

                }
            }
                yield return new WaitForSeconds(5f);
            
            
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!isAI)
        {
            if (instance.playerHasArrows()) // if there is a positive amount of ammo left
            {

                if (Input.GetKeyDown("space"))
                {

                    charge_time_set = Time.time; // if stores the time since the game began

                }
                if (Input.GetKeyUp("space"))
                {

                    charge_time_set = Time.time - charge_time_set; // this gives the difference in the time between the press and release of the space

                    GameObject projectile_new_object = Instantiate(projectile, shootpoint_object.position, Quaternion.identity); // creates a new game object of type projectile and at the shootpoint
                    projectile_new_object.GetComponent<collisiondestroy>().setshooter(gameObject);
                    
                    charge_time_set += baseShotCharge;
                    if (charge_time_set < 3f)
                        projectile_new_object.GetComponent<collisiondestroy>().SetChargeTime(charge_time_set); // pass the time that the space was pressed to the collsiondestry script so it knows when to destry the object
                    else
                        projectile_new_object.GetComponent<collisiondestroy>().SetChargeTime(3); // so that the projectile does not go on forever
                    projectile_new_object.GetComponent<collisiondestroy>().set_damage(projectile_damage); // set the amount that the projectile will damage


                    charge_time_set = 0f; // reset value
                    Rigidbody2D projectile_new_object_rb = projectile_new_object.GetComponent<Rigidbody2D>(); // retreiving the rigid body of the projectile obj
                    projectile_new_object_rb.AddForce(shootpoint_object.up * force_mag, ForceMode2D.Impulse);// providing a force to the object for it to move
                    
                    
                        instance.incrementArrows(-1);

                    
                    
                    
                   

                }
            }
            else
            {
                // any display for no ammo remaining
            }
        }
       
        
        
    }
    
    private void TP()
    {
       
    }
    public void Set_damageVal(int value)
    {
        projectile_damage = value;
    }

    public float getBaseCharge() // to set the distance (in seconds before destroy) a projectile travles if the space bar is tapped instantaniously
    {
        return baseShotCharge;
    }

    public void setBaseCharge(float n)// to set the distance (in seconds before destroy) a projectile travles if the space bar is tapped instantaniously
    {
        baseShotCharge = n;
    }
    public int Get_damageVal()
    {
        return projectile_damage;
    }

    public int Get_ammoLeft()
    {
        return instance.getArrows();
    }
    public void add_ammo(int n) // adds n to the ammo if it is a negative value it will subtract
    {
      
        instance.incrementArrows(n);
       
        // updates the inventory displaying the ammo left
    }

    public void setTarget(Transform n)
    {
        target = n;
    }

    public void setMultiplier(float n) // for potions it takes out all the multipliers that have a value 1 since they make no difference this allows more than one potion uses at the same time
    {
        if (multipliers != null)
        {
            while (multipliers.IndexOf(1) != -1) // while 1 is still in the multipliers
            {
                multipliers.Remove(1f);
            }
        }
        multipliers.Add(n);
    }

    public void setMultiplier(float n, int index)
    {
        multipliers[index] = n; 
    }

    public ArrayList GetMultipliers() // this is got when the projectiles collides to inflict damage
    {
        return multipliers;
    }
    

    private void AI_shooting()
    {
            
        charge_time_set = 5; // preset time

        if (target != null) // this is done when the player dies so stop shooting
        {
            GameObject projectile_new_object = Instantiate(projectile, shootpoint_object.position, Quaternion.identity); // creates a new game object of type projectile and at the shootpoint
            projectile_new_object.GetComponent<collisiondestroy>().setshooter(gameObject); // passes the present enemy in the enemyWho shot it so that the projectile is aware of the enemy is belongs to

            projectile_new_object.GetComponent<collisiondestroy>().SetChargeTime(charge_time_set); // pass the time that the space was pressed to the collsiondestry script so it knows when to destry the object
            projectile_new_object.GetComponent<collisiondestroy>().set_damage(projectile_damage); // set the amount that the projectile will damage
            var direction = target.position - shootpoint_object.position;

            charge_time_set = 0f; // reset value
            Rigidbody2D projectile_new_object_rb = projectile_new_object.GetComponent<Rigidbody2D>(); // retreiving the rigid body of the projectile obj
            projectile_new_object_rb.AddForce(direction * force_mag, ForceMode2D.Impulse);// providing a force to the object for it to move
        }
        
    }
    // todo shridhar
}

