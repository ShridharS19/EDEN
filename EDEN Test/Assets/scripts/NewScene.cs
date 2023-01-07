using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*

This script is used to load a new scene

*/

public class NewScene : MonoBehaviour
{

    public string scene_name; //Stores the name of the scene that you want to switch to

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
      //Debug.Log("Collision");
      if(collision.gameObject.CompareTag("Player")) {
        SceneManager.LoadScene(scene_name);
      }
    }
}
