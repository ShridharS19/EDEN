using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

Correctly displays the amount of money on the UI

*/

public class MoneyCount : MonoBehaviour
{
    public GameObject text;       //Reference to the gameobject with the text to be changed
    int money = 0;

    // Start is called before the first frame update
    void Start()
    {
      money = DataMaster.getMoney();
      updateText();
    }

    // Update is called once per frame
    void Update()
    {
      money = DataMaster.getMoney();
      updateText();
    }

    public void updateText() {
      text.GetComponent<Text>().text = "$" + money.ToString();
    }
}
