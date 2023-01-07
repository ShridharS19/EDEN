using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementALL : MonoBehaviour
{
    public float speed;[SerializeField]
    private Vector3 change;
    Rigidbody2D player_rigidBody;
    public bool isAI = false; // it changes this value automatically in the start function
    void Start()
    {

        if(gameObject.CompareTag("Enemy"))
        {
            isAI = true;
        }
        else if(gameObject.CompareTag("Player") || gameObject.CompareTag("DummyPlayer"))
        {
            isAI = false;
        }
        player_rigidBody = GetComponent<Rigidbody2D>();



    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isAI)
        {
            change = Vector3.zero;// when new frame reset change

            change.x = Input.GetAxisRaw("Horizontal"); // for input right and left using ad and arrow keys
            if (Vector3.zero == change) // if no horizontal key stroke detectd
                change.y = Input.GetAxisRaw("Vertical");
            if (change != Vector3.zero)
            {
                Move_character();

            }
        }
        else
        {
            AImovement();
        }


    }

    void Move_character()
    {
        player_rigidBody.MovePosition(

            transform.position + change.normalized * speed  //change .normalize gives a unit vector, the multiplication with delta time and speed is done to comtrol the speed
        );
    }
    public Vector3 Turn_dir() // returns the direction the player is facing at a point in time can be used by other game objects hence it is public
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            return new Vector3(0, 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            return new Vector3(0, -1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            return new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return new Vector3(-1, 0, 0);
        }
        return Vector3.zero;
    }
    private void AImovement()
    {
        // todo this is the enemy AI shridhar
    }
}
