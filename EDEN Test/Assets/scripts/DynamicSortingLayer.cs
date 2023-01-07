using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSortingLayer : MonoBehaviour
{
    GameObject entity;
    // Start is called before the first frame update
    void Start()
    {
        entity = gameObject;
        //s = GetComponent<SpriteRenderer>();
        //s.sortingOrder = -transform.position.y;
        SpriteRenderer s = entity.GetComponent<SpriteRenderer>();
        s.sortingOrder = (int)Mathf.Ceil(-100*(transform.position.y-entity.GetComponent<SpriteRenderer>().bounds.size.y/2));
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer s = entity.GetComponent<SpriteRenderer>();
        s.sortingOrder = (int)Mathf.Ceil(-100*(transform.position.y-entity.GetComponent<SpriteRenderer>().bounds.size.y/2));
    }
}
