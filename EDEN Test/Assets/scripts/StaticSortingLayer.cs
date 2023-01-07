using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSortingLayer : MonoBehaviour
{

    GameObject entity;
    // Start is called before the first frame update
    void Start()
    {
      entity = gameObject;
      SpriteRenderer s = entity.GetComponent<SpriteRenderer>();
      s.sortingOrder = (int)Mathf.Floor(-100*(transform.position.y-entity.GetComponent<SpriteRenderer>().bounds.size.y/2));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
