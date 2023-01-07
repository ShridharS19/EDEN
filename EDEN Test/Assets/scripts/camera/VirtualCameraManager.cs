using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;

public class VirtualCameraManager : MonoBehaviour

{
    private bool isZoomed;
    public CinemachineVirtualCamera BlackOutCam; // this will allow me to put out a blackout effect
    public CinemachineVirtualCamera baseCam;
    public CinemachineVirtualCamera ZoomCam;
    public GameObject DayLight; // stores the volume light for the entire world which changes day and night
    public GameObject subsidiaryGlobalLight; // disabled by defualt used for lighting local to rooms
    // the way the cameras work is that when you leave an area (say a house) the virtual cameras for that area are disabled and the cams for the new area get enabled
    void Start()
    {

        baseCam.Priority = 1; // the base cam is shown
        ZoomCam.Priority = -1; // by default the zoom cam is hidden
        
    }
    


    public void ChangeRoom(CinemachineVirtualCamera baseCam, CinemachineVirtualCamera ZoomCam, bool IsDayLight, float Intensity = 0)
    {
        
        if(IsDayLight) // if it only needs daylight
        {
            subsidiaryGlobalLight.SetActive(false);
            DayLight.SetActive(true);
            
        }
        else
        {
            DayLight.SetActive(false);
            subsidiaryGlobalLight.GetComponent<Light2D>().intensity = Intensity; // changing the intensity of the global light to the desired amount
            subsidiaryGlobalLight.SetActive(true);
        }
        GameObject.FindWithTag("MainCamera").GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 0; // when switching rooms make the switch a cut instead of a gradual thing
   
        this.baseCam.GetComponent<CinemachineVirtualCamera>().Follow = null;
        this.ZoomCam.GetComponent<CinemachineVirtualCamera>().Follow = null;  // so that the cameras stay in their place and dont follow the player
        this.baseCam.gameObject.SetActive(false);
        this.ZoomCam.gameObject.SetActive(false) ; // disables the cameras in the room that the player leaves
        
        this.baseCam = baseCam; // sets the cameras to that of the present room
        this.ZoomCam = ZoomCam;
        baseCam.gameObject.SetActive(true) ;
        ZoomCam.gameObject.SetActive(true) ;

        baseCam.Priority = 1; // the base camera's default priority
        if(isZoomed)
        {
            // sets the zoom value for the new camera
            ZoomCam.Priority = 2; // if zoom is true then make the zoom cam priority more than the base cam
        }
        else
        {
            ZoomCam.Priority = -1; // if it is not zoomed then its priority is less than the base cam
        }

       
    }

    public void ChangeRoom(RoomInWorld newRoom) // overload to accept a room class 
    {
        
        ChangeRoom(newRoom.baseCam, newRoom.zoomCam, newRoom.InDayLight, newRoom.GlobalLightIntensity);
    }

    public void setIsZoomed(bool n)
    {

        GameObject.FindWithTag("MainCamera").GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 3; // so that the cameras have a cool blend animation
        isZoomed = n;
        baseCam.Priority = 1; // the base camera's default priority
        if (isZoomed)
        {
            
            ZoomCam.Priority = 2; // if zoom is true then make the zoom cam priority more than the base cam
        }
        else
        {
            ZoomCam.Priority = -1; // if it is not zoomed then its priority is less than the base cam
        }
    }
    private int counter_total;
    private CinemachineVirtualCamera ObaseCam;
    private CinemachineVirtualCamera OZoomCam;
    public void blackOut(float seconds) // seconds must be a multiple of 0.05
    {
        if (!baseCam.Equals(BlackOutCam))
        {
            counter_total = (int)(seconds * 20);
            // blackout must be greater than one minute
            this.baseCam.gameObject.SetActive(false);
            this.ZoomCam.gameObject.SetActive(false); // disables the cameras

            ObaseCam = baseCam;
            OZoomCam = ZoomCam;

            this.baseCam = BlackOutCam;
            this.ZoomCam = BlackOutCam;
            this.baseCam.gameObject.SetActive(true);

            StartCoroutine("AlphaChange");

            //GameObject.Find("player").SetActive(false);
            FuncTimer.Create(endBlackOut, seconds);
        }
    }
    private int counter;
    IEnumerator AlphaChange()
    {

        while (counter < counter_total-1)
        {
            if (counter > counter_total - 5)
            {
                Debug.Log("entered here");
                
                if (BlackOutCam.GetComponent<CinemachineStoryboard>().m_Alpha >= 20)
                    BlackOutCam.GetComponent<CinemachineStoryboard>().m_Alpha -= 20;
            }
            else if(counter == counter_total - 5)
            {
                GameObject.Find("player").GetComponent<PlayerControl>().enablecontrol();

            }

            else if (counter < 9)
            {
                
                
                if (BlackOutCam.GetComponent<CinemachineStoryboard>().m_Alpha <= 80)
                    BlackOutCam.GetComponent<CinemachineStoryboard>().m_Alpha += 20;
            }
            else if(counter == 10)
            {
                GameObject.Find("player").GetComponent<PlayerControl>().disablecontrol();
            }


            counter++;
            yield return new WaitForSeconds(0.05f);
        }
        
    }
    public void endBlackOut()
    {
        counter = 0; // reset the counter variable
        
        this.baseCam.gameObject.SetActive(false); // so that the player cant move during the black out
        this.baseCam = ObaseCam;
        this.ZoomCam = OZoomCam;

        this.baseCam.gameObject.SetActive(true);
        this.ZoomCam.gameObject.SetActive(true);

        baseCam.Priority = 1; // the base camera's default priority
        if (isZoomed)
        {
            // sets the zoom value for the new camera
            ZoomCam.Priority = 2; // if zoom is true then make the zoom cam priority more than the base cam
        }
        else
        {
            ZoomCam.Priority = -1; // if it is not zoomed then its priority is less than the base cam
        }
    }
}
