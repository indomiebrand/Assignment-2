using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //list to store items the player has picked up
    public List<GameObject> inventoryItems = new List<GameObject>();

    public void AddItem(GameObject item) // adds an item to inventory
    {
        if (!inventoryItems.Contains(item)) // checks if the item exists in the inventory
        {
            inventoryItems.Add(item);
            Debug.Log("Item added to inventory: " + item.name);
        }
        else
        {
            Debug.Log("Item already in inventory: " + item.name);
        }
    }

    public bool HasItem(GameObject item)
    {
        return inventoryItems.Contains(item); //checks if the item is in the inventory and returns a boolean value
    }
}
