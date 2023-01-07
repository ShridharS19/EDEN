using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInit : MonoBehaviour
{
  public bool inv_open;                      //1)
  public int[,] inventory = new int[3, 15]; //2)

  public int[] order;    //3)
  public bool[] active; //4)

  public int weapon_num;      //5)
  public int current_weapon; //6)

  public int money; //7)

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
