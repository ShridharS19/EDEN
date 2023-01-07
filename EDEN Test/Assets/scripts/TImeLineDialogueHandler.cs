using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineDialogueHandler : MonoBehaviour
{
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        active = GameObject.Find("dialogue").activeSelf;
        if (active)
        {
            GameObject.Find("TimelineManager").GetComponent<PlayableDirector>().Pause();
        }
        else
        {
            GameObject.Find("TimelineManager").GetComponent<PlayableDirector>().Play();
        }
    }
}