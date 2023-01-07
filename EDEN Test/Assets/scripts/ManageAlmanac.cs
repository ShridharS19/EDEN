using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageAlmanac : MonoBehaviour
{

    private bool open = false;
    public GameObject almanacText;


    // Start is called before the first frame update
    void Start()
    {
      Close();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void Open() {
      gameObject.transform.position = new Vector3(Screen.width/2, Screen.height/2, 0);
      open = true;
    }

    private void Close() {
      gameObject.transform.position = new Vector3(-Screen.width/2, -Screen.height/2, 0);
      open = false;
    }

    public void toggleView() {
      if(open) {
        Close();
      } else {
        Open();
      }
    }

    public bool isOpen() {
      return(open);
    }

    public void replaceText(string text) {
      almanacText.GetComponent<Text>().text = text;
    }
}
