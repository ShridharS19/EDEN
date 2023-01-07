using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionColourSetter : MonoBehaviour
{
    public float[] arrayOfStats; // its a array of values between 1 and 100 percent
    public GameObject melee_strip;
    public GameObject projectile_strip;
    public GameObject speed_strip;
    public GameObject HP_strip;
    public GameObject defence_strip;
    // Start is called before the first frame update
    void Start()
    {
        arrayOfStats = new float[5];
        //SetStripColors(); // this is default which is going to set the entire thing to white
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public GameObject SetArrayOfStats(MaterialP mat)
    {
        float[] array = new float[5];
        array[0] = mat.melee;
        array[1] = mat.projectile;
        array[2] = mat.speed;
        array[3] = mat.HP;
        array[4] = mat.defence;
        return SetArrayOfStats(array);

    }
    public GameObject SetArrayOfStats(MaterialP mat1, MaterialP mat2, MaterialP mat3, MaterialP cat, float cat_number, out float timer_len)
    {
        MaterialP[] array = new MaterialP[4];
        array[0] = mat1;
        array[1] = mat2;
        array[2] = mat3;
        array[3] = cat;
        float timer;
        MaterialP effectOfPotion = PotionCreationMath.Calculate(array, out timer);
        timer_len = 30 + (int)(210 * timer * cat_number); // this is the weighted formula for timer

        Debug.Log(effectOfPotion.ToString());

        float[] array2 = new float[5];
        array2[0] = effectOfPotion.melee;
        array2[1] = effectOfPotion.projectile;
        array2[2] = effectOfPotion.speed;
        array2[3] = effectOfPotion.HP;
        array2[4] = effectOfPotion.defence;

        /*for(int i = 0 ; i < array2.Length; i++) {
          Debug.Log(array2[i]);
        }*/

        return SetArrayOfStats(array2);

    }

    public GameObject SetArrayOfStats(float[] array)
    {
        arrayOfStats = array;

        /*for(int i = 0 ; i < arrayOfStats.Length; i++) {
          Debug.Log(arrayOfStats[i]);
        }*/

        SetStripColors();
        return gameObject;
    }

    public float[] GetArrayOfStat()
    {
        return arrayOfStats;
    }

    private void SetStripColors()
    {
        // changes all the colors based on the stats array
        melee_strip.GetComponent<SpriteRenderer>().color = createColor(arrayOfStats[0]);
        projectile_strip.GetComponent<SpriteRenderer>().color = createColor(arrayOfStats[1]);
        speed_strip.GetComponent<SpriteRenderer>().color = createColor(arrayOfStats[2]);
        HP_strip.GetComponent<SpriteRenderer>().color = createColor(arrayOfStats[3]);
        defence_strip.GetComponent<SpriteRenderer>().color = createColor(arrayOfStats[4]);
    }

    private Color createColor(float n) // creates a color given a percent
    {
        float value = n / 100;// gives a value between 0 and 100
        //Debug.Log("The value is " + value);
        if (n > 0)
        {

            value = 1f - value;
            return new Color(value, value, value);
        }
        else if (n < 0)
        {
            value += 1f;
            Color output =  new Color(1f, value, value);
            Debug.Log(output.ToString());
            return output;

        }
        else // if  n = 0
        {

            return new Color(1f, 1f, 1f); // this is white
        }


    }
}
