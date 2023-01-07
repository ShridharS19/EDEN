using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

This class contains all the save data for the player. Instances of this class can be initialised. Then, it is stored as a binary file. When the player reloads, it is converted back and
the data is transfered back to the scene variables.

*/

[Serializable]
public class SaveState
{
  //PLAYER DATA
  //Position
  public float[] player_position;//
  public string scene_name;//

  //Health
  public float max_health;//
  public float current_health;//

//---------------------------------------------------

  //INVENTORY
  //Items
  public int[,] items;//
  public int blocks;
  public int arrows;
  public int current_potion;
  public int money;//

  //Weapons
  public int acquired_weapons;//
  public int current_weapon;//

  //Orbs
  public int[] acquired_orbs;//
  public int active_orb1;//
  public int active_orb2;//

  //Materials and Custom Potions
  public int[] material_amounts;
  public PotionStorage[] custom_potions;

//---------------------------------------------------


  //Constructor
  public SaveState(float[] pos, string scene, float mh, float ch, int[,] inv, int b, int a, int cp, int m, int aw, int cw, int[] ao, int ao1, int ao2, int[] mat_amt, PotionStorage[] pot) {
    player_position = pos;
    scene_name = scene;

    max_health = mh;
    current_health = ch;

    items = inv;
    blocks = b;
    arrows = a;
    current_potion = cw;
    money = m;

    acquired_weapons = aw;
    current_weapon = cw;

    acquired_orbs = ao;
    active_orb1 = ao1;
    active_orb2 = ao2;

    material_amounts = mat_amt;
    custom_potions = pot;

  }

}
