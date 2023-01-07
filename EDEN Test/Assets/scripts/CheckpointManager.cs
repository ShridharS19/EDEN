using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*

Detects when a player hots the Checkpoint
Instansiates and Initializes an instance of SaveState
Makes and stores it as a binary file.
(Does not handle any of the loading of the saved file)

*/

public class CheckpointManager : MonoBehaviour
{
    public GameObject ItemInventory;
    public GameObject BlockSettings;
    public GameObject ArrowSettings;
    public GameObject PotionSettings;
    public GameObject WeaponInventory;
    public GameObject OrbInventory;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Checks if there is a collision between the player and the Checkpoint
    private void OnTriggerEnter2D(Collider2D collision) {
      if(collision.gameObject.CompareTag("Player")) {
        createNewSaveState();
      }
    }

    //Manages the process for creating a new save
    private void createNewSaveState() {
      //Debug.Log("Need to save now!!");

      float[] pos = {gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z};
      string s = SceneManager.GetActiveScene().name;

      float mh = player.GetComponent<Health_manager>().Get_HP();
      float ch = player.GetComponent<Health_manager>().getCurrentHealth();

      int[,] inv = ItemInventory.GetComponent<ItemInventoryData>().getInventory();
      int b = BlockSettings.GetComponent<BlockUI>().getBlocks();
      int a = ArrowSettings.GetComponent<ArrowInventory>().getArrows();
      int cp = PotionSettings.GetComponent<PotionLauncherSettings>().getCurrentPotion();
      int m = DataMaster.money;

      int aw = WeaponInventory.GetComponent<ManageWeapons>().weapon_num;
      int cw = WeaponInventory.GetComponent<ManageWeapons>().getActiveWeapon();

      int[] ao = OrbInventory.GetComponent<HideOrbs>().getOrder();
      int[] activeOrbs = OrbInventory.GetComponent<HideOrbs>().getActiveOrbIDs();
      int ao1 = activeOrbs[0];
      int ao2 = activeOrbs[1];

      int[] mat_amt = DataMaster.material_amounts;
      PotionStorage[] pot = DataMaster.custom_potions;

      SaveState data = new SaveState(pos, s, mh, ch, inv, b, a, cp, m, aw, cw, ao, ao1, ao2, mat_amt, pot);

      SaveSystem.Save(data);
    }
}
