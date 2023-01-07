using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetection : MonoBehaviour
{
    private bool isIN = false;
    private Collider2D player;

    /*private void Update()
    {

        if (isIN)
        {


            if (Input.GetKeyDown(KeyCode.Space))

            {

                if (player.gameObject.CompareTag("Player"))
                {
                    if ((player.gameObject.GetComponent<DetectBlocks>().getTests()) > 0)
                    {
                        Debug.Log("True");
                        Debug.Log((GameObject.Find("Player").GetComponent<DetectBlocks>().getTests()));
                    }

                }
                Debug.Log("false");
                player.gameObject.GetComponent<DetectBlocks>().changeTests(-1);

            }

        }
        else
            if (Input.GetKeyUp(KeyCode.Space))
            Debug.Log("Wrong");

    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision;
        if(collision.gameObject.CompareTag("Player"))
        isIN = true;

    }
    /*void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("entered");
            
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            if (other.gameObject.CompareTag("player"))
            {
                if ((other.gameObject.GetComponent<DetectBlocks>().getTests()) > 0)
                {
                    Debug.Log("True");
                    Debug.Log((GameObject.Find("player").GetComponent<DetectBlocks>().getTests()));
                }
            }
            other.gameObject.GetComponent<DetectBlocks>().changeTests(-1);
        } 
    }*/
    private void OnTriggerExit2D(Collider2D collision)
    {
        isIN = false;
    }

    public bool getStatus()
    {
        return isIN;
    }

}