using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightBlinker : MonoBehaviour
{
    Light2D Light;  // Start is called before the first frame update
    void Start()
    {
        Light = GetComponent<Light2D>();
        StartCoroutine(Blink());
    }

   
    IEnumerator Blink()
    {
        
        while (true)
        {
            
            Light.intensity = 0;
            yield return new WaitForSeconds(0.3f);
            Light.intensity = 2;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
