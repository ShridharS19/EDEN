using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashWithBlock : MonoBehaviour
{
    private bool hadsword = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "block")
        {
            swordcombat s = new swordcombat();
            if (gameObject.GetComponent<swordcombat>() != null)
            {
                hadsword = false;
                s = gameObject.AddComponent(typeof(swordcombat)) as swordcombat;
                
            }
            else 
            {

                s = gameObject.GetComponent<swordcombat>();
            }

            s.AI_sword();
           
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "block")
        {
            if (!hadsword) 
            {
               swordcombat s= gameObject.GetComponent<swordcombat>();
                Destroy(s);
            }
        } 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
