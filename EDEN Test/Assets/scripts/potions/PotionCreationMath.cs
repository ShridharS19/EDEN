using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCreationMath
{


        public static MaterialP Calculate(MaterialP[] array, out float TimerOfPotion) // the array is of size 5
        {
        // all items are of type 00xyz
        // 00111 and 00-1-1-1 are the rarest and rest are commoner
        //
        // there are 4 materials that you need to input



            MaterialP left;
            MaterialP[] adder = { array[0], array[1], array[2] };
            left = MaterialP.Add(adder);
            MaterialP right;
            right = MaterialP.ConstantMult(array[3], 3);
            float mag = correlation(left, right);
            float time = 1 / mag; // the time * number of teh 4th potion you put in = time

            //MaterialP final = MaterialP.ConstantMult(left, (mag * 14.2857f)/ 3);
            MaterialP final = MaterialP.ConstantMult(left, mag*3);
            //MaterialP final = MaterialP.ConstantMult(left, mag);

        TimerOfPotion = time;
        return final;



        }
        public static float correlation(MaterialP a1, MaterialP a2) // these are 5 length
        {

            float output = 19 - a1.AbsDifference(a2);
            return Mathf.CeilToInt(output/ 3);

        }

}
