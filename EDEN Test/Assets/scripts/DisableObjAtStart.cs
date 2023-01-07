using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Disables the GameObject obj at the start

*/

public class DisableObjAtStart : MonoBehaviour
{

    public GameObject obj; //GameObject to be disabled at the beginning

    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
