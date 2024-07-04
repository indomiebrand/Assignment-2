using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> items; // list to hold the interactables

    private void Start()
    {
        items = new List<GameObject>(); // initializes the inventory as an empty list
    }

    public void AddItem(GameObject item)
    {
        items.Add(item);
        Debug.Log(item + " has been added to the inventory.");
    }

    public bool HasItem(GameObject item)
    {
        return items.Contains(item);
    }
}

