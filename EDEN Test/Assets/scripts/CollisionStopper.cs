using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionStopper : MonoBehaviour
{
    public string[] StopWithTags;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D r = gameObject.GetComponent<Rigidbody2D>();
        foreach (string i in StopWithTags)
        {
            if (collision.gameObject.tag == i)
            {
                r.isKinematic = true;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D r = gameObject.GetComponent<Rigidbody2D>();
        foreach (string i in StopWithTags) { 
        if (collision.gameObject.tag==i)
        {
                r.isKinematic = true;
        } 
    }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        foreach (string i in StopWithTags)
        {
            Rigidbody2D r = gameObject.GetComponent<Rigidbody2D>();
            if (collision.gameObject.tag == i)
            {
                r.isKinematic = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
