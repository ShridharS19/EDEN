using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingDernardTrapDoor : MonoBehaviour
{
	Animator animationDoor;
    bool elecSys;
	void Start()
	{
        
        GameObject.Find("ShopKeeper").GetComponent<ShopKeeLibrarianConvoTrigger>().OnTalkWithShopKeeper += FindingDernardTrapDoor_OnStartFindingDernard;
		animationDoor = this.GetComponent<Animator>(); // gets the animator component from this gamobject
        animationDoor.enabled = false;
    }

    private void FindingDernardTrapDoor_OnStartFindingDernard(object sender, GameObject e)
    {
        animationDoor.enabled = true;// this will make the trap door visible
        animationDoor.SetBool("entered", true); // open door animation
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
        if(animationDoor.enabled)
        {
            if (elecSys)
            {
                if (collision.gameObject.CompareTag("Player")) // if it is the player
                    animationDoor.SetBool("entered", true); // open door animation
            }
            else
            {
                if (collision.gameObject.CompareTag("Player")) // if it is the player
                    animationDoor.SetBool("entered", false); // close door animation
            }
        }
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
        if (animationDoor.enabled)
        {
            if (elecSys)
            {
                if (collision.gameObject.CompareTag("Player")) // if it is the player
                    animationDoor.SetBool("entered", false); // close door animation
            }
            else
            {
                if (collision.gameObject.CompareTag("Player")) // if it is the player
                    animationDoor.SetBool("entered", true); // open door animation
            }
        }
    }
	
}
