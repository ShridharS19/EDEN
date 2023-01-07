using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

This class will be used to transfer data between scenes

Data that needs to be transfered:
1) Is inventory open?
2) Items stored in inventory (2D array of Item IDs)
3) Orb Order
4) Orbs activated
5) Number of weapons the player has
6) Number of the current weapon
7) Money (Works irrespective of local variables, only one global static variable for money)

*/

public class DataMaster : MonoBehaviour
{
    public static float[] player_position;
    public static float max_health;
    public static float current_health;

    public static int blocks;
    public static int arrows;
    public static int current_potion;

    public static bool inv_open;     //1)
    public static int[,] inventory; //2)

    public static int[] order;    //3)
    public static bool[] active; //4)

    public static int weapon_num;      //5)
    public static int current_weapon; //6)

    public static int money; //7)

    public static int[] material_amounts;
    public static PotionStorage[] custom_potions; //Stores the custom potions that the player has

    public GameObject itemsInv;
    public GameObject items;
    public GameObject armour;
    public GameObject weapon;
    public GameObject player;
    public GameObject blockUI;
    public GameObject Arrows;
    public GameObject PotionLauncher;

   // public static MagnetPuzzleData MagData = new MagnetPuzzleData();

    public static bool auto_init = true;
    public DataInit init;

    public static SaveState save;

      // Start is called before the first frame update
     //Sets the scene variables
    //Checks if there is SaveState Available. Else Checks if auto initialiser is available. Else sets default values.
    void Start()
    {
      //If save Data Available
      if(save != null) {

        bool[] actO = new bool[save.acquired_orbs.Length];

        for(int i = 0; i < actO.Length; i++) {
          if(save.acquired_orbs[i] == save.active_orb1 || save.acquired_orbs[i] == save.active_orb2) {
            actO[i] = true;
          }
        }

        inv_open = false;                           //1)
        inventory = save.items;                    //2)
        order = save.acquired_orbs;               //3)
        active = actO;                           //4)
        weapon_num = save.acquired_weapons;     //5)
        current_weapon = save.current_weapon;  //6)
        money = save.money;                   //7)

        //---------------------------EXTRA-------------------------------//

        player_position = save.player_position;

        max_health = save.max_health;
        current_health = save.current_health;

        blocks = save.blocks;
        arrows = save.arrows;
        current_potion = save.current_potion;

        player.transform.position = new Vector3(player_position[0], player_position[1], player_position[2]);
        player.GetComponent<Health_manager>().Set_HP(save.max_health);
        player.GetComponent<Health_manager>().setCurrentHealth(save.current_health);

        blocks = save.blocks;
        arrows = save.arrows;
        current_potion = save.current_potion;

        blockUI.GetComponent<BlockUI>().setTotalBlocks(blocks);
        Arrows.GetComponent<ArrowInventory>().setArrows(arrows);
        PotionLauncher.GetComponent<PotionLauncherSettings>().setCurrentPotionNum(current_potion);

        //---------------------------------------------------------------//

        //1)
        (itemsInv.GetComponent(typeof(InventoryDown)) as InventoryDown).makeInvisible();
        (armour.GetComponent(typeof(InventroyLeft)) as InventroyLeft).makeInvisible();
        (weapon.GetComponent(typeof(InventoryRight)) as InventoryRight).makeInvisible();

        //2)
        (items.GetComponent(typeof(ItemInventoryData)) as ItemInventoryData).setInventory(save.items);
        (items.GetComponent(typeof(ItemInventoryData)) as ItemInventoryData).reloadGameObjects();

        //3)
        (armour.GetComponent(typeof(HideOrbs)) as HideOrbs).order = order;

        //4)
        (armour.GetComponent(typeof(HideOrbs)) as HideOrbs).active = active;

        //5)
        (weapon.GetComponent(typeof(ManageWeapons)) as ManageWeapons).weapon_num = weapon_num;

        //6)
        (weapon.GetComponent(typeof(ManageWeapons)) as ManageWeapons).current_weapon = current_weapon;

        material_amounts = save.material_amounts;
        custom_potions = save.custom_potions;

        auto_init = false;
        save = null;
      //If auto_initialiser is available
      } else if(auto_init) {
        //Init DataMaster
        inv_open = init.inv_open;                   //1)
        inventory = init.inventory;                //2)
        order = init.order;                       //3)
        active = init.active;                    //4)
        weapon_num = init.weapon_num;           //5)
        current_weapon = init.current_weapon;  //6)
        money = init.money;                   //7)

        //Init Scene variables from init

        //1)
        if(init.inv_open) {
          (itemsInv.GetComponent(typeof(InventoryDown)) as InventoryDown).makeVisible();
          (armour.GetComponent(typeof(InventroyLeft)) as InventroyLeft).makeVisible();
          (weapon.GetComponent(typeof(InventoryRight)) as InventoryRight).makeVisible();
        } else {
          (itemsInv.GetComponent(typeof(InventoryDown)) as InventoryDown).makeInvisible();
          (armour.GetComponent(typeof(InventroyLeft)) as InventroyLeft).makeInvisible();
          (weapon.GetComponent(typeof(InventoryRight)) as InventoryRight).makeInvisible();
        }

        //2)
        (items.GetComponent(typeof(ItemInventoryData)) as ItemInventoryData).setInventory(init.inventory);
        (items.GetComponent(typeof(ItemInventoryData)) as ItemInventoryData).reloadGameObjects();

        //3)
        (armour.GetComponent(typeof(HideOrbs)) as HideOrbs).order = init.order;

        //4)
        (armour.GetComponent(typeof(HideOrbs)) as HideOrbs).active = init.active;

        //5)
        (weapon.GetComponent(typeof(ManageWeapons)) as ManageWeapons).weapon_num = init.weapon_num;

        //6)
        (weapon.GetComponent(typeof(ManageWeapons)) as ManageWeapons).current_weapon = init.current_weapon;

        material_amounts = new int[12];
        custom_potions = new PotionStorage[4];

        for(int i = 0; i < 12; i++) {
          material_amounts[i] = i;
        }

        auto_init = false;

      //If auto_initialiser is not available
      } else {
        //Init Scene variables from Master

        player.transform.position = new Vector3(player_position[0], player_position[1], player_position[2]);
        player.GetComponent<Health_manager>().Set_HP(max_health);
        player.GetComponent<Health_manager>().setCurrentHealth(current_health);

        blockUI.GetComponent<BlockUI>().setTotalBlocks(blocks);
        Arrows.GetComponent<ArrowInventory>().setArrows(arrows);
        PotionLauncher.GetComponent<PotionLauncherSettings>().setCurrentPotionNum(current_potion);

        //1)
        if(inv_open) {
          (itemsInv.GetComponent(typeof(InventoryDown)) as InventoryDown).makeVisible();
          (armour.GetComponent(typeof(InventroyLeft)) as InventroyLeft).makeVisible();
          (weapon.GetComponent(typeof(InventoryRight)) as InventoryRight).makeVisible();
        } else {
          (itemsInv.GetComponent(typeof(InventoryDown)) as InventoryDown).makeInvisible();
          (armour.GetComponent(typeof(InventroyLeft)) as InventroyLeft).makeInvisible();
          (weapon.GetComponent(typeof(InventoryRight)) as InventoryRight).makeInvisible();
        }

        //2)
        (items.GetComponent(typeof(ItemInventoryData)) as ItemInventoryData).setInventory(inventory);
        (items.GetComponent(typeof(ItemInventoryData)) as ItemInventoryData).reloadGameObjects();

        //3)
        (armour.GetComponent(typeof(HideOrbs)) as HideOrbs).order = order;

        //4)
        (armour.GetComponent(typeof(HideOrbs)) as HideOrbs).active = active;

        //5)
        (weapon.GetComponent(typeof(ManageWeapons)) as ManageWeapons).weapon_num = weapon_num;

        //6)
        (weapon.GetComponent(typeof(ManageWeapons)) as ManageWeapons).current_weapon = current_weapon;
      }

      //Debug.Log(inventoryToString());
      //Debug.Log((items.GetComponent(typeof(ItemInventoryData)) as ItemInventoryData).toString());
    }

     // Update is called once per frame
    //Updates static variables with scene variables
    void Update()
    {
      //Updates static variables with scene variables

      //1)
      inv_open = (itemsInv.GetComponent(typeof(InventoryDown)) as InventoryDown).isVisible;

      //2)
      inventory = (items.GetComponent(typeof(ItemInventoryData)) as ItemInventoryData).getInventory();

      //3)
      order = (armour.GetComponent(typeof(HideOrbs)) as HideOrbs).order;

      //4)
      active = (armour.GetComponent(typeof(HideOrbs)) as HideOrbs).active;

      //5)
      weapon_num = (weapon.GetComponent(typeof(ManageWeapons)) as ManageWeapons).weapon_num;

      //6)
      current_weapon = (weapon.GetComponent(typeof(ManageWeapons)) as ManageWeapons).current_weapon;

      //Debug.Log(inventoryToString());
      //Debug.Log((items.GetComponent(typeof(ItemInventoryData)) as ItemInventoryData).toString());

      Vector3 playerPos = player.transform.position;

      player_position = new float[3];
      player_position[0] = playerPos.x;
      player_position[1] = playerPos.y;
      player_position[2] = playerPos.z;

      max_health = player.GetComponent<Health_manager>().Get_HP();;
      current_health = player.GetComponent<Health_manager>().getCurrentHealth();

      blocks = blockUI.GetComponent<BlockUI>().getBlocks();
      arrows = Arrows.GetComponent<ArrowInventory>().getArrows();
      current_potion = PotionLauncher.GetComponent<PotionLauncherSettings>().getCurrentPotion();
    }

    //Returns the inventory saved in Data Master as a string
    public string inventoryToString() {
      int height = 3;
      int width = 15;
      string inv_str = "[[";
      for(int i = 0; i < height; i++) {
        for(int j = 0; j < width; j++) {
          inv_str = inv_str + inventory[i, j].ToString();
          if(j != width - 1) {
            inv_str = inv_str + ", ";
          } else {
            inv_str = inv_str + "] ";
          }
        }
        if(i != height - 1) {
          inv_str = inv_str + ",\n";
        } else {
          inv_str = inv_str + "]";
        }
      }

      return(inv_str);
    }

    //returns the amount of money that the player has
    public static int getMoney() {
      return(money);
    }
}
