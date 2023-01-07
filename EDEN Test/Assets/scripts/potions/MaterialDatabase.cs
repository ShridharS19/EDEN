using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDatabase
{
    public static MaterialP[] MasterList = {
      new MaterialP("Kainan stone", 0, 1, 1, 0, -1),
      new MaterialP("Luminol", -1, 0, 1, 0, 1),
      new MaterialP("Lapis", -1, 0, -1, 1, 0),
      new MaterialP("Hermit crystal", 0, 0, 1, 1, 1),
      new MaterialP("Elixir", 1, 1, 0, 0, 1),
      new MaterialP("Aconital", -1, -1, 0, 0, -1),
      new MaterialP("Devil's fruit", 0, 0, -1, -1, -1),
      new MaterialP("Eulerium", -1, 0, 1, 1, 0),
      new MaterialP("Lunawood", 0, 1, -1, 1, 0),
      new MaterialP("Triphala", 1, -1, 0, -1, 0),
      new MaterialP("Calcite water", -1, 0, -1, 0, 1),
      new MaterialP("Fools vearth", 0, -1, 1, 0, -1),
    };
    // the above list needs to be filled up

    public static MaterialP[] getMaterialList()
    {
        return MasterList;
    }

}
