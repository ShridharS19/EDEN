using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopKeeLibrarianConvoTrigger : MonoBehaviour

{
    
    public Dialogue[] ForTheFindingDernardPuzzle; [SerializeField]
    public event EventHandler<GameObject> OnTalkWithShopKeeper;
    private void Start()
    {
        GameObject.Find("ComputerbossRoom").GetComponent<MainTrigger_bossRoom>().OnStartFindingDernard += ShopKeeLibrarianConvoTrigger_OnStartFindingDernard;
    }

    private void ShopKeeLibrarianConvoTrigger_OnStartFindingDernard(object sender, GameObject e)
    {

        GetComponent<dialogue_trigger>().inputdialogue(ForTheFindingDernardPuzzle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.name == "ShopKeeper") // if it is the shopkeeper
        {
            OnTalkWithShopKeeper?.Invoke(this, gameObject);
        }
        Debug.Log("entered the radius of the librarian/shopkeeper");
        if(collision.gameObject.CompareTag("Player"))
        {
            GetComponent<dialogue_trigger>().StartDialogue();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<dialogue_trigger>().EndDialogue();
        }
    }
}
