using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Portal : MonoBehaviour
{
    private RoomInWorld thisRoom;
    public CinemachineVirtualCamera basecam;
    public Transform teleportTo;
    private bool once = true;
    
    void Update()
    {
        if (once)
        {
            thisRoom = WorldRoomDatabase.GetRoom(basecam); // finds the room
            once = false;
        }


        if (thisRoom == null)
        {
            Debug.Log("there is a problem with one of the portals as the room is null"); // just incase
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) // if the player has collided with the  portal
        {
            if(thisRoom == null)
            {
                Debug.Log("there is something wrong");
            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VirtualCameraManager>().ChangeRoom(thisRoom);
            GameObject.Find("player").transform.position = teleportTo.position; // teleporting the player
            
            GameObject.Find("player").GetComponent<RoomTrackerPlayer>().switchArea(thisRoom);
            GameObject.FindWithTag("MainCamera").GetComponent<cameraTargeControl>().SetTarget(GameObject.Find("player"));

            // switches the room of the player
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VirtualCameraManager>().blackOut(1f);
        }
        else if(collision.gameObject.CompareTag("DummyPlayer"))
        {
            
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VirtualCameraManager>().ChangeRoom(thisRoom);
            GameObject.Find("dummyPlayer(Clone)").transform.position = teleportTo.position; // teleporting the player

            GameObject.Find("dummyPlayer(Clone)").GetComponent<RoomTrackerPlayer>().switchArea(thisRoom);
            GameObject.FindWithTag("MainCamera").GetComponent<cameraTargeControl>().SetTarget(GameObject.Find("dummyPlayer(Clone)"));
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VirtualCameraManager>().blackOut(1f);
        }

    }
}
