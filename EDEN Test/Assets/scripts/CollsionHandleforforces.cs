using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionHandleforforces : MonoBehaviour
{
    public bool hasCollided;
    public Vector3 position;
    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if(gameObject.CompareTag("Enemy"))
        {
            if (!hasCollided)
                position = GetComponent<Rigidbody2D>().position;
            else
                GetComponent<Rigidbody2D>().position = position;
        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            hasCollided = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            hasCollided = false;
        }
    }
}


