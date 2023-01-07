using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //class to handle player money
    private int moneyamount;
    private string money;
    // Start is called before the first frame update
    void Start()// use to instantiate moneybar once made
    {
        
    }

    public void changeMoney(int additionamount)//used to both add and subtract money, enter a negative value to subtract
    {
        moneyamount += additionamount;
    }

    // Update is called once per frame
    void Update()// use to update moneybar once made
    {
        money = moneyamount.ToString();
    }
    public int getMoney()// returns money value if needed for whatever reason
    {
        return moneyamount;
    }

    public string returnMoney()
    {
        return money;
    }
}
