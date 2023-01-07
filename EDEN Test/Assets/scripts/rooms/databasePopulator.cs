using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class databasePopulator : MonoBehaviour
{
    //every new room made needs to be added to this
    
    public RoomInWorld[] AllRooms; // it has all the room information
    // Start is called before the first frame update
    void Start()
    {
        WorldRoomDatabase.populateArray(AllRooms);
    }
    

    
}
