using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collsion_with_player : MonoBehaviour
{
    private GameObject ItemGameObject;
    private GameObject armour_inventory;// to access the add orbs method
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ItemGameObject = GameObject.FindGameObjectWithTag("item_UI");
        armour_inventory = GameObject.FindGameObjectWithTag("armourInv");
        // works with the inventory to place the item / orb in the inventory if the player collides with it
        if (collision.gameObject.CompareTag("Player")) // if the player collided with it
        {
            types_of_pickables Instant = GetComponent<types_of_pickables>();
            if (Instant.GetpickableType() == types_of_pickables.ItemType.Potion)// if the item is not a orb
            {
                for (int a = 0; a < Instant.GetNumberOfItems(); a++) // this will add the number of necessary items
                    ItemGameObject.GetComponent<ItemInventoryData>().addItem(GetComponent<types_of_pickables>().get_item_code());

            }
            else if (Instant.GetpickableType() == types_of_pickables.ItemType.Orb) // if the item is a orb
            {
                armour_inventory.GetComponent<HideOrbs>().addOrb(GetComponent<types_of_pickables>().get_item_code());
            }
            else if(Instant.GetpickableType() == types_of_pickables.ItemType.Block) // if it is a block
            {
                GameObject.FindGameObjectWithTag("block_UI").GetComponent<BlockUI>().incrementBlocks(GetComponent<types_of_pickables>().GetNumberOfItems()); // adds the specified amount of arrow to the arrow inventory
            }
            else if(Instant.GetpickableType() == types_of_pickables.ItemType.arrow)
            {
                GameObject.FindGameObjectWithTag("arrowDisplay_UI").GetComponent<ArrowInventory>().incrementArrows(GetComponent<types_of_pickables>().GetNumberOfItems()); // adds the specified amount of arrow to the arrow inventory
            }
            
            Destroy(gameObject); // destroy the pickable item
        }
    }
}
