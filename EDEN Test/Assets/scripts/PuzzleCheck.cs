using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PuzzleCheck : MonoBehaviour
{
    public Transform smallBoulder1;
    public Transform smallBoulder2;
    public GameObject SB1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        smallBoulder1 = SB1.transform;
        if (((smallBoulder1.position.y <= -4.5) && (smallBoulder1.position.y >= -5.5)) && ((smallBoulder1.position.x <= -2.5) && (smallBoulder1.position.x >= -3.5)))
        {
            SB1.transform.Translate(3f,3f,0f);
        }
        //Debug.Log("nope"); // commented by arnav cause it was pissing off
    }
}
