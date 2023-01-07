using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createDummy : MonoBehaviour

{
    private GameObject Inventory;
    public GameObject DummyPrefab; // the prefab that we need to copy
    private bool DummyInGame;
    private GameObject dummy;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = GameObject.Find("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        if (!DummyInGame)
        {
            if (Input.GetKeyDown(KeyCode.M)) // if M is pressed this is to test needs to be connected to the inventory later
            {
                dummy = Instantiate(DummyPrefab, gameObject.transform.position, Quaternion.identity); // creates the dummy
                
                SetStateOfComponents(false); 
                DummyInGame = true;
            }
        }
    }

    public void SetStateOfComponents(bool state)
    {
        /* foreach(Transform i in gameObject.GetComponentsInChildren<Transform>()) // all the players children
         {
             i.gameObject.SetActive(state); // sets the state of all the players children

         }*/

        //GetComponent<swordcombat>().enabled = state;
        //GetComponent<shooting_projectiles>().enabled = state;
        //GetComponent<>
        
        GameObject.Find("EnemyMaster").GetComponent<enemyMaster>().changeTargetOfEnemies(dummy); // directs all the enemies towards the dummy
        Inventory.SetActive(state);
        if (state == false)
        {
            GameObject.Find("EnemyMaster").GetComponent<enemyMaster>().changeTargetOfEnemies(dummy);

            GameObject.FindWithTag("MainCamera").GetComponent<cameraTargeControl>().SetTarget(dummy);
        }
        else
        {
            GameObject.Find("EnemyMaster").GetComponent<enemyMaster>().changeTargetOfEnemies(gameObject);
            if (gameObject.GetComponent<RoomTrackerPlayer>().GetPresentRoom() != GameObject.Find("dummyPlayer(Clone)").GetComponent<RoomTrackerPlayer>().GetPresentRoom())
            {
                GameObject.FindWithTag("MainCamera").GetComponent<VirtualCameraManager>().ChangeRoom(gameObject.GetComponent<RoomTrackerPlayer>().GetPresentRoom());
                Debug.Log("Switch room done");
            }
            GameObject.FindWithTag("MainCamera").GetComponent<cameraTargeControl>().SetTarget(gameObject);
        }

    }
    public void SetDummyInGame(bool state)
    {
        DummyInGame = state;                 
    }
}
