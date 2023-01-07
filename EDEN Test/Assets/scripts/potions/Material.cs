using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MaterialP

{

    // to store a single material
    // all of these are floats to perform the average operation
    public string materialName;
    public float melee;
    public float speed;
    public float defence;
    public float projectile;
    public float HP;

    public MaterialP(string materialName, float melee, float speed, float defence, float projectile, float HP)
    {
        this.materialName = materialName;
        this.melee = melee;
        this.speed = speed;
        this.defence = defence;
        this.projectile = projectile;
        this.HP = HP;

    }

    public MaterialP MaterialOut()
    {
        return this;
    }

    public float AbsDifference(MaterialP a)
    {
        float sum = 0;
        sum += Mathf.Abs(a.melee - melee);
        sum += Mathf.Abs(a.speed - speed);
        sum += Mathf.Abs(a.defence - defence);
        sum += Mathf.Abs(a.HP - HP);
        sum += Mathf.Abs(a.projectile - projectile);
        return sum;

    }

    public static MaterialP Add(MaterialP[] array)
    {
        float size = array.Length;
        float speedf = 0;
        float meleef = 0;
        float HPf = 0;
        float defencef = 0;
        float projectilef = 0;
        for(int a = 0; a < size; a++)
        {
            speedf += array[a].speed;
            HPf += array[a].HP;
            defencef += array[a].defence;
            projectilef += array[a].projectile;
            meleef += array[a].melee;
        }
        return new MaterialP("", meleef, speedf, defencef, projectilef, HPf);

    }

    public static MaterialP Average(MaterialP[] array)
    {
        int size = array.Length;
        MaterialP output = Add(array);
        return new MaterialP("", output.melee / size, output.speed / size, output.defence / size, output.projectile / size, output.HP / size);
    }

    public static MaterialP ConstantMult(MaterialP a, float k)
    {
        return new MaterialP("", a.melee * k, a.speed * k, a.defence * k, a.projectile * k, a.HP * k);
    }

    public override string ToString() {
      string name = materialName + " " + melee.ToString() + " " + speed.ToString() + " " + defence.ToString() + " " + projectile.ToString() + " " + HP.ToString();
      return(name);
    }
}
