using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.TerrainAPI;
//using System.Media;
using UnityEngine;
public class SandBlockDevice : MonoBehaviour
{
    public Transform sword_range;
    public float attack_radius = 1;
    public LayerMask enemy_layer;

    private int damage = 20;
    public bool puzzlestart = false;
    private bool DeviceOwned = false;
    private int rocks;
    private int breaks=12;
    public int tests = 10;
    public float seconds = 3; // seconds before the dialogue disappears


    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<swordcombat>().attack_radius = 0.5f; // CHANGE THIS BACK
    }


    // Update is called once per frame
    void Update()
    {
        if (puzzlestart)
        {
            if (Input.GetKeyDown(KeyCode.X)) // if space button is pressed
            {
                Debug.Log("enetered loop 1");
                Collider2D enemy = Physics2D.OverlapCircle(sword_range.position, 0.25f, enemy_layer);// stores the collider for the enemy that enteres into the circle
                if (enemy != null && breaks > 0) // if there is a enemy that was present in the circle
                {
                    enemy.GetComponent<SandBank>().blockDamage(damage);
                

               /*     if (enemy.gameObject.CompareTag("MagBlock"))
                    {
                        enemy.GetComponent<SandBank>().blockDamage(damage); // Break block
                    }

                    if (enemy.gameObject.CompareTag("ArmourBlock"))
                    {
                        enemy.GetComponent<SandBank>().blockDamage(damage); // Break block
                    }*/
                }
            }

            if (DeviceOwned)
            {
                if (Input.GetKeyDown(KeyCode.Y)) // T is pressed
                {
                    if (tests > 0)
                    {
                        Collider2D enemy = Physics2D.OverlapCircle(sword_range.position, attack_radius, enemy_layer);// stores the collider for the enemy that enteres into the circle

                        if (enemy != null) // if there is a enemy that was present in the circle
                        {
                            rocks = enemy.GetComponent<SandBank>().howManyRocks();
                            Dialogue speech = new Dialogue("Im reading " + rocks + " rocks in a row", "Cernard");
                            Dialogue[] convo = new Dialogue[1];
                            convo[0] = speech;
                            GetComponent<dialogue_trigger>().inputdialogue(convo);
                            GetComponent<dialogue_trigger>().StartDialogue();
                            FuncTimer.Create(endDialogue, seconds, "dialogueSandProg"); // waits for seconds before the dialogue is hidden 

                        }
                    }
                    else
                        DeviceOwned = false;
                }
                tests--;
            }
        }
    }

    private void endDialogue()
    {
        
        GetComponent<dialogue_trigger>().EndDialogue();
    }
    public void setDamageAmount(int value) // sets the amount damage each attack does
    {
        damage = value;
    }
    public int getDamageAmount() // gets the value of how much damage each attack does
    {
        return damage;
    }
    public void setbreaks(int value) // sets the amount damage each attack does
    {
        breaks += value;
    }
    public int getbreaks() // gets the value of how much damage each attack does
    {
        return breaks;
    }
    public void settests(int value) // sets the amount damage each attack does
    {
        tests += value;
    }
    public int gettests() // gets the value of how much damage each attack does
    {
        return tests;
    }
    public bool getDevice()
    {
        return DeviceOwned;
    }
    public void setDevice(bool set)
    {
        DeviceOwned = set;
    }

}