using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockAttributes : MonoBehaviour

{
    private float damageOnCollsion = 100;
    public Transform Target;
    private GameObject builder;
    private float speed = 0.05f;

    private bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if(Input.GetKeyDown(KeyCode.T))
        {
            SetIsTriggered(true);
        }*/

        if(isTriggered)
        gameObject.transform.position = Vector3.Lerp(transform.position, Target.position, speed);

    }

    public float GetDamageOnCollision()
    {
        return damageOnCollsion;
    }

    public void SetDamageOnCollision(float s)
    {
        damageOnCollsion = s;
    }

  

    public void Setspeed(float t)
    {
        speed = t; 
    }

    public float GetSpeed()
    {
        return speed;
    }


    

    public bool GetIsTriggered()
    {
        return isTriggered;
    }

    public void SetIsTriggered(bool t)
    {
        isTriggered = t;
    }

    public Transform GetTarget()
    {
        return Target;
    }

    public void SetTarget(Transform tar)
    {
        Target = tar;
    }

    public GameObject getBuilder()
    {
        return builder;
    }

    public void setBuilder(GameObject b)
    {
        builder = b;
    } 

}
