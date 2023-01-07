using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraTargeControl : MonoBehaviour
{
    private GameObject target;

    /*private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null && GameObject.FindGameObjectWithTag("DummyPlayer") != null)  // if a dummy is created
        {
            SetTarget(GameObject.FindGameObjectWithTag("DummyPlayer")); 
        }
        else if(GameObject.FindGameObjectWithTag("Player") != null) // if only the player is there
        {
            SetTarget(GameObject.FindGameObjectWithTag("Player"));
        }
    }*/
    // i would do the above but its too expensive as it checks every frame so im only calling this when a dummy is created

    public void SetTarget(GameObject t)
    {
        target =  t;
        ChangeAllCams();
    }

    public GameObject GetTarget()
    {
        return target;
    }

    private void ChangeAllCams()
    {
        // sets all the cinemachine cameras to look at the current target this is to handle for dummies
       GameObject[] cameras =  GameObject.FindGameObjectsWithTag("camera");
       foreach(GameObject i in cameras)
        {
            if (target != null)
                i.GetComponent<CinemachineVirtualCamera>().Follow = target.transform;
            else
                i.GetComponent<CinemachineVirtualCamera>().Follow = null;
        }

     
    }


}
