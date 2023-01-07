using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PotionStorage
{
    MaterialP stats;
    float timer_len;

    public PotionStorage(MaterialP s, float t) {
      stats = s;
      timer_len = t;
    }

    public MaterialP getStats() {
      return(stats);
    }

    public float getTimer() {
      return(timer_len);
    }

    public void logStats() {
      Debug.Log(stats.ToString());
    }
}
