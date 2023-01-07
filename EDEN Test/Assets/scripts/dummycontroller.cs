using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummycontroller : MonoBehaviour

{
    private float TimerLength = 30f; // by defualt the timer is 30 seconds
    private GameObject player; // stores the player
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Health_manager>().Ondeathofobject += Dummycontroller_Ondeathofobject;
        player = GameObject.Find("player"); // find the player
        
        player.SetActive(false);// disables the player
        FuncTimer.Create(SwitchBackToPlayer, TimerLength, "Dummy");
    }

    private void Dummycontroller_Ondeathofobject(object sender, GameObject e)
    {
        SwitchBackToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Escape)) // if hits the escape key
        {
           
            SwitchBackToPlayer(); 
        }
    }

   
    public float GetTimerLen()
    {
        return TimerLength; 
    }

    private void SwitchBackToPlayer()
    {
        // it changes camera and activavtes the player gameobject
        FuncTimer.stopTimer("Dummy"); // stops the timer
        player.SetActive(true);
        
        player.GetComponent<createDummy>().SetDummyInGame(false); // tells the player that the dummy is deactivated
        player.GetComponent<createDummy>().SetStateOfComponents(true);
        Destroy(gameObject); // destroy the dummy
    }

    
}
