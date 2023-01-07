using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// it just tracks which area the player is at 
public class RoomTrackerPlayer : MonoBehaviour
{
    private RoomInWorld PresentRoom;
    private bool once = true;
    public void switchArea(RoomInWorld changedRoom)
    {

        PresentRoom = changedRoom;
    }


    private void Update()
    {
        if (once)
        {
            refreshRoom();
            once = false;
        }
    }
    public void refreshRoom()
    {
        
        PresentRoom = WorldRoomDatabase.GetRoom(GameObject.FindWithTag("MainCamera").GetComponent<VirtualCameraManager>().baseCam);
        Debug.Log(PresentRoom.baseCam.name);
    }

    public RoomInWorld GetPresentRoom()
    {
        
        return PresentRoom;
    }
}
    
        

    
