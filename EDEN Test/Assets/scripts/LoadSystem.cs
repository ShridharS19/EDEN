using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSystem
{
    public static void Load(SaveState save) {
      DataMaster.save = save;
      DataMaster.money = save.money;
      SceneManager.LoadScene(save.scene_name);
    }
}
