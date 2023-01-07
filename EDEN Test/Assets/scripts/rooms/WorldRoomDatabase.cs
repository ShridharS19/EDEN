using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class WorldRoomDatabase 

{
    
    public enum world {
        plains_new,
        plains_old,
        desert_new,
        desert_old,
        rainforest_new,
        rainforest_old,
        rockNMountain_new,
        rockNMountain_old,
        EDEN

    }
	public static RoomInWorld[] array; // stores all the rooms in the world

    
    // each room in a world is defined by its camera
    // each room will have its own virtual camera

    public static void populateArray(RoomInWorld[] arrayI)
    {
        array = arrayI;

    }
    public static RoomInWorld GetRoom(CinemachineVirtualCamera baseCamera) // returns null if no room related to that camera and world
    {

        foreach(RoomInWorld i in array)
        {
            if (i.baseCam == baseCamera)
            {
                
                return i;
            }
        }
        return null;
    }



    // a return of false will mean that the room is already been added to the array
    

    
        
}
