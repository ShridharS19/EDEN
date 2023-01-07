using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable] // this makes the class visible in the inspector if an instant of it is made public
public class RoomInWorld 
{
    public WorldRoomDatabase.world world; // defines the world in which the room is in
    public CinemachineVirtualCamera baseCam;
    public CinemachineVirtualCamera zoomCam;
    public float GlobalLightIntensity;
    public bool InDayLight;

    public RoomInWorld(WorldRoomDatabase.world world, CinemachineVirtualCamera baseCam, CinemachineVirtualCamera zoomCam, float GlobalLightIntensity, bool InDayLight)
    {
        this.world = world;
        this.baseCam = baseCam;
        this.zoomCam = zoomCam;
        this.GlobalLightIntensity = GlobalLightIntensity;
        this.InDayLight = InDayLight;
    }



}
