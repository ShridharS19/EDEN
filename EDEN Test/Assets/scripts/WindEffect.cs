using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    private GameObject origin;
    public GameObject OrbInventory;
    private int radius;
    Collider2D[] projectile;
    private Vector2[] velSign;
    private bool[] activeOrbs;
    int dir;
    // Start is called before the first frame update
    public WindEffect(GameObject origin,int radius,bool isOpposite) 
    {
        if (isOpposite)
        {
            dir = 1;
        }
        else 
        {
            dir = -1;
        }
        this.origin = origin;
        this.radius=radius;
        activeOrbs = OrbInventory.GetComponent<HideOrbs>().active;
    }
    void Start()
    {

        projectile = Physics2D.OverlapCircleAll(origin.transform.position, radius);
        int len = 0;
        for (int i = 0; i < projectile.Length; i++)
        {

            if (projectile[i].gameObject.tag == "projectile")
            {
                Rigidbody2D rb = projectile[i].gameObject.GetComponent<Rigidbody2D>(); // retreiving the rigid body of the projectile obj
                var sign = rb.velocity;
                velSign[len] = sign;
                len++;
            }
        } 
    
    }


    // Update is called once per frame
    void Update()
    {
        projectile = Physics2D.OverlapCircleAll(origin.transform.position, radius);
        Debug.Log("WE WIN THESE");
        if (activeOrbs[3]) {
            int len = 0;
            foreach (Collider2D a in projectile)
            {
               if (Vector2.Distance(a.gameObject.transform.position,origin.transform.position)<=1) 
                {
                    Rigidbody2D rb = a.gameObject.GetComponent<Rigidbody2D>(); // retreiving the rigid body of the projectile obj
                    float dist = Vector2.Distance(origin.transform.position, a.gameObject.transform.position);
                    var Power = rb.velocity * 1 / ((dist+1) * (dist+1));
                    rb.AddForce(Power * Time.deltaTime);
                }
               else if (a.gameObject.tag == "projectile")
                {
                    Rigidbody2D rb = a.gameObject.GetComponent<Rigidbody2D>(); // retreiving the rigid body of the projectile obj
                    var opposite = dir * velSign[len];
                    len++;
                    float dist = Vector2.Distance(origin.transform.position, a.gameObject.transform.position);
                    var Power = opposite.normalized * 1 / ((dist) * (dist));
                    rb.AddForce(Power * Time.deltaTime);
                    //rb.AddForce(shootpoint_object.up * force_mag, ForceMode2D.Impulse);// providing a force to the object for it to move

                }
                
            }
        }
    }
}
