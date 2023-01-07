using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*

Loads the given scene when the given Button is pressed.

*/

public class LoadSceneOnButton : MonoBehaviour
{
    public string scene_name;
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
      if(button.GetComponent<PublicButton>().PressedState()) {
        SceneManager.LoadScene(scene_name);
        Debug.Log("YAY");
      }
    }

    // Update is called once per frame
    void Update()
    {
      if(button.GetComponent<PublicButton>().PressedState()) {
        SceneManager.LoadScene(scene_name);
        Debug.Log("YAY");
      }
    }
}
