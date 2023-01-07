using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBlocks : MonoBehaviour
{
    private int tests = 2;
    private bool trigered;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            {
                if (tests > 0)
                {
                    trigered = (GameObject.Find("Magcolliderdiagonal").GetComponent<BlockDetection>().getStatus() || GameObject.Find("Magcollidervert").GetComponent<BlockDetection>().getStatus() || GameObject.Find("MagcolliderHori").GetComponent<BlockDetection>().getStatus());
                    if (trigered)
                    {
                        Debug.Log("HIT");
                    }
                    else
                    {
                    Debug.Log("Miss");
                    }
                    tests--;
                }
            }
    }

    public void AddTests(int amount)
    {
        tests += amount;
    }
    public void SetTests(int amount)
    {
        tests = amount;

    }
    public int getTests()
    {
        return tests;
    }
}
