using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*

This class can be triggered with a button to load the first scene.

*/

public class StartGame : MonoBehaviour
{

    public string first_scene;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadScene() {
      SaveState save = SaveSystem.Load();
      if(save != null) {
        LoadSystem.Load(save);
      } else {
        SceneManager.LoadScene(first_scene);
      }
    }
}
