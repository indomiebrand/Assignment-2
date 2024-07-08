using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> inventoryItems = new List<GameObject>();

    public void AddItem(GameObject item)
    {
        if (!inventoryItems.Contains(item))
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
        return inventoryItems.Contains(item);
    }
}
