using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using UnityEngine;

public class CutTest : MonoBehaviour
{
    bool IsIn = false;
    public GameObject TM;
    // Start is called before the first frame update
    void Awake()
    {
        TM.SetActive(false);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsIn)
        {
            TM.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Here");
        IsIn = true;
    }
}
