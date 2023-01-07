using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
 * all the timers must be handled from this class
 * to make a timer call the static method create and to stop the timer mid way call the stopTimer static function
 * the create function takes in an action -> basically a function which has a return type  of void and has no parameters
 * it calls this function after the given number of seconds specified as a float
 * it also has an optional timer name which you can use later as a way to stop the timer midway.
 * 
 */

public class FuncTimer
{
    private static List<FuncTimer> activeTimerList; // store all the timers in the game
    private static GameObject initGameobject;
    private Action action;
    private float timer;
    private bool Isdestroyed;
    private GameObject gameobject;
    private string timer_name;
    private FuncTimer(Action action, float timer, string timer_name, GameObject gameobject)
    {
        this.timer_name = timer_name;
        this.action = action;
        this.timer = timer;
        this.gameobject = gameobject;

    }
    private static void InitIfneeded()

    {
        if (initGameobject == null) // if the gameobject does not exist in the scene
        {
            initGameobject = new GameObject("FunctionTimer_initgamobj");
            activeTimerList = new List<FuncTimer>();
        }
    }

    public static FuncTimer Create(Action action, float seconds, string time_name = null)
    {
        InitIfneeded();
        GameObject function_timer = new GameObject("functimer", typeof(MonoBeehaviourLink));
        FuncTimer Functiontimer = new FuncTimer(action, seconds,time_name, function_timer);
        
        function_timer.GetComponent<MonoBeehaviourLink>().OnUpdate = Functiontimer.Update; // repeats the update function of the Functimer class every frame
        activeTimerList.Add(Functiontimer);
        return Functiontimer;

    }

    private static void removeTimer(FuncTimer timer) // to remove a timer from the list
    {
        InitIfneeded();
        activeTimerList.Remove(timer);
    }

    public static float secondsLeftForTimer(string Timer_name) // the amount of time remainging a certain timer
    {
        if (!(activeTimerList == null))
        {
            for (int i = 0; i < activeTimerList.Count; i++)
            {
                
                if (activeTimerList[i].timer_name == Timer_name)
                {
                    return activeTimerList[i].getseconds();
                    
                }
            }
        }
        return -1f;
    }

    public static float secondsLeftForTimer(FuncTimer Timer) // overloaded for FuncTImer type
    {
        return Timer.getseconds();
        
    }

    public static bool addSeconds(String Timer, float n) // add number of seconds to a timer
    {
        if (!(activeTimerList == null))
        {
            for (int i = 0; i < activeTimerList.Count; i++)
            {
                if (activeTimerList[i].timer_name == Timer)
                {
                    activeTimerList[i].addseconds(n);
                    return true;
                }
            }
        }
        return false;
    }
    public static void addSeconds(FuncTimer Timer, float n) // overloaded
    {
        Timer.addseconds(n);

    }

    public static void stopTimer(string Timer_name) // to stop a timermidway
    {
        if (!(activeTimerList == null))
        {
            for (int i = 0; i < activeTimerList.Count; i++)
            {
                if (activeTimerList[i].timer_name == Timer_name)
                {
                    // stop timer
                    activeTimerList[i].Destroy_self();
                    i--;
                }
            }
        }
    }

    public static void stopTimer(FuncTimer timer) // overload the above
    {
        if (!(activeTimerList == null))
        {
            for (int i = 0; i < activeTimerList.Count; i++)
            {
                if (activeTimerList[i] == timer)
                {
                    // stop timer
                    activeTimerList[i].Destroy_self();
                    break;
                }
            }
        }
    }
    public void Update() // runs every second called from the monobehaviour link
    {
        if (!Isdestroyed)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                action();
                Destroy_self();
            }
        }
    }

    private float getseconds()
    {
        return timer;
    }

    private void addseconds(float n)
    {
        timer += n;
    }
    public void Destroy_self() // destroys the currect timer object
    {
        Isdestroyed = true;
        UnityEngine.Object.Destroy(gameobject); // destoys the gameobject which is running the timer
        removeTimer(this);
    }
}


    public class MonoBeehaviourLink : MonoBehaviour // dummy class to access monobehavious for the update func
    {
        public Action OnUpdate;
        public void Update()
        {
            if (OnUpdate  != null)
                OnUpdate();
                
        }
    }
 

    

  